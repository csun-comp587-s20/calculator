using OpenQA.Selenium.Appium.Windows;
using org.mariuszgromada.math.mxparser.mathcollection;
using System;

namespace UITests.TestFramework
{
    class ProgrammerCalculatorPage
    {
        public WindowsDriver<WindowsElement> _session;

        public ProgrammerCalculatorPage(WindowsDriver<WindowsElement> session)
        {
            this._session = session;
        }
        public void BitShift(int arg1, string op, bool carry)
        {
            PressNumber(arg1);
            if (carry)
            {
                switch (op)
                {
                    case "Lsh":
                        Lsh_RoLC.Click();
                        break;
                    case "Rsh":
                        Rsh_RoRC.Click();
                        break;
                    default:
                        break;
                }
            } else
            {
                switch (op)
                {
                    case "Lsh":
                        Lsh_RoL.Click();
                        break;
                    case "Rsh":
                        Rsh_RoR.Click();
                        break;
                    default:
                        break;
                }
            }
        }
        public void BitShift(int arg1, string op, int arg2, bool isLogicalShift)
        {
            PressNumber(arg1);
            if (isLogicalShift)
            {
                switch (op)
                {
                    case "Lsh":
                        Lsh_Btn.Click();
                        break;
                    case "Rsh":
                        Rsh_Btn.Click();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (op)
                {
                    case "Lsh":
                        Lsh_Btn2.Click();
                        break;
                    case "Rsh":
                        Rsh_Btn2.Click();
                        break;
                    default:
                        break;
                }
            }

            PressNumber(arg2);
        }
        public void Not(int arg1)
        {
            PressNumber(arg1);
            BitwiseBtn.Click();
            Not_Btn.Click();

        }
        public void Bitwise(int arg1, string op, int arg2)
        {
            PressNumber(arg1);
            BitwiseBtn.Click();
            switch (op)
            {
                case "AND":
                    And_Btn.Click();
                    break;
                case "OR":
                    Or_Btn.Click();
                    break;
                case "NAND":
                    Nand_Btn.Click();
                    break;
                case "XOR":
                    Xor_Btn.Click();
                    break;
                case "NOR":
                    Nor_Btn.Click();
                    break;
                default:
                    break;
            }
            PressNumber(arg2);
        }
        public void EvaluateStringInput(string exp)
        {
            if (exp[0] == '-')
            {
                exp = exp.Substring(1);
                foreach (char c in exp)
                {
                    PressKey(c);
                }
                NegativeBtn.Click();
            }
            else
            {
                exp = exp.Trim();
                foreach (char c in exp)
                {
                    PressKey(c);
                }
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
        public void PressKey(char c)
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
        #region NumPad
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
        #endregion

        #region Operators
        public WindowsElement AdditionBtn => _session.FindElementByName("Plus");
        public WindowsElement SubtractBtn => _session.FindElementByName("Minus");
        public WindowsElement MultiplyBtn => _session.FindElementByName("Multiply by");
        public WindowsElement DivideBtn => _session.FindElementByName("Divide by");
        public WindowsElement NegativeBtn => _session.FindElementByName("Positive Negative");
        public WindowsElement LeftParenthesisBtn => _session.FindElementByName("Left parenthesis");
        public WindowsElement RightParenthesisBtn => _session.FindElementByName("Right parenthesis");
        public WindowsElement PercentBtn => _session.FindElementByName("Percent");
        public WindowsElement ClearBtn => _session.FindElementByName("Clear");
        public WindowsElement ClearEntryBtn => _session.FindElementByName("Clear entry");
        public WindowsElement BackspaceBtn => _session.FindElementByName("Backspace");
        public WindowsElement Lsh_Btn => _session.FindElementByName("Left Shift");
        public WindowsElement Rsh_Btn => _session.FindElementByName("Right Shift");
        public WindowsElement Lsh_Btn2 => _session.FindElementByName("Left shift");
        public WindowsElement Rsh_Btn2 => _session.FindElementByName("Right shift");
        public WindowsElement Lsh_RoL => _session.FindElementByName("Rotate on left");
        public WindowsElement Rsh_RoR => _session.FindElementByName("Rotate on right");
        public WindowsElement Lsh_RoLC => _session.FindElementByName("Rotate on left with carry");
        public WindowsElement Rsh_RoRC => _session.FindElementByName("Rotate on right with carry");
        #endregion

        #region Base
        public WindowsElement HexaDecimalRadioBtnBtn => _session.FindElementByName("HexaDecimal 0");
        public WindowsElement DecimalRadioBtn => _session.FindElementByName("Decimal 0");
        public WindowsElement OctalRadioBtn => _session.FindElementByName("Octal 0");
        public WindowsElement BinaryRadioBtn => _session.FindElementByName("Binary 0");
        #endregion

        #region HEX
        public WindowsElement A_Btn => _session.FindElementByName("A");
        public WindowsElement B_Btn => _session.FindElementByName("B");
        public WindowsElement C_Btn => _session.FindElementByName("C");
        public WindowsElement D_Btn => _session.FindElementByName("D");
        public WindowsElement E_Btn => _session.FindElementByName("E");
        public WindowsElement F_Btn => _session.FindElementByName("F");
        #endregion

        #region Bitwise
        public WindowsElement BitwiseBtn => _session.FindElementByName("Bitwise");
        public WindowsElement And_Btn => _session.FindElementByName("And");
        public WindowsElement Or_Btn => _session.FindElementByName("Or");
        public WindowsElement Not_Btn => _session.FindElementByName("Not");
        public WindowsElement Xor_Btn => _session.FindElementByName("Exclusive or");
        public WindowsElement Nand_Btn => _session.FindElementByName("Nand");
        public WindowsElement Nor_Btn => _session.FindElementByName("Nor");
        #endregion

        #region Bit Shift
        public WindowsElement BitShiftBtn => _session.FindElementByName("Bit Shift");
        public WindowsElement ArithmeticShiftRadioBtn => _session.FindElementByName("Arithmetic Shift");
        public WindowsElement LogicalShiftRadioBtn => _session.FindElementByName("Logical Shift");
        public WindowsElement RotateCircularShiftRadioBtn => _session.FindElementByName("Rotate Circular Shift");
        public WindowsElement RotateThroughCarryCircularShiftRadioBtn => _session.FindElementByName("Rotate Through Carry Circular Shift");
        #endregion

        #region DataTypes
        public WindowsElement ByteToggleBtn => _session.FindElementByName("Byte toggle");
        public WindowsElement WordToggleBtn => _session.FindElementByName("Word toggle");
        public WindowsElement DWordToggleBtn => _session.FindElementByName("Double Word toggle");
        public WindowsElement QWordToggleBtn => _session.FindElementByName("Quadruple Word toggle");
        #endregion

        #region Mode
        public WindowsElement FullKeypadRadioBtn => _session.FindElementByName("Full keypad");
        public WindowsElement BitTogglingKeypadRadioBtn => _session.FindElementByName("Bit toggling keypad");
        #endregion
    }
}
