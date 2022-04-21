using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Inspections;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
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

        [Category("WB")]
        [Test(Description = "Creating inspection from task")]
        public void TC_079_Create_inspection_from_task()
        {
            //string taskId = "477";
            string taskId = "472";
            string name = " - Collect Domestic Recycling";
            //string location = "14 LONSDALE ROAD, BARNES, LONDON, SW13 9EB";
            string location = "2B RALEIGH ROAD, RICHMOND, TW9 2DX";
            string[] sourceNameList = { location, location };
            string inspectionTypeValue = "Site Inspection";
            string allocatedUnitValue_1 = "Ancillary";
            string allocatedUnitValue_2 = "East Waste";
            string allocatedUserValue_1 = "josie";
            string allocatedUserValue_2 = "User";
            string address = "Collect Domestic Recycling";
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
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(taskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage(name, location)
                .ClickOnInspectionBtn()
                .IsInspectionPopup()
                .VerifyDefaultValue(location)
                .ClickAndVerifySourceDd(sourceNameList)
                .ClickInspectionTypeDdAndSelectValue(inspectionTypeValue)
                .ClickAllocatedUnitAndSelectValue(allocatedUnitValue_1)
                .ClickAllocatedUserAndSelectValue(allocatedUserValue_1)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage)
                .ClickOnSuccessLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .IsDetailInspectionPage(allocatedUnitValue_1, allocatedUserValue_1, noteValue)
                .VerifyStateInspection("Pending")
                .VerifyInspectionAddress(location)
                .VerifyValidFromValidToAndOtherDateField()
                .ClickOnDataTab()
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataInHistoryTab(AutoUser14.DisplayName, noteValue, allocatedUnitValue_1, allocatedUserValue_1, "0");
            //Get inspection Id
            int inspectionId = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));
            //Get data in DB to verify
            string query = "select * from inspections where inspectionID=" + inspectionId + ";";
            SqlCommand commandInspection = new SqlCommand(query, DatabaseContext.Conection);
            SqlDataReader readerInspection = commandInspection.ExecuteReader();
            List<InspectionDBModel> inspections = ObjectExtention.DataReaderMapToList<InspectionDBModel>(readerInspection);
            readerInspection.Close();

            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyDataDisplayedWithDB(inspections[0], noteValue, 5, 0, 2)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailTaskPage
                .ClickInspectionTab()
                .WaitForLoadingIconToDisappear();
            List<InspectionModel> allInspectionModels = detailTaskPage
                .getAllInspection();
            detailTaskPage
                .VerifyInspectionCreated(allInspectionModels[0], inspectionId.ToString(), inspectionTypeValue, AutoUser14.UserName, allocatedUserValue_1, allocatedUnitValue_1, "Pending", CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT) + " 00:00", CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1) + " 00:00");
            //Add new item
            detailTaskPage
                .ClickAddNewInspectionItem()
                .IsInspectionPopup()
                .VerifyDefaultValue(location)
                .ClickAndVerifySourceDd(sourceNameList)
                .ClickInspectionTypeDdAndSelectValue(inspectionTypeValue)
                .ClickAllocatedUnitAndSelectValue(allocatedUnitValue_2)
                .ClickAllocatedUserAndSelectValue(allocatedUserValue_2)
                //Valid From and Valid To are the past
                .InputValidFrom(validFromInThePast)
                .InputValidTo(validToInThePast)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage)
                .ClickOnSuccessLink()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            int inspectionId_2 = Int32.Parse(PageFactoryManager.Get<DetailInspectionPage>()
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", ""));
            PageFactoryManager.Get<DetailInspectionPage>()
                .IsDetailInspectionPage(allocatedUnitValue_2, allocatedUserValue_2, noteValue)
                .VerifyStateInspection("Expired")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            allInspectionModels = detailTaskPage
                .getAllInspection();
             detailTaskPage
                .VerifyInspectionCreated(allInspectionModels[0], inspectionId_2.ToString() , inspectionTypeValue, AutoUser14.UserName, allocatedUserValue_2, allocatedUnitValue_2, "Expired", validFromInThePast + " 00:00", validToInThePast + " 00:00")
                //Clik First Inspection Row
                .ClickOnFirstInspection()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyAllFieldsInPopupAndDisabled(allocatedUnitValue_2, allocatedUserValue_2, noteValue, validFromInThePast + " 00:00", validToInThePast + " 00:00")
                .VerifyStateInspection("Expired");
            //Get sourceId
            string querySourceId = "select * from inspections where inspectionID=" + inspectionId_2 + ";";
            SqlCommand commandSource = new SqlCommand(querySourceId, DatabaseContext.Conection);
            SqlDataReader readerSource = commandSource.ExecuteReader();
            List<InspectionDBModel> inspectionNew = ObjectExtention.DataReaderMapToList<InspectionDBModel>(readerSource);
            readerSource.Close();

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
                .ClickMainOption("Inspections")
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .getAllInspectionInList();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .VerifyTheFirstInspection(allInspectionModels[0], inspectionModels[0], location, "North Star", location, "Domestic Recycling");

        }
    }
}
