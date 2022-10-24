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
    public class ResourceTermPage : BasePageCommonActions
    {
        public ResourceTermPage()
        {
            resourceTermTableEle = new TableElement(ResourceTermTable, ResourceTermRow, new List<string>() { ResourceTermCheckboxCell, ResourceTermIdCell, ResourceTermPrefixCell, ResourceTermNameCell, ResourceTermStartDateCell, ResourceTermEndDateCell });
            resourceTermTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        private string ResourceTermTable = "//div[@class='grid-canvas']";
        private string ResourceTermRow = "./div[contains(@class, 'slick-row')]";
        private string ResourceTermCheckboxCell = "./div[contains(@class, 'l0')]//input";
        private string ResourceTermIdCell = "./div[contains(@class, 'l1')]";
        private string ResourceTermPrefixCell = "./div[contains(@class, 'l2')]";
        private string ResourceTermNameCell = "./div[contains(@class, 'l3')]";
        private string ResourceTermStartDateCell = "./div[contains(@class, 'l4')]";
        private string ResourceTermEndDateCell = "./div[contains(@class, 'l5')]";

        private TableElement resourceTermTableEle;
        public TableElement ResourceTermTableEle
        {
            get => resourceTermTableEle; 
        }
        [AllureStep]

        public ResourceTermPage DoubleClickRow(int rowIdx)
        {
            ResourceTermTableEle.DoubleClickRow(rowIdx);
            return this;
        }
    }
}
