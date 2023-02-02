using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Paties;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class PriceLineDetailPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Price Line']");
        private readonly By id = By.CssSelector("h4[title='Id']");
        private readonly By markForCreditBtn = By.XPath("//button[text()='Mark For Credit']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By unmarkFromCreditBtn = By.XPath("//button[text()='Unmark From Credit']");
        private readonly By partyHyperLink = By.CssSelector("h5[title='Open Party']");

        [AllureStep]
        public PriceLineDetailPage IsPriceLineDetailPage(string idValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.AreEqual(idValue, GetElementText(id));
            Assert.IsTrue(IsControlDisplayed(detailTab));
            return this;
        }
        [AllureStep]
        public PriceLineDetailPage ClickOnMarkForCreditBtn()
        {
            ClickOnElement(markForCreditBtn);
            return this;
        }
        [AllureStep]
        public PriceLineDetailPage VerifyDisplayUnMarkFromCreditBtn()
        {
            Assert.IsTrue(IsControlEnabled(unmarkFromCreditBtn));
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnPartyHyperlinkText()
        {
            ClickOnElement(partyHyperLink);
            return PageFactoryManager.Get<DetailPartyPage>();
        }
        [AllureStep]
        public string GetPartyName()
        {
            return GetElementText(partyHyperLink);
        }
    }


}
