﻿using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyVehiclePage;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.WB.Tickets;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.WeighbridgeTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class WeighbridgeProductTests : BaseTest
    {
        private readonly string address = "Twickenham";
        private readonly string siteName = CommonConstants.WBSiteName;

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
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
        }

        [Category("WB")]
        [Test(Description = "WB Site product 2")]
        public void TC_056_WB_Site_product_2()
        {
            string partyNameCustomer = "Auto56Customer" + CommonUtil.GetRandomString(2);
            string partyNameHaulier = "Auto056Haulier" + CommonUtil.GetRandomString(2);
            string siteName56 = "Site Twickenham 56" + CommonUtil.GetRandomNumber(4);
            string stationNameTC56 = "AutoStation" + CommonUtil.GetRandomNumber(4);
            string resourceName = "Auto56 WB Van" + CommonUtil.GetRandomNumber(2);
            string locationNameActive56 = "Location56WBActive" + CommonUtil.GetRandomNumber(2);
            string resourceType56 = "Van";
            string clientRef56 = "ClientRef56" + CommonUtil.GetRandomNumber(2);
            string product56 = "Garden";
            string ticketType56 = "Incoming";

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
                .SelectResourceType(resourceType56)
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
            string addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage
                .SelectAddressClickNextBtn()
                .InsertSiteName(siteName56)
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
            detailPartyPage
                //Create new Vehicle in Vehicles tab
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyTableDisplayedInVehicle()
                .ClickAddNewVehicleBtn()
                .SwitchToLastWindow();
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
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName56 + " / " + addressAdded45)
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
                .InputName(stationNameTC56)
                .SelectDefaultTicket("Incoming")
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
                .InputName(locationNameActive56)
                .SelectActiveCheckbox()
                .InputClientName(clientRef56)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage);
            addLocationPage
                .VerifyActiveCheckboxSelected()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
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
                .ClickAnyProduct(product56)
            //Select any ticket Type
                .ClickAnyTicketType(ticketType56)
                //Click default Location
                .ClickDefaultLocationDdAndSelectAnyOption(locationNameActive56)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteProductSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveSiteSuccessMessage)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            //Back to the WB ticket menu
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
                .ClickStationDdAndSelectStation(stationNameTC56)
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
                .ClickAnyTicketType(ticketType56)
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            //Add ticket line
            createNewTicketPage
                .InputLicenceNumberExpDate()
                .InputLicenceNumber()
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product56)
                //Verify Location
                .VerifyLocationPrepolulated(locationNameActive56)
                //Mandatory field remaining
                .InputFirstWeight(2)
                .InputFirstDate()
                .InputSecondDate()
                .InputSecondWeight(1)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBTicketSuccessMessage);
        }

        [Category("WB")]
        [Test(Description = "WB Site product 3")]
        public void TC_057_WB_Site_product_3()
        {
            string partyNameCustomer = "Auto57Customer" + CommonUtil.GetRandomString(2);
            string partyNameHaulier = "Auto057Haulier" + CommonUtil.GetRandomString(2);
            string siteName57 = "Site Twickenham 57" + CommonUtil.GetRandomNumber(4);
            string stationNameTC57 = "AutoStation" + CommonUtil.GetRandomNumber(4);
            string resourceName = "Auto57 WB Van" + CommonUtil.GetRandomNumber(2);
            string locationNameActive57 = "Location57WBActive" + CommonUtil.GetRandomNumber(2);
            string resourceType57 = "Van";
            string clientRef57 = "ClientRef57" + CommonUtil.GetRandomNumber(2);
            string product57 = "Cardboard";
            string ticketType57 = "Incoming";

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
                .SelectResourceType(resourceType57)
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
            string addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage
                .SelectAddressClickNextBtn()
                .InsertSiteName(siteName57)
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
            detailPartyPage
                //Create new Vehicle in Vehicles tab
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyTableDisplayedInVehicle()
                .ClickAddNewVehicleBtn()
                .SwitchToLastWindow();
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
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName57 + " / " + addressAdded45)
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
                .InputName(stationNameTC57)
                .SelectDefaultTicket("Incoming")
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
                .InputName(locationNameActive57)
                .SelectActiveCheckbox()
                .InputClientName(clientRef57)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteLocationSuccessMessage);
            addLocationPage
                .VerifyActiveCheckboxSelected()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
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
                .ClickAnyProduct(product57)
            //Select any ticket Type
                .ClickAnyTicketType(ticketType57)
                //Click [Is Location Mandatory] checkbox
                .ClickOnIsLocationMandatoryCheckbox()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteProductSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SaveSiteSuccessMessage)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            //Back to the WB ticket menu
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
                .ClickStationDdAndSelectStation(stationNameTC57)
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
                .ClickAnyTicketType(ticketType57)
                //Verify Haulie field displayed
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            //Add ticket line
            createNewTicketPage
                .InputLicenceNumberExpDate()
                .InputLicenceNumber()
                .ClickAddTicketLineBtn()
                //Select Product created
                .ClickProductDd()
                .ClickAnyProductValue(product57)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageRequiredFieldConstants.LocationRequiredMessage);
            //Select Location
            createNewTicketPage
                .SelectLocation(locationNameActive57)
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
