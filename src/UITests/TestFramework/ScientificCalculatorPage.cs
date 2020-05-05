using System;
using OpenQA.Selenium.Appium.Windows;

namespace UITests.TestFramework
{
    class ScientificCalculatorPage
    {
        public WindowsDriver<WindowsElement> _session;

        public ScientificCalculatorPage(WindowsDriver<WindowsElement> session)
        {
            this._session = session;
        }

        public void AreaOfCircle(int r)
        {
            PiBtn.Click();
            MultiplyBtn.Click();
            Power(r, 2);
        }

        public string CheckAccuracy(string ans)
        {
            if(ans.Length > 13)
            {
                return ans.Substring(0, 12);
            } else
            {
                return ans;
            }
        }

        public void LogBase10(int x)
        {
            PressNumber(x);
            LogBtnBtn.Click();
        }

        public void NaturalLog(int x)
        {
            PressNumber(x);
            NaturalLogBtn.Click();
        }

        public void Power(int x, int y)
        {
            PressNumber(x);
            xToExponentBtn.Click();
            PressNumber(y);
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
            if (value < 0)
            {
                EvaluateStringInput(Math.Abs(value).ToString());
                NegativeBtn.Click();
            }
            else
            {
                EvaluateStringInput(Math.Abs(value).ToString());
            }
        }
        public  void PressKey(char c)
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
                case '(': LeftParenthesisBtn.Click(); break;
                case ')': RightParenthesisBtn.Click(); break;
                default: throw new NotSupportedException($"Charactor: {c} not supported");
            }
        }

        #region Header
        public WindowsElement WindowHeader => _session.FindElementByName("Scientific Calculator mode");
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
        public WindowsElement LeftParenthesisBtn => _session.FindElementByName("Left parenthesis");
        public WindowsElement RightParenthesisBtn => _session.FindElementByName("Right parenthesis");
        public WindowsElement FactorialBtn => _session.FindElementByName("Factorial");
        public WindowsElement AbsBtn => _session.FindElementByName("Absolute Value");
        public WindowsElement ExponentialBtn => _session.FindElementByName("Exponential");
        public WindowsElement PiBtn => _session.FindElementByName("Pi");
        public WindowsElement EulersNumBtn => _session.FindElementByName("Euler's number");
        public WindowsElement ModuloBtn => _session.FindElementByName("Modulo");
        #endregion

        #region 2nd
        public WindowsElement SecondBtn => _session.FindElementByName("Inverse Function");
        public WindowsElement SquareBtn => _session.FindElementByName("Square");
        public WindowsElement CubeBtn => _session.FindElementByName("Cube");
        public WindowsElement xToExponentBtn => _session.FindElementByName("'X' to the exponent");
        public WindowsElement tenToExponentBtn => _session.FindElementByName("Ten to the exponent");
        public WindowsElement LogBtnBtn => _session.FindElementByName("Log");
        public WindowsElement NaturalLogBtn => _session.FindElementByName("Natural log");
        public WindowsElement SquareRootBtn => _session.FindElementByName("Square root");
        public WindowsElement CubeRootBtn => _session.FindElementByName("Cube root");
        public WindowsElement yRootXBtn => _session.FindElementByName("'y' root of 'x'");
        public WindowsElement twoToExponentBtn => _session.FindElementByName("Two to the exponent");
        public WindowsElement LogBaseXBtn => _session.FindElementByName("Log base X");
        public WindowsElement eToExponentBtn => _session.FindElementByName("'e' to the exponent");
        #endregion

        #region Trigonometry
        public WindowsElement TrigBtn => _session.FindElementByName("Trigonometry");
        public WindowsElement HyperbolicFunctionBtn => _session.FindElementByName("Hyperbolic function");
        public WindowsElement SineBtn => _session.FindElementByName("Sine");
        public WindowsElement CosineBtn => _session.FindElementByName("Cosine");
        public WindowsElement TagentBtn => _session.FindElementByName("Tagent");
        public WindowsElement SecantBtn => _session.FindElementByName("Secant");
        public WindowsElement CosecantBtn => _session.FindElementByName("Cosecant");
        public WindowsElement Cotangent => _session.FindElementByName("Cotanget");
        public WindowsElement HypSineBtn => _session.FindElementByName("Hyperbolic Sine");
        public WindowsElement HypCosineBtn => _session.FindElementByName("Hyperbolic Cosine");
        public WindowsElement HypTagentBtn => _session.FindElementByName("Hyperbolic Tagent");
        public WindowsElement HypSecantBtn => _session.FindElementByName("Hyperbolic Secant");
        public WindowsElement HypCosecantBtn => _session.FindElementByName("Hyperbolic Cosecant");
        public WindowsElement HypCotangent => _session.FindElementByName("Hyperbolic Cotanget");
        #endregion

        #region Function
        public WindowsElement FunctionsBtn => _session.FindElementByName("Functions");
        public WindowsElement FloorBtn => _session.FindElementByName("Floor");
        public WindowsElement CeilingBtn => _session.FindElementByName("Ceiling");
        public WindowsElement RandomBtn => _session.FindElementByName("Random");
        public WindowsElement DegreeMinuteSecondBtn => _session.FindElementByName("Degree Minute Second");
        public WindowsElement DegreesBtn => _session.FindElementByName("Degrees");
        #endregion

        #region Modes
        public WindowsElement F_E_Btn => _session.FindElementByName("Scientific notation");
        public WindowsElement DegreeesToggle => _session.FindElementByName("Degrees toggle");
        public WindowsElement RadiansToggle => _session.FindElementByName("Radians toggle");
        public WindowsElement GradiansToggle => _session.FindElementByName("Gradians toggle");
        #endregion
    }
}
