using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContact;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;

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
        private readonly By title = By.XPath("//h4[text()='Party']");

        //COMMON DYNAMIC LOCATOR
        private const string partyName = "//p[text()='{0}']";

        //DETAIL TAB LOCATOR
        private const string InvoiceAddressAddBtn = "//label[text()='Invoice Address']/following-sibling::div//span[text()='Add']";
        private const string PartyNameInput = "//label[text()='Party Name']/following-sibling::div/input";
        private const string ContractInput = "//label[text()='Contract']/following-sibling::div/input";
        private readonly By CorresspondenceAddressDd = By.Id("party-correspondence-address");
        private const string CorrespondenceAddressAddBtn = "//label[text()='Correspondence Address']/following-sibling::div//span[text()='Add']";
        private const string SitesTab = "//a[text()='Sites']";
        private const string SaveBtn = "//button[@title='Save']";
        private const string InvoiceAddress = "//label[text()='Invoice Address']/following-sibling::div//select";
        private const string DetailsTab = "//a[text()='Details']";
        private readonly By InvoiceAddressButton = By.Id("party-invoice-address");
        private const string InvoiceAddressOnPage = "//div[contains(@data-bind,'invoiceAddress')]/p[text()='{0}']";
        private const string SiteAddressValue = "//label[text()='Correspondence Address']/following-sibling::div//option[text()='{0}']";
        private const string AddressTitle = "//div[text()='{0}']";
        private const string InvoiceAddressValueDetails = "//select[@id='party-invoice-address']/option[text()='{0}']";
        private readonly By PrimaryContactDd = By.CssSelector("select#primary-contact");
        private readonly By InvoiceContactDd = By.CssSelector("select#invoice-contact");
        private readonly By primaryContactAddBtn = By.XPath("//select[@id='primary-contact']/following-sibling::span[text()='Add']");

        //DETAIL TAB DYNAMIC LOCATOR
        private const string PrimaryContact = "//label[text()='Primary Contact']/following-sibling::div//select/option[text()={0}]";
        private const string InvoiceAddressValue = "//label[text()='Invoice Address']/following-sibling::div//option[text()='{0}']";
        private const string CorresspondenceValue = "//label[text()='Correspondence Address']/following-sibling::div//select/option[text()='{0}']";
        private const string PrimaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string PrimaryContactDisplayed = "//div[@data-bind='with:primaryContact']/p[text()='{0}']";
        private const string InvoiceContactValue = "//select[@id='invoice-contact']/option[text()='{0}']";
        private const string InvoiceContactDisplayed = "//div[@data-bind='with:invoiceContact']/p[text()='{0}']";

        //SITE TAB LOCATOR
        private const string AddNewItemBtn = "//button[text()='Add New Item']";
        private readonly By firstSiteRow = By.XPath("//div[@id='sites-tab']//div[@class='grid-canvas']/div[1]");
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
        private readonly By SiteList = By.XPath("//button[contains(.,'Add New Item')]/ancestor::nav/following-sibling::div/div/div[@class='slick-viewport']");
        private readonly By TotalRow = By.XPath("//span[contains(text(),'Total rows')]");

        //Agreement tab
        private readonly By agreementTab = By.XPath("//a[text()='Agreements']");
        private readonly By partyStartDate = By.XPath("//span[@title='Start Date']");

        //Contact tab
        private readonly By contactTab = By.XPath("//a[text()='Contacts']");
        private readonly By addNewItemAtContactTabBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By totalContactRow = By.XPath("//div[@id='contacts-tab']//div[@class='grid-canvas']/div");
        private const string ColumnOfRowContact = "//div[@id='contacts-tab']//div[@class='grid-canvas']/div/div[count(//div[@id='contacts-tab']//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private const string firstContactRow = "//div[@id='contacts-tab']//div[@class='grid-canvas']/div[1]";

        public DetailPartyPage WaitForDetailPartyPageLoadedSuccessfully(string name)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(string.Format(partyName, name));
            return this;
        }

        //TAB
        public List<string> GetAllTabDisplayed()
        {
            List<string> allTabs = new List<string>();
            List<IWebElement> allElements = GetAllElements(AllTabDisplayed);
            for (int i = 0; i < allElements.Count; i++)
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
        //Agreement tab
        public AgreementTab OpenAgreementTab()
        {
            ClickOnElement(agreementTab);
            WaitForLoadingIconToDisappear();
            return PageFactoryManager.Get<AgreementTab>();
        }

        public string GetPartyStartDate()
        {
            return WaitUtil.WaitForElementVisible(partyStartDate).Text;
        }

        public SiteDetailPage OpenFirstSiteRow()
        {
            DoubleClickOnElement(firstSiteRow);
            return new SiteDetailPage();
        }

        //DETAIL TAB
        public DetailPartyPage ClickOnAddInvoiceAddressBtn()
        {
            ClickOnElement(InvoiceAddressAddBtn);
            return this;
        }

        public DetailPartyPage ClickOnPrimaryContactDd()
        {
            ClickOnElement(PrimaryContactDd);
            return this;
        }

        public DetailPartyPage VerifyValueInPrimaryContactDd(string[] expectedOption)
        {
            foreach(string option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(PrimaryContactValue, option)));
            }
            return this;
        }

        public DetailPartyPage VerifyFirstValueInPrimaryContactDd(ContactModel contactModel)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(PrimaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }

        public DetailPartyPage SelectAnyPrimaryContactAndVerify(ContactModel contactModel)
        {
            ClickOnElement(string.Format(PrimaryContactValue, contactModel.FirstName + " " + contactModel.LastName));
            //Verify
            Assert.IsTrue(IsControlDisplayed(PrimaryContactDisplayed, contactModel.Title));
            Assert.IsTrue(IsControlDisplayed(PrimaryContactDisplayed, contactModel.Telephone));
            Assert.IsTrue(IsControlDisplayed(PrimaryContactDisplayed, contactModel.Mobile));
            Assert.IsTrue(IsControlDisplayed(PrimaryContactDisplayed, contactModel.Email));
            //Verify contact saved in primary contact dd
            Assert.AreEqual(GetFirstSelectedItemInDropdown(PrimaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }

        public AddPrimaryContactPage ClickAddPrimaryContactBtn()
        {
            ClickOnElement(primaryContactAddBtn);
            return new AddPrimaryContactPage();
        }

        public DetailPartyPage ClickAddCorrespondenceAddress()
        {
            WaitUtil.WaitForElementVisible(CorrespondenceAddressAddBtn);
            ClickOnElement(CorrespondenceAddressAddBtn);
            return this;
        }

        public DetailPartyPage VerifyCreatedSiteAddressAppearAtAddress(AddressDetailModel addressDetail)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(InvoiceAddress), addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            return this;
        }

        public DetailPartyPage VerifyValueDefaultInCorresspondenAddress()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(CorresspondenceAddressDd), "Select...");
            return this;
        }

        public DetailPartyPage VerifyValueDefaultInInvoiceAddress()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(InvoiceAddress), "Select...");
            return this;
        }

        public DetailPartyPage ClickCorresspondenAddress()
        {
            ClickOnElement(CorresspondenceAddressDd);
            return this;
        }

        public DetailPartyPage ClickInvoiceAddressDd()
        {
            ClickOnElement(InvoiceAddress);
            return this;
        }

        public DetailPartyPage VerifyDisplayNewSiteAddressInCorresspondence(AddressDetailModel addressDetail, bool isAddress)
        {
            if(isAddress)
            {
                Assert.IsTrue(IsControlDisplayed(InvoiceAddressValue, addressDetail.Address + ", " + addressDetail.Town + ", " + addressDetail.PostCode));
            }
            else
            {
                Assert.IsTrue(IsControlDisplayed(CorresspondenceValue, addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode));
            }
            return this;
        }

        public DetailPartyPage VerifyDisplayNewSiteAddressInInvoiceAddress(AddressDetailModel addressDetail,bool isAddress)
        {
            if(isAddress)
            {
                Assert.IsTrue(IsControlDisplayed(InvoiceAddressValue, addressDetail.Address + ", " + addressDetail.Town + ", " + addressDetail.PostCode));
            }
            else
            {
                Assert.IsTrue(IsControlDisplayed(InvoiceAddressValue, addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode));
            }
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
            Thread.Sleep(3000);
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

        public DetailPartyPage VerifySiteManualCreated(AddressDetailModel addressDetail, SiteModel siteModel, string serviceSite, bool isAddress)
        {
            Assert.AreEqual(siteModel.Name, addressDetail.SiteName);
            if(isAddress)
            {
                Assert.AreEqual(siteModel.Address, addressDetail.Address + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            } else
            {
                Assert.AreEqual(siteModel.Address, addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            }
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

        public DetailPartyPage VerifyAddressAppearAtSitesTab(string title)
        {
            WaitUtil.WaitForElementVisible(AddressTitle, title);
            Assert.IsTrue(IsControlDisplayed(AddressTitle, title));
            return this;
        }
        public DetailPartyPage VerifyCreatedSiteAddressAppearAtAddress(string address)
        {
            WaitUtil.WaitForElementVisible(SiteAddressValue, address);
            Assert.IsTrue(IsControlDisplayed(SiteAddressValue, address));
            return this;
        }

        public DetailPartyPage ClickOnInvoiceAddressButton()
        {
            ClickOnElement(InvoiceAddressButton);
            return this;
        }

        public DetailPartyPage VerifyCreatedAddressAppearAtInvoiceAddress(string address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressValueDetails, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressValueDetails, address));
            return this;
        }

        public DetailPartyPage SelectCreatedAddress(string address)
        {
            ClickOnElement(InvoiceAddressValue, address);
            return this;
        }

        public DetailPartyPage VerifySelectedAddressOnInvoicePage(String address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressOnPage, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressOnPage, address));
            return this;
        }

        public DetailPartyPage ClickInvoiceContactDd()
        {
            ClickOnElement(InvoiceContactDd);
            return this;
        }

        public DetailPartyPage VerifyValueInInvoiceContactDd(string[] expectedOption)
        {
            foreach (String option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(InvoiceContactValue, option)));
            }
            return this;
        }

        public DetailPartyPage SelectAnyInvoiceContactAndVerify(ContactModel contactModel)
        {
            ClickOnElement(string.Format(InvoiceContactValue, contactModel.FirstName + " " + contactModel.LastName));
            //Verify
            Assert.IsTrue(IsControlDisplayed(InvoiceContactDisplayed, contactModel.Title));
            Assert.IsTrue(IsControlDisplayed(InvoiceContactDisplayed, contactModel.Telephone));
            Assert.IsTrue(IsControlDisplayed(InvoiceContactDisplayed, contactModel.Mobile));
            Assert.IsTrue(IsControlDisplayed(InvoiceContactDisplayed, contactModel.Email));
            //Verify contact saved in invoice contact Dd
            Assert.AreEqual(GetFirstSelectedItemInDropdown(InvoiceContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }

        //Site Tab
        public DetailPartyPage IsOnSitesTab()
        {
            WaitUtil.WaitForElementClickable(AddNewItemBtn);
            Assert.IsTrue(IsControlDisplayed(AddNewItemBtn));
            Assert.IsTrue(IsControlDisplayed(SiteList));
            return this;
        }
        public DetailPartyPage ClickOnAddNewItemInSiteTabBtn()
        {
            ClickOnElement(AddNewItemBtn);
            return this;
        }

        //CONTACT TAB
        public DetailPartyPage ClickOnContactTab()
        {
            WaitUtil.WaitForElementVisible(contactTab);
            ClickOnElement(contactTab);
            return this;
        }

        public DetailPartyPage ClickAddNewItemAtContactTab()
        {
            ClickOnElement(addNewItemAtContactTabBtn);
            return this;
        }

        public List<ContactModel> GetAllContact()
        {
            List<ContactModel> contactModels = new List<ContactModel>();
            List<IWebElement> totalRow = GetAllElements(totalContactRow);
            List<IWebElement> allIdRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[0]));
            List<IWebElement> titleRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[1]));
            List<IWebElement> firstNameRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[2]));
            List<IWebElement> lastNameRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[3]));
            List<IWebElement> positionRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[4]));
            List<IWebElement> telephoneRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[5]));
            List<IWebElement> mobileRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[6]));
            List<IWebElement> emailRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[7]));
            List<IWebElement> receiveEmailRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[8]));
            List<IWebElement> contactGroupsRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[9]));
            List<IWebElement> startDateRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[10]));
            List<IWebElement> endDateRow = GetAllElements(string.Format(ColumnOfRowContact, CommonConstants.ContactTable[11]));

            for(int i = 0; i < totalRow.Count; i++)
            {
                string id = GetElementText(allIdRow[i]);
                string title = GetElementText(titleRow[i]);
                string firstName = GetElementText(firstNameRow[i]);
                string lastName = GetElementText(lastNameRow[i]);
                string position = GetElementText(positionRow[i]);
                string telephone = GetElementText(telephoneRow[i]);
                string mobile = GetElementText(mobileRow[i]);
                string email = GetElementText(emailRow[i]);
                bool receiveEmail = false;
                if (GetElementText(receiveEmailRow[i]).Equals("✓"))
                {
                    receiveEmail = true;
                } 
                string contactGroup = GetElementText(contactGroupsRow[i]);
                string startDate = GetElementText(startDateRow[i]);
                string endDate = GetElementText(endDateRow[i]);
                contactModels.Add(new ContactModel(id, title, firstName, lastName, position, telephone, mobile, email, receiveEmail, contactGroup, startDate, endDate));
            }
            return contactModels;
        }

        public DetailPartyPage VerifyContactCreated(ContactModel contactModelExpected, ContactModel contacModelActual)
        {
            Assert.AreEqual(contactModelExpected.Title, contacModelActual.Title);
            Assert.AreEqual(contactModelExpected.FirstName, contacModelActual.FirstName);
            Assert.AreEqual(contactModelExpected.LastName, contacModelActual.LastName);
            Assert.AreEqual(contactModelExpected.Position, contacModelActual.Position);
            Assert.AreEqual(contactModelExpected.Telephone, contacModelActual.Telephone);
            //Assert.AreEqual(contactModelExpected.Mobile, contacModelActual.Mobile);
            Assert.AreEqual(contactModelExpected.Email, contacModelActual.Email);
            Assert.AreEqual(contactModelExpected.ReceiveEmail, contacModelActual.ReceiveEmail);
            Assert.AreEqual(contactModelExpected.ContactGroups, contacModelActual.ContactGroups);
            Assert.AreEqual(contactModelExpected.StartDate + " 00:00", contacModelActual.StartDate);
            Assert.AreEqual(contactModelExpected.EndDate + " 00:00", contacModelActual.EndDate);
            return this;
        }

        public DetailPartyPage VerifyContactCreatedWithSomeFields(ContactModel contactModelExpected, ContactModel contacModelActual)
        {
            Assert.AreEqual(contactModelExpected.FirstName, contacModelActual.FirstName);
            Assert.AreEqual(contactModelExpected.LastName, contacModelActual.LastName);
            Assert.AreEqual(contactModelExpected.Mobile, contacModelActual.Mobile);
            Assert.AreEqual(contactModelExpected.StartDate + " 00:00", contacModelActual.StartDate);
            Assert.AreEqual(contactModelExpected.EndDate + " 00:00", contacModelActual.EndDate);
            return this;
        }

        public EditPartyContactPage ClickFirstContact()
        {
            DoubleClickOnElement(firstContactRow);
            return PageFactoryManager.Get<EditPartyContactPage>();
        }
    }

}
