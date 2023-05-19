using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceTaskLineTab : BasePageCommonActions
    {
        private readonly By assetType = By.XPath("//div[contains(@data-bind,'assetType')]//select");
        private readonly By scheduleAssetQty = By.Id("assetQuantityScheduled.id");
        private readonly By product = By.XPath("//echo-select[contains(@params,'product')]/select");
        private readonly By unit = By.XPath("//div[contains(@data-bind,'pUnit')]//select");
        private readonly string startDate = "//div[@id='tasklines-tab']//td[contains(@data-bind,'startDate')]";
        private readonly string endDate = "//div[@id='tasklines-tab']//td[contains(@data-bind,'endDate')]";
        public readonly By AddNewItemButton = By.XPath("//div[@id='tasklines-tab']//button[text()[contains(.,'Add New Item')]]");
        private string TaskLineTable = "//div[@id='tasklines-tab']//table//tbody";
        private string TaskLineRow = "./tr";
        private string OrderCell = "./td//input[@id='order.id']";
        private string TaskLineTypeCell = "./td//select[@id='taskLineType.id']";
        private string AssetTypeCell = "./td//echo-select[contains(@params, 'assetType')]//select";
        private string ScheduleAssetQtyCell = "./td//input[@id='assetQuantityScheduled.id']";
        private string MinAssetQtyCell = "./td//input[@id='minAssetQty.id']";
        private string MaxAssetQtyCell = "./td//input[@id='maxAssetQty.id']";
        private string ProductCell = "./td//echo-select[contains(@params, 'product')]//select";
        private string SheduleProductQtyCell = "./td//input[@id='productQuantityScheduled.id']";
        private string MinProductQtyCell = "./td//input[@id='minProductQty.id']";
        private string MaxProductQtyCell = "./td//input[@id='maxProductQty.id']";
        private string PUnitCell = "./td//echo-select[contains(@params, 'pUnit')]//select";
        private string SerialisedCell = "./td//input[contains(@data-bind, 'isSerialised')]";
        private string DestinationSiteCell = "./td//select[@id='destinationSite.id']";
        private string SiteProductCell = "./td//select[@id='siteProduct.id']";
        private string RetireButtonCell = "./td//button[contains(text(), 'Retire')]";
        private readonly By assetTypeDd = By.XPath("//td//echo-select[contains(@params, 'assetType')]//select");
        private readonly string assetTypeOption = "//td//echo-select[contains(@params, 'assetType')]//select//option[text()='{0}']";
        public readonly By ServiceDesTitle = By.XPath("//a[contains(@data-bind, 'serviceUnitDesc')]");

        public TableElement TaskLineTableEle
        {
            get => new TableElement(TaskLineTable, TaskLineRow, 
                new List<string>() { 
                    OrderCell, TaskLineTypeCell, AssetTypeCell, 
                    ScheduleAssetQtyCell, MinAssetQtyCell, MaxAssetQtyCell, 
                    ProductCell, SheduleProductQtyCell, MinProductQtyCell,
                    MaxProductQtyCell, PUnitCell, SerialisedCell, 
                    DestinationSiteCell, SiteProductCell, RetireButtonCell});
        }

        [AllureStep]
        public ServiceTaskLineTab VerifyTaskLineIsReadOnly()
        {
            var rowCount = TaskLineTableEle.GetRows().Count;
            for (int i = 0; i < rowCount; i++)
            {
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 0), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 1), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 2), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 3), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 4), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 5), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 6), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 7), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 8), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 9), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 10), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 11), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 12), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 13), false);
                VerifyElementEnable(TaskLineTableEle.GetCell(i, 14), false);
            }
            return this;
        }
        [AllureStep]
        public ServiceTaskLineTab DoubleClickTaskLine(int rowIdx)
        {
            TaskLineTableEle.DoubleClickRow(rowIdx);
            return this;
        }
        [AllureStep]
        public ServiceTaskLineTab VerifyTaskLine(string type, string assetType, string scheduleAssetQty, string product, string sheduleProductQty, string unit, string startDate, string endDate)
        {
            VerifyCellValue(TaskLineTableEle, 0, 1, type);
            VerifyCellValue(TaskLineTableEle, 0, 2, assetType);
            VerifyCellValue(TaskLineTableEle, 0, 3, scheduleAssetQty);
            VerifyCellValue(TaskLineTableEle, 0, 6, product);
            VerifyCellValue(TaskLineTableEle, 0, 7, sheduleProductQty);
            VerifyCellValue(TaskLineTableEle, 0, 8, unit);
            VerifyCellValue(TaskLineTableEle, 0, 9, startDate);
            VerifyCellValue(TaskLineTableEle, 0, 10, endDate);
            return this;
        }
        [AllureStep]
        public ServiceTaskLineTab verifyTaskInfo(String _assetType, String _scheduledAssetQty, String _product, String _unit, String _startDate, String _endDate)
        {
            Assert.AreEqual(_assetType, GetFirstSelectedItemInDropdown(assetType));
            Assert.AreEqual(_scheduledAssetQty, GetAttributeValue(scheduleAssetQty, "value"));
            Assert.AreEqual(_product, GetFirstSelectedItemInDropdown(product));
            Assert.AreEqual(_unit, GetFirstSelectedItemInDropdown(unit));
            Assert.AreEqual(_startDate, GetElementText(startDate));
            Assert.AreEqual(_endDate, GetElementText(endDate));
            return this;
        }
        [AllureStep]
        public ServiceTaskLineTab verifyTaskInfo(String _assetType, String _scheduledAssetQty, String _product, String _unit, String _endDate)
        {
            Assert.AreEqual(_assetType, GetFirstSelectedItemInDropdown(assetType));
            Assert.AreEqual(_scheduledAssetQty, GetAttributeValue(scheduleAssetQty, "value"));
            Assert.AreEqual(_product, GetFirstSelectedItemInDropdown(product));
            Assert.AreEqual(_unit, GetFirstSelectedItemInDropdown(unit));
            Assert.AreEqual(_endDate, GetElementText(endDate));
            return this;
        }
        [AllureStep]
        public ServiceTaskLineTab verifyTaskLineStartDate(string date)
        {
            string startdate = GetElementText(startDate);
            Assert.AreEqual(startdate, date);
            return this;
        }
        [AllureStep]
        public ServiceTaskLineTab verifyTaskLineEndDate(string date)
        {
            string enddate = GetElementText(endDate);
            Assert.AreEqual(enddate, date);
            return this;
        }

        [AllureStep]
        public ServiceTaskLineTab ClickOnAssetTypeAndSelectValue(string assetTypeValue)
        {
            ClickOnElement(assetTypeDd);
            ClickOnElement(assetTypeOption, assetTypeValue);
            return this;
        }

        [AllureStep]
        public ServiceTaskLineTab InputTaskLine(int rowIdx, string type, string assetType, string shedAssetQty, string product, string shedProductQty)
        {
            TaskLineTableEle.SetCellValue(rowIdx, TaskLineTableEle.GetCellIndex(OrderCell), rowIdx.ToString());
            TaskLineTableEle.SetCellValue(rowIdx, TaskLineTableEle.GetCellIndex(TaskLineTypeCell), type);
            TaskLineTableEle.SetCellValue(rowIdx, TaskLineTableEle.GetCellIndex(AssetTypeCell), assetType);
            TaskLineTableEle.SetCellValue(rowIdx, TaskLineTableEle.GetCellIndex(ScheduleAssetQtyCell), shedAssetQty);
            TaskLineTableEle.SetCellValue(rowIdx, TaskLineTableEle.GetCellIndex(ProductCell), product);
            TaskLineTableEle.SetCellValue(rowIdx, TaskLineTableEle.GetCellIndex(SheduleProductQtyCell), shedProductQty);
            return this;
        }

        [AllureStep]
        public ServiceTaskLineTab VerifyTaskLine(int rowIdx, string type, string assetType, string scheduleAssetQty, string product, string sheduleProductQty)
        {
            VerifyCellValue(TaskLineTableEle, rowIdx, 1, type);
            VerifyCellValue(TaskLineTableEle, rowIdx, 2, assetType);
            VerifyCellValue(TaskLineTableEle, rowIdx, 3, scheduleAssetQty);
            VerifyCellValue(TaskLineTableEle, rowIdx, 6, product);
            VerifyCellValue(TaskLineTableEle, rowIdx, 7, sheduleProductQty);
            return this;
        }

        [AllureStep]
        public int GetNewTaskLineIndex()
        {
            return TaskLineTableEle.GetRows().Count - 1;
        }
    }
}
