using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.ResourceTerm
{
    public class ResourceTermDetailPage : BasePageCommonActions
    {
        public readonly By EntitlementTab = By.XPath("//a[@aria-controls='entitlements-tab']");
        private string EntitlementTable = "//table//tbody[contains(@data-bind, 'resourceTermStates')]";
        private string EntitlementRow = "./tr";
        private string ResourceStateCell = "./td[not(contains(@style,'display: none;'))]//select[contains(@data-bind, 'resourceStates')]";
        private string EntitleDaysCell = "./td[not(contains(@style,'display: none;'))]//input[@class='smaller-txtbox']";
        private string ProRataCell = "./td[not(contains(@style,'display: none;'))]//input[@type='checkbox']";
        private string StartDateCell = "./td[not(contains(@style,'display: none;'))]//input[@id='start-date']";
        private string EndDateCell = "./td[not(contains(@style,'display: none;'))]//input[@id='end-date']";
        private string RemoveResourceButtonCell = "./td[not(contains(@style,'display: none;'))]//button[contains(@data-bind, 'removeResourceTermState')]";

        public TableElement ResourceTermTableEle
        {
            get => new TableElement(EntitlementTable, EntitlementRow, new List<string>() { ResourceStateCell, EntitleDaysCell, ProRataCell, StartDateCell, EndDateCell, RemoveResourceButtonCell });
        }

        public ResourceTermDetailPage VerifyResourceStateValue(int rowIdx, List<string> resourceStates)
        {
            ResourceTermTableEle.ClickCell(rowIdx, 0);
            IWebElement webElement = ResourceTermTableEle.GetCell(rowIdx, 0);
            VerifySelectContainDisplayValues(webElement, resourceStates);
            return this;
        }
    }
}
