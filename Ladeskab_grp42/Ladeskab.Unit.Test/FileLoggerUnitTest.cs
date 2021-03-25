using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using System.IO;
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
        public void FileLoggerUnitTest_NoFileExistWrite_NewFileWrittenTo()
        {
            var fakeFileWriter = Substitute.For<FileWriter>();
            var fakeFileReader = Substitute.For<FileReader>();

            _uut = new FileLogger(fakeFileWriter, fakeFileReader);

            //fakeFileWriter.WriteFile("$(SolutionDir)/log.txt", "hello")

            _uut.LogFile("This is log 1");
            _uut.LogFile("This is log 2");
            _uut.LogFile("This is log 3");

            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 1");
            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 2");
            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 3");

            Assert.AreEqual(File.Exists("$(SolutionDir)/log.txt"), true);

            //Console.getCurrentdirectory

            //Open() for at se om findes
            //
            //Bliver fil oprettet
            //read funktion så kan om er skrevet rigtigt
            //Assert does exist
        }

        [Test]
        public void FileLoggerUnitTest_FileExistWrite_FileWrittenTo()
        {
            var fakeFileWriter = Substitute.For<FileWriter>();
            var fakeFileReader = Substitute.For<FileReader>();

            _uut = new FileLogger(fakeFileWriter, fakeFileReader);
            File.Create("$(SolutionDir)/log.txt");


            _uut.LogFile("This is log 1");
            _uut.LogFile("This is log 2");
            _uut.LogFile("This is log 3");

            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 1");
            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 2");
            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 3");

            Assert.AreEqual(File.Exists("$(SolutionDir)/log.txt"), true);

        }

        [Test]
        public void FileLoggerUnitTest_NoFileExistRead_NewFileCreated()
        {

        }

        [Test]
        public void FileLoggerUnitTest_FileExistRead_NewFileCreated()
        {

        }
    }
}
