using Ladeskab.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class RFIDReaderUnitTest
    {
        private RFIDReader _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new RFIDReader();
        }

        [Test]
        public void ScanRfidTag_NoSubscriber_NoSucces()
        {
            _uut.ScanRFfidTag(12);  //Ingen subscribers


        }

        [Test]
        public void ScanRfidtag_1Subscriber_Notified()
        {
	        //Arrange.
            bool notified = false;
            //Act
            _uut.RfidEvent += (sender, args) => notified = true;
            _uut.ScanRFfidTag(12);
            //Assert
            Assert.IsTrue(notified);

        }

        [TestCase(12,12)]
        [TestCase(-1,0)]
        [TestCase(1,1)]
        public void ScanRfidTag_IDTransmitted_IdRecieved(int Id,int expectedid)
        {
            //Arrange
            int recievedID = 0;

            //Act
            _uut.RfidEvent += (sender, args) => recievedID = args.Id;
            _uut.ScanRFfidTag(Id);
            //Assert
            Assert.That(recievedID, Is.EqualTo(expectedid));
        }

    }
}