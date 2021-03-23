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

        [Test]
        public void DoorTest_unLockClosedDoor_DoorIsLocked()
        {
				// Arrange

				//Act
				bool result = _uut.UnlockDoor();
				//Assert

				Assert.IsTrue(result);
	        
        }
		[Test]
        public void DoorTest_LockClosedDoor_DoorIsLocked()
        {
	        // Arrange
	        //Act
	        bool result = _uut.LockDoor();
	        //Assert

	        Assert.IsFalse(result);

        }
	}
}