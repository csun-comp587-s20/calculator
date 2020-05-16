using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using UITests.TestFramework;
using System.Threading;

namespace UITests
{
    class ProgrammerCalculatorTests
    {
        private WindowsDriver<WindowsElement> _driver;
        private ProgrammerCalculatorPage _programmerCalculatorPage;
        private CalculatorResults _calculatorResults;
        private static WindowsElement header;
        private static WindowsElement calculatorResult;

        [SetUp]
        public void TestInit()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _programmerCalculatorPage = new ProgrammerCalculatorPage(_driver);
            _calculatorResults = new CalculatorResults(_driver);

            // Identify calculator mode by locating the header
            try
            {
                header = _driver.FindElementByAccessibilityId("Header");
            }
            catch
            {
                header = _driver.FindElementByAccessibilityId("ContentPresenter");
            }

            // Ensure that calculator is in standard mode
            if (!header.Text.Equals("Programmer", StringComparison.OrdinalIgnoreCase))
            {
                _driver.FindElementByName("Open Navigation").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var menu = _driver.FindElementByClassName("SplitViewPane");
                menu.FindElementByName("Programmer Calculator").Click();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                Assert.IsTrue(header.Text.Equals("Programmer", StringComparison.OrdinalIgnoreCase));
            }

            // Locate the calculatorResult element
            calculatorResult = _driver.FindElementByAccessibilityId("CalculatorResults");
            Assert.IsNotNull(calculatorResult);
        }

        [TearDown]
        public void TestCleanup()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        [Test]
        public void BitwiseTest()
        {
            // AND
            _programmerCalculatorPage.Bitwise(1, "AND", 0);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "AND", 1);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "AND", 0);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(1, "AND", 1);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.ClearEntryBtn.Click();
            // OR
            _programmerCalculatorPage.Bitwise(1, "OR", 0);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "OR", 1);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "OR", 0);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(1, "OR", 1);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.ClearEntryBtn.Click();
            // NAND
            _programmerCalculatorPage.Bitwise(1, "NAND", 0);
            Assert.AreEqual("-1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "NAND", 1);
            Assert.AreEqual("-1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "NAND", 0);
            Assert.AreEqual("-1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(1, "NAND", 1);
            Assert.AreEqual("-2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.ClearEntryBtn.Click();
            // NOR
            _programmerCalculatorPage.Bitwise(1, "NOR", 0);
            Assert.AreEqual("-2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "NOR", 1);
            Assert.AreEqual("-2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(1, "NOR", 1);
            Assert.AreEqual("-2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "NOR", 0);
            Assert.AreEqual("-1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.ClearEntryBtn.Click();
            // XOR
            _programmerCalculatorPage.Bitwise(1, "XOR", 1);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "XOR", 0);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(1, "XOR", 0);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Bitwise(0, "XOR", 1);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void NotTest()
        {
            // NOT
            _programmerCalculatorPage.Not(1);
            Assert.AreEqual("-2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Not(0);
            Assert.AreEqual("-1", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Not(-1);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.Not(-2);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void BitShiftTest1()
        {
            // Logical Shift
            _programmerCalculatorPage.BitShiftBtn.Click();
            _programmerCalculatorPage.LogicalShiftRadioBtn.Click();
            _programmerCalculatorPage.BitShift(1, "Lsh", 2, true);
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("64", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(64, "Rsh", 2, true);
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.ClearEntryBtn.Click();

            // Arithmetic Shift
            _programmerCalculatorPage.BitShiftBtn.Click();
            _programmerCalculatorPage.ArithmeticShiftRadioBtn.Click();
            _programmerCalculatorPage.BitShift(1, "Lsh", 1, false);
            Assert.AreEqual("2", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("32", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("64", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(64, "Rsh", 1, false);
            Assert.AreEqual("32", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("2", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void BitShiftTest2()
        {
            // Rotate Ciruclar Shift
            _programmerCalculatorPage.BitShiftBtn.Click();
            _programmerCalculatorPage.RotateCircularShiftRadioBtn.Click();
            _programmerCalculatorPage.BitShift(1, "Lsh", false);
            Assert.AreEqual("2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(2, "Lsh", false);
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(4, "Lsh", false);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(8, "Lsh", false);
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(16, "Lsh", false);
            Assert.AreEqual("32", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(32, "Lsh", false);
            Assert.AreEqual("64", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(64, "Rsh", false);
            Assert.AreEqual("32", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(32, "Rsh", false);
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(16, "Rsh", false);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(8, "Rsh", false);
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(4, "Rsh", false);
            Assert.AreEqual("2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.ClearEntryBtn.Click();
        }
        [Test]
        public void BitShiftTest3()
        {
            // Rotate Through Carry Ciruclar Shift
            _programmerCalculatorPage.BitShiftBtn.Click();
            _programmerCalculatorPage.RotateThroughCarryCircularShiftRadioBtn.Click();
            _programmerCalculatorPage.BitShift(1, "Lsh", true);
            Assert.AreEqual("2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(2, "Lsh", true);
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(4, "Lsh", true);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(8, "Lsh", true);
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(16, "Lsh", true);
            Assert.AreEqual("32", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(32, "Lsh", true);
            Assert.AreEqual("64", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(64, "Rsh", true);
            Assert.AreEqual("32", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(32, "Rsh", true);
            Assert.AreEqual("16", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(16, "Rsh", true);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(8, "Rsh", true);
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.BitShift(4, "Rsh", true);
            Assert.AreEqual("2", _calculatorResults.ReturnCalculatorResults());
            _programmerCalculatorPage.ClearEntryBtn.Click();
        }
    }
}
