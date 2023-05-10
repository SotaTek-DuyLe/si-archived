using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Common
{
    public class PointPickerPage : BasePageCommonActions
    {
        public readonly By PointDescriptionInput = By.XPath("//div[@id='results-grid']//div[contains(@class, 'slick-headerrow-column l1 r1')]//input");
        public readonly By SearchButton = By.XPath("//button[text()='Search']");
        private readonly By ServiceRows = By.XPath("//div[@id='services-tab']//div[@class='parent-row']//div[@data-bind='foreach: $data.asset']");
        private readonly By EventSelect = By.XPath("//div[@id='services-tab']//div[@class='parent-row']//div[@id='create-event-dropdown']//ul");

        public readonly By ServiceTab = By.XPath("//a[@aria-controls='services-tab']");
        public readonly By AssetTypeColumn = By.XPath("//div[@class='services-grid--root']//div[text()='Asset Type (Product)']");

        private string ResultTable = "//div[@id='results-grid']//div[@class='grid-canvas']";
        private string ResultRow = "./div[contains(@class, 'slick-row')]";
        private string PointIdCell = "./div[@class='slick-cell l0 r0']";
        private string PointDescriptionCell = "./div[@class='slick-cell l1 r1']";
        public TableElement ResultTableEle
        {
            get => new TableElement(ResultTable, ResultRow, new List<string>() { PointIdCell, PointDescriptionCell });
        }

        [AllureStep]
        public IWebElement GetPostCodeInput()
        {
            return this.driver.FindElements(By.XPath("//input[@id='postcode']")).FirstOrDefault(x => x.Displayed);
        }

        [AllureStep]
        public PointPickerPage SetPostCode(string value)
        {
            SetInputValue(GetPostCodeInput(), value);
            return this;
        }

        [AllureStep]
        public PointPickerPage SelectPoint(string pointAddress)
        {
            var rows = ResultTableEle.GetRows();
            for (int i = 0; i < rows.Count; i++)
            {
                string description = ResultTableEle.GetCellValue(i, ResultTableEle.GetCellIndex(PointDescriptionCell)).AsString();
                string cellPointAddress = description.Split(',').First().Trim();
                if (cellPointAddress == pointAddress)
                {
                    ResultTableEle.ClickRow(i);
                }
            }
            return this;
        }

        [AllureStep]
        public PointPickerPage VerifyPointAddressAndClickEventButton(List<string> assetTypes, string eventValue)
        {
            var serviceRows = GetAllElements(ServiceRows);
            int clickedRow = 0;
            for (int i = 0; i < serviceRows.Count; i++)
            {
                string[] serviceAssetTypes = serviceRows[i].FindElements(By.XPath("./div[@data-bind='text: $data']")).Select(x => x.Text).ToArray();
                if(serviceAssetTypes.Length == 6)
                {
                    foreach (var assetType in assetTypes)
                    {
                        Assert.IsTrue(serviceAssetTypes.Any(x => x.Contains(assetType)));
                    }
                    clickedRow = i;
                }
            }
            var eventButtons = GetAllElements(By.XPath("//div[@id='services-tab']//div[@class='parent-row']//div[@class='services-grid--add-event' and text()='Event']"));
            eventButtons[clickedRow].Click();
            SleepTimeInMiliseconds(300);
            SelectByDisplayValueOnUlElement(EventSelect, eventValue);
            return this;
        }
    }
}
