using System;
using System.Collections.Generic;
using System.Text;
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

        private string TaskLineTable = "//div[@id='tasklines-tab']//table//tbody";
        private string TaskLineRow = "./tr";
        private string TaskLineOrderCell = "./td//input[@id='order.id']";
        private string TaskLineTypeCell = "./td//select[@id='taskLineType.id']";
        private string TaskLineAssetTypeCell = "./td//echo-select[contains(@params, 'assetType')]//select";
        private string TaskLineScheduleAssetQtyCell = "./td//input[@id='assetQuantityScheduled.id']";
        private string TaskLineMinAssetQtyCell = "./td//input[@id='minAssetQty.id']";
        private string TaskLineMaxAssetQtyCell = "./td//input[@id='maxAssetQty.id']";
        private string TaskLineProductCell = "./td//echo-select[contains(@params, 'product')]//select";
        private string TaskLineSheduleProductQtyCell = "./td//input[@id='productQuantityScheduled.id']";
        private string TaskLineUnitCell = "./td//echo-select[contains(@params, 'pUnit')]//select";
        private string TaskLineStartDate = "./td[@data-bind='text: startDate.value']";
        private string TaskLineEndDate = "./td[@data-bind='text: endDate.value']";

        public TableElement TaskLineTableEle
        {
            get => new TableElement(TaskLineTable, TaskLineRow, new List<string>() { TaskLineOrderCell, TaskLineTypeCell, TaskLineAssetTypeCell, TaskLineScheduleAssetQtyCell, 
                TaskLineMinAssetQtyCell, TaskLineMaxAssetQtyCell, TaskLineProductCell, TaskLineSheduleProductQtyCell, TaskLineUnitCell, TaskLineStartDate, TaskLineEndDate});
        }

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
        public ServiceTaskLineTab verifyTaskInfo(String _assetType, String _scheduledAssetQty, String _product, String _unit, String _endDate)
        {
            Assert.AreEqual(_assetType, GetFirstSelectedItemInDropdown(assetType));
            Assert.AreEqual(_scheduledAssetQty, GetAttributeValue(scheduleAssetQty, "value"));
            Assert.AreEqual(_product, GetFirstSelectedItemInDropdown(product));
            Assert.AreEqual(_unit, GetFirstSelectedItemInDropdown(unit));
            Assert.AreEqual(_endDate, GetElementText(endDate));
            return this;
        }

        public ServiceTaskLineTab verifyTaskLineStartDate(string date)
        {
            string startdate = GetElementText(startDate);
            Assert.AreEqual(startdate, date);
            return this;
        }
        public ServiceTaskLineTab verifyTaskLineEndDate(string date)
        {
            string enddate = GetElementText(endDate);
            Assert.AreEqual(enddate, date);
            return this;
        }
    }
}
