using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Models.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ScheduleTaskTab : BasePageCommonActions
    {
        public readonly By AddNewItemButton = By.XPath("//div[@id='schedules-tab']//button[text()[contains(.,'Add New Item')]]");

        private string ScheduleTaskTable = "//div[@id='schedules-tab']//table//tbody";
        private string ScheduleRow = "./tr";
        private string ScheduleCell = "./td";
        private string StartDateCell = "./td";
        private string EndDateCell = "./td";
        private string RolloverCell = "./td//input[@type='checkbox']";
        private string RoundCell = "./td";
        private string RoundGroupCell = "./td";
        private string DuplicateButtonCell = "./td//button[@title='Duplicate']";

        public TableElement ScheduleTaskTableEle
        {
            get => new TableElement(ScheduleTaskTable, ScheduleRow, new List<string>() { ScheduleCell, StartDateCell, EndDateCell, RolloverCell, RoundCell, RoundGroupCell, DuplicateButtonCell });
        }

        public ScheduleTaskTab VerifyTableIsReadonly()
        {
            var rowCount = ScheduleTaskTableEle.GetRows().Count;
            for (int i = 0; i < rowCount; i++)
            {
                VerifyElementEnable(ScheduleTaskTableEle.GetCell(i, 3), false);
            }
            return this;
        }

        public ScheduleTaskTab DoubleClickSchedule(int rowIdx)
        {
            ScheduleTaskTableEle.DoubleClickRow(rowIdx);
            return this;
        }
    }
}
