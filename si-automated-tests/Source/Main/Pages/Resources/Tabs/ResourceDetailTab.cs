using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;

namespace si_automated_tests.Source.Main.Pages.Resources.Tabs
{
    public class ResourceDetailTab : BasePage
    {
        private readonly By resourceName = By.Id("resource");
        private readonly By resourceBtn = By.XPath("//button[@data-id='resource-type']");
        private readonly By businessUnitDd = By.CssSelector("select[id='businessUnit']");
        private readonly By resourceType = By.Id("resource-type"); 
        private readonly By clientReference = By.Id("clientReference");
        private readonly By contract = By.Id("contract");
        private readonly By service = By.Id("service");
        private readonly By businessUnit = By.Id("businessUnit");
        private readonly By contractUnit = By.Id("contract-unit");
        private readonly By site = By.Id("site");
        private readonly By contractRoam = By.Id("contract-roam");
        private readonly By serviceRoam = By.Id("service-roam");
        private readonly By siteRoam = By.Id("site-roam");
        private readonly By thirdParty = By.Id("third-party");
        private readonly By generic = By.Id("generic");
        private readonly By startDate = By.Id("startDate");
        private readonly By endDate = By.Id("endDate");
        private readonly By associatedAccount = By.Id("user");
        private readonly By workingTime = By.Id("record-working-time");
        private readonly By contactNum = By.Id("contactNumber");
        private readonly By personalContactNum = By.Id("personalNumber");

        //Resource type options
        private readonly string resourceOptions = "//span[text()='{0}']";

        #region shiftSchedules-tab
        public readonly By ShiftScheduleTab = By.XPath("//a[@aria-controls='shiftSchedules-tab']");
        public readonly By AddNewShiftScheduleButton = By.XPath("//div[@id='shiftSchedules-tab']//button[text()='Add New Item']");
        #endregion

        [AllureStep]
        public ResourceDetailTab IsOnDetailTab()
        {
            WaitUtil.WaitForElementVisible(resourceName);
            WaitUtil.WaitForElementVisible(resourceBtn);
            //WaitUtil.WaitForElementVisible(resourceType);
            WaitUtil.WaitForElementVisible(clientReference);
            WaitUtil.WaitForElementVisible(contract);
            WaitUtil.WaitForElementVisible(service);
            WaitUtil.WaitForElementVisible(businessUnit);
            WaitUtil.WaitForElementVisible(contractUnit);
            WaitUtil.WaitForElementVisible(site);
            WaitUtil.WaitForElementVisible(contractRoam);
            WaitUtil.WaitForElementVisible(serviceRoam);
            WaitUtil.WaitForElementVisible(siteRoam);
            WaitUtil.WaitForElementVisible(thirdParty);
            WaitUtil.WaitForElementVisible(generic);
            WaitUtil.WaitForElementVisible(startDate);
            WaitUtil.WaitForElementVisible(endDate);
            WaitUtil.WaitForElementVisible(associatedAccount);
            WaitUtil.WaitForElementVisible(workingTime);
            WaitUtil.WaitForElementVisible(contactNum);
            WaitUtil.WaitForElementVisible(personalContactNum);
            return this;
        }
        [AllureStep]
        public ResourceDetailTab InputResourceName(string name)
        {
            SendKeys(resourceName, name);
            return this;
        }
        [AllureStep]
        public ResourceDetailTab SelectResourceType(string type)
        {
            ClickOnElement(resourceBtn);
            ClickOnElement(String.Format(resourceOptions, type));

            //Unable to interact with this select 
            //SelectTextFromDropDown(resourceType, type);
            return this;
        }
        [AllureStep]
        public ResourceDetailTab SelectBusinessUnit(string businessUnitValue)
        {
            SelectTextFromDropDown(businessUnitDd, businessUnitValue);
            return this;
        }

        [AllureStep]
        public ResourceDetailTab SelectService(string _service)
        {
            SelectTextFromDropDown(service, _service);
            return this;
        }
        [AllureStep]
        public ResourceDetailTab TickSiteRoam()
        {
            if (!IsElementSelected(siteRoam)) ClickOnElement(siteRoam);
            return this;
        }
        [AllureStep]
        public ResourceDetailTab UntickSiteRoam()
        {
            if (IsElementSelected(siteRoam)) ClickOnElement(siteRoam);
            return this;
        }
        [AllureStep]
        public ResourceDetailTab TickContractRoam()
        {
            if (!IsElementSelected(contractRoam)) ClickOnElement(contractRoam);
            return this;
        }
        [AllureStep]
        public ResourceDetailTab UntickContractRoam()
        {
            if (IsElementSelected(contractRoam)) ClickOnElement(contractRoam);
            return this;
        }
    }
}
