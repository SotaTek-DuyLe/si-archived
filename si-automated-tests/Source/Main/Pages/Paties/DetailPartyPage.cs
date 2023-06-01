using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyVehiclePage;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.WB.Tickets;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class DetailPartyPage : BasePageCommonActions
    {
        private const string AllTabDisplayed = "//li[@role='presentation' and not(contains(@style, 'visibility: collapse'))]/a";
        private const string AllTabInDropdown = "//ul[@class='dropdown-menu']//a";
        private const string DropdownBtn = "//li[contains(@class, 'dropdown')]/a[contains(@class, 'dropdown-toggle')]";
        private const string SuccessfullyToastMessage = "//div[@class='nsotifyjs-corner']//div[text()='Success']";
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";
        private const string SaveWithDetailsBtn = "//a[@aria-controls='details-tab']/ancestor::body//button[@title='Save']";
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");
        private readonly By dropdown = By.XPath("//li[@class='dropdown']");
        private readonly By accountNumber = By.CssSelector("p[title='Account Number']");
        private readonly By purchaseOrderTab = By.XPath("//a[text()='Purchase Orders']");

        private const string PartyName = "//div[text()='{0}']";
        private readonly By partyNameValue = By.XPath("//p[@class='object-name']");
        private readonly By title = By.XPath("//h4[text()='Party']");
        private readonly By wBtab = By.XPath("//a[text()='Weighbridge Settings']");
        private readonly By wBTicketTab = By.XPath("//a[text()='Weighbridge Tickets']");
        private readonly By taskTab = By.XPath("//ul[@class='dropdown-menu']//a[@aria-controls='tasks-tab']");
        private readonly By suspensionTab = By.XPath("//ul[@class='dropdown-menu']//a[@aria-controls='suspensions-tab']");
        public readonly By pricesTab = By.XPath("//ul[@class='dropdown-menu']//a[@aria-controls='prices-tab']");
        private readonly By adhocTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='adhoc-tab']");
        private readonly By canlendarTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='calendar-tab']");
        private readonly By siteTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='sites-tab']");
        private readonly By accountTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='account-tab']");
        private readonly By accountStatementTab = By.XPath("//ul[contains(@class,'nav-tabs')]/li[contains(@style,'visible')]/a[@aria-controls='partyAccountStatements-tab']");
        private readonly By accountStatementTabAlt = By.XPath("//span[text()='Account Statement']/parent::a");
        private readonly By historyTab = By.XPath("//ul[contains(@class,'nav-tabs')]/li[contains(@style,'visible')]/a[@aria-controls='partyHistory-tab']");
        private readonly By historyTabAlt = By.XPath("//span[text()='History']/parent::a");
        private readonly By notesTab = By.XPath("//ul[contains(@class,'nav-tabs')]/li[contains(@style,'visible')]/a[@aria-controls='notes-tab']");
        private readonly By notesTabAlt = By.XPath("(//ul[contains(@class,'nav-tabs')]//a[@aria-controls='notes-tab'])[2]");
        private readonly By costAgreementTab = By.XPath("//ul[contains(@class,'nav-tabs')]/li[contains(@style,'visible')]/a[@aria-controls='costAgreements-tab']");
        private readonly By costAgreementTabAlt = By.XPath("//span[text()='Cost Agreements']/parent::a");

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
        private readonly By partyNameInput = By.CssSelector("input#party-name");

        //DETAIL TAB DYNAMIC LOCATOR
        private const string InvoiceAddressValue = "//label[text()='Invoice Address']/following-sibling::div//option[text()='{0}']";
        private const string CorresspondenceValue = "//label[text()='Correspondence Address']/following-sibling::div//option[text()='{0}']";
        private const string PrimaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string PrimaryContactDisplayed = "//div[@data-bind='with:primaryContact']/p[text()='{0}']";
        private const string InvoiceContactValue = "//select[@id='invoice-contact']/option[text()='{0}']";
        private const string InvoiceContactDisplayed = "//div[@data-bind='with:invoiceContact']/p[text()='{0}']";
        private const string PartyTypeCheckbox = "//span[text()='{0}']/preceding-sibling::input";

        //SITE TAB LOCATOR
        private const string AddNewItemSiteBtn = "//div[@id='sites-tab']//button[contains(.,'Add New Item')]";
        private const string addNewItemSiteTabLoading = "//div[@id='sites-tab']//button[text()='Add New Item'and contains(@class, 'echo-disabled')]";
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
        private readonly By addNewItemContactTabLoading = By.XPath("//div[@id='contacts-tab']//button[text()='Add New Item'and contains(@class, 'echo-disabled')]");
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
        private readonly By restrictedSiteSelect = By.CssSelector("select[id='weighbridge-sites']");
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
        private const string anyRestrictedSites = "//span[text()='{0}']/parent::a/parent::li";

        //WB TICKET TAB
        private readonly By addNewItemWBTicket = By.XPath("//div[@id='weighbridgeTickets-tab']//button[text()='Add New Item']");
        private readonly By addNewItemItemWBTicketTabLoading = By.XPath("//div[@id='weighbridgeTickets-tab']//button[text()='Add New Item' and contains(@class, 'echo-disabled')]");

        //VEHICLE TAB
        private readonly By addNewItemLoadingVehicleTab = By.XPath("//div[@id='weighbridgeVehicleCustomerHauliers-tab']//button[text()='Add New Item'and contains(@class, 'echo-disabled')]");
        private readonly By addNewItemVehicleTab = By.XPath("//div[@id='weighbridgeVehicleCustomerHauliers-tab']//button[text()='Add New Item']");
        private const string TotalVehicleRow = "//div[@id='weighbridgeVehicleCustomerHauliers-tab']//div[@class='grid-canvas']/div";
        private const string ColumnInGrid = "//div[@id='weighbridgeVehicleCustomerHauliers-tab']//span[text()='{0}']/parent::div";
        private const string ColumnInRow = "//div[@id='weighbridgeVehicleCustomerHauliers-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By siteRows = By.XPath("//div[@id='sites-tab']//div[@class='grid-canvas']//div[contains(@class,'ui-widget-content')]");

        public readonly By OnStopButton = By.XPath("//div[@id='account-tab']//button[text()='ON STOP']");
        public readonly By OffStopButton = By.XPath("//div[@id='account-tab']//button[text()='OFF STOP']");
        public readonly By PartyStatus = By.XPath("//div[@title='Party Status']//span");
        #region OffStop Dialog
        public readonly By OffStopTitle = By.XPath("//div[@class='text-info' and text()='Are you sure you want to take all linked Agreements \"Off Stop\"?']");
        public readonly By CancelButton = By.XPath("//div[@class='bootbox modal fade in']//button[text()='Cancel']");
        public readonly By YesButton = By.XPath("//div[@class='bootbox modal fade in']//button[text()='Yes']");
        public readonly By NoButton = By.XPath("//div[@class='bootbox modal fade in']//button[text()='No']");
        #endregion

        //HISTORY TAB
        private readonly string historyItem = "(//span[text()='{0}']/following-sibling::span[1])[1]";
        private readonly By firstUpdatedUser = By.XPath("(//strong[text()='Update - Party'])[1]/parent::div/following-sibling::div/strong[1]");

        //NOTES TAB
        private readonly By titleInNotesTab = By.XPath("//label[text()='Title']/following-sibling::input");
        private readonly By notesInNotesTab = By.XPath("//label[text()='Note']/following-sibling::textarea");

        //ACCOUNT TAB
        private readonly By accountNumberInput = By.CssSelector("input[id='account-number']");
        private readonly By accountTypeDd = By.CssSelector("select[id='account-type']");
        private readonly By creditLimitInput = By.CssSelector("input[id='party-credit-limit']");
        private readonly By wipBalanceInput = By.CssSelector("input[id='wip-balance']");
        private readonly By accountBalanceInput = By.CssSelector("input[id='account-balance']");
        public readonly By OnStopBtnInAccountTab = By.XPath("//div[@id='account-tab']//button[text()='ON STOP']");

        [AllureStep]
        public DetailPartyPage InputCreditLimt(string creditLimitValue)
        {
            SendKeys(creditLimitInput, creditLimitValue);
            return this;
        }


        [AllureStep]
        public DetailPartyPage VerifyValueInCreditLimt(string creditLimitValue)
        {
            Assert.AreEqual(creditLimitValue, GetAttributeValue(creditLimitInput, "value"));
            return this;
        }

        //STEP
        [AllureStep]
        public DetailPartyPage WaitForDetailPartyPageLoadedSuccessfully(string name)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(string.Format(partyName, name));
            WaitUtil.WaitForElementVisible(DetailsTab);
            WaitUtil.WaitForElementVisible(agreementTab);
            WaitUtil.WaitForElementVisible(siteTab);
            WaitUtil.WaitForPageLoaded();
            return this;
        }


        [AllureStep]
        public DetailPartyPage InputPartyNameInput(string partyName)
        {
            SendKeys(partyNameInput, partyName);
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyPartyNameAfterUpdated(string partyNameValue)
        {
            Assert.AreEqual(partyNameValue, GetAttributeValue(partyNameInput, "value"));
            return this;
        }

        [AllureStep]
        public string GetWIPBalance()
        {
            return GetAttributeValue(wipBalanceInput, "value");
        }

        [AllureStep]
        public string GetAccountBalance()
        {
            return GetAttributeValue(accountBalanceInput, "value");
        }

        //TAB
        [AllureStep]
        public DetailPartyPage ClickTabDropDown()
        {
            ClickOnElement(dropdown);
            return this;
        }
        [AllureStep]
        public DetailPartyPage GoToATab(string tabName)
        {
            ClickOnElement(aTab, tabName);
            return this;
        }
        [AllureStep]
        public TaskTab ClickTasksTab()
        {
            if (!IsControlDisplayedNotThrowEx(taskTab))
            {
                ClickTabDropDown();
                ClickOnElement(taskTab);
            }
            else
            {
                ClickOnElement(taskTab);
            }
            return new TaskTab();
        }


        [AllureStep]
        public DetailPartyPage ClickSuspensionTab()
        {
            ClickOnElement(suspensionTab);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickAdHocTab()
        {
            ClickOnElement(adhocTab);
            return this;
        }
        [AllureStep]
        public PartyCalendarPage ClickCalendarTab()
        {
            ClickOnElement(canlendarTab);
            return new PartyCalendarPage();
        }
        [AllureStep]
        public DetailPartyPage ClickSiteTab()
        {
            ClickOnElement(siteTab);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickAccountTab()
        {
            ClickOnElement(accountTab);
            return this;
        }
        [AllureStep]
        public DetailPartyPage CLickOnStopBtn()
        {
            ClickOnElement(OnStopButton);
            return this;
        }
        [AllureStep]
        public DetailPartyPage CLickOffStopBtn()
        {
            if(IsControlDisplayedNotThrowEx(OnStopButton))
            {
                CLickOnStopBtn();
                WaitForLoadingIconToDisappear();
                SleepTimeInSeconds(2);
            }
            ClickOnElement(OffStopButton);
            return this;
        }
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public DetailPartyPage MergeAllTabInDetailPartyAndVerify()
        {
            WaitUtil.WaitForElementVisible(AllTabDisplayed);
            List<string> allTabDisplayed = GetAllTabDisplayed();
            List<string> allTabInDropdown = GetAllTabInDropdown();
            allTabDisplayed.AddRange(allTabInDropdown);
            Assert.AreEqual(allTabDisplayed, PartyTabConstant.AllTabInDetailParty.ToList());
            return this;
        }
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public DetailPartyPage VerifyDisplaySuccessfullyMessage()
        {
            //Assert.IsTrue(IsControlDisplayed(SuccessfullyToastMessage));
            //WaitUtil.WaitForElementInvisible(SuccessfullyToastMessage);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnParty(string name)
        {
            ClickOnElement(PartyName, name);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyPartyTypeChecked(string type)
        {
            WaitUtil.WaitForElementVisible(PartyTypeCheckbox, type);
            Assert.IsTrue(IsElementSelected(PartyTypeCheckbox, type));
            return this;
        }
        //Agreement tab

        [AllureStep]
        public AgreementTab OpenAgreementTab()
        {
            ClickOnElement(agreementTab);
            WaitForLoadingIconToDisappear();
            return new AgreementTab();
        }
        [AllureStep]
        public string GetPartyStartDate()
        {
            return WaitUtil.WaitForElementVisible(partyStartDate).Text;
        }
        [AllureStep]
        public SiteDetailPage OpenFirstSiteRow()
        {
            DoubleClickOnElement(firstSiteRow);
            return new SiteDetailPage();
        }

        //DETAIL TAB
        [AllureStep]
        public DetailPartyPage ClickOnAddInvoiceAddressBtn()
        {
            ClickOnElement(InvoiceAddressAddBtn);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnPrimaryContactDd()
        {
            ClickOnElement(PrimaryContactDd);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyValueInPrimaryContactDd(string[] expectedOption)
        {
            foreach(string option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(PrimaryContactValue, option)));
            }
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyFirstValueInPrimaryContactDd(ContactModel contactModel)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(PrimaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public AddPrimaryContactPage ClickAddPrimaryContactBtn()
        {
            ClickOnElement(primaryContactAddBtn);
            return new AddPrimaryContactPage();
        }
        [AllureStep]
        public DetailPartyPage ClickAddCorrespondenceAddress()
        {
            WaitUtil.WaitForElementVisible(CorrespondenceAddressAddBtn);
            ClickOnElement(CorrespondenceAddressAddBtn);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyCreatedSiteAddressAppearAtAddress(AddressDetailModel addressDetail)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(InvoiceAddress), addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyValueDefaultInCorresspondenAddress()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(CorresspondenceAddressDd), "Select...");
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyValueDefaultInInvoiceAddress()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(InvoiceAddress), "Select...");
            return this;
        }
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public DetailPartyPage SelectCorresspondenAddress(AddressDetailModel addressDetail)
        {
            ClickOnElement(CorresspondenceValue, addressDetail.Property.ToString() + " " + addressDetail.Street + ", " + addressDetail.Town + ", " + addressDetail.PostCode);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnSitesTab()
        {
            ClickOnElement(SitesTab);
            WaitUtil.WaitForElementInvisible(addNewItemSiteTabLoading);
            return this;
        }

        public DetailPartyPage ClickOnSitesTabNoWait()
        {
            ClickOnElement(SitesTab);
            return this;
        }

        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public DetailPartyPage ClickSaveWithDetailsBtn()
        {
            ClickOnElement(SaveWithDetailsBtn);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnDetailsTab()
        {
            ClickOnElement(DetailsTab);
            return this;
        }
        [AllureStep]
        public DetailPartyPage CloseWithoutSaving()
        {
            WaitUtil.WaitForElementClickable(closeWithoutSavingBtn);
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyAddressAppearAtSitesTab(string title)
        {
            WaitUtil.WaitForElementVisible(AddressTitle, title);
            Assert.IsTrue(IsControlDisplayed(AddressTitle, title));
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyCreatedSiteAddressAppearAtAddress(string address)
        {
            WaitUtil.WaitForElementVisible(SiteAddressValue, address);
            Assert.IsTrue(IsControlDisplayed(SiteAddressValue, address));
            return this;
        }
        [AllureStep]
        public DetailPartyPage SelectCreatedAddressInCorresspondenceAddress(string address)
        {
            ClickOnElement(CorresspondenceValue, address);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnInvoiceAddressButton()
        {
            ClickOnElement(InvoiceAddressButton);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyAddressIsFilledAtInvoiceAddress(string address)
        {
            Assert.AreEqual(address, GetFirstSelectedItemInDropdown(InvoiceAddress));
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyCreatedAddressAppearAtInvoiceAddress(string address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressValueDetails, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressValueDetails, address));
            return this;
        }
        [AllureStep]
        public DetailPartyPage SelectCreatedAddress(string address)
        {
            ClickOnElement(InvoiceAddressValue, address);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifySelectedAddressOnInvoicePage(String address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressOnPage, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressOnPage, address));
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickInvoiceContactDd()
        {
            ClickOnElement(InvoiceContactDd);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyValueInInvoiceContactDd(string[] expectedOption)
        {
            foreach (String option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(InvoiceContactValue, option)));
            }
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public DetailPartyPage ClickInternalCheckbox()
        {
            ClickOnElement(internalInputCheckbox);
            return this;
        }

        //Site Tab
        [AllureStep]
        public DetailPartyPage IsOnSitesTab()
        {
            WaitUtil.WaitForElementClickable(AddNewItemSiteBtn);
            Assert.IsTrue(IsControlDisplayed(AddNewItemSiteBtn));
            Assert.IsTrue(IsControlDisplayed(SiteList));
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnAddNewItemInSiteTabBtn()
        {
            ClickOnElement(AddNewItemSiteBtn);
            return this;
        }

        //CONTACT TAB
        [AllureStep]
        public DetailPartyPage ClickOnContactTab()
        {
            WaitUtil.WaitForElementVisible(contactTab);
            ClickOnElement(contactTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickAddNewItemAtContactTab()
        {
            WaitUtil.WaitForElementInvisible(addNewItemContactTabLoading);
            ClickOnElement(addNewItemAtContactTabBtn);
            return this;
        }
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public DetailPartyPage VerifyContactCreatedWithSomeFields(ContactModel contactModelExpected, ContactModel contacModelActual)
        {
            Assert.AreEqual(contactModelExpected.FirstName, contacModelActual.FirstName);
            Assert.AreEqual(contactModelExpected.LastName, contacModelActual.LastName);
            Assert.AreEqual(contactModelExpected.Mobile, contacModelActual.Mobile);
            Assert.AreEqual(contactModelExpected.StartDate + " 00:00", contacModelActual.StartDate);
            Assert.AreEqual(contactModelExpected.EndDate + " 00:00", contacModelActual.EndDate);
            return this;
        }
        [AllureStep]
        public EditPartyContactPage ClickFirstContact()
        {
            DoubleClickOnElement(firstContactRow);
            return PageFactoryManager.Get<EditPartyContactPage>();
        }

        //WB tab
        [AllureStep]
        public DetailPartyPage ClickWBSettingTab()
        {
            ClickOnElement(wBtab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyWBSettingTabIsNotDisplayed()
        {
            Assert.IsTrue(IsControlUnDisplayed(wBtab), "Weighbridge Settings tab is displayed");
            return this;
        }

        [AllureStep]
        public DetailPartyPage ClickOnAutoPrintTickedCheckbox()
        {
            ClickOnElement(autoPrintTicketCheckbox);
            return this;
        }

        [AllureStep]
        public DetailPartyPage ClickOnPurchaseOrderNumberRequiredCheckbox()
        {
            ClickOnElement(purcharseOrderNumberRequiredCheckbox);
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyPurchaseOrderNumberRequiredChecked()
        {
            Assert.IsTrue(IsCheckboxChecked(purcharseOrderNumberRequiredCheckbox));
            return this;
        }

        [AllureStep]
        public DetailPartyPage ClickOnDriverNameRequiredCheckbox()
        {
            ClickOnElement(driverNameRequiredCheckbox);
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyDriverNameRequiredChecked()
        {
            Assert.IsTrue(IsCheckboxChecked(driverNameRequiredCheckbox), "[Driver Name Required] is not checked");
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyDriverNameRequiredNotChecked()
        {
            Assert.IsFalse(IsCheckboxChecked(driverNameRequiredCheckbox), "[Driver Name Required] is checked");
            return this;
        }

        [AllureStep]
        [AllureDescription("Click on [Use Store Purchase order number] checkout")]
        public DetailPartyPage ClickOnUseStorePoNumberCheckbox()
        {
            ClickOnElement(userStorePoNumberCheckbox);
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyUseStorePoNumberChecked()
        {
            Assert.IsTrue(IsCheckboxChecked(userStorePoNumberCheckbox));
            return this;
        }

        [AllureStep]
        [AllureDescription("Click on Allow Manual Purchase order number checkout")]
        public DetailPartyPage ClickOnAllowManualPoNumberCheckbox()
        {
            ClickOnElement(allowManualPoNumberCheckbox);
            return this;
        }

        [AllureStep]
        [AllureDescription("Click on [external round code required] checkout")]
        public DetailPartyPage ClickOnExternalRoundCodeRequiredCheckbox()
        {
            ClickOnElement(externalRoundCodeCheckbox);
            return this;
        }

        [AllureStep]
        [AllureDescription("Click on [allow manual external round code] checkout")]
        public DetailPartyPage ClickOnAllowManualExternalRoundCodeCheckbox()
        {
            ClickOnElement(allowManualExternalCheckbox);
            return this;
        }

        [AllureStep]
        [AllureDescription("Click on [use stored external round code] checkout")]
        public DetailPartyPage ClickOnUseStoredExternalRoundCodeRequiredCheckbox()
        {
            ClickOnElement(userStoredExternalCheckbox);
            return this;
        }

        [AllureStep]
        [AllureDescription("Click on [allow manual name entry] checkout")]
        public DetailPartyPage ClickOnAllowManualNameEntryCheckbox()
        {
            ClickOnElement(allowManualNameEntryCheckbox);
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyAllowManualNameEntryChecked()
        {
            Assert.IsTrue(IsCheckboxChecked(allowManualNameEntryCheckbox), "[Allow Manual Name Entry] is not checked");
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyAllowManualNameEntryUnChecked()
        {
            Assert.IsFalse(IsCheckboxChecked(allowManualNameEntryCheckbox), "[Allow Manual Name Entry] is checked");
            return this;
        }

        [AllureStep]
        [AllureDescription("Click on [Restrict Products] checkout")]
        public DetailPartyPage ClickOnRestrictProductsCheckbox()
        {
            ClickOnElement(restricProductCheckbox);
            return this;
        }

        [AllureStep]
        [AllureDescription("Select any [Authorise Tipping]")]
        public DetailPartyPage SelectAnyOptionAuthoriseTipping(string optionValue)
        {
            ClickOnElement(authoriseTypingOption, optionValue);
            return this;
        }

        [AllureStep]
        [AllureDescription("Verify option in [Authorise Tipping] is checked")]
        public DetailPartyPage VerifyOptionAuthoriseTippingChecked(string optionValue)
        {
            Assert.IsTrue(IsCheckboxChecked(authoriseTypingOption, optionValue));
            return this;
        }

        [AllureStep]
        [AllureDescription("Select any [Restricted Sites]")]
        public DetailPartyPage SelectAnyOptionRestrictedSites(string optionValue)
        {
            ClickOnElement(restrictedSiteDd);
            ClickOnElement(anyRestrictedSites, optionValue);
            return this;
        }

        [AllureStep]
        [AllureDescription("Verify the [Restricted Sites] is blank")]
        public DetailPartyPage VerifyRestrictedSitesIsBlank()
        {
            Assert.AreEqual("", GetAttributeValue(restrictedSiteDd, "title"));
            return this;
        }

        [AllureStep]
        [AllureDescription("Input [Dormant Date]")]
        public DetailPartyPage InputDormantDate(string dormantDateValue)
        {
            SendKeys(dormantDateInput, dormantDateValue);
            return this;
        }

        [AllureStep]
        [AllureDescription("Clear text in [Warning Limit £]")]
        public DetailPartyPage ClearTextInWarningLimit()
        {
            ClearInputValue(warningLimitInput);
            return this;
        }

        [AllureStep]
        [AllureDescription("Input text in [Warning Limit £]")]
        public DetailPartyPage InputTextInWarningLimit(string  warningLimitValue)
        {
            SendKeys(warningLimitInput, warningLimitValue);
            return this;
        }

        [AllureStep]
        [AllureDescription("Verify value in [Warning Limit £]")]
        public DetailPartyPage VerifyValueInWarningLimit(string warningLimitValue)
        {
            Assert.AreEqual(GetAttributeValue(warningLimitInput, "value"), warningLimitValue);
            return this;
        }

        [AllureStep]
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
           // Assert.AreEqual(GetAttributeValue(warningLimitInput, "value"), "0");
            Assert.IsTrue(GetElement(string.Format(authoriseTypingOption, "Do Not Override On Stop")).Selected);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyDisplayYellowMesInLicenceNumberExField()
        {
            Assert.IsTrue(IsControlDisplayed(licenceNumberExpriryIsRequiredMessage));
            //Verify color
            Assert.AreEqual("rgba(159, 139, 64, 1)", GetCssValue(licenceNumberExpriryIsRequiredMessage, "color"));
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyForcusOnLicenceNumberExField()
        {
            VerifyFocusElement(licenceNumberExpriedInput);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyForcusOnLicenceNumberField()
        {
            VerifyFocusElement(licenceNumberInput);
            return this;
        }
        [AllureStep]
        public DetailPartyPage InputLienceNumberExField(string date)
        {
            SendKeys(licenceNumberExpriedInput, date);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyDisplayYellowMesInLicenceNumberField()
        {
            Assert.IsTrue(IsControlDisplayed(licenceNumberRequiredMessage));
            //Verify color
            Assert.AreEqual("rgba(159, 139, 64, 1)", GetCssValue(licenceNumberRequiredMessage, "color"));
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyDisplayGreenBoderInLicenceNumberField()
        {
            //Assert.AreEqual("rgb(102, 175, 233)", GetCssValue(licenceNumberInput, "border-color"));
            VerifyColorInBlueRange(licenceNumberInput);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyDisplayGreenBoderInLicenceNumberExField()
        {
            //Assert.AreEqual("rgb(102, 175, 233)", GetCssValue(licenceNumberExpriedInput, "border-color"));
            return this;
        }
        [AllureStep]
        public DetailPartyPage InputLicenceNumber(string value)
        {
            SendKeys(licenceNumberInput, value);
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyValueAtLicenceNumber(string value)
        {
            Assert.AreEqual(value, GetAttributeValue(licenceNumberInput, "value"));
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyValueAtLicenceNumberExp(string value)
        {
            Assert.AreEqual(value, GetAttributeValue(licenceNumberExpriedInput, "value"));
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyDisplayMesInInvoiceAddressField()
        {
            Assert.IsTrue(IsControlDisplayed(invoiceAddressRequiredMessage));
            return this;
        }
        [AllureStep]
        public DetailPartyPage VerifyDisplayMesInCorresspondenAddressField()
        {
            Assert.IsTrue(IsControlDisplayed(corresspondenRequiredMessage));
            //Verify color
            Assert.AreEqual("rgba(159, 139, 64, 1)", GetCssValue(corresspondenRequiredMessage, "color"));
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickDownloadBtnAndVerify()
        {
            ClickOnElement(downloadBtn);
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible("//h1[contains(text(), 'Search across all the registers')]");
            Assert.AreEqual("https://environment.data.gov.uk/public-register/view/search-all", GetCurrentUrl());
            Assert.AreEqual("Search all public registers", GetCurrentTitle());
            CloseCurrentWindow();
            SwitchToChildWindow(2);
            return this;
        }
        [AllureStep]
        public string GetPartyId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/parties/", "");
        }

        //VEHICLE TAB
        [AllureStep]
        public DetailPartyPage ClickOnVehicleTab()
        {
            ClickOnElement(VehicleTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyTableDisplayedInVehicle()
        {
            WaitUtil.WaitForElementVisible(addNewItemVehicleTab);
            foreach(string column in CommonConstants.ColumnInVehicleCustomerHaulierPage)
            {
                Assert.IsTrue(IsControlDisplayed(ColumnInGrid, column));
            }
            return this;
        }
        [AllureStep]
        public AddVehiclePage ClickAddNewVehicleBtn()
        {
            WaitUtil.WaitForElementInvisible(addNewItemLoadingVehicleTab);
            ClickOnElement(addNewItemVehicleTab);
            return PageFactoryManager.Get< AddVehiclePage>();
        }
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public DetailPartyPage ClickWBTicketTab()
        {
            ClickOnElement(wBTicketTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickAddNewWBTicketBtn()
        {
            WaitUtil.WaitForElementInvisible(addNewItemItemWBTicketTabLoading);
            ClickOnElement(addNewItemWBTicket);
            return PageFactoryManager.Get<CreateNewTicketPage>();
        }
        [AllureStep]
        public DetailPartyPage DoubleClickSiteRow(int siteId)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(By.XPath("//div[@id='sites-tab']//div[contains(@class, 'slick-headerrow-column l1 r1')]//input"), siteId.ToString());
            ClickOnElement(By.XPath("//button[@type='button' and @title='Apply Filters']"));
            WaitForLoadingIconToDisappear();
            DoubleClickOnElement(By.XPath("//div[@id='sites-tab']//div[contains(@class,'ui-widget-content slick-row even')]"));
            return this;
        }

        //TASK TAB
        private readonly By taskIdInput = By.XPath("//div[@id='tasks-tab']//div[contains(@class, 'l5 r5')]//input");
        private readonly By applyTaskBtn = By.XPath("//div[@id='tasks-tab']//button[@title='Apply Filters']");
        private readonly By firstCheckbox = By.XPath("//div[@id='tasks-tab']//div[contains(@class, 'l0 r0')]//input");
        private readonly By bulkUpdateBtn = By.XPath("//div[@id='tasks-tab']//button[text()='Bulk Update']");


        [AllureStep]
        public DetailPartyPage FilterTaskId(string taskId)
        {
            SendKeys(taskIdInput, taskId);
            ClickOnElement(applyTaskBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickFirstTaskCheckbox()
        {
            ClickOnElement(firstCheckbox);
            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage ClickBulkUpdateBtn()
        {
            WaitUtil.WaitForElementClickable(bulkUpdateBtn);
            ClickOnElement(bulkUpdateBtn);
            return PageFactoryManager.Get<TasksBulkUpdatePage>();
        }
        [AllureStep]
        public DetailPartyPage ClickOnAccountStatement()
        {
            if (!IsControlDisplayedNotThrowEx(accountStatementTab))
            {
                ClickTabDropDown();
                ClickOnElement(accountStatementTabAlt);
            }
            else
            {
                ClickOnElement(accountStatementTab);
            }
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClickOnHistoryTab()
        {
            if (!IsControlDisplayedNotThrowEx(historyTab))
            {
                ClickTabDropDown();
                ClickOnElement(historyTabAlt);
            }
            else
            {
                ClickOnElement(historyTab);
            }
            return this;
        }

        [AllureStep]
        public DetailPartyPage ClickOnNotesTab()
        {
            if (!IsControlDisplayedNotThrowEx(notesTab))
            {
                ClickTabDropDown();
                ClickOnElement(notesTabAlt);
            }
            else
            {
                ClickOnElement(notesTab);
            }
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyInfoInHistoryTab(string[] historyTitle, string[] valueExp, string userUpdatedValue)
        {
            Assert.AreEqual(userUpdatedValue, GetElementText(firstUpdatedUser));
            for(int i = 0; i < historyTitle.Length; i++)
            {
                Assert.AreEqual(valueExp[i], GetElementText(historyItem, historyTitle[i]), "Value at [" + historyTitle[i] + "] is not correct");
            }
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyRestrictedSite(string restrictedSiteValue)
        {
            Assert.AreEqual(restrictedSiteValue, GetElementText(historyItem, "Restricted Site"));
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyDisplayNotesTab()
        {
            Assert.IsTrue(IsControlDisplayed(titleInNotesTab), "Title input in Notes tab is not displayed");
            Assert.IsTrue(IsControlDisplayed(notesInNotesTab), "Notes input in Notes tab is not displayed");
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.CssSelector("div[id='retire-modal'] div[class='modal-header']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By checkboxWarning = By.CssSelector("input[id='confirmRetire']");
        private readonly By warningMessage = By.XPath("//label[contains(string(), 'I understand that this action cannot be undone.')]");
        private readonly By bodyRetiredPopup = By.CssSelector("p[data-bind='html: retireMessage']");

        #endregion

        [AllureStep]
        public DetailPartyPage IsRetiredPopup(string partyId)
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Console.WriteLine(GetElementText(retirePopupTitle));
            Assert.AreEqual("By retiring " + " Party" + partyId + "  the associated data below will also be  retired:", GetElementText(retirePopupTitle).Replace(Environment.NewLine, " "));
            Assert.IsTrue(IsControlDisplayed(warningMessage), "Warning message is not displayed");
            Assert.IsTrue(IsControlDisplayed(checkboxWarning), "Check box is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectParty)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public DetailPartyPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public string GetPartyName()
        {
            WaitUtil.WaitForTextToDisappearInElement(partyNameValue, "");
            return GetElementText(partyNameValue);
        }
        public string GetAddress()
        {
            return GetFirstSelectedItemInDropdown(CorresspondenceAddressDd);
        }

        [AllureStep]
        public DetailPartyPage SelectAnyAccountType(string accountTypeValue)
        {
            WaitUtil.WaitForElementVisible(accountNumberInput);
            WaitUtil.WaitForElementVisible(accountTypeDd);
            SelectTextFromDropDown(accountTypeDd, accountTypeValue);
            return this;
        }

        //SITES TAB
        private readonly By siteIdInput = By.XPath("//div[@id='sites-tab']//div[contains(@class, 'l1')]//input");
        private readonly By applyBtnSiteTab = By.XPath("//div[@id='sites-tab']//button[@title='Apply Filters']");
        private readonly By clearBtnSiteTab = By.XPath("//div[@id='sites-tab']//button[@title='Clear Filters']");
        private readonly By firstCheckboxAtRow = By.XPath("//div[@id='sites-tab']//div[@class='grid-canvas']//div[contains(@class, 'l0')]");
        private readonly By addNewItemInSiteTab = By.XPath("//div[@id='sites-tab']//button[text()='Add New Item']");
        private readonly By allRowInSitesTab = By.XPath("//div[@id='sites-tab']//div[@class='grid-canvas']/div");
        private const string AccountingAtRow = "//div[@class='grid-canvas']/div[{0}]/div[contains(@class, 'l8')]";

        [AllureStep]
        public DetailPartyPage FilterBySiteId(string siteIdValue)
        {
            WaitUtil.WaitForElementsPresent(allRowInSitesTab);
            SendKeys(siteIdInput, siteIdValue);
            ClickOnElement(applyBtnSiteTab);
            WaitForLoadingIconToDisappear();
            ClickOnElement(firstCheckboxAtRow);
            DoubleClickOnElement(firstSiteRow);
            return this;
        }

        [AllureStep]
        public DetailPartyPage ClickOnClearBtn()
        {
            ClickOnElement(clearBtnSiteTab);
            return this;
        }

        [AllureStep]
        public DetailPartyPage VerifyAccountingRefAnyRow(string rowNumber, string accountingValue)
        {
            Assert.AreEqual(accountingValue, GetElementText(AccountingAtRow, rowNumber), "Accounting Ref at row " + rowNumber + " is incorrect");
            return this;
        }

        [AllureStep]
        public string GetAccountNumber()
        {
            return GetElementText(accountNumber);
        }

        #region Purchase Orders tab
        private readonly By addNewItemPurchaseOrderTab = By.XPath("//div[@id='purchaseOrders-tab']//button[text()='Add New Item']");
        private readonly By addNewItemItemPurchaseOrderTabLoading = By.XPath("//div[@id='purchaseOrders-tab']//button[text()='Add New Item' and contains(@class, 'echo-disabled')]");
        private readonly By loadingAtPurchaseOrderTab = By.XPath("//div[@id='purchaseOrders-tab']//div[@class='loading-data']");

        [AllureStep]
        public DetailPartyPage ClickOnPurchaseOrdersTab()
        {
            ClickOnElement(purchaseOrderTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public PurchaseOrderDetailsPage ClickOnAddNewItemInPurchaseOrderTab()
        {
            WaitUtil.WaitForElementInvisible(addNewItemItemPurchaseOrderTabLoading);
            ClickOnElement(addNewItemPurchaseOrderTab);
            return new PurchaseOrderDetailsPage();
        }

        #endregion

        #region
        private readonly By addNewItemCostAgreementTab = By.XPath("//div[@id='costAgreements-tab']//button[text()='Add New Item']");
        private readonly By addNewItemLoadingCostAgreementTab = By.XPath("//div[@id='costAgreements-tab']//button[text()='Add New Item' and contains(@class, 'echo-disabled')]");

        [AllureStep]
        public DetailPartyPage ClickOnCostAgreementsTab()
        {
            if (!IsControlDisplayedNotThrowEx(costAgreementTab))
            {
                ClickTabDropDown();
                ClickOnElement(costAgreementTabAlt);
            }
            else
            {
                ClickOnElement(costAgreementTab);
            }
            return this;
        }

        [AllureStep]
        public DetailPartyPage ClickOnAddNewItemCostAgreementTab()
        {
            WaitUtil.WaitForElementInvisible(addNewItemLoadingCostAgreementTab);
            ClickOnElement(addNewItemCostAgreementTab);
            return this;
        }

        #endregion
    }
}
