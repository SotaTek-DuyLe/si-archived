using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.PointAddress
{
    public class PointAddressListingPage : BasePage
    {
        private readonly By addNewPointAddressBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By firstPointAddressRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l6 r6')]/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");

        //DYNAMIC LOCATOR
        private const string columnInRowPointAddress = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        public PointAddressListingPage WaitForPointAddressPageDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(addNewPointAddressBtn);
            return this;
        }

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


        public PointAddressDetailPage DoubleClickFirstPointAddressRow()
        {
            DoubleClickOnElement(firstPointAddressRow);
            return PageFactoryManager.Get<PointAddressDetailPage>();
        }

        public PointAddressListingPage FilterPointAddressWithId(string pointAddressId)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, pointAddressId);
            ClickOnElement(applyBtn);
            return this;
        }
    }
}
