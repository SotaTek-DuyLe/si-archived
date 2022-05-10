using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.PointAddress;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.EventTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateEventTests : BaseTest
    {
        [Category("CreateEvent")]
        [Test(Description = "Creating event from point address with service unit")]
        public void TC_094_Create_event_from_point_address_with_service_unit()
        {
            string searchForAddresses = "Addresses";
            string eventOption = "Standard - Complaint";
            string pointAddressId = "483986";
            string eventType = "Complaint";
            List<ServiceForPointDBModel> serviceForPoint = new List<ServiceForPointDBModel>();
            List<ServiceTaskForPointDBModel> serviceTaskForPoint = new List<ServiceTaskForPointDBModel>();
            List<CommonServiceForPointDBModel> commonService = new List<CommonServiceForPointDBModel>();

            //Check SP
            SqlCommand command = new SqlCommand("EW_GetServicesInfoForPoint", DatabaseContext.Conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 483986;
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
            List<ActiveSeviceModel> allActiveServices = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetAllServiceWithServiceUnitModel();
            //Verify data in [Active Service] tab with SP

            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickFirstEventInFirstServiceRow()
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyEventType(eventType)
                //Need to confirm
                //.VerifyServiceGroupAndService()
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
            List<ActiveSeviceModel> activeSeviceModelsInSubTab = eventDetailPage
                .GetAllActiveServiceWithServiceUnitModel();
            eventDetailPage
                .VerifyDataInServiceSubTab(allActiveServices, activeSeviceModelsInSubTab)
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
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage)
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
            SqlCommand commandInspection = new SqlCommand(query_1, DatabaseContext.Conection);
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
                //Verify Source in Detail toggle
                .ClickOnSourceInputInDetailToggle()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .VerifyPointAddressId(eventModels[0].eventpointID.ToString())
                .ClickCloseBtn()
                .SwitchToLastWindow();
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

        [Category("CreateEvent")]
        [Test(Description = "Creating event from point address without service unit")]
        public void TC_094_Create_event_from_point_address_without_service_unit()
        {
            string searchForAddresses = "Addresses";
            string pointAddressId = "483986";

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
            List<ActiveSeviceModel> allActiveServices = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetAllServiceInTab();
            //Service = Skip
            ActiveSeviceModel activeSeviceModelWithSkip = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetActiveServiceWithSkipService(allActiveServices);
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickAnyEventInActiveServiceRow(activeSeviceModelWithSkip.eventLocator)
                .VerifyToastMessage(MessageRequiredFieldConstants.NoEventsAvailableWarningMessage)
                .WaitUntilToastMessageInvisiable(MessageRequiredFieldConstants.NoEventsAvailableWarningMessage);
            //Get all active service no service unit
            List<ActiveSeviceModel> allActiveServicesNoServiceUnit = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetAllServiceWithoutServiceUnitModel(allActiveServices);
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
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage)
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
            SqlCommand commandInspection = new SqlCommand(query_1, DatabaseContext.Conection);
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
                //Verify Source in Detail toggle
                .ClickOnSourceInputInDetailToggle()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .VerifyPointAddressId(eventModels[0].eventpointID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
        }

    }
}
