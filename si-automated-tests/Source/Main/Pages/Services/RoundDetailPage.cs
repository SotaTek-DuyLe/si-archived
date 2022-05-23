using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundDetailPage : BasePage
    {
        private readonly By roundInput = By.XPath("//div[@id='details-tab']//input[@name='round']");
        private readonly By roundTypeSelect = By.XPath("//div[@id='details-tab']//select[@id='roundType.id']");
        private readonly By dispatchSiteSelect = By.XPath("//div[@id='details-tab']//select[@id='dispatchSite.id']");
        private readonly By shiftSelect = By.XPath("//div[@id='details-tab']//select[@id='shift.id']");
        private const string AllTabDisplayed = "//li[@role='presentation' and not(contains(@style, 'visibility: collapse'))]/a";
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";

        public RoundDetailPage VerifyRoundInput(string expectedValue)
        {
            Assert.IsTrue(GetElement(roundInput).GetAttribute("value") == expectedValue);
            return this;
        }

        public RoundDetailPage VerifyRoundType(string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(roundTypeSelect) == expectedValue);
            return this;
        }

        public RoundDetailPage VerifyDispatchSite(string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(dispatchSiteSelect) == expectedValue);
            return this;
        }

        public RoundDetailPage VerifyShift(string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(shiftSelect) == expectedValue);
            return this;
        }

        public RoundDetailPage ClickAllTabAndVerify()
        {
            List<IWebElement> allElements = GetAllElements(AllTabDisplayed);
            int clickButtonIdx = 1;
            while (clickButtonIdx < allElements.Count)
            {
                ClickOnElement(allElements[clickButtonIdx]);
                clickButtonIdx++;
                WaitForLoadingIconToDisappear();
                Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
            }
            return this;
        }
    }
}
