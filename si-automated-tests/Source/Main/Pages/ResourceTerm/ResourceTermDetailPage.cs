using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using si_automated_tests.Source.Core.WebElements;
using NUnit.Allure.Attributes;

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

        #region EntitleTab
        public readonly By AddEntitleButton = By.XPath("//button[contains(@data-bind, 'addResourceTermState')]");

        public int GetNewRowIdx()
        {
            return ResourceTermTableEle.GetRows().Count - 1;
        }

        public ResourceTermDetailPage VerifyNewRowIsDisappear(int rowIdx)
        {
            Assert.IsTrue(GetNewRowIdx() < rowIdx);
            return this;
        }

        public ResourceTermDetailPage EditResourceEntitleValues(int rowIdx, string state, string days, bool proDate, string stateDate, string endDate)
        {
            ResourceTermTableEle.SetCellValue(rowIdx, ResourceTermTableEle.GetCellIndex(ResourceStateCell), state);
            ResourceTermTableEle.SetCellValue(rowIdx, ResourceTermTableEle.GetCellIndex(EntitleDaysCell), days);
            ResourceTermTableEle.SetCellValue(rowIdx, ResourceTermTableEle.GetCellIndex(ProRataCell), proDate);
            IWebElement startDateCell = ResourceTermTableEle.GetCell(rowIdx, ResourceTermTableEle.GetCellIndex(StartDateCell));
            IWebElement endDateCell = ResourceTermTableEle.GetCell(rowIdx, ResourceTermTableEle.GetCellIndex(EndDateCell));
            InputCalendarDate(startDateCell, stateDate);
            InputCalendarDate(endDateCell, endDate);
            return this;
        }

        public ResourceTermDetailPage VerifyResourceEntitleValues(int rowIdx, string state, string days, bool proDate, string stateDate, string endDate)
        {
            VerifyCellValue(ResourceTermTableEle, rowIdx, ResourceTermTableEle.GetCellIndex(ResourceStateCell), state);
            VerifyCellValue(ResourceTermTableEle, rowIdx, ResourceTermTableEle.GetCellIndex(EntitleDaysCell), days);
            VerifyCellValue(ResourceTermTableEle, rowIdx, ResourceTermTableEle.GetCellIndex(ProRataCell), proDate);
            VerifyCellValue(ResourceTermTableEle, rowIdx, ResourceTermTableEle.GetCellIndex(StartDateCell), stateDate);
            VerifyCellValue(ResourceTermTableEle, rowIdx, ResourceTermTableEle.GetCellIndex(EndDateCell), endDate);
            return this;
        }
        #endregion

        [AllureStep]
        public ResourceTermDetailPage VerifyResourceStateValue(int rowIdx, List<string> resourceStates)
        {
            ResourceTermTableEle.ClickCell(rowIdx, 0);
            IWebElement webElement = ResourceTermTableEle.GetCell(rowIdx, 0);
            VerifySelectContainDisplayValues(webElement, resourceStates);
            return this;
        }
    }
}
