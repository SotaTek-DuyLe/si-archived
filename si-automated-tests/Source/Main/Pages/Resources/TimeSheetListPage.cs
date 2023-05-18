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
    public class TimeSheetListPage : BasePageCommonActions
    {
        public readonly By TimeSheetIframe = By.XPath("//div[@id='iframe-container']//iframe");
        public readonly By BussinessUnitGroupFilterInput = By.XPath("//div[@class='ui-state-default slick-headerrow-column l3 r3']//input");
        public readonly By SupplierFilterInput = By.XPath("//div[@class='ui-state-default slick-headerrow-column l5 r5']//input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private string TimeSheetTable = "//div[@class='grid-canvas']";
        private string TimeSheetRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[@class='slick-cell l0 r0']//input";
        private string IdCell = "./div[@class='slick-cell l1 r1']";
        private string ContractCell = "./div[@class='slick-cell l2 r2']";
        private string BusinessUnitGroupCell = "./div[@class='slick-cell l3 r3']";
        private string BusinessUnitCell = "./div[@class='slick-cell l4 r4']";
        private string SupplierCell = "./div[@class='slick-cell l5 r5']";

        public TableElement TimeSheetTableEle
        {
            get => new TableElement(TimeSheetTable, TimeSheetRow, new List<string>() { CheckboxCell, IdCell, ContractCell, BusinessUnitGroupCell, BusinessUnitCell, SupplierCell });
        }

        [AllureStep]
        public TimeSheetListPage VerifyTimeSheetDisplayCorrectly()
        {
            VerifyElementVisibility(By.XPath(TimeSheetTable), true);
            Assert.IsNotNull(TimeSheetTableEle.GetCell(0, TimeSheetTableEle.GetCellIndex(CheckboxCell)));
            Assert.IsNotNull(TimeSheetTableEle.GetCell(0, TimeSheetTableEle.GetCellIndex(IdCell)));
            Assert.IsNotNull(TimeSheetTableEle.GetCell(0, TimeSheetTableEle.GetCellIndex(ContractCell)));
            Assert.IsNotNull(TimeSheetTableEle.GetCell(0, TimeSheetTableEle.GetCellIndex(BusinessUnitGroupCell)));
            Assert.IsNotNull(TimeSheetTableEle.GetCell(0, TimeSheetTableEle.GetCellIndex(BusinessUnitCell)));
            Assert.IsNotNull(TimeSheetTableEle.GetCell(0, TimeSheetTableEle.GetCellIndex(SupplierCell)));
            return this;
        }

        [AllureStep]
        public TimeSheetListPage FilterBussinessUnitGroup(string unit)
        {
            SendKeys(BussinessUnitGroupFilterInput, unit);
            WaitForLoadingIconToDisappear();
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public TimeSheetListPage VerifyBussinessUnitGroup(string unit)
        {
            if (TimeSheetTableEle.GetRows().Count > 0)
            {
                VerifyCellValue(TimeSheetTableEle, 0, TimeSheetTableEle.GetCellIndex(BusinessUnitGroupCell), unit);
            }
            return this;
        }

        [AllureStep]
        public TimeSheetListPage FilterSupplier(string supplier)
        {
            SendKeys(SupplierFilterInput, supplier);
            WaitForLoadingIconToDisappear();
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public TimeSheetListPage VerifySupplier(string supplier)
        {
            if (TimeSheetTableEle.GetRows().Count > 0)
            {
                VerifyCellValue(TimeSheetTableEle, 0, TimeSheetTableEle.GetCellIndex(SupplierCell), supplier);
            }
            return this;
        }

        [AllureStep]
        public TimeSheetModel DoubleClickBussinessGroup(int rowIdx = 0)
        {
            TimeSheetModel timeSheetModel = new TimeSheetModel();
            timeSheetModel.BussinessUnitGroup = TimeSheetTableEle.GetCellValue(rowIdx, TimeSheetTableEle.GetCellIndex(BusinessUnitGroupCell)).AsString();
            timeSheetModel.BussinessUnit = TimeSheetTableEle.GetCellValue(rowIdx, TimeSheetTableEle.GetCellIndex(BusinessUnitCell)).AsString();
            timeSheetModel.Supplier = TimeSheetTableEle.GetCellValue(rowIdx, TimeSheetTableEle.GetCellIndex(SupplierCell)).AsString();
            TimeSheetTableEle.DoubleClickRow(rowIdx);
            return timeSheetModel;
        }

        [AllureStep]
        public TimeSheetModel DoubleClickEmptySupplier()
        {
            IWebElement row = TimeSheetTableEle.GetRowByCellValue(TimeSheetTableEle.GetCellIndex(SupplierCell), "");
            int rowIdx = TimeSheetTableEle.GetRows().IndexOf(row);
            return DoubleClickBussinessGroup(rowIdx);
        }
    }
}
