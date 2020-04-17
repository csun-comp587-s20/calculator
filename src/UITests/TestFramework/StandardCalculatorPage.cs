using OpenQA.Selenium.Appium.Windows;
using System;

namespace UITests.TestFramework
{
    public class StandardCalculatorPage
    {
        public WindowsDriver<WindowsElement> _session;

        public StandardCalculatorPage(WindowsDriver<WindowsElement> session)
        {
            this._session = session;
        }
        public void Addition(int value1, int value2)
        {
            PressNumber(value1);
            AdditionBtn.Click();
            PressNumber(value2);
        }
        public void Subtraction(int value1, int value2)
        {
            PressNumber(value1);
            SubtractBtn.Click();
            PressNumber(value2);
        }
        public void Multiplication(int value1, int value2)
        {
            PressNumber(value1);
            MultiplyBtn.Click();
            PressNumber(value2);
        }
        public void Division(int value1, int value2)
        {
            PressNumber(value1);
            DivideBtn.Click();
            PressNumber(value2);
        }

        public void EvaluateStringInput(string exp)
        {
            exp = exp.Trim();
            foreach (char c in exp)
            {
                PressKey(c);
            }
        }

        public void PressNumber(int value)
        {
            if(value < 0)
            {
                EvaluateStringInput(Math.Abs(value).ToString());
                NegativeBtn.Click();
            } else
            {
                EvaluateStringInput(Math.Abs(value).ToString());
            }
        }

        private void PressKey(char c)
        {
            switch (c)
            {
                case '0': ZeroBtn.Click(); break;
                case '1': OneBtn.Click(); break;
                case '2': TwoBtn.Click(); break;
                case '3': ThreeBtn.Click(); break;
                case '4': FourBtn.Click(); break;
                case '5': FiveBtn.Click(); break;
                case '6': SixBtn.Click(); break;
                case '7': SevenBtn.Click(); break;
                case '8': EightBtn.Click(); break;
                case '9': NineBtn.Click(); break;
                case '.': DecimalBtn.Click(); break;
                case '+': AdditionBtn.Click(); break;
                case '-': SubtractBtn.Click(); break;
                case '/': DivideBtn.Click(); break;
                case '*': MultiplyBtn.Click(); break;
                case '%': PercentBtn.Click(); break;
                default: throw new NotSupportedException($"Charactor: {c} not supported");
            }
        }

        #region Header
        public WindowsElement WindowHeader => _session.FindElementByName("Standard Calculator mode");
        public WindowsElement CalculatorMenu => _session.FindElementByName("Open Navigation");
        #endregion

        #region Number Pad
        public WindowsElement ZeroBtn => _session.FindElementByName("Zero");
        public WindowsElement OneBtn => _session.FindElementByName("One");
        public WindowsElement TwoBtn => _session.FindElementByName("Two");
        public WindowsElement ThreeBtn => _session.FindElementByName("Three");
        public WindowsElement FourBtn => _session.FindElementByName("Four");
        public WindowsElement FiveBtn => _session.FindElementByName("Five");
        public WindowsElement SixBtn => _session.FindElementByName("Six");
        public WindowsElement SevenBtn => _session.FindElementByName("Seven");
        public WindowsElement EightBtn => _session.FindElementByName("Eight");
        public WindowsElement NineBtn => _session.FindElementByName("Nine");
        public WindowsElement DecimalBtn => _session.FindElementByName("Decimal Separator");
        public WindowsElement AdditionBtn => _session.FindElementByName("Plus");
        public WindowsElement SubtractBtn => _session.FindElementByName("Minus");
        public WindowsElement MultiplyBtn => _session.FindElementByName("Multiply by");
        public WindowsElement DivideBtn => _session.FindElementByName("Divide by");
        public WindowsElement NegativeBtn => _session.FindElementByName("Positive Negative");
        public WindowsElement BackspaceBtn => _session.FindElementByName("Backspace");
        public WindowsElement ClearBtn => _session.FindElementByName("Clear");
        public WindowsElement ClearEntryBtn => _session.FindElementByName("Clear entry");
        public WindowsElement PercentBtn => _session.FindElementByName("Percent");
        public WindowsElement ReciprocalBtn => _session.FindElementByName("Reciprocal");
        public WindowsElement SquareBtn => _session.FindElementByName("Square");
        public WindowsElement SquareRootBtn => _session.FindElementByName("Square root");
        #endregion

        #region Memory Buttons
        public WindowsElement MemAddBtn => _session.FindElementByName("Memory Add");
        public WindowsElement MemRemoveBtn => _session.FindElementByName("Memory Subtract");
        public WindowsElement MemStoreBtn => _session.FindElementByName("Memory Store");
        #endregion

        #region Calculator Results
        public WindowsElement ResultsOutput => _session.FindElementByAccessibilityId("CalculatorResults");
        #endregion
    }
}
