using System;
using NUnit.Allure.Attributes;
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
        private readonly string firstCheckboxInFirstRow = "(//div[@class='grid-canvas']//div[text()='{0}']/preceding-sibling::div/input)[1]";
        private readonly string firstRow = "(//div[@class='grid-canvas']//div[text()='{0}']/parent::div)[1]";

        private string RiskRegisterTable = "//div[@class='grid-canvas']";
        private string RiskRegisterRow = "./div[contains(@class, 'slick-row')]";
        private string RiskCheckboxCell = "./div[contains(@class, 'l0')]";
        private string RiskIdCell = "./div[contains(@class, 'l1')]";
        public TableElement RiskRegisterTableEle
        {
            get => new TableElement(RiskRegisterTable, RiskRegisterRow, new System.Collections.Generic.List<string>() { RiskCheckboxCell, RiskIdCell });
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
