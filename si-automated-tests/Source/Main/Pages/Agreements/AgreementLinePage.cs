using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Agrrements
{
    public class AgreementLinePage : BasePage
    {
        private readonly By allTabs = By.XPath("//a[@role='tab']");
        private readonly By title = By.XPath("//h4[text()='AGREEMENTLINE']");
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");
        private readonly By billingRuleDd = By.XPath("//select[@id='billing-rule']");
        private readonly By invoiceAddress = By.CssSelector("select[id='invoice-address']");
        private readonly By invoiceContact = By.CssSelector("select[id='invoice-contact']");
        private readonly By invoiceSchedule = By.CssSelector("select[id='invoice-schedule']");
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");

        private const string frameMessage = "//div[@class='notifyjs-corner']/div";

        //HISTORY TAB
        private readonly By updateAgreementLineTitle = By.XPath("//strong[text()='Update - AgreementLine']");
        private readonly By billingRuleUpdated = By.XPath("//div[contains(text(), 'Billing Rule: ')]");
        private readonly By displayUserUpdated = By.XPath("//strong[text()='Update - AgreementLine']/parent::div/following-sibling::div/strong[1]");
        private readonly By timeUpdated = By.XPath("//strong[text()='Update - AgreementLine']/parent::div/following-sibling::div/strong[2]");
        private readonly By recordUpdated = By.XPath("//strong[text()='Update - AgreementLine']/following-sibling::div");

        //DYNAMIC LOCATOR
        private const string titleContainsId = "//p[text()='Agreement ID {0}']";
        private const string anyTab = "//a[text()='{0}']";
        private const string billingRuleOption = "//select[@id='billing-rule']/option[text()='{0}']";
        private const string invoiceAddressOption = "//select[@id='invoice-address']/option[text()='{0}']";
        private const string invoiceContactOption = "//select[@id='invoice-contact']/option[text()='{0}']";
        private const string invoiceScheduleOption = "//select[@id='invoice-schedule']/option[text()='{0}']";

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
        public AgreementLinePage IsAgreementLinePage()
        {
            WaitUtil.WaitForAllElementsVisible(title);
            WaitUtil.WaitForAllElementsVisible(anyTab, "Details");
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
        public AgreementLinePage ClickOnAgreementLineHyperlink(string id)
        {
            ClickOnElement(titleContainsId, id);
            return this;
        }
        [AllureStep]
        public AgreementLinePage ClickDetailTab()
        {
            ClickOnElement(string.Format(anyTab, "Details"));
            return this;
        }
        [AllureStep]
        public AgreementLinePage ClickTasksTab()
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
        public AgreementLinePage ClickOnInvoiceAddress()
        {
            ClickOnElement(invoiceAddress);
            return this;
        }

        [AllureStep]
        public AgreementLinePage SelectAnyInvoiceAddress(string invoiceAddressValue)
        {
            ClickOnElement(invoiceAddressOption, invoiceAddressValue);
            return this;
        }

        [AllureStep]
        public AgreementLinePage ClickOnInvoiceContact()
        {
            ClickOnElement(invoiceContact);
            return this;
        }

        [AllureStep]
        public AgreementLinePage SelectAnyInvoiceContact(string invoiceContactValue)
        {
            ClickOnElement(invoiceContactOption, invoiceContactValue);
            return this;
        }

        [AllureStep]
        public AgreementLinePage ClickOnInvoiceSchedule()
        {
            ClickOnElement(invoiceSchedule);
            return this;
        }

        [AllureStep]
        public AgreementLinePage SelectAnyInvoiceSchedule(string invoiceScheduleValue)
        {
            ClickOnElement(invoiceScheduleOption, invoiceScheduleValue);
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

        [AllureStep]
        public AgreementLinePage VerifyDefaulValueInISICIABR(string invoiceScheduleExp, string invoiceContactExp, string invoiceAddressExp, string billingRuleExp)
        {
            Assert.AreEqual(invoiceScheduleExp, GetFirstSelectedItemInDropdown(invoiceSchedule), "Value in [Invoice Schedule] is incorrect");
            Assert.AreEqual(invoiceContactExp, GetFirstSelectedItemInDropdown(invoiceContact), "Value in [Invoice Contact] is incorrect");
            Assert.AreEqual(invoiceAddressExp, GetFirstSelectedItemInDropdown(invoiceAddress), "Value in [Invoice Address] is incorrect");
            Assert.AreEqual(billingRuleExp, GetFirstSelectedItemInDropdown(billingRuleDd), "Value in [Billing Rule] is incorrect");
            return this;
        }

        [AllureStep]
        public AgreementLinePage VerifyHistoryAfterUpdatingAgreementLine(string[] historyTitle, string[] valueExp, string userUpdatedExp)
        {
            Assert.IsTrue(IsControlDisplayed(updateAgreementLineTitle));
            Assert.AreEqual(userUpdatedExp, GetElementText(displayUserUpdated));
            string[] allInfoDisplayed = GetElementText(recordUpdated).Split(Environment.NewLine);
            for (int i = 0; i < historyTitle.Length; i++)
            {
                Assert.AreEqual(historyTitle[i] + ": " + valueExp[i] + ".", allInfoDisplayed[i]);
            }
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Agreement Line?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public AgreementLinePage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectServiceUnit)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public AgreementLinePage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public AgreementLinePage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public AgreementLinePage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }

        [AllureStep]
        public string GetAgreementLineId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/agreement-lines/", "");
        }
    }
}
