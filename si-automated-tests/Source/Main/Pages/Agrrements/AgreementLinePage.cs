using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements
{
    public class AgreementLinePage : BasePage
    {
        private readonly By allTabs = By.XPath("//a[@role='tab']");
        private readonly By title = By.XPath("//h4[text()='AGREEMENTLINE']");
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");
        private readonly By billingRuleDd = By.XPath("//select[@id='billing-rule']");
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");

        private const string frameMessage = "//div[@class='notifyjs-corner']/div";

        //HISTORY TAB
        private readonly By updateAgreementLineTitle = By.XPath("//strong[text()='Update - AgreementLine']");
        private readonly By billingRuleUpdated = By.XPath("//div[contains(text(), 'Billing Rule: ')]");
        private readonly By displayUserUpdated = By.XPath("//strong[text()='Update - AgreementLine']/parent::div/following-sibling::div/strong[1]");
        private readonly By timeUpdated = By.XPath("//strong[text()='Update - AgreementLine']/parent::div/following-sibling::div/strong[2]");

        //DYNAMIC LOCATOR
        private const string titleContainsId = "//p[text()='Agreement ID {0}']";
        private const string anyTab = "//a[text()='{0}']";
        private const string billingRuleOption = "//select[@id='billing-rule']/option[text()='{0}']";


        [AllureStep]
        public new AgreementLinePage GoToAllTabAndConfirmNoError()
        {
            IList<IWebElement> elements = WaitUtil.WaitForAllElementsVisible(allTabs);
            foreach (IWebElement element in elements)
            {
                Thread.Sleep(1000);
                element.Click();
                WaitForLoadingIconToDisappear();
                Assert.IsFalse(IsControlDisplayedNotThrowEx(frameMessage));
            }
            return this;
        }
        [AllureStep]
        public AgreementLinePage CloseWithoutSaving()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }
        [AllureStep]
        public AgreementLinePage WaitForWindowLoadedSuccess(string id)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(string.Format(titleContainsId, id));
            return this;
        }
        [AllureStep]
        public AgreementLinePage ClickDetailTab()
        {
            ClickOnElement(string.Format(anyTab, "Details"));
            return this;
        }
        [AllureStep]
        publ[AllureStep]ic AgreementLinePage ClickTasksTab()
        {
            ClickOnElement(string.Format(anyTab, "Tasks"));
            return this;
        }
        [AllureStep]

        public AgreementLinePage ClickOnBillingRuleDd()
        {
            ClickOnElement(billingRuleDd);
            return this;
        }
        [AllureStep]

        public AgreementLinePage SelectAnyBillingRuleOption(string option)
        {
            ClickOnElement(billingRuleOption, option);
            return this;
        }
        [AllureStep]
        public AgreementLinePage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]

        public AgreementLinePage VerifyHistoryAfterUpdatingAgreementLine(string billingRuleExp, string userUpdatedExp, string timeUpdatedExp)
        {
            Assert.IsTrue(IsControlDisplayed(updateAgreementLineTitle));
            Assert.AreEqual("Billing Rule: " + billingRuleExp + ".", GetElementText(billingRuleUpdated));
            Assert.AreEqual(userUpdatedExp, GetElementText(displayUserUpdated));
            Assert.AreEqual(timeUpdatedExp, GetElementText(timeUpdated));
            return this;
        }
    }
}
