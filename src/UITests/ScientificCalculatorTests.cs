using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using UITests.TestFramework;
using System.Threading;

namespace UITests
{
    [TestFixture]
    public class ScientificCalculatorTests
    {
        private WindowsDriver<WindowsElement> _driver;
        private StandardCalculatorPage _StandardCalculatorPage;
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

            _StandardCalculatorPage = new StandardCalculatorPage(_driver);
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

            // Ensure that calculator is in scientific mode
            if (!header.Text.Equals("Scientific", StringComparison.OrdinalIgnoreCase))
            {
                _driver.FindElementByAccessibilityId("TogglePaneButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var menu = _driver.FindElementByClassName("SplitViewPane");
                menu.FindElementByName("Scientific Calculator").Click();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                Assert.IsTrue(header.Text.Equals("Scientific", StringComparison.OrdinalIgnoreCase));
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
            _StandardCalculatorPage.Addition(5, 7);
            Assert.AreEqual("12", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void Division()
        {
            _StandardCalculatorPage.Division(8, 1);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void Multiplication()
        {
            _StandardCalculatorPage.Multiplication(9, 9);
            Assert.AreEqual("81", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void Subtraction()
        {
            _StandardCalculatorPage.Subtraction(9, 1);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
        }
    }
}
