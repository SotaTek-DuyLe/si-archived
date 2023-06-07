using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class BusinessUnitListPage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By allRecords = By.XPath("//div[@class='grid-canvas']/div");

        [AllureStep]
        public BusinessUnitListPage IsBusinessUnitListPage()
        {
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            WaitUtil.WaitForAllElementsVisible(allRecords);
            return this;
        }

        [AllureStep]
        public BusinessUnitListPage ClickOnAddNewItemBtn()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }

    }
}
