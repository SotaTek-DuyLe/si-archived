﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage
{
    public class SiteDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Serviced Site']");
        private readonly By siteName = By.XPath("//p[text()='Jaflong Tandoori / 16 ASHBURNHAM ROAD, HAM, RICHMOND, TW10 7NF']");
        private readonly By primaryContactDd = By.CssSelector("select#primary-contact");
        private readonly By primaryContactAddBtn = By.XPath("//select[@id='primary-contact']/following-sibling::span");

        //DYNAMIC LOCATOR
        private const string allPrimaryContactValue = "//select[@id='primary-contact']/option";
        private const string primaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string primaryContactDisplayed = "//div[@data-bind='with: primaryContact']//span[text()='{0}']";
        private const string titleDetail = "//p[text()='{0}']";
        private const string nameDetail = "//h4[text()='{0}']";

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

        public SiteDetailPage ClickPrimaryContactAddBtn()
        {
            ClickOnElement(primaryContactAddBtn);
            return this;
        }
    }
}
