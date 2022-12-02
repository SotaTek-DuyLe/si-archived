using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class ReceiptListPage : BasePage
    {
        private readonly By AddNewItem = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[@class='grid-canvas']/div[1]");

        [AllureStep]
        public ReceiptListPage FilterReceiptById(string id)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(AddNewItem);
            SendKeys(filterInputById, id);
            SendKeys(filterInputById, Keys.Enter);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ReceiptListPage ClickOnApplyBtn()
        {
            ClickOnElement(applyBtn);
            return this;
        }

        [AllureStep]
        public SalesReceiptPage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<SalesReceiptPage>();
        }
    }
}
