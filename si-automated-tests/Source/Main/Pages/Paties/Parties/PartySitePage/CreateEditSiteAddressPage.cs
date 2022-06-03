using System;
using System.Collections.Generic;
using si_automated_tests.Source.Core;
using OpenQA.Selenium;
using NUnit.Framework;

namespace si_automated_tests.Source.Main.Pages.PartySitePage
{
    public class CreateEditSiteAddressPage : BasePage
    {
        private readonly By CreateSiteAddressTitle = By.XPath("//h1[text()='Create/Edit Site Address']");
        private readonly By CancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By NextBtn = By.XPath("//button[text()='Next']");
        private readonly By SearchInput = By.Id("search");
        private readonly By SearchBtn = By.XPath("//button[@type='submit']");
        private readonly string btnNamed = "//div[@style='display: block;']//button[contains(text(),'{0}')]";

        //First screen
        private readonly By searchResultScreen1 = By.XPath("//div[@id='screen1']//div[@data-bind='foreach: geocodedAddresses']/div");

        //Second Screen
        private readonly By searchResultScreen2 = By.XPath("//div[@id='screen2']//div[@data-bind='foreach: existingAddresses']/div");

        private readonly By SiteNameInput = By.XPath("//label[text()='Site Name']/following-sibling::input");
        private readonly By SelectAddressNextBtn = By.XPath("//button[text()='Next' and contains(@data-bind,'selectedExistingAddress')]");
        private readonly By CreateBtn = By.XPath("//button[text()='Create']");
        private readonly By siteDd = By.CssSelector("select#site-type");

        private readonly By ErrorMessageDublicateSite = By.XPath("//*[contains(.,'An active Site already exists at this address for this Party! You cannot create a duplicate.')]");

        //DYNAMIC LOCATOR
        private const string AnySite = "//select[@id = 'site-type']/option[text()='{0}']";

        public CreateEditSiteAddressPage IsOnCreateEditSiteAddressPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(CreateSiteAddressTitle);
            Assert.IsTrue(IsControlDisplayed(CreateSiteAddressTitle));
            Assert.IsTrue(IsControlDisplayed(CancelBtn));
            Assert.IsTrue(IsControlDisplayed(NextBtn));
            return this;
        }

        public CreateEditSiteAddressPage VerifyCreateSiteAddressPageClosed()
        {
            Assert.False(IsControlDisplayed(CreateSiteAddressTitle));
            return this;
        }

        public string SelectRandomSiteAddress()
        {
            WaitUtil.WaitForElementVisible("//div[contains(@data-bind, 'existingAddresses')]/div[1]");
            List<IWebElement> AllSiteAddressRows = GetAllElements("//div[contains(@data-bind, 'existingAddresses')]/div");
            Random rnd = new Random();
            int num = rnd.Next(0, AllSiteAddressRows.Count - 1);
            IWebElement site = AllSiteAddressRows[num];
            ClickOnElement(site);
            return GetElementText(site);
        }

        public CreateEditSiteAddressPage SelectSiteAddress(string address)
        {
            WaitUtil.WaitForElementVisible("//div[contains(@data-bind, 'existingAddresses')]/div[1]");
            List<IWebElement> AllSiteAddressRows = GetAllElements("//div[contains(@data-bind, 'existingAddresses')]/div");
            for (int i = 0; i < AllSiteAddressRows.Count; i++)
            {
                if (GetElementText(AllSiteAddressRows[i]) == address)
                {
                    ClickOnElement(AllSiteAddressRows[i]);
                    break;
                }
            }
            return this;
        }

        public CreateEditSiteAddressPage ClickNextBtn()
        {
            ScrollDownToElement(NextBtn);
            ClickOnElement(NextBtn);
            return this;
        }

        public CreateEditSiteAddressPage SelectAddressClickNextBtn()
        {
            ScrollDownToElement(SelectAddressNextBtn);
            ClickOnElement(SelectAddressNextBtn);
            return this;
        }

        public CreateEditSiteAddressPage InsertSiteName(string name)
        {
            SendKeys(SiteNameInput, name);
            return this;
        }

        public CreateEditSiteAddressPage VerifySiteNameValue(string name)
        {
            WaitUtil.WaitForElementVisible(SiteNameInput);
            Assert.AreEqual(GetElementText(SiteNameInput), name);
            return this;
        }

        public CreateEditSiteAddressPage ClickCreateBtn()
        {
            ClickOnElement(CreateBtn);
            return this;
        }

        public CreateEditSiteAddressPage VerifyDuplicateErrorMessage()
        {
            WaitUtil.WaitForElementVisible(ErrorMessageDublicateSite);
            Assert.IsTrue(IsControlDisplayed(ErrorMessageDublicateSite));
            return this;
        }

        public CreateEditSiteAddressPage ClickAnySiteInDd(string site)
        {
            ClickOnElement(siteDd);
            ClickOnElement(string.Format(AnySite, site));
            //verify after selected
            Assert.AreEqual(GetFirstSelectedItemInDropdown(siteDd), site);
            return this;
        }
        public CreateEditSiteAddressPage SearchForSite(string siteName)
        {
            SendKeys(SearchInput, siteName);
            ClickOnElement(SearchBtn);
            return this;
        }
        public CreateEditSiteAddressPage SelectResultInScreen1(string value)
        {
            IList<IWebElement> list = WaitUtil.WaitForAllElementsVisible(searchResultScreen1);
            foreach (var result in list)
            {
                if (GetElementText(result).Contains(value))
                {
                    ClickOnElement(result);
                    break;
                }
            }
            ClickOnElement(btnNamed, "Next");
            return this;
        }
        public CreateEditSiteAddressPage SelectResultInScreen2(string value)
        {
            IList<IWebElement> list = WaitUtil.WaitForAllElementsVisible(searchResultScreen2);
            foreach (var result in list)
            {
                if (GetElementText(result).Contains(value))
                {
                    ClickOnElement(result);
                    break;
                }
            }
            ClickOnElement(btnNamed, "Next");
            return this;
        }
    }
 }
