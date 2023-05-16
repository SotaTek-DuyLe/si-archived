using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.PointAddress
{
    public class PointAddressListingPage : BasePage
    {
        public readonly By addNewPointAddressBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By firstPointAddressRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l1 r1')]/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By allPointAddressRows = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        //DYNAMIC LOCATOR
        private const string columnInRowPointAddress = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        private string PointAddressTable = "//div[@class='grid-canvas']";
        private string PointAddressRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[contains(@class, 'l0')]//input[@type='checkbox']";
        private string IdCell = "./div[contains(@class, 'l1')]";
        private string NameCell = "./div[contains(@class, 'l2')]";
        private string ClientRefCell = "./div[contains(@class, 'l3')]";
        private string LatCell = "./div[contains(@class, 'l4')]";
        private string LonCell = "./div[contains(@class, 'l5')]";
        private string StartDateCell = "./div[contains(@class, 'l6')]";
        private string EndDateCell = "./div[contains(@class, 'l7')]";
        public TableElement PointAddressTableEle
        {
            get => new TableElement(PointAddressTable, PointAddressRow, new List<string>() { CheckboxCell, IdCell, NameCell, ClientRefCell, LatCell, LonCell, StartDateCell, EndDateCell });
        }

        [AllureStep]
        public PointAddressListingPage DoubleClickPointAddress(string pointId)
        {
            PointAddressTableEle.DoubleClickCellOnCellValue(1, 1, pointId);
            return this;
        }

        [AllureStep]
        public PointAddressListingPage VerifyPointAddressHasEndDate(string description)
        {
            var cellVal = PointAddressTableEle.GetCellByCellValues(7, new Dictionary<int, object>() { { 2, description } });
            Assert.IsNotEmpty(cellVal.Text);
            return this;
        }

        [AllureStep]
        public PointAddressListingPage WaitForPointAddressPageDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(addNewPointAddressBtn);
            WaitUtil.WaitForElementVisible(allPointAddressRows);
            return this;
        }
        [AllureStep]
        public List<PointAddressModel> getAllPointAddressInList(int numberOfRow)
        {
            List<PointAddressModel> allModel = new List<PointAddressModel>();
            for (int i = 0; i < numberOfRow; i++)
            {
                string id = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[0])[i]);
                string name = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[1])[i]);
                string uprn = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[2])[i]);
                string postcode = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[3])[i]);
                string propertyName = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[4])[i]);
                string toProperty = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[5])[i]);
                string subBuilding = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[6])[i]);
                string propertySuff = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[7])[i]);
                string pointSegmentId = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[8])[i]);
                string pointAddressType = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[9])[i]);
                string startDate = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[10])[i]);
                string endDate = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointAddressColumn[11])[i]);
                
                allModel.Add(new PointAddressModel(id, name, uprn, postcode, propertyName, toProperty, subBuilding, propertySuff, pointSegmentId, pointAddressType, startDate, endDate));
            }
            return allModel;
        }

        [AllureStep]
        public PointAddressDetailPage DoubleClickFirstPointAddressRow()
        {
            DoubleClickOnElement(firstPointAddressRow);
            return PageFactoryManager.Get<PointAddressDetailPage>();
        }
        [AllureStep]
        public PointAddressListingPage FilterPointAddressWithId(string pointAddressId)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, pointAddressId);
            ClickOnElement(applyBtn);
            return this;
        }

        [AllureStep]
        public PointAddressListingPage VerifyDisplayVerticalScrollBarPointAddressPage()
        {
            List<IWebElement> webElements = GetAllElements(allPointAddressRows);
            if (webElements.Count >= 25)
            {
                VerifyDisplayVerticalScrollBar(containerPage);
            }
            return this;
        }
    }
}
