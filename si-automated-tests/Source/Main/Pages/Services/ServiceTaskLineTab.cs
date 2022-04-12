using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceTaskLineTab : BasePage
    {
        private readonly By assetType = By.XPath("//div[contains(@data-bind,'assetType')]//select");
        private readonly By scheduleAssetQty = By.Id("assetQuantityScheduled.id");
        private readonly By product = By.XPath("//echo-select[contains(@params,'product')]/select");
        private readonly By unit = By.XPath("//div[contains(@data-bind,'pUnit')]//select");
        private readonly string startDate = "//div[@id='tasklines-tab']//td[contains(@data-bind,'startDate')]";
        private readonly string endDate = "//div[@id='tasklines-tab']//td[contains(@data-bind,'endDate')]";

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
