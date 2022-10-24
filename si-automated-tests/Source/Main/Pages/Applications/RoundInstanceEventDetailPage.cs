using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceEventDetailPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Round Instance Event']");

        //DETAIL TAB
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By roundEventTypeDd = By.XPath("//label[text()='Round Event Type']/following-sibling::select");
        private readonly By resourceDd = By.XPath("//label[text()='Resource']/following-sibling::select");

        //HISTORY TAB
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By firstValueInDetailColumn = By.XPath("//div[@class='grid-canvas']/div[1]//button[@title='Click to see full details']");

        //HISTORY POPUP
        private readonly By titleHistoryPopup = By.XPath("//h4[text()='History Details']");
        private readonly By tagContent = By.XPath("//h4[text()='History Details']/parent::div/following-sibling::div[@class='modal-body']");
        private readonly By closeHistoryPopupBtn = By.XPath("//h4[text()='History Details']/preceding-sibling::button");

        //DYNAMIC
        private readonly string anyRoundEventTypeOption = "//label[text()='Round Event Type']/following-sibling::select/option[text()='{0}']";
        private readonly string anyResourceOption = "//label[text()='Resource']/following-sibling::select/option[text()='{0}']";

        [AllureStep]
        public RoundInstanceEventDetailPage IsRoundInstanceEventDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(detailTab));
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage SelectRoundEventTypeAndResource(string roundEventTypeValue, string resourceValue)
        {
            //Select - Round Event Type
            ClickOnElement(roundEventTypeDd);
            ClickOnElement(anyRoundEventTypeOption, roundEventTypeValue);

            //Select - Resource
            ClickOnElement(resourceDd);
            ClickOnElement(anyResourceOption, resourceValue);
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnFirstValueInDetailColumn()
        {
            ClickOnElement(firstValueInDetailColumn);
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage IsHistoryPopup()
        {
            WaitUtil.WaitForAllElementsVisible(titleHistoryPopup);
            Assert.IsTrue(IsControlDisplayed(tagContent));
            Assert.IsTrue(IsControlDisplayed(closeHistoryPopupBtn));
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnCloseHistoryPopup()
        {
            ClickOnElement(closeHistoryPopupBtn);
            return this;
        }
    }
}
