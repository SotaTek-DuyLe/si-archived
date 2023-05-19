using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using System.Collections.Generic;

namespace si_automated_tests.Source.Main.Pages.Paties.Sites
{
    public class PointAddressPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//h4[text()='Point Address']");
        private readonly By detailsTab = By.CssSelector("a[aria-controls='details-tab']");
        public readonly By AllServiceTab = By.XPath("//a[@aria-controls='allServices-tab']");
        public readonly string AllServiceTable = "//div[@id='allServices-tab']//table//tbody";
        public readonly string AllServiceRow = "./tr";
        public readonly string AllServiceContractCell = "./td[@data-bind='text: $data.contract']";
        public readonly string AllServiceServiceCell = "./td[@data-bind='text: $data.service']";
        public readonly string AllServiceServiceUnitCell = "./td//a";
        public readonly string AllServiceTaskCountCell = "./td[@data-bind='text: $data.taskCount']";

        public TableElement AllServiceTableEle
        {
            get => new TableElement(AllServiceTable, AllServiceRow, new List<string>() { AllServiceContractCell, AllServiceServiceCell, AllServiceServiceUnitCell, AllServiceTaskCountCell });
        }

        [AllureStep]
        public PointAddressPage ClickServiceUnit(string service)
        {
            AllServiceTableEle.ClickCellOnCellValue(2, 1, service);
            return this;
        }

        [AllureStep]
        public PointAddressPage IsPointAddressPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailsTab);
            WaitUtil.WaitForElementVisible(AllServiceTab);
            return this;
        }

        [AllureStep]
        public PointAddressPage ClickOnAllServicesTab()
        {
            ClickOnElement(AllServiceTab);
            return this;
        }
    }
}
