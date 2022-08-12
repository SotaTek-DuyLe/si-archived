using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Applications.RiskRegister
{
    public class RiskRegisterListingPage : BasePage
    {
        private readonly By bulkUpdateBtn = By.CssSelector("button[title='Bulk Create']");
        private readonly By retireBtn = By.CssSelector("button[title='Retire']");
        private readonly By idFilter = By.XPath("//div[contains(@class, 'l1 r1')]//input");

        private string RiskRegisterTable = "//div[@class='grid-canvas']";
        private string RiskRegisterRow = "./div[contains(@class, 'slick-row')]";
        private string RiskCheckboxCell = "./div[contains(@class, 'l0')]";
        private string RiskIdCell = "./div[contains(@class, 'l1')]";
        public TableElement RiskRegisterTableEle
        {
            get => new TableElement(RiskRegisterTable, RiskRegisterRow, new System.Collections.Generic.List<string>() { RiskCheckboxCell, RiskIdCell });
        }

        public RiskRegisterListingPage IsRiskStreetForm()
        {
            WaitUtil.WaitForElementVisible(bulkUpdateBtn);
            Assert.IsTrue(IsControlDisplayed(bulkUpdateBtn));
            Assert.IsTrue(IsControlDisplayed(retireBtn));
            return this;
        }

        public RiskRegisterListingPage FilterByRiskId(string riskId)
        {
            SendKeys(idFilter, riskId);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public RiskDetailPage DoubleClickAtFirstRisk()
        {
            RiskRegisterTableEle.ClickCell(0, 0);
            SleepTimeInMiliseconds(1000);
            RiskRegisterTableEle.DoubleClickRow(0);
            return PageFactoryManager.Get<RiskDetailPage>();
        }
    }
}
