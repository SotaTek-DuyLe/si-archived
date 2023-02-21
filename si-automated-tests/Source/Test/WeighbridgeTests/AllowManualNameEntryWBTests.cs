using System;
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
    public class AllowManualNameEntryWBTests : BaseTest
    {
        private string partyCustomerId;
        private string partyNameCustomer = "PCAllowManualNameEntryTC261_" + CommonUtil.GetRandomString(2);
        private string partyNameHaulier = "PHAllowManualNameEntryTC261_" + CommonUtil.GetRandomString(2);
        private string siteName56 = "SiteAllowManualNameEntryTC261_" + CommonUtil.GetRandomNumber(4);
        private string stationNameTC56 = "StationAllowManualNameEntry TC261_" + CommonUtil.GetRandomNumber(4);
        private string resourceName = "ResourceAllowManualNameEntry TC261_" + CommonUtil.GetRandomNumber(2);
        private string locationNameActive56 = "LocationAllowManualNameEntryTC261_" + CommonUtil.GetRandomNumber(2);
        private string resourceType56 = "Van";
        private string clientRef56 = "ClientAllowManualNameEntryTC261_" + CommonUtil.GetRandomNumber(2);
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
        [Test(Description = "Set up data test for tab allow manual name entry"), Order(1)]
        public void SetupDataTest_Tab_Allow_Manual_Name_Entry()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser85.UserName, AutoUser85.Password)
                .IsOnHomePage(AutoUser85);

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
                .ClickOnVehicleTab()
                .WaitForLoadingIconVehicleTabDissaprear();
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
        [Test(Description = "Verify that user has the option to manually add a name/address for adhoc customer when Allow manual name entry is ticked"), Order(2)]
        public void TC_261_Tab_Allow_Manual_Name_Entry_Allow_Manual_Name_Entry_Is_Ticket() 
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser85.UserName, AutoUser85.Password)
                .IsOnHomePage(AutoUser85);
            //Open the party Id = partyCustomerId
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Step line 8: Click on [WB Settings] tab and Tick [Allow manual name entry]
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnAllowManualNameEntryCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            string accountNumber = PageFactoryManager.Get<DetailPartyPage>()
                .GetAccountNumber();

            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyAllowManualNameEntryChecked()
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
            //Verify the display of the text under the [Source] field
            string manualSourceParty = "Costa";
            createNewTicketPage
                .VerifyTextUnderSourceField(accountNumber)
                //Step line 10: Input [manual Source party] and add new ticket
                .InputManualSourceParty(manualSourceParty)
                //Add ticket line and Save
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

            createNewTicketPage
                .VerifyValueInManualSourceParty(manualSourceParty);
            //Step line 11: DB
            WBTicketDBModel wBTicketDBModel = commonFinder.GetWBTicketByTicketId(ticketIdFirst);
            Assert.AreEqual(partyNameCustomer, wBTicketDBModel.destination_party);
            Assert.AreEqual(manualSourceParty, wBTicketDBModel.source_party);
            //Step line 12: Change ticket type on the same ticket and verify the text next to destination party
            createNewTicketPage
                .ClickTicketType()
                .VerifyValueInTicketTypeDd()
                .ClickAnyTicketType("Outbound")
                .WaitForLoadingIconToDisappear();
                //Select Haulier
            createNewTicketPage
                .VerifyDisplayHaulierDd()
                .ClickAnyHaulier(partyNameHaulier)
                .WaitForLoadingIconToDisappear();
            string manualDestinationParty = "Tesco";
            createNewTicketPage
                .VerifyTextFieldNextToDestinationField()
                //Step line 13: Input [Manual Destination Party] field and Add ticket line
                .InputManualDestinationParty(manualDestinationParty)
                .ClickAddTicketLineBtn()
                .ClickProductDd()
                .ClickAnyProductValue(product56)
                //Verify Location
                .VerifyLocationPrepolulated(locationNameActive56)
                //Mandatory field remaining
                .InputFirstWeight(1)
                .InputFirstDate()
                .InputSecondDate()
                .InputSecondWeight(2)
                .ClickSaveBtn();
            //Step line 14: Select [Reason] and [Save]
            createNewTicketPage
                .IsSelectReasonPopup()
                .InputNoteInReasonPopup()
                .ClickOnSaveBtnInReasonPopup()
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .ClickOnNoWarningPopup();

            createNewTicketPage
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createNewTicketPage
                .VerifyValueInDestinationParty(manualDestinationParty);
            //Step line 15: Run query
            WBTicketDBModel wBTicketDBModelAfter = commonFinder.GetWBTicketByTicketId(ticketIdFirst);
            Assert.AreEqual(partyNameCustomer, wBTicketDBModelAfter.destination_party);
            Assert.AreEqual(manualDestinationParty, wBTicketDBModelAfter.source_party);
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 19: [Add new item] and DO NOT fill manual entry field
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
            createNewTicketPage
                .VerifyTextUnderSourceField(accountNumber)
                //Step line 20: DO NOT fill [manual Source party] and add new ticket
                //Add ticket line and Save
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
            string ticketId21 = createNewTicketPage
                .GetWBTicketId();
            string sourceName = createNewTicketPage
                .GetSourceValue();
            //Step line 21: Verify [Manual] entered field is autopopulated with [Source] party name
            createNewTicketPage
                .VerifyValueInManualSourceParty(sourceName);
            //Step line 22: Run a query
            WBTicketDBModel wBTicketDBModel21 = commonFinder.GetWBTicketByTicketId(ticketId21);
            Assert.AreEqual(partyNameCustomer, wBTicketDBModel21.destination_party);
            Assert.AreEqual(partyNameCustomer, wBTicketDBModel21.source_party);

        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Verify that text field is not displayed  when Allow manual name entry is unticked"), Order(3)]
        public void TC_261_Tab_Allow_Manual_Name_Entry_Allow_Manual_Name_Entry_Is_Unticked()
        {
            //Go to the [WB Settings] tab and unticket [Allow Manual Name Entry]
            CommonFinder commonFinder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser85.UserName, AutoUser85.Password)
                .IsOnHomePage(AutoUser85);
            //Open the party Id = partyCustomerId
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyCustomerId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyNameCustomer)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Step line 24: Click on [WB Settings] tab and UnTick [Allow manual name entry]
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnAllowManualNameEntryCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            string accountNumber = PageFactoryManager.Get<DetailPartyPage>()
                .GetAccountNumber();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyAllowManualNameEntryUnChecked()
                //Step line 25: Click on [WB tickets] tab and [Add new item]
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
            //Verify not display the text next to the [Source] field
            createNewTicketPage
                .VerifyTextFieldIsNotDisplayedNextToSourceDd()
                //Step line 26: Add a new ticket line
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
            //Step line 27: DB
            WBTicketDBModel wBTicketDBModel = commonFinder.GetWBTicketByTicketId(ticketIdFirst);
            Assert.AreEqual(partyNameCustomer, wBTicketDBModel.destination_party);
            Assert.AreEqual(partyNameCustomer, wBTicketDBModel.source_party);

        }
    }
}
