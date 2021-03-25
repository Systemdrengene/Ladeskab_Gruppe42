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
        private string testlog = "testlog.txt";
        private string testpath = "./testlog.txt";

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void FileLoggerUnitTest_NoFileExistWrite_NewFileWrittenTo()
        {
            var fakeFileWriter = Substitute.For<FileWriter>();
            var FileReader = new FileReader();

            _uut = new FileLogger(fakeFileWriter, FileReader);

            //fakeFileWriter.WriteFile("$(SolutionDir)/testlog, "hello")

            _uut.LogFile("This is log 1");
            _uut.LogFile("This is log 2");
            _uut.LogFile("This is log 3");

            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 1");
            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 2");
            fakeFileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 3");

            Assert.AreEqual(File.Exists(testpath), true);
            Assert.AreEqual(_uut.ReadFile(), "This is log 3");

            File.Delete(testpath);
        }

        [Test]
        public void FileLoggerUnitTest_FileExistWrite_FileWrittenTo()
        {
            var fakeFileWriter = Substitute.For<FileWriter>();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            File.Create(testpath);

            _uut.LogFile("This is log 1");
            _uut.LogFile("This is log 2");
            _uut.LogFile("This is log 3");

            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 1");
            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 2");
            FileWriter.Received().WriteFile(Arg.Any<string>(), "This is log 3");

            Assert.AreEqual(_uut.ReadFile(), "This is log 3");

            File.Delete(testpath);
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

            File.Delete(testpath);
        }

        [Test]
        public void FileLoggerUnitTest_FileExistWriteRead_FileRead()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            File.Create(testpath);

            _uut.LogFile("This is log 1");
            Assert.AreEqual(_uut.ReadFile(), "This is log 1");

            _uut.LogFile("This is log 2");
            Assert.AreEqual(_uut.ReadFile(), "This is log 2");

            _uut.LogFile("This is log 3");
            Assert.AreEqual(_uut.ReadFile(), "This is log 3");

            File.Delete(testpath);
        }

        public void FileLoggerUnitTest_FileExistRead_FileRead()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            File.Create(testpath);
            _uut.ReadFile();

            FileReader.Received().ReadFile(testpath);
        }

        public void FileLoggerUnitTest_NoFileExistRead_FileRead()
        {
            var FileWriter = new FileWriter();
            var FileReader = new FileReader();

            _uut = new FileLogger(FileWriter, FileReader);
            _uut.ReadFile();

            FileReader.Received().ReadFile(testpath);
        }
    }
}
