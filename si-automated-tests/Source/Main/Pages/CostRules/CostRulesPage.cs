using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.CostRules
{
    public class CostRulesPage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By allCostRuleRow = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By firstCostRuleRow = By.XPath("//div[@class='grid-canvas']/div[1]");

        [AllureStep]
        public CostRulesPage IsCostRuleGridPage()
        {
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            WaitUtil.WaitForAllElementsVisible(allCostRuleRow);
            return this;
        }

        [AllureStep]
        public CostRuleDetailPage ClickOnFirstCostRuleRow()
        {
            DoubleClickOnElement(firstCostRuleRow);
            return PageFactoryManager.Get<CostRuleDetailPage>();
        }
    }
}
