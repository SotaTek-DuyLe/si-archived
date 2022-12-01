using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

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
    }
}
