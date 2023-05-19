using NTextCat;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Common;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using SiteServiceUnitPage = si_automated_tests.Source.Main.Pages.Paties.Sites.ServiceUnitPage;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ServiceUnitPointTest : BaseTest
    {
        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "Verify that correct info displays in add Service Unit Point pop up")]
        public void TC_166_Service_Unit_Point()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password)
                .IsOnHomePage(AutoUser40);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .OpenOption("Active Service Units");
            ServiceUnitPage serviceUnit = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnit.SwitchToFrame(serviceUnit.UnitIframe);
            serviceUnit.WaitForLoadingIconToDisappear();
            serviceUnit.FindServiceUnitWithId("230038");
            serviceUnit.DoubleClickServiceUnitById("230038")
                       .SwitchToChildWindow(2);

            ServiceUnitDetailPage serviceUnitDetail = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceUnitPointTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddPointButton);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.AddServiceUnitPointDiv, true);
            serviceUnitDetail.VerifyRadioIsSelected()
                .VerifySelectedValue(serviceUnitDetail.SectorSelect, "Richmond")
                .ClickOnElement(serviceUnitDetail.AddServiceUnitPointCloseButton);
            serviceUnitDetail.ClickCloseBtn()
                .SwitchToFirstWindow();
            serviceUnit.ClickOnElement(serviceUnit.GetToogleButton(AutoUser40.DisplayName));
            serviceUnit.SelectByDisplayValueOnUlElement(serviceUnit.UserDropDown, "Locale Languages")
                .SwitchToChildWindow(2);
            LocalLanguagePage localLanguagePage = PageFactoryManager.Get<LocalLanguagePage>();
            localLanguagePage.SelectTextFromDropDown(localLanguagePage.LanguageSelect, "French")
                .ClickOnElement(localLanguagePage.SaveButton);
            localLanguagePage.SwitchToFirstWindow();
            PageFactoryManager.Get<NavigationBase>()
                 .ClickMainOption(MainOption.Services)
                 .ExpandOption("Régions")
                 .ExpandOption(Region.UK)
                 .ExpandOption(Contract.Commercial)
                 .ExpandOption("Collections")
                 .ExpandOption("Commercial Collections")
                 .OpenOption("Unités de service actives");
            serviceUnit.SwitchToFrame(serviceUnit.UnitIframe);
            serviceUnit.WaitForLoadingIconToDisappear();
            serviceUnit.FindServiceUnitWithId("230038");
            serviceUnit.DoubleClickServiceUnitById("230038")
                       .SwitchToChildWindow(2);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceUnitPointTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddPointButton);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.AddServiceUnitPointDiv, true);
            serviceUnitDetail.VerifyFrenchRadioIsSelected()
                .VerifySelectedValue(serviceUnitDetail.SectorSelect, "Richmond")
                .ClickOnElement(serviceUnitDetail.AddServiceUnitPointCloseButton);
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "The enddate is not getting set on some PointNodes affecting search for nodes in Add service unit points")]
        public void TC_183_The_enddate_is_not_getting_set_on_some_PointNodes()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password)
                .IsOnHomePage(AutoUser40);
            var homePage = PageFactoryManager.Get<HomePage>();
            homePage.ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption("Nodes")
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            var pointAddressListPage = PageFactoryManager.Get<PointAddressListingPage>();
            pointAddressListPage.DoubleClickPointAddress("2")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.ClientRefInput, "1478");
            pointAddressDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            //Click on add new item in the point node grid
            pointAddressListPage.ClickOnElement(pointAddressListPage.addNewPointAddressBtn);
            pointAddressListPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            string description = "Hiltribe, 18 Red lion street";
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.DescriptionInput, description);
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.ClientRefInput, "");
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.LatitudeInput, "51.4589021");
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.LongitudeInput, "-0.3070383");
            pointAddressDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            pointAddressListPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //pointAddressListPage.VerifyPointAddressHasEndDate(description);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/230012");
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.AddPointButton);
            serviceUnitPage.ClickOnElement(serviceUnitPage.NodeRadio);
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond Commercial");
            serviceUnitPage.SendKeys(serviceUnitPage.ClientRefInput, "1478");
            serviceUnitPage.ClickOnElement(serviceUnitPage.SearchButton);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifySearchResult("2")
                .CheckSearchResult("2");
            serviceUnitPage.ClickOnElement(serviceUnitPage.AddServiceUnitButton);
            serviceUnitPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyPointIdOnServiceUnitPointList("2");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "Service Unit map does not display serviced points")]
        public void TC_185_Service_Unit_map_does_not_display_serviced_points()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/230011");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyServiceUnitType("Both Serviced and Point of Service");
            serviceUnitPage.ClickOnElement(serviceUnitPage.MapTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.DragBluePointToAnotherPosition();
            serviceUnitPage.VerifyBlueAndRedPointVisible();

            //Click back to service unit points -> Change the type to Point of service -> Save -> Refresh the form
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectServiceUnitPointType("Point of Service")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyServiceUnitType("Point of Service");

            //Click map tab -> Drag blue pin away
            serviceUnitPage.ClickOnElement(serviceUnitPage.MapTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.DragBluePointToAnotherPosition();
            serviceUnitPage.VerifyBlueAndRedPointVisible();

            //Click back to service unit points -> Change the type to Serviced point -> Save -> Refresh the form
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectServiceUnitPointType("Serviced Point")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyServiceUnitType("Serviced Point");

            //Click map tab -> Drag blue pin away
            serviceUnitPage.ClickOnElement(serviceUnitPage.MapTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.DragBluePointToAnotherPosition();
            serviceUnitPage.VerifyBlueAndRedPointVisible();
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "selected street is cleared out from filter when changing the sectors")]
        public void TC_189_Service_Unit_form_Add_SUP()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/229673");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.AddPointButton);
            //Type the street ‘Church Terrace’ and select the street CHURCH TERRACE,TW10,RICHMOND and then select a sector East
            serviceUnitPage.SendKeys(serviceUnitPage.StreetInput, "Church Terrace");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectByDisplayValueOnUlElement(serviceUnitPage.StreetAutoCompleteTextBox, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "East");
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            //After switch back to sector from step 1 - Richmond
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "");
            //Type the street ‘Church Terrace’ and select the street CHURCH TERRACE,TW10,RICHMOND and then select a sector Richmond commercial
            serviceUnitPage.SendKeys(serviceUnitPage.StreetInput, "Church Terrace");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectByDisplayValueOnUlElement(serviceUnitPage.StreetAutoCompleteTextBox, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond Commercial");
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            //After switch back to sector from step 1 - Richmond
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "CHURCH TERRACE,TW10,RICHMOND");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_234_Cannot_remove_indicators_start_and_end_date_when_indicator_is_unset()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120501");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            servicesTaskPage.WaitForLoadingIconToDisappear();
            servicesTaskPage.SelectTextFromDropDown(servicesTaskPage.TaskIndicatorSelect, "Repeat Missed")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "01/01/2050");
            servicesTaskPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "01/01/2050");
            //Remove indicator end date -> Save
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorEndDateInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Indicator End Date is required")
                .WaitUntilToastMessageInvisible("Indicator End Date is required");
            //Remove indicator start date -> Save 
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorEndDateInput, "01/01/2050");
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorStartDateInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Indicator Start Date is required")
                .WaitUntilToastMessageInvisible("Indicator Start Date is required");
            //Set different indicator start and end date -> Save 
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorStartDateInput, londonCurrentDate.AddDays(2).ToString("dd/MM/yyyy"));
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorEndDateInput, londonCurrentDate.AddDays(30).ToString("dd/MM/yyyy"));
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Deselect task indicators -> Save
            servicesTaskPage.SelectTextFromDropDown(servicesTaskPage.TaskIndicatorSelect, "")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, "");
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "");
            servicesTaskPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, "");
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_270_AO_Intra_day_MultiRound_Optimisation()
        {
            //Verify whether Service form is updated to display new checkbox 
            PageFactoryManager.Get<LoginPage>()
              .GoToURL(WebUrl.MainPageUrl + "web/services/15");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            ServiceDetailPage serviceDetailPage = PageFactoryManager.Get<ServiceDetailPage>();
            serviceDetailPage.VerifyElementVisibility(serviceDetailPage.DynamicOptimisationLabel, true)
                .VerifyCheckboxIsSelected(serviceDetailPage.DynamicOptimisationCheckbox, false);
            //Verify whether question mark is added next to field (like on screenshot of Service Unit below)
            serviceDetailPage.ClickOnElement(serviceDetailPage.DynamicOptimisationHelpButton);
            serviceDetailPage.VerifyTooltip("Improve service efficiency by allowing automated task re-allocations between optimised round instances. If set to True, Tasks will only be re-allocated to round instances which are in the same service, are scheduled for the same day, and are configured to be able to perform that task.");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_309_Active_Services_tab()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/229631");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            string serviceUnit = serviceUnitPage.GetElementText(serviceUnitPage.ServiceUnitTitle);
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceTaskScheduleTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.AddServiceTaskButton);
            serviceUnitPage.SleepTimeInMiliseconds(200);
            serviceUnitPage.ClickOnElement(serviceUnitPage.CommercialCollectionOpt);
            serviceUnitPage.ClickOnElement(serviceUnitPage.CreateSTButton);
            serviceUnitPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            servicesTaskPage.ClickOnTaskLineTab();
            ServiceTaskLineTab serviceTaskLineTab = PageFactoryManager.Get<ServiceTaskLineTab>();
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            //a) Type = Service, Asset Type = 1100L, Sched Asset Qty = 3; Product = General Recycling, Sched Product Qty = 300.Save Service task
            string typeA = "Service";
            string AssetTypeA = "1100L";
            string SchedAssetQtyA = "3";
            string productA = "General Recycling";
            string schedProductQtyA = "300";
            serviceTaskLineTab.InputTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeA, assetType: AssetTypeA, shedAssetQty: SchedAssetQtyA, product: productA, shedProductQty: schedProductQtyA);
            serviceTaskLineTab.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.VerifyTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeA, assetType: AssetTypeA, scheduleAssetQty: SchedAssetQtyA, product: productA, sheduleProductQty: schedProductQtyA);

            //b) Type = Service, Asset Type = 1100L, Sched Asset Qty = 5.Save Service task
            string typeB = "Service";
            string AssetTypeB = "1100L";
            string SchedAssetQtyB = "5";
            serviceTaskLineTab.ClickOnElement(serviceTaskLineTab.AddNewItemButton);
            serviceTaskLineTab.InputTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeB, assetType: AssetTypeB, shedAssetQty: SchedAssetQtyB, product: "", shedProductQty: "");
            serviceTaskLineTab.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.VerifyTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeB, assetType: AssetTypeB, scheduleAssetQty: SchedAssetQtyB, product: "", sheduleProductQty: "");

            //c) Type = Service, Product = General Recycling, Sched Product Qty = 500.Save Service task
            string typeC = "Service";
            string productC = "General Recycling";
            string schedProductQtyC = "500";
            serviceTaskLineTab.ClickOnElement(serviceTaskLineTab.AddNewItemButton);
            serviceTaskLineTab.InputTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeC, assetType: "", shedAssetQty: "", product: productC, shedProductQty: schedProductQtyC);
            serviceTaskLineTab.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.VerifyTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeC, assetType: "", scheduleAssetQty: "", product: productC, sheduleProductQty: schedProductQtyC);

            //d) Type = Service, Asset Type = 660L.Save Service task
            string typeD = "Service";
            string AssetTypeD = "660L";
            serviceTaskLineTab.ClickOnElement(serviceTaskLineTab.AddNewItemButton);
            serviceTaskLineTab.InputTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeD, assetType: AssetTypeD, shedAssetQty: "", product: "", shedProductQty: "");
            serviceTaskLineTab.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.VerifyTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeD, assetType: AssetTypeD, scheduleAssetQty: "", product: "", sheduleProductQty: "");

            //e) Type = Service, Product = General Refuse.Save Service task
            string typeE = "Service";
            string productE = "General Refuse";
            serviceTaskLineTab.ClickOnElement(serviceTaskLineTab.AddNewItemButton);
            serviceTaskLineTab.InputTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeE, assetType: "", shedAssetQty: "", product: productE, shedProductQty: "");
            serviceTaskLineTab.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.VerifyTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeE, assetType: "", scheduleAssetQty: "", product: productE, sheduleProductQty: "");

            // f) Type = Service, Asset Type = 660L, Product = General Refuse.Save Service task
            string typeF = "Service";
            string AssetTypeF = "660L";
            string productF = "General Refuse";
            serviceTaskLineTab.ClickOnElement(serviceTaskLineTab.AddNewItemButton);
            serviceTaskLineTab.InputTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeF, assetType: AssetTypeF, shedAssetQty: "", product: productF, shedProductQty: "");
            serviceTaskLineTab.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.WaitForLoadingIconToDisappear();
            serviceTaskLineTab.VerifyTaskLine(serviceTaskLineTab.GetNewTaskLineIndex(), type: typeF, assetType: AssetTypeF, scheduleAssetQty: "", product: productF, sheduleProductQty: "");
            
            string postCode = serviceUnit.Split(',').Last().Trim();
            string pointAddress = serviceUnit.Split(',')[1].Trim();
            serviceTaskLineTab.SwitchToFirstWindow();
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/point-picker");
            PointPickerPage pointPickerPage = PageFactoryManager.Get<PointPickerPage>();
            pointPickerPage.WaitForLoadingIconToDisappear();
            pointPickerPage.SetPostCode(postCode)
                .ClickOnElement(pointPickerPage.SearchButton);
            pointPickerPage.WaitForLoadingIconToDisappear();
            pointPickerPage.SetInputValue(pointPickerPage.PointDescriptionInput, pointAddress);
            pointPickerPage.WaitForLoadingIconToDisappear();
            pointPickerPage.SelectPoint(pointAddress);
            pointPickerPage.WaitForLoadingIconToDisappear();
            string GetAssetType(string assetQty, string assetType, string product)
            {
                string productDisplay = string.IsNullOrEmpty(product) ? "" : $" ({product})";
                string assetTypeDisplay = string.IsNullOrEmpty(assetType) ? "" : $" x {assetType}";
                return (assetQty + assetTypeDisplay + productDisplay).Trim().TrimStart('x').Trim();
            }
            List<string> assetTypes = new List<string>()
            {
                GetAssetType(SchedAssetQtyA, AssetTypeA, productA),
                GetAssetType(SchedAssetQtyB, AssetTypeB, ""),
                GetAssetType("", "", productC),
                GetAssetType("", AssetTypeD, ""),
                GetAssetType("", "", productE),
                GetAssetType("", AssetTypeF, productF),
            };
            pointPickerPage.VerifyPointAddressAndClickEventButton(assetTypes, "Standard - Complaint");
            pointPickerPage.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            EventComplaintPage eventComplaintPage = PageFactoryManager.Get<EventComplaintPage>();
            eventComplaintPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            eventComplaintPage.WaitForLoadingIconToDisappear();
            eventComplaintPage.ClickOnElement(eventComplaintPage.ServiceTab);
            eventComplaintPage.WaitForLoadingIconToDisappear();
            eventComplaintPage.VerifyPointAddress(assetTypes)
                .CloseCurrentWindow();
            eventComplaintPage.SwitchToChildWindow(2);

            serviceTaskLineTab.ClickOnElement(serviceTaskLineTab.ServiceDesTitle);
            serviceTaskLineTab.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickPointAddress("5 CHURCH ROAD, TEDDINGTON, TW11 8PF");
            serviceTaskLineTab.SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();

            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage.ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();
            pointAddressDetailPage.VerifyPointAddress(assetTypes);
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_310_Service_Unit_Point_validation_in_UI()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/229719");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.DoubleClickServiceUnitPoint()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage.SelectTextFromDropDown(serviceUnitPointDetailPage.serviceUnitPointTypeDd, "")
                .ClickSaveBtn()
                .VerifyToastMessage("Service Unit Point Type is required")
                .WaitUntilToastMessageInvisible("Service Unit Point Type is required");

            //Select any value in 'Service Unit Point Type'  and click 'Save' on SUP form
            serviceUnitPointDetailPage.SelectAnyValueInServiceUnitPointType("Point of Service")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            //Verify that Type field  is mandatory on Service Unit form>Service Unit Points tab
            serviceUnitPointDetailPage.ClickCloseBtn()
                .SwitchToFirstWindow()
                .WaitForLoadingIconToDisappear();

            serviceUnitPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectServiceUnitPointType("")
                .ClickSaveBtn()
                .VerifyToastMessage("Service Unit Point Type is required")
                .WaitUntilToastMessageInvisible("Service Unit Point Type is required");

            //Select any value in 'Service Unit Point Type'  and click 'Save' on SUP form
            serviceUnitPage.SelectServiceUnitPointType("Both Serviced and Point of Service")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        }

        [Category("ActiveService")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_308_Active_Services_tab()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl + "web/point-picker");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            PointPickerPage pointPickerPage = PageFactoryManager.Get<PointPickerPage>();
            pointPickerPage.WaitForLoadingIconToDisappear();
            pointPickerPage.WaitForLoadingIconToDisappear();
            pointPickerPage.ClickOnElement(pointPickerPage.ServiceTab);
            pointPickerPage.WaitForLoadingIconToDisappear();
            pointPickerPage.VerifyElementVisibility(pointPickerPage.AssetTypeColumn, true);

            //1) Navigate to a Point Address> Active Services tab by enter following URL: https://test.echoweb.co.uk/web/point-addresses/363175
            //2) Click 'Active Services' tab
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl + "web/point-addresses/363175");
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage.WaitForLoadingIconToDisappear();
            pointAddressDetailPage.ClickOnElement(pointAddressDetailPage.ServiceTab);
            pointAddressDetailPage.WaitForLoadingIconToDisappear();
            pointAddressDetailPage.VerifyElementVisibility(pointAddressDetailPage.AssetTypeColumn, true);

            //1) Navigate to a Point Area> Active Services tab by enter following URL: https://test.echoweb.co.uk/web/point-areas/1
            //2) Click 'Active Services' tab
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(WebUrl.MainPageUrl + "web/point-areas/1");
            PointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<PointAreaDetailPage>();
            pointAreaDetailPage.WaitForLoadingIconToDisappear();
            pointAreaDetailPage.ClickOnElement(pointAreaDetailPage.ServiceTab);
            pointAreaDetailPage.WaitForLoadingIconToDisappear();
            pointAreaDetailPage.VerifyElementVisibility(pointAreaDetailPage.AssetTypeColumn, true);
            
            //1) Navigate to a Point Segment > Active Services tab by enter following URL: https://test.echoweb.co.uk/web/point-segments/32684
            //2) Click 'Active Services' tab
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(WebUrl.MainPageUrl + "web/point-segments/32684");
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage.WaitForLoadingIconToDisappear();
            pointSegmentDetailPage.ClickOnElement(pointSegmentDetailPage.ServiceTab);
            pointSegmentDetailPage.WaitForLoadingIconToDisappear();
            pointSegmentDetailPage.VerifyElementVisibility(pointSegmentDetailPage.AssetTypeColumn, true);
            
            //1) Navigate to a Point Node> Active Services tab by enter following URL: https://test.echoweb.co.uk/web/point-nodes/2
            //2) Click 'Active Services' tab"
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(WebUrl.MainPageUrl + "web/point-addresses/363175");
            PointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<PointNodeDetailPage>();
            pointNodeDetailPage.WaitForLoadingIconToDisappear();
            pointNodeDetailPage.ClickOnElement(pointNodeDetailPage.ServiceTab);
            pointNodeDetailPage.WaitForLoadingIconToDisappear();
            pointNodeDetailPage.VerifyElementVisibility(pointNodeDetailPage.AssetTypeColumn, true);
        }
    }
}
