using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages.Events;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class FindServiceUnitDetailPage : BasePage
    {
        private readonly By closeNotSavingBtn = By.XPath("//button[@title='Close Without Saving']");
        private readonly By serviceUnitLabel = By.XPath("//label[contains(string(), 'Service Unit')]");
        private readonly By serviceUnitInput = By.XPath("//label[contains(string(), 'Service Unit')]/following-sibling::input");
        private readonly By findBtn = By.CssSelector("button[title='Find']");
        private readonly By refreshBtn = By.CssSelector("button[title='Refresh']");
        private readonly By helpBtn = By.CssSelector("button[title='Help']");
        private readonly By closeWithoutSavingBtn = By.CssSelector("button[title='Close Without Saving']");

        //DYNAMIC
        private const string columnInGrid = "//tr/th[text()='{0}']";
        private const string resultNotFoundInGrid = "//td/label[text()=\"No results found '{0}'\"]";
        private readonly By allServiceUnitRows = By.XPath("//tbody/tr");
        private const string idInServiceUnitGrid = "//tbody/tr[{0}]/td[@data-bind='text: id']";
        private const string serviceUnitNameInServiceUnitGrid = "//tbody/tr[{0}]//a";
        private const string pointCoundInServiceUnitGrid = "//tbody/tr[{0}]/td[@data-bind='text: pointCount']";
        private const string selectLocatorInServiceUnitGrid = "//tbody/tr[{0}]//button";

        //Verify the display of service unit page
        public FindServiceUnitDetailPage IsFindServiceUnitPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(serviceUnitLabel);
            Assert.IsTrue(IsControlDisplayed(closeNotSavingBtn));
            Assert.IsTrue(IsControlDisplayed(serviceUnitLabel));
            Assert.IsTrue(IsControlDisplayed(serviceUnitInput));
            Assert.IsTrue(IsControlDisplayed(findBtn));
            //Refresh btn
            Assert.IsTrue(IsControlDisplayed(refreshBtn));
            //Help btn
            Assert.IsTrue(IsControlDisplayed(helpBtn));
            //Close without saving btn
            Assert.IsTrue(IsControlDisplayed(closeWithoutSavingBtn));
            foreach (string column in CommonConstants.ColumnInFindServiceUnitWindow)
            {
                Assert.IsTrue(IsControlDisplayed(columnInGrid, column));
            }
            return this;
        }

        //Click [Close without Saving] btn
        public FindServiceUnitDetailPage ClickCloseWithoutSavingBtn()
        {
            ClickOnElement(closeNotSavingBtn);
            return this;
        }

        //Click [Help] btn
        public FindServiceUnitDetailPage ClickHelpBtnAndVerify()
        {
            ClickOnElement(helpBtn);
            //Verify
            SwitchToLastWindow();
            Assert.AreEqual(WebUrl.MainPageUrl + "web/help", GetCurrentUrl());
            CloseCurrentWindow();
            SwitchToChildWindow(3);
            return this;
        }

        //Send key into [Search]
        public FindServiceUnitDetailPage InputKeyInSearch(string sectionValue)
        {
            SendKeys(serviceUnitInput, sectionValue);
            return this;
        }

        //Click [Find] btn
        public FindServiceUnitDetailPage ClickFindBtn()
        {
            ClickOnElement(findBtn);
            return this;
        }

        //Verify the display of the result after searching
        public FindServiceUnitDetailPage VerifyDisplayNoResultFound(string textSearch)
        {
            WaitUtil.WaitForElementVisible(resultNotFoundInGrid, textSearch);
            Assert.IsTrue(IsControlDisplayed(resultNotFoundInGrid, textSearch));
            return this;
        }

        //Get All Service unit in list
        public List<FindServiceUnitModel> GetAllServiceUnit(int numberOfRow)
        {
            WaitUtil.WaitForAllElementsPresent(allServiceUnitRows);
            List<FindServiceUnitModel> findServiceUnitModels = new List<FindServiceUnitModel>();
            for (int i = 0; i < numberOfRow; i++)
            {
                string id = GetElementText(idInServiceUnitGrid, (i + 1).ToString());
                string serviceUnitName = GetElementText(serviceUnitNameInServiceUnitGrid, (i + 1).ToString());
                string serviceUnitLocator = string.Format(serviceUnitNameInServiceUnitGrid, (i + 1).ToString());
                string pointCount = GetElementText(pointCoundInServiceUnitGrid, (i + 1).ToString());
                string selectLocator = string.Format(selectLocatorInServiceUnitGrid, (i + 1).ToString());
                findServiceUnitModels.Add(new FindServiceUnitModel(id, serviceUnitName, serviceUnitLocator, pointCount, selectLocator));
            }
            return findServiceUnitModels;
        }

        public List<FindServiceUnitModel> GetAllServiceUnit()
        {
            WaitUtil.WaitForAllElementsPresent(allServiceUnitRows);
            List<FindServiceUnitModel> findServiceUnitModels = new List<FindServiceUnitModel>();
            List<IWebElement> allRows = GetAllElements(allServiceUnitRows);
            for (int i = 0; i < allRows.Count; i++)
            {
                string id = GetElementText(idInServiceUnitGrid, (i + 1).ToString());
                string serviceUnitName = GetElementText(serviceUnitNameInServiceUnitGrid, (i + 1).ToString());
                string serviceUnitLocator = string.Format(serviceUnitNameInServiceUnitGrid, (i + 1).ToString());
                string pointCount = GetElementText(pointCoundInServiceUnitGrid, (i + 1).ToString());
                string selectLocator = string.Format(selectLocatorInServiceUnitGrid, (i + 1).ToString());
                findServiceUnitModels.Add(new FindServiceUnitModel(id, serviceUnitName, serviceUnitLocator, pointCount, selectLocator));
            }
            return findServiceUnitModels;
        }

        //Verify [Service unit] after searching
        public FindServiceUnitDetailPage VerifyResultAfterSearch(List<FindServiceUnitModel> findServiceUnitModels, string textSearch)
        {
            foreach(FindServiceUnitModel findServiceUnitModel in findServiceUnitModels)
            {
                Assert.IsTrue(findServiceUnitModel.serviceUnit.Contains(textSearch));
            }
            return this;
        }

        //Click any link in [Service unit]
        public ServiceUnitDetailPage ClickAnyLinkInServiceUnit(string linkToServiceDetail)
        {
            ClickOnElement(linkToServiceDetail);
            return new ServiceUnitDetailPage();
        }

        //Click any [Service] btn
        public ServiceUnitPointDetailPage ClickAnySelect(string selectLocator)
        {
            ClickOnElement(selectLocator);
            return new ServiceUnitPointDetailPage();
        }
    }
}
