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
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder;
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
    public class PONumberRequiredWBTests : BaseTest
    {
        private string partyCustomerId;
        private string partyNameCustomer = "PCPONumberRequiredTC261_" + CommonUtil.GetRandomString(2);
        private string partyNameHaulier = "PHPONumberRequiredTC261_" + CommonUtil.GetRandomString(2);
        private string siteName56 = "SitePONumberRequiredTC261_" + CommonUtil.GetRandomNumber(4);
        private string stationNameTC56 = "StationPONumberRequired TC261_" + CommonUtil.GetRandomNumber(4);
        private string resourceName = "ResourcePONumberRequired TC261_" + CommonUtil.GetRandomNumber(2);
        private string locationNameActive56 = "LocationPONumberRequiredTC261_" + CommonUtil.GetRandomNumber(2);
        private string resourceType56 = "Van";
        private string clientRef56 = "ClientPONumberRequiredTC261_" + CommonUtil.GetRandomNumber(2);
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
        [Category("PONumber")]
        [Test(Description = "Setup data test for PO number required tab"), Order(1)]
        public void SetupDataTest_Tab_PO_Number_Required()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser86.UserName, AutoUser86.Password)
                .IsOnHomePage(AutoUser86);

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
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2))
                .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
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
            ////TC54: Create new product in Product tab with type = neutral
            //siteDetailPage
            //    .ClickAddNewProductItem()
            //    .SwitchToLastWindow();
            //addProductPage
            //    .WaitForAddProductPageDisplayed()
            //    .IsAddProductPage()
            //    //Select any product
            //    .ClickAnyProduct(neutralProduct)
            //    //Select any ticket Type
            //    .ClickAnyTicketType(neutralTicketType)
            //    //Click default Location
            //    .ClickDefaultLocationDdAndSelectAnyOption(locationNameActive56)
            //    .ClickSaveBtn()
            //    .VerifyToastMessage(MessageSuccessConstants.SaveWBSiteProductSuccessMessage)
            //    .ClickCloseBtn()
            //    .SwitchToChildWindow(3)
            //    .WaitForLoadingIconToDisappear();
            //siteDetailPage
            //    .WaitForLoadingIconInProductTabDisappear();
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
        [Category("PONumber")]
        [Test(Description = "Verify that user has to fill Po number when Purchase order number required is ticked."), Order(2)]
        public void TC_261_Tab_PO_Number_Required_Verify_That_User_Has_To_Fill_PO_Number_When_Purchase_Order_Number_Required_Is_Ticked()
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
            //Step line 8: Click on [WB Settings] tab and [Purchase Order Number Required] is checked, [Use Stored Purchase Order Number] checked, [Allow Manual Purchase Order Number] is not checked
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnPurchaseOrderNumberRequiredCheckbox()
                .ClickOnUseStorePoNumberCheckbox()
                //Default: [Allow Manual Purchase Order Number] is checked => Click on [Allow Manual Purchase Order Number] is not checked
                .ClickOnAllowManualPoNumberCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyPurchaseOrderNumberRequiredChecked()
                .VerifyUseStorePoNumberChecked()
                //Step line 9: CLick on [Purchase orders] tab and add new item
                .ClickOnPurchaseOrdersTab()
                .ClickOnAddNewItemInPurchaseOrderTab()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            string poNumber = "right";
            string firstDayIsTodayDate = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            PageFactoryManager.Get<PurchaseOrderDetailsPage>()
                .WaitingForPurchaseOrderPageLoadedSuccessfully()
                .InputPurchaseOrderNumberName(poNumber)
                .SetFirstDay(firstDayIsTodayDate)
                .SetLastDay("01/01/2049")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
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
            //Step line 10: Verify the display of the dropdown field next to PO Number
            createNewTicketPage
                .VerifyDisplayDdUnderPONumber()
                //Add ticket line
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
                //Step line 11: Click on [Save]
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.PONumberIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.PONumberIsRequiredMessage);
            //Step line 12: Click on [PO Number] dd and verify
            string[] poNumberOptions = { "Select...", poNumber };
            createNewTicketPage
                .ClickOnPONumberAndVerifyValue(poNumberOptions)
                //Step line 13: Select any [PO Number] and save the ticket
                .ClickOnAnyPONumber(poNumber)
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Step line 14: Run query to check
            List<PurchaseOderListVDBModel> purchaseOderListVDBModels = commonFinder.GetPurchaseOrderListVByPartyId(partyCustomerId);
            Assert.AreEqual(partyCustomerId, purchaseOderListVDBModels[0].partyID.ToString());
            Assert.AreEqual(partyNameCustomer, purchaseOderListVDBModels[0].customername);
            Assert.AreEqual(poNumber, purchaseOderListVDBModels[0].number);
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 15: Go to Weighbridge settings tab -> Tick [Purchase order number required] and [Allow manual purchase number]
            //[Use Stored Purchase Order Number] not checked -> Save the form
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickWBSettingTab()
                .ClickOnUseStorePoNumberCheckbox()
                .ClickOnAllowManualPoNumberCheckbox()
                .WaitForLoadingIconToDisappear()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickWBTicketTab()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Step line 16: Verify the display of the Free entry fields next to PO Number
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
                .VerifyDisplayFreeEntryFieldNextToPONumber()
                //Add ticket line
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
                //Step line 17: Click on [Save]
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.PONumberIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.PONumberIsRequiredMessage);
            //Step line 18:  Enter PO Number and Save ticket
            createNewTicketPage
                .InputFreeEntryFieldNextToPONumber("12345")
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string ticketIdAfter = createNewTicketPage
                .GetWBTicketId();
            //Step line 19: Run query to check
            List<PurchaseOderListVDBModel> purchaseOderListVDBModelsAfter = commonFinder.GetPurchaseOrderListVByPartyId(partyCustomerId);
            Assert.AreEqual(1, purchaseOderListVDBModelsAfter.Count);
            Assert.AreEqual(partyCustomerId, purchaseOderListVDBModelsAfter[0].partyID.ToString());
            Assert.AreEqual(partyNameCustomer, purchaseOderListVDBModelsAfter[0].customername);
            Assert.AreEqual(poNumber, purchaseOderListVDBModelsAfter[0].number);
            //Step line 20: Run query to check
            WBTicketDBModel wBTicketDBModel = commonFinder.GetWBTicketByTicketId(ticketIdAfter);
            Assert.AreEqual("12345", wBTicketDBModel.purchaseordernumber);
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 25: Go to Weighbridge settings tab -> Tick Purchase order number required, Allow manual purchase number and Use Stored Purchase Number
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickWBSettingTab()
                .ClickOnUseStorePoNumberCheckbox()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickWBTicketTab()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Step line 26: Verify the display of the Free entry fields and drop down next to PO Number
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
                .VerifyDisplayFreeEntryFieldNextToPONumber()
                .VerifyDisplayDdUnderPONumber()
                //Add ticket line
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
                //Step line 27: Click on [Save]
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.PONumberIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.PONumberIsRequiredMessage);
            //Step line 28: Enter the [PO] number and save the ticket
            createNewTicketPage
                .InputFreeEntryFieldNextToPONumber(poNumber)
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify Entered PO number is displayed in the dropdown and free entry field is cleared
            createNewTicketPage
                .VerifyDefaultValueInPoNumberDd(poNumber)
                .VerifyValueInFreeEntryField("");
            string ticketIdLine30 = createNewTicketPage
                .GetWBTicketId();
            //Step line 29: Verify in API
            List<PurchaseOderListVDBModel> purchaseOderListVDBModelsLine29 = commonFinder.GetPurchaseOrderListVByPartyId(partyCustomerId);
            Assert.AreEqual(1, purchaseOderListVDBModelsLine29.Count);
            Assert.AreEqual(partyCustomerId, purchaseOderListVDBModelsLine29[0].partyID.ToString());
            Assert.AreEqual(partyNameCustomer, purchaseOderListVDBModelsLine29[0].customername);
            Assert.AreEqual(poNumber, purchaseOderListVDBModelsLine29[0].number);
            //Step line 30: Verify with ticketId
            WBTicketDBModel wBTicketDBModelLine30 = commonFinder.GetWBTicketByTicketId(ticketIdLine30);
            Assert.AreEqual(poNumber, wBTicketDBModelLine30.purchaseordernumber);
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 31: CLick back on [WB tickets] and Add new item
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Step line 32: Verify the display of the Free entry fields and drop down next to PO Number
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
                .VerifyDisplayFreeEntryFieldNextToPONumber()
                .VerifyDisplayDdUnderPONumber()
                //Add ticket line
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
                //Step line 32: Click on [Save]
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.PONumberIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.PONumberIsRequiredMessage);
            //Step line 33: Enter the [PO] number and save the ticket
            createNewTicketPage
                .InputFreeEntryFieldNextToPONumber("12345")
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify Entered PO number is displayed in the dropdown and free entry field is cleared
            createNewTicketPage
                .VerifyDefaultValueInPoNumberDd("Select...")
                .VerifyValueInFreeEntryField("12345");
            string ticketIdLine34 = createNewTicketPage
                .GetWBTicketId();
            //Step line 34: Verify in API
            List<PurchaseOderListVDBModel> purchaseOderListVDBModelsLine34 = commonFinder.GetPurchaseOrderListVByPartyId(partyCustomerId);
            Assert.AreEqual(1, purchaseOderListVDBModelsLine34.Count);
            Assert.AreEqual(partyCustomerId, purchaseOderListVDBModelsLine34[0].partyID.ToString());
            Assert.AreEqual(partyNameCustomer, purchaseOderListVDBModelsLine34[0].customername);
            Assert.AreEqual(poNumber, purchaseOderListVDBModelsLine34[0].number);
            //Step line 35: Verify with ticketId
            WBTicketDBModel wBTicketDBModelLine35 = commonFinder.GetWBTicketByTicketId(ticketIdLine34);
            Assert.AreEqual("12345", wBTicketDBModelLine35.purchaseordernumber);
            createNewTicketPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 36: CLick back on [WB tickets] and Add new item
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickAddNewWBTicketBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                //Step line 37: Verify the display of the Free entry fields and drop down next to PO Number
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
                .VerifyDisplayFreeEntryFieldNextToPONumber()
                .VerifyDisplayDdUnderPONumber()
                //Add ticket line
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
                //Step line 37: Click on [Save]
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.PONumberIsRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.PONumberIsRequiredMessage);
            //Step line 38: Select PO Number from dropdown and Enter the [PO] number = 321 and save the ticket
            createNewTicketPage
                .ClickOnAnyPONumber(poNumber)
                .InputFreeEntryFieldNextToPONumber("321")
                .ClickSaveBtn();
            createNewTicketPage
                .ClickOnNoWarningPopup()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string ticketIdLine40 = createNewTicketPage
                .GetWBTicketId();
            //Step line 39: Verify in API
            List<PurchaseOderListVDBModel> purchaseOderListVDBModelsLine39 = commonFinder.GetPurchaseOrderListVByPartyId(partyCustomerId);
            Assert.AreEqual(1, purchaseOderListVDBModelsLine39.Count);
            Assert.AreEqual(partyCustomerId, purchaseOderListVDBModelsLine39[0].partyID.ToString());
            Assert.AreEqual(partyNameCustomer, purchaseOderListVDBModelsLine39[0].customername);
            Assert.AreEqual(poNumber, purchaseOderListVDBModelsLine39[0].number);
            //Step line 40: Verify with ticketId
            WBTicketDBModel wBTicketDBModelLine40 = commonFinder.GetWBTicketByTicketId(ticketIdLine40);
            Assert.AreEqual("321", wBTicketDBModelLine40.purchaseordernumber);
            //Step line 41: Change a PO number in free entry field and click on PO number dd and select a number dropdown
            createNewTicketPage
                .InputFreeEntryFieldNextToPONumber(poNumber)
                .ClickOnAnyPONumber(poNumber)
                .SleepTimeInSeconds(2);
            createNewTicketPage
                .VerifyValueInFreeEntryField("")
                //Step line 41: Click on [Save]
                .ClickSaveBtn();
            createNewTicketPage
                .IsSelectReasonPopup()
                .InputNoteInReasonPopup()
                .ClickOnSaveBtnInReasonPopup()
                .WaitForLoadingIconToDisappear();
            createNewTicketPage
                .ClickOnNoWarningPopup()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string ticketIdLine43 = createNewTicketPage
                .GetWBTicketId();
            //Step line 42: Verify in API
            List<PurchaseOderListVDBModel> purchaseOderListVDBModelsLine41 = commonFinder.GetPurchaseOrderListVByPartyId(partyCustomerId);
            Assert.AreEqual(1, purchaseOderListVDBModelsLine41.Count);
            Assert.AreEqual(partyCustomerId, purchaseOderListVDBModelsLine41[0].partyID.ToString());
            Assert.AreEqual(partyNameCustomer, purchaseOderListVDBModelsLine41[0].customername);
            Assert.AreEqual(poNumber, purchaseOderListVDBModelsLine41[0].number);
            //Step line 43: Verify with ticketId
            WBTicketDBModel wBTicketDBModelLine43 = commonFinder.GetWBTicketByTicketId(ticketIdLine43);
            Assert.AreEqual(poNumber, wBTicketDBModelLine43.purchaseordernumber);
        }
    }
}
