using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.IE_Configuration;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccount;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccountStatement;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class PartyTest : BaseTest
    {
        [Category("Create party")]
        [Test]
        public void TC_004_Create_a_party_customer_type_form_grid()
        {
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(4), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
                .MergeAllTabInDetailPartyAndVerify()
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify();

        }

        [Category("Create party")]
        [Test]
        public void TC_005_Create_party_customer_type_from_action_dropdown()
        {
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(5), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickCreateEventDropdownAndVerify()
                .GoToThePatiesByCreateEvenDropdown()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectContract(3)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            //Verify all tab display correctly
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
                .MergeAllTabInDetailPartyAndVerify()
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify()
                .SwitchToFirstWindow();

        }

        [Category("Create party")]
        [Test]
        public void TC_006_Dont_Create_a_party_customer_type_from_parties_grid_with_future_start_date()
        {
            string errorMessage = "Start Date cannot be in the future";

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput("Auto" + CommonUtil.GetRandomString(2))
                .SelectStartDatePlusOneDay()
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<CreatePartyPage>()
                .VerifyDisplayErrorMessage(errorMessage);
        }

        [Category("SiteAddress")]
        [Test]
        public void TC_007_CreateASiteFromPartyDetailsTab()
        {
            LoginPage login = new LoginPage();
            HomePage homePage = new HomePage();
            PartySiteAddressPage partySiteAddressPage = new PartySiteAddressPage();
            CreateEditSiteAddressPage createEditSiteAddressPage = new CreateEditSiteAddressPage();
            PartyCommonPage partyCommonPage = new PartyCommonPage();
            CreatePartyPage createPartyPage = new CreatePartyPage();
            DetailPartyPage detailPartyPage = new DetailPartyPage();
            string PartyName = "AutoPartyy " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            //login
            login.GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party 
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage.WaitForLoadingIconToDisappear();
            createPartyPage
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage();
            //Test path for TC 007
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.ClickOnDetailsTab()
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
                
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            createEditSiteAddressPage.WaitForLoadingIconToDisappear();
            string addressAdded = createEditSiteAddressPage.SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
                .SelectCreatedAddress(addressAdded)
                .VerifySelectedAddressOnInvoicePage(address)
                .ClickOnSitesTab()
                .VerifyAddressAppearAtSitesTab(addressSite1)
                .ClickOnDetailsTab()
                .ClickSaveBtn();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage();
        }

        [Category("SiteAddress")]
        [Test]
        public void TC_008_CreateASiteFromPartySideTab()
        {
            LoginPage login = new LoginPage();
            HomePage homePage = new HomePage();
            PartySiteAddressPage partySiteAddressPage = new PartySiteAddressPage();
            CreateEditSiteAddressPage createEditSiteAddressPage = new CreateEditSiteAddressPage();
            PartyCommonPage partyCommonPage = new PartyCommonPage();
            CreatePartyPage createPartyPage = new CreatePartyPage();
            DetailPartyPage detailPartyPage = new DetailPartyPage();
            string PartyName = "AutoPartyy " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            //login
            login.GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser6)
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party 
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage();
            //Test path for TC 008
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.ClickOnSitesTab()
                .IsOnSitesTab()
                .ClickOnAddNewItemInSiteTabBtn()
                .SwitchToLastWindow();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            createEditSiteAddressPage.WaitForLoadingIconToDisappear();
            string addressAdded = createEditSiteAddressPage.SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.IsOnSitesTab()
                .VerifyAddressAppearAtSitesTab(addressSite1);
            detailPartyPage.ClickOnDetailsTab()
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
                .ClickOnSitesTab()
                .VerifyAddressAppearAtSitesTab(addressSite1);
        }

        [Category("SiteAddress")]
        [Test]
        public void TC_009_CreateASiteNegaTive()
        {
            LoginPage login = new LoginPage();
            HomePage homePage = new HomePage();
            PartySiteAddressPage partySiteAddressPage = new PartySiteAddressPage();
            CreateEditSiteAddressPage createEditSiteAddressPage = new CreateEditSiteAddressPage();
            PartyCommonPage partyCommonPage = new PartyCommonPage();
            CreatePartyPage createPartyPage = new CreatePartyPage();
            DetailPartyPage detailPartyPage = new DetailPartyPage();
            string PartyName = "AutoPartyy " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            string addressSite2 = "Site Twickenham 2" + CommonUtil.GetRandomNumber(4);
            //login
            login.GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser6)
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage();
            //Test path for TC 009
            //create site 1
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.ClickOnSitesTab()
                .IsOnSitesTab()
                .ClickOnAddNewItemInSiteTabBtn()
                .SwitchToLastWindow();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            createEditSiteAddressPage.WaitForLoadingIconToDisappear();
            string addressAdded = createEditSiteAddressPage.SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.IsOnSitesTab()
                .VerifyAddressAppearAtSitesTab(addressSite1);
            //create duplicate site
            detailPartyPage.IsOnSitesTab()
               .ClickOnAddNewItemInSiteTabBtn()
               .SwitchToLastWindow();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            createEditSiteAddressPage.WaitForLoadingIconToDisappear();
            createEditSiteAddressPage
                .SelectSiteAddress(addressAdded)
                .SelectAddressClickNextBtn()
                .ClickCreateBtn()
                .VerifyDuplicateErrorMessage() //verify error message when create duplicate site address
                .InsertSiteName(addressSite2)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.IsOnSitesTab()
                .VerifyAddressAppearAtSitesTab(addressSite2); //successful save site address with other name 
        }

        [Category("SiteAddress")]
        [Test]
        public void TC_010_Create_manual_site()
        {
            string PartyName = "AutoPartyTC10 " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
            //Test path for TC 010
                .ClickOnAddInvoiceAddressBtn()
                .SwitchToChildWindow(3);
            string siteName = "SiteAutoTC10" + CommonUtil.GetRandomNumber(3);
            string postCode = CommonUtil.GetRandomString(2) + CommonUtil.GetRandomNumber(3);
            AddressDetailModel addressDetailModel = new AddressDetailModel(siteName, postCode);
            PageFactoryManager.Get<PartySiteAddressPage>()
                .IsOnPartySiteAddressPage()
                .ClickOnCreateManuallyBtn()
                .IsCheckAddressDetailScreen(false)
                .SendKeyInSiteNameInput(siteName)
                .VerifyCreateBtnDisabled()
                .InputAllMandatoryFieldInCheckAddressDetailScreen(addressDetailModel)
                .ClickCreateBtn()
                .WaitForLoadingIconInvisiable()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyCreatedSiteAddressAppearAtAddress(addressDetailModel)
                .ClickCorresspondenAddress()
                .VerifyDisplayNewSiteAddressInCorresspondence(addressDetailModel, false)
                .SelectCorresspondenAddress(addressDetailModel)
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            List<SiteModel> allSiteModel = PageFactoryManager.Get<DetailPartyPage>()
                .GetAllSiteInList();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifySiteManualCreated(addressDetailModel, allSiteModel[0], "Serviced Site", false)
                .ClickOnDetailsTab()
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage();
        }

        [Category("SiteAddress")]
        [Test]
        public void TC_011_Create_Non_Georgaphical_address()
        {
            string siteName = "SiteAutoTC11" + CommonUtil.GetRandomNumber(3);
            string postCode = CommonUtil.GetRandomString(2) + CommonUtil.GetRandomNumber(3);
            AddressDetailModel addressDetailModel = new AddressDetailModel(siteName, postCode);

            string PartyName = "AutoPartyTC11 " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
            //Test path for TC 011
                 .ClickOnSitesTab()
                 .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .IsOnSitesTab()
                .ClickOnAddNewItemInSiteTabBtn()
                .SwitchToChildWindow(3);
            PageFactoryManager.Get<PartySiteAddressPage>()
                .IsOnPartySiteAddressPage()
                .ClickOnNonGeoGraphicalAddressBtn()
                .IsCheckAddressDetailScreen(true)
                .SendKeyInSiteNameInput(siteName)
                .InputSomeMandatoryFieldInCheckAddressDetailScreen(addressDetailModel)
                .VerifyCreateBtnDisabled()
                .InputValueInCountry(addressDetailModel.Country)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            List<SiteModel> allSiteModel = PageFactoryManager.Get<DetailPartyPage>()
                .GetAllSiteInList();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifySiteManualCreated(addressDetailModel, allSiteModel[0], "Serviced Site", true)
                .ClickOnDetailsTab()
                .VerifyValueDefaultInCorresspondenAddress()
                .ClickCorresspondenAddress()
                .VerifyDisplayNewSiteAddressInCorresspondence(addressDetailModel, true)
                //Invoice Address
                .VerifyValueDefaultInInvoiceAddress()
                .ClickInvoiceAddressDd()
                .VerifyDisplayNewSiteAddressInInvoiceAddress(addressDetailModel, true);
        }

        [Category("Accounting Reference")]
        [Category("Dee")]
        [Test]
        public void TC_134_the_setting_of_accounting_refference()
        {
            string accountTypeSettingUrl = WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=AccountType&ObjectID=6";
            string accountSettingUrl = WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=AccountType&ObjectID=6";
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(4), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            string overrideValue = "Account reference value " + CommonUtil.GetRandomString(12);
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(accountTypeSettingUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<AccountTypeDetailPage>()
                .inputOverrideValue(overrideValue)
                .TickOverrideCheckbox()
                .clickOnSaveButton()
                .WaitForLoadingIconToDisappear()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
                .SwitchToTab("Account");
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .SelectAccountType("Charity")
                .VerifyAccountReferenceEnabled(false)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(73)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickTabDropDown()
                .ClickOnAccountStatement();
            PageFactoryManager.Get<AccountStatementPage>()
                .ClickCreateCreditNote()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .ClickYesBtn()
                .VerifyAccountReferenceIsReadonly()
                .CloseCurrentWindow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<AccountStatementPage>()
               .ClickTakePayment()
               .SwitchToLastWindow();
            PageFactoryManager.Get<SalesReceiptPage>()
                .IsAccountRefReadOnly()
                .CloseCurrentWindow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<AccountStatementPage>()
               .ClickCreateSaleInvoice()
               .SwitchToLastWindow();
            PageFactoryManager.Get<CreateInvoicePage>()
                .VerifyAccountReferenceIsReadonly()
                .CloseCurrentWindow()
                .SwitchToLastWindow();

            //.MergeAllTabInDetailPartyAndVerify()
            //.ClickAllTabAndVerify()
            //.ClickAllTabInDropdownAndVerify();

        }
    }
}
