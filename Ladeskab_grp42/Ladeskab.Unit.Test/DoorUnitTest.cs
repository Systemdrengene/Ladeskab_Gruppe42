using Ladeskab.Lib;
using NSubstitute;
using NUnit.Framework;


namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class DoorUnitTest
    {
	    private Door _uut;

	    [SetUp]
	    public void Setup()
	    {
		    _uut = new Door();
	    }

		#region ClosedDoorTests
		//Test der lukker door

		//Close Door
		[Test]
		public void DoorUnitTest_CloseDoor_DoorClosed()
		{
			//Arrange
			var result = _uut.OnDoorOpen();  // doorOpen True
			//Act
			result = _uut.OnDoorClose();  // doorOpen False

			//Assert
			Assert.IsFalse(result);  //Expected false
		}

		// Close Door
		[Test]
		public void DoorUnitTest_onDoorClose_Notify()
		{
			//arrange  - Open door so we can close door with notify
			_uut.OnDoorClose(); //  doorOpen = false
			//Act
			_uut.OnDoorClose();  // doorOpen = false
			//Assert

			Assert.That(_uut.OnDoorClose, Is.False);

		}


		//Test UnlockDoor is unlocked
		[Test]
		public void DoorUnitTest_unLockClosedDoor_DoorIsLocked()
		{
			// Arrange
			_uut.OnDoorClose();
			//Act
			bool result = _uut.UnlockDoor(); //  Doorunlocked = true
			//Assert

			Assert.IsTrue(result);  // Expected = True

		}
		//Test LockDoor is locked
		[Test]
		public void DoorUnitTest_NotifyTestCloseDoor_DoorClosed()
		{
			_uut.UnlockDoor();  //Lås op
			_uut.OnDoorOpen();  // Open door
			//Act

			var obs = Substitute.For<IObserver>();
			_uut.Attach(obs);
			_uut.OnDoorClose();  //Close door
			obs.Received().Update(_uut, "Door closed");

		}


		#endregion


		#region OpenDoorTests


		// Open Door
		[Test]
		public void DoorUnitTest_OpenDoor_DoorOpen()
		{
			//Arrange

			//Act
			var result = _uut.OnDoorClose();  // doorOpen False
			result = _uut.OnDoorOpen();  // doorOpen True

			//Assert
			Assert.IsFalse(result);  //Expected true
		}


		// Door Open
		[Test]
		public void DoorUnitTest_onDoorOpen_Notify()
		{
			//arrange  - Open door so we can close door with notify
			//_uut.OnDoorClose();  //Notify door open
			_uut.UnlockDoor();
			//Act

			var obs = Substitute.For<IObserver>();
			_uut.Attach(obs);
			_uut.OnDoorOpen();
			obs.Received().Update(_uut,"Door opened");

			//Assert


		}

		
		#endregion









    }
}