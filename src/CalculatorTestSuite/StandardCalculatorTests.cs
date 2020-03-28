using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using CalculatorTestSuite.TestFramework;
using System.Threading;

namespace CalculatorTestSuite
{
    [TestFixture]
    public class StandardCalculatorTests
    {
        private WindowsDriver<WindowsElement> _driver;
        private StandardCalculatorPage _calcStandardView;
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

            _calcStandardView = new StandardCalculatorPage(_driver);

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
            if (!header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase))
            {
                _driver.FindElementByAccessibilityId("TogglePaneButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var menu = _driver.FindElementByClassName("SplitViewPane");
                menu.FindElementByName("Standard Calculator").Click();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                Assert.IsTrue(header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase));
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
        public void Addition()
        {
            _calcStandardView.Addition(5,7);
            _calcStandardView.EqualsBtn.Click();
            Assert.AreEqual("12", _calcStandardView.ReturnCalculatorResults());
        }

        [Test]
        public void Division()
        {
            _calcStandardView.Division(8, 1);
            _calcStandardView.EqualsBtn.Click();
            Assert.AreEqual("8", _calcStandardView.ReturnCalculatorResults());
        }

        [Test]
        public void Multiplication()
        {
            _calcStandardView.Multiplication(9, 9);
            _calcStandardView.EqualsBtn.Click();
            Assert.AreEqual("81", _calcStandardView.ReturnCalculatorResults());
        }

        [Test]
        public void Subtraction()
        {
            _calcStandardView.Subtraction(9, 1);
            _calcStandardView.EqualsBtn.Click();
            Assert.AreEqual("8", _calcStandardView.ReturnCalculatorResults());
        }
    }
}