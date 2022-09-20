using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceDataManagementPage : BasePage
    {
        private readonly By serviceLocationTypeTitle = By.XPath("//label[text()='Service Location Type']");
        private readonly By refreshPageBtn = By.XPath("//label[text()='Service Location Type']/parent::div/following-sibling::div//button[@title='Refresh']");
        private readonly By showInformationBtn = By.XPath("//label[text()='Service Location Type']/parent::div/following-sibling::div//button[@title='Show Information']");
        private readonly By popOutBtn = By.XPath("//label[text()='Service Location Type']/parent::div/following-sibling::div//button[@title='Pop out']");
        private readonly By inputServicesTree = By.XPath("//label[text()='Services']/following-sibling::input");
        private readonly By selectTypeDd = By.CssSelector("select[id='type']");
        private readonly By selectAndDeselectBtn = By.CssSelector("div[title='Select/Deselect All']");
        private readonly By nextBtn = By.CssSelector("button[id='next-button']");
        private readonly By firstRowWithServiceTaskSchedule = By.XPath("(//tbody/tr[1]/td[contains(@data-bind, 'retiredPoint')]/span)[1]");
        private readonly By firstRowWithoutServiceTaskSchedule = By.XPath("(//tbody/tr[1]/td[contains(@data-bind, 'retiredPoint')])[1]");
        private readonly By applyFiltersBtn = By.CssSelector("button[id='filter-button']");

        //WARINING POPUP
        private readonly By warningTitle = By.XPath("//h4[text()='Warning']");
        private readonly By warningContent = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//div[text()='Please Note – Any previous row selections will be lost once filters are applied']");
        private readonly By checkboxMessage = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//label[text()='Do not show this message again']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/following-sibling::button");

        //DYNAMIC
        private readonly string serviceTypeOption = "//select[@id='type']/option[text()='{0}']";
        private readonly string actionOption = "//div[@class='action-container']/button[text()='{0}']";
        private readonly string anyServicesGroupByContract = "//li[contains(@class, 'serviceGroups')]//a[text()='{0}']/i[1]";

        [AllureStep]
        public ServiceDataManagementPage IsServiceDataManagementPage()
        {
            WaitUtil.WaitForElementVisible(serviceLocationTypeTitle);
            Assert.IsTrue(IsControlDisplayed(serviceLocationTypeTitle));
            Assert.IsTrue(IsControlDisplayed(refreshPageBtn));
            Assert.IsTrue(IsControlDisplayed(showInformationBtn));
            Assert.IsTrue(IsControlDisplayed(popOutBtn));
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickServiceLocationTypeDdAndSelectOption(string typeOptionValue)
        {
            ClickOnElement(selectTypeDd);
            //Select value
            ClickOnElement(serviceTypeOption, typeOptionValue);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnServicesAndSelectGroupInTree(string serviceGroupName)
        {
            ClickOnElement(inputServicesTree);
            ClickOnElement(anyServicesGroupByContract, serviceGroupName);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnApplyFiltersBtn()
        {
            ClickOnElement(applyFiltersBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage VerifyWarningPopupDisplayed()
        {
            WaitUtil.WaitForElementVisible(warningTitle);
            Assert.IsTrue(IsControlDisplayed(warningTitle));
            Assert.IsTrue(IsControlDisplayed(checkboxMessage));
            Assert.IsTrue(IsControlEnabled(okBtn));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnOkBtn()
        {
            ClickOnElement(okBtn);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnSelectAndDeselectBtn()
        {
            ClickOnElement(selectAndDeselectBtn);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnNextBtn()
        {
            ClickOnElement(nextBtn);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstRowWithServiceTaskSchedule()
        {
            RightClickOnElement(firstRowWithServiceTaskSchedule);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage VerifyActionMenuDisplayedWithActions()
        {
            foreach(string action in CommonConstants.ActionMenuSDM)
            {
                Assert.IsTrue(IsControlDisplayed(actionOption, action), action + " is not displayed");
            }
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage VerifyActionInActionMenuDisabled(string[] nameActions)
        {
            foreach(string action in nameActions)
            {
                Assert.AreEqual("true", GetAttributeValue(string.Format(actionOption, action), "disabled"), action + " is not disabaled");
            }
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstRowWithoutServiceTaskSchedule()
        {
            RightClickOnElement(firstRowWithoutServiceTaskSchedule);
            return this;
        }

    }
}
