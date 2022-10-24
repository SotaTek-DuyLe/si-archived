using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Search.PointAreas
{
    public class PointAreaListingPage : BasePage
    {
        private readonly By addNewPointAreasBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstPointSegementRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");

        [AllureStep]
        public PointAreaListingPage WaitForPointAreaListingPageDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(addNewPointAreasBtn);
            return this;
        }
        [AllureStep]
        public PointAreaListingPage FilterAreaById(string id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id);
            ClickOnElement(applyBtn);
            return this;
        }
        [AllureStep]
        public PointAreaDetailPage DoubleClickFirstPointAreaRow()
        {
            DoubleClickOnElement(firstPointSegementRow);
            return PageFactoryManager.Get<PointAreaDetailPage>();
        }


    }
}
