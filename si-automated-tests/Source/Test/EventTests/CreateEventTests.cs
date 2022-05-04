using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
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
        [Test(Description = "Creating event from point address")]
        public void TC_094_Create_event_from_point_address()
        {
            string searchForAddresses = "Addresses";
            string eventOption = "Standard - Complaint";
            string eventTypeValue = "Bulky Collection";
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
                .ClickAndSelectRichmondCommercialSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<PointAddressListingPage>()
                .WaitForPointAddressPageDisplayed()
                .FilterPointAddressWithId(pointAddressId)
                .DoubleClickFirstPointAddressRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string locationValue = PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .GetPointAddressName();
            string idPointAddress = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/point-addresses/", "");
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
            //Get all data in [Active Services]
            List<ActiveSeviceModel> allActiveServices = PageFactoryManager.Get<PointAddressDetailPage>()
                .GetAllActiveServiceModel();
            PageFactoryManager.Get<PointAddressDetailPage>()
                .ClickFirstEventInFirstServiceRow()
                .ClickAnyEventOption(eventOption)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyEventType(eventTypeValue)
                //Need to confirm
                //.VerifyServiceGroupAndService()
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
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
                .GetAllActiveServiceModel();
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
                .SwitchToLastWindow();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyCurrentEventUrl();
            string eventId = eventDetailPage
                .GetEventId();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(locationValue, "New")
                .VerifyDueDate(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                .VerifyDisplayTabsAfterSaveEvent();
            //Verify History tab
            eventDetailPage
                .ClickHistoryTab();
        }
    }
}
