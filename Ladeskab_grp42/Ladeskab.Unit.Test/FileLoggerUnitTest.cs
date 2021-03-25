using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    class FileLoggerUnitTest
    {
        private FileLogger _uut;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void FileLoggerUnitTest_NoFileWrite_NewFileCreated()
        {
            var fakeFileWriter = Substitute.For<FileWriter>();
            var fakeFileReader = Substitute.For<FileReader>();

            _uut = new FileLogger(fakeFileWriter, fakeFileReader);



            Console.getCurrentdirectory

            //Open() for at se om findes
            //
            //Bliver fil oprettet
            //read funktion så kan om er skrevet rigtigt
            //Assert does exist
        }



    }
}
