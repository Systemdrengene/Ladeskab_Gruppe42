using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Lib;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace Ladeskab.Unit.Test
{
	[TestFixture]
	public class StationControlUnitTest
	{
		private StationControl _uut;
		private IDoor _fakeDoor;
		private IRFIDReader _fakeRfidReader;
		private IChargeControl _fakeChargeControl;
		private IDisplay _fakeDisplay;
		private FileLogger _fileLogger;

		private IFileReader _fakeFileReader;
		private IFileWriter _fakeFileWriter;

		[SetUp]
		public void Setup()
		{
			_fakeDoor = Substitute.For<IDoor>();
			_fakeRfidReader = Substitute.For<IRFIDReader>();
			_fakeChargeControl = Substitute.For<IChargeControl>();
			_fakeDisplay = Substitute.For<IDisplay>();
			_fakeFileReader = Substitute.For<IFileReader>();
			_fakeFileWriter = Substitute.For<IFileWriter>();
			_fileLogger = Substitute.For<FileLogger>(_fakeFileWriter,_fakeFileReader);
			_uut = new StationControl(_fakeChargeControl, _fakeDoor, _fakeRfidReader, _fakeDisplay, _fileLogger);
		}

		//Eventuel test af dooreventhandler
		#region DoorEventHandler()

		//Test door event at door open
		[Test]
		public void DoorEventHandler_DoorOpenStateAvailable_stateChangeToDoorOpen()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;

			// Act - Raise event in fake
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() {DoorState = true});

			// Assert
			Assert.That(_uut._state,Is.EqualTo(StationControl.LadeskabState.DoorOpen));

		}

		[Test]
		public void DoorEventHandler_DoorOpenStateAvailable_DisplayCallOnce()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;
			//Act
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() {DoorState = true});
			//Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Tilslut Telefon");
			_fakeDisplay.Received(0).UpdateUserMsg("Indlæs RFID");	
		}

		[Test]
		public void DoorEventHandler_DoorOpenStateDoorOpen_AlreadyOpen()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.DoorOpen;

			//Act
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() { DoorState = true });

			//Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Door cannot open when state = DoorOpen");
		}

		[Test]
		public void DoorEventHandler_DoorOpenStateLocked_CannotOpen()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Locked;

			//Act
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() { DoorState = true });

			//Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Door cannot open when state = Locked");
		}

		//Test når State Available men DoorState = False 
		[Test]
		public void DoorEventHandler_DoorClosedStateAvailable_CannotOpen()
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;

			// Act
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() { DoorState = false });

			// Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Door cannot close when state is Available");

		}

		[Test]
		public void DoorEventHandler_DoorClosedStateDoorOpen_StateChangeToAvailable()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.DoorOpen;

			//Act
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() { DoorState = false });

			//Assert
			Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
		}

		[Test]
		public void DoorEventHandler_DoorClosedStateDoorOpen_DisplayCallOnce()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.DoorOpen;

			//Act
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() { DoorState = false });

			//Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Indlæs RFID");
		}

		[Test]
		public void DoorEventHandler_DoorClosedStateLocked_CantOpen()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Locked;

			//Act
			_fakeDoor.DoorEvent +=
				Raise.EventWith(new DoorEventArgs() { DoorState = false });

			//Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Door cannot open when state = Locked");
		}

		#endregion

		#region  RFIDDetected

		//Test låsning af door med RFid event
		[Test]
		public void RFIDDetected_StateAvailableAndChargerConnected_DoorCallOnce()
		{

			//Arrange
			_uut._state = StationControl.LadeskabState.Available;  //Test Available state
			_fakeChargeControl.IsConnected().Returns(true); // Connected med usb
			//Act - Event i fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() {Id = 1});

			//Assert
			_fakeDoor.Received(1).LockDoor();

		}

		//Test af start opladning når RFid event
		[Test]
		public void RFIDDetected_StateAvailableAndChargerConnected_StartChargeCallOnce()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;  //Test Available state
			_fakeChargeControl.IsConnected().Returns(true); // Connected med usb
			//Act - Event i fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() {Id = 1});

			//Assert
			_fakeChargeControl.Received(1).StartCharge();

		}

		// Logger test om korrekt loggning af RFID Id
		[Test]
		public void RfidDetected_StateAvailableChargerConnected_LogDoorLockedCallOnce()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);
			//Act  -- Brug for EVENT
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 1 });
			//Assert
			_fileLogger.Received(1).LogFile("Skab låst med RFID: ");

		}

		// Display test når RFID event og State Available Charger Connected
		[Test]
		public void RfidDetected_StateAvailableAndChargerConnect_DisplayCallOnce()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);
			//Act -- EVENT Ligesom overstående test
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 1 });
			//Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
		}

		//Tester LadeskabState.Available ->> LadeskabState.Locked
		[Test]
		public void RfidDetected_StateAvailableAndChargerConnect_StateChanges()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);
			//Act -- EVENT Ligesom overstående test
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 1 });

			//Assert -- Available state -> locked state
			Assert.That(_uut._state,Is.EqualTo(StationControl.LadeskabState.Locked));
		}

		[Test]
		public void RfidDetected_StateAvailableAndChargerNotConnected_DisplayCalledOnce()
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(false);

			// Act - Raise event in fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 1 });

			// Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
		}
		// Tests for when rfid event is received and state=DoorOpen
		[Test]
		public void RfidDetected_StateDoorOpen_Throws()
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.DoorOpen;
			_fakeChargeControl.IsConnected().Returns(false);

			//Act -- RFID Event
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 1 });

			// Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Dør er allerede åbnet med et RF-ID");
		}


		//Tests gennemløb, RFID open close, open + 
		[TestCase(12, 12, 1)] // Samme id, kan anvende skab
		[TestCase(12, 13, 0)] // Forskelligt id så blokerer
		public void RfidDetected_FullCycleSim_DisplayCalls(int id1, int id2, int res)
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event RFIDDetected
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id1 });
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id2 });

			// Assert -- Eventuelt to forskellige, da det to forskellige beskeder ikke sikker
			_fakeDisplay.Received(res).UpdateUserMsg("Tag din telefon ud af skabet og luk døren");
		}

		[TestCase(12, 12, 1)]
		[TestCase(12, 13, 0)]
		public void RfidDetected_FullCycleSim_CallsChargerOnce(int id1, int id2, int res)
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event in fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id1 });
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id2 });

			// Assert
			_fakeChargeControl.Received(res).StopCharge();
		}

		[TestCase(12, 12, 1)]
		[TestCase(12, 13, 0)]
		public void RfidDetected_FullCycleSim_CallsDoorOnce(int id1, int id2, int res)
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event in fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id1 });
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id2 });
			// Assert
			_fakeDoor.Received(res).UnlockDoor();
		}

		[TestCase(12, 12, 1)]
		[TestCase(12, 13, 0)]
		public void RfidDetected_FullCycleSim_CallsLogfileOnce(int id1, int id2, int res)
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event in fake
			// Act - Raise event in fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id1 });
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = id2 });

			// Assert
			_fakeFileWriter.Received(res).WriteFile("./log.txt", "Skab låst op med RFID: " + id1);
		
		}

		[Test]
		public void RfidDetected_FullCycleSimRFIDMatch_StateChangesBack()
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event in fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 12 });
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 12 });
			// Assert
			Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
		}
		[Test]
		public void RfidDetected_FullCycleSimNoRFIDMatch_StateChangesBack()
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event in fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 12 });
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 13 });

			// Assert
			Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
		}
		[Test]
		public void RfidDetected_FullCycleSimNoRFIDMatch_DisplayCalledOnce()
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event in fake
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 12 });
			_fakeRfidReader.RfidEvent +=
				Raise.EventWith(new RfidEventArgs() { Id = 13 });

			// Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Forkert RFID tag");
		}



		#endregion



	}
}
