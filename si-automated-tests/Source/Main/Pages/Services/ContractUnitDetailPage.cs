using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ContractUnitDetailPage : BasePage
    {
        private readonly By contractUnitNameInput = By.Id("contract-unit");
        private readonly By clientRefInput = By.Id("client-ref");
        private readonly By title = By.XPath("//h4[text()='Contract Unit']");
        private readonly By usersTab = By.CssSelector("a[aria-controls='contractunitusers-tab']");

        //DYNAMIC
        private readonly string contractUnitName = "//p[text()='{0}']";

        [AllureStep]
        public ContractUnitDetailPage InputContractUnitDetails(string name, string refer)
        {
            SendKeys(contractUnitNameInput, name);
            SendKeys(clientRefInput, refer);
            return this;
        }

        [AllureStep]
        public ContractUnitDetailPage IsContractUnit(string contractUnitValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(contractUnitName, contractUnitValue);
            return this;
        }

        #region USERS TAB
        private readonly By addNewItemInUsersTab = By.XPath("//div[@id='contractunitusers-tab']//button[text()='Add New Item']");

        [AllureStep]
        public ContractUnitDetailPage ClickOnUsersTab()
        {
            ClickOnElement(usersTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ContractUnitDetailPage ClickOnAddNewItemInUsersTab()
        {
            ClickOnElement(addNewItemInUsersTab);
            return this;
        }

        #endregion

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Contract Unit?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public ContractUnitDetailPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectContractUnits)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ContractUnitDetailPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public ContractUnitDetailPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public ContractUnitDetailPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
