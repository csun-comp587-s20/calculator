using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using UITests.TestFramework;
using System.Threading;


namespace UITests
{
    class HistoryAndMemoryTest
    {
        private WindowsDriver<WindowsElement> _driver;
        private StandardCalculatorPage _calcStandardView;
        private HistoryPage _historyPage;
        private MemoryPage _memoryPage;
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
            _historyPage = new HistoryPage(_driver);
            _memoryPage = new MemoryPage(_driver);

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
                _driver.FindElementByName("Open Navigation").Click();
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

        // Memory
        [Test]
        public void Store_Memory()
        {
            _memoryPage.selectMemory();
            _calcStandardView.PressNumber(25);
            _memoryPage.MemStoreBtn.Click();
            _calcStandardView.ClearBtn.Click();
            _memoryPage.MemRecallBtn.Click();
            Assert.AreEqual("25", _calculatorResults.ReturnCalculatorResults());
        }

        [Test]
        public void Increment_Memory()
        {
            _memoryPage.selectMemory();
            _calcStandardView.PressNumber(25);
            _memoryPage.MemStoreBtn.Click();
            _memoryPage.MemAddBtn.Click();
            _calcStandardView.ClearBtn.Click();
            _memoryPage.MemRecallBtn.Click();
            Assert.AreEqual("50", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void Decrease_Memory()
        {
            _memoryPage.selectMemory();
            _calcStandardView.PressNumber(25);
            _memoryPage.MemStoreBtn.Click();
            _calcStandardView.ClearBtn.Click();
            _calcStandardView.PressNumber(10);
            _memoryPage.MemAddBtn.Click();
            _memoryPage.MemRecallBtn.Click();
            Assert.AreEqual("35", _calculatorResults.ReturnCalculatorResults());
        }
        [Test]
        public void Clear_Memory()
        {
            _memoryPage.selectMemory();
            _calcStandardView.PressNumber(25);
            _memoryPage.MemStoreBtn.Click();
            _memoryPage.ClearMemBtn.Click();
            string ans = _memoryPage.EmptyMemory();
            ans = ans.Replace('’', '\'');
            Assert.AreEqual(ans, "There's nothing saved in memory");
        }

        // History
        [Test]
        public void AddHistory()
        {
            String exp = "12 + 1=";
            _historyPage.selectHistory();
            _calcStandardView.EvaluateStringInput(exp);
            String ans = _calculatorResults.ReturnCalculatorResults();
            Assert.AreEqual(exp + " " + ans, _historyPage.getFirstHistoryEvent());

        }
        [Test]
        public void ClearHistory()
        {
            String exp = "12 + 1=";
            _historyPage.selectHistory();
            _calcStandardView.EvaluateStringInput(exp);
            _calculatorResults.ReturnCalculatorResults();
            _historyPage.clearHistory();
            String ans = _historyPage.EmptyHistory();
            ans = ans.Replace('’', '\'');
            Assert.AreEqual(ans, "There's no history yet");
        }
    }
}
