using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class ResolutionCodeGrid : BasePage
    {

        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By deleteItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By mainColumns = By.XPath("//div[contains(@class,'slick-header-column') and not(@unselectable='on')]");
        private readonly By resolutionCodes = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]");
        private readonly By resolutionCodeNames = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]/div[contains(@class,'l2 r2')]");

        [AllureStep]
        public ResolutionCodeGrid IsOnResolutionCodeGrid()
        {
            WaitUtil.WaitForElementVisible(deleteItemBtn);
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            WaitUtil.WaitForAllElementsVisible(mainColumns);
            return this;
        }

        [AllureStep]
        public ResolutionCodeGrid DoubleClickFirstResolutionCode()
        {
            var firstResolutionCode = WaitUtil.WaitForAllElementsVisible(resolutionCodes)[0];
            ClickOnElement(firstResolutionCode);
            return this;
        }
        [AllureStep]
        public ResolutionCodeGrid ClickAddNewItem()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }
        public ResolutionCodeGrid VerifyResolutionCodeIsCreated(string name)
        {
            var firstResoCodeName = WaitUtil.WaitForAllElementsVisible(resolutionCodeNames)[0];
            Assert.AreEqual(name, firstResoCodeName.Text);
            return this;
        }
    }
}
