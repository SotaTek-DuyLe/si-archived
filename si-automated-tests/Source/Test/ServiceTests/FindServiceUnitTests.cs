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
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
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
                .SwitchToChildWindow(2)
                .VerifyWindowClosed(2);
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
            FindServiceUnitModel findServiceUnitModel = listServiceUnit.Find(x => x.id == "5");
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
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
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
                .WaitForLoadingIconToAppear()
                .WaitForLoadingIconToDisappear();
            pointAddressDetailPage
                .ClickOnAllServicesTab()
                .VerifyServiceRowAfterRefreshing("2", findServiceUnitModel.serviceUnit, "0", "0", "Active");

            //Step 18: Run the query: GetAllServicesForPoint2 and compare UI with DB

            SqlCommand command = new SqlCommand("GetAllServicesForPoint2", DbContext.Connection);
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
                .VerifyServiceUnitId("5");
            //Step 20: Run query to check service unit detail
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(5);
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
                .VerifyFirstServiceUnitPoint(serviceUnitPointModels[0], "5", allService[1].serviceUnit, "Both Serviced and Point of Service", "01/01/2050");
            //Step 22: Run query to check
            List<ServiceUnitPointDBModel> serviceUnitPointAllData = finder.GetServiceUnitPointWithNoLock(5);
            //Get Point address
            List<PointAddressModel> pointAddressFirstRow = finder.GetPointAddress(serviceUnitPointAllData[0].pointID.ToString());
            List<PointAddressModel> pointAddressSecondRow = finder.GetPointAddress(serviceUnitPointAllData[1].pointID.ToString());
            serviceUnitDetailPage
                .VerifyServiceUnitPointAddressWithDB(serviceUnitPointModels, serviceUnitPointAllData, pointAddressFirstRow[0], pointAddressSecondRow[0].Sourcedescription);
        }

        [Category("Find Service Unit")]
        [Test(Description = "Find Service unit from Point Segment")]
        public void TC_127_Find_service_unit_from_point_segment()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string searchForSegment = "Segments";
            string idSegment = "32839";
            string segment = "Summerwood Road 95 To 237 (Single Carriageway) Local Road";
            string address = "213 SUMMERWOOD ROAD, ISLEWORTH, TW7 7QH";

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
                .ClickAnySearchForOption(searchForSegment)
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
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            string pointSegmentName = pointSegmentDetailPage
                .GetPointSegmentName();
            pointSegmentDetailPage
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
                .SwitchToChildWindow(2)
                .VerifyWindowClosed(2);
            //Step 10: Verify that the form works correctly
            pointSegmentDetailPage
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
            string anyServiceUnitInDB = "1 ELM ROAD, EAST SHEEN, LONDON, SW14 7JL";
            findServiceUnitDetailPage
                .InputKeyInSearch(anyServiceUnitInDB)
                .ClickFindBtn()
                .WaitForLoadingIconToDisappear();
            List<FindServiceUnitModel> listServiceUnit = findServiceUnitDetailPage
                .GetAllServiceUnit();
            findServiceUnitDetailPage
                .VerifyResultAfterSearch(listServiceUnit, anyServiceUnitInDB);
            FindServiceUnitModel findServiceUnitModel = listServiceUnit.Find(x => x.id == "98001");
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
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(pointSegmentName)
                .VerifyValuesInDetailTab(idSegment, "Point Segment");
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
            pointSegmentDetailPage
                .Refresh()
                .WaitForLoadingIconToDisappear();
            pointSegmentDetailPage
                .ClickOnAllServicesTab()
                .VerifyServiceRowAfterRefreshing("2", findServiceUnitModel.serviceUnit, "0", "0", "Active");

            //Step 18: Run the query: GetAllServicesForPoint2 and compare UI with DB

            SqlCommand command = new SqlCommand("GetAllServicesForPoint2", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 32839;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 2;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 1102;

            List<ServiceForPoint2DBModel> serviceForPoint2DBModels = new List<ServiceForPoint2DBModel>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint2DBModels = ObjectExtention.DataReaderMapToList<ServiceForPoint2DBModel>(reader);
            }
            List<AllServiceInPointAddressModel> allService = pointSegmentDetailPage
                .GetAllServicesInAllServicesTab();
            pointSegmentDetailPage
                .VerifyDBWithUI(allService, serviceForPoint2DBModels);
            //Step 19: Click on the [Service unit]
            pointSegmentDetailPage
                .ClickServiceUnitLinkAdded(allService[1].serviceUnitLinkToDetail)
                .SwitchToLastWindow();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForServiceUnitDetailPageDisplayed(allService[1].serviceUnit)
                .VerifyServiceUnitId("98001");
            //Step 20: Run query to check service unit detail
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(98001);
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
            Assert.AreEqual(2, serviceUnitPointModels.Count);
            //Step 22: Run query to check
            List<ServiceUnitPointDBModel> serviceUnitPointAllData = finder.GetServiceUnitPointWithNoLock(98001);
            //Get Point address
            List<PointSegmentDBModel> pointAddressFirstRow = finder.GetPointSegment(serviceUnitPointAllData[0].pointID);
            List<ServiceUnitDBModel> serviceUnitDBModelsSecondRow = finder.GetServiceUnit(serviceUnitPointAllData[1].serviceunitID);
            serviceUnitDetailPage
                .VerifyServiceUnitPointSegmentWithDB(serviceUnitPointModels, serviceUnitPointAllData, pointAddressFirstRow[0], serviceUnitDBModelsSecondRow[0].serviceunit);
        }

        [Category("Find Service Unit")]
        [Test(Description = "Find Service unit from Point Area")]
        public void TC_127_Find_service_unit_from_point_area()
        {
            string searchForAreas = "Areas";
            string idArea = "13";
            string address = "HOBBLEDOWN WEST LONDON, STAINES ROAD, HOUNSLOW, TW4 5DS";
            string segment = "Staines Road 12 To 14 Near Roman Close";

            CommonFinder finder = new CommonFinder(DbContext);
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
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            PointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<PointAreaDetailPage>();
            string pointAreaName = pointAreaDetailPage
                .GetPointAreaName();
            pointAreaDetailPage
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
                .SwitchToChildWindow(2)
                .VerifyWindowClosed(2);
            //Step 10: Verify that the form works correctly
            pointAreaDetailPage
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
            string anyServiceUnitInDB = "1 GORDON AVENUE, TWICKENHAM, TW1 1NH";
            findServiceUnitDetailPage
                .InputKeyInSearch(anyServiceUnitInDB)
                .ClickFindBtn()
                .WaitForLoadingIconToDisappear();
            List<FindServiceUnitModel> listServiceUnit = findServiceUnitDetailPage
                .GetAllServiceUnit();
            findServiceUnitDetailPage
                .VerifyResultAfterSearch(listServiceUnit, anyServiceUnitInDB);
            string id = "12781";
            FindServiceUnitModel findServiceUnitModel = listServiceUnit.Find(x => x.id == id);
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
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(pointAreaName)
                .VerifyValuesInDetailTab(idArea, "Point Area");
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
            //Step 17: Navigate back to point area -> Refresh the form
            pointAreaDetailPage
                .Refresh()
                .WaitForLoadingIconToDisappear();
            pointAreaDetailPage
                .ClickOnAllServicesTab()
                .VerifyServiceRowAfterRefreshing("2", findServiceUnitModel.serviceUnit, "0", "0", "Active");

            //Step 18: Run the query: GetAllServicesForPoint2 and compare UI with DB

            SqlCommand command = new SqlCommand("GetAllServicesForPoint2", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 13;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 3;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 1102;

            List<ServiceForPoint2DBModel> serviceForPoint2DBModels = new List<ServiceForPoint2DBModel>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint2DBModels = ObjectExtention.DataReaderMapToList<ServiceForPoint2DBModel>(reader);
            }
            List<AllServiceInPointAddressModel> allService = pointAreaDetailPage
                .GetAllServicesInAllServicesTab();
            pointAreaDetailPage
                .VerifyDBWithUI(allService, serviceForPoint2DBModels);
            //Step 19: Click on the [Service unit]
            pointAreaDetailPage
                .ClickServiceUnitLinkAdded(allService[1].serviceUnitLinkToDetail)
                .SwitchToLastWindow();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForServiceUnitDetailPageDisplayed(allService[1].serviceUnit)
                .VerifyServiceUnitId(id);
            //Step 20: Run query to check service unit detail
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(int.Parse(id));
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
            Assert.AreEqual(2, serviceUnitPointModels.Count);
            //Step 22: Run query to check
            List<ServiceUnitPointDBModel> serviceUnitPointAllData = finder.GetServiceUnitPointWithNoLock(int.Parse(id));
            //Get Point address
            List<PointAreaDBModel> pointAreaFirstRow = finder.GetPointArea(serviceUnitPointAllData[0].pointID);
            List<ServiceUnitDBModel> serviceUnitDBModelsSecondRow = finder.GetServiceUnit(serviceUnitPointAllData[1].serviceunitID);
            serviceUnitDetailPage
                .VerifyServiceUnitPointAreaWithDB(serviceUnitPointModels, serviceUnitPointAllData, pointAreaFirstRow[0], serviceUnitDBModelsSecondRow[0].serviceunit);

        }

        [Category("Find Service Unit")]
        [Test(Description = "Find Service unit from Point Note")]
        public void TC_127_Find_service_unit_from_point_note()
        {
            string searchForPointNodes = "Nodes";
            string idNode = "5";
            string address = "2 CASTLE PLACE, CHISWICK, LONDON, W4 1RT";
            string segment = "Windmill Road 1 To 51 Near Chiswick Common Road";

            CommonFinder finder = new CommonFinder(DbContext);
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
                .ClickAnySearchForOption(searchForPointNodes)
                .ClickAndSelectRichmondCommercialSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Filter note with id
            PageFactoryManager.Get<PointNodeListingPage>()
                .WaitForPointNodeListingPageDisplayed()
                .FilterNodeById(idNode)
                .DoubleClickFirstPointNodeRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Detail node page
            PageFactoryManager.Get<PointNodeDetailPage>()
                .WaitForPointNodeDetailDisplayed()
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            PointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<PointNodeDetailPage>();
            string pointAreaName = pointNodeDetailPage
                .GetPointNodeName();
            pointNodeDetailPage
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
                .SwitchToChildWindow(2)
                .VerifyWindowClosed(2);
            //Step 10: Verify that the form works correctly
            pointNodeDetailPage
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
            string anyServiceUnitInDB = "1 MELROSE AVENUE, TWICKENHAM, TW2 7JE";
            findServiceUnitDetailPage
                .InputKeyInSearch(anyServiceUnitInDB)
                .ClickFindBtn()
                .WaitForLoadingIconToDisappear();
            List<FindServiceUnitModel> listServiceUnit = findServiceUnitDetailPage
                .GetAllServiceUnit();
            findServiceUnitDetailPage
                .VerifyResultAfterSearch(listServiceUnit, anyServiceUnitInDB);
            string id = "13513";
            FindServiceUnitModel findServiceUnitModel = listServiceUnit.Find(x => x.id == id);
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
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(pointAreaName)
                .VerifyValuesInDetailTab(idNode, "Point Node");
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
            //Step 17: Navigate back to point area -> Refresh the form
            pointNodeDetailPage
                .Refresh()
                .WaitForLoadingIconToDisappear();
            pointNodeDetailPage
                .ClickOnAllServicesTab()
                .VerifyServiceRowAfterRefreshing("2", findServiceUnitModel.serviceUnit, "0", "0", "Active");

            //Step 18: Run the query: GetAllServicesForPoint2 and compare UI with DB

            SqlCommand command = new SqlCommand("GetAllServicesForPoint2", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PointID", SqlDbType.Int).Value = 5;
            command.Parameters.Add("@PointTypeID", SqlDbType.Int).Value = 4;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = 1102;

            List<ServiceForPoint2DBModel> serviceForPoint2DBModels = new List<ServiceForPoint2DBModel>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                serviceForPoint2DBModels = ObjectExtention.DataReaderMapToList<ServiceForPoint2DBModel>(reader);
            }
            List<AllServiceInPointAddressModel> allService = pointNodeDetailPage
                .GetAllServicesInAllServicesTab();
            pointNodeDetailPage
                .VerifyDBWithUI(allService, serviceForPoint2DBModels);
            //Step 19: Click on the [Service unit]
            pointNodeDetailPage
                .ClickServiceUnitLinkAdded(allService[1].serviceUnitLinkToDetail)
                .SwitchToLastWindow();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForServiceUnitDetailPageDisplayed(allService[1].serviceUnit)
                .VerifyServiceUnitId(id);
            //Step 20: Run query to check service unit detail
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(int.Parse(id));
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
            Assert.AreEqual(2, serviceUnitPointModels.Count);
            //Step 22: Run query to check
            List<ServiceUnitPointDBModel> serviceUnitPointAllData = finder.GetServiceUnitPointWithNoLock(int.Parse(id));
            //Get Point address
            List<PointNodeDBModel> pointNodeDBModels = finder.GetPointNode(serviceUnitPointAllData[0].pointID);
            List<ServiceUnitDBModel> serviceUnitDBModelsSecondRow = finder.GetServiceUnit(serviceUnitPointAllData[1].serviceunitID);
            serviceUnitDetailPage
                .VerifyServiceUnitPointNodeWithDB(serviceUnitPointModels, serviceUnitPointAllData, pointNodeDBModels[0], serviceUnitDBModelsSecondRow[0].serviceunit);
        }
    }
}
