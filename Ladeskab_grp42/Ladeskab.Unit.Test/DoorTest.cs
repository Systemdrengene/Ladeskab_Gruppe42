using Ladeskab.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Ladeskab.Unit.Test
{
    [TestClass]
    public class DoorTest
    {
	    private Door _uut;

	    [SetUp]
	    public void Setup()
	    {
		    _uut = new Door();
	    }


		//Test notifiy funktioner virker
	    [Test]
	    public void DoorTest_OnDoorOpenOnDoorClose_DoorOpenFalse()
	    {
			//Arrange
			
			//Act
			var result = _uut.OnDoorOpen();
			result = _uut.OnDoorClose();

			//Assert
			Assert.IsFalse(result);
	    }

		//Test UnlockDoor is unlocked
        [Test]
        public void DoorTest_unLockClosedDoor_DoorIsLocked()
        {
				// Arrange

				//Act
				bool result = _uut.UnlockDoor();
				//Assert

				Assert.IsTrue(result);
	        
        }
		//Test LockDoor is locked
		[Test]
        public void DoorTest_LockClosedDoor_DoorIsLocked()
        {
	        // Arrange
	        //Act
	        bool result = _uut.LockDoor();
	        //Assert

	        Assert.IsFalse(result);

        }

		//Test OnDoorClose 
        [Test]
        public void DoorTest_onDoorClose_Notify()
        {
			//arrange  - Open door so we can close door with notify
			_uut.OnDoorOpen();  //Notify door open

			//Act

			var result = _uut.OnDoorClose();

			//Assert

			Assert.IsFalse(result);


        }

		//Test OnDoorOpen
        [Test]
        public void DoorTest_onDoorOpen_Notify()
        {
	        //arrange  - Open door so we can close door with notify
	        _uut.OnDoorClose();  //Notify door open

	        //Act

	        var result = _uut.OnDoorOpen();

	        //Assert

	        Assert.IsTrue(result);


        }


	}
}