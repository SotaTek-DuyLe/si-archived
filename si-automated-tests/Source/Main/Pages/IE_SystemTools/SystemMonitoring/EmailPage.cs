using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.SystemTools.SystemMonitoring
{
    public class EmailPage : BasePage
    {
        private readonly By rightFrame = By.XPath("//iframe[@id='RightFrame']");
        private readonly By actionBtn = By.XPath("//span[contains(text(),'Actions')]");
        private readonly By moveLastBtn = By.XPath("//img[@title='Move Last']");
        private readonly By lastRow = By.XPath("//tr[contains(@class,'LIST_ROW')][last()]");

        private readonly String rightTableId = "RightDef";

        public EmailPage IsOnEmailPage()
        {
            SwitchToFrame(rightFrame);
            WaitUtil.WaitForElementVisible(actionBtn);
            WaitUtil.WaitForElementVisible(moveLastBtn);
            return this;
        }
        public EmailPage ClickMoveLast()
        {
            ClickOnElement(moveLastBtn);
            return this;
        }
        public EmailPage ClickLastRow()
        {
            ScrollDownInElement(rightTableId);
            SleepTimeInMiliseconds(1000);
            DoubleClickOnElement(lastRow);
            return this;
        }

    }
}
