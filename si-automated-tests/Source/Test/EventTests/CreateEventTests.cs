using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.DBModels.GetPointHistory;
using si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.EventTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateEventTests : BaseTest
    {
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from point address with service unit")]
        public void TC_094_Create_event_from_point_address_with_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string searchForAddresses = "Addresses";
            string eventOption = "Standard - Complaint";
            string pointAddressId = "363256";
            string eventType = "Complaint";
            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();

            //Check SP
            SqlCommand command = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 363256;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 1;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint = ObjectExtention.DataReaderMapToList<ServiceForPointDBModel>(reader);
                if (reader.NextResult())
                {
                    serviceTaskForPoint = ObjectExtention.DataReaderMapToList<ServiceTaskForPointDBModel>(reader);
                }
                if (reader.NextResult())
                {
                    commonService = ObjectExtention.DataReaderMapToList<CommonServiceForPointDBModel>(reader);
                }
            }

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForAddresses)
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<PointAddressListingPage>()
                .WaitForPointAddressPageDisplayed()
                .FilterPointAddressWithId(pointAddressId)
                .DoubleClickFirstPointAddressRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Get all point history in point history tab
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInDetail = pointAddressDetailPage
                .GetAllPointHistory();
            //Active service tab
            pointAddressDetailPage
                .ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();
            string locationValue = pointAddressDetailPage
                .GetPointAddressName();
            //Get all data in [Active Services] with Service unit
            List<ActiveSeviceModel> allAServicesWithServiceUnit = pointAddressDetailPage
                .GetAllServiceWithServiceUnitModel();
            List<ActiveSeviceModel> allServices = pointAddressDetailPage
                .GetAllServiceInTab();
            //Get all data in [Active Services] without Service unit
            List<ActiveSeviceModel> GetAllServiceWithoutServiceUnitModel = pointAddressDetailPage
                .GetAllServiceWithoutServiceUnitModel(allServices);
            //Verify data in [Active Service] tab with SP
            pointAddressDetailPage
                .VerifyDataInActiveServicesTab363256(allAServicesWithServiceUnit, serviceForPoint)
                .VerifyDataInActiveServicesTab(GetAllServiceWithoutServiceUnitModel, serviceForPoint);
            List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId = pointAddressDetailPage
                .FilterCommonServiceForPointWithServiceId(commonService, serviceForPoint[0].serviceID);
            pointAddressDetailPage
                .ClickFirstEventInFirstServiceRow()
                .VerifyEventTypeWhenClickEventBtn(FilterCommonServiceForPointWithServiceId)
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed();
            string serviceId = CommonUtil.GetBetween(eventDetailPage.GetCurrentUrl(), "serviceId=", "&serviceUnitId");

            //DB - get service and service group
            List<ServiceJoinServiceGroupDBModel> serviceDBs = finder.GetServiceAndServiceGroupInfo(int.Parse(serviceId));
            eventDetailPage
                .VerifyEventType(eventType)
                .VerifyServiceGroupAndService(serviceDBs[0].servicegroup, serviceDBs[0].service)
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Service - sub tab display without error
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();
            //DB - Get servicegroup with contractID = 2
            string query_servicegroup = "SELECT * FROM services s join servicegroups s2 on s.servicegroupID = s2.servicegroupID  WHERE s2.contractID = 2;";
            SqlCommand commandService = new SqlCommand(query_servicegroup, DbContext.Connection);
            SqlDataReader readerService = commandService.ExecuteReader();
            List<ServiceDBModel> serviceInDB = ObjectExtention.DataReaderMapToList<ServiceDBModel>(readerService);
            readerService.Close();
            List<ServiceForPointDBModel> filterServiceWithContract = eventDetailPage
                .FilterServicePointWithServiceID(serviceForPoint, serviceInDB);

            List<ActiveSeviceModel> activeSeviceWithUnitModelsInSubTab = eventDetailPage
                .GetAllActiveServiceWithServiceUnitModel();
            eventDetailPage
                .VerifyDataInServiceSubTab(allAServicesWithServiceUnit, filterServiceWithContract)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab = eventDetailPage
                .GetAllPointHistory();
            eventDetailPage
                .VerifyPointHistoryInSubTab(pointHistoryModelsInDetail, pointHistoryModelsInPointHistorySubTab);
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            string query_1 = "select * from events where eventid=" + eventId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<EventDBModel> eventModels = ObjectExtention.DataReaderMapToList<EventDBModel>(readerInspection);
            readerInspection.Close();
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", eventType, serviceUnit)
                .ExpandDetailToggle()
                //Step 22: Verify Source Desc in Detail toggle => [Source] field read only
                .VerifySourceInputReadOnly();
                
            //Check service unit link
            PageFactoryManager.Get<EventDetailPage>()
                .ClickOnLocation()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string serviceUnitId = PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForServiceUnitDetailPageDisplayed(serviceUnit)
                .GetServiceUnitId();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .VerifyServiceUnitId(eventModels[0].echoID.ToString(), serviceUnitId)
                .ClickCloseBtn();
        }

        //Done Bug: Cannot Click on [Source input]
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from point address without service unit")]
        public void TC_094_Create_event_from_point_address_without_service_unit()
        {
            string searchForAddresses = "Addresses";
            string pointAddressId = "483995";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForAddresses)
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<PointAddressListingPage>()
                .WaitForPointAddressPageDisplayed()
                .FilterPointAddressWithId(pointAddressId)
                .DoubleClickFirstPointAddressRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Get all point history in point history tab
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInDetail = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetAllPointHistory();
            //Active service tab
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();
            string locationValue = PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .GetPointAddressName();
            //Get all data in [Active Services]
            List<ActiveSeviceModel> allServices = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetAllServiceInTab();
            //Service = Skip
            ActiveSeviceModel activeSeviceModelWithSkip = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetActiveServiceWithSkipService(allServices);
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickAnyEventInActiveServiceRow(activeSeviceModelWithSkip.eventLocator)
                .VerifyToastMessage(MessageRequiredFieldConstants.NoEventsAvailableWarningMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.NoEventsAvailableWarningMessage);
            //Get all active service no service unit
            List<ActiveSeviceModel> allActiveServicesNoServiceUnit = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetAllServiceWithoutServiceUnitModel(allServices);
            List<string> allEventTypes = PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickAnyEventInActiveServiceRow(allActiveServicesNoServiceUnit[0].eventLocator)
                .GetAllEventTypeInDd();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickAnyEventOption(allEventTypes[0])
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            //Event detail
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyEventType(allEventTypes[0])
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            string dataNameValue = "Auto " + CommonUtil.GetRandomNumber(5);
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                .InputNameInDataTab(dataNameValue)
                //Verify Service - sub tab display without error
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();
            List<ActiveSeviceModel> allSeviceModelsInSubTab = eventDetailPage
                .GetAllServiceInTab();
            eventDetailPage
                //.VerifyDataInServiceSubTab(allActiveServices, allSeviceModelsInSubTab)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab = eventDetailPage
                .GetAllPointHistory();
            eventDetailPage
                .VerifyPointHistoryInSubTab(pointHistoryModelsInDetail, pointHistoryModelsInPointHistorySubTab);
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl()
                .VerifyDisplayBlueIcon();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            string query_1 = "select * from events where eventid=" + eventId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<EventDBModel> eventModels = ObjectExtention.DataReaderMapToList<EventDBModel>(readerInspection);
            readerInspection.Close();
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", allEventTypes[0], serviceUnit)
                //Check service unit link show popup
                .ClickOnLocationShowPopup()
                .VeriryDisplayPopupLinkEventToServiceUnit("Richmond")
                .ClickCloseEventPopupBtn()
                .ExpandDetailToggle()
                //Verify Source in Detail toggle => cannot click source input
                .VerifySourceInputReadOnly();
                
        }

        //Done Bug: Cannot Click on [Source input]
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from point segment with service unit")]
        public void TC_096_Create_event_from_point_segment_with_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string searchForSegments = "Segments";
            string idSegmentWithServiceUnit = "32839";
            string eventOption = "Standard - Complaint";
            string eventType = "Complaint";

            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();

            //Check SP
            SqlCommand command = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 32839;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 2;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint = ObjectExtention.DataReaderMapToList<ServiceForPointDBModel>(reader);
                if (reader.NextResult())
                {
                    serviceTaskForPoint = ObjectExtention.DataReaderMapToList<ServiceTaskForPointDBModel>(reader);
                }
                if (reader.NextResult())
                {
                    commonService = ObjectExtention.DataReaderMapToList<CommonServiceForPointDBModel>(reader);
                }
            }

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForSegments)
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Filter segment with id
            PageFactoryManager.Get<PointSegmentListingPage>()
                .WaitForPointSegmentsPageDisplayed()
                .FilterSegmentById(idSegmentWithServiceUnit)
                .DoubleClickFirstPointSegmentRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed();
            //Get all point history in point history tab
            pointSegmentDetailPage
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInDetail = pointSegmentDetailPage
                .GetAllPointHistory();
            //Active Service tab
            pointSegmentDetailPage
                .ClickOnActiveServiceTab()
                .WaitForLoadingIconToDisappear();
            string locationValue = pointSegmentDetailPage
                .GetPointSegmentName();
            List<ActiveSeviceModel> activeSeviceModelsDisplayed = pointSegmentDetailPage
                .GetAllActiveServiceInTab32839();
            //Verify Active service displayed with SPs
            pointSegmentDetailPage
                .VerifyActiveServiceDisplayedWithDB(activeSeviceModelsDisplayed, serviceForPoint);
            List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId = pointSegmentDetailPage
                .FilterCommonServiceForPointWithServiceId(commonService, serviceForPoint[0].serviceID);
            pointSegmentDetailPage
                .ClickFirstEventInFirstServiceRow()
                .VerifyEventTypeWhenClickEventBtn(FilterCommonServiceForPointWithServiceId)
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Event Detail page
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed();
            string serviceId = CommonUtil.GetBetween(eventDetailPage.GetCurrentUrl(), "serviceId=", "&serviceUnitId");

            //DB - get service and service group
            List<ServiceJoinServiceGroupDBModel> serviceDBs = finder.GetServiceAndServiceGroupInfo(int.Parse(serviceId));
            eventDetailPage
                .VerifyEventType(eventType)
                .VerifyServiceGroupAndService(serviceDBs[0].servicegroup, serviceDBs[0].service)
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();
            List<ActiveSeviceModel> activeSeviceModelsFullInfoSubTab = eventDetailPage
                .GetAllActiveServiceInTabFullInfo32839();

            eventDetailPage
                .VerifyDataInServiceSubTab(activeSeviceModelsDisplayed, activeSeviceModelsFullInfoSubTab)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab = eventDetailPage
                .GetAllPointHistory();
            eventDetailPage
                .VerifyPointHistoryInSubTab(pointHistoryModelsInDetail, pointHistoryModelsInPointHistorySubTab);
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            //DB - Get Event
            List<EventDBModel> eventModels = finder.GetEvent(int.Parse(eventId));
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", eventType, serviceUnit)
                .ExpandDetailToggle()
                //Verify Source in Detail toggle => Bug: Cannot Click [Source] input
                .VerifySourceInputReadOnly();
                
            //Check service unit link
            PageFactoryManager.Get<EventDetailPage>()
                .ClickOnLocation()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string serviceUnitId = PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForServiceUnitDetailPageDisplayed(serviceUnit)
                .GetServiceUnitId();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .VerifyServiceUnitId(eventModels[0].echoID.ToString(), serviceUnitId)
                .ClickCloseBtn();
        }

        //Done Bug: Cannot Click on [Source input]
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from point segment without service unit")]
        public void TC_096_Create_event_from_point_segment_without_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string searchForSegments = "Segments";
            string idSegmentWithoutServiceUnit = "32844";
            string eventOption = "Standard - Clear Flytip";
            string eventType = "Clear Flytip";

            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();

            //Check SP
            SqlCommand command = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 32844;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 2;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint = ObjectExtention.DataReaderMapToList<ServiceForPointDBModel>(reader);
                if (reader.NextResult())
                {
                    serviceTaskForPoint = ObjectExtention.DataReaderMapToList<ServiceTaskForPointDBModel>(reader);
                }
                if (reader.NextResult())
                {
                    commonService = ObjectExtention.DataReaderMapToList<CommonServiceForPointDBModel>(reader);
                }
            }

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForSegments)
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Filter segment with id
            PageFactoryManager.Get<PointSegmentListingPage>()
                .WaitForPointSegmentsPageDisplayed()
                .FilterSegmentById(idSegmentWithoutServiceUnit)
                .DoubleClickFirstPointSegmentRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed();
            pointSegmentDetailPage
                .ClickOnActiveServiceTab()
                .WaitForLoadingIconToDisappear();
            string locationValue = pointSegmentDetailPage
                .GetPointSegmentName();
            List<ActiveSeviceModel> activeSeviceModelsDisplayed = pointSegmentDetailPage
                .GetAllActiveServiceWithoutServiceUnit();
            //Verify Active service displayed with SPs
            pointSegmentDetailPage
                .VerifyActiveServiceWithoutServiceUnitDisplayedWithDB(activeSeviceModelsDisplayed, serviceForPoint);
            List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId = pointSegmentDetailPage
                .FilterCommonServiceForPointWithServiceId(commonService, serviceForPoint[0].serviceID);
            pointSegmentDetailPage
                .ClickFirstEventInFirstServiceRow()
                .VerifyEventTypeWhenClickEventBtn(FilterCommonServiceForPointWithServiceId)
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Event Detail page
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed();
            string serviceId = CommonUtil.GetBetween(eventDetailPage.GetCurrentUrl(), "serviceId=", "&serviceUnitId");
            //DB - get service and service group
            List<ServiceJoinServiceGroupDBModel> serviceDBs = finder.GetServiceAndServiceGroupInfo(int.Parse(serviceId));
            eventDetailPage
                .VerifyEventType(eventType)
                .VerifyServiceGroupAndService(serviceDBs[0].servicegroup, serviceDBs[0].service)
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();
            List<ActiveSeviceModel> activeSeviceModelsFullInfoSubTab = eventDetailPage
                .GetAllServiceInTab();

            eventDetailPage
                .VerifyDataInServiceSubTab(activeSeviceModelsDisplayed, activeSeviceModelsFullInfoSubTab)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl()
                .VerifyDisplayBlueIcon();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            //DB - Get Event
            List<EventDBModel> eventModels = finder.GetEvent(int.Parse(eventId));
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", eventType, serviceUnit)
                .ExpandDetailToggle()
                //Verify Source in Detail toggle => Bug: Cannot click on Source input
                .VerifySourceInputReadOnly();
            //Check service unit link
            eventDetailPage
                .ClickOnLocationShowPopup()
                .VeriryDisplayPopupLinkEventToServiceUnit("Richmond")
                .ClickCloseEventPopupBtn();
        }

        //Done Bug: Cannot Click on [Source input]
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from point node with service unit")]
        public void TC_097_Create_event_from_point_node_with_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string searchForNodes = "Nodes";
            string eventOption = "Standard - Additional Service Request";
            string pointNodeId = "6";
            string eventType = "Additional Service Request";
            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();
            //Check SP
            SqlCommand command = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 6;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 4;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint = ObjectExtention.DataReaderMapToList<ServiceForPointDBModel>(reader);
                if (reader.NextResult())
                {
                    serviceTaskForPoint = ObjectExtention.DataReaderMapToList<ServiceTaskForPointDBModel>(reader);
                }
                if (reader.NextResult())
                {
                    commonService = ObjectExtention.DataReaderMapToList<CommonServiceForPointDBModel>(reader);
                }
            }

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForNodes)
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<PointNodeListingPage>()
                .WaitForPointNodeListingPageDisplayed()
                .FilterNodeById(pointNodeId)
                .DoubleClickFirstPointNodeRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Detail node page
            PageFactoryManager.Get<PointNodeDetailPage>()
                .WaitForPointNodeDetailDisplayed()
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            PointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<PointNodeDetailPage>();
            List<PointHistoryModel> pointHistoryModelsInDetail = pointNodeDetailPage
                .GetAllPointHistory();
            pointNodeDetailPage
                .ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();
            string pointNodeName = pointNodeDetailPage
                .GetPointNodeName();
            //Get all [Active services]
            List<ActiveSeviceModel> allActiveServices = pointNodeDetailPage
                .GetAllServiceWithServiceUnitModel();
            //Verify Active service displayed with SPs
            pointNodeDetailPage
                .VerifyActiveServiceDisplayedWithDB(allActiveServices, serviceForPoint, serviceTaskForPoint);
            List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId = pointNodeDetailPage
                .FilterCommonServiceForPointWithServiceId(commonService, serviceForPoint[0].serviceID);
            pointNodeDetailPage
                .ClickFirstEventInFirstServiceRow()
                .VerifyEventTypeWhenClickEventBtn(FilterCommonServiceForPointWithServiceId)
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed();
            string serviceId = CommonUtil.GetBetween(eventDetailPage.GetCurrentUrl(), "serviceId=", "&serviceUnitId");

            //DB - get service and service group
            List<ServiceJoinServiceGroupDBModel> serviceDBs = finder.GetServiceAndServiceGroupInfo(int.Parse(serviceId));
            eventDetailPage
                .VerifyEventType(eventType)
                .VerifyServiceGroupAndService(serviceDBs[0].servicegroup, serviceDBs[0].service)
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(pointNodeName, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Service - sub tab display without error
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();

            List<ActiveSeviceModel> activeSeviceWithUnitModelsInSubTab = eventDetailPage
                .GetAllActiveServiceInTabFullInfo();
            eventDetailPage
                .VerifyDataInServiceSubTab(activeSeviceWithUnitModelsInSubTab, allActiveServices)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab = eventDetailPage
                .GetAllPointHistory();
            eventDetailPage
                .VerifyPointHistoryInSubTab(pointHistoryModelsInDetail, pointHistoryModelsInPointHistorySubTab);
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(pointNodeName, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            //DB get event info
            List<EventDBModel> eventModels = finder.GetEvent(int.Parse(eventId));
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", eventType, serviceUnit)
                .ExpandDetailToggle()
                //Verify Source in Detail toggle => Bug Cannot click Source Input
                .VerifySourceInputReadOnly();
                
            //Check service unit link
            PageFactoryManager.Get<EventDetailPage>()
                .ClickOnLocation()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string serviceUnitId = PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForServiceUnitDetailPageDisplayed(serviceUnit)
                .GetServiceUnitId();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .VerifyServiceUnitId(eventModels[0].echoID.ToString(), serviceUnitId)
                .ClickCloseBtn();
        }

        //Done Bug: Cannot Click on [Source input]
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from point area with service unit")]
        public void TC_098_Create_event_from_point_area_with_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string searchForAreas = "Areas";
            string eventOption = "Standard - Complaint";
            string pointAreaId = "10";
            string eventType = "Complaint";
            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();
            //Check SP
            SqlCommand command = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 10;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 3;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint = ObjectExtention.DataReaderMapToList<ServiceForPointDBModel>(reader);
                if (reader.NextResult())
                {
                    serviceTaskForPoint = ObjectExtention.DataReaderMapToList<ServiceTaskForPointDBModel>(reader);
                }
                if (reader.NextResult())
                {
                    commonService = ObjectExtention.DataReaderMapToList<CommonServiceForPointDBModel>(reader);
                }
            }

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForAreas)
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Filter area with id
            PageFactoryManager.Get<PointAreaListingPage>()
                .WaitForPointAreaListingPageDisplayed()
                .FilterAreaById(pointAreaId)
                .DoubleClickFirstPointAreaRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Point Area detail
            PageFactoryManager.Get<PointAreaDetailPage>()
                .WaitForAreaDetailDisplayed()
                .ClickPointHistoryTab()
                .WaitForLoadingIconToDisappear();
            PointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<PointAreaDetailPage>();
            List<PointHistoryModel> pointHistoryModelsInDetail = pointAreaDetailPage
                .GetAllPointHistory();
            pointAreaDetailPage
                .ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();
            string pointAreaName = pointAreaDetailPage
                .GetPointAreaName();
            //Get all [Active services]
            List<ActiveSeviceModel> allActiveServices = pointAreaDetailPage
                .GetAllServiceWithServiceUnitModel();
            //Verify Active service displayed with SPs
            pointAreaDetailPage
                .VerifyActiveServiceDisplayedWithDB(allActiveServices, serviceForPoint, serviceTaskForPoint);
            List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId = pointAreaDetailPage
                .FilterCommonServiceForPointWithServiceId(commonService, serviceForPoint[0].serviceID);
            pointAreaDetailPage
                .ClickFirstEventInFirstServiceRow()
                .VerifyEventTypeWhenClickEventBtn(FilterCommonServiceForPointWithServiceId)
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed();
            string serviceId = CommonUtil.GetBetween(eventDetailPage.GetCurrentUrl(), "serviceId=", "&serviceUnitId");

            //DB - get service and service group
            List<ServiceJoinServiceGroupDBModel> serviceDBs = finder.GetServiceAndServiceGroupInfo(int.Parse(serviceId));
            eventDetailPage
                .VerifyEventType(eventType)
                .VerifyServiceGroupAndService(serviceDBs[0].servicegroup, serviceDBs[0].service)
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(pointAreaName, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Service - sub tab display without error
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();

            List<ActiveSeviceModel> activeSeviceWithUnitModelsInSubTab = eventDetailPage
                .GetAllActiveServiceInTabFullInfo();
            eventDetailPage
                .VerifyDataInServiceSubTab(activeSeviceWithUnitModelsInSubTab, allActiveServices)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab = eventDetailPage
                .GetAllPointHistory();
            eventDetailPage
                .VerifyPointHistoryInSubTab(pointHistoryModelsInDetail, pointHistoryModelsInPointHistorySubTab);
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(pointAreaName, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            //DB get event info
            List<EventDBModel> eventModels = finder.GetEvent(int.Parse(eventId));
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", eventType, serviceUnit)
                .ExpandDetailToggle()
                //Verify Source in Detail toggle => Bug: Cannot click Source Input
                .VerifySourceInputReadOnly();
                
            //Check service unit link
            PageFactoryManager.Get<EventDetailPage>()
                .ClickOnLocation()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string serviceUnitId = PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForServiceUnitDetailPageDisplayed(serviceUnit)
                .GetServiceUnitId();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .VerifyServiceUnitId(eventModels[0].echoID.ToString(), serviceUnitId)
                .ClickCloseBtn();
        }

        //Done Bug: Cannot Click on [Source input]
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from event with service unit")]
        public void TC_105_Create_event_from_event_with_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string eventIdWithServiceUnit = "2";
            string eventOption = "Standard - Additional Service Request";
            string eventType = "Additional Service Request";

            //Get pointID
            List<EventDBModel> eventDBModels = finder.GetEvent(int.Parse(eventIdWithServiceUnit));
            int pointID = eventDBModels[0].eventpointID;

            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();
            List<PointHistoryDBModel> pointHistoryDBModels = new List<PointHistoryDBModel>();
            //Get Service from SP
            SqlCommand command_Service = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command_Service.CommandType = CommandType.StoredProcedure;
            command_Service.Parameters.Add("@PointID", SqlDbType.Int).Value = pointID;
            command_Service.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 1;
            command_Service.Parameters.Add("@SectorID", SqlDbType.Int).Value = 1;
            command_Service.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command_Service.ExecuteReader())
            {
                serviceForPoint = ObjectExtention.DataReaderMapToList<ServiceForPointDBModel>(reader);
                if (reader.NextResult())
                {
                    serviceTaskForPoint = ObjectExtention.DataReaderMapToList<ServiceTaskForPointDBModel>(reader);
                }
                if (reader.NextResult())
                {
                    commonService = ObjectExtention.DataReaderMapToList<CommonServiceForPointDBModel>(reader);
                }
            }

            //Get Point History from SP
            SqlCommand command_PointHistory = new SqlCommand("GetPointHistory", DbContext.Connection);
            command_PointHistory.CommandType = CommandType.StoredProcedure;
            command_PointHistory.Parameters.Add("@EventID", SqlDbType.Int).Value = 0;
            command_PointHistory.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 1;
            command_PointHistory.Parameters.Add("@PointID", SqlDbType.Int).Value = pointID;
            command_PointHistory.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command_PointHistory.ExecuteReader())
            {
                pointHistoryDBModels = ObjectExtention.DataReaderMapToList<PointHistoryDBModel>(reader);
            }

            //Check in web
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.RM)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventsListingPage>()
                .FilterByEventId(eventIdWithServiceUnit)
                //Click row with icon
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .ClickOnServicesTab()
                .WaitForLoadingIconToDisappear();
            string locationName = eventDetailPage
                .GetLocationName();
            List<ActiveSeviceModel> allActiveServices = eventDetailPage
                .GetAllServiceWithServiceUnitModel();
            eventDetailPage
                .VerifyActiveServiceDisplayedWithDB(allActiveServices, serviceForPoint, serviceTaskForPoint);
            List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId = eventDetailPage
                .FilterCommonServiceForPointWithServiceId(commonService, serviceForPoint[0].serviceID);
            eventDetailPage
                .ClickFirstEventInFirstServiceRow()
                .VerifyEventTypeWhenClickEventBtn(FilterCommonServiceForPointWithServiceId)
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed();
            string serviceId = CommonUtil.GetBetween(eventDetailPage.GetCurrentUrl(), "serviceId=", "&serviceUnitId");
            //DB - get service and service group
            List<ServiceJoinServiceGroupDBModel> serviceDBs = finder.GetServiceAndServiceGroupInfo(int.Parse(serviceId));
            eventDetailPage
                .VerifyEventType(eventType)
                .VerifyServiceGroupAndService(serviceDBs[0].servicegroup, serviceDBs[0].service)
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationName, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Service - sub tab display without error
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();

            List<ActiveSeviceModel> activeSeviceWithUnitModelsInSubTab = eventDetailPage
                .GetAllServiceWithServiceUnitModel();
            eventDetailPage
                .VerifyDataInServiceSubTab(activeSeviceWithUnitModelsInSubTab, allActiveServices)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            //DB - Get point history 
            List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab = eventDetailPage
                .GetAllPointHistory();
            eventDetailPage
                .VerifyPointHistoryInSubTab(pointHistoryDBModels, pointHistoryModelsInPointHistorySubTab);
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationName, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            //DB get event info
            List<EventDBModel> eventModels = finder.GetEvent(int.Parse(eventId));
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", eventType, serviceUnit)
                .ExpandDetailToggle()
                //Verify Source in Detail toggle => Bug: Cannot Click on Source input
                .VerifySourceInputReadOnly();
                
            //Check service unit link
            PageFactoryManager.Get<EventDetailPage>()
                .ClickOnLocation()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string serviceUnitId = PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForServiceUnitDetailPageDisplayed(serviceUnit)
                .GetServiceUnitId();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .VerifyServiceUnitId(eventModels[0].echoID.ToString(), serviceUnitId)
                .ClickCloseBtn();
        }

        //Done Bug: Cannot Click on [Source input]
        [Category("CreateEvent")]
        [Category("Chang")]
        [Test(Description = "Creating event from event without service unit")]
        public void TC_105_Create_event_from_event_without_service_unit()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string eventIdWithServiceUnit = "2";
            List<EventDBModel> eventDBModels = finder.GetEvent(int.Parse(eventIdWithServiceUnit));
            int pointID = eventDBModels[0].eventpointID;

            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();
            List<PointHistoryDBModel> pointHistoryDBModels = new List<PointHistoryDBModel>();
            //Get Service from SP
            SqlCommand command_Service = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command_Service.CommandType = CommandType.StoredProcedure;
            command_Service.Parameters.Add("@PointID", SqlDbType.Int).Value = pointID;
            command_Service.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 1;
            command_Service.Parameters.Add("@SectorID", SqlDbType.Int).Value = 1;
            command_Service.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command_Service.ExecuteReader())
            {
                serviceForPoint = ObjectExtention.DataReaderMapToList<ServiceForPointDBModel>(reader);
                if (reader.NextResult())
                {
                    serviceTaskForPoint = ObjectExtention.DataReaderMapToList<ServiceTaskForPointDBModel>(reader);
                }
                if (reader.NextResult())
                {
                    commonService = ObjectExtention.DataReaderMapToList<CommonServiceForPointDBModel>(reader);
                }
            }

            //Get [Service] withour serviceUnit
            List<ServiceForPointDBModel> serviceForPointDBModelsWithoutServiceUnit = serviceForPoint.FindAll(x => x.serviceunit.Equals("No Service Unit"));

            //Check in web
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.RM)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventsListingPage>()
                .FilterByEventId(eventIdWithServiceUnit)
                //Click row with icon
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .ClickOnServicesTab()
                .WaitForLoadingIconToDisappear();
            string locationName = eventDetailPage
                .GetLocationName();
            List<ActiveSeviceModel> allActiveServices = eventDetailPage
                .GetAllServiceWithServiceUnitModel();

            List<ActiveSeviceModel> allActiveServicesNoServiceUnit = eventDetailPage
                .GetAllServiceWithoutServiceUnitModel(allActiveServices);

            List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId = eventDetailPage
                .FilterCommonServiceForPointWithServiceId(commonService, serviceForPointDBModelsWithoutServiceUnit[0].serviceID);
            List<string> allEventTypes = eventDetailPage
                .ClickAnyEventInActiveServiceRow(allActiveServicesNoServiceUnit[0].eventLocator)
                .VerifyEventTypeWhenClickEventBtn(FilterCommonServiceForPointWithServiceId)
                .GetAllEventTypeInDd();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickAnyEventOption(allEventTypes[0])
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Event detail
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyEventType(allEventTypes[0])
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationName, "New")
                .VerifyDueDateEmpty()
                //Verify Data - sub tab display without error
                .ClickDataSubTab()
                .WaitForLoadingIconToDisappear();
            string dataNameValue = "Auto " + CommonUtil.GetRandomNumber(5);
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                .InputNameInDataTab(dataNameValue)
                //Verify Service - sub tab display without error
                .ClickServicesSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage();
            List<ActiveSeviceModel> allSeviceModelsInSubTab = eventDetailPage
                .GetAllServiceWithServiceUnitModel();
            eventDetailPage
                .VerifyDataInServiceSubTab(allActiveServices, allSeviceModelsInSubTab)
                //Verify Outstanding - sub tab display without error
                .ClickOutstandingSubTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNotDisplayErrorMessage()
                //Verify Point History - sub tab display without error
                .ClickPointHistorySubTab()
                .WaitForLoadingIconToDisappear();
            List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab = eventDetailPage
                .GetAllPointHistory();

            //Get Point History from SP
            SqlCommand command_PointHistory = new SqlCommand("GetPointHistory", DbContext.Connection);
            command_PointHistory.CommandType = CommandType.StoredProcedure;
            command_PointHistory.Parameters.Add("@EventID", SqlDbType.Int).Value = 0;
            command_PointHistory.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 1;
            command_PointHistory.Parameters.Add("@PointID", SqlDbType.Int).Value = pointID;
            command_PointHistory.Parameters.Add("@UserID", SqlDbType.Int).Value = 54;

            using (SqlDataReader reader = command_PointHistory.ExecuteReader())
            {
                pointHistoryDBModels = ObjectExtention.DataReaderMapToList<PointHistoryDBModel>(reader);
            }

            eventDetailPage
                .VerifyPointHistoryInSubTab(pointHistoryDBModels, pointHistoryModelsInPointHistorySubTab);
            //Line 15
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl()
                .VerifyDisplayBlueIcon();
            string eventId = eventDetailPage
                .GetEventId();
            string serviceUnit = eventDetailPage
                .GetLocationName();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationName, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            string query_1 = "select * from events where eventid=" + eventId + "; ";
            SqlCommand commandInspection = new SqlCommand(query_1, DbContext.Connection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<EventDBModel> eventModels = ObjectExtention.DataReaderMapToList<EventDBModel>(readerInspection);
            readerInspection.Close();
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(eventModels[0], AutoUser12.DisplayName)
                //Verify Map tab
                .ClickMapTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyDataInMapTab("event", allEventTypes[0], serviceUnit)
                //Check service unit link show popup
                .ClickOnLocationShowPopup()
                .VeriryDisplayPopupLinkEventToServiceUnit("Richmond")
                .ClickCloseEventPopupBtn()
                .ExpandDetailToggle()
                //Verify Source in Detail toggle => Bug: Cannot Click on [Source input]
                .VerifySourceInputReadOnly();
                
        }
    }
}
