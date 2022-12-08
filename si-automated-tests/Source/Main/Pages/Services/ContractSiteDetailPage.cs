using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;

namespace si_automated_tests.Source.Main.Pages.Services
{
    class ContractSiteDetailPage :BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Contract Site']");
        private readonly By addBtn = By.XPath("//button[text()='Add']");
        private readonly By searchBox = By.XPath("//input[@type='search']");
        private readonly String addressOption = "//li[@class='list-group-item' and contains(text(),'{0}')]";

        [AllureStep]
        public ContractSiteDetailPage ClickAddBtn()
        {
            ClickOnElement(addBtn);
            return this;
        }
        [AllureStep]
        public ContractSiteDetailPage SearchForAddress(string add)
        {
            SendKeys(searchBox, add);
            return this;
        }
        [AllureStep]
        public ContractSiteDetailPage SelectAddress(string add)
        {
            ClickOnElement(addressOption, add);
            return this;
        }

        [AllureStep]
        public ContractSiteDetailPage IsContractSiteDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }
        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Contract Site?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public ContractSiteDetailPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectContractSite)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ContractSiteDetailPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public ContractSiteDetailPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public ContractSiteDetailPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
