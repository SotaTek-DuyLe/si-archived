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
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Finders;
using System.Linq;
using System;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;

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
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(4), Contract.Commercial, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<DetailPartyPage>()
            //    .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyModel.PartyName)
                .SleepTimeInMiliseconds(3000);
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
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(5), Contract.Commercial, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickCreateEventDropdownAndVerify()
                .GoToThePatiesByCreateEvenDropdown()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Municipal)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectContract(3)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //Verify all tab display correctly
            //PageFactoryManager.Get<DetailPartyPage>()
            //    .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DetailPartyPage>()
                 .WaitForDetailPartyPageLoadedSuccessfully(partyModel.PartyName)
                 .SleepTimeInMiliseconds(3000);
            //.MergeAllTabInDetailPartyAndVerify()
            PageFactoryManager.Get<DetailPartyPage>()
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
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput("Auto" + CommonUtil.GetRandomString(2))
                .SelectStartDatePlusOneDay()
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<CreatePartyPage>()
                .VerifyDisplayErrorMessage(errorMessage);
        }

        [Category("SiteAddress")]
        [Category("Huong")]
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.Commercial, CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            //login
            login.GoToURL(WebUrl.MainPageUrl);
            login
                .Login(AutoUser6.UserName, AutoUser6.Password);
            homePage
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage.WaitForLoadingIconToDisappear();
            createPartyPage
                .IsCreatePartiesPopup(Contract.Commercial)
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
        [Category("Huong")]
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.Commercial, CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            //login
            login.GoToURL(WebUrl.MainPageUrl);
            login
                .Login(AutoUser6.UserName, AutoUser6.Password);
            homePage
                .IsOnHomePage(AutoUser6)
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage
                .IsCreatePartiesPopup(Contract.Commercial)
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
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .WaitForLoadingIconToDisappear();
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
                .ClickOnSitesTabNoWait()
                .VerifyAddressAppearAtSitesTab(addressSite1);
        }

        [Category("SiteAddress")]
        [Category("Huong")]
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.Commercial, CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            string addressSite2 = "Site Twickenham 2" + CommonUtil.GetRandomNumber(4);
            //login
            login.GoToURL(WebUrl.MainPageUrl);
            login
                .Login(AutoUser6.UserName, AutoUser6.Password);
            homePage
                .IsOnHomePage(AutoUser6)
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            createPartyPage
                .IsCreatePartiesPopup(Contract.Commercial)
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
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.Commercial, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
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
            PartyModel partyModel = new PartyModel(PartyName, Contract.Commercial, CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
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
            string partyUrl = WebUrl.MainPageUrl + "web/parties/" + partyId;
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(charityAccountTypeSettingUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
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
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
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
                .SleepTimeInMiliseconds(1000)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                //.VerifyDisplaySuccessfullyMessage()
                //.ClickTabDropDown()
                .ClickOnAccountStatement()
                .WaitForLoadingIconToDisappear();
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
               .SwitchToLastWindow()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .IsAccountRefReadOnly()
                .CloseCurrentWindow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<AccountStatementPage>()
               .ClickCreateInvoice()
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
                .Login(AutoUser30.UserName, AutoUser30.Password);
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
        [Category("Huong")]
        [Test(Description = "Verify that Tasks in Core Task State: Closed, Cancelled, Failed don't display 'On Stop' icon when a Party is On Stop")]
        public void TC_164_Tasks_On_Stop_Status_icon()
        {
            int partyId = 40;
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
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
            var wednesdaysInMonth = CommonUtil.GetWeekDaysInCurrentMonth(System.DayOfWeek.Wednesday).Select(x => x.ToString("yyyy-MM-dd")).ToList();
            var mondaysInMonth = CommonUtil.GetWeekDaysInCurrentMonth(System.DayOfWeek.Monday).Select(x => x.ToString("yyyy-MM-dd")).ToList();
            //Edit Task 1
            partyCalendarPage
                .ClickDayInstance(CommonUtil.StringToDateTime(wednesdaysInMonth[0], "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            var detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Completed");
            string scheduledate = detailTaskPage.GetAttributeValue(detailTaskPage.ScheduleDateInput, "value");
            detailTaskPage.SendKeys(detailTaskPage.completionDateInput, scheduledate);
            detailTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Success")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Edit Task 2
            partyCalendarPage.ClickDayInstance(CommonUtil.StringToDateTime(wednesdaysInMonth[1], "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Cancelled");
            detailTaskPage.ClickSaveBtn()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Edit Task 3
            partyCalendarPage.ClickDayInstance(CommonUtil.StringToDateTime(wednesdaysInMonth[2], "yyyy-MM-dd"))
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
            WaitUtil.TextToBePresentInElementLocated(detailPartyPage.PartyStatus, "On Stop");
            detailPartyPage.VerifyElementText(detailPartyPage.PartyStatus, "On Stop");
            detailPartyPage.ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            partyCalendarPage.ClickOnElement(partyCalendarPage.ServicesDropdownButton);
            partyCalendarPage.ClickSellectAllServices();
            partyCalendarPage.ClickOnElement(partyCalendarPage.ProductDropdownButton);
            partyCalendarPage.ClickSellectAllSites();
            partyCalendarPage.ClickApplyCalendarButton()
                .WaitForLoadingIconToDisappear();
            partyCalendarPage.VerifyDayInstanceHasRaiseHandStatus(CommonUtil.StringToDateTime(wednesdaysInMonth[0], "yyyy-MM-dd"), false)
                .VerifyDayInstanceHasRaiseHandStatus(DateTime.Now, false)
                .VerifyDayInstanceHasRaiseHandStatus(CommonUtil.StringToDateTime(mondaysInMonth[0], "yyyy-MM-dd"), true);
            partyCalendarPage
                .ClickDayInstance(CommonUtil.StringToDateTime(wednesdaysInMonth[0], "yyyy-MM-dd"))
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            string taskId = detailTaskPage.GetCurrentUrl().Split('/').LastOrDefault();
            detailTaskPage.VerifyElementVisibility(detailTaskPage.OnHoldImg, false)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            partyCalendarPage
                .ClickDayInstance(CommonUtil.StringToDateTime(mondaysInMonth[0], "yyyy-MM-dd"))
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
            string from = CommonUtil.GetFirstDayInMonth(DateTime.Now).ToString("dd/MM/yyyy");
            string to = CommonUtil.GetLastDayInMonth(DateTime.Now).ToString("dd/MM/yyyy");
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Commercial);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Commercial)
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
            taskAllocationPage.SendKeys(taskAllocationPage.IdFilterInput, taskId);
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
            taskConfirmationPage.SelectTextFromDropDown(taskConfirmationPage.ContractSelect, Contract.Commercial);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ServiceInput);
            taskConfirmationPage.ExpandRoundNode(Contract.Commercial)
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
            taskConfirmationPage.SendKeys(taskConfirmationPage.IdFilterInput, taskId);
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
            roundInstanceDetailPage.SendKeys(roundInstanceDetailPage.IdFilterInput, taskId);
            roundInstanceDetailPage.SleepTimeInMiliseconds(200);
            roundInstanceDetailPage.VerifyRoundInstanceStatusCompleted();
        }

        [Category("Edit Party")]
        [Category("Huong")]
        [Test(Description = "Verify that user can enter in step 3 Asset Qty equal or less than number of assets on agreementlineassetproducts defined in Step 2.")]
        public void TC_157_Agreement_Line_Amendment_of_Asset_Qty()
        {
            //Verify that user can enter in step 3 Asset Qty equal or less than number of assets on agreementlineassetproducts defined in Step 2.
            int partyId = 73;
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
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
            //PartyId=73 (Greggs)-> navigate to Agreements tab->Click 'Add New Item'
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully("Greggs")
                .OpenAgreementTab()
                .WaitForLoadingIconToDisappear();
            AgreementTab agreementTab = PageFactoryManager.Get<AgreementTab>();
            agreementTab.ClickAddNewItem()
                .SwitchToChildWindow(3);
            PartyAgreementPage partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            string selectedValue = "Use Customer";
            string agreementType = "Commercial Collections";
            partyAgreementPage
               .IsOnPartyAgreementPage()
               .VerifyStartDateIsCurrentDate()
               .VerifyEndDateIsPredefined()
               .VerifyElementIsMandatory(partyAgreementPage.agreementTypeInput)
               .VerifySelectedValue(partyAgreementPage.primaryContract, selectedValue)
               .VerifySelectedValue(partyAgreementPage.invoiceContact, selectedValue)
               .VerifySelectedValue(partyAgreementPage.correspondenceAddressInput, selectedValue)
               .VerifySelectedValue(partyAgreementPage.invoiceAddressInput, selectedValue)
               .VerifySelectedValue(partyAgreementPage.invoiceScheduleInput, selectedValue);
            partyAgreementPage
               .SelectAgreementType(agreementType)
               .ClickSaveBtn()
               .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitForLoadingIconToDisappear();
            partyAgreementPage
               .WaitForAgreementPageLoadedSuccessfully("COMMERCIAL COLLECTIONS", "Greggs")
               .VerifyAgreementStatus("New")
               .VerifyElementVisibility(partyAgreementPage.addServiceBtn, true)
               .VerifyElementVisibility(partyAgreementPage.approveBtn, true)
               .VerifyElementVisibility(partyAgreementPage.cancelBtn, true);
            //On the opened Agreement, click 'Add Services'
            partyAgreementPage
               .ClickAddService()
               .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
               .IsOnSiteServiceTab()
               .SelectServiceSite("Greggs - 8 KING STREET, TWICKENHAM, TW1 3SN")
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteAndServiceTab>()
               .SelectService("Commercial")
               .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
               .WaitForLoadingIconToDisappear();
            //'Click ' +Add'
            string assetType = AgreementConstants.ASSET_TYPE_1100L;
            int assetQty = 3;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string tenure = AgreementConstants.TENURE_RENTAL;
            int productQty = 1000;
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .SelectAssetType(assetType)
                .InputAssetQuantity(assetQty)
                .ChooseTenure(tenure)
                .TickAssetOnSite()
                .InputAssetOnSiteNum(1)
                .ChooseProduct(product)
                .ChooseEwcCode("150106")
                .InputProductQuantity(productQty)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .ClickNext();
            //Click on '+Add regular service'
            PageFactoryManager.Get<ScheduleServiceTab>()
                  .IsOnScheduleTab()
                  .ClickAddService()
                  .SleepTimeInMiliseconds(300);
            PageFactoryManager.Get<ScheduleServiceTab>()
                .InputAssestQtyAndVerifyStateDoneBtn("4", false)
                .InputAssestQtyAndVerifyStateDoneBtn("2", true);
            PageFactoryManager.Get<ScheduleServiceTab>()
                  .ClickDoneScheduleBtn()
                  .ClickOnNotSetLink()
                  .ClickOnWeeklyBtn()
                  .UntickAnyDayOption()
                  .SelectDayOfWeek("Mon")
                  .ClickDoneRequirementBtn()
                  .VerifyScheduleSummary("Once Every week on any Monday")
                  .ClickNext();
            var priceTab = PageFactoryManager.Get<PriceTab>();
            priceTab.WaitForLoadingIconToDisappear();
            priceTab.ClickNext();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .VerifyInvoiceOptions("Use Agreement")
                .ClickFinish();
            partyAgreementPage
                .VerifyServicePanelPresent()
                .VerifyAgreementLineFormHasGreenBorder()
                .ClickSaveBtn()
                .VerifyToastMessageOnParty(MessageSuccessConstants.SuccessMessage, true)
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(5000);
            partyAgreementPage.ExpandAgreementLine()
                .ExpandAgreementHeader("Mobilization")
                .VerifyTasklineInAgreement(0, "1100L", "3", "General Recycling", "1000", "Kilograms")
                .ExpandAgreementHeader("Regular")
                .VerifyTasklineInAgreement(1, "1100L", "2", "General Recycling", "1000", "Kilograms")
                .ExpandAgreementHeader("De-Mobilization")
                .VerifyTasklineInAgreement(2, "1100L", "3", "General Recycling", "1000", "Kilograms")
                .ExpandAgreementHeader("Ad-hoc")
                .VerifyTasklineInAgreement(3, "1100L", "3", "General Recycling", "1000", "Kilograms");
        }

        [Category("Credit note")]
        [Category("Huong")]
        [Test(Description = "Verify no duplication occur in salescreditlines grid")]
        public void TC_230_The_orphan_salescreditlines_are_duplicated_in_the_grid()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/68");
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password);
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnAccountStatement()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AccountStatementPage>()
               .ClickCreateCreditNote()
               .SwitchToLastWindow()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreditNotePage>().VerifyNotDuplicateSaleCreditLine();
            CommonFinder finder = new CommonFinder(DbContext);
            var saleCreditLines = finder.GetSaleCreditLineDBs();
            var duplicates = saleCreditLines.GroupBy(x => x.salescreditlineID)
                                .Where(g => g.Count() > 1)
                                .Select(y => y.Key)
                                .ToList();
            Assert.IsTrue(duplicates.Count == 0);
        }
        [Category("Sale invoice")]
        [Category("Dee")]
        [Test]
        public void TC_136_oprhan_sale_invoices()
        {
            string lineType = "Commercial Line Type";
            string product = "General Refuse";
            string priceElement = "Revenue";
            string quantity = "1";
            string price = "100.00";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/68");
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password)
                .WaitForLoadingIconToDisappear();
            var partyName = PageFactoryManager.Get<DetailPartyPage>()
                .GetPartyName();
            PageFactoryManager.Get<DetailPartyPage>()
                .SwitchToTab("Details");
            var address = PageFactoryManager.Get<DetailPartyPage>()
                .GetAddress();
            var site = partyName + " - " + address;
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnAccountStatement()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AccountStatementPage>()
                .ClickCreateInvoiceItem()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                .SelectDepot(Contract.Commercial)
                .InputInfo(lineType, site, product, priceElement, quantity, price, price)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValueInTab("Transaction Type", "Uninvoiced Item")
                .VerifyFirstResultValueInTab("", "")
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AccountStatementPage>()
                .ClickCreateInvoice()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceDetailPage>()
                .ClickCancelButton()
                .SleepTimeInSeconds(3)
                .SwitchToLastWindow<AccountStatementPage>()
                .ClickCreateInvoice()
                .SwitchToLastWindow<SalesInvoiceDetailPage>()
                .ClickCreateAdhocInvoiceBtn()
                .IsOnSaleInvoiceDetailPage()
                .CloseCurrentWindow()
                //.AcceptAlert()
                .SwitchToLastWindow<AccountStatementPage>()
                .ClickCreateInvoice()
                .SwitchToLastWindow();
            var invoiceId = PageFactoryManager.Get<CommonBrowsePage>()
                .GetFirstResultValueOfField("ID");
            PageFactoryManager.Get<SalesInvoiceDetailPage>()
                .SelectFirstUninvoicedItem()
                .IsOnSaleInvoiceDetailPage()
                .SleepTimeInSeconds(3)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceDetailPage>()
                .ClickOnLinesTab();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValueInTab("ID", invoiceId)
                .CloseCurrentWindow()
                .SwitchToLastWindow<AccountStatementPage>()
                .ClickCreateInvoiceItem()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                .SelectDepot(Contract.Commercial)
                .InputInfo(lineType, site, product, priceElement, quantity, price)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToLastWindow<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                .ClickDeleteItem()
                .ClickConfirmActionButton()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);


        }

        [Category("Contracts")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_313_Contact_Changes()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser6.UserName, AutoUser6.Password)
                .IsOnHomePage(AutoUser6);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Contacts")
                .SwitchNewIFrame();
            ContractListPage contractListPage = PageFactoryManager.Get<ContractListPage>();
            contractListPage.WaitForLoadingIconToDisappear();
            contractListPage.IsContractListExist();
            contractListPage.ClickOnElement(contractListPage.AddNewContractButton);
            contractListPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            CreateContractPage createContractPage = PageFactoryManager.Get<CreateContractPage>();
            createContractPage.SelectIndexFromDropDown(createContractPage.ContractSelect, 2);
            createContractPage.ClickOnElement(createContractPage.NextButton);
            createContractPage.WaitForLoadingIconToDisappear();
            createContractPage.WaitForLoadingIconToDisappear();

            CreatePartyContactPage createPartyContactPage = PageFactoryManager.Get<CreatePartyContactPage>();
            ContactModel contactModel = new ContactModel();
            contactModel.Title = "Test Contact";
            contactModel.FirstName = "First Contact Name";
            contactModel.LastName = "Last Contact Name";
            contactModel.Mobile = "+4421234567";
            contactModel.Position = "";
            createPartyContactPage.EnterContactInfo(contactModel)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Navigate to Contacts grid under the Contract for which the Party for which the contact was created
            createPartyContactPage.SwitchToFirstWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            contractListPage.VerifyNewContact(contactModel)
                .SwitchToChildWindow(2);

            //Navigate to the Contact you added. Edit soem fields. Save.
            contactModel.Title = "Test Contact 11";
            contactModel.Position = "New Position";
            createPartyContactPage.EnterContactInfo(contactModel)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createPartyContactPage.SwitchToFirstWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            contractListPage.VerifyNewContact(contactModel);
        }
    }
}
