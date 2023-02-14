using System;
using NUnit.Framework;
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
    public class AuthoriseTippingWBTests : BaseTest
    {
        private string partyCustomerId;
        private string partyNameCustomer = "PCAuthoriseTippingTC261_" + CommonUtil.GetRandomString(2);
        private string partyNameHaulier = "PHAuthoriseTippingTC261_" + CommonUtil.GetRandomString(2);
        private string siteName56 = "SiteAuthoriseTippingTC261_" + CommonUtil.GetRandomNumber(4);
        private string stationNameTC56 = "StationAuthoriseTipping TC261_" + CommonUtil.GetRandomNumber(4);
        private string resourceName = "ResourceAuthoriseTipping TC261_" + CommonUtil.GetRandomNumber(2);
        private string locationNameActive56 = "LocationAuthoriseTippingTC261_" + CommonUtil.GetRandomNumber(2);
        private string resourceType56 = "Van";
        private string clientRef56 = "ClientAuthoriseTippingTC261_" + CommonUtil.GetRandomNumber(2);
        private string product56 = "Garden";
        private string ticketType56 = "Incoming";
        private string neutralProduct = "Plastic";
        private string outboundProduct = "Clinical";
        private string neutralTicketType = "Neutral";
        private string outboundTicketType = "Outbound";
        private string address = "Twickenham";
        private string siteName = CommonConstants.WBSiteName;

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Set up data test for Authorise tipping tab"), Order(1)]
        public void SetupDataTest_Tab_authorise_tipping_tab()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser84.UserName, AutoUser84.Password)
                .IsOnHomePage(AutoUser84);

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
                .SelectResourceType(resourceType56)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
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
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameHaulier)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayYellowMesInLicenceNumberExField()
                .VerifyForcusOnLicenceNumberExField()
                .VerifyDisplayGreenBoderInLicenceNumberExField()
                .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2))
                .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyDisplayMesInCorresspondenAddressField()
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
            createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage
                .SelectAddressClickNextBtn()
                .InsertSiteName("Haulier" + siteName56)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailPartyPage
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
                .ClickSaveBtn()
                //.VerifyDisplaySuccessfullyMessage()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickOnDetailsTab();
            //Get partyId
            partyCustomerId = detailPartyPage
                .GetPartyId();
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
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                //Create new Vehicle in Vehicles tab
                .ClickOnVehicleTab();
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
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveWBVCHRegistered);
            PageFactoryManager.Get<AddVehiclePage>()
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
                .ClickStationTab();
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
            //TC52: Create new active location
            siteDetailPage
                .ClickOnLocationTab();
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
                .ClickProductTab();
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
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .WaitForLoadingIconInProductTabDisappear();
            //TC54: Create new product in Product tab with type = neutral
            siteDetailPage
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
                //Select any product
                .ClickAnyProduct(neutralProduct)
                //Select any ticket Type
                .ClickAnyTicketType(neutralTicketType)
                //Click default Location
                .ClickDefaultLocationDdAndSelectAnyOption(locationNameActive56)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteProductSuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .WaitForLoadingIconInProductTabDisappear();
            //TC54: Create new product in Product tab with type = outbound
            siteDetailPage
                .ClickAddNewProductItem()
                .SwitchToLastWindow();
            addProductPage
                .WaitForAddProductPageDisplayed()
                .IsAddProductPage()
                //Select any product
                .ClickAnyProduct(outboundProduct)
                //Select any ticket Type
                .ClickAnyTicketType(outboundTicketType)
                //Click default Location
                .ClickDefaultLocationDdAndSelectAnyOption(locationNameActive56)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteProductSuccessMessage);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Verify that ticket can't be created for the customer with 'Never allow tipping' set. ON HOLD button is not set"), Order(2)]
        public void TC_261_Tab_Authrise_Tipping_Verify_that_ticket_can_not_be_created_for_the_customer_with_Never_Allow_Tipping_set_ON_HOLD_button_is_not_set()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser83.UserName, AutoUser83.Password)
                .IsOnHomePage(AutoUser83);
            //Open the party Id = partyCustomerId
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Step line 8: Click on [WB Settings] tab and Select Option [Never Allow Tipping]
            PageFactoryManager.Get<DetailPartyPage>()
                .SelectAnyOptionAuthoriseTipping(CommonConstants.AuthoriseTipping[0])
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyOptionAuthoriseTippingChecked(CommonConstants.AuthoriseTipping[0])
                //Step line 9: Click on [WB tickets] tab and [Add new item], Verify the display of the message [Customer PCAuthoriseTippingTC261_HZ is not authorised to tip]
                .ClickWBTicketTab()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(2)
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
            //Select Haulier
            createNewTicketPage
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .VerifyDisplayToastMessage(string.Format(MessageRequiredFieldConstants.CustomerAuthoriseTippingMessage, partyNameCustomer))
                .WaitUntilToastMessageInvisible(string.Format(MessageRequiredFieldConstants.CustomerAuthoriseTippingMessage, partyNameCustomer));
            //Step line 10: Add new Ticket line and Save
            createNewTicketPage
                .ClickAddTicketLineBtn()
                .ClickProductDd()
                .ClickAnyProductValue(product56)
                //Verify Location
                .VerifyLocationPrepolulated(locationNameActive56)
                //Mandatory field remaining
                .InputFirstWeight(2)
                .InputFirstDate()
                .InputSecondDate()
                .InputSecondWeight(1)
                .ClickSaveBtn();
            createNewTicketPage
                .VerifyDisplayToastMessage(string.Format(MessageRequiredFieldConstants.CustomerAuthoriseTippingMessage, partyNameCustomer))
                .WaitUntilToastMessageInvisible(string.Format(MessageRequiredFieldConstants.CustomerAuthoriseTippingMessage, partyNameCustomer));
            createNewTicketPage
                .ClickCloseBtn()
                .AcceptAlert();
            //Step line 11: Go to [Acount tab] and tick [On Credit hold] option

        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Verify that ticket can be always created for customer with 'Always allow tipping' set"), Order(3)]
        public void TC_261_Tab_Authrise_Tipping_Verify_that_ticket_can_be_always_created_for_customer_with_Always_allow_tipping_set()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser83.UserName, AutoUser83.Password)
                .IsOnHomePage(AutoUser83);
            //Open the party Id = partyCustomerId
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Step line 15: Click on [WB Settings] tab and Select Option [Always Allow Tipping]
            PageFactoryManager.Get<DetailPartyPage>()
                .SelectAnyOptionAuthoriseTipping(CommonConstants.AuthoriseTipping[2])
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyOptionAuthoriseTippingChecked(CommonConstants.AuthoriseTipping[2])
                //Step line 16: Add new ticket in [WB Tickets] tab
                .ClickWBTicketTab()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(2)
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
            //Select Haulier
            createNewTicketPage
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            //Add new ticket line
            createNewTicketPage
                .ClickAddTicketLineBtn()
                .ClickProductDd()
                .ClickAnyProductValue(product56)
                //Verify Location
                .VerifyLocationPrepolulated(locationNameActive56)
                //Mandatory field remaining
                .InputFirstWeight(3)
                .InputFirstDate()
                .InputSecondDate()
                .InputSecondWeight(2)
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup();
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

        }
    }
}
