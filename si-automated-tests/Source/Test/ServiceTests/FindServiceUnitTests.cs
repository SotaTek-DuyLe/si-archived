using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.DBModels.GetAllServicesForPoint2;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class FindServiceUnitTests : BaseTest
    {
        [Category("Find Service Unit")]
        [Test(Description = "Find Service unit from Point Address")]
        public void TC_127_Find_service_unit_from_point_address()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string searchForAddresses = "Addresses";
            string pointAddressId = "363507";
            string serviceUnit = "richmond";
            string address = "SHEARWATER HOUSE, 21 THE GREEN, RICHMOND, TW9 1PX";
            string segment = "The Green 1 To 32 Between Duke Street And Old Palace Terrace";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password)
                .IsOnHomePage(AutoUser56);
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
            //Step 1: Open [Find Service Unit] form => Detail Point Address > Action > Find Service Unit
            PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            string pointAddressName = pointAddressDetailPage
                .GetPointAddressName();
            pointAddressDetailPage
                .ClickOnAnyActionBtn(2)
                .ClickOnAnyFindServiceUnitBtn(2)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Step 2: Verify top bar actions
            FindServiceUnitDetailPage findServiceUnitDetailPage = PageFactoryManager.Get<FindServiceUnitDetailPage>();
            findServiceUnitDetailPage
                .IsFindServiceUnitPage()
                //Step 7: Refresh
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            findServiceUnitDetailPage
                .IsFindServiceUnitPage()
                //Step 8: Help
                .ClickHelpBtnAndVerify()
                //Step 5: Close without saving
                .ClickCloseWithoutSavingBtn()
                .VerifyWindowClosed(2)
                .SwitchToChildWindow(2);
            //Step 10: Verify that the form works correctly
            pointAddressDetailPage
                .ClickOnAnyActionBtn(2)
                .ClickOnAnyFindServiceUnitBtn(2)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Step 23: Input any name
            string textSearch = CommonUtil.GetRandomString(5);
            findServiceUnitDetailPage
                .InputKeyInSearch(textSearch)
                .ClickFindBtn()
                .WaitForLoadingIconToDisappear();
            findServiceUnitDetailPage
                .VerifyDisplayNoResultFound(textSearch);
            //Step 24: SP to verify
            string anyServiceUnitInDB = "1 ABBOTT CLOSE, HAMPTON, TW12 3XR";
            findServiceUnitDetailPage
                .InputKeyInSearch(anyServiceUnitInDB)
                .ClickFindBtn()
                .WaitForLoadingIconToDisappear();
            List<FindServiceUnitModel> listServiceUnit = findServiceUnitDetailPage
                .GetAllServiceUnit();
            findServiceUnitDetailPage
                .VerifyResultAfterSearch(listServiceUnit, anyServiceUnitInDB);

            findServiceUnitDetailPage
                .InputKeyInSearch(serviceUnit)
                .ClickFindBtn()
                .WaitForLoadingIconToDisappear();
            List<FindServiceUnitModel> list = findServiceUnitDetailPage
                .GetAllServiceUnit(5);
            FindServiceUnitModel findServiceUnitModel = list[0];
            //Step 12: Click on the hyperlink 
            findServiceUnitDetailPage
                .ClickAnyLinkInServiceUnit(findServiceUnitModel.serviceUnitLocator)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForServiceUnitDetailPageDisplayed(findServiceUnitModel.serviceUnit)
                .VerifyServiceUnitId(findServiceUnitModel.id)
                .CloseCurrentWindow()
                .SwitchToChildWindow(3);
            //Step 13: Click on [Select] btn
            findServiceUnitDetailPage
                .ClickAnySelect(findServiceUnitModel.selectLocator)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(pointAddressName)
                .VerifyValuesInDetailTab(pointAddressId, "Point Address");
            //Step 14: Verify [Map] tab
            serviceUnitPointDetailPage
                .ClickOnMapTab()
                .WaitForLoadingIconToDisappear();
            serviceUnitPointDetailPage
                .ClickOnMapTab()
                .VerifyValueInMapTab(address, segment);
            //Step 15: Click [Save] btn
            serviceUnitPointDetailPage
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceUnitPointDetailPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            string serviceUnitPointId = serviceUnitPointDetailPage
                .VerifyServiceUnitPointTypeAfter("Serviced Point")
                .GetServiceUnitPointId();
            //Step 16: Run query to check service unit point
            List<ServiceUnitPointDBModel> serviceUnitPointDBModels = finder.GetServiceUnitPoint(int.Parse(serviceUnitPointId));
            List<PointTypeDBModel> pointTypeDBModels = finder.GetPointType(serviceUnitPointDBModels[0].pointtypeID);
            serviceUnitPointDetailPage
                .VerifyUIWithDB(serviceUnitPointDBModels[0], pointTypeDBModels[0])
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            findServiceUnitDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Step 17: Navigate back to point address -> Refresh the form
            pointAddressDetailPage
                .Refresh()
                .WaitForLoadingIconToDisappear();
            pointAddressDetailPage
                .ClickOnAllServicesTab()
                .VerifyServiceRowAfterRefreshing("2", findServiceUnitModel.serviceUnit, "0", "0", "Active");

            //Step 18: Run the query: GetAllServicesForPoint2 and compare UI with DB

            SqlCommand command = new SqlCommand("EW_GetServicesInfoForPoint", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 363507;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 1;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 1102;

            List<ServiceForPoint2DBModel> serviceForPoint2DBModels = new List<ServiceForPoint2DBModel>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint2DBModels = ObjectExtention.DataReaderMapToList<ServiceForPoint2DBModel>(reader);
            }
            List<AllServiceInPointAddressModel> allService = pointAddressDetailPage
                .GetAllServicesInAllServicesTab();
            pointAddressDetailPage
                .VerifyDBWithUI(allService, serviceForPoint2DBModels);
            //Step 19: Click on the [Service unit]
            pointAddressDetailPage
                .ClickServiceUnitLinkAdded(allService[1].serviceUnitLinkToDetail)
                .SwitchToLastWindow();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForServiceUnitDetailPageDisplayed(allService[1].serviceUnit)
                .VerifyServiceUnitId("13");
            //Step 20: Run query to check service unit detail
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(13);
            List<ServiceUnitTypeDBModel> serviceUnitTypeDBModels = finder.GetServiceUnitType(serviceUnitDBModels[0].serviceunittypeID);
            List<PointSegmentDBModel> pointSegmentDBModels = finder.GetPointSegment(serviceUnitDBModels[0].pointsegmentID);
            List<StreetDBModel> streetDBModels = finder.GetStreet(serviceUnitDBModels[0].streetID);

            serviceUnitDetailPage
                .VerifyServiceUnitDetailTab(serviceUnitDBModels[0], serviceUnitTypeDBModels[0], pointSegmentDBModels[0], streetDBModels[0]);
            //Step 21: Click on service unit points tab
            serviceUnitDetailPage
                .ClickOnServiceUnitPointsTab()
                .WaitForLoadingIconToDisappear();
            List<ServiceUnitPointModel> serviceUnitPointModels = serviceUnitDetailPage.GetAllServiceUnitPointInTab();
            serviceUnitDetailPage
                .VerifyFirstServiceUnitPoint(serviceUnitPointModels[0], "13", pointAddressId, allService[1].serviceUnit, "Both Serviced and Point of Service", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), "01/01/2050")
                .VerifySecondServiceUnitPoint(serviceUnitPointModels[1], "423332");
            //Step 22: Run query to check
            List<ServiceUnitPointDBModel> serviceUnitPointAllData = finder.GetServiceUnitPointWithNoLock(13);
            //Get Point address
            List<PointAddressModel> pointAddressFirstRow = finder.GetPointAddress(serviceUnitPointAllData[0].pointID.ToString());
            List<PointAddressModel> pointAddressSecondRow = finder.GetPointAddress(serviceUnitPointAllData[1].pointID.ToString());
            serviceUnitDetailPage
                .VerifyServiceUnitPointWithDB(serviceUnitPointModels, serviceUnitPointAllData, pointAddressFirstRow[0], pointAddressSecondRow[1]);
            //Step 23: 
        }
    }
}
