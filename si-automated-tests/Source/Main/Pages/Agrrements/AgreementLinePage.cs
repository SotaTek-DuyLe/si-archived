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
        private readonly By title = By.XPath("//h4[text()='AGREEMENTLINE']");
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");

        private const string frameMessage = "//div[@class='notifyjs-corner']/div";

        //DYNAMIC LOCATOR
        private const string titleContainsId = "//p[text()='Agreement ID {0}']";
        private const string anyTab = "//a[text()='{0}']";

        public AgreementLinePage GoToAllTabAndConfirmNoError()
        {
            IList<IWebElement> elements = WaitUtil.WaitForAllElementsVisible(allTabs);
            foreach (IWebElement element in elements)
            {
                Thread.Sleep(1500);
                element.Click();
                WaitForLoadingIconToDisappear();
                Assert.IsFalse(IsControlDisplayedNotThrowEx(frameMessage));
            }
            return this;
        }
        public AgreementLinePage CloseWithoutSaving()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }

        public AgreementLinePage WaitForWindowLoadedSuccess(string id)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(string.Format(titleContainsId, id));
            return this;
        }

        public AgreementLinePage ClickDetailTab()
        {
            ClickOnElement(string.Format(anyTab, "Details"));
            return this;
        }
    }
}
