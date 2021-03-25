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
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);

            //fakeFileWriter.WriteFile("$(SolutionDir)/log.txt", "hello")

            _uut.LogFile("This is log 1");
            _uut.LogFile("This is log 2");
            _uut.LogFile("This is log 3");

            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 1");
            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 2");
            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 3");

            Assert.AreEqual(File.Exists("$(SolutionDir)/log.txt"), true);
            Assert.AreEqual(_uut.ReadFile(), "This is log 3");

            File.Delete("./.log.txt");
        }

        [Test]
        public void FileLoggerUnitTest_FileExistWrite_FileWrittenTo()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            File.Create("./log.txt");

            _uut.LogFile("This is log 1");
            _uut.LogFile("This is log 2");
            _uut.LogFile("This is log 3");

            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 1");
            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 2");
            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 3");

            Assert.AreEqual(_uut.ReadFile(), "This is log 3");

            File.Delete("./log.txt");
        }

        [Test]
        public void FileLoggerUnitTest_NoFileExistWriteRead_NewFileRead()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);

            _uut.LogFile("This is log 1");
            Assert.AreEqual(_uut.ReadFile(), "This is log 1");

            _uut.LogFile("This is log 2");
            Assert.AreEqual(_uut.ReadFile(), "This is log 2");

            _uut.LogFile("This is log 3");
            Assert.AreEqual(_uut.ReadFile(), "This is log 3");

            File.Delete("./log.txt");
        }

        [Test]
        public void FileLoggerUnitTest_FileExistWriteRead_FileRead()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            File.Create("./log.txt");

            _uut.LogFile("This is log 1");
            Assert.AreEqual(_uut.ReadFile(), "This is log 1");

            _uut.LogFile("This is log 2");
            Assert.AreEqual(_uut.ReadFile(), "This is log 2");

            _uut.LogFile("This is log 3");
            Assert.AreEqual(_uut.ReadFile(), "This is log 3");

            File.Delete("./log.txt");
        }

        public void FileLoggerUnitTest_FileExistRead_FileRead()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            File.Create("./log.txt");
            _uut.ReadFile();

            FileReader.Received().ReadFile("./log.txt");
        }

        public void FileLoggerUnitTest_NoFileExistRead_FileRead()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            _uut.ReadFile();

            FileReader.Received().ReadFile("./log.txt");
        }
    }
}
