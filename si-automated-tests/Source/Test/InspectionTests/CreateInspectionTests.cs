using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.Inspections;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using InspectionModel = si_automated_tests.Source.Main.Models.InspectionModel;

namespace si_automated_tests.Source.Test.InspectionTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateInspectionTests : BaseTest
    {
        private string allocatedUnitValue = "Ancillary";
        private string assignedUserValue = "josie";

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Creating inspection from task")]
        public void TC_079_Create_inspection_from_task()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            string inspectionTypeValue = "Site Inspection";
            string allocatedUnitValue_2 = "West Waste";
            string allocatedUserValue_2 = "";
            string noteValue = "AutoNote" + CommonUtil.GetRandomString(5);
            string validFromInThePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -2);
            string validToInThePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -1);

            ////Procedue
            //SqlCommand command = new SqlCommand("GetCreateInspectionLists", DatabaseContext.Conection);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlDataReader reader = command.ExecuteReader();
            //List<WBSiteProduct> products = ObjectExtention.DataReaderMapToList<WBSiteProduct>(reader);
            //Get data in DB to verify

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RM)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage();
            string location = detailTaskPage
                .GetLocationName();
            string serviceName = detailTaskPage
                .GetServiceName();
            string[] sourceNameList = { location, location };
            detailTaskPage
                .ClickOnInspectionBtn()
                .IsInspectionPopup()
                .VerifyDefaultValue(location)
                .ClickAndVerifySourceDd(sourceNameList)
                .ClickInspectionTypeDdAndSelectValue(inspectionTypeValue)
                .ClickAllocatedUnitAndSelectValue(allocatedUnitValue)
                .ClickAllocatedUserAndSelectValue(assignedUserValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage)
                .ClickOnSuccessLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(location)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1));
            //Get inspection Id
            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));
            //Query to verify
            string query_1 = "select u.username , c.contractunit , inspec.note , inspec.inspectioninstance, inspec.inspectionvaliddate, inspec.inspectionexpirydate from inspections inspec join users u on inspec.inspectioncreateduserID = u.userID join contractunits c on inspec.contractunitID = c.contractunitID where inspectionID = " + inspectionId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<InspectionQueryModel> inspections = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection);
            readerInspection.Close();
            string query_2 = "select u.username from inspections inspec join users u on inspec.userID = u.userID where inspectionID =" + inspectionId + "; ";
            SqlCommand commandInspection_2 = new SqlCommand(query_2, DbContext.Connection);
            SqlDataReader readerInspection_2 = commandInspection_2.ExecuteReader();
            List<InspectionQueryModel> inspections_2 = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection_2);
            readerInspection_2.Close();

            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, allocatedUnitValue, 0, AutoUser14.UserName, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_MM_DD_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 1), inspections_2[0].username, assignedUserValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailTaskPage
                .ClickInspectionTab()
                .WaitForLoadingIconToDisappear();
            List<InspectionModel> allInspectionModels = detailTaskPage
                .getAllInspection();
            detailTaskPage
                .VerifyInspectionCreated(allInspectionModels[0], inspectionId.ToString(), inspectionTypeValue, AutoUser14.DisplayName, assignedUserValue, allocatedUnitValue, "Pending", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT) + " 00:00", CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1) + " 00:00");
            //Bug => Add new item
            detailTaskPage
                .ClickAddNewInspectionItem()
                .IsInspectionPopup()
                .VerifyDefaultValue(location)
                .ClickAndVerifySourceDd(sourceNameList)
                .ClickInspectionTypeDdAndSelectValue(inspectionTypeValue)
                .ClickAllocatedUnitAndSelectValue(allocatedUnitValue_2)
                //.ClickAllocatedUserAndSelectValue(allocatedUserValue_2)
                //Valid From and Valid To are the past
                .InputValidFrom(validFromInThePast)
                .InputValidTo(validToInThePast)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage)
                .ClickOnSuccessLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            int inspectionId_2 = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));
            PageFactoryManager.Get<DetailInspectionPage>()
                //.IsDetailInspectionPage(allocatedUnitValue_2, allocatedUserValue_2, noteValue)
                .IsDetailInspectionPage(allocatedUnitValue_2, "Select...", noteValue)
                .VerifyStateInspection("Expired")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            allInspectionModels = detailTaskPage
                .getAllInspection();
            detailTaskPage
               .VerifyInspectionCreated(allInspectionModels[0], inspectionId_2.ToString(), inspectionTypeValue, AutoUser14.DisplayName, allocatedUserValue_2, allocatedUnitValue_2, "Expired", validFromInThePast + " 00:00", validToInThePast + " 00:00")
               //Clik First Inspection Row
               .ClickOnFirstInspection()
               .SwitchToLastWindow()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                //.VerifyAllFieldsInPopupAndDisabled(allocatedUnitValue_2, allocatedUserValue_2, noteValue, validFromInThePast + " 00:00", validToInThePast + " 00:00")
                .VerifyAllFieldsInPopupAndDisabled(allocatedUnitValue_2, "Select...", noteValue, validFromInThePast + " 00:00", validToInThePast + " 00:00")
                .VerifyStateInspection("Expired");

            //Get sourceId
            List<InspectionDBModel> inspectionNew = finder.GetInspectionById(inspectionId_2);

            PageFactoryManager.Get<DetailInspectionPage>()
                .ClickAddressLinkAndVerify(location, inspectionNew[0].echoID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailTaskPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            PageFactoryManager.Get<TasksListingPage>()
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId_2.ToString())
                .getAllInspectionInList(1);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(allInspectionModels[0], inspectionModels[0], location, Contract.RM, location, serviceName);
        }

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Creating inspection from event")]
        public void TC_080_Create_inspection_from_event()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string inspectionTypeValue = "Site Inspection";
            string eventIdWithIcon = AutoUser14.EventIDWithIcon;
            string[] sourceValueWithIcon = { "Select..." };
            string eventIdWithoutIcon = AutoUser14.EventIDWithoutIcon;
            string validFromValue = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            string validToValue = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 3);
            string noteValue = "AutoNote " + CommonUtil.GetRandomString(5);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.RM)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventsListingPage>()
                .FilterByEventId(eventIdWithIcon)
                //Click row with icon
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            PageFactoryManager.Get<EventDetailPage>()
                .WaitForEventDetailDisplayed()
                .ClickInspectionBtn()
                .IsCreateInspectionPopup(true)
                .VerifyDefaulValue(true)
                .ClickSourceDdAndVerify(sourceValueWithIcon)
                //Cancel btn
                .ClickCancelBtn()
                .VerifyPopupDisappears()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Click row without icon
            PageFactoryManager.Get<EventsListingPage>()
                .ClickClearBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventsListingPage>()
                .FilterByEventId(eventIdWithoutIcon)
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            PageFactoryManager.Get<EventDetailPage>()
                .WaitForEventDetailDisplayed();

            string locationValueWithoutIcon = PageFactoryManager.Get<EventDetailPage>()
                .GetLocationName();
            PageFactoryManager.Get<EventDetailPage>()
                .ClickInspectionBtn()
                .IsCreateInspectionPopup(false)
                .VerifyDefaulValue(false)
                .VerifyDefaultSourceDd(locationValueWithoutIcon)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .ClickAndSelectAssignedUser(assignedUserValue)
                //Change valid From and valid To to future dates
                .InputValidFrom(validFromValue)
                .InputValidTo(validToValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage)
                .ClickOnSuccessLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValueWithoutIcon)
                .VerifyValidFromValidToAndOtherDateField(validFromValue, validToValue)
                .ClickOnDataTab()
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", validFromValue, validToValue);
            //Get inspection Id
            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));
            //Query to verify
            string query_1 = "select u.username , c.contractunit , inspec.note , inspec.inspectioninstance, inspec.inspectionvaliddate, inspec.inspectionexpirydate from inspections inspec join users u on inspec.inspectioncreateduserID = u.userID join contractunits c on inspec.contractunitID = c.contractunitID where inspectionID = " + inspectionId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<InspectionQueryModel> inspections = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection);
            readerInspection.Close();
            string query_2 = "select u.username from inspections inspec join users u on inspec.userID = u.userID where inspectionID =" + inspectionId + "; ";
            SqlCommand commandInspection_2 = new SqlCommand(query_2, DbContext.Connection);
            SqlDataReader readerInspection_2 = commandInspection_2.ExecuteReader();
            List<InspectionQueryModel> inspections_2 = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection_2);
            readerInspection_2.Close();

            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, allocatedUnitValue, 0, AutoUser14.UserName, CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 2), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 3), inspections_2[0].username, assignedUserValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<EventDetailPage>()
                .ClickRefreshEventDetailBtn()
                .WaitForLoadingIconToDisappear();
            //Verify in Point History tab
            PageFactoryManager.Get<EventDetailPage>()
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModels = PageFactoryManager.Get<EventDetailPage>()
                .FilterByPointHistoryId(inspectionId.ToString())
                .GetAllPointHistory();

            PageFactoryManager.Get<EventDetailPage>()
                .VerifyPointHistory(pointHistoryModels[0], "Inspection:Site Inspection", inspectionId.ToString(), "Inspection", "Clinical Waste", locationValueWithoutIcon, validFromValue, validToValue, "Pending")
                .DoubleClickOnCreatedInspection()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyAllFieldsInPopupAndDisabled(allocatedUnitValue, assignedUserValue, noteValue, validFromValue + " 00:00", validToValue + " 00:00")
                .VerifyStateInspection("Pending");
            //Query
            List<InspectionDBModel> inspectionNew = finder.GetInspectionById(inspectionId);

            //Verify
            PageFactoryManager.Get<DetailInspectionPage>()
                .ClickServiceUnitLinkAndVerify(locationValueWithoutIcon, inspectionNew[0].echoID.ToString())
                .SwitchToChildWindow(3)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<EventDetailPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            PageFactoryManager.Get<TasksListingPage>()
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId.ToString())
                .getAllInspectionInList(1);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(pointHistoryModels[0], inspectionModels[0], locationValueWithoutIcon, Contract.RM, locationValueWithoutIcon, "Clinical Waste", AutoUser14.DisplayName, assignedUserValue, allocatedUnitValue);
        }

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Creating inspection from service unit")]
        public void TC_081_Create_inspection_from_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string inspectionTypeValue = "Site Inspection";
            string eventIdWithoutIcon = "13";
            string noteValue = "Auto081 " + CommonUtil.GetRandomString(5);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.RM)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventsListingPage>()
                .FilterByEventId(eventIdWithoutIcon)
                //Click row with icon
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            PageFactoryManager.Get<EventDetailPage>()
                .WaitForEventDetailDisplayed();
            string locationValue = PageFactoryManager.Get<EventDetailPage>()
                .GetLocationName();
            //Click source hyperlink
            PageFactoryManager.Get<EventDetailPage>()
                .ClickOnSourceHyperlink(locationValue)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForServiceUnitDetailPageDisplayed(locationValue)
                //Click inspect btn
                .ClickOnInspectionBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                //Click [x] in popup
                .ClickClosePopupBtn()
                .VerifyPopupDisappears()
                //Click [Cancel] btn
                .ClickOnInspectionBtn()
                .IsCreateInspectionPopup()
                .ClickCancelBtn()
                .VerifyPopupDisappears()
                //Fill all mandatory field
                .ClickOnInspectionBtn()
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputValidTo(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickAndSelectAssignedUser(assignedUserValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                //Bug: not display link => Fixed: 7/7/2022
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage)
                .ClickOnSuccessLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();

            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));

            PageFactoryManager.Get<DetailInspectionPage>()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            //Verify detail tab 
            PageFactoryManager.Get<DetailInspectionPage>()
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1));
            //Get data in DB to verify
            List<InspectionDBModel> inspections = finder.GetInspectionById(inspectionId);

            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, 5, 0, 2, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_MM_DD_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 1))
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<EventDetailPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId.ToString())
                .getAllInspectionInList(1);

            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(inspectionModels[0], locationValue, Contract.RM, locationValue, "Bulky Collections", AutoUser14.DisplayName, assignedUserValue, allocatedUnitValue, inspectionId.ToString(), inspectionTypeValue, "Pending", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                //Click on header
                .ClickServiceUnitLinkAndVerify(locationValue, inspections[0].echoID.ToString());
        }

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Creating inspection from point address")]
        public void TC_083_Create_inspection_from_point_address()
        {
            string inspectionTypeValue = "Repeat Missed Assessment";
            string noteValue = "AutoTC083 " + CommonUtil.GetRandomString(5);
            string searchForAddresses = "Addresses";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForAddresses)
                .ClickAndSelectRichmondCommercialSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<PointAddressListingPage>()
                .WaitForPointAddressPageDisplayed()
                .DoubleClickFirstPointAddressRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string locationValue = PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .GetPointAddressName();
            string idPointAddress = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/point-addresses/", "");

            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputValidTo(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickAndSelectAssignedUser(assignedUserValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();

            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));

            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1));
            //Query to verify
            string query_1 = "select u.username , c.contractunit , inspec.note , inspec.inspectioninstance, inspec.inspectionvaliddate, inspec.inspectionexpirydate from inspections inspec join users u on inspec.inspectioncreateduserID = u.userID join contractunits c on inspec.contractunitID = c.contractunitID where inspectionID = " + inspectionId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<InspectionQueryModel> inspections = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection);
            readerInspection.Close();
            string query_2 = "select u.username from inspections inspec join users u on inspec.userID = u.userID where inspectionID =" + inspectionId + "; ";
            SqlCommand commandInspection_2 = new SqlCommand(query_2, DbContext.Connection);
            SqlDataReader readerInspection_2 = commandInspection_2.ExecuteReader();
            List<InspectionQueryModel> inspections_2 = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection_2);
            readerInspection_2.Close();

            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, allocatedUnitValue, 0, AutoUser14.UserName, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_MM_DD_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 1), inspections_2[0].username, assignedUserValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Verify in [All Inspections]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId.ToString())
                .getAllInspectionInList(2);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(inspectionModels[0], locationValue, Contract.RM, locationValue, "", AutoUser14.DisplayName, assignedUserValue, allocatedUnitValue, inspectionId.ToString(), inspectionTypeValue, "Pending", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                //Click on header
                .ClickAddressLink(locationValue)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .VerifyPointAddressId(idPointAddress)
                //Verify data in [Point History] tab
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModels = PageFactoryManager.Get<PointAddressDetailPage>()
                .FilterByPointHistoryId(inspectionId.ToString())
                .GetAllPointHistory();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .VerifyPointHistory(pointHistoryModels[0], "Inspection:" + inspectionTypeValue, inspectionId.ToString(), "Inspection", locationValue, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), "Pending");
        }

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Creating inspection from point segment")]
        public void TC_086_Create_inspection_from_point_segment()
        {
            string searchForSegments = "Segments";
            string inspectionTypeValue = "Street Cleansing Assessment";
            string noteValue = "AutoTC086 " + CommonUtil.GetRandomString(5);
            string idSegment = "32844";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForSegments)
                .ClickAndSelectRichmondCommercialSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Filter segment with id
            PageFactoryManager.Get<PointSegmentListingPage>()
                .WaitForPointSegmentsPageDisplayed()
                .FilterSegmentById(idSegment)
                .DoubleClickFirstPointSegmentRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PointSegmentDetailPage>()
                .WaitForPointSegmentDetailPageDisplayed()
                //Click [Inspect] btn
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();

            string locationValue = PageFactoryManager.Get<PointSegmentDetailPage>()
                .GetPointSegmentName();
            string idPointSegment = PageFactoryManager.Get<PointSegmentDetailPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/point-segments/", "");
            PageFactoryManager.Get<PointSegmentDetailPage>()
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputValidTo(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickAndSelectAssignedUser(assignedUserValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                //Bug wrong message
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);

            PageFactoryManager.Get<PointSegmentDetailPage>()
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));

            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1));
            //Get data in DB to verify
            //Query to verify
            string query_1 = "select u.username , c.contractunit , inspec.note , inspec.inspectioninstance, inspec.inspectionvaliddate, inspec.inspectionexpirydate from inspections inspec join users u on inspec.inspectioncreateduserID = u.userID join contractunits c on inspec.contractunitID = c.contractunitID where inspectionID = " + inspectionId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<InspectionQueryModel> inspections = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection);
            readerInspection.Close();
            string query_2 = "select u.username from inspections inspec join users u on inspec.userID = u.userID where inspectionID =" + inspectionId + "; ";
            SqlCommand commandInspection_2 = new SqlCommand(query_2, DbContext.Connection);
            SqlDataReader readerInspection_2 = commandInspection_2.ExecuteReader();
            List<InspectionQueryModel> inspections_2 = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection_2);
            readerInspection_2.Close();

            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, allocatedUnitValue, 0, AutoUser14.UserName, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_MM_DD_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 1), inspections_2[0].username, assignedUserValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<PointSegmentDetailPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Verify in [All Inspections]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId.ToString())
                .getAllInspectionInList(1);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(inspectionModels[0], locationValue, Contract.RM, locationValue, "", AutoUser14.DisplayName, assignedUserValue, allocatedUnitValue, inspectionId.ToString(), inspectionTypeValue, "Pending", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                //Click on header
                .ClickAddressLink(locationValue)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PointSegmentDetailPage>()
                .WaitForPointSegmentDetailPageDisplayed()
                .VerifyPointSegmentId(idPointSegment)
                //Verify data in [Point History] tab
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModels = PageFactoryManager.Get<PointSegmentDetailPage>()
                .FilterByPointHistoryId(inspectionId.ToString())
                .GetAllPointHistory();
            PageFactoryManager.Get<PointSegmentDetailPage>()
                .VerifyPointHistory(pointHistoryModels[0], "Inspection:" + inspectionTypeValue, inspectionId.ToString(), "Inspection", locationValue, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), "Pending");
        }

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Creating inspection from point area")]
        public void TC_087_Create_inspection_from_point_area()
        {
            string searchForAreas = "Areas";
            string inspectionTypeValue = "Grounds Maintenance Assessment";
            string noteValue = "AutoTC087 " + CommonUtil.GetRandomString(5);
            string idArea = "17";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForAreas)
                .ClickAndSelectRichmondCommercialSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Filter area with id
            PageFactoryManager.Get<PointAreaListingPage>()
                .WaitForPointAreaListingPageDisplayed()
                .FilterAreaById(idArea)
                .DoubleClickFirstPointAreaRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Point Area detail
            PageFactoryManager.Get<PointAreaDetailPage>()
                .WaitForAreaDetailDisplayed()
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();

            string locationValue = PageFactoryManager.Get<PointAreaDetailPage>()
                .GetPointAreaName();
            PageFactoryManager.Get<PointAreaDetailPage>()
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputValidTo(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickAndSelectAssignedUser(assignedUserValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                //Bug wrong message
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            PageFactoryManager.Get<PointAreaDetailPage>()
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));

            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1));
            //Query to verify
            string query_1 = "select u.username , c.contractunit , inspec.note , inspec.inspectioninstance, inspec.inspectionvaliddate, inspec.inspectionexpirydate from inspections inspec join users u on inspec.inspectioncreateduserID = u.userID join contractunits c on inspec.contractunitID = c.contractunitID where inspectionID = " + inspectionId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<InspectionQueryModel> inspections = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection);
            readerInspection.Close();
            string query_2 = "select u.username from inspections inspec join users u on inspec.userID = u.userID where inspectionID =" + inspectionId + "; ";
            SqlCommand commandInspection_2 = new SqlCommand(query_2, DbContext.Connection);
            SqlDataReader readerInspection_2 = commandInspection_2.ExecuteReader();
            List<InspectionQueryModel> inspections_2 = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection_2);
            readerInspection_2.Close();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, allocatedUnitValue, 0, AutoUser14.UserName, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_MM_DD_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 1), inspections_2[0].username, assignedUserValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<PointSegmentDetailPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();

            //Verify in [All Inspections]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId.ToString())
                .getAllInspectionInList(1);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(inspectionModels[0], locationValue, Contract.RM, locationValue, "", AutoUser14.DisplayName, assignedUserValue, allocatedUnitValue, inspectionId.ToString(), inspectionTypeValue, "Pending", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                //Click on header
                .ClickAddressLink(locationValue)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PointAreaDetailPage>()
                .WaitForAreaDetailDisplayed()
                .VerifyPointAreaId(idArea)
                //Verify data in [Point History] tab
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModels = PageFactoryManager.Get<PointAreaDetailPage>()
                .FilterByPointHistoryId(inspectionId.ToString())
                .GetAllPointHistory();
            PageFactoryManager.Get<PointAreaDetailPage>()
                .VerifyPointHistory(pointHistoryModels[0], "Inspection:" + inspectionTypeValue, inspectionId.ToString(), "Inspection", locationValue, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), "Pending");

        }

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Creating inspection from point node")]
        public void TC_088_Create_inspection_from_point_node()
        {
            string searchForPointNodes = "Nodes";
            string inspectionTypeValue = "Site Inspection";
            string noteValue = "AutoTC088 " + CommonUtil.GetRandomString(5);
            string idNode = "3";
            string locationValue = "Sainsburys Hampton Bring Site";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser45.UserName, AutoUser45.Password)
                .IsOnHomePage(AutoUser45);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForPointNodes)
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<PointNodeListingPage>()
                .WaitForPointNodeListingPageDisplayed()
                .FilterNodeById(idNode)
                .DoubleClickFirstPointNodeRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Detail node page
            PageFactoryManager.Get<PointNodeDetailPage>()
                .WaitForPointNodeDetailDisplayed()
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PointAreaDetailPage>()
               .IsCreateInspectionPopup()
               .VerifyDefaulValue()
               .VerifyDefaultSourceDd(locationValue)
               .ClickAndSelectInspectionType(inspectionTypeValue)
               .ClickAndSelectAllocatedUnit(allocatedUnitValue)
               .InputValidTo(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
               .ClickAndSelectAssignedUser(assignedUserValue)
               .InputNote(noteValue)
               .ClickCreateBtn()
               //Bug wrong message
               .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            PageFactoryManager.Get<PointNodeDetailPage>()
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));
            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser45.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1));
            //Query to verify
            string query_1 = "select u.username , c.contractunit , inspec.note , inspec.inspectioninstance, inspec.inspectionvaliddate, inspec.inspectionexpirydate from inspections inspec join users u on inspec.inspectioncreateduserID = u.userID join contractunits c on inspec.contractunitID = c.contractunitID where inspectionID = " + inspectionId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<InspectionQueryModel> inspections = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection);
            readerInspection.Close();
            string query_2 = "select u.username from inspections inspec join users u on inspec.userID = u.userID where inspectionID =" + inspectionId + "; ";
            SqlCommand commandInspection_2 = new SqlCommand(query_2, DbContext.Connection);
            SqlDataReader readerInspection_2 = commandInspection_2.ExecuteReader();
            List<InspectionQueryModel> inspections_2 = ObjectExtention.DataReaderMapToList<InspectionQueryModel>(readerInspection_2);
            readerInspection_2.Close();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, allocatedUnitValue, 0, AutoUser45.UserName, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_MM_DD_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 1), inspections_2[0].username, assignedUserValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<PointNodeDetailPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();

            //Verify in [All Inspections]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId.ToString())
                .getAllInspectionInList(1);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(inspectionModels[0], locationValue, Contract.RM, locationValue, "", AutoUser45.DisplayName, assignedUserValue, allocatedUnitValue, inspectionId.ToString(), inspectionTypeValue, "Pending", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                //Verify detail tab 
                .IsDetailInspectionPage(allocatedUnitValue, assignedUserValue, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(locationValue)
                .VerifyValidFromValidToAndOtherDateField(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                .ClickOnDataTab()
                //Verify history tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser45.DisplayName, noteValue, allocatedUnitValue, assignedUserValue, "0", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1))
                //Click on header
                .ClickAddressLink(locationValue)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PointNodeDetailPage>()
                .WaitForPointNodeDetailDisplayed()
                .VerifyPointNodeId(idNode)
                //Verify data in [Point History] tab
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModels = PageFactoryManager.Get<PointNodeDetailPage>()
                .FilterByPointHistoryId(inspectionId.ToString())
                .GetAllPointHistory();
            PageFactoryManager.Get<PointNodeDetailPage>()
                .VerifyPointHistory(pointHistoryModels[0], "Inspection:" + inspectionTypeValue, inspectionId.ToString(), "Inspection", locationValue, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), "Pending");
        }
    }
}
