using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Sites
{
    public class SitePage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//span[text()='Serviced Site']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By LockCheckbox = By.XPath("//input[@id='locked']");
        public readonly By LockReferenceInput = By.XPath("//input[@id='lockReference']");
        public readonly By LockHelpButton = By.XPath("//span[contains(@class, 'lock-help')]");
        public readonly By LockHelpContent = By.XPath("//div[contains(@class, 'popover-content')]");
        public readonly By SiteAbvInput = By.XPath("//input[@id='site-abv']");
        public readonly By SiteAddressTitle = By.XPath("//p[@class='object-name']");
        private readonly By accountingRefInput = By.CssSelector("input[id='account-reference']");

        [AllureStep]
        public SitePage IsSiteDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        [AllureStep]
        public SitePage ClickOnDetailTab()
        {
            ClickOnElement(DetailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public SitePage InputAccountingRef(string accountingRefValue)
        {
            SendKeys(accountingRefInput, accountingRefValue);
            return this;
        }

        [AllureStep]
        public SitePage RemoveAccountingRef()
        {
            ClearInputValue(accountingRefInput);
            return this;
        }

        [AllureStep]
        public SitePage VerifyAccountingRefAfterSaving(string accountingRefValue)
        {
            Assert.AreEqual(accountingRefValue, GetAttributeValue(accountingRefInput, "value"));
            return this;
        }
    }
}
