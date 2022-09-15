using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitPage : BasePageCommonActions
    {
        public readonly By UnitIframe = By.XPath("//div[@id='iframe-container']//iframe");
        private readonly string ServiceUnitTable = "//div[@class='echo-grid']//div[@class='grid-canvas']";
        private readonly string ServiceUnitRow = "./div[contains(@class, 'slick-row')]";
        private readonly string IdCell = "./div[contains(@class, 'l1 r1')]";
        private readonly string NameCell = "./div[contains(@class, 'l2 r2')]";
        private readonly By applyFilterBtn = By.XPath("//button[@title='Apply Filters']");
        private readonly By retireBtn = By.XPath("button[title='Retire']");

        public TableElement ServiceUnitTableEle
        {
            get => new TableElement(ServiceUnitTable, ServiceUnitRow, new List<string>() { IdCell, NameCell });
        }

        public ServiceUnitPage DoubleClickServiceUnit()
        {
            ServiceUnitTableEle.DoubleClickRow(0);
            return this;
        }

        public ServiceUnitPage DoubleClickServiceUnitById(string id)
        {
            var row = ServiceUnitTableEle.GetRowByCellValue(0, id);
            DoubleClickOnElement(row);
            return this;
        }

        public ServiceUnitPage FindServiceUnitWithId(string serviceUnitId)
        {
            SendKeys(By.XPath("//div[contains(@class, 'slick-headerrow-column l1 r1')]//input"), serviceUnitId);
            SleepTimeInMiliseconds(300);
            ClickOnElement(applyFilterBtn);
            WaitForLoadingIconToDisappear(false);
            return this;
        }

    }
}
