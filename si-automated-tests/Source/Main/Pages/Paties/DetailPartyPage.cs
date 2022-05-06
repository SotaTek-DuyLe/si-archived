using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyVehiclePage;
using si_automated_tests.Source.Main.Pages.WB.Tickets;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class DetailPartyPage : BasePage
    {
        private const string AllTabDisplayed = "//li[@role='presentation' and not(contains(@style, 'visibility: collapse'))]/a";
        private const string AllTabInDropdown = "//ul[@class='dropdown-menu']//a";
        private const string DropdownBtn = "//li[contains(@class, 'dropdown')]/a[contains(@class, 'dropdown-toggle')]";
        private const string SuccessfullyToastMessage = "//div[@class='notifyjs-corner']//div[text()='Successfully saved party.']";
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";
        private const string SaveWithDetailsBtn = "//a[@aria-controls='details-tab']/ancestor::body//button[@title='Save']";
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");
        private readonly By dropdown = By.XPath("//li[@class='dropdown']");

        private const string PartyName = "//div[text()='{0}']";
        private readonly By title = By.XPath("//h4[text()='Party']");
        private readonly By wBtab = By.XPath("//a[text()='Weighbridge Settings']");
        private readonly By wBTicketTab = By.XPath("//a[text()='Weighbridge Tickets']");
        private readonly By taskTab = By.XPath("//ul[@class='dropdown-menu']//a[@aria-controls='tasks-tab']");

        //COMMON DYNAMIC LOCATOR
        private const string partyName = "//p[text()='{0}']";
        private const string aTab = "//a[text()='{0}']";

        //DETAIL TAB LOCATOR
        private const string InvoiceAddressAddBtn = "//label[text()='Invoice Address']/following-sibling::div//span[text()='Add']";
        private readonly By CorresspondenceAddressDd = By.Id("party-correspondence-address");
        private const string CorrespondenceAddressAddBtn = "//label[text()='Correspondence Address']/following-sibling::div//span[text()='Add']";
        private const string SitesTab = "//a[text()='Sites']";
        private const string VehicleTab = "//a[text()='Vehicles']";
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
        private readonly By internalInputCheckbox = By.CssSelector("input#is-internal");

        //DETAIL TAB DYNAMIC LOCATOR
        private const string InvoiceAddressValue = "//label[text()='Invoice Address']/following-sibling::div//option[text()='{0}']";
        private const string CorresspondenceValue = "//label[text()='Correspondence Address']/following-sibling::div//option[text()='{0}']";
        private const string PrimaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string PrimaryContactDisplayed = "//div[@data-bind='with:primaryContact']/p[text()='{0}']";
        private const string InvoiceContactValue = "//select[@id='invoice-contact']/option[text()='{0}']";
        private const string InvoiceContactDisplayed = "//div[@data-bind='with:invoiceContact']/p[text()='{0}']";
        private const string PartyTypeCheckbox = "//span[text()='{0}']/preceding-sibling::input";

        //SITE TAB LOCATOR
        private const string AddNewItemSiteBtn = "//div[@id='sites-tab']//button[text()='Add New Item']";
        private readonly By firstSiteRow = By.XPath("//div[@id='sites-tab']//div[@class='grid-canvas']/div[1]");
        private const string TotalSiteRow = "//div[@id='sites-tab']//div[@class='grid-canvas']/div";
        private const string IdColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='ID']/parent::div/preceding-sibling::div) + 1]";
        private const string NameColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='Name']/parent::div/preceding-sibling::div) + 1]";
        private const string AddressColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='Address']/parent::div) + 1]";
        private const string SiteTypeColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='Site Type']/parent::div/preceding-sibling::div) + 1]";
        private const string StartDateColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='Start Date']/parent::div/preceding-sibling::div) + 1]";
        private const string EndDateColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='End Date']/parent::div/preceding-sibling::div) + 1]";
        private const string ClientReferenceColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='Client Reference']/parent::div/preceding-sibling::div) + 1]";
        private const string AccountingReferenceColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='Accounting Reference']/parent::div/preceding-sibling::div) + 1]";
        private const string AbvColumn = "//div[@id='sites-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='Abv']/parent::div/preceding-sibling::div) + 1]";
        private readonly By SiteList = By.XPath("//button[contains(.,'Add New Item')]/ancestor::nav/following-sibling::div/div/div[@class='slick-viewport']");

        //Agreement tab
        private readonly By agreementTab = By.XPath("//a[text()='Agreements']");
        private readonly By partyStartDate = By.XPath("//span[@title='Start Date']");

        //Contact tab
        private readonly By contactTab = By.XPath("//a[text()='Contacts']");
        private readonly By addNewItemAtContactTabBtn = By.XPath("//div[@id='contacts-tab']//button[text()='Add New Item']");
        private readonly By totalContactRow = By.XPath("//div[@id='contacts-tab']//div[@class='grid-canvas']/div");
        private const string ColumnOfRowContact = "//div[@id='contacts-tab']//div[@class='grid-canvas']/div/div[count(//div[@id='contacts-tab']//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private const string firstContactRow = "//div[@id='contacts-tab']//div[@class='grid-canvas']/div[1]";

        //WB Setting tab
        private readonly By autoPrintTicketCheckbox = By.CssSelector("input#auto-print-ticket");
        private readonly By driverNameRequiredCheckbox = By.XPath("//label[text()='Driver Name Required']/following-sibling::div/input");
        private readonly By purcharseOrderNumberRequiredCheckbox = By.CssSelector("input#po-number-required");
        private readonly By userStorePoNumberCheckbox = By.CssSelector("input#use-stored-po-number");
        private readonly By allowManualPoNumberCheckbox = By.CssSelector("input#allow-manual-po-number");
        private readonly By externalRoundCodeCheckbox = By.CssSelector("input#external-round-code-required");
        private readonly By userStoredExternalCheckbox = By.CssSelector("input#use-stored-external-round-code");
        private readonly By allowManualExternalCheckbox = By.CssSelector("input#allow-manual-external-round-code");
        private readonly By allowManualNameEntryCheckbox = By.CssSelector("input#allow-manual-name-entry");
        private readonly By restricProductCheckbox = By.CssSelector("input#restrictProducts");
        private readonly By restrictedSiteDd = By.CssSelector("button[data-id='weighbridge-sites']");
        private readonly By licenceNumberInput = By.CssSelector("input#party-licence-number");
        private readonly By licenceNumberExpriedInput = By.CssSelector("input#party-licence-number-expiry");
        private readonly By calenderLicenceNumberEx = By.XPath("//input[@id='party-licence-number-expiry']/following-sibling::span");
        private readonly By dormantDateInput = By.CssSelector("input#dormant-date");
        private readonly By calenderDormantDate = By.XPath("//input[@id='dormant-date']/following-sibling::span");
        private readonly By warningLimitInput = By.CssSelector("input#warning-limit");
        private readonly By licenceNumberExpriryIsRequiredMessage = By.XPath("//div[text()='Licence Number Expiry is required']/parent::div");
        private readonly By licenceNumberRequiredMessage = By.XPath("//div[text()='Licence Number is required']/parent::div");
        private readonly By invoiceAddressRequiredMessage = By.XPath("//div[text()='Invoice Address is required']/parent::div");
        private readonly By corresspondenRequiredMessage = By.XPath("//div[text()='Correspondence Address is required']/parent::div");
        private readonly By downloadBtn = By.CssSelector("input#party-licence-number + span");

        private const string authoriseTypingOption = "//label[text()='Authorise Tipping']/following-sibling::div//label[text()='{0}']/input";

        //WB TICKET TAB
        private readonly By addNewItemWBTicket = By.XPath("//div[@id='weighbridgeTickets-tab']//button[text()='Add New Item']");

        //VEHICLE TAB
        private readonly By addNewItemVehicleTab = By.XPath("//div[@id='weighbridgeVehicleCustomerHauliers-tab']//button[text()='Add New Item']");
        private const string TotalVehicleRow = "//div[@id='weighbridgeVehicleCustomerHauliers-tab']//div[@class='grid-canvas']/div";
        private const string ColumnInGrid = "//div[@id='weighbridgeVehicleCustomerHauliers-tab']//span[text()='{0}']/parent::div";
        private const string ColumnInRow = "//div[@id='weighbridgeVehicleCustomerHauliers-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";


        //STEP
        public DetailPartyPage WaitForDetailPartyPageLoadedSuccessfully(string name)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(string.Format(partyName, name));
            return this;
        }

        //TAB
        public DetailPartyPage ClickTabDropDown()
        {
            ClickOnElement(dropdown);
            return this;
        }
        public DetailPartyPage GoToATab(string tabName)
        {
            ClickOnElement(aTab, tabName);
            return this;
        }
        public TaskTab ClickTasksTab()
        {
            ClickOnElement(taskTab);
            return new TaskTab();
        }
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
                WaitForLoadingIconToDisappear();
                Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
                allElements = GetAllElements(AllTabDisplayed);
            }
            return this;
        }
        public DetailPartyPage ClickAllTabInDropdownAndVerify()
        {
            if (IsControlDisplayedNotThrowEx(DropdownBtn))
            {
                int clickButtonIdx = 0;
                while (true)
                {
                    ClickOnElement(DropdownBtn);
                    List<IWebElement> allElements = GetAllElements(AllTabInDropdown);
                    if (clickButtonIdx >= allElements.Count)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                    ClickOnElement(allElements[clickButtonIdx]);
                    clickButtonIdx++;
                    WaitForLoadingIconToDisappear();
                    Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
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
        public DetailPartyPage VerifyPartyTypeChecked(string type)
        {
            WaitUtil.WaitForElementVisible(PartyTypeCheckbox, type);
            Assert.IsTrue(IsElementSelected(PartyTypeCheckbox, type));
            return this;
        }
        //Agreement tab
        public AgreementTab OpenAgreementTab()
        {
            ClickOnElement(agreementTab);
            WaitForLoadingIconToDisappear();
            return new AgreementTab();
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

        public DetailPartyPage ClickSaveWithDetailsBtn()
        {
            ClickOnElement(SaveWithDetailsBtn);
            return this;
        }

        public DetailPartyPage ClickOnDetailsTab()
        {
            ClickOnElement(DetailsTab);
            return this;
        }
        public DetailPartyPage CloseWithoutSaving()
        {
            WaitUtil.WaitForElementClickable(closeWithoutSavingBtn);
            ClickOnElement(closeWithoutSavingBtn);
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

        public DetailPartyPage SelectCreatedAddressInCorresspondenceAddress(string address)
        {
            ClickOnElement(CorresspondenceValue, address);
            return this;
        }

        public DetailPartyPage ClickOnInvoiceAddressButton()
        {
            ClickOnElement(InvoiceAddressButton);
            return this;
        }

        public DetailPartyPage VerifyAddressIsFilledAtInvoiceAddress(string address)
        {
            Assert.AreEqual(address, GetFirstSelectedItemInDropdown(InvoiceAddress));
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

        public DetailPartyPage ClickInternalCheckbox()
        {
            ClickOnElement(internalInputCheckbox);
            return this;
        }

        //Site Tab
        public DetailPartyPage IsOnSitesTab()
        {
            WaitUtil.WaitForElementClickable(AddNewItemSiteBtn);
            Assert.IsTrue(IsControlDisplayed(AddNewItemSiteBtn));
            Assert.IsTrue(IsControlDisplayed(SiteList));
            return this;
        }
        public DetailPartyPage ClickOnAddNewItemInSiteTabBtn()
        {
            ClickOnElement(AddNewItemSiteBtn);
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

        //WB tab
        public DetailPartyPage ClickWBSettingTab()
        {
            ClickOnElement(wBtab);
            return this;
        }

        public DetailPartyPage VerifyWBSettingTab()
        {
            WaitUtil.WaitForElementVisible(autoPrintTicketCheckbox);
            Assert.IsTrue(GetElement(autoPrintTicketCheckbox).Selected);
            Assert.IsFalse(GetElement(driverNameRequiredCheckbox).Selected);
            Assert.IsFalse(GetElement(purcharseOrderNumberRequiredCheckbox).Selected);
            Assert.IsFalse(GetElement(userStorePoNumberCheckbox).Selected);
            Assert.IsTrue(GetElement(allowManualPoNumberCheckbox).Selected);
            Assert.IsFalse(GetElement(externalRoundCodeCheckbox).Selected);
            Assert.IsFalse(GetElement(userStoredExternalCheckbox).Selected);
            Assert.IsTrue(GetElement(allowManualExternalCheckbox).Selected);
            Assert.IsFalse(GetElement(allowManualNameEntryCheckbox).Selected);
            Assert.IsFalse(GetElement(restricProductCheckbox).Selected);
            ScrollToBottomOfPage();
            Assert.IsTrue(IsControlDisplayed(authoriseTypingOption, "Never Allow Tipping"));
            Assert.IsTrue(IsControlDisplayed(authoriseTypingOption, "Do Not Override On Stop"));
            Assert.IsTrue(IsControlDisplayed(authoriseTypingOption, "Always Allow Tipping"));
            Assert.IsTrue(IsControlDisplayed(restrictedSiteDd));
            Assert.IsTrue(IsControlDisplayed(licenceNumberInput));
            Assert.IsTrue(IsControlDisplayed(licenceNumberExpriedInput));
            Assert.IsTrue(IsControlDisplayed(calenderLicenceNumberEx));
            Assert.IsTrue(IsControlDisplayed(dormantDateInput));
            Assert.IsTrue(IsControlDisplayed(calenderDormantDate));
            Assert.IsTrue(IsControlDisplayed(warningLimitInput));
            //Default value
            Assert.AreEqual(GetAttributeValue(warningLimitInput, "value"), "0");
            Assert.IsTrue(GetElement(string.Format(authoriseTypingOption, "Do Not Override On Stop")).Selected);
            return this;
        }

        public DetailPartyPage VerifyDisplayYellowMesInLicenceNumberExField()
        {
            Assert.IsTrue(IsControlDisplayed(licenceNumberExpriryIsRequiredMessage));
            //Verify color
            Assert.AreEqual("rgba(159, 139, 64, 1)", GetCssValue(licenceNumberExpriryIsRequiredMessage, "color"));
            return this;
        }

        public DetailPartyPage VerifyForcusOnLicenceNumberExField()
        {
            VerifyFocusElement(licenceNumberExpriedInput);
            return this;
        }

        public DetailPartyPage VerifyForcusOnLicenceNumberField()
        {
            VerifyFocusElement(licenceNumberInput);
            return this;
        }

        public DetailPartyPage InputLienceNumberExField(string date)
        {
            SendKeys(licenceNumberExpriedInput, date);
            return this;
        }

        public DetailPartyPage VerifyDisplayYellowMesInLicenceNumberField()
        {
            Assert.IsTrue(IsControlDisplayed(licenceNumberRequiredMessage));
            //Verify color
            Assert.AreEqual("rgba(159, 139, 64, 1)", GetCssValue(licenceNumberRequiredMessage, "color"));
            return this;
        }

        public DetailPartyPage VerifyDisplayGreenBoderInLicenceNumberField()
        {
            Assert.AreEqual("rgb(102, 175, 233)", GetCssValue(licenceNumberInput, "border-color"));
            return this;
        }

        public DetailPartyPage VerifyDisplayGreenBoderInLicenceNumberExField()
        {
            Assert.AreEqual("rgb(102, 175, 233)", GetCssValue(licenceNumberExpriedInput, "border-color"));
            return this;
        }

        public DetailPartyPage InputLicenceNumber(string value)
        {
            SendKeys(licenceNumberInput, value);
            return this;
        }

        public DetailPartyPage VerifyDisplayMesInInvoiceAddressField()
        {
            Assert.IsTrue(IsControlDisplayed(invoiceAddressRequiredMessage));
            return this;
        }

        public DetailPartyPage VerifyDisplayMesInCorresspondenAddressField()
        {
            Assert.IsTrue(IsControlDisplayed(corresspondenRequiredMessage));
            //Verify color
            Assert.AreEqual("rgba(159, 139, 64, 1)", GetCssValue(corresspondenRequiredMessage, "color"));
            return this;
        }

        public DetailPartyPage ClickDownloadBtnAndVerify()
        {
            ClickOnElement(downloadBtn);
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible("//h2[text()='Tell us whether you accept cookies']");
            Assert.AreEqual("https://environment.data.gov.uk/public-register/view/search-all", GetCurrentUrl());
            Assert.AreEqual("Search all public registers", GetCurrentTitle());
            CloseCurrentWindow();
            SwitchToChildWindow(2);
            return this;
        }

        public string GetPartyId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/parties/", "");
        }

        //VEHICLE TAB
        public DetailPartyPage ClickOnVehicleTab()
        {
            ClickOnElement(VehicleTab);
            return this;
        }

        public DetailPartyPage VerifyTableDisplayedInVehicle()
        {
            WaitUtil.WaitForElementVisible(addNewItemVehicleTab);
            foreach(string column in CommonConstants.ColumnInVehicleCustomerHaulierPage)
            {
                Assert.IsTrue(IsControlDisplayed(ColumnInGrid, column));
            }
            return this;
        }

        public AddVehiclePage ClickAddNewVehicleBtn()
        {
            ClickOnElement(addNewItemVehicleTab);
            return PageFactoryManager.Get< AddVehiclePage>();
        }

        public List<VehicleModel> GetAllVehicleModel()
        {
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            List<IWebElement> totalRow = GetAllElements(TotalVehicleRow);
            List<IWebElement> allIdVehicle = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[0]));
            List<IWebElement> allResource = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[1]));
            List<IWebElement> allCustomer = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[2]));
            List<IWebElement> allHaulier = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[3]));
            List<IWebElement> allHireStart = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[4]));
            List<IWebElement> allHireEnd = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[5]));
            List<IWebElement> allSuspendedDate = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[6]));
            List<IWebElement> allSuspendedReason = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[7]));
            for (int i = 0; i < totalRow.Count(); i++)
            {
                string id = GetElementText(allIdVehicle[i]);
                string resource = GetElementText(allResource[i]);
                string customer = GetElementText(allCustomer[i]);
                string haulier = GetElementText(allHaulier[i]);
                string hireStart = GetElementText(allHireStart[i]);
                string hireEnd = GetElementText(allHireEnd[i]);
                string suspendedDate = GetElementText(allSuspendedDate[i]);
                string suspendedReason = GetElementText(allSuspendedReason[i]);
                vehicleModels.Add(new VehicleModel(id, resource, customer, haulier, hireStart, hireEnd, suspendedDate, suspendedReason));
            }
            return vehicleModels;
        }

        public DetailPartyPage VerifyVehicleCreated(VehicleModel vehicleModelDisplayed, string resource, string customer, string haulier, string hireStart, string hireEnd)
        {
            Assert.AreEqual(resource, vehicleModelDisplayed.Resource);
            Assert.AreEqual(customer, vehicleModelDisplayed.Customer);
            Assert.AreEqual(haulier, vehicleModelDisplayed.Haulier);
            Assert.AreEqual(hireStart, vehicleModelDisplayed.HireStart);
            Assert.AreEqual(hireEnd, vehicleModelDisplayed.HireEnd);
            return this;
        }

        //WB ticket tab
        public DetailPartyPage ClickWBTicketTab()
        {
            ClickOnElement(wBTicketTab);
            return this;
        }

        public CreateNewTicketPage ClickAddNewWBTicketBtn()
        {
            ClickOnElement(addNewItemWBTicket);
            return PageFactoryManager.Get<CreateNewTicketPage>();
        }

    }

}
