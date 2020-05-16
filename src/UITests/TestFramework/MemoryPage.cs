using System;
using OpenQA.Selenium.Appium.Windows;

namespace UITests.TestFramework
{
    class MemoryPage
    {
        public WindowsDriver<WindowsElement> _session;

        public MemoryPage(WindowsDriver<WindowsElement> session)
        {
            this._session = session;
        }

        public string getFirstMemoryEvent()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var memoryLabel = docPivot.FindElementByAccessibilityId("MemoryLabel");
            var MemoryList = memoryLabel.FindElementByAccessibilityId("MemoryListView");
            return MemoryList.FindElementByClassName("ListViewItem").Text;
        }

        public void selectMemory()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var memoryLabel = docPivot.FindElementByAccessibilityId("MemoryLabel");
            memoryLabel.Click();
        }

        public void clearMemory()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var clearButton = docPivot.FindElementByAccessibilityId("ClearMemory");
            clearButton.Click();
        }

        public string EmptyMemory()
        {
            var docPanel = _session.FindElementByAccessibilityId("DockPanel");
            var docPivot = docPanel.FindElementByAccessibilityId("DockPivot");
            var memoryLabel = docPivot.FindElementByAccessibilityId("MemoryLabel");
            string emptyMemory = memoryLabel.FindElementByAccessibilityId("MemoryPaneEmpty").Text;
            return emptyMemory;
        }

        public WindowsElement MemAddBtn => _session.FindElementByAccessibilityId("MemPlus");
        public WindowsElement MemSubBtn => _session.FindElementByAccessibilityId("MemMinus");
        public WindowsElement MemStoreBtn => _session.FindElementByAccessibilityId("memButton");
        public WindowsElement MemRecallBtn => _session.FindElementByAccessibilityId("MemRecall");
        public WindowsElement ClearMemBtn => _session.FindElementByAccessibilityId("ClearMemoryButton");
    }
}
