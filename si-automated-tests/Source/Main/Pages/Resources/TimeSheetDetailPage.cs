using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Resources;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class TimeSheetDetailPage : BasePageCommonActions
    {
        private readonly By bussinessTitle = By.XPath("//span[@title='Business Unit Group - Business Unit: Party']");
        private readonly By bussinessTitleWithoutParty = By.XPath("//span[@title='Business Unit Group - Business Unit']");
        private string timesheetTable = "//div[@id='timesheet-tab']//table/tbody";
        private string timesheetRow = "./tr";
        private string EmployeeNumberCell = "./td//span[@data-bind='text: employeeNumber']";
        private string JobTitleCell = "./td//span[@data-bind='text: resourceType']";
        private string NameCell = "./td//span[@class='btn-link']";
        public TableElement TimeSheetTableEle
        {
            get => new TableElement(timesheetTable, timesheetRow, new List<string>() { EmployeeNumberCell, NameCell, JobTitleCell });
        }

        [AllureStep]
        public TimeSheetDetailPage ClickOpenResource()
        {
            TimeSheetTableEle.ClickCell(0, TimeSheetTableEle.GetCellIndex(NameCell));
            return this;
        }

        [AllureStep]
        public TimeSheetDetailPage VerifyBussinessTitle(TimeSheetModel timeSheetModel)
        {
            if (string.IsNullOrEmpty(timeSheetModel.Supplier))
            {
                VerifyElementText(bussinessTitleWithoutParty, string.Format("{0} - {1}", timeSheetModel.BussinessUnitGroup, timeSheetModel.BussinessUnit));
            }
            else
            {
                VerifyElementText(bussinessTitle, string.Format("{0} - {1}: {2}", timeSheetModel.BussinessUnitGroup, timeSheetModel.BussinessUnit, timeSheetModel.Supplier));
            }
            return this;
        }
    }
}