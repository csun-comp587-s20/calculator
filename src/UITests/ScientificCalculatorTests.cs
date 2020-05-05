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
        private ScientificCalculatorPage _ScientificCalculatorPage;
        private CalculatorResults _calculatorResults;
        private static WindowsElement header;
        private static WindowsElement calculatorResult;

        #region Init
        [SetUp]
        public void TestInit()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _ScientificCalculatorPage = new ScientificCalculatorPage(_driver);
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
        #endregion

        [Test]
        public void FactorialTest()
        {
            string[] answers = new string[] {"1", "1", "2", "6", "24", "120", "720", "5040", "40320", "362880"};
            for(int i = 0; i < answers.Length; i++)
            {
                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.FactorialBtn.Click();
                Assert.AreEqual(answers[i], _calculatorResults.ReturnCalculatorResults());
            }
        }

        [Test]
        public void OrderOfOperationsTest()
        {
            string exp1 = "1+2+(";
            string exp2 = "*6)/10";
            _ScientificCalculatorPage.EvaluateStringInput(exp1);
            _ScientificCalculatorPage.Power(5,3);
            _ScientificCalculatorPage.EvaluateStringInput(exp2);
            Assert.AreEqual("78", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void LogTests()
        {
            for (int i = 0; i < 10; i++)
            {
                _ScientificCalculatorPage.NaturalLog(i);
                if (i == 0)
                {
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                }
                else
                {
                    string ans = Math.Log((double)i).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }
                _ScientificCalculatorPage.LogBase10(i);
                if (i == 0)
                {
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                }
                else
                {
                    string ans = Math.Log10((double)i).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }
            }


        }

        [Test]
        public void AreaOfCirlceTest()
        {
            for(int i = 0; i < 10; i++)
            {
                string ans = (Math.PI * Math.Pow((double)i, 2.0)).ToString();
                _ScientificCalculatorPage.AreaOfCircle(i);
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));

            }
        }
        [Test]
        public void AbsoluteValueTest()
        {
            Random rand = new Random();
            for(int i = 0; i < 10; i++)
            {
                int pos = rand.Next(1, 1000000000);
                int neg = -pos;
                _ScientificCalculatorPage.PressNumber(neg);
                _ScientificCalculatorPage.AbsBtn.Click();
                Assert.AreEqual(pos.ToString(),_calculatorResults.ReturnCalculatorResults());

            }
        }
    }
}
