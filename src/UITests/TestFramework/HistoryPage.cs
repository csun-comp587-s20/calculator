using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace UITests.TestFramework
{
    class HistoryPage
    {
        public WindowsDriver<WindowsElement> _session;

        public HistoryPage(WindowsDriver<WindowsElement> session)
        {
            this._session = session;
        }

        public string getFirstHistoryEvent()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var historyLabel = docPivot.FindElementByAccessibilityId("HistoryLabel");
            var HistoryList = historyLabel.FindElementByAccessibilityId("HistoryListView");
            return HistoryList.FindElementByClassName("ListViewItem").Text;
        }

        public void selectHistory()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var historyLabel = docPivot.FindElementByAccessibilityId("HistoryLabel");
            historyLabel.Click();
        }

        public void clearHistory()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var clearButton = docPivot.FindElementByAccessibilityId("ClearHistory");
            clearButton.Click();
        }
        public string EmptyHistory()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var memoryLabel = docPivot.FindElementByAccessibilityId("HistoryLabel");
            string emptyMemory = memoryLabel.FindElementByAccessibilityId("HistoryEmpty").Text;
            return emptyMemory;
        }
    }
}
