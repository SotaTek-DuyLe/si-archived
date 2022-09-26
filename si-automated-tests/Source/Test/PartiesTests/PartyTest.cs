using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
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
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyHistory;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using TaskAllocationPage = si_automated_tests.Source.Main.Pages.Applications.TaskAllocationPage;
using TaskConfirmationPage = si_automated_tests.Source.Main.Pages.Applications.TaskConfirmationPage;
using RoundInstanceDetailPage = si_automated_tests.Source.Main.Pages.Applications.RoundInstanceDetailPage;

namespace si_automated_tests.Source.Test.PartiesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class PartyTest : BaseTest
    {
        [Category("Create party")]
        [Category("Chang")]
        [Test]
        public void TC_004_Create_a_party_customer_type_form_grid()
        {
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(4), Contract.RMC, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.RMC)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            //PageFactoryManager.Get<DetailPartyPage>()
            //    .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyModel.PartyName);
            //PageFactoryManager.Get<DetailPartyPage>()
            //    .MergeAllTabInDetailPartyAndVerify();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify();

        }

        [Category("Create party")]
        [Category("Chang")]
        [Test]
        public void TC_005_Create_party_customer_type_from_action_dropdown()
        {
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(5), Contract.RMC, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

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
                .IsCreatePartiesPopup(Contract.RM)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectContract(3)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            //Verify all tab display correctly
            //PageFactoryManager.Get<DetailPartyPage>()
            //    .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DetailPartyPage>()
                 .WaitForDetailPartyPageLoadedSuccessfully(partyModel.PartyName)
                //.MergeAllTabInDetailPartyAndVerify()
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify()
                .SwitchToFirstWindow();

        }

        [Category("Create party")]
        [Category("Chang")]
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.RMC)
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.RMC, CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage.WaitForLoadingIconToDisappear();
            createPartyPage
                .IsCreatePartiesPopup(Contract.RMC)
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.RMC, CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage
                .IsCreatePartiesPopup(Contract.RMC)
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.RMC, CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage
                .IsCreatePartiesPopup(Contract.RMC)
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
        [Category("Chang")]
        [Test]
        public void TC_010_Create_manual_site()
        {
            string PartyName = "AutoPartyTC10 " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, Contract.RMC, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.RMC)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyModel.PartyName)
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
            //PageFactoryManager.Get<DetailPartyPage>()
            //    .VerifyDisplaySuccessfullyMessage();
        }

        [Category("SiteAddress")]
        [Category("Chang")]
        [Test]
        public void TC_011_Create_Non_Georgaphical_address()
        {
            string siteName = "SiteAutoTC11" + CommonUtil.GetRandomNumber(3);
            string postCode = CommonUtil.GetRandomString(2) + CommonUtil.GetRandomNumber(3);
            AddressDetailModel addressDetailModel = new AddressDetailModel(siteName, postCode);

            string PartyName = "AutoPartyTC11 " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, Contract.RMC, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.RMC)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
            //    .VerifyDisplaySuccessfullyMessage()
            //Test path for TC 011
                 .WaitForDetailPartyPageLoadedSuccessfully(partyModel.PartyName)
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
        public void TC_134_the_setting_of_accounting_referrence()
        {
            string charityAccountTypeSettingUrl = WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=AccountType&ObjectID=6";
            string adminRolePriviledgeUrl = WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?VName=SysConfigView&VNodeID=316&CPath=T77R1297&ObjectID=1297&TypeName=EchoObjectView&RefTypeName=none&ReferenceName=none";
            string accountRefPriviledgeUrl = WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?VName=SysConfigView&VNodeID=318&CPath=T77R1297M402T78R15920&ObjectID=15920&TypeName=EchoObjectViewNode&RefTypeName=none&ReferenceName=none";
            string userAuto30SettingUrl = WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?CPath=&ObjectID=1076&CTypeName=User&CReferenceName=none&CObjectID=0&TypeName=User&RefTypeName=none&ReferenceName=none&InEdit=true#";
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(4), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            string overrideValue = "Account reference value " + CommonUtil.GetRandomString(12);
            int partyId = 73;
            string partyUrl = "https://test.echoweb.co.uk/web/parties/" + partyId;
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(charityAccountTypeSettingUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<AccountTypeDetailPage>()
                .inputOverrideValue(overrideValue)
                .TickOverrideCheckbox()
                .clickSaveButton()
                .WaitForLoadingIconToDisappear()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.RMC)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .SleepTimeInMiliseconds(1)
                //.VerifyDisplaySuccessfullyMessage()
                .SwitchToTab("Account");
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .SelectAccountType("Charity")
                .VerifyAccountReferenceEnabled(false)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .GoToURL(partyUrl);
            PageFactoryManager.Get<DetailPartyPage>()
                .SwitchToTab("Account");
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .SelectAccountType("Charity")
                .SelectAccountType("Credit")
                .ClickSaveBtn();
            PageFactoryManager.Get<PartyAccountPage>()
                .SelectAccountType("Charity")
                .ClickSaveBtn()
                .SleepTimeInMiliseconds(1000);
            PageFactoryManager.Get<DetailPartyPage>()
                //.VerifyDisplaySuccessfullyMessage()
                //.ClickTabDropDown()
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
            PageFactoryManager.Get<DetailPartyPage>()
                //.ClickTabDropDown()
                .ClickOnHistoryTab()
                .ClickRefreshBtn();
            PageFactoryManager.Get<PartyHistoryPage>()
                .VerifyNewestAccountingReference(overrideValue);

            //Set override value to null
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(charityAccountTypeSettingUrl);
            PageFactoryManager.Get<AccountTypeDetailPage>()
                .inputOverrideValue("")
                .TickOverrideCheckbox()
                .clickSaveButton()
                .WaitForLoadingIconToDisappear()
                .GoToURL(partyUrl);
            PageFactoryManager.Get<DetailPartyPage>()
                .SwitchToTab("Account");
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .SelectAccountType("Charity")
                .SelectAccountType("Credit")
                .ClickSaveBtn();
            PageFactoryManager.Get<PartyAccountPage>()
                .SelectAccountType("Charity")
                .VerifyAccountReferenceEnabled(false)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                //.ClickTabDropDown()
                .ClickOnHistoryTab()
                .ClickRefreshBtn();

            //go to url to deny update
            PageFactoryManager.Get<PartyHistoryPage>()
                .VerifyNewestAccountingReference("");
            PageFactoryManager.Get<BasePage>()
                .GoToURL(accountRefPriviledgeUrl);
            PageFactoryManager.Get<AdminRolePriviledgePage>()
                .TurnOnDenyUpdate()
                .ClickSaveButton()
                .SleepTimeInMiliseconds(2000);
            //go to another url to change role of different user
            PageFactoryManager.Get<BasePage>()
                .GoToURL(userAuto30SettingUrl);
            PageFactoryManager.Get<UserDetailPage>()
                .IsOnUserDetailPage()
                .ClickAdminRoles()
                .UntickAdminRole("System Administrator")
                .ChooseAdminRole("Search - Parties")
                .ChooseAdminRole("Parties")
                .ClickSave()
                .SleepTimeInMiliseconds(2000);
            //go to that user and parties id and verify can only read account ref for all ref type
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickUserNameDd()
                .ClickLogoutBtn();
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser30.UserName)
                .SendKeyToPassword(AutoUser30.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser30)
                .GoToURL(partyUrl);
            PageFactoryManager.Get<DetailPartyPage>()
                .SwitchToTab("Account");
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .VerifyAllAcountReferenceDisabled();
        }

        [Category("Edit Party")]
        [Test(Description = "Verify that Tasks in Core Task State: Closed, Cancelled, Failed don't display 'On Stop' icon when a Party is On Stop")]
        public void TC_164_Tasks_On_Stop_Status_icon()
        {
            int partyId = 40;
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully("Twickenham Stoop")
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            var partyCalendarPage = PageFactoryManager.Get<PartyCalendarPage>();
            partyCalendarPage.GoToAugust()
                .WaitForLoadingIconToDisappear();
            //Edit Task 1
            partyCalendarPage
                .ClickDayInstance(CommonUtil.StringToDateTime("2022-08-03", "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            var detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Completed");
            string scheduledate = detailTaskPage.GetElementText(detailTaskPage.ScheduleDateInput);
            detailTaskPage.SendKeys(detailTaskPage.completionDateInput, scheduledate);
            detailTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Success")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Edit Task 2
            partyCalendarPage.ClickDayInstance(CommonUtil.StringToDateTime("2022-08-10", "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Cancelled");
            detailTaskPage.ClickSaveBtn()
                .ClickCloseBtn()
                .AcceptAlert()
                .SwitchToChildWindow(2);
            //Edit Task 3
            partyCalendarPage.ClickDayInstance(CommonUtil.StringToDateTime("2022-08-17", "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Not Completed");
            detailTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Success")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            var detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage.ClickOnElement(detailPartyPage.OnStopButton);
            detailPartyPage.VerifyToastMessage("Success");
            detailPartyPage.VerifyElementText(detailPartyPage.PartyStatus, "On Stop");
            detailPartyPage.ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            partyCalendarPage.ClickOnElement(partyCalendarPage.ServicesDropdownButton);
            partyCalendarPage.ClickSellectAllServices();
            partyCalendarPage.ClickOnElement(partyCalendarPage.ProductDropdownButton);
            partyCalendarPage.ClickSellectAllSites();
            partyCalendarPage.ClickApplyCalendarButton()
                .WaitForLoadingIconToDisappear();
            partyCalendarPage.GoToAugust()
                .WaitForLoadingIconToDisappear();
            partyCalendarPage.VerifyDayInstanceHasRaiseHandStatus(CommonUtil.StringToDateTime("2022-08-03", "yyyy-MM-dd"), false)
                .VerifyDayInstanceHasRaiseHandStatus(CommonUtil.StringToDateTime("2022-08-10", "yyyy-MM-dd"), false)
                .VerifyDayInstanceHasRaiseHandStatus(CommonUtil.StringToDateTime("2022-08-17", "yyyy-MM-dd"), false)
                .VerifyDayInstanceHasRaiseHandStatus(CommonUtil.StringToDateTime("2022-08-01", "yyyy-MM-dd"), true);
            partyCalendarPage
                .ClickDayInstance(CommonUtil.StringToDateTime("2022-08-03", "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyElementVisibility(detailTaskPage.OnHoldImg, false)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            partyCalendarPage
                .ClickDayInstance(CommonUtil.StringToDateTime("2022-08-01", "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyElementVisibility(detailTaskPage.OnHoldImg, true)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            partyCalendarPage.ClickCloseBtn()
                .SwitchToFirstWindow();

            //Navigate to Applications>Task Allocation and filter Contract: Richmond Commercial, Services: Collections:Commercial Collections, From and to date of date for which Task was changed to 
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();

            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "03/08/2022";
            string to = "03/08/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.RMC);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.RMC)
                .ExpandRoundNode("Collections")
                .SelectRoundNode("Commercial Collections");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Control + "a");
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Delete);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.SendKeys(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("REC1-AM", "Wednesday")
                .WaitForLoadingIconToDisappear(false);
            taskAllocationPage.SendKeys(taskAllocationPage.IdFilterInput, "9292");
            taskAllocationPage.SleepTimeInMiliseconds(200);
            taskAllocationPage.VerifyRoundInstanceStatusCompleted();

            //Navigate to Applications>Task Confirmation and filter Contract: Richmond Commercial, Services: Collections:Commercial Collections, select REC1-AM Wednesday, Scheduled date of date for which Task was changed to Completed ( the same date as Round Instance date)
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Confirmation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskConfirmationPage taskConfirmationPage = PageFactoryManager.Get<TaskConfirmationPage>();
            taskConfirmationPage.SelectTextFromDropDown(taskConfirmationPage.ContractSelect, Contract.RMC);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ServiceInput);
            taskConfirmationPage.ExpandRoundNode(Contract.RMC)
                .ExpandRoundNode("Collections")
                .ExpandRoundNode("Commercial Collections")
                .SelectRoundNode("REC1-AM");
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ScheduleDateInput);
            taskConfirmationPage.SleepTimeInMiliseconds(1000);
            taskConfirmationPage.SendKeysWithoutClear(taskConfirmationPage.ScheduleDateInput, Keys.Control + "a");
            taskConfirmationPage.SendKeysWithoutClear(taskConfirmationPage.ScheduleDateInput, Keys.Delete);
            taskConfirmationPage.SendKeysWithoutClear(taskConfirmationPage.ScheduleDateInput, from);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ContractSelect);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ButtonGo);
            taskConfirmationPage.WaitForLoadingIconToDisappear(false);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ExpandRoundsGo);
            taskConfirmationPage.SleepTimeInMiliseconds(200);
            taskConfirmationPage.SendKeys(taskConfirmationPage.IdFilterInput, "9292");
            taskConfirmationPage.SleepTimeInMiliseconds(200);
            taskConfirmationPage.VerifyRoundInstanceStatusCompleted();
            taskConfirmationPage.DoubleClickRoundInstance()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceDetailPage roundInstanceDetailPage = PageFactoryManager.Get<RoundInstanceDetailPage>();
            roundInstanceDetailPage.ClickOnElement(roundInstanceDetailPage.WorkSheetTab);
            roundInstanceDetailPage.WaitForLoadingIconToDisappear();
            roundInstanceDetailPage.SwitchNewIFrame();
            roundInstanceDetailPage.ClickOnElement(roundInstanceDetailPage.ExpandRoundsGo);
            roundInstanceDetailPage.SleepTimeInMiliseconds(300);
            roundInstanceDetailPage.SendKeys(roundInstanceDetailPage.IdFilterInput, "9292");
            roundInstanceDetailPage.SleepTimeInMiliseconds(200);
            roundInstanceDetailPage.VerifyRoundInstanceStatusCompleted();
        }
    }
}
