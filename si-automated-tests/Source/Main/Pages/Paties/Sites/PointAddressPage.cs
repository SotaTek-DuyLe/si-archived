using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Paties.Sites
{
    public class PointAddressPage : BasePageCommonActions
    {
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

        public PointAddressPage ClickServiceUnit(string service)
        {
            AllServiceTableEle.ClickCellOnCellValue(2, 1, service);
            return this;
        }
    }
}
