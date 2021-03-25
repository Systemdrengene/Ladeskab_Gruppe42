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

		[SetUp]
		public void Setup()
		{
			_fakeDoor = Substitute.For<IDoor>();
			_fakeRfidReader = Substitute.For<IRFIDReader>();
			_fakeChargeControl = Substitute.For<IChargeControl>();
			_fakeDisplay = Substitute.For<IDisplay>();
			_fileLogger = Substitute.For<FileLogger>();
			_uut = new StationControl(_fakeChargeControl, _fakeDoor, _fakeRfidReader, _fakeDisplay, _fileLogger);
		}

		//Eventuel test af dooreventhandler
		#region DoorEventHandler() 

		

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
			//var obs = Substitute.For<IObserver>();  // ? Eventuelt brug for RfidEventArgs
			//_uut.Attach(obs);
			//_uut.OnRfidRead(2);
			//obs.ReceivedWithAnyArgs().Update(_uut, "RFID");
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
			//var obs = Substitute.For<IObserver>();  // ? Eventuelt brug for RfidEventArgs
			//_uut.Attach(obs);
			//_uut.OnRfidRead(2);
			//obs.ReceivedWithAnyArgs().Update(_uut, "RFID");
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
			_fakeRfidReader.GetID();  // Har brug for OnRFIDRead -- Ellers ingen Notify

			//Assert
			_fileLogger.Received(1).ReadFile();

		}

		// Display test når RFID event og State Available Charger Connected
		[Test]
		public void RfidDetected_StateAvailableAndChargerConnect_DisplayCallOnce()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);
			//Act -- EVENT Ligesom overstående test

			//Assert
			_fakeDisplay.Received(1).DisplayMessage();
		}

		//Tester LadeskabState.Available ->> LadeskabState.Locked
		[Test]
		public void RfidDetected_StateAvailableAndChargerConnect_StateChanges()
		{
			//Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);
			//Act -- EVENT Ligesom overstående test


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


			// Assert
			_fileLogger.Received(res).ReadFile();
		}

		[Test]
		public void RfidDetected_FullCycleSimRFIDMatch_StateChangesBack()
		{
			// Arrange
			_uut._state = StationControl.LadeskabState.Available;
			_fakeChargeControl.IsConnected().Returns(true);

			// Act - Raise event in fake

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


			// Assert
			_fakeDisplay.Received(1).UpdateUserMsg("Forkert RFID tag");
		}



		#endregion



	}
}
