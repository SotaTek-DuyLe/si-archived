using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.WB.Tickets
{
    public class WeighbridgeTicketDetailPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Weighbridge Ticket']");
        private readonly By id = By.XPath("//h4[@title='Id']");

        public WeighbridgeTicketDetailPage IsWBTicketDetailPage(string idExp)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.AreEqual(idExp, GetElementText(id));
            return this;
        }

        //HISTORY TAB
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly string anyValueInDetailColumn = "//div[@class='grid-canvas']/div[{0}]//button[@title='Click to see full details']";

        //HISTORY POPUP
        private readonly By titleHistoryPopup = By.XPath("//h4[text()='History Details']");
        private readonly By tagContent = By.XPath("//h4[text()='History Details']/parent::div/following-sibling::div[@class='modal-body']");
        private readonly By closeHistoryPopupBtn = By.XPath("//h4[text()='History Details']/preceding-sibling::button");

        public WeighbridgeTicketDetailPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public WeighbridgeTicketDetailPage ClickOnFirstValueInDetailColumn(string row)
        {
            ClickOnElement(anyValueInDetailColumn, row);
            return this;
        }

        public WeighbridgeTicketDetailPage IsHistoryPopup()
        {
            WaitUtil.WaitForAllElementsVisible(titleHistoryPopup);
            Assert.IsTrue(IsControlDisplayed(tagContent));
            Assert.IsTrue(IsControlDisplayed(closeHistoryPopupBtn));
            return this;
        }

        public WeighbridgeTicketDetailPage ClickOnCloseHistoryPopup()
        {
            ClickOnElement(closeHistoryPopupBtn);
            return this;
        }
    }
}
