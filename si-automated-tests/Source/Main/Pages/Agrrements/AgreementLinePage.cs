using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements
{
    public class AgreementLinePage : BasePage
    {
        private readonly By allTabs = By.XPath("//a[@role='tab']");
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");

        private const string frameMessage = "//div[@class='notifyjs-corner']/div";
        public AgreementLinePage GoToAllTabAndConFirmNoError()
        {
            IList<IWebElement> elements = WaitUtil.WaitForAllElementsVisible(allTabs);
            foreach (IWebElement element in elements)
            {
                Thread.Sleep(1000);
                element.Click();
                WaitForLoadingIconToDisappear();
                Assert.IsFalse(IsControlDisplayedNotThrowEx(frameMessage));
            }
            return this;
        }
        public AgreementLinePage CloseWithoutSaving()
        {
            WaitUtil.WaitForElementClickable(closeWithoutSavingBtn);
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }
    }
}
