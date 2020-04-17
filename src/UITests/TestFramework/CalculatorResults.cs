using System;
using OpenQA.Selenium.Appium.Windows;

namespace UITests.TestFramework
{
    class CalculatorResults
    {
        public WindowsDriver<WindowsElement> _session;

        public CalculatorResults(WindowsDriver<WindowsElement> session)
        {
            this._session = session;
        }

        public WindowsElement ResultsOutput => _session.FindElementByAccessibilityId("CalculatorResults");

        public string ReturnCalculatorText()
        {
            return ResultsOutput.Text.Replace("Display is", string.Empty).Trim();
        }

        public WindowsElement EqualsBtn => _session.FindElementByName("Equals");
        public string ReturnCalculatorResults()
        {
            EqualsBtn.Click();

            string result = ResultsOutput.Text.Replace("Display is ", string.Empty).Trim();
            if (result.Contains(","))
            {
                result = result.Replace(",", string.Empty);
            }
            if (result.Contains("e-"))
            {
                var nextInd = result.IndexOf('-') + 1;
                if (nextInd == result.Length - 1)
                {
                    result = result.Replace("e-", "E-0");
                }
                else
                {
                    result = result.Replace("e-", "E-");
                }
            }
            if (result.Contains("e+"))
            {
                var nextInd = result.IndexOf('+') + 1;
                if (nextInd == result.Length - 1)
                {
                    result = result.Replace("e+", "E+0");
                }
                else
                {
                    result = result.Replace("e+", "E+");
                }
            }
            return result;
        }
    }
}
