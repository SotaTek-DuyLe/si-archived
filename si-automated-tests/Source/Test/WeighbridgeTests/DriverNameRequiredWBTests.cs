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
    public class DriverNameRequiredWBTests : BaseTest
    {
        private string partyCustomerId;
        private string partyNameCustomer = "PCDriverNameTC261_" + CommonUtil.GetRandomString(2);
        private string partyNameHaulier = "PHDriverNameTC261_" + CommonUtil.GetRandomString(2);
        private string siteName56 = "Site Twickenham TC261_" + CommonUtil.GetRandomNumber(4);
        private string stationNameTC56 = "AutoStation TC261_" + CommonUtil.GetRandomNumber(4);
        private string resourceName = "WB Van TC261_" + CommonUtil.GetRandomNumber(2);
        private string locationNameActive56 = "LocationTC261WBActive" + CommonUtil.GetRandomNumber(2);
        private string resourceType56 = "Van";
        private string clientRef56 = "ClientRefTC261_" + CommonUtil.GetRandomNumber(2);
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
        [Test(Description = "Set up data test"), Order(1)]
        public void SetupDataTest_Tab_Driver_Name_Required_()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser83.UserName, AutoUser83.Password)
                .IsOnHomePage(AutoUser83);

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
        [Test(Description = "Test ID = 1 Verify that when Driver name required user has to add the name"), Order(2)]
        public void TC_261_Tab_Driver_Name_Required_TestID_1_Verify_that_when_driver_name_required_user_has_to_add_the_name()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser83.UserName, AutoUser83.Password)
                .IsOnHomePage(AutoUser83);
            //Open the party Id = 1164
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Step line 8: Click on [WB Settings] tab and Tick [Driver Name Required]
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnDriverNameRequiredCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDriverNameRequiredChecked()
                //Step line 9: Click on [WB tickets] tab and [Add new item]
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

            //Step line 9: Verify the display of the message [Driver Name is required]
            createNewTicketPage
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.DriverNameIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.DriverNameIsRequiredMessage);

            //Step line 10: Input value [Driver name]
            createNewTicketPage
                .InputDriverName("Pablo")
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createNewTicketPage
                .VerifyValueAtDriverName("Pablo");
            string ticketId = createNewTicketPage
                .GetWBTicketId();
            //Step line 11: API
            WBTicketDBModel wBTicketDBModel = commonFinder.GetWBTicketByTicketId(ticketId);
            Assert.AreEqual("Pablo", wBTicketDBModel.driver);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Test ID = 2 Verify that user doesn't have to enter the name when flag is not set"), Order(3)]
        public void TC_261_Tab_Driver_Name_Required_TestID_2_Verify_that_user_does_not_have_to_enter_the_name_when_flag_is_not_set()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser83.UserName, AutoUser83.Password)
                .IsOnHomePage(AutoUser83);
            //Open the party Id = 1164
            PageFactoryManager.Get<BasePage>()
               .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Step line 12: Click on [WB Settings] tab and UnTick [Driver Name Required]
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnDriverNameRequiredCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDriverNameRequiredNotChecked()
                //Step line 13: Click on [WB tickets] tab and [Add new item] > Fill all mandatory field
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
            string ticketIdFirst = createNewTicketPage
                .GetWBTicketId();
            //Step line 14: API
            WBTicketDBModel wBTicketDBModel = commonFinder.GetWBTicketByTicketId(ticketIdFirst);
            Assert.IsNull(wBTicketDBModel.driver);
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 15: 
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
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
                //Step line 15: Input [Driver name]
                .InputDriverName("Mark")
                .ClickSaveBtn();
            createNewTicketPage
                 .ClickOnNoWarningPopup()
                 .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                 .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createNewTicketPage
                .VerifyValueAtDriverName("Mark");
            string ticketIdSecond = createNewTicketPage
                .GetWBTicketId();
            //Step line 11: API
            WBTicketDBModel wBTicketDBModelAfter = commonFinder.GetWBTicketByTicketId(ticketIdSecond);
            Assert.AreEqual("Mark", wBTicketDBModelAfter.driver);
        }
    }
}
