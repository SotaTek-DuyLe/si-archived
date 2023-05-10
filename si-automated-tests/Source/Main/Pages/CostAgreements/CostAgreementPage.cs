using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.CostAgreements
{
    public class CostAgreementPage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By allCostAgreementsRow = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By firstCostAgreementsRow = By.XPath("//div[@class='grid-canvas']/div[1]");

        [AllureStep]
        public CostAgreementPage IsCostAgreementPage()
        {
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            WaitUtil.WaitForAllElementsVisible(allCostAgreementsRow);
            return this;
        }

    }
}
