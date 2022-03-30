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
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.WB.Sites;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.WeighbridgeTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class WeighbridgeTests : BaseTest
    {
        private readonly string address = "Twickenham";
        private readonly string siteName45 = "Site Twickenham 45" + CommonUtil.GetRandomNumber(4);
        private string addressAdded45;
        private readonly string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
        private readonly string siteName = CommonConstants.WBSiteName;
        private string addressAdded;
        private List<SiteModel> allSiteModel = new List<SiteModel>();
        private List<SiteModel> siteModelBefore = new List<SiteModel>();
        private List<SiteModel> siteModel045;
        private string partyIdCustomer, partyIdHaulier;
        private string partyName045 = "Auto045Customer" + CommonUtil.GetRandomString(2);
        private string partyName047 = "Auto047Haulier" + CommonUtil.GetRandomString(2);

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
                .VerifyToastMessage("Successfully saved party.");
            //Internal flag checked
            detailPartyPage
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved party.");
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
                .ClickMapTabAndVerifyMessage("No Service Unit(s) associated to this Site ")
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyWBSettingTab()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
        }

        //[Category("WB")]
        //[Test(Description = "WB create party customer and haulier"), Order(3)]
        //public void TC_046_WB_Create_party_customer_and_haulier()
        //{
        //    //Create new party
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption("Parties")
        //        .ExpandOption("North Star Commercial")
        //        .OpenOption("Parties")
        //        .SwitchNewIFrame();
        //    PageFactoryManager.Get<PartyCommonPage>()
        //        .ClickAddNewItem()
        //        .SwitchToChildWindow(2);
        //    PageFactoryManager.Get<CreatePartyPage>()
        //        .IsCreatePartiesPopup("North Star Commercial")
        //        .SendKeyToThePartyInput("Auto" + CommonUtil.GetRandomString(2))
        //        .SelectPartyType(1)
        //        .SelectPartyType(2)
        //        .ClickSaveBtn();
        //    DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
        //    detailPartyPage
        //        .VerifyDisplaySuccessfullyMessage()
        //        .ClickSaveBtn()
        //        .WaitForLoadingIconToDisappear();
        //    detailPartyPage
        //        .VerifyDisplayGreenBoderInLicenceNumberExField()
        //        .VerifyDisplayYellowMesInLicenceNumberExField()
        //        .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
        //        .ClickSaveBtn()
        //        .WaitForLoadingIconToDisappear();
        //    detailPartyPage
        //        .VerifyDisplayGreenBoderInLicenceNumberField()
        //        .VerifyDisplayYellowMesInLicenceNumberField()
        //        .InputLicenceNumber(CommonUtil.GetRandomNumber(5))
        //        .ClickSaveBtn();
        //    detailPartyPage
        //        .VerifyDisplayMesInInvoiceAddressField()
        //        .ClickOnAddInvoiceAddressBtn()
        //        .SwitchToChildWindow(3);
        //    PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
        //    partySiteAddressPage.WaitForLoadingIconToDisappear();
        //    partySiteAddressPage.IsOnPartySiteAddressPage()
        //        .InputTextToSearchBar(address)
        //        .ClickSearchBtn()
        //        .VerifySearchedAddressAppear(address)
        //        .ClickOnSearchedAddress(address)
        //        .ClickOnNextButton()
        //        .SwitchToLastWindow();
        //    CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
        //    createEditSiteAddressPage
        //        .WaitForLoadingIconToDisappear();
        //    addressAdded = createEditSiteAddressPage
        //        .SelectRandomSiteAddress();
        //    createEditSiteAddressPage.SelectAddressClickNextBtn()
        //        .InsertSiteName(addressSite1)
        //        .ClickAnySiteInDd(siteName)
        //        .ClickCreateBtn()
        //        .SwitchToChildWindow(2);
        //    detailPartyPage.WaitForLoadingIconToDisappear();
        //    detailPartyPage
        //        .VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
        //        .ClickOnInvoiceAddressButton()
        //        .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
        //        .SelectCreatedAddress(addressAdded)
        //        .ClickSaveBtn();
        //    detailPartyPage
        //        .ClickOnSitesTab()
        //        .WaitForLoadingIconToDisappear();
        //    List<SiteModel> siteModel = detailPartyPage
        //        .GetAllSiteInList();
        //    allSiteModel.Add(siteModel[0]);
        //}

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
                .InputLienceNumberExField(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
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
                .ClickMapTabAndVerifyMessage("No Service Unit(s) associated to this Site ")
                .ClickSaveAndCloseBtn();
        }

        //[Category("WB")]
        //[Test(Description = "WB Station"), Order(5)]
        //public void TC_048_WB_Station()
        //{
        //    string stationName = "AutoStation" + CommonUtil.GetRandomNumber(2);
        //    //Verify data in TC45, 46, 47 not apprear in WB Site
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption("Weighbridge")
        //        .ExpandOption("North Star Commercial")
        //        .OpenOption("Sites")
        //        .SwitchNewIFrame();
        //    SiteListingPage siteListingPage = PageFactoryManager.Get<SiteListingPage>();
        //    List<SiteModel> siteModelsAfter = siteListingPage
        //        .GetAllSiteDisplayed();
        //    siteListingPage
        //        .VerifySiteCreatedIsNotDisplayed(siteModelsAfter, allSiteModel, siteModelBefore)
        //    //Back to the party customer in TC045
        //        .SwitchToDefaultContent();
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption("Parties")
        //        .ExpandOption("North Star Commercial")
        //        .OpenOption("Parties")
        //        .SwitchNewIFrame();
        //    PageFactoryManager.Get<PartyCommonPage>()
        //        .FilterPartyById(Int32.Parse(partyIdCustomer))
        //        .OpenFirstResult()
        //        .SwitchToLastWindow();
        //    DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
        //    detailPartyPage
        //        .ClickOnSitesTab()
        //        .WaitForLoadingIconToDisappear();
        //    detailPartyPage
        //        .OpenFirstSiteRow()
        //        .SwitchToLastWindow();
        //    SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
        //    siteDetailPage
        //        .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, siteName45 + " / " + addressAdded45)
        //        .ClickStationTab()
        //        .WaitForLoadingIconToDisappear();
        //    siteDetailPage
        //        .ClickAddNewItem()
        //        .SwitchToLastWindow();
        //    CreateStationPage createStationPage = PageFactoryManager.Get<CreateStationPage>();
        //    createStationPage
        //        .WaitForLoadingIconToDisappear();
        //    createStationPage
        //        .WaitForCreateStationPageLoaded("WEIGHBRIDGE STATION")
        //        .IsCreateStationPage()
        //        .ClickSaveBtn();
        //    createStationPage
        //        .VerifyDisplayErrorMesMissingTimezone()
        //        .SelectTimezone("Europe/London")
        //        .ClickSaveBtn();
        //    //Missing message error name input
        //    createStationPage
        //        .InputName(stationName)
        //        .ClickSaveBtn()
        //        .WaitForLoadingIconToDisappear()
        //        .VerifyToastMessage("Successfully saved Weighbridge Station");
        //    createStationPage
        //        .SelectDefaultTicket("Incoming")
        //        .ClickSaveBtn()
        //        .WaitForLoadingIconToDisappear()
        //        .VerifyToastMessage("Successfully saved Weighbridge Station")
        //        .ClickCloseBtn()
        //        .SwitchToChildWindow(3);
        //    siteDetailPage
        //        .ClickStationTab()
        //        .ClickSaveBtn()
        //        .WaitForLoadingIconToDisappear()
        //        .VerifyToastMessage("Successfully saved Site")
        //        .ClickSaveAndCloseBtn()
        //        .SwitchToChildWindow(2);
        //    detailPartyPage
        //        .ClickCloseBtn()
        //        .SwitchToChildWindow(1)
        //        .SwitchNewIFrame()
        //        .SwitchToDefaultContent();
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption("Weighbridge")
        //        .ExpandOption("North Star Commercial")
        //        .OpenOption("Sites")
        //        .SwitchNewIFrame();
        //    List<SiteModel> siteModelsNew = siteListingPage
        //        .GetAllSiteDisplayed();
        //    siteListingPage
        //        .VerifyDisplayNewSite(siteModel045[0], siteModelsNew[0]);
        //}

        //[Category("WB")]
        //[Test(Description = "WB VCH Human"), Order(6)]
        //public void TC_050_WB_VCH_Human()
        //{
        //    string resourceName = "Auto WB " + CommonUtil.GetRandomNumber(2);
        //    string resourceType = "Driver";

        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption("Resources")
        //        .OpenOption("North Star Commercial")
        //        .SwitchNewIFrame();
        //    PageFactoryManager.Get<CommonBrowsePage>()
        //        .ClickAddNewItem()
        //        .SwitchToLastWindow();
        //    PageFactoryManager.Get<ResourceDetailTab>()
        //        .IsOnDetailTab()
        //        .InputResourceName(resourceName)
        //        .SelectResourceType(resourceType)
        //        .ClickSaveBtn()
        //        .WaitForLoadingIconToDisappear()
        //        .VerifyToastMessage("Successfully saved resource.")
        //        .ClickCloseBtn()
        //        .SwitchToChildWindow(1)
        //        .SwitchNewIFrame()
        //        .SwitchToDefaultContent();
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption("Resources")
        //        .ExpandOption("North Star Commercial")
        //        .OpenOption("Vehicle_Customer_Haulier")
        //        .SwitchNewIFrame();
        //    PageFactoryManager.Get<VehicleCustomerHaulierPage>()
        //        .VerifyVehicleCustomerHaulierPageDisplayed()
        //        .ClickAddNewItemBtn()
        //        .SwitchToLastWindow();
        //    PageFactoryManager.Get<CreateVehicleCustomerHaulierPage>()
        //        .IsCreateVehicleCustomerHaulierPage()
        //        .VerifyDefaultMandatoryField()
        //        //Input party customer from TC045
        //        .InputCustomer(partyName045)
        //        //Input party haulier from TC047
        //        .InputHaulier(partyName047)
        //        //Input human resource name
        //        .InputHumanResourceName(resourceName)
        //        //Verify not display suggestion
        //        .VerifyNotDisplaySuggestionInResourceInput()
        //        .ClickSaveBtn();
        //    PageFactoryManager.Get<CreateVehicleCustomerHaulierPage>()
        //        .VerifyDisplayResourceRequiredMessage();

        //}

        [Category("WB")]
        [Test(Description = "WB VCH Human"), Order(7)]
        public void TC_051_WB_VCH_Vehicle()
        {
            string resourceName = "Auto WB Van" + CommonUtil.GetRandomNumber(2);
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
                .VerifyToastMessage("Successfully saved resource.")
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
                .ClickOnVehicleTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyTableDisplayedInVehicle()
                .ClickAddNewVehicleBtn()
                .SwitchToLastWindow();
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
                .VerifyToastMessage("Weighbridge Vehicle Customer Haulier Successfully registered")
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
    }
}
