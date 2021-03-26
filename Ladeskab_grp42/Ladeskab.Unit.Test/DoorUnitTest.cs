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
			var result = _uut.OnDoorOpen(); 
			//Act
			result = _uut.OnDoorClose();  

			//Assert
			Assert.IsFalse(result);  
		}

		// Close - a closed door
		[Test]
		public void DoorUnitTest_CloseClosedDoor_doorOpenFalse()
		{
			//Arrange

			//Act
			var result = _uut.OnDoorClose(); 
			//Assert
			Assert.IsFalse(result); 
		}


		// Close a unlocked door
		[Test]
		public void DoorUnitTest_CloseUnlockedDoor_doorOpenFalse()
		{
			// Arrange
			_uut.UnlockDoor(); 

			//Act
			bool result = _uut.OnDoorClose(); 
			//Assert

			Assert.IsFalse(result);  

		}

		// Close a locked door
		[Test]
		public void DoorUnitTest_CloseLockedDoor_DoorIsLocked()
		{
			// Arrange

			//Act

			//Assert
			Assert.IsFalse(_uut.OnDoorClose());

		}



		//Test DoorEvent
		[Test]
		public void DoorUnitTest_TestDoorEvent_DoorClosed()
		{
			//Arrange
			bool notified = false;

			_uut.UnlockDoor();  
			_uut.OnDoorOpen();  
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
			_uut.UnlockDoor(); 
			//Act
			_uut.OnDoorClose(); 

			var result = _uut.OnDoorOpen();  

			//Assert
			Assert.IsTrue(result); 
		}

		// Open Open door
		[Test]
		public void DoorUnitTest_OpenOpenDoor_doorOpenTrue()
		{
			//Arrange
			_uut.UnlockDoor();
			//Act
			var result = _uut.OnDoorOpen();  
			result = _uut.OnDoorOpen();  

			//Assert
			Assert.IsTrue(result);  //Expected true
		}


		// Open a unlocked door
		[Test]
		public void DoorUnitTest_OpenUnlockedDoor_doorOpenTrue()
		{
			// Arrange
			_uut.UnlockDoor(); 


			//Act
			bool result = _uut.OnDoorOpen(); 
			//Assert

			Assert.IsTrue(result);  

		}

		// Open a locked door
		[Test]
		public void DoorUnitTest_OpenLockedDoor_doorOpenTrue()
		{
			// Arrange
            _uut.LockDoor();

			//Act
            bool result = _uut.OnDoorOpen(); 

			//Assert

			Assert.IsFalse(result); 

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
			_uut.OnDoorOpen(); 

			//Act
			var result = _uut.LockDoor();

			//Assert
			Assert.IsTrue(result);


		}


		#endregion









    }
}