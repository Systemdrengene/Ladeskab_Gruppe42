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

        [TestCase(12)]
        [TestCase(-1)]
        [TestCase(0)]
        public void ScanRfidTag_IDTransmitted_IdRecieved(int Id)
        {
            //Arrange
            int recievedID = 0;

            //Act
            _uut.RfidEvent += (sender, args) => recievedID = args.Id;
            _uut.ScanRFfidTag(Id);
            //Assert
            Assert.That(recievedID, Is.EqualTo(Id));
        }

        //public void OnRfidReadTest()
        //{


        //    var obs = Substitute.For<IObserver>();
        //    _uut.Attach(obs);
        //    _uut.OnRfidRead(2);
        //    obs.ReceivedWithAnyArgs().Update(_uut, "RFID");
        //}

        //[Test]
        //public void GetIDTest_DefaultValue_Exception()
        //{
        //    Assert.Throws<Exception>(() => _uut.GetID(), "RFID hasn't been read!");
        //}

        //[Test]
        //public void GetIDTest_PositiveValue()
        //{
        //    _uut.Id = 2;
        //    Assert.That(_uut.GetID(), Is.EqualTo(2));
        //}

        //[Test]
        //public void GetIDTest_NegativeValue()
        //{
        //    _uut.Id = -2;
        //    Assert.That(_uut.GetID(), Is.EqualTo(-2));
        //}

        //[Test]
        //public void FullTest_PositiveIDValue()
        //{
        //    var obs = Substitute.For<IObserver>();
        //    _uut.Attach(obs);
        //    _uut.OnRfidRead(2);
        //    obs.Received(1).Update(_uut, "RFID");
        //    Assert.That(_uut.GetID(), Is.EqualTo(2));
        //}
    }
}