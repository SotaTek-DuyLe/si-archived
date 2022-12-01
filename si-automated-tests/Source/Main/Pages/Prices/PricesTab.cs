using NUnit.Allure.Attributes;
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

namespace si_automated_tests.Source.Main.Pages.Prices
{
    public class PricesTab : BasePageCommonActions
    {
        public PricesTab()
        {
            pricesTableEle = new TableElement(PricesTable, PricesRow, new List<string>() { PricesListCell, AddNewPriceButtonCell });
            pricesTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.Where(row => row.FindElements(By.XPath(PricesListCell)).Count != 0).ToList();
            };
        }

        public readonly By ApplyChangesButton = By.XPath("//button[@title='Save']");
        public readonly By PricesIFrame = By.XPath("//iframe[@id='prices-tab']");
        public readonly By PriceTab = By.XPath("//a[@aria-controls='prices-tab']");
        private string PricesTable = "//table//tbody";
        private string PricesRow = "./tr[not(@class)]";
        private string PricesListCell = "./td//div[@class='price-list']";
        private string AddNewPriceButtonCell = "./td//button[@class='btn-link add-price']";

        private TableElement pricesTableEle;
        public TableElement PriceTableEle
        {
            get => pricesTableEle;
        }

        private string PricesInputRow = "./tr[contains(@data-bind, 'visible: name()') and not(contains(@style,'display: none;'))]";
        private string PriceNameInputCell = "./td//input[@placeholder='Price Name']";
        private string PriceCell = "./td//input[@placeholder='Price £']";
        private string MinPriceCell = "./td//input[@placeholder='Min Price £']";
        private string UnitCell = "./td//select[contains(@data-bind, 'Select Unit')]";
        private string QtyCell = "./td//input[@placeholder='Unit Qty']";
        private string RemoveButtonCell = "./td//button[@title='Retire/Remove']";
        
        public TableElement PricesInputTable
        {
            get => new TableElement(PricesTable, PricesInputRow, new List<string>() { PriceNameInputCell, PriceCell, MinPriceCell, UnitCell, QtyCell, RemoveButtonCell });
        }
        [AllureStep]

        public PricesTab ClickAddNewPrice(int rowIdx)
        {
            PriceTableEle.ClickCell(rowIdx, 1);
            return this;
        }
        [AllureStep]

        public PricesTab EditPriceRecord(int rowIdx, string name, string price, string minprice)
        {
            PricesInputTable.SetCellValue(rowIdx, 0, name);
            PricesInputTable.SetCellValue(rowIdx, 1, price);
            PricesInputTable.SetCellValue(rowIdx, 2, minprice);
            PricesInputTable.ClickCell(rowIdx, 4);
            return this;
        }
        [AllureStep]

        public PricesTab VerifyPriceRecord(int rowIdx, string name, string price, string minprice)
        {
            VerifyCellValue(PricesInputTable, rowIdx, 0, name);
            VerifyCellValue(PricesInputTable, rowIdx, 1, price);
            VerifyCellValue(PricesInputTable, rowIdx, 2, minprice);
            return this;
        }
    }
}
