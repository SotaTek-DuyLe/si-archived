using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
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
using si_automated_tests.Source.Main.Pages.WB.Sites;
using si_automated_tests.Source.Main.Pages.WB.Tickets;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.WeighbridgeTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
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
        public void TC_055_WB_Site_location_delete()
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

            //Create new Resource with type = Van in TC51
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("North Star Commercial")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveResourceSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //TC45+48+51

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            //Create new party Haulier TC047
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
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
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput(partyNameCustomer)
                .SelectPartyType(1)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
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
                .VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage);
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBVCHRegistered)
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBStationSuccessMessage);
            createStationPage
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveSiteSuccessMessage);
            //TC54: Create new product in Product tab
            siteDetailPage
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
            //Select any product
                .ClickAnyProduct(product)
            //Select any ticket Type
                .ClickAnyTicketType(ticketType)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteProductSuccessMessage)
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage);
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
                .SwitchToLastWindow();
            DeleteWBLocation deleteWBLocation = PageFactoryManager.Get<DeleteWBLocation>();
            deleteWBLocation
                .IsWarningPopupDisplayed()
                .ClickYesBtn();
            deleteWBLocation
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage)
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            //TC55: Click WB Ticket tab and verify
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Weighbridge")
                .ExpandOption("North Star Commercial")
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
        [Test(Description = "WB create party customer"), Order(1)]
        public void GetAllSiteInWBBefore()
        {
            //Verify data in TC45, 46, 47 not apprear in WB Site
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Weighbridge")
                .ExpandOption("North Star Commercial")
                .OpenOption("Sites")
                .SwitchNewIFrame();
            SiteListingPage siteListingPage = PageFactoryManager.Get<SiteListingPage>();
            siteModelBefore = siteListingPage
                .GetAllSiteDisplayed();
        }

        [Category("WB")]
        [Test(Description = "WB create party customer"), Order(2)]
        public void TC_045_WB_Create_party_customer()
        {
            //Create new party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput(partyName045)
                .SelectPartyType(1)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
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
                .VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SavePartySuccessMessage);
            //Internal flag checked
            detailPartyPage
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SavePartySuccessMessage)
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
                .ClickMapTabAndVerifyMessage(MessageRequiredFieldConstants.WBMapTabWarningMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.WBMapTabWarningMessage)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyWBSettingTab();
        }

        [Category("WB")]
        [Test(Description = "WB create party customer and haulier"), Order(3)]
        public void TC_046_WB_Create_party_customer_and_haulier()
        {
            //Create new party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput("Auto" + CommonUtil.GetRandomString(2))
                .SelectPartyType(1)
                .SelectPartyType(2)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayGreenBoderInLicenceNumberExField()
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
                .VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SavePartySuccessMessage);
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            List<SiteModel> siteModel = detailPartyPage
                .GetAllSiteInList();
            allSiteModel.Add(siteModel[0]);
        }

        [Category("WB")]
        [Test(Description = "WB create party haulier"), Order(4)]
        public void TC_047_WB_Create_party_haulier()
        {
            //Create new party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput(partyName047)
                .SelectPartyType(2)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            partyIdHaulier = detailPartyPage
                .GetPartyId();
            detailPartyPage
                .VerifyDisplayYellowMesInLicenceNumberExField()
                .VerifyForcusOnLicenceNumberExField()
                .VerifyDisplayGreenBoderInLicenceNumberExField()
                .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2))
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayYellowMesInLicenceNumberField()
                .VerifyForcusOnLicenceNumberField()
                .VerifyDisplayGreenBoderInLicenceNumberField()
                //Verify search Btn (waiting to confirm)
                //.ClickDownloadBtnAndVerify()
                //Input LicenceNumber
                .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
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
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            List<SiteModel> siteModel = detailPartyPage
                .GetAllSiteInList();
            allSiteModel.Add(siteModel[0]);
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, addressSite1 + " / " + addressAdded)
                .VerifyDisplayAllTab(CommonConstants.AllSiteTabCase47)
                .ClickDetailTab()
                .ClickSomeTabAndVerifyNoErrorMessage()
                .ClickMapTabAndVerifyMessage(MessageRequiredFieldConstants.WBMapTabWarningMessage)
                .ClickSaveAndCloseBtn();
        }

        [Category("WB")]
        [Test(Description = "WB Station"), Order(5)]
        public void TC_048_WB_Station()
        {
            //Verify data in TC45, 46, 47 not apprear in WB Site
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Weighbridge")
                .ExpandOption("North Star Commercial")
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
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
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
                .ClickSaveBtn();
            //Missing message error name input
            createStationPage
                .InputName(stationNameTC48)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBStationSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveWBStationSuccessMessage);
            createStationPage
                .SelectDefaultTicket("Incoming")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBStationSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveWBStationSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickStationTab()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveSiteSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveSiteSuccessMessage)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Weighbridge")
                .ExpandOption("North Star Commercial")
                .OpenOption("Sites")
                .SwitchNewIFrame();
            List<SiteModel> siteModelsNew = siteListingPage
                .GetAllSiteDisplayed();
            siteListingPage
                .VerifyDisplayNewSite(siteModel045[0], siteModelsNew[0]);
        }

        [Category("WB")]
        [Test(Description = "WB VCH Human"), Order(6)]
        public void TC_050_WB_VCH_Human()
        {
            string resourceName = "Auto WB " + CommonUtil.GetRandomNumber(2);
            string resourceType = "Driver";

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("North Star Commercial")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveResourceSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveResourceSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .ExpandOption("North Star Commercial")
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

        [Category("WB")]
        [Test(Description = "WB VCH Vehicle"), Order(7)]
        public void TC_051_WB_VCH_Vehicle()
        {
            resourceName = "Auto WB Van" + CommonUtil.GetRandomNumber(2);
            string resourceType = "Van";
            string vehicleNotActiveName = "COM7 NST";

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("North Star Commercial")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveResourceSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveResourceSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Navigate to party detail in TC045
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
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
            PageFactoryManager.Get<AddVehiclePage>()
                .IsCreateVehicleCustomerHaulierPage()
                .VerifyDefaultMandatoryFieldAndDefaultValue(partyName045)
                .ClickDefaultCustomerAddressDropdownAndVerify(addressAdded45)
                .ClickSaveBtn();
            PageFactoryManager.Get<AddVehiclePage>()
                .VerifyDisplayResourceRequiredMessage()
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
                .ClickMainOption("Resources")
                .ExpandOption("North Star Commercial")
                .OpenOption("Vehicle_Customer_Haulier")
                .SwitchNewIFrame();
            List<VehicleModel> allVehicleCustomerHaulier = PageFactoryManager.Get<VehicleCustomerHaulierPage>()
                .VerifyVehicleCustomerHaulierPageDisplayed()
                .GetAllVehicleModel();
            detailPartyPage
                .VerifyVehicleCreated(allVehicleCustomerHaulier[0], resourceName, partyName045, partyName047, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonConstants.EndDateAgreement);
        }

        [Category("WB")]
        [Test(Description = "WB Location"), Order(8)]
        public void TC_052_WB_Location()
        {
            string locationNameNotActive = "Location52WBNotActive" + CommonUtil.GetRandomNumber(2);
            string clientRef = "Client" + CommonUtil.GetRandomNumber(2);

            //Navigate to party detail in TC048
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage)
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage);
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            List<LocationModel> allModelsNew = siteDetailPage
                .GetAllLocationInGrid();
            siteDetailPage
                .VerifyLocationCreated(allModelsNew[0], locationNameActive, true, clientRef);
        }

        [Category("WB")]
        [Test(Description = "WB Station No ticket type"), Order(9)]
        public void TC_053_WB_Station_No_ticket_type()
        {
            string stationName = "AutoStation" + CommonUtil.GetRandomNumber(2);
            //Back to the party customer in TC45
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
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
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveSiteSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveSiteSuccessMessage)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Weighbridge")
                .ExpandOption("North Star Commercial")
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

        [Category("WB")]
        [Test(Description = "WB Site product 1"), Order(10)]
        public void TC_054_WB_Site_product_1()
        {
            string product = "Food";
            string ticketType = "Incoming";

            //Find party - Customer: TC045
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
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
                //Create new product TC54
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteProductSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveWBSiteProductSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveSiteSuccessMessage)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            //Back to the WB ticket (same the TC053)
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Weighbridge")
                .ExpandOption("North Star Commercial")
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
                .ClickAnyTicketType(ticketType)
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyName047)
                .WaitForLoadingIconToDisappear();
            //Add ticket line
            createNewTicketPage
                .InputLicenceNumberExpDate()
                .InputLicenceNumber()
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
                .VerifyToastMessage(MessageSuccessConstants.SaveWBTicketSuccessMessage);
        }

    }
}
