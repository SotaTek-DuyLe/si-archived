using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyVehiclePage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyWBTicketPage;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.WB;
using si_automated_tests.Source.Main.Pages.WB.Sites;
using si_automated_tests.Source.Main.Pages.WB.Tickets;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.WeighbridgeTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class WeighbridgeTests : BaseTest
    {
        private readonly string address = "Twickenham";
        private readonly string siteName45 = "Site Twickenham 45" + CommonUtil.GetRandomNumber(5);
        private string addressAdded45;
        private readonly string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(5);
        private readonly string siteName = CommonConstants.WBSiteName;
        private string addressAdded;
        private List<SiteModel> allSiteModel = new List<SiteModel>();
        private List<SiteModel> siteModelBefore = new List<SiteModel>();
        private List<SiteModel> siteModel045;
        private string partyIdCustomer, partyIdHaulier;
        private string partyName045 = "Auto045Customer" + CommonUtil.GetRandomNumber(5);
        private string partyName047 = "Auto047Haulier" + CommonUtil.GetRandomNumber(5);
        private string resourceName;
        private string stationNameTC48 = "AutoStation" + CommonUtil.GetRandomNumber(4);
        private string locationNameActive = "Location52WBActive" + CommonUtil.GetRandomNumber(3);
        private string licenceNumberHaulier = CommonUtil.GetRandomNumber(5);
        private string licenceExpHaulier = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
        private string product = "Food";

        public override void Setup()
        {
            base.Setup();
            Login();
        }

        public void Login()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser9.UserName, AutoUser9.Password)
                .IsOnHomePage(AutoUser9);
        }

        [Category("WB")]
        [Test(Description = "WB Site location delete")]
        [Category("Chang")]
        public void TC_055_261_Step_3_WB_Site_location_delete()
        {
            string partyNameCustomer = "Auto55Customer" + CommonUtil.GetRandomString(2);
            string partyNameHaulier = "Auto055Haulier" + CommonUtil.GetRandomString(2);
            string siteName55 = "Site Twickenham 55" + CommonUtil.GetRandomNumber(4);
            string stationNameTC55 = "AutoStation" + CommonUtil.GetRandomNumber(4);
            resourceName = "Auto55 WB Van" + CommonUtil.GetRandomNumber(2);
            string locationNameActive = "Location55WBActive" + CommonUtil.GetRandomNumber(2);
            string resourceType = "Van";
            string clientRef = "ClientRef55" + CommonUtil.GetRandomNumber(2);
            string product = "Food";
            string ticketType = "Incoming";
            string businessUnit = "Collections - Recycling";

            //Create new Resource with type = Van in TC51
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(businessUnit)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveResourceSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //TC45+48+51

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Create new party Haulier TC047
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameHaulier)
                .SelectPartyType(2)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();

            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            //Add all info for party haulier
            detailPartyPage
                .ClickWBSettingTab()
                .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
                .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -1))
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(siteName55)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                //Internal flag checked
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Create new party Customer TC045
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameCustomer)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
               .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer);
            detailPartyPage
                .ClickOnDetailsTab()
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
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(siteName55)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                //Internal flag checked
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage);
            //Create new Vehicle
            detailPartyPage
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyTableDisplayedInVehicle()
                .ClickAddNewVehicleBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AddVehiclePage>()
                .IsCreateVehicleCustomerHaulierPage()
                .InputResourceName(resourceName)
                .SelectResourceName(resourceName)
                .InputHaulierName(partyNameHaulier)
                //Input haulier name in TC47
                .SelectHaulierName(partyNameHaulier)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Create new station in TC048
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName55 + " / " + addressAdded45)
                .ClickStationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .ClickAddNewStationItem()
                .SwitchToLastWindow();
            CreateStationPage createStationPage = PageFactoryManager.Get<CreateStationPage>();
            createStationPage
                .WaitForLoadingIconToDisappear();
            createStationPage
                .WaitForCreateStationPageLoaded("WEIGHBRIDGE STATION")
                .IsCreateStationPage()
                .SelectTimezone("Europe/London")
                .InputName(stationNameTC55)
                .SelectDefaultTicket(ticketType)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            createStationPage
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            //TC54: Create new product in Product tab
            siteDetailPage
                .ClickProductTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInProductTabGrid()
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            AddProductPage addProductPage = PageFactoryManager.Get<AddProductPage>();
            //Bug
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
            //Select any product
                .ClickAnyProduct(product)
            //Select any ticket Type
                .ClickAnyTicketType(ticketType)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //TC52: Create new active location
            siteDetailPage
                .ClickOnLocationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInGrid()
                .ClickAddNewLocationItem()
                .SwitchToLastWindow();
            AddLocationPage addLocationPage = PageFactoryManager.Get<AddLocationPage>();
            addLocationPage
                .WaitForAddLocationPageLoaded()
                .VerifyDisplayPartySitePage()
                .InputName(locationNameActive)
                .SelectActiveCheckbox()
                .InputClientName(clientRef)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            addLocationPage
                .VerifyActiveCheckboxSelected()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();

            //TC55: Navigate to Location tab to delete location
            siteDetailPage
                .ClickOnLocationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .ClickFirstSelectAndDeSelectLocatorRow()
                .ClickDeleteItemInLocationTabBtn()
                .SwitchToChildWindow(4);
            DeleteWBLocation deleteWBLocation = PageFactoryManager.Get<DeleteWBLocation>();
            deleteWBLocation
                .IsWarningPopupDisplayed()
                .ClickYesBtn();
            deleteWBLocation
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //TC55: Click WB Ticket tab and verify
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage
                .ClickAddNewTicketBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            CreateNewTicketPage createNewTicketPage = PageFactoryManager.Get<CreateNewTicketPage>();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC55)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(resourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Verify vehicle type field displayed
                .VerifyDisplayVehicleType("Van")
                //Verify Ticket Type field displayed
                .VerifyDisplayTicketTypeInput()
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
               //Select Ticket Type is the same with TicketType of the product
               .ClickAnyTicketType(ticketType)
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.HaulierLicenceNumberHasExpiredMessage);
            //Add ticket line
            createNewTicketPage
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product)
                //Verify Location
                .VerifyNoLocationIsPrepolulated()
                .VerifyLocationDeletedNotDisplay(locationNameActive);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB create party customer"), Order(1)]
        public void GetAllSiteInWBBefore()
        {
            //Verify data in TC45, 46, 47 not apprear in WB Site
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Sites")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            SiteListingPage siteListingPage = PageFactoryManager.Get<SiteListingPage>();
            siteModelBefore = siteListingPage
                .GetAllSiteDisplayed();
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB create party customer"), Order(2)]
        public void TC_045_WB_Create_party_customer()
        {
            //Create new party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyName045)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .waitForLoadingIconDisappear();
            //PageFactoryManager.Get<CreatePartyPage>()
            //    .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName045);
            //Get id
            partyIdCustomer = detailPartyPage
                .GetPartyId();
            detailPartyPage
                .ClickOnDetailsTab()
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(siteName45)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
                //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Internal flag checked
            detailPartyPage
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Navigate to Site page
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            siteModel045 = detailPartyPage
                .GetAllSiteInList();
            allSiteModel.Add(siteModel045[0]);
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName45 + " / " + addressAdded45)
                .VerifyDisplayAllTab(CommonConstants.AllSiteTabCase46)
                .ClickDetailTab()
                .ClickSomeTabAndVerifyNoErrorMessage()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyWBSettingTab();
        }


        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB create party customer and haulier"), Order(3)]
        public void TC_046_WB_Create_party_customer_and_haulier()
        {
            string partyNameTC046 = "Auto" + CommonUtil.GetRandomString(2);
            //Create new party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameTC046)
                .SelectPartyType(1)
                .SelectPartyType(2)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameTC046);
            detailPartyPage
                .ClickSaveBtn();
            detailPartyPage
                //.VerifyDisplayGreenBoderInLicenceNumberExField()
                .VerifyDisplayYellowMesInLicenceNumberExField()
                .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayGreenBoderInLicenceNumberField()
                .VerifyDisplayYellowMesInLicenceNumberField()
                .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
                .ClickSaveBtn();
            detailPartyPage
                .VerifyDisplayMesInInvoiceAddressField()
                .ClickOnAddInvoiceAddressBtn()
                .SwitchToChildWindow(3);
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            addressAdded = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage
                .SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickCorresspondenAddress()
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .SelectCreatedAddressInCorresspondenceAddress(addressAdded)
                .VerifyAddressIsFilledAtInvoiceAddress(addressAdded)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage)
            //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SavePartySuccessMessage);
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            List<SiteModel> siteModel = detailPartyPage
                .GetAllSiteInList();
            allSiteModel.Add(siteModel[0]);
        }

        //This TC depends on TC-45
        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB create party haulier"), Order(4)]
        public void TC_047_WB_Create_party_haulier()
        {
            //Create new party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyName047)
                .SelectPartyType(2)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForLoadingIconToDisappear();
            detailPartyPage
               .WaitForDetailPartyPageLoadedSuccessfully(partyName047);
            partyIdHaulier = detailPartyPage
                .GetPartyId();
            detailPartyPage
                .ClickSaveBtn();
            detailPartyPage
                .VerifyDisplayYellowMesInLicenceNumberExField()
                .VerifyForcusOnLicenceNumberExField()
                .VerifyDisplayGreenBoderInLicenceNumberExField()
                .InputLienceNumberExField(licenceExpHaulier)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayYellowMesInLicenceNumberField()
                .VerifyForcusOnLicenceNumberField()
                .VerifyDisplayGreenBoderInLicenceNumberField()
                .ClickDownloadBtnAndVerify()
                //Input LicenceNumber
                .InputLicenceNumber(licenceNumberHaulier)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayMesInCorresspondenAddressField()
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            //Test 7
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            string addressAdded = createEditSiteAddressPage.SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .ClickCorresspondenAddress()
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .SelectCreatedAddressInCorresspondenceAddress(addressAdded)
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            List<SiteModel> siteModel = detailPartyPage
                .GetAllSiteInList();
            allSiteModel.Add(siteModel[0]);
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToChildWindow(3);
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, addressSite1 + " / " + addressAdded)
                .VerifyDisplayAllTab(CommonConstants.AllSiteTabCase47)
                .ClickDetailTab()
                .ClickSomeTabAndVerifyNoErrorMessage()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
        }

        //This TC depends on the TC-045, TC-046 and TC-047
        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB Station"), Order(5)]
        public void TC_048_WB_Station()
        {
            //Verify data in TC45, 46, 47 not apprear in WB Site
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Sites")
                .SwitchNewIFrame();
            SiteListingPage siteListingPage = PageFactoryManager.Get<SiteListingPage>();
            List<SiteModel> siteModelsAfter = siteListingPage
                .GetAllSiteDisplayed();
            siteListingPage
                .VerifySiteCreatedIsNotDisplayed(siteModelsAfter, allSiteModel, siteModelBefore)
            //Back to the party customer in TC045
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(Int32.Parse(partyIdCustomer))
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName045)
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName45 + " / " + addressAdded45)
                //Station tab
                .ClickStationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .ClickAddNewStationItem()
                .SwitchToLastWindow();
            CreateStationPage createStationPage = PageFactoryManager.Get<CreateStationPage>();
            createStationPage
                .WaitForLoadingIconToDisappear();
            createStationPage
                .WaitForCreateStationPageLoaded("WEIGHBRIDGE STATION")
                .IsCreateStationPage()
                .ClickSaveBtn();
            createStationPage
                .VerifyDisplayErrorMesMissingTimezone()
                .SelectTimezone("Europe/London")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.NameRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.NameRequiredMessage);
            //Missing message error name input => Fixed
            createStationPage
                .InputName(stationNameTC48)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createStationPage
                .SelectDefaultTicket("Incoming")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickStationTab()
                .WaitForLoadingIconToDisappear();
            //Get siteID
            string siteID = siteDetailPage
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/sites/", "");
            siteDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Sites")
                .SwitchNewIFrame();
            siteListingPage
                .FilterSiteById(siteID)
                .WaitForLoadingIconToDisappear();
            List<SiteModel> siteModelsNew = siteListingPage
                .GetAllSiteDisplayed();
            siteListingPage
                .VerifyDisplayNewSite(siteModel045[0], siteModelsNew[0]);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB VCH Human"), Order(6)]
        public void TC_050_WB_VCH_Human()
        {
            string resourceName = "Auto WB50 " + CommonUtil.GetRandomNumber(2);
            string resourceType = "Driver";
            string businessUnit = "Collections - Recycling";

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(businessUnit)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Vehicle_Customer_Haulier")
                .SwitchNewIFrame();
            PageFactoryManager.Get<VehicleCustomerHaulierPage>()
                .VerifyVehicleCustomerHaulierPageDisplayed()
                .ClickAddNewItemBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreateVehicleCustomerHaulierPage>()
                .IsCreateVehicleCustomerHaulierPage()
                .VerifyDefaultMandatoryField()
                //Input party customer from TC045
                .InputCustomer(partyName045)
                //Input party haulier from TC047
                .InputHaulier(partyName047)
                //Input human resource name
                .InputHumanResourceName(resourceName)
                //Verify not display suggestion
                .VerifyNotDisplaySuggestionInResourceInput()
                .ClickSaveBtn();
            PageFactoryManager.Get<CreateVehicleCustomerHaulierPage>()
                .VerifyDisplayResourceRequiredMessage();

        }

        //This TC depends on the TC-45, TC-47
        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB VCH Vehicle"), Order(7)]
        public void TC_051_WB_VCH_Vehicle()
        {
            resourceName = "Auto WB Van" + CommonUtil.GetRandomNumber(2);
            string resourceType = "Van";
            string vehicleNotActiveName = "COM7 NST";
            string businessUnit = "Collections - Recycling";

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(businessUnit)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Navigate to party detail in TC045
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(Int32.Parse(partyIdCustomer))
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName045)
                //Create new Vehicle in Vehicles tab
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyTableDisplayedInVehicle()
                .ClickAddNewVehicleBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            AddVehiclePage addVehiclePage = PageFactoryManager.Get<AddVehiclePage>();
            addVehiclePage
                .IsCreateVehicleCustomerHaulierPage()
                .VerifyDefaultMandatoryFieldAndDefaultValue(partyName045)
                .ClickDefaultCustomerAddressDropdownAndVerify(addressAdded45)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageRequiredFieldConstants.ResourceRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.ResourceRequiredMessage);
            addVehiclePage
                .InputResourceName(vehicleNotActiveName)
                .VerifyNotDisplaySuggestionInResourceInput()
                .InputResourceName(resourceName)
                .SelectResourceName(resourceName)
                .InputHaulierName(partyName045)
                .VerifyNotDisplaySuggestionInHaulierInput()
                //Input haulier name in TC47
                .InputHaulierName(partyName047)
                .SelectHaulierName(partyName047)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBVCHRegistered)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Verify party customer
            List<VehicleModel> allVehiclePartyCustomer = detailPartyPage
                .GetAllVehicleModel();
            detailPartyPage
                .VerifyVehicleCreated(allVehiclePartyCustomer[0], resourceName, partyName045, partyName047, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonConstants.EndDateAgreement)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Verify party haulier
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(Int32.Parse(partyIdHaulier))
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName047)
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            List<VehicleModel> allVehiclePartyHaulier = detailPartyPage
                .GetAllVehicleModel();
            detailPartyPage
                .VerifyVehicleCreated(allVehiclePartyHaulier[0], resourceName, partyName045, partyName047, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonConstants.EndDateAgreement)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Verify in Vehicle_Customer_Haulier
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Vehicle_Customer_Haulier")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Filter vehicleID
            PageFactoryManager.Get<VehicleCustomerHaulierPage>()
                .FilterVehicleById(allVehiclePartyHaulier[0].Id);
            List<VehicleModel> allVehicleCustomerHaulier = PageFactoryManager.Get<VehicleCustomerHaulierPage>()
                .VerifyVehicleCustomerHaulierPageDisplayed()
                .GetAllVehicleModel();
            detailPartyPage
                .VerifyVehicleCreated(allVehicleCustomerHaulier[0], resourceName, partyName045, partyName047, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonConstants.EndDateAgreement);
        }

        //This TC depends on TC-45, TC-48
        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB Location"), Order(8)]
        public void TC_052_WB_Location()
        {
            string locationNameNotActive = "Location52WBNotActive" + CommonUtil.GetRandomNumber(2);
            string clientRef = "Client" + CommonUtil.GetRandomNumber(2);

            //Navigate to party detail in TC048
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(Int32.Parse(partyIdCustomer))
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName045)
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName45 + " / " + addressAdded45)
                .ClickOnLocationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInGrid()
                .ClickAddNewLocationItem()
                .SwitchToLastWindow();
            AddLocationPage addLocationPage = PageFactoryManager.Get<AddLocationPage>();
            addLocationPage
                .WaitForAddLocationPageLoaded()
                .VerifyDisplayPartySitePage()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageRequiredFieldConstants.NameRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.NameRequiredMessage);
            addLocationPage
                .InputName(locationNameNotActive)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickAddNewLocationItem()
                .SwitchToLastWindow();
            addLocationPage
                .WaitForAddLocationPageLoaded()
                .VerifyDisplayPartySitePage()
                .InputName(locationNameActive)
                .SelectActiveCheckbox()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            addLocationPage
                .VerifyActiveCheckboxSelected()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            //Verify in grid
            List<LocationModel> allModels = siteDetailPage
                .GetAllLocationInGrid();
            siteDetailPage
                .VerifyLocationCreated(allModels[0], locationNameActive, true, "")
                .VerifyLocationCreated(allModels[1], locationNameNotActive, false, "");
            siteDetailPage
                .ClickAddNewLocationItem()
                .SwitchToLastWindow();
            addLocationPage
                .WaitForAddLocationPageLoaded()
                .VerifyDisplayPartySitePage()
                .InputName(locationNameActive)
                .SelectActiveCheckbox()
                .InputClientName(clientRef)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            List<LocationModel> allModelsNew = siteDetailPage
                .GetAllLocationInGrid();
            siteDetailPage
                .VerifyLocationCreated(allModelsNew[0], locationNameActive, true, clientRef);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB Station No ticket type"), Order(9)]
        public void TC_053_WB_Station_No_ticket_type()
        {
            string stationName = "AutoStation" + CommonUtil.GetRandomNumber(2);
            //Back to the party customer in TC45
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(Int32.Parse(partyIdCustomer))
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName045)
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName45 + " / " + addressAdded45)
                .ClickStationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .ClickAddNewStationItem()
                .SwitchToLastWindow();
            CreateStationPage createStationPage = PageFactoryManager.Get<CreateStationPage>();
            createStationPage
                .WaitForLoadingIconToDisappear();
            createStationPage
                .WaitForCreateStationPageLoaded("WEIGHBRIDGE STATION")
                .IsCreateStationPage()
                .ClickSaveBtn();
            createStationPage
                .VerifyDisplayErrorMesMissingTimezone()
                .SelectTimezone("Europe/London")
                .InputName(stationName)
                .ClickSaveBtn()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage
                .ClickAddNewTicketBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            CreateNewTicketPage createNewTicketPage = PageFactoryManager.Get<CreateNewTicketPage>();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(resourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleType("Van")
                .VerifyDisplayTicketTypeInput();
        }


        //This TC depends on the TC-045 and TC-47, TC-048, TC-051, TC-052
        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB Site product 1"), Order(10)]
        public void TC_054_WB_Site_product_1()
        {
            string ticketType = "Incoming";
            string neutralTicketType = "Neutral";
            string outboundTicketType = "Outbound";
            string neutralProduct = "Glass";
            string outboundProduct = "General Recycling";

            //Find party - Customer: TC045
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(Int32.Parse(partyIdCustomer))
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName045)
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName45 + " / " + addressAdded45)
                //==> Create new product TC54 - ticketType = Incomming
                .ClickProductTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInProductTabGrid()
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            AddProductPage addProductPage = PageFactoryManager.Get<AddProductPage>();
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageRequiredFieldConstants.ProductRequiredMessage);
            //Select any product
            addProductPage
                .ClickAnyProduct(product)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageRequiredFieldConstants.TicketTypeRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.TicketTypeRequiredMessage);
            //Select any ticket Type
            addProductPage
                .ClickAnyTicketType(ticketType)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            //==> Add new product with ticketType = Neutral
            siteDetailPage
                .ClickProductTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInProductTabGrid()
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
                //Select any product
                .ClickAnyProduct(neutralProduct)
                //Select any ticket Type
                .ClickAnyTicketType(neutralTicketType)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            //==> Add new product with ticketType = Outbound
            siteDetailPage
                .ClickProductTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInProductTabGrid()
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
                //Select any product
                .ClickAnyProduct(outboundProduct)
                //Select any ticket Type
                .ClickAnyTicketType(outboundTicketType)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);

            siteDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Back to the WB ticket (same the TC053)
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage
                .ClickAddNewTicketBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            CreateNewTicketPage createNewTicketPage = PageFactoryManager.Get<CreateNewTicketPage>();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC48)
                .WaitForLoadingIconToDisappear();
            //Input resource name TC-051
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(resourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Verify vehicle type field displayed
                .VerifyDisplayVehicleType("Van")
                //Verify Ticket Type field displayed
                .VerifyDisplayTicketTypeInput()
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
                //Select Ticket Type is the same with TicketType of the product
                //===> TC063 - Select ticketType = Neutral and verify product displayed
                .ClickAnyTicketType(neutralTicketType)
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyName047)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(neutralProduct)
                .VerifyNoLocationIsPrepolulated()
                .VerifyActiveLocationIsDisplayed(locationNameActive)
                //===> TC063 - Select ticketType = Outbound and verify product displayed
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
                .ClickAnyTicketType(outboundTicketType)
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyName047)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(outboundProduct)
                .VerifyNoLocationIsPrepolulated()
                .VerifyActiveLocationIsDisplayed(locationNameActive)
                //===> TC063 - Select ticketType = Incoming and verify product displayed
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
                .ClickAnyTicketType(ticketType)
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyName047)
                .WaitForLoadingIconToDisappear();
            //Add ticket line
            createNewTicketPage
                .InputLicenceNumberExpDate()
                .InputLicenceNumber(licenceNumberHaulier)
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product)
                //Verify Location
                .VerifyNoLocationIsPrepolulated()
                .VerifyActiveLocationIsDisplayed(locationNameActive)
                //Mandatory field remaining
                .InputFirstWeight(2)
                .InputFirstDate()
                .InputSecondDate()
                .InputSecondWeight(1)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
        }

        //BUG
        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "The Weighbridge setting is not recorded in party actions (bug fix)")]
        public void TC_177_The_Weighbridge_setting_is_not_recorded_in_party_actions()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            string partyId = "1122";
            string userId = "51";
            string partyName = "The Mitre";
            string restrictedSite = "Kingston Tip - 20 Chapel Mill Road, Kingston upon Thames, KT1 3GZ";
            string licenceNumber = CommonUtil.GetRandomNumber(5);
            string licenceNumberExp = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            string dormanceDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 3);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Change some fields in tab
            detailPartyPage
                //Change [Auto-print Weighbridge Ticket] - Before: Ticked
                .ClickOnAutoPrintTickedCheckbox();
            //Change [purchase order number required] - Before: UnTicked
            detailPartyPage
                //.ClickOnPurchaseOrderNumberRequiredCheckbox()
                //Change [driver name required] - Before: UnTicked
                .ClickOnDriverNameRequiredCheckbox()
                //Change [use stored purchase order number] - Before: UnTicked
                .ClickOnUseStorePoNumberCheckbox()
                //Change [allow manual purchase order number,] - Before: Ticked
                .ClickOnAllowManualPoNumberCheckbox()
                //Change [external round code required] - Before: UnTicked
                .ClickOnExternalRoundCodeRequiredCheckbox()
                //Change [use stored external round code] - Before: UnTicked
                .ClickOnUseStoredExternalRoundCodeRequiredCheckbox()
                //Change [allow manual external round code] - Before: Ticked
                .ClickOnAllowManualExternalRoundCodeCheckbox()
                //Change [allow manual name entry] - Before: UnTicked
                .ClickOnAllowManualNameEntryCheckbox()
                //Change [Restrict Products] - Before: UnTicked
                .ClickOnRestrictProductsCheckbox()
                //Select [Authorise Tipping] - Before [Do Not Override On Stop]
                .SelectAnyOptionAuthoriseTipping("Never Allow Tipping")
                //Select [Restricted Sites]
                .SelectAnyOptionRestrictedSites(restrictedSite)
                //Input [Licence Number]
                .InputLicenceNumber(licenceNumber)
                //Input [Licence Number Expiry]
                .InputLienceNumberExField(licenceNumberExp)
                //Input [Domain Date]
                .InputDormantDate(dormanceDate)
                //Clear [Warning Limit £]
                .ClearTextInWarningLimit()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Click on [History] tab and verify
            detailPartyPage
                .ClickOnHistoryTab()
                .ClickRefreshBtn();
            string[] valueChangedExp = { ".", "NO.", "YES.", "YES.", "NO.", "YES.", "YES.", "NO.", "YES.", "NO.", licenceNumber + ".", licenceNumberExp + " 00:00.", dormanceDate + " 00:00.", "YES." };
            detailPartyPage
                .VerifyInfoInHistoryTab(CommonConstants.HistoryTitleAfterUpdateWBTicketTab, valueChangedExp, AutoUser9.DisplayName)
                .VerifyRestrictedSite("Kingston Tip.");

            //API to verify
            List<PartyActionDBModel> list = commonFinder.GetPartyActionByPartyIdAndUserId(partyId, userId);
            PartyActionDBModel partyActionDBModel = list[1];
            Assert.AreEqual(licenceNumber, partyActionDBModel.wb_licencenumber, "Licence number is incorrect");
            Assert.IsFalse(partyActionDBModel.wb_autoprint, "Auto-print is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_driverrequired, "Driver Name Required is incorrect");
            //Assert.IsTrue(partyActionDBModel.wb_driverrequired, "Purchase Order Number Required is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_usestoredpo, "Use Stored Purchase Order Number is incorrect");
            Assert.IsFalse(partyActionDBModel.wb_usemanualpo, "Allow Manual Purchase Order Number is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_externalroundrequired, "External Round Code Required is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_usestoredround, "Use Stored External Round Code is incorrect");
            Assert.IsFalse(partyActionDBModel.wb_usemanualround, "Allow Manual External Round Code is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_allowmanualname, "Allow Manual Name Entry is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_restrictproducts, "Restrict Products is incorrect");
            Assert.AreEqual(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 2), partyActionDBModel.wb_licencenumberexpiry.ToString(CommonConstants.DATE_MM_DD_YYYY_FORMAT), "Licence Number Expiry is incorrect");
            Assert.AreEqual(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 3), partyActionDBModel.wb_dormantdate.ToString(CommonConstants.DATE_MM_DD_YYYY_FORMAT), "Dormant Date is incorrect");
            Assert.AreEqual(null, partyActionDBModel.wb_creditlimitwarning, "Warning Limit £ is incorrect");
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Customer specific weighbridge settings")]
        public void TC_261_Customer_specific_weighbridge_settings()
        {
            //Seting WB
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.WBSettingUrlIE);
            PageFactoryManager.Get<WBActiveIEPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<WBActiveIEPage>()
                .IsWBActivePage()
                //Step line 9: Set [WB Active] to false
                .InputSettingValue("False")
                .ClickOnSaveWBSetting()
                .WaitForLoadingIconToDisappear();
            //Step line 10: Verify [WB Settings] tab is not displayed in [Party detail]
            PageFactoryManager.Get<BasePage>()
                .SleepTimeInSeconds(5)
                .GoToURL(WebUrl.MainPageUrl + "web/parties/1085");
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully("Richmond upon Thames College")
                .VerifyWBSettingTabIsNotDisplayed();
            //Step line 11: Set [WB Active] is true
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.WBSettingUrlIE);
            PageFactoryManager.Get<WBActiveIEPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<WBActiveIEPage>()
                .IsWBActivePage()
                .InputSettingValue("True")
                .ClickOnSaveWBSetting()
                .WaitForLoadingIconToDisappear();
            //Step line 12: Verify [WB Settings] tab is displayed in [Party detail]
            PageFactoryManager.Get<BasePage>()
                .SleepTimeInSeconds(3)
                .GoToURL(WebUrl.MainPageUrl + "web/parties/1085");
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<DetailPartyPage>()
                .Refresh();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully("Richmond upon Thames College")
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Step line 13: Click on [WB Settings] and verify the correct settings
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyWBSettingTab();
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Restricted sites")]
        public void TC_261_Restricted_sites()
        {
            string partyNameCustomerAndHaulier = "PartyCustomerAndHaulierTC_2612";
            string restrictedSiteValue = "Kingston Tip - 20 Chapel Mill Road, Kingston upon Thames, KT1 3GZ";

            //Step line 8: Verify that if party is created, restricted sites will be blank
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameCustomerAndHaulier)
                .SelectPartyType(1)
                .SelectPartyType(2)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomerAndHaulier);
            detailPartyPage
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyRestrictedSitesIsBlank();
            //Step line 10: Select one value from [Restricted Sites]
            detailPartyPage
                .SelectAnyOptionRestrictedSites(restrictedSiteValue)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //Step line 11: Click on [WB tickets tab] and Click on Add new item
            //detailPartyPage
            //    .ClickWBTicketTab()
            //    .WaitForLoadingIconToDisappear();
            //detailPartyPage
            //    .ClickAddNewWBTicketBtn();
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Licence number & expiry"), Order(13)]
        public void TC_261_Licence_number_expiry_Step_1()
        {
            string partyNameHaulier = "TC261_PartyHaulier" + CommonUtil.GetRandomNumber(5);
            string partyNameHaulierCustomer = "TC261_PartyHaulierCustomer" + CommonUtil.GetRandomNumber(5);
            string accountTypeName = "Charity";
            string licenceNumberValue = CommonUtil.GetRandomNumber(5);
            PartySiteAddressPage partySiteAddressPage = new PartySiteAddressPage();

            //Verify that Licence number and Licence number expiry are mandatory for party Haulier
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameHaulier)
                .SelectPartyType(2)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .SelectAnyAccountType(accountTypeName)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.LicenceNumberExIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.LicenceNumberExIsRequiredMessage);
            detailPartyPage
                .InputLienceNumberExField(CommonConstants.FUTURE_END_DATE)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.LicenceNumberIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.LicenceNumberIsRequiredMessage);
            //Step line 10: Input [Licence number] and save
            detailPartyPage
                .InputLicenceNumber(licenceNumberValue)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.CorrespondenceAddressRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.CorrespondenceAddressRequiredMessage);
            //Step line 11: Add [Correspondence address]
            detailPartyPage
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
            PageFactoryManager.Get<CreatePartyPage>()
                .WaitForLoadingIconToDisappear();
            string addressAdded = PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .SelectRandomSiteAddress();
            PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
                .SelectCreatedAddress(addressAdded)
                .VerifySelectedAddressOnInvoicePage(address)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 12: Create new Party Haulier and Customer
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameHaulierCustomer)
                .SelectPartyType(2)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .SelectAnyAccountType(accountTypeName)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.LicenceNumberExIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.LicenceNumberExIsRequiredMessage);
            //Step line 13: Input [Licence number expiry]
            detailPartyPage
                .InputLienceNumberExField(CommonConstants.FUTURE_END_DATE)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.LicenceNumberIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.LicenceNumberIsRequiredMessage);
            //Step line 14: Click on icon next to Licence number field
            detailPartyPage
                .ClickDownloadBtnAndVerify()
                //Input LicenceNumber
                .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayMesInCorresspondenAddressField()
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
            PageFactoryManager.Get<CreatePartyPage>()
                .WaitForLoadingIconToDisappear();
            addressAdded = PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .SelectRandomSiteAddress();
            PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
                .SelectCreatedAddress(addressAdded)
                .VerifySelectedAddressOnInvoicePage(address)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 1: Create new Party Customer
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameHaulierCustomer)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .SelectAnyAccountType(accountTypeName)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.CorrespondenceAddressRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.CorrespondenceAddressRequiredMessage);
            //Step line 18: Add [Correspondence address]
            detailPartyPage
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
            PageFactoryManager.Get<CreatePartyPage>()
                .WaitForLoadingIconToDisappear();
            addressAdded = PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .SelectRandomSiteAddress();
            PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
                .SelectCreatedAddress(addressAdded)
                .VerifySelectedAddressOnInvoicePage(address)
                .ClickSaveBtn();

        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Licence number & expiry"), Order(14)]
        public void TC_261_Licence_number_expiry_Step_2()
        {
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage
                .ClickAddNewTicketBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            CreateNewTicketPage createNewTicketPage = PageFactoryManager.Get<CreateNewTicketPage>();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC48)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(resourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Verify vehicle type field displayed
                .VerifyDisplayVehicleType("Van")
                //Verify Ticket Type field displayed
                .VerifyDisplayTicketTypeInput()
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
               //Select Ticket Type is the same with TicketType of the product
               .ClickAnyTicketType("Incoming")
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyName047)
                .WaitForLoadingIconToDisappear();
            //Verify [Haulier Licence Number] and [Haulier Licence Number Expiry] populated
            createNewTicketPage
                .VerifyValueInLicenceNumber(licenceNumberHaulier)
                .VerifyValueInLicenceNumberExp(licenceExpHaulier);
            //Add ticket line
            createNewTicketPage
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product)
                //Input Net quantity
                .InputNetQuantity("12")
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup();
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        }


        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Licence number & expiry"), Order(15)]
        public void TC_261_Licence_number_expiry_Step_4()
        {
            string partyNameCustomer = "Auto261_Step4Customer" + CommonUtil.GetRandomString(2);
            string partyNameHaulier = "Auto261_Step4Haulier" + CommonUtil.GetRandomString(2);
            string siteName55 = "Site Twickenham 261_Step4_" + CommonUtil.GetRandomNumber(4);
            string stationNameTC55 = "AutoStation261_Step4_" + CommonUtil.GetRandomNumber(4);
            resourceName = "Auto261_Step4 WB Van" + CommonUtil.GetRandomNumber(4);
            string locationNameActive = "Location261_Step4WBActive" + CommonUtil.GetRandomNumber(2);
            string resourceType = "Van";
            string clientRef = "ClientRef261_Step4" + CommonUtil.GetRandomNumber(2);
            string product = "Food";
            string ticketType = "Incoming";
            string businessUnit = "Collections - Recycling";

            //Create new Resource with type = Van in TC51
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(businessUnit)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveResourceSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //TC45+48+51

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //Create new party Haulier TC047
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameHaulier)
                .SelectPartyType(2)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();

            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            //Add all info for party haulier
            detailPartyPage
                .ClickWBSettingTab()
                .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
                .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -1))
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(siteName55)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            partyIdHaulier = detailPartyPage
                .GetPartyId();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                //Internal flag checked
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Create new party Customer TC045
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameCustomer)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickOnDetailsTab()
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
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(siteName55)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                //Internal flag checked
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage);
            //Create new Vehicle
            detailPartyPage
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyTableDisplayedInVehicle()
                .ClickAddNewVehicleBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AddVehiclePage>()
                .IsCreateVehicleCustomerHaulierPage()
                .InputResourceName(resourceName)
                .SelectResourceName(resourceName)
                .InputHaulierName(partyNameHaulier)
                //Input haulier name in TC47
                .SelectHaulierName(partyNameHaulier)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Create new station in TC048
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName55 + " / " + addressAdded45)
                .ClickStationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .ClickAddNewStationItem()
                .SwitchToLastWindow();
            CreateStationPage createStationPage = PageFactoryManager.Get<CreateStationPage>();
            createStationPage
                .WaitForLoadingIconToDisappear();
            createStationPage
                .WaitForCreateStationPageLoaded("WEIGHBRIDGE STATION")
                .IsCreateStationPage()
                .SelectTimezone("Europe/London")
                .InputName(stationNameTC55)
                .SelectDefaultTicket(ticketType)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            createStationPage
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            //TC54: Create new product in Product tab
            siteDetailPage
                .ClickProductTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInProductTabGrid()
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            AddProductPage addProductPage = PageFactoryManager.Get<AddProductPage>();
            //Bug
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
            //Select any product
                .ClickAnyProduct(product)
            //Select any ticket Type
                .ClickAnyTicketType(ticketType)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //TC52: Create new active location
            siteDetailPage
                .ClickOnLocationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInGrid()
                .ClickAddNewLocationItem()
                .SwitchToLastWindow();
            AddLocationPage addLocationPage = PageFactoryManager.Get<AddLocationPage>();
            addLocationPage
                .WaitForAddLocationPageLoaded()
                .VerifyDisplayPartySitePage()
                .InputName(locationNameActive)
                .SelectActiveCheckbox()
                .InputClientName(clientRef)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            addLocationPage
                .VerifyActiveCheckboxSelected()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //TC55: Click WB Ticket tab and verify
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage
                .ClickAddNewTicketBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            CreateNewTicketPage createNewTicketPage = PageFactoryManager.Get<CreateNewTicketPage>();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC55)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(resourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Verify vehicle type field displayed
                .VerifyDisplayVehicleType("Van")
                //Verify Ticket Type field displayed
                .VerifyDisplayTicketTypeInput()
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
               //Select Ticket Type is the same with TicketType of the product
               .ClickAnyTicketType(ticketType)
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.HaulierLicenceNumberHasExpiredMessage);
            //Step line 30: Change Haulier licence number and Licence number epx
            string newLicenceNumber = "1" + CommonUtil.GetRandomNumber(5);
            string newLicenceNumberExp = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 5);
            createNewTicketPage
                .InputLicenceNumber(newLicenceNumber)
                .InputLicenceNumberExpDate(newLicenceNumberExp)
                //Add ticket line
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product)
                //Verify Location
                .InputFirstWeight(2)
                .InputFirstDate()
                .InputSecondDate()
                .InputSecondWeight(1)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createNewTicketPage
                .ClickOnNoWarningPopup();
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Back to the Party haulier > WB Settings tab and verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //Filter Party haulier by id
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyIdHaulier)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameHaulier)
                .ClickWBSettingTab()
                .VerifyValueAtLicenceNumber(newLicenceNumber)
                .VerifyValueAtLicenceNumberExp(newLicenceNumberExp);
            CommonFinder com = new CommonFinder(DbContext);
            //API Quert to check
            List<WBPartySettingDBModel> wBPartySettingDBModels = com.GetWBPartySettingByPartyId(partyIdHaulier);
            Assert.AreEqual(newLicenceNumber, wBPartySettingDBModels[0].licencenumber);
            Assert.AreEqual(newLicenceNumberExp, wBPartySettingDBModels[0].licencenumberexpiry.ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT));

        }

        [Category("WB")]
        [Category("TC_260")]
        [Category("Chang")]
        [Test(Description = "Verify that Grey list can be created"), Order(11)]
        public void TC_260_Grey_lists_Verify_greylist_can_be_created()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string firstResourceName = "1_TC260_" + CommonUtil.GetRandomNumber(5);
            string secondResourceName = "2_TC260_" + CommonUtil.GetRandomNumber(5);
            string businessUnit = "Collections - Recycling";
            string inactiveResource = "COM7 NST";
            string vehicleTypeName = "Van";
            string partyNameHaulier = "TC260_PartyHaulier" + CommonUtil.GetRandomNumber(4);
            string partyNameCustomer = "TC260_PartyCustomer" + CommonUtil.GetRandomNumber(4);
            string siteName260 = "Site Twickenham 260" + CommonUtil.GetRandomNumber(4);
            string product = "Armchair";
            string locationNameActive = "Location260WBActive" + CommonUtil.GetRandomNumber(2);
            string stationNameTC260 = "AutoStation260" + CommonUtil.GetRandomNumber(4);

            //Create new Resource with type = Van in TC51
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .SelectResourceType(vehicleTypeName)
                .SelectBusinessUnit(businessUnit)
                .InputResourceName(firstResourceName)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveResourceSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .SelectResourceType(vehicleTypeName)
                .SelectBusinessUnit(businessUnit)
                .InputResourceName(secondResourceName)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveResourceSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();

            //CREATE: Create party customer and party haulier
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            //Create new party Haulier TC047
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameHaulier)
                .SelectPartyType(2)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Create new party Customer TC045
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyNameCustomer)
                .SelectPartyType(1)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
               .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer);
            detailPartyPage
                .ClickOnDetailsTab();
            string partyCustomerId = detailPartyPage
                .GetPartyId();

            detailPartyPage
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage
                .SelectAddressClickNextBtn()
                .InsertSiteName(siteName260)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                //Internal flag checked
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage);
            //Create new Vehicle
            detailPartyPage
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyTableDisplayedInVehicle()
                .ClickAddNewVehicleBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AddVehiclePage>()
                .IsCreateVehicleCustomerHaulierPage()
                .InputResourceName(secondResourceName)
                .SelectResourceName(secondResourceName)
                .InputHaulierName(partyNameHaulier)
                //Input haulier name in TC47
                .SelectHaulierName(partyNameHaulier)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Create new station in TC048
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName260 + " / " + addressAdded45)
                .ClickStationTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .ClickAddNewStationItem()
                .SwitchToLastWindow();
            CreateStationPage createStationPage = PageFactoryManager.Get<CreateStationPage>();
            createStationPage
                .WaitForLoadingIconToDisappear();
            createStationPage
                .WaitForCreateStationPageLoaded("WEIGHBRIDGE STATION")
                .IsCreateStationPage()
                .SelectTimezone("Europe/London")
                .InputName(stationNameTC260)
                .SelectDefaultTicket("Incoming")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            createStationPage
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            //TC54: Create new product in Product tab
            siteDetailPage
                .ClickProductTab()
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .VerifyDisplayColumnInProductTabGrid()
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            AddProductPage addProductPage = PageFactoryManager.Get<AddProductPage>();
            //Bug
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
                //Select any product
                .ClickAnyProduct(product)
                //Select any ticket Type
                .ClickAnyTicketType("Incoming")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            //Create new ticket 
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage
                .ClickAddNewTicketBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            CreateNewTicketPage createNewTicketPage = PageFactoryManager.Get<CreateNewTicketPage>();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Verify vehicle type field displayed
                .VerifyDisplayVehicleType("Van")
                //Verify Ticket Type field displayed
                .VerifyDisplayTicketTypeInput()
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
               //Select Ticket Type is the same with TicketType of the product
               .ClickAnyTicketType("Incoming")
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            //Add ticket line
            createNewTicketPage
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product)
                //Input Net quantity
                .InputNetQuantity("12")
                //Input LicenceNumber
                .InputLicenceNumber()
                //Input Licence Number Exp
                .InputLicenceNumberExpDate()
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup();
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Get ticket number
            string ticketNumber = createNewTicketPage
                .GetTicketNumber();
            Console.WriteLine(ticketNumber);

            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //START: TEST for TC260
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Grey Lists")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<GreyListPage>()
                .IsGreyListPage()
                //Step line 11: [Add new item] btn
                .ClickOnAddNewItemBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                //Step line 12: Select a vehicle and [Save]
                .InputVehicle(firstResourceName)
                .SelectVehicleName(firstResourceName)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.GreylistCodeIsRequiredMessage);

            //API: Get [Grey list] code
            List<GreyListCodeDBModel> greyListCodeDBModels = commonFinder.GetGreyList();
            List<string> greyListDesc = greyListCodeDBModels.Select(p => p.description).ToList();

            //Step line 14: Input [Inactive resource] on Grey list form
            PageFactoryManager.Get<CreateGreyListPage>()
                .ClickOnGreylistCode()
                .InputValueInGreyListCode(inactiveResource)
                .VerifyNoResultMatchedGreylistCode(inactiveResource)
                //Step line 15: Click on [Greylist] code and verify codes
                .ClickOnGreylistCodeAndVerifyValueMatchingDB(greyListDesc)
                //Step line 16: Select one [Greylist] code and click [Save]
                .InputValueInGreyListCode(greyListDesc[0])
                .ClickOnAnyGreyListCode(greyListDesc[0])
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string lastUpdated = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            PageFactoryManager.Get<CreateGreyListPage>()
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInLastUpdatedBy(AutoUser9.DisplayName)
                .VerifyValueInLastUpdated(lastUpdated)
                //Step line 17: Change [Vehicle] value and click [Save]
                .InputVehicle(secondResourceName)
                .SelectVehicleName(secondResourceName)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string lastUpdatedAfter = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            PageFactoryManager.Get<CreateGreyListPage>()
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInLastUpdatedBy(AutoUser9.DisplayName)
                .VerifyValueInLastUpdated(lastUpdatedAfter)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();

            //Step line 18: Add new ticket
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .OpenOption("Tickets")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TicketListingPage>()
                .IsTicketListingPage()
                .ClickAddNewTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsGreylistCodeModel(secondResourceName, greyListDesc[0]);
            string greylistId = createNewTicketPage.GetGreylistCodeId();
            createNewTicketPage
                .ClickOnGreyListId()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                //Step line 20: Select [Customer] and [Haulier]
                .ClickAndSelectCustomer(partyNameCustomer)
                .ClickAndSelectHaulier(partyNameHaulier)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string updatedValue_2 = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            PageFactoryManager.Get<CreateGreyListPage>()
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInLastUpdatedBy(AutoUser9.DisplayName)
                .VerifyValueInLastUpdated(updatedValue_2)
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Step line 21: Click back on the Ticket and Refresh
            createNewTicketPage
                .ClickOnOkBtn()
                .WaitForPopupDisappear()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsGreylistCodeModel(secondResourceName, greyListDesc[0])
                //Step line 22: Click on the Grey list ID
                .ClickOnGreyListId()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            string comment = "Comment " + CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                //Step line 23: Select one [Visited site] and change [Greylist Code] > Add Comment
                .ClickAndSelectVisitedSite("Kingston Tip")
                .ClickOnGreylistCode()
                .InputValueInGreyListCode(greyListDesc[1])
                .ClickOnAnyGreyListCode(greyListDesc[1])
                .InputComment(comment)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInLastUpdatedBy(AutoUser9.DisplayName)
                .VerifyCommentValue(comment)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            createNewTicketPage
                .ClickOnOkBtn()
                .WaitForPopupDisappear()
                //Step line 24: Click back on the Ticket and Refresh the screen
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsGreylistCodeModel(secondResourceName, greyListDesc[1])
                //Step line 25: Click on the Grey list ID
                .ClickOnGreyListId()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                //Step line 26: Input [Start date] to tomorrow and Save
                .InputStartDate(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInLastUpdatedBy(AutoUser9.DisplayName)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            createNewTicketPage
                .ClickOnOkBtn()
                .WaitForPopupDisappear()
                //Step line 27: Click back on the ticket and Refresh the screen => Grey list popup is not displayed
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerityGreylistCodeModelIsNotDisplayed()
                .ClickCloseBtn()
                .AcceptAlert()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 28: Click back on the Grey list form and change start date to the past and End date yesterday
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .OpenOption("Grey Lists")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            string startDateInThePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -2);
            string endDateInThePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -1);
            PageFactoryManager.Get<GreyListPage>()
                .IsGreyListPage()
                .FilterGreylistCodeById(greylistId)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                .InputStartDate(startDateInThePast)
                .InputEndDate(endDateInThePast)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInLastUpdatedBy(AutoUser9.DisplayName)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 29: Click back on the Ticket and Refresh the screen and verify the grey list popup is not displayed
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .OpenOption("Tickets")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TicketListingPage>()
                .IsTicketListingPage()
                .ClickAddNewTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerityGreylistCodeModelIsNotDisplayed()
                .ClickCloseBtn()
                .AcceptAlert()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 30: Click on [Ticket] and select one ticket
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .OpenOption("Grey Lists")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<GreyListPage>()
                .IsGreyListPage()
                .FilterGreylistCodeById(greylistId)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                .ClickOnTicketDdAndVerify(new string[] { ticketNumber })
                //Step line 31: Select one ticket and verify value is populated
                .SelectOneTicket(ticketNumber)
                .WaitForLoadingIconToDisappear();
            string newComment = "New comment";
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInCustomer(partyNameCustomer)
                .VerifyValueInHaulier(partyNameHaulier)
                .VerifyValueInVisitedSite(siteName260)
                //Step line 32: Select a greylist code and add comment
                .ClickOnGreylistCode()
                .InputValueInGreyListCode(greyListDesc[2])
                .ClickOnAnyGreyListCode(greyListDesc[2])
                .InputComment(newComment)
                .InputEndDate(CommonConstants.FUTURE_END_DATE)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateGreyListPage>()
                .VerifyValueInLastUpdatedBy(AutoUser9.DisplayName)
                .VerifyCommentValue(newComment)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 33: Click back on the Ticket and refresh
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .OpenOption("Tickets")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TicketListingPage>()
                .IsTicketListingPage()
                .ClickAddNewTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsGreylistCodeModel(secondResourceName, greyListDesc[2])
                //Step line 34: Click on the Grey list ID
                .ClickOnGreyListId()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            createNewTicketPage
                .ClickOnOkBtn()
                .WaitForPopupDisappear()
                .ClickCloseBtn()
                .AcceptAlert()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 35: Create two tickets on Weighbridge Tickets
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyCustomerId)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBTicketTab()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsGreylistCodeModel(secondResourceName, greyListDesc[2])
                .ClickOnOkBtn()
                .WaitForPopupDisappear()
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .ClickAnySource(partyNameCustomer)
                .WaitForLoadingIconToDisappear();
            //Add ticket line
            createNewTicketPage
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product)
                //Input Net quantity
                .InputNetQuantity("12")
                //Input LicenceNumber
                .InputLicenceNumber()
                //Input Licence Number Exp
                .InputLicenceNumberExpDate()
                .ClickSaveBtn();
            createNewTicketPage
                //Step line 36: Click on [Yes] btn
                .ClickOnYesWarningPopup()
                .IsPayForThisTicketPopup()
                .VerifyAmountPaidValue("33.84")
                //Step line 37: Change [Amount paid] to ticket line will be underpaid and click [Pay]
                .InputAmountPaidValue("30")
                .ClickOnPayBtn()
                .IsWarningAmountPaidNotEqualToTicketPrice()
                //Step line 38: Click on [Yes] btn
                .ClickOnYesWarningAmountPaidPopupBtn()
                .IsGreyListCodePopup()
                .VerifyCommentValueGreyListCodePopup("WB-", "33.84", "30")
                //Step line 39: Select one [Greylist] code in dd
                .SelectOnGreylistCode(greyListDesc[1])
                .ClickOnSaveGreyListCodePopupBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string ticketNumberNew = createNewTicketPage
                .GetTicketNumber();
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 40: Go to WB
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .OpenOption("Grey Lists")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<GreyListPage>()
               .IsGreyListPage()
               .FilterGreylistCodeByTicket(ticketNumberNew)
               .SwitchToChildWindow(2)
               .WaitForLoadingIconToDisappear();
            string[] expComment = { ticketNumberNew, "33.84", "30" };
            PageFactoryManager.Get<CreateGreyListPage>()
                .IsCreateWBGreyListPage()
                .VerifyAllValueInWBGreylistDetail(secondResourceName, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonConstants.FUTURE_END_DATE, ticketNumberNew, partyNameCustomer, partyNameHaulier, siteName260, greyListDesc[1], expComment, AutoUser9.DisplayName, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchToDefaultContent();
            //Step line 41: Verify in Add new item WB ticket grid
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .OpenOption("Tickets")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TicketListingPage>()
                .IsTicketListingPage()
                .ClickAddNewTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsCreateNewTicketPage()
                .ClickStationDdAndSelectStation(stationNameTC260)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayVehicleRegInput()
                .InputVehicleRegInput(secondResourceName)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .IsGreylistCodeModel(secondResourceName, new string[] { greyListDesc[2], greyListDesc[1] });

        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Verify that it is possible to delete grey list record"), Order(12)]
        public void TC_260_Grey_lists_Verify_greylist_can_be_deleted()
        {
            string greylistCodeId = "1";
            CommonFinder commonFinder = new CommonFinder(DbContext);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Grey Lists")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<GreyListPage>()
                .IsGreyListPage()
                .FilterGreylistCodeByIdToDelete(greylistCodeId)
                .ClickOnDeleteBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<RemoveGreyListPage>()
                .IsRemoveGreyListPopup()
                //Step line 43: Click on [No] btn
                .ClickOnNoBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<GreyListPage>()
                .VerifyWindowClosed(1);
            //Step line 43: Click on [x] btn
            PageFactoryManager.Get<GreyListPage>()
                .ClickOnDeleteBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<RemoveGreyListPage>()
                .IsRemoveGreyListPopup()
                .ClickOnCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<GreyListPage>()
                .VerifyWindowClosed(1);
            //Step line 44: Click on [Yes] btn
            PageFactoryManager.Get<GreyListPage>()
                .ClickOnDeleteBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<RemoveGreyListPage>()
                .IsRemoveGreyListPopup()
                .ClickOnYesBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(1);
            PageFactoryManager.Get<GreyListPage>()
                .VerifyRecordNoLongerDisplayInGrid();
            //Step line 45: Run query to check
            List<WBGreylistDBModel> wBGreylistDBModels = commonFinder.GetGreyListById(greylistCodeId);
            Assert.AreEqual(0, wBGreylistDBModels.Count);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "WB ticket issues")]
        public void TC_161_WB_ticket_issues()
        {
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage
                .ClickAddNewTicketBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            CreateNewTicketPage createNewTicketPage = PageFactoryManager.Get<CreateNewTicketPage>();
            createNewTicketPage.IsCreateNewTicketPage()
                .SelectTextFromDropDown(createNewTicketPage.stationDd, "Townmead In Bridge")
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .InputVehicleRegInputAndClickOK("NS22 8GH")
                .WaitForLoadingIconToDisappear();
            createNewTicketPage.SelectTextFromDropDown(createNewTicketPage.haulierDd, "Waste Management Ltd")
                .WaitForLoadingIconToDisappear();
            createNewTicketPage.SelectTextFromDropDown(createNewTicketPage.SourcePartySelect, "GeoFossils")
                .WaitForLoadingIconToDisappear();
            createNewTicketPage.SendKeys(createNewTicketPage.PONumberInput, "1234");
            createNewTicketPage.ClickOnElement(createNewTicketPage.AddButton);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            (string firstDate, string secondDate) firstTicketLine = createNewTicketPage.InputTicketLineData(0, "General Recycling", "100", "80");
            createNewTicketPage.ClickOnElement(createNewTicketPage.AddButton);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            (string firstDate, string secondDate) secondTicketLine = createNewTicketPage.InputTicketLineData(1, "General Refuse", "80", "60");
            createNewTicketPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();

            //Click No
            createNewTicketPage.VerifyTakePayment()
                .ClickOnElement(createNewTicketPage.NoTakePaymentButton);
            createNewTicketPage.SleepTimeInMiliseconds(500);
            createNewTicketPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyTicketLineData(0, "General Recycling", "100", "80", firstTicketLine.firstDate, firstTicketLine.secondDate)
                .VerifyTicketLineData(1, "General Refuse", "80", "60", secondTicketLine.firstDate, secondTicketLine.secondDate);

            //Scroll down to the bottom of the page
            createNewTicketPage.ScrollDownToElement(createNewTicketPage.TakePaymentButton);
            createNewTicketPage.VerifyElementEnable(createNewTicketPage.TakePaymentButton, true)
                .VerifyElementEnable(createNewTicketPage.CancelTicketButton, true)
                .VerifyElementEnable(createNewTicketPage.DuplicateTicketButton, true)
                .VerifyElementEnable(createNewTicketPage.CopyToGreyListButton, true)
                .VerifyElementEnable(createNewTicketPage.MarkForCreditButton, false)
                .VerifyElementEnable(createNewTicketPage.UnmarkForCreditButton, false);

            //Click on take payment -> Click pay on the modal
            createNewTicketPage.ClickOnElement(createNewTicketPage.TakePaymentButton);
            createNewTicketPage.ClickOnElement(createNewTicketPage.PayButton);
            createNewTicketPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyElementText(createNewTicketPage.TicketState, "Paid");
            createNewTicketPage.VerifyElementEnable(createNewTicketPage.TakePaymentButton, false)
                .VerifyElementEnable(createNewTicketPage.CancelTicketButton, false)
                .VerifyElementEnable(createNewTicketPage.DuplicateTicketButton, true)
                .VerifyElementEnable(createNewTicketPage.CopyToGreyListButton, true)
                .VerifyElementEnable(createNewTicketPage.MarkForCreditButton, true)
                .VerifyElementEnable(createNewTicketPage.UnmarkForCreditButton, false);

            //Click on mark for credit
            createNewTicketPage.ClickOnElement(createNewTicketPage.MarkForCreditButton);
            createNewTicketPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyElementText(createNewTicketPage.TicketState, "Credited");
            createNewTicketPage.VerifyElementEnable(createNewTicketPage.TakePaymentButton, false)
                .VerifyElementEnable(createNewTicketPage.CancelTicketButton, false)
                .VerifyElementEnable(createNewTicketPage.DuplicateTicketButton, true)
                .VerifyElementEnable(createNewTicketPage.CopyToGreyListButton, true)
                .VerifyElementEnable(createNewTicketPage.MarkForCreditButton, false)
                .VerifyElementEnable(createNewTicketPage.UnmarkForCreditButton, true);

            //Click on Unmark from credit
            createNewTicketPage.ClickOnElement(createNewTicketPage.UnmarkForCreditButton);
            createNewTicketPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyElementText(createNewTicketPage.TicketState, "Paid");
            createNewTicketPage.VerifyElementEnable(createNewTicketPage.TakePaymentButton, false)
                .VerifyElementEnable(createNewTicketPage.CancelTicketButton, false)
                .VerifyElementEnable(createNewTicketPage.DuplicateTicketButton, true)
                .VerifyElementEnable(createNewTicketPage.CopyToGreyListButton, true)
                .VerifyElementEnable(createNewTicketPage.MarkForCreditButton, true)
                .VerifyElementEnable(createNewTicketPage.UnmarkForCreditButton, false);

            //Click on copy to Grey list -> Modal: select Grey list code in the dropdown and add the comment -> Save
            createNewTicketPage.ClickOnElement(createNewTicketPage.CopyToGreyListButton);
            createNewTicketPage.SleepTimeInMiliseconds(500);
            createNewTicketPage.SelectTextFromDropDown(createNewTicketPage.GreyListSelect, "MORE INFO REQUIRED")
                .SendKeys(createNewTicketPage.CommentInput, "test");
            createNewTicketPage.ClickOnElement(createNewTicketPage.SaveGreyListButton);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            string idTicket = createNewTicketPage.GetElementText(createNewTicketPage.IdTicket);
            createNewTicketPage.SwitchToFirstWindow()
                .SwitchNewIFrame();
            ticketListingPage.FilterTicketById(idTicket.AsInteger());
            ticketListingPage
                .WaitForLoadingIconToDisappear();
            ticketListingPage.SleepTimeInMiliseconds(3000);
            string number = ticketListingPage.GetFirstTicketNumber();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Weighbridge)
               .ExpandOption(Contract.Commercial)
               .OpenOption("Grey Lists")
               .SwitchNewIFrame();
            GreyListPage greyListPage = PageFactoryManager.Get<GreyListPage>();
            greyListPage.WaitForLoadingIconToDisappear();
            greyListPage.DoubleClickRow(number)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            GreyListDetailPage greyListDetailPage = PageFactoryManager.Get<GreyListDetailPage>();
            greyListDetailPage.VerifyElementText(greyListDetailPage.TicketInput, number, true)
                .VerifySelectedValue(greyListDetailPage.GreyListCodeSelect, "MORE INFO REQUIRED")
                .VerifyInputValue(greyListDetailPage.CommentInput, "test")
                .ClickCloseBtn()
                .SwitchToFirstWindow();

            ticketListingPage.SwitchToChildWindow(2);
            createNewTicketPage.ClickOnElement(createNewTicketPage.HistoryTab);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyHistory(new List<string>() { "Delete credit", "Mark for credit", "Pay ticket" });
            createNewTicketPage.ClickOnElement(createNewTicketPage.DetailTab);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            createNewTicketPage.ClickOnElement(createNewTicketPage.DuplicateTicketButton);
            createNewTicketPage.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifySelectedValue(createNewTicketPage.stationDd, "Townmead In Bridge");
            createNewTicketPage.VerifySelectedValue(createNewTicketPage.haulierDd, "Waste Management Ltd");
            createNewTicketPage.VerifySelectedValue(createNewTicketPage.SourcePartySelect, "GeoFossils");
            createNewTicketPage.VerifyInputValue(createNewTicketPage.PONumberInput, "1234");
            createNewTicketPage.VerifyTicketLineData(0, "General Recycling", "100", "80", firstTicketLine.firstDate, firstTicketLine.secondDate)
                .VerifyTicketLineData(1, "General Refuse", "80", "60", secondTicketLine.firstDate, secondTicketLine.secondDate);
            //click save
            createNewTicketPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBTicketSuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Click No
            createNewTicketPage.VerifyTakePayment()
                .ClickOnElement(createNewTicketPage.NoTakePaymentButton);
            createNewTicketPage.SleepTimeInMiliseconds(500);
            createNewTicketPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyTicketLineData(0, "General Recycling", "100", "80", firstTicketLine.firstDate, firstTicketLine.secondDate)
                .VerifyTicketLineData(1, "General Refuse", "80", "60", secondTicketLine.firstDate, secondTicketLine.secondDate);

            //Scroll down to the bottom of the page and click on Cancel ticket -> Modal update: Select a reason in the dropdown and add a note -> Click on cancel ticket
            createNewTicketPage.ScrollDownToElement(createNewTicketPage.CancelTicketButton);
            createNewTicketPage.ClickOnElement(createNewTicketPage.CancelTicketButton);
            createNewTicketPage.ClickCancelExpandReasonButton();
            createNewTicketPage.SelectByDisplayValueOnUlElement(createNewTicketPage.CancelReasonSelect, "Cancelled by Customer")
                .SendKeys(createNewTicketPage.CancelReasonNote, "test");
            createNewTicketPage.ClickOnElement(createNewTicketPage.CancelReasonButton);
            createNewTicketPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyElementEnable(createNewTicketPage.TakePaymentButton, false)
                .VerifyElementEnable(createNewTicketPage.CancelTicketButton, false)
                .VerifyElementEnable(createNewTicketPage.DuplicateTicketButton, true)
                .VerifyElementEnable(createNewTicketPage.CopyToGreyListButton, true)
                .VerifyElementEnable(createNewTicketPage.MarkForCreditButton, false)
                .VerifyElementEnable(createNewTicketPage.UnmarkForCreditButton, false);
            createNewTicketPage.ClickOnElement(createNewTicketPage.HistoryTab);
            createNewTicketPage.WaitForLoadingIconToDisappear();
            createNewTicketPage.VerifyHistory(new List<string>() { "Cancelled" });
        }

        [Category("WB")]
        [Category("Huong")]
        [Test(Description = "Verify that only active users are displayed in the dropdown")]
        public void TC_231_Retired_user_can_be_added_as_a_WB_station_user()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/weighbridge-stations/1");
            WeighbridgeStationPage weighbridgeStationPage = PageFactoryManager.Get<WeighbridgeStationPage>();
            weighbridgeStationPage.WaitForLoadingIconToDisappear();
            weighbridgeStationPage.ClickOnElement(weighbridgeStationPage.WeighbridgeStationUserTab);
            weighbridgeStationPage.WaitForLoadingIconToDisappear();
            weighbridgeStationPage.ClickOnElement(weighbridgeStationPage.AddNewWeighbridgeButton);
            weighbridgeStationPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            WeighbridgeStationUserPage weighbridgeStationUserPage = PageFactoryManager.Get<WeighbridgeStationUserPage>();
            weighbridgeStationUserPage.ClickOnElement(weighbridgeStationUserPage.UserToggleDropdownButton);
            weighbridgeStationUserPage.SendKeys(weighbridgeStationUserPage.SearchUserInput, "Neha");
            weighbridgeStationUserPage.VerifyElementVisibility(weighbridgeStationUserPage.NoResultLabel, true);
            CommonFinder finder = new CommonFinder(DbContext);
            var inactiveUsers = finder.GetUserInActive();
            Assert.IsTrue(inactiveUsers.Count != 0);
        }
    }
}
