using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Applications.RiskRegister
{
    public class RiskRegisterListingPage : BasePageCommonActions
    {
        public readonly By bulkUpdateBtn = By.CssSelector("button[title='Bulk Create']");
        public readonly By retireBtn = By.CssSelector("button[title='Retire']");
        public readonly By ShowAllBtn = By.XPath("//button[@title='Show All']");
        public readonly By ToggleFilterBtn = By.XPath("//button[@title='Toggle Filter Row']");
        public readonly By SaveGridLayoutBtn = By.XPath("//button[@title='Save Grid Layout']");
        public readonly By ShowInformationBtn = By.XPath("//button[@title='Show Information']");
        public readonly By PopoutBtn = By.XPath("//button[@title='Pop out']");
        public readonly By StartDateHeaderInput = By.XPath("//div[contains(@class, 'slick-headerrow-column l9 r9')]//input");
        public readonly By ToggleFilterStartDateHeader = By.XPath("//div[contains(@class, 'slick-headerrow-column l9 r9')]//button");
        public readonly By SelectFilterDropdown = By.XPath("//ul[@aria-expanded='true']");
        private readonly By idFilter = By.XPath("//div[contains(@class, 'l1 r1')]//input");
        private readonly string firstCheckboxInFirstRow = "(//div[@class='grid-canvas']//div[text()='{0}']/preceding-sibling::div/input)[1]";
        private readonly string firstRow = "(//div[@class='grid-canvas']//div[text()='{0}']/parent::div)[1]";
        private readonly By loadingIconFrameTab = By.XPath("//div[@id='form']/following-sibling::div[@data-bind='shield: loading']");

        private string HeaderNameXPath = "//div[@id='risk-grid']//div[contains(@class, 'slick-header-column')]//span[@class='slick-column-name' and text()='{0}']";
        private string RiskRegisterTable = "//div[@class='grid-canvas']";
        private string RiskRegisterRow = "./div[contains(@class, 'slick-row')]";
        private string RiskCheckboxCell = "./div[contains(@class, 'l0')]";
        private string RiskIdCell = "./div[contains(@class, 'l1')]";
        public TableElement RiskRegisterTableEle
        {
            get => new TableElement(RiskRegisterTable, RiskRegisterRow, new System.Collections.Generic.List<string>() { RiskCheckboxCell, RiskIdCell });
        }
        #region Retire Modal
        public readonly By RetireTitle = By.XPath("//div[@role='dialog']//div[@data-bind='text: getRetiringMode().message']");
        public readonly By OKButton = By.XPath("//div[@role='dialog']//button[text()='OK']");
        #endregion

        [AllureStep]
        public RiskRegisterListingPage ClickAtFirstRisk()
        {
            RiskRegisterTableEle.ClickCell(0, 0);
            SleepTimeInMiliseconds(1000);
            return this;
        }

        [AllureStep]
        public RiskRegisterListingPage VerifyActionButtonsVisible()
        {
            VerifyElementVisibility(bulkUpdateBtn, true);
            VerifyElementVisibility(retireBtn, true);
            VerifyElementVisibility(ShowAllBtn, true);
            VerifyElementVisibility(ToggleFilterBtn, true);
            VerifyElementVisibility(SaveGridLayoutBtn, true);
            VerifyElementVisibility(ShowInformationBtn, true);
            VerifyElementVisibility(PopoutBtn, true);
            return this;
        }

        [AllureStep]
        public RiskRegisterListingPage VerifyHeadersVisible(List<string> headerNames)
        {
            VerifyElementVisibility(By.XPath("//div[@id='risk-grid']//div[contains(@class, 'slick-header-column')]//span[@class='slick-column-name']//input"), true);
            foreach (var item in headerNames)
            {
                VerifyElementVisibility(By.XPath(string.Format(HeaderNameXPath, item)), true);
            }
            return this;
        }

        [AllureStep]
        public RiskRegisterListingPage ClickOnHeader(string hearName)
        {
            ClickOnElement(By.XPath(string.Format(HeaderNameXPath, hearName)));
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RiskRegisterListingPage IsRiskStreetForm()
        {
            WaitUtil.WaitForElementVisible(bulkUpdateBtn);
            Assert.IsTrue(IsControlDisplayed(bulkUpdateBtn));
            Assert.IsTrue(IsControlDisplayed(retireBtn));
            return this;
        }
        [AllureStep]
        public RiskRegisterListingPage FilterByRiskId(string riskId)
        {
            SendKeys(idFilter, riskId);
            WaitUtil.WaitForElementVisible(loadingIconFrameTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public RiskDetailPage DoubleClickAtFirstRisk()
        {
            RiskRegisterTableEle.ClickCell(0, 0);
            SleepTimeInMiliseconds(1000);
            RiskRegisterTableEle.DoubleClickRow(0);
            return PageFactoryManager.Get<RiskDetailPage>();
        }

        [AllureStep]
        public RiskDetailPage DoubleClickAtFirstRisk(string riskIdValue)
        {
            ClickOnElement(firstCheckboxInFirstRow, riskIdValue);
            DoubleClickOnElement(string.Format(firstRow, riskIdValue));
            return PageFactoryManager.Get<RiskDetailPage>();
        }
    }
}
