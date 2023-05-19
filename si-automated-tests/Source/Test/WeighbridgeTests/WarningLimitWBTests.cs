using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
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
    public class WarningLimitWBTests : BaseTest
    {
        private string partyCustomerId;
        private string partyNameCustomer = "PCWarningLimitTC261_" + CommonUtil.GetRandomString(2);
        private string partyNameHaulier = "PHWarningLimitTC261_" + CommonUtil.GetRandomString(2);
        private string siteName56 = "SiteWarningLimitTC261_" + CommonUtil.GetRandomNumber(4);
        private string stationNameTC56 = "StationWarningLimit TC261_" + CommonUtil.GetRandomNumber(4);
        private string resourceName = "ResourceWarningLimit TC261_" + CommonUtil.GetRandomNumber(2);
        private string locationNameActive56 = "LocationWarningLimitTC261_" + CommonUtil.GetRandomNumber(2);
        private string resourceType56 = "Van";
        private string clientRef56 = "ClientRefWarningLimitTC261_" + CommonUtil.GetRandomNumber(2);
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
        [Category("WarningLimit")]
        [Test(Description = "Set up data test"), Order(1)]
        public void SetupDataTest_Tab_Warning_Limit()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser82.UserName, AutoUser82.Password)
                .IsOnHomePage(AutoUser82);

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
        [Category("WarningLimit")]
        [Test(Description = "Test ID = 1, 3, Verify that Warning limit is set up correctly for existing party and Warning limit can be modified"), Order(2)]
        public void TC_261_Tab_Warning_limit_existing_party_TestID_1_3()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string partyId = "30";
            string partyName = "GeoFossils";

            //Scenario 1
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser82.UserName, AutoUser82.Password)
                .IsOnHomePage(AutoUser82);
            //Open the party Id = 30 and change [Credit limit]
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .InputCreditLimt("1000")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInCreditLimt("1000")
                //Step line 9: Verify in WB settings tab
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInWarningLimit("750");
            //Step line 10: Run query to check
            List<WBPartySettingDBModel> list = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.AreEqual(0, list[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModels = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(1000, partiesDBModels[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModels = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(750, wBPartySettingVDBModels[0].creditlimitwarning);

            PageFactoryManager.Get<DetailPartyPage>()
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            //Step line 11: Change [Credit limit]
            PageFactoryManager.Get<DetailPartyPage>()
                .InputCreditLimt("900")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInCreditLimt("900")
                //Step line 12: Verify in WB settings tab
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
               .VerifyValueInWarningLimit("675");
            //Step line 13: Run query to check
            List<WBPartySettingDBModel> listAfterChanged = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.AreEqual(0, listAfterChanged[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModelsAfterChanged = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(900, partiesDBModelsAfterChanged[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModelsAfterChanged = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(675, wBPartySettingVDBModelsAfterChanged[0].creditlimitwarning);
            //Step line 17: Change Warning limit
            PageFactoryManager.Get<DetailPartyPage>()
                .InputTextInWarningLimit("1000")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInWarningLimit("1000");
            //Step line 18: API run query to check
            List<WBPartySettingDBModel> listAfterChangeWarningLimit = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.AreEqual(1000, listAfterChangeWarningLimit[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModelsAfterChangeWarningLimit = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(900, partiesDBModelsAfterChangeWarningLimit[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModelsAfterChangeWarningLimit = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(1000, wBPartySettingVDBModelsAfterChangeWarningLimit[0].creditlimitwarning);
            //Step line 19 + 20: Already set up and verify in other Tcs
            //Step line 26: Click on WB tickets tab and Set Warning limit = 0
            PageFactoryManager.Get<DetailPartyPage>()
                .InputTextInWarningLimit("0")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInWarningLimit("0");
            //Step line 27: Verify API
            List<WBPartySettingDBModel> listWarningLimit0 = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.AreEqual(0, listWarningLimit0[0].creditlimitwarning);
        }

        [Category("WB")]
        [Category("Chang")]
        [Category("WarningLimit")]
        [Test(Description = "Test ID = 2, Verify that Warning limit is set up correctly for a new party"), Order(3)]
        public void TC_261_Tab_Warning_limit_a_new_party_TestID_2()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string partyName = "TC261PartyName";
            string address = "Twickenham";
            string siteName261 = "Site Twickenham 261" + CommonUtil.GetRandomNumber(4);
            string siteName = CommonConstants.WBSiteName;

            //Step line 14: Create a new party
            PageFactoryManager.Get<LoginPage>()
                    .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser82.UserName, AutoUser82.Password)
                .IsOnHomePage(AutoUser82);
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
                .SendKeyToThePartyInput(partyName)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();

            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
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
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(siteName261)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            //Click on [Account] and change the credit limit]
            detailPartyPage
                .VerifyValueInCreditLimt("125")
                .InputCreditLimt("1000")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyValueInCreditLimt("1000")
                //Step line 15: Verify in WB settings tab
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyValueInWarningLimit("750");
            string partyId = detailPartyPage.GetPartyId();
            //Step line 16: Run API to check
            List<WBPartySettingDBModel> listAfterChanged = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.AreEqual(0, listAfterChanged[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModelsAfterChanged = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(1000, partiesDBModelsAfterChanged[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModelsAfterChanged = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(750, wBPartySettingVDBModelsAfterChanged[0].creditlimitwarning);
        }

        [Category("WB")]
        [Category("Chang")]
        [Category("WarningLimit")]
        [Test(Description = "Test ID = 6, Verify that no warning limit is displayed when Warning limit is set to 0 "), Order(4)]
        public void TC_261_Tab_Warning_limit_TestID_6_No_warning_limit_is_displayed_when_warning_limit_is_set_to_0()
        {
            //Go to the [WB Settings] tab in the detail party and set Warning Limit = 0
            //Default value in [Account] tab: [Account Balance] = 0 and [WIP Balance] = 5.64
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser82.UserName, AutoUser82.Password)
                .IsOnHomePage(AutoUser82);
            //Open the party
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .InputTextInWarningLimit("0")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
               .VerifyValueInWarningLimit("0");
            //Add new WB ticket for this party
            PageFactoryManager.Get<DetailPartyPage>()
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
            //Add ticket line
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
                .ClickOnNoWarningPopup();
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        }

        [Category("WB")]
        [Category("Chang")]
        [Category("WarningLimit")]
        [Test(Description = "Test ID = 5, Verify that warning message is displayed when the customer’s Account Balance + WIP Balance >= Warning Limit. "), Order(5)]
        public void TC_261_Tab_Warning_limit_TestID_5_Warning_limit_a_new_party()
        {
            //Go to the [WB Settings] tab in the detail party and set [Account Balance + WIP Balance >= Warning Limit]
            //Default value in [Account] tab: [Account Balance] = 0 and [WIP Balance] = 2.82
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser82.UserName, AutoUser82.Password)
                .IsOnHomePage(AutoUser82);
            //Open the party
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            //Get WIP balance
            string wipBalanceBefore = PageFactoryManager.Get<DetailPartyPage>()
                .GetWIPBalance();
            string accountBalanceBefore = PageFactoryManager.Get<DetailPartyPage>()
                .GetAccountBalance();
            string warningLimitTest = Math.Round(((Double.Parse(wipBalanceBefore) + Double.Parse(accountBalanceBefore)) - 1.00), 2,
                                             MidpointRounding.ToEven).ToString();

            PageFactoryManager.Get<DetailPartyPage>()
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            
            PageFactoryManager.Get<DetailPartyPage>()
                .InputTextInWarningLimit(warningLimitTest)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInWarningLimit(warningLimitTest);
            //Add new WB ticket for this party
            PageFactoryManager.Get<DetailPartyPage>()
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
                .VerifyDisplayToastMessageDoubleQuote(string.Format(MessageRequiredFieldConstants.CustomterBalanceExceededMessage, partyNameCustomer, wipBalanceBefore, warningLimitTest))
                .WaitUntilToastMessageInvisible(string.Format(MessageRequiredFieldConstants.CustomterBalanceExceededMessage, partyNameCustomer, wipBalanceBefore, warningLimitTest));
            //Create new ticket as normal
            //Add ticket line
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
                .ClickOnNoWarningPopup();
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        }

    }
}
