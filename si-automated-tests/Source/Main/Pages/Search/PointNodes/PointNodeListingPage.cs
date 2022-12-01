using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Search.PointNodes
{
    public class PointNodeListingPage : BasePage
    {
        private readonly By addNewPointNodeBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstPointNodeRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");

        [AllureStep]
        public PointNodeListingPage WaitForPointNodeListingPageDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(addNewPointNodeBtn);
            return this;
        }
        [AllureStep]
        public PointNodeListingPage FilterNodeById(string id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id);
            ClickOnElement(applyBtn);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage DoubleClickFirstPointNodeRow()
        {
            DoubleClickOnElement(firstPointNodeRow);
            return PageFactoryManager.Get<PointNodeDetailPage>();
        }
    }
}
