using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class FindServiceUnitDetailPage : BasePage
    {
        private readonly By closeNotSavingBtn = By.XPath("//button[@title='Close Without Saving']");
        private readonly By serviceUnitLabel = By.XPath("//label[contains(string(), 'Service Unit')]");
        private readonly By serviceUnitInput = By.XPath("//label[contains(string(), 'Service Unit')]/following-sibling::input");
        private readonly By findBtn = By.CssSelector("button[title='Find']");

        //DYNAMIC
        private const string columnInGrid = "//tr/th[text()='{0}']";

        public FindServiceUnitDetailPage IsFindServiceUnitPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(serviceUnitLabel);
            Assert.IsTrue(IsControlDisplayed(closeNotSavingBtn));
            Assert.IsTrue(IsControlDisplayed(serviceUnitLabel));
            Assert.IsTrue(IsControlDisplayed(serviceUnitInput));
            Assert.IsTrue(IsControlDisplayed(findBtn));
            foreach(string column in CommonConstants.ColumnInFindServiceUnitWindow)
            {
                Assert.IsTrue(IsControlDisplayed(columnInGrid, column));
            }
            return this;
        }
    }
}
