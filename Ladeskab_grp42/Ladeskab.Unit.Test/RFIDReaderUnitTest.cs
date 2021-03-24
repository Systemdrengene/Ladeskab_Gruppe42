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
        public void OnRfidReadTest()
        {
            var obs = Substitute.For<IObserver>();
            _uut.Attach(obs);
            _uut.OnRfidRead(2);
            obs.ReceivedWithAnyArgs().Update(_uut, "RFID");
        }

        [Test]
        public void GetIDTest_DefaultValue_Exception()
        {
            Assert.Throws<Exception>(() => _uut.GetID(), "RFID hasn't been read!");
        }

        [Test]
        public void GetIDTest_PositiveValue()
        {
            _uut.Id = 2;
            Assert.That(_uut.GetID(), Is.EqualTo(2));
        }

        [Test]
        public void GetIDTest_NegativeValue()
        {
            _uut.Id = -2;
            Assert.That(_uut.GetID(), Is.EqualTo(-2));
        }

        public void FullTest_PositiveIDValue()
        {
            var obs = Substitute.For<IObserver>();
            _uut.Attach(obs);
            _uut.OnRfidRead(2);
            obs.ReceivedWithAnyArgs().Update(_uut, "RFID");
            Assert.That(_uut.GetID(), Is.EqualTo(2));
        }
    }
}