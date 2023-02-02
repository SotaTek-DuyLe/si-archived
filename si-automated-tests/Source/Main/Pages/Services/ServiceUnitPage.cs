using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using System.Collections.Generic;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//span[text()='Service Unit']");
        public readonly By UnitIframe = By.XPath("//div[@id='iframe-container']//iframe");
        private readonly string ServiceUnitTable = "//div[@class='echo-grid']//div[@class='grid-canvas']";
        private readonly string ServiceUnitRow = "./div[contains(@class, 'slick-row')]";
        private readonly string IdCell = "./div[contains(@class, 'l1 r1')]";
        private readonly string NameCell = "./div[contains(@class, 'l2 r2')]";
        private readonly By applyFilterBtn = By.XPath("//button[@title='Apply Filters']");
        private readonly By retireBtn = By.XPath("button[title='Retire']");
        private readonly By firstServiceUnitRow = By.XPath("//div[@class='grid-canvas']/div[1]");

        public TableElement ServiceUnitTableEle
        {
            get => new TableElement(ServiceUnitTable, ServiceUnitRow, new List<string>() { IdCell, NameCell });
        }

        [AllureStep]
        public ServiceUnitPage IsServiceUnitPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage DoubleClickServiceUnit()
        {
            ServiceUnitTableEle.DoubleClickRow(0);
            return this;
        }
        [AllureStep]
        public ServiceUnitPage DoubleClickServiceUnitById(string id)
        {
            var row = ServiceUnitTableEle.GetRowByCellValue(0, id);
            DoubleClickOnElement(row);
            return this;
        }
        [AllureStep]
        public ServiceUnitPage FindServiceUnitWithId(string serviceUnitId)
        {
            SendKeys(By.XPath("//div[contains(@class, 'slick-headerrow-column l1 r1')]//input"), serviceUnitId);
            SleepTimeInSeconds(1);
            ClickOnElement(applyFilterBtn);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(firstServiceUnitRow);
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Service Unit?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public ServiceUnitPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectServiceUnit)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ServiceUnitPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public ServiceUnitPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }

    }
}
