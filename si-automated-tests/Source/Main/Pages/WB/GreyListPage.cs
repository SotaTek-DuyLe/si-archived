using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.WB
{
    public class GreyListPage : BasePageCommonActions
    {
        private string GreyListTable = "//div[@class='grid-canvas']";
        private string GreyListRow = "./div[contains(@class, 'slick-row')]";
        private string GreyListCheckboxCell = "./div[contains(@class, 'l0')]//input";
        private string GreyListIdCell = "./div[contains(@class, 'l1')]";
        private string GreyListVehicle = "./div[contains(@class, 'l2')]";
        private string GreyListTicket = "./div[contains(@class, 'l3')]";

        public TableElement GreyListTableEle
        {
            get => new TableElement(GreyListTable, GreyListRow, new List<string>() { GreyListCheckboxCell, GreyListIdCell, GreyListVehicle, GreyListTicket });
        }

        public GreyListPage DoubleClickRow(string ticketNumber)
        {
            var row = GreyListTableEle.GetRowByCellValue(3, ticketNumber);
            DoubleClickOnElement(row);
            return this;
        }
    }
}
