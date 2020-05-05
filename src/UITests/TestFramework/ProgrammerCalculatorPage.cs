using OpenQA.Selenium.Appium.Windows;
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
        public WindowsElement LeftShitBtn => _session.FindElementByName("Left shift");
        public WindowsElement RightShiftBtn => _session.FindElementByName("Right shift");
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
