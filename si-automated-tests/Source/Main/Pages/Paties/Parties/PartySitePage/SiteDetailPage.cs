using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage
{
    public class SiteDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Serviced Site']");
        private readonly By siteName = By.XPath("//p[text()='Jaflong Tandoori / 16 ASHBURNHAM ROAD, HAM, RICHMOND, TW10 7NF']");
        private readonly By primaryContactDd = By.CssSelector("select#primary-contact");
        private readonly By primaryContactAddBtn = By.XPath("//select[@id='primary-contact']/following-sibling::span");
        private const string loadingData = "//div[@class='loading-data']";
        private const string frameMessage = "//div[@class='notifyjs-corner']/div";

        //DYNAMIC LOCATOR
        private const string allPrimaryContactValue = "//select[@id='primary-contact']/option";
        private const string primaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string primaryContactDisplayed = "//div[@data-bind='with: primaryContact']//span[text()='{0}']";
        private const string titleDetail = "//p[text()='{0}']";
        private const string nameDetail = "//h4[text()='{0}']";
        private const string siteNameDynamic = "//span[text()='{0}']";
        private const string allTabInSite = "//ul[@role='tablist']//a[text()='{0}']";
        private const string messageAtMapTab = "//div[@class='notifyjs-corner']//div[text()='{0}']";

        public SiteDetailPage WaitForSiteDetailPageLoaded()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(siteName);
            return this;
        }

        public SiteDetailPage WaitForSiteDetailPageLoaded(string titleA, string agreementNameA)
        {
            WaitUtil.WaitForElementVisible(string.Format(titleDetail, titleA));
            WaitUtil.WaitForElementVisible(string.Format(nameDetail, agreementNameA));
            return this;
        }

        public SiteDetailPage WaitForSiteDetailsLoaded(string titleA, string siteNameDisplayed)
        {
            WaitUtil.WaitForElementVisible(string.Format(siteNameDynamic, titleA));
            WaitUtil.WaitForElementVisible(string.Format(titleDetail, siteNameDisplayed));
            return this;
        }


        public SiteDetailPage ClickPrimaryContactDd()
        {
            ClickOnElement(primaryContactDd);
            return this;
        }

        public SiteDetailPage VerifyNumberOfContact(int numberOfContact)
        {
            Assert.AreEqual(numberOfContact, GetAllElements(allPrimaryContactValue).Count);
            return this;
        }

        public SiteDetailPage VerifyValueInPrimaryContactDd(string[] expectedOption)
        {
            foreach (String option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(primaryContactValue, option)));
            }
            return this;
        }

        public SiteDetailPage SelectAnyPrimaryContactAndVerify(ContactModel contactModel)
        {
            ClickOnElement(string.Format(primaryContactValue, contactModel.FirstName + " " + contactModel.LastName));
            //Verify
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Telephone));
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Mobile));
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Email));
            //Verify contact saved in primary contact dd
            Assert.AreEqual(GetFirstSelectedItemInDropdown(primaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }

        public AddPrimaryContactPage ClickPrimaryContactAddBtn()
        {
            ClickOnElement(primaryContactAddBtn);
            return PageFactoryManager.Get<AddPrimaryContactPage>();
        }

        public SiteDetailPage VerifyFirstValueInPrimaryContactDd(ContactModel contactModel)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(primaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }

        public SiteDetailPage VerifyDisplayAllTab(string[] expectedAllTab)
        {
            foreach(string tab in expectedAllTab)
            {
                Assert.IsTrue(IsControlDisplayed(allTabInSite, tab));
            }
            return this;
        }

        public SiteDetailPage ClickSomeTabAndVerifyNoErrorMessage(string[] expectedAllTab)
        {
            foreach(string tab in expectedAllTab)
            {
                ClickOnElement(allTabInSite, tab);
                WaitUtil.WaitForElementInvisible(loadingData);
                Assert.IsFalse(IsControlDisplayedNotThrowEx(frameMessage));
            }
            return this;
        }

        public SiteDetailPage ClickMapTabAndVerifyMessage(string message)
        {
            ClickOnElement(allTabInSite, CommonConstants.AllSiteTab[3]);
            Assert.IsTrue(IsControlDisplayed(string.Format(messageAtMapTab, message)));
            return this;
        }

    }
}
