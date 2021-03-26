using Ladeskab.Lib;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
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

		//Close - a Open door
		[Test]
		public void DoorUnitTest_CloseOpenDoor_doorOpenFalse()
		{
			//Arrange
			var result = _uut.OnDoorOpen();  // doorOpen True
			//Act
			result = _uut.OnDoorClose();  // doorOpen False

			//Assert
			Assert.IsFalse(result);  //Expected false
		}

		// Close - a closed door
		[Test]
		public void DoorUnitTest_CloseClosedDoor_doorOpenFalse()
		{
			//arrange
			//Act
			var result = _uut.OnDoorClose();  // doorOpen = false
			//Assert
			Assert.IsFalse(result);  //Expected false
		}


		// Close a unlocked door
		[Test]
		public void DoorUnitTest_CloseUnlockedDoor_doorOpenFalse()
		{
			// Arrange
			_uut.UnlockDoor(); //  Doorunlocked = true

			//Act
			bool result = _uut.OnDoorClose();  //doorOpen False
			//Assert

			Assert.IsFalse(result);  // Expected = false

		}

		// Close a locked door
		[Test]
		public void DoorUnitTest_CloseLockedDoor_DoorIsLocked()
		{
			// Arrange
			
			//I forvejen lukket og l�st i default
			 
			//Act

			//Assert
			Assert.IsFalse(_uut.OnDoorClose());

		}



		//Test DoorEvent
		[Test]
		public void DoorUnitTest_TestDoorEvent_DoorClosed()
		{
			bool notified = false;

			_uut.UnlockDoor();  //L�s op
			_uut.OnDoorOpen();  // Open door
			//Act
			_uut.DoorEvent += (sender, args) => notified = true;
			_uut.OnDoorClose();

			//Assert
			Assert.IsTrue(notified);

		}


		#endregion


		#region OpenDoorTests


		// Open Closed Door
		[Test]
		public void DoorUnitTest_OpenClosedDoor_doorOpenTrue()
		{
			//Arrange
			_uut.UnlockDoor(); // Unlock door s� den kan �bnes
			//Act
			_uut.OnDoorClose();  // Hav d�ren v�re lukket s� den kan �bnes

			var result = _uut.OnDoorOpen();  // doorOpen True

			//Assert
			Assert.IsTrue(result);  //Expected true
		}

		// Open Open door
		[Test]
		public void DoorUnitTest_OpenOpenDoor_doorOpenTrue()
		{
			//Arrange
			_uut.UnlockDoor();
			//Act
			var result = _uut.OnDoorOpen();  // doorOpen True
			result = _uut.OnDoorOpen();  // doorOpen True

			//Assert
			Assert.IsTrue(result);  //Expected true
		}


		// Open a unlocked door
		[Test]
		public void DoorUnitTest_OpenUnlockedDoor_doorOpenTrue()
		{
			// Arrange
			_uut.UnlockDoor(); //  Doorunlocked = true


			//Act
			bool result = _uut.OnDoorOpen();  //doorOpen True
			//Assert

			Assert.IsTrue(result);  // Expected = True

		}

		// Open a locked door
		[Test]
		public void DoorUnitTest_OpenLockedDoor_doorOpenTrue()
		{
			// Arrange
            _uut.LockDoor();

			//Act
            bool result = _uut.OnDoorOpen();  //doorOpen false fordi locked

			//Assert

			Assert.IsFalse(result);  // Expected = false

		}


		[Test]
		public void DoorUnitTest_UnlockDoor_DoorUnlockedTrue()
		{
			//Arrange
			_uut.LockDoor();
			//act
			var result = _uut.UnlockDoor();
			//Assert
			Assert.IsTrue(result);

		}

		[Test]
		public void DoorUnitTest_LockDoor_AlreadyLocked()
		{
			//Arrange
			_uut.OnDoorOpen(); //_doorOpen = true

			//Act
			var result = _uut.LockDoor();

			//Assert
			Assert.IsTrue(result);


		}


		#endregion









    }
}