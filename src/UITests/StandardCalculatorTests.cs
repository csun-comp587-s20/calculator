using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using UITests.TestFramework;
using System.Threading;

namespace UITests
{
    [TestFixture]
    public class StandardCalculatorTests
    {
        private WindowsDriver<WindowsElement> _driver;
        private StandardCalculatorPage _calcStandardView;
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

            _calcStandardView = new StandardCalculatorPage(_driver);
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
        public void Division_Positive_Integers()
        {
            _calcStandardView.Division(8, 1);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void Division_Negative_Integers()
        {
            _calcStandardView.Division(1, -2);
            Assert.AreEqual("-0.5", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void Division_By_Zero()
        {
            _calcStandardView.Division(8, 0);
            Assert.AreEqual("Cannot divide by zero", _calculatorResults.ReturnCalculatorResults()); ;
        }

        [Test]
        public void Subtraction_Positive_Integers()
        {
            _calcStandardView.Subtraction(9, 1);
            Assert.AreEqual("8", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void Subtraction_Negative_Integers()
        {
            _calcStandardView.Subtraction(-9, -10);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void AdditionWithGenerator()
        {
            for (int i = 1; i < 10; i++)
            {
                string exp = SimpleArithmeticGenerator.GenerateExpression(i, new bool[] { true, false, false, false });
                string ans = SimpleArithmeticGenerator.ArithmeticEvaluator(exp);
                _calcStandardView.EvaluateStringInput(exp);
                Assert.AreEqual(ans, _calculatorResults.ReturnCalculatorResults());
                _calcStandardView.ClearBtn.Click();
            }
        }

        [Test]
        public void MultiplicationWithGenerator()
        {
            for (int i = 1; i < 5; i++)
            {
                string exp = SimpleArithmeticGenerator.GenerateExpression(i, new bool[] { false, false, false, true });
                string ans = SimpleArithmeticGenerator.ArithmeticEvaluator(exp);
                _calcStandardView.EvaluateStringInput(exp);
                Assert.AreEqual(ans, _calculatorResults.ReturnCalculatorResults());
            }
        }

        [Test]
        public void PercentageTest()
        {
            string exp = "100*25%";
            _calcStandardView.EvaluateStringInput(exp);
            Assert.AreEqual("25", _calculatorResults.ReturnCalculatorResults());

            exp = "100*587%";
            _calcStandardView.EvaluateStringInput(exp);
            Assert.AreEqual("587", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void FractionTest()
        {
            for (int i = 1; i < 6; i++)
            {
                string exp = "1/" + i;
                string ans = SimpleArithmeticGenerator.ArithmeticEvaluator(exp);
                _calcStandardView.EvaluateStringInput(exp);
                Assert.AreEqual(ans, _calculatorResults.ReturnCalculatorResults());
            }
        }

        [Test]
        public void Square_Postive_Integers()
        {
            for (int i = 0; i < 12; i++)
            {
                _calcStandardView.EvaluateStringInput(i.ToString());
                _calcStandardView.SquareBtn.Click();
                string ans = SimpleArithmeticGenerator.ArithmeticEvaluator(i + "^2");
                Assert.AreEqual(ans, _calculatorResults.ReturnCalculatorResults());
            }
        }
        [Test]
        public void Square_Negative_Integers()
        {
            _calcStandardView.PressNumber(-1);
            _calcStandardView.SquareBtn.Click();
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void SquareRootTest_Positve_Integers()
        {
            _calcStandardView.PressNumber(0);
            _calcStandardView.SquareRootBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _calcStandardView.PressNumber(1);
            _calcStandardView.SquareRootBtn.Click();
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            _calcStandardView.PressNumber(2);
            _calcStandardView.SquareRootBtn.Click();
            StringAssert.Contains("1.414213562373095‬", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void SquareRootTest_Negative_Integers()
        {
            _calcStandardView.PressNumber(-1);
            _calcStandardView.SquareRootBtn.Click();
            Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
        }

        [Test]
        public void CombinationTest()
        {
            _calcStandardView.PressNumber(1);
            _calcStandardView.AdditionBtn.Click();
            _calcStandardView.PressNumber(2);
            _calcStandardView.AdditionBtn.Click();
            _calcStandardView.PressNumber(5);
            _calcStandardView.SquareBtn.Click();
            _calcStandardView.AdditionBtn.Click();
            _calcStandardView.PressNumber(2);
            _calcStandardView.MultiplyBtn.Click();
            _calcStandardView.PressNumber(6);
            _calcStandardView.DivideBtn.Click();
            _calcStandardView.PressNumber(10);
            Assert.AreEqual("29.2", _calculatorResults.ReturnCalculatorResults());
        }
    }
}
