using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class DetailPartyPage : BasePage
    {
        private const string AllTabDisplayed = "//li[@role='presentation' and not(contains(@style, 'visibility: collapse'))]/a";
        private const string AllTabInDropdown = "//ul[@class='dropdown-menu']//a";
        private const string DropdownBtn = "//li[contains(@class, 'dropdown')]/a[contains(@class, 'dropdown-toggle')]";
        private const string SuccessfullyToastMessage = "//div[@class='notifyjs-corner']//div[text()='Successfully saved party.']";
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";
        private const string LoadingData = "//div[@class='loading-data']";

        private const string PartyName = "//div[text()='{0}']";

        //DETAIL TAB LOCATOR
        private const string InvoiceAddressAddBtn = "//label[text()='Invoice Address']/following-sibling::div//span[text()='Add']";
        private const string PartyNameInput = "//label[text()='Party Name']/following-sibling::div/input";
        private const string ContractInput = "//label[text()='Contract']/following-sibling::div/input";
        private readonly By CorresspondenceAddressDd = By.Id("party-correspondence-address");
        private const string SitesTab = "//a[text()='Sites']";
        private const string SaveBtn = "//button[@title='Save']";
        private const string InvoiceAddress = "//label[text()='Invoice Address']/following-sibling::div//select";
        private const string DetailsTab = "//a[text()='Details']";

        //DETAIL TAB DYNAMIC LOCATOR
        private const string PrimaryContact = "//label[text()='Primary Contact']/following-sibling::div//select/option[text()={0}]";
        private const string InvoiceAddressValue = "//label[text()='Invoice Address']/following-sibling::div//option[text()='{0}']";
        private const string CorresspondenceValue = "//label[text()='Correspondence Address']/following-sibling::div//select/option[text()='{0}']";

        //SITE TAB LOCATOR
        private const string TotalSiteRow = "//div[@class='grid-canvas']/div";
        private const string IdColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='ID']/parent::div/preceding-sibling::div) + 1]";
        private const string NameColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='Name']/parent::div/preceding-sibling::div) + 1]";
        private const string AddressColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='Address']/parent::div) + 1]";
        private const string SiteTypeColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='Site Type']/parent::div/preceding-sibling::div) + 1]";
        private const string StartDateColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='Start Date']/parent::div/preceding-sibling::div) + 1]";
        private const string EndDateColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='End Date']/parent::div/preceding-sibling::div) + 1]";
        private const string ClientReferenceColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='Client Reference']/parent::div/preceding-sibling::div) + 1]";
        private const string AccountingReferenceColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='Accounting Reference']/parent::div/preceding-sibling::div) + 1]";
        private const string AbvColumn = "//div[@class='grid-canvas']/div/div[count(//span[text()='Abv']/parent::div/preceding-sibling::div) + 1]";

        //TAB
        public List<string> GetAllTabDisplayed()
        {
            List<string> allTabs = new List<string>();
            List<IWebElement> allElements = GetAllElements(AllTabDisplayed);
            for(int i = 0; i < allElements.Count; i++)
            {
                allTabs.Add(GetElementText(allElements[i]));
            }
            return allTabs;
        }
        public List<string> GetAllTabInDropdown()
        {
            
            List<string> allTabs = new List<string>();
            if (!IsControlDisplayedNotThrowEx(DropdownBtn))
            {
                return allTabs;
            }
            List<IWebElement> allElements = GetAllElements(AllTabInDropdown);
            for (int i = 0; i < allElements.Count; i++)
            {
                allTabs.Add(GetElementText(allElements[i]));
            }
            return allTabs;
        }
        public DetailPartyPage MergeAllTabInDetailPartyAndVerify()
        {
            WaitUtil.WaitForElementVisible(AllTabDisplayed);
            List<string> allTabDisplayed = GetAllTabDisplayed();
            List<string> allTabInDropdown = GetAllTabInDropdown();
            allTabDisplayed.AddRange(allTabInDropdown);
            Assert.AreEqual(allTabDisplayed, PartyTabConstant.AllTabInDetailParty.ToList());
            return this;
        }
        public DetailPartyPage ClickAllTabAndVerify()
        {
            List<IWebElement> allElements = GetAllElements(AllTabDisplayed);
            int clickButtonIdx = 0;
            while (clickButtonIdx < allElements.Count)
            {
                ClickOnElement(allElements[clickButtonIdx]);
                clickButtonIdx++;
                WaitUtil.WaitForElementInvisible(LoadingData);
                Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
                allElements = GetAllElements(AllTabDisplayed);
            }
            return this;
        }
        public DetailPartyPage ClickAllTabInDropdownAndVerify()
        {
            if (IsControlDisplayedNotThrowEx(DropdownBtn))
            {
                List<IWebElement> allElements = GetAllElements(AllTabInDropdown);
                int clickButtonIdx = 0;
                while (clickButtonIdx < allElements.Count)
                {
                    ClickOnElement(DropdownBtn);
                    Thread.Sleep(500);
                    ClickOnElement(allElements[clickButtonIdx]);
                    clickButtonIdx++;
                    WaitUtil.WaitForElementInvisible(LoadingData);
                    Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
                    allElements = GetAllElements(AllTabInDropdown);
                }
            }
            return this;
        }
        public DetailPartyPage VerifyDisplaySuccessfullyMessage()
        {
            Assert.IsTrue(IsControlDisplayed(SuccessfullyToastMessage));
            WaitUtil.WaitForElementInvisible(SuccessfullyToastMessage);
            return this;
        }

        public DetailPartyPage ClickOnParty(string name)
        {
            ClickOnElement(PartyName, name);
            return this;
        }

        //DETAIL TAB
        public DetailPartyPage ClickOnAddInvoiceAddressBtn()
        {
            ClickOnElement(InvoiceAddressAddBtn);
            return this;
        }

        public DetailPartyPage VerifyCreatedSiteAddressAppearAtAddress(AddressDetailModel addressDetail)
        {
            Console.WriteLine(addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(InvoiceAddress), addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            return this;
        }

        public DetailPartyPage ClickCorresspondenAddress()
        {
            ClickOnElement(CorresspondenceAddressDd);
            return this;
        }

        public DetailPartyPage VerifyDisplayNewSiteAddress(AddressDetailModel addressDetail)
        {
            Assert.IsTrue(IsControlDisplayed(CorresspondenceValue, addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode));
            return this;
        }

        public DetailPartyPage SelectCorresspondenAddress(AddressDetailModel addressDetail)
        {
            ClickOnElement(CorresspondenceValue, addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            return this;
        }

        public DetailPartyPage ClickOnSitesTab()
        {
            ClickOnElement(SitesTab);
            return this;
        }

        public DetailPartyPage WaitForLoadingIconInvisiable()
        {
            WaitUtil.WaitForElementInvisible(LoadingData);
            return this;
        }

        public List<SiteModel> GetAllSiteInList()
        {
            List<SiteModel> siteModels = new List<SiteModel>();
            List<IWebElement> totalRow = GetAllElements(TotalSiteRow);
            List<IWebElement> allIdSite = GetAllElements(IdColumn);
            List<IWebElement> allIdName = GetAllElements(NameColumn);
            List<IWebElement> allAddress = GetAllElements(AddressColumn);
            List<IWebElement> allSiteType = GetAllElements(SiteTypeColumn);
            List<IWebElement> allStartDate = GetAllElements(StartDateColumn);
            List<IWebElement> allEndDate = GetAllElements(EndDateColumn);
            List<IWebElement> allClientReference = GetAllElements(ClientReferenceColumn);
            List<IWebElement> allAccountingRef = GetAllElements(AccountingReferenceColumn);
            List<IWebElement> allAbv = GetAllElements(AbvColumn);
            for (int i = 0; i < totalRow.Count(); i++)
            {
                string id = GetElementText(allIdSite[i]);
                string name = GetElementText(allIdName[i]);
                string address = GetElementText(allAddress[i]);
                string siteType = GetElementText(allSiteType[i]);
                string startDate = GetElementText(allStartDate[i]);
                string endDate = GetElementText(allEndDate[i]);
                string clientRef = GetElementText(allClientReference[i]);
                string accountRef = GetElementText(allAccountingRef[i]);
                string abv = GetElementText(allAbv[i]);
                siteModels.Add(new SiteModel(id, name, address, siteType, startDate, endDate, clientRef, accountRef, abv));
            }
            return siteModels;
        }

        public DetailPartyPage VerifySiteManualCreated(AddressDetailModel addressDetail, SiteModel siteModel, string serviceSite)
        {
            Assert.AreEqual(siteModel.Name, addressDetail.SiteName);
            Assert.AreEqual(siteModel.Address, addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            Assert.AreEqual(siteModel.SiteType, serviceSite);
            return this;
        }

        public DetailPartyPage ClickSaveBtn()
        {
            ClickOnElement(SaveBtn);
            return this;
        }

        public DetailPartyPage ClickOnDetailsTab()
        {
            ClickOnElement(DetailsTab);
            return this;
        }
    }
}
