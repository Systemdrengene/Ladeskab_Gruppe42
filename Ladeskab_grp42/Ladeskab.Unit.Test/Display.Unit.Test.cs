using Ladeskab.Lib;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace Ladeskab.Unit.Test
{
	[TestFixture]
	public class DisplayTest
	{
		private Display uut;

		[SetUp]
		public void Setup()
		{
			uut = new Display();
		}

		[Test]
		public void DisplayTest_ShowUserMessage_TextPrintToConsole()
		{
			// Arrange
			StringWriter sw = new();
			Console.SetOut(sw);

			//Act
			uut.UpdateUserMsg("Text");
            //Assert
            var expected = uut.Separator + "\r\n" + "Text" + "\r\n" + "" + "\r\n" + uut.Separator + "\r\n";

            Assert.That(sw.ToString(), Is.EqualTo(expected));
        }
        [Test]
        public void DisplayTest_ShowChargeMessage_TextPrintToConsole()
        {
            // Arrange
            StringWriter sw = new();
            Console.SetOut(sw);

            //Act
            uut.UpdateChargeMsg("Text");
            //Assert
            var expected = uut.Separator + "\r\n" + "" + "\r\n" + "Text" + "\r\n" + uut.Separator + "\r\n";

            Assert.That(sw.ToString(), Is.EqualTo(expected));
        }
        [Test]
        public void DisplayTest_ShowBothMessages_TextPrintToConsole()
        {
            // Arrange
            StringWriter sw = new();
            Console.SetOut(sw);

            //Act
            uut.UpdateUserMsg("Text");

            // Remove previous text from Console
            StringBuilder sb = sw.GetStringBuilder();
            sb.Remove(0, sb.Length);

            uut.UpdateChargeMsg("Text");

            //Assert
            var expected = uut.Separator + "\r\n" + "Text" + "\r\n" + "Text" + "\r\n" + uut.Separator + "\r\n";

            Assert.That(sw.ToString(), Is.EqualTo(expected));
        }
    }
}