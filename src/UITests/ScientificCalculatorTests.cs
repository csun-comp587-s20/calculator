using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using UITests.TestFramework;
using System.Threading;
using System.Diagnostics.Tracing;

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
                _driver.FindElementByName("Open Navigation").Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
                var menu = _driver.FindElementByClassName("SplitViewPane");
                menu.FindElementByName("Scientific Calculator").Click();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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
        public void LogTest()
        {
            for (int i = 0; i < 10; i++)
            {
                _ScientificCalculatorPage.NaturalLog(i);
                if (i == 0)
                {
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                    _ScientificCalculatorPage.ClearBtn.Click();
                }
                else
                {
                    string ans = Math.Log((double)i).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                    _ScientificCalculatorPage.ClearEntryBtn.Click();
                }
                _ScientificCalculatorPage.LogBase10(i);
                if (i == 0)
                {
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                    _ScientificCalculatorPage.ClearBtn.Click();
                }
                else
                {
                    string ans = Math.Log10((double)i).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                    _ScientificCalculatorPage.ClearEntryBtn.Click();
                }
            }
        }
        [Test]
        public void LogTest2()
        {
            for(int i = 0; i < 10; i++)
            {
                _ScientificCalculatorPage.LogyX(10, i);
                if (i == 0)
                {
                    _calculatorResults.EqualsBtn.Click();
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                    _ScientificCalculatorPage.ClearBtn.Click();
                }
                else
                {
                    string ans = Math.Log10((double)i).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                    _ScientificCalculatorPage.ClearEntryBtn.Click();
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
            
            _ScientificCalculatorPage.PressNumber(100000);
            _ScientificCalculatorPage.AbsBtn.Click();
            Assert.AreEqual("100000", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void ScientificNotationTest()
        {
            _ScientificCalculatorPage.ScientificNotation("-3.6", -4);
            Assert.AreEqual("-0.00036", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.ScientificNotation("3.6", -4);
            Assert.AreEqual("0.00036", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.ScientificNotation("3.6", 4);
            Assert.AreEqual("36000", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.ScientificNotation("-3.6", 4);
            Assert.AreEqual("-36000", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void ModTest()
        {
            _ScientificCalculatorPage.PressNumber(4);
            _ScientificCalculatorPage.ModuloBtn.Click();
            _ScientificCalculatorPage.PressNumber(2);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.PressNumber(4);
            _ScientificCalculatorPage.ModuloBtn.Click();
            _ScientificCalculatorPage.PressNumber(-2);
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.PressNumber(4);
            _ScientificCalculatorPage.ModuloBtn.Click();
            _ScientificCalculatorPage.PressNumber(3);
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.PressNumber(4);
            _ScientificCalculatorPage.ModuloBtn.Click();
            _ScientificCalculatorPage.PressNumber(-3);
            Assert.AreEqual("-2", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void RootTest()
        {
            // Cubed Root
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.PressNumber(64);
            _ScientificCalculatorPage.CubeRootBtn.Click();
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.PressNumber(125);
            _ScientificCalculatorPage.CubeRootBtn.Click();
            Assert.AreEqual("5", _calculatorResults.ReturnCalculatorResults());
            // Y Root
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.PressNumber(64);
            _ScientificCalculatorPage.yRootXBtn.Click();
            _ScientificCalculatorPage.PressNumber(3);
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.PressNumber(125);
            _ScientificCalculatorPage.yRootXBtn.Click();
            _ScientificCalculatorPage.PressNumber(3);
            Assert.AreEqual("5", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void PowerBaseXTest()
        {
            for(int i = 0; i < 12; i++)
            {
                _ScientificCalculatorPage.SecondBtn.Click();
                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.twoToExponentBtn.Click();
                string ans = Math.Pow(2, i).ToString();
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                    _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                _ScientificCalculatorPage.ClearEntryBtn.Click();

                _ScientificCalculatorPage.SecondBtn.Click();
                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.eToExponentBtn.Click();
                ans = Math.Pow(Math.E, i).ToString();
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                    _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                _ScientificCalculatorPage.ClearEntryBtn.Click();
            }

        }
        [Test]
        public void TrigonometryTest1()
        {
            // Sin 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SineBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            // Inverse Sin 0
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSineBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            // Sin 30
            _ScientificCalculatorPage.PressNumber(30);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SineBtn.Click();
            Assert.AreEqual("0.5", _calculatorResults.ReturnCalculatorResults());
            // Inverse Sin 30
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSineBtn.Click();
            Assert.AreEqual("30", _calculatorResults.ReturnCalculatorResults());
            // Sin 60
            _ScientificCalculatorPage.PressNumber(60);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SineBtn.Click();
            Assert.AreEqual("0.8660254037",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Sin 60
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSineBtn.Click();
            Assert.AreEqual("60", _calculatorResults.ReturnCalculatorResults());
            // Sin 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SineBtn.Click();
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            // Inverse Sin 90
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSineBtn.Click();
            Assert.AreEqual("90", _calculatorResults.ReturnCalculatorResults());

            // Cos 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosineBtn.Click();
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            // Inverse Cos 0
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosineBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            // Cos 30
            _ScientificCalculatorPage.PressNumber(30);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosineBtn.Click();
            Assert.AreEqual("0.8660254037",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Cos 30
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosineBtn.Click();
            Assert.AreEqual("30", _calculatorResults.ReturnCalculatorResults());
            // Cos 60
            _ScientificCalculatorPage.PressNumber(60);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosineBtn.Click();
            Assert.AreEqual("0.5", _calculatorResults.ReturnCalculatorResults());
            // Inverse Cos 60
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosineBtn.Click();
            Assert.AreEqual("60", _calculatorResults.ReturnCalculatorResults());
            // Cos 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosineBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            // Inverse Cos 90
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosineBtn.Click();
            Assert.AreEqual("90", _calculatorResults.ReturnCalculatorResults());

            // Tan 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.TagentBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            // Inverse Tan 0
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcTangentBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            // Tan 30
            _ScientificCalculatorPage.PressNumber(30);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.TagentBtn.Click();
            Assert.AreEqual("0.5773502691",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Tan 30
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcTangentBtn.Click();
            Assert.AreEqual("30", _calculatorResults.ReturnCalculatorResults());
            // Tan 60
            _ScientificCalculatorPage.PressNumber(60);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.TagentBtn.Click();
            Assert.AreEqual("1.7320508075",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Tan 60
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcTangentBtn.Click();
            Assert.AreEqual("60", _calculatorResults.ReturnCalculatorResults());
            // Tan 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.TagentBtn.Click();
            Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
            // Inverse Tan 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcTangentBtn.Click();
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.TagentBtn.Click();
            Assert.AreEqual("90", _calculatorResults.ReturnCalculatorResults());

        }
        [Test]
        public void TrigonometryTest2()
        {
            // Sec 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecantBtn.Click();
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            // Inverse Sec 0
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSecantBtn.Click();
            Assert.AreEqual("0", _calculatorResults.ReturnCalculatorResults());
            // Sec 30
            _ScientificCalculatorPage.PressNumber(30);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecantBtn.Click();
            Assert.AreEqual("1.1547005383",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Sec 30
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSecantBtn.Click();
            Assert.AreEqual("30", _calculatorResults.ReturnCalculatorResults());
            // Sec 60
            _ScientificCalculatorPage.PressNumber(60);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecantBtn.Click();
            Assert.AreEqual("2",_calculatorResults.ReturnCalculatorResults());
            // Inverse Sec 60
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSecantBtn.Click();
            Assert.AreEqual("60", _calculatorResults.ReturnCalculatorResults());
            // Sec 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecantBtn.Click();
            Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
            // Inverse Sec 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcSecantBtn.Click();
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecantBtn.Click();
            Assert.AreEqual("90", _calculatorResults.ReturnCalculatorResults());

            // Csc 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosecantBtn.Click();
            Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
            // Inverse Csc 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosecantBtn.Click();
            Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
            // Csc 30
            _ScientificCalculatorPage.PressNumber(30);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosecantBtn.Click();
            Assert.AreEqual("2", _calculatorResults.ReturnCalculatorResults());
            // Inverse Csc 30
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosecantBtn.Click();
            Assert.AreEqual("30", _calculatorResults.ReturnCalculatorResults());
            // Csc 60
            _ScientificCalculatorPage.PressNumber(60);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosecantBtn.Click();
            Assert.AreEqual("1.1547005383",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Csc 60
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosecantBtn.Click();
            Assert.AreEqual("60", _calculatorResults.ReturnCalculatorResults());
            // Csc 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CosecantBtn.Click();
            Assert.AreEqual("1", _calculatorResults.ReturnCalculatorResults());
            // Inverse Csc 90
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCosecantBtn.Click();
            Assert.AreEqual("90", _calculatorResults.ReturnCalculatorResults());

            // Cot 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CotangentBtn.Click();
            Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
            // Inverse Cot 0
            _ScientificCalculatorPage.PressNumber(0);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCoTangentBtn.Click();
            Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
            // Cot 30
            _ScientificCalculatorPage.PressNumber(30);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CotangentBtn.Click();
            Assert.AreEqual("1.7320508075",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Cot 30
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCoTangentBtn.Click();
            Assert.AreEqual("30", _calculatorResults.ReturnCalculatorResults());
            // Cot 60
            _ScientificCalculatorPage.PressNumber(60);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CotangentBtn.Click();
            Assert.AreEqual("0.5773502691",
                _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
            // Inverse Cot 60
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCoTangentBtn.Click();
            Assert.AreEqual("60", _calculatorResults.ReturnCalculatorResults());
            // Cot 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CotangentBtn.Click();
            Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
            // Inverse Cot 90
            _ScientificCalculatorPage.PressNumber(90);
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.SecondBtn.Click();
            _ScientificCalculatorPage.ArcCoTangentBtn.Click();
            _ScientificCalculatorPage.TrigBtn.Click();
            _ScientificCalculatorPage.CotangentBtn.Click();
            Assert.AreEqual("90", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void TrigonometryTest3()
        {
            String ans;
            for(int i = -3; i < 3; i++)
            {
                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.TrigBtn.Click();
                _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                _ScientificCalculatorPage.HypSineBtn.Click();
                ans = Math.Sinh(i).ToString();
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                    _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));

                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.TrigBtn.Click();
                _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                _ScientificCalculatorPage.HypCosineBtn.Click();
                ans = Math.Cosh(i).ToString();
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                    _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));

                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.TrigBtn.Click();
                _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                _ScientificCalculatorPage.HypTagentBtn.Click();
                ans = Math.Tanh(i).ToString();
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                    _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));

                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.TrigBtn.Click();
                _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                _ScientificCalculatorPage.HypSecantBtn.Click();
                ans = (2 / (Math.Exp(i) + Math.Exp(-i))).ToString();
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                    _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));

                if (i == 0)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.HypCosecantBtn.Click();
                    Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
                    _ScientificCalculatorPage.ClearBtn.Click();

                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.HypCotangentBtn.Click();
                    Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
                    _ScientificCalculatorPage.ClearBtn.Click();
                }
                else
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.HypCosecantBtn.Click();
                    ans = (2 / (Math.Exp(i) - Math.Exp(-i))).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                    

                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.HypCotangentBtn.Click();
                    ans = ((Math.Exp(i) + Math.Exp(-i)) / (Math.Exp(i) - Math.Exp(-i))).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }
            }
        }
        [Test]
        public void TrigonometryTest4()
        {
            String ans;
            for (int i = -3; i < 3; i++)
            {
                _ScientificCalculatorPage.PressNumber(i);
                _ScientificCalculatorPage.TrigBtn.Click();
                _ScientificCalculatorPage.SecondBtn.Click();
                _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                _ScientificCalculatorPage.ArcHypSineBtn.Click();
                ans = Math.Log(i + Math.Sqrt(i * i + 1)).ToString();
                Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                    _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));

                if(i > 0)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypCosineBtn.Click();
                    ans = Math.Log(i + Math.Sqrt(i * i - 1)).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }

                if (i > -1 && i < 1)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypTagentBtn.Click();
                    ans = (Math.Log((1 + i) / (1 - i)) / 2).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }
                else if(i == 1)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypTagentBtn.Click();
                    Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
                }
                else if(i == -1)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypTagentBtn.Click();
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                }
                else
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypTagentBtn.Click();
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                }

                if(i == 0)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypSecantBtn.Click();
                    Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());

                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypCosecantBtn.Click();
                    Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
                }
                else
                {
                    if (i < 0 || i > 1)
                    {
                        _ScientificCalculatorPage.PressNumber(i);
                        _ScientificCalculatorPage.TrigBtn.Click();
                        _ScientificCalculatorPage.SecondBtn.Click();
                        _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                        _ScientificCalculatorPage.ArcHypSecantBtn.Click();
                        Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                    }
                    else
                    {
                        _ScientificCalculatorPage.PressNumber(i);
                        _ScientificCalculatorPage.TrigBtn.Click();
                        _ScientificCalculatorPage.SecondBtn.Click();
                        _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                        _ScientificCalculatorPage.ArcHypSecantBtn.Click();
                        ans = Math.Log((Math.Sqrt(-i * i + 1) + 1) / i).ToString();
                        Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                            _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                    }

                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypCosecantBtn.Click();
                    ans = Math.Log((Math.Sign(i) * Math.Sqrt(i * i + 1) + 1) / i).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }

                if(i > 1 && i < -1)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypCotangentBtn.Click();
                    ans = (Math.Log((i + 1) / (i - 1)) / 2).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }
                else
                {
                    if(i == 1)
                    {
                        _ScientificCalculatorPage.PressNumber(i);
                        _ScientificCalculatorPage.TrigBtn.Click();
                        _ScientificCalculatorPage.SecondBtn.Click();
                        _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                        _ScientificCalculatorPage.ArcHypCotangentBtn.Click();
                        Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
                    }
                }

                if(1 > i && i > -1)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypTagentBtn.Click();
                    ans = (Math.Log((1 + i) / (1 - i)) / 2).ToString();
                    Assert.AreEqual(_ScientificCalculatorPage.CheckAccuracy(ans),
                        _ScientificCalculatorPage.CheckAccuracy(_calculatorResults.ReturnCalculatorResults()));
                }
                else if(i == 1)
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypTagentBtn.Click();
                    Assert.IsTrue("Cannot divide by zero" == _calculatorResults.ReturnCalculatorText());
                }
                else
                {
                    _ScientificCalculatorPage.PressNumber(i);
                    _ScientificCalculatorPage.TrigBtn.Click();
                    _ScientificCalculatorPage.SecondBtn.Click();
                    _ScientificCalculatorPage.HyperbolicFunctionBtn.Click();
                    _ScientificCalculatorPage.ArcHypTagentBtn.Click();
                    Assert.IsTrue("Invalid input" == _calculatorResults.ReturnCalculatorText());
                }
            }
        }
        [Test]
        public void CeilingTest()
        {
            _ScientificCalculatorPage.PressNumber(3.6);
            _ScientificCalculatorPage.FunctionsBtn.Click();
            _ScientificCalculatorPage.CeilingBtn.Click();
            Assert.AreEqual("4", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.PressNumber(3.0);
            _ScientificCalculatorPage.FunctionsBtn.Click();
            _ScientificCalculatorPage.CeilingBtn.Click();
            Assert.AreEqual("3", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void FloorTest()
        {
            _ScientificCalculatorPage.PressNumber(3.6);
            _ScientificCalculatorPage.FunctionsBtn.Click();
            _ScientificCalculatorPage.FloorBtn.Click();
            Assert.AreEqual("3", _calculatorResults.ReturnCalculatorResults());
            _ScientificCalculatorPage.PressNumber(3.0);
            _ScientificCalculatorPage.FunctionsBtn.Click();
            _ScientificCalculatorPage.CeilingBtn.Click();
            Assert.AreEqual("3", _calculatorResults.ReturnCalculatorResults());
        }
    }
}
