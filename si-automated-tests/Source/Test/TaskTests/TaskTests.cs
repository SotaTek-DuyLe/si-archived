using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.DBModels.GetTaskDebrief;
using si_automated_tests.Source.Main.DBModels.GetTaskHistory;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.DebriefResult;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Services.ServiceTask;
using si_automated_tests.Source.Main.Pages.Services.ServiceUnit;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TaskTests : BaseTest
    {
        private CommonFinder finder;

        //Task from Contract = Commercial
        private string firstTaskLineId;
        private string secondTaskLineId;
        private string taskIDWithSourceServiceTask = "3964";
        //Update task from In Progrees -> Completed
        private string completedState = "Completed";
        private string notCompletedState = "Not Completed";
        private string cancelledState = "Cancelled";
        private string inprogressState = "In Progress";
        private string pendingState = "Pending";
        private string manualConfirmedOnWeb = "Manually Confirmed on Web";
        private TaskLineModel taskLineModelNew = new TaskLineModel("1", "660L", "1", "3", "1", "100", "Kingston Tip, 20 Chapel Mill Road, Kingston upon Thames, KT1 3GZ", "5", "0", "6", "0", "Completed", "GW1", "Client Ref",  "General Recycling", "Kilograms", "Relift");
        private string destinationSiteName = "Kingston Tip";
        private string destinationSiteNameFull = "Kingston Tip, 20 Chapel Mill Road, Kingston upon Thames, KT1 3GZ";
        private List<TaskLineDBModel> taskLineDBModelsAfterRemoved;

        public override void Setup()
        {
            base.Setup();
            finder = new CommonFinder(DbContext);
            Login();
        }

        public void Login()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser92.UserName, AutoUser92.Password)
                .IsOnHomePage(AutoUser92);
        }

        //DONE
        [Category("Tasks/tasklines")]
        [Category("Chang")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task"), Order(1)]
        public void TC_125_Detail_tasks_update_and_create()
        {
            //Run query to get task's information
            List<TaskDBModel> taskInfoById = finder.GetTask(int.Parse(taskIDWithSourceServiceTask));
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(taskInfoById[0].serviceunitID);
            List<TaskLineDBModel> taskLineDBModels = finder.GetTaskLine(int.Parse(taskIDWithSourceServiceTask));
            List<TaskLineTypeDBModel> taskLineTypeDBModels = finder.GetTaskLineType(taskLineDBModels[0].tasklinetypeID);
            List<AssetTypeDBModel> assetTypeDBModels = finder.GetAssetType(taskLineDBModels[0].assettypeID);
            List<ProductDBModel> productDBModels = finder.GetProduct(taskLineDBModels[0].productID);
            List<PartiesDBModel> partiesDBModels = finder.GetPartiesByPartyId(taskInfoById[0].partyID.ToString());

            //Login ECHO and check the detail of the task
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/tasks/" + taskIDWithSourceServiceTask);

            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .VerifyCurrentUrlOfDetailTaskPage(taskIDWithSourceServiceTask)
                //Verify response returned from DB
                .VerifyTaskWithDB(taskInfoById[0], serviceUnitDBModels[0])
                //Line 9: Click on the hyperlink next to Task
                .ClickHyperlinkNextToTask()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceTaskDetailPage serviceTaskDetailPage = PageFactoryManager.Get<ServiceTaskDetailPage>();
            serviceTaskDetailPage
                .WaitForServiceTaskDetailPageDisplayed()
                .VerifyCurrentUrlServiceTaskDetail(taskInfoById[0].servicetaskID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 12: Click on hyperlink in a description
            detailTaskPage
                .ClickOnHyperlinkInADesciption()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForServiceUnitDetailPageDisplayed()
                .VerifyCurrentUrlServiceTaskDetail(taskInfoById[0].serviceunitID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 13: only party, site and round have hyperlink in the header
            detailTaskPage
                .VerifyFieldInHeaderWithLink("Party")
                .VerifyFieldInHeaderWithLink("Site")
                .VerifyFieldInHeaderWithLink("Round")
                .VerifyFieldInHeaderReadOnly("Contract")
                .VerifyFieldInHeaderReadOnly("Service Group")
                .VerifyFieldInHeaderReadOnly("Service")
                .VerifyFieldInHeaderReadOnly("Round Group")
                //Line 14: Click party hyperlink
                .ClickPartyHyperLink()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Lidl")
                .VerifyCurrentUrlPartyDetailPage(taskInfoById[0].partyID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 15: Click site hyperlink
            detailTaskPage
                .ClickSiteHyperLink()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailSitePage detailSitePage = PageFactoryManager.Get<DetailSitePage>();
            detailSitePage
                .WaitForSiteDetailPageDisplayed()
                .VerifyCurrentUrlSitePage(partiesDBModels[0].correspondence_siteID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 16: Click round hyperlink
            detailTaskPage
                .ClickRoundHyperLink()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceDetailPage roundInstancesDetailPage = PageFactoryManager.Get<RoundInstanceDetailPage>();
            roundInstancesDetailPage
                .WaitForRoundInstanceDetailPageDisplayed()
                .VerifyCurrentUrlRoundPage(taskInfoById[0].roundinstanceID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 17 - Details tab
            detailTaskPage
                .ClickOnDetailTab()
                .VerifyDetailTabWithDataInDB(taskInfoById[0])
                //Line 19 - Click Data tab and verify no error displayed
                .ClickDataTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            //Line 20 - Click Source Data and verify no error displayed
            detailTaskPage
                .ClickSourceDataTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            //Line 21 - Click Task line and verify no error displayed
            detailTaskPage
                .ClickTasklinesTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            //Line 22: Verify data in task line tab
            List<TaskLineModel> allTaskLines = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyDataInTaskLinesTab(taskLineDBModels[0], allTaskLines[0], taskLineTypeDBModels[0], assetTypeDBModels[0], productDBModels[0]);
            //Line 23: CLick Add new Item
            detailTaskPage
                .ClickAddNewItemTaskLineBtn()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected)
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.TaskLineTypeRequired);
            //Line 24: Select type
            detailTaskPage
                .SelectType(2)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected);
            //Line 25: Select assetType and add scheduled qty = 1 -> Save
            detailTaskPage
                .InputOderAtAnyRow(2, "2")
                .SelectAssetType(2)
                .InputScheduledAssetQty(2, "1")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);

            DateTime londonCurrentDateSecondTaskLine = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeCreatedSecondTaskLine = CommonUtil.ParseDateTimeToFormat(londonCurrentDateSecondTaskLine, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);

            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            List<TaskLineModel> allTaskLinesAfter = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyTaskLineCreated(allTaskLinesAfter[1], "Service", "1100L", "1", "Pending");
            //Line 26: Click on add new item -> Select type, product and enter scheduled qty = 80 -> Save
            detailTaskPage
                .ClickAddNewItemTaskLineBtn()
                .InputOderAtAnyRow(3, "3")
                .SelectType(3)
                .InputScheduledAssetQty(3, "1")
                .SelectGeneralRecyclingProductAtAnyRow(3)
                .InputSheduledProductQuantiAtAnyRow(3, "80")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            allTaskLinesAfter = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyTaskLineCreated(allTaskLinesAfter[2], "Service", "General Recycling", 80);
            //Line 28: Double click on first task line
            detailTaskPage
                .DoubleClickAnyTaskLine("1")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Line 29: Verify all fields display correctly
            DetailTaskLinePage detailTaskLinePage = PageFactoryManager.Get<DetailTaskLinePage>();
            firstTaskLineId = detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .GetTaskLineId().ToString();
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLines[0]);
            //Line 30: Run query to get taskline detail and verify
            List<TaskLineDBModel> taskLineDBModelsDetail = finder.GetTaskLineByTaskLineId(int.Parse(firstTaskLineId));
            TaskLineDBModel taskLineDBModel = taskLineDBModelsDetail[0];
            //Get product by productid
            int productId = taskLineDBModel.productID;
            ProductDBModel productDBModel = finder.GetProduct(productId)[0];
            string productNameDB = productDBModel.product;

            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLines[0], taskLineDBModelsDetail[0], productNameDB);
            //Step line 31: Click on the task line hyperlink in the header
            detailTaskLinePage
                .ClickOnHyperlinkOnHeader()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .VerifyCurrentUrl(taskIDWithSourceServiceTask)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Step line 32: Run query and check
            List<TaskAndTaskTypeByTaskIdDBModel> taskAndTaskTypeByTaskIdDBModels = finder.GetTaskAndTaskTypeByTaskId(taskIDWithSourceServiceTask);

            detailTaskLinePage
                .VerifyHyperlinkOnHeader(taskAndTaskTypeByTaskIdDBModels[0], taskIDWithSourceServiceTask);
            //Step line 33: CLick on [Data tab]
            detailTaskLinePage
                .ClickOnDataTab()
                .VerifyNotDisplayErrorMessage();
            string[] expValueHistoryTab = { "0", "1100L", "0", "2", "105", "", "General Refuse", "Kilograms", pendingState, "Unticked", "0" };
            //Step line 34: Click on [History tab]
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();

            detailTaskLinePage
                .VerifyActionCreateWithUserDisplay("09/06/2022 02:07", CommonConstants.ActionCreateInHistoryTaskLineDetail, expValueHistoryTab, "")
                //Step line 35: Detail tab and Update all fields that are not read only and set state to completed
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskLinePage
                .InputAllFieldInDetailTab(taskLineModelNew)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);

            DateTime londonCurrentDate1 = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeUpdated1 = CommonUtil.ParseDateTimeToFormat(londonCurrentDate1, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);

            detailTaskLinePage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskLinePage
                .SelectResolutionCode(taskLineModelNew.resolutionCode)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDate2 = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeUpdated2 = CommonUtil.ParseDateTimeToFormat(londonCurrentDate2, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            taskLineModelNew.type = "Service";

            detailTaskLinePage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskLinePage
                .VerifyTaskLineInfo(taskLineModelNew)
                .VeriryConfirmationAndCompletedDate(timeUpdated1, manualConfirmedOnWeb)
                //Step line 36: Click on History tab and verify all updated
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            string[] expValueHistoryAfterUpdate = { taskLineModelNew.assetType, taskLineModelNew.scheduledAssetQty, taskLineModelNew.scheduledProductQuantity, taskLineModelNew.product, taskLineModelNew.state, taskLineModelNew.order, CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), taskLineModelNew.clientRef, destinationSiteName, taskLineModelNew.siteProduct, taskLineModelNew.minAssetQty, taskLineModelNew.maxAssetQty, taskLineModelNew.minProductQty, taskLineModelNew.maxProductQty, manualConfirmedOnWeb };
            string[] resolutionCodeAfterUpdate = { taskLineModelNew.resolutionCode };

            detailTaskLinePage
                .VerifyActionUpdateWithUserDisplay(timeUpdated1, CommonConstants.ActionUpdateInHistoryTaskLineDetail, expValueHistoryAfterUpdate, AutoUser92.DisplayName, 2)
                .VerifyActionUpdateWithUserDisplay(timeUpdated2, CommonConstants.ActionUpdateResolutionCodeInHistoryTaskLineDetail, resolutionCodeAfterUpdate, AutoUser92.DisplayName, 1)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            detailTaskPage
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage();
            //Step line 37: Tasks > Verdict tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                //First tasks
                .VerifyFirstTaskLineStateVerdictTab(timeUpdated1, taskLineModelNew.state, manualConfirmedOnWeb, taskLineModelNew.product, taskLineModelNew.actualProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.actualAssetQuantity)
                .VerifyFirstResolutionCode(taskLineModelNew.resolutionCode)
                //Step line 44: Tasks > Verdict tab -> Task lines tab: Second tasks
                .VerifySecondTaskLineStateVerdictTab(pendingState, "1100L", "", "1", "0", "0");
            //Step line 39: Double click on the taskline you have created
            detailTaskPage
                .ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .DoubleClickAnyTaskLine("2")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            int taskLineIdNew = detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .GetTaskLineId();
            detailTaskLinePage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLinesAfter[1]);
            //Line 41: Run query to get new taskline detail and verify
            List<TaskLineDBModel> taskLineDBModelsDetailNew = finder.GetTaskLineByTaskLineId(taskLineIdNew);
            //Get product by productid
            int productIdNew = taskLineDBModelsDetailNew[0].productID;
            string productNameDBNew = "";
            if (productIdNew != 0)
            {
                ProductDBModel productDBModelNew = finder.GetProduct(productIdNew)[0];
                productNameDBNew = productDBModelNew.product;
            }
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLinesAfter[1], taskLineDBModelsDetailNew[0], productNameDBNew);
            //Step line 42: Click on data tab
            detailTaskLinePage
                .ClickOnDataTab()
                .VerifyNotDisplayErrorMessage();
            //Step line 43: Click on [History] tab
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();
            string[] expValueSecondTaskLineHistotyTab = { "0", "1100L", "0", "1", "0", "", pendingState, "Unticked", "2", "", "0", "0", "0", "0", "0", "Not Certified", "0" };
            detailTaskLinePage
                .VerifyActionCreateWithUserDisplay(timeCreatedSecondTaskLine, CommonConstants.ActionCreateSecondTaskLineInHistoryTaskLineDetail, expValueSecondTaskLineHistotyTab, AutoUser92.DisplayName);
            //Step line 45: Click back on details tab and change the state to Cancelled
            detailTaskLinePage
                .ClickOnDetailTab()
                .ChangeState(cancelledState)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskLinePage
                .VerifyCurrentTaskState(cancelledState);
            secondTaskLineId = detailTaskLinePage
                .GetTaskLineId().ToString();
            detailTaskLinePage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 46: Task -> Verdict tab -> Task lines tab
            detailTaskPage
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                .VerifySecondTaskLineStateVerdictTab(cancelledState, "1100L", "", "1", "0", "0", secondTaskLineId);
            //Step line 48: Verify task lines
            detailTaskPage
                .ClickTasklinesTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            List<TaskLineModel> allTaskLines1After = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyTaskLineCreated(allTaskLines1After[1], "Service", "1100L", "1", cancelledState);
            //Step line 49: Update the state to [Not Completed] and select one resolution code for last task line created (third task line)
            detailTaskPage
                .SelectAnyState(3, notCompletedState)
                .SelectAnyResolutionCode(3, "No key")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDate3 = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeUpdatedTaskLineNumber3 = CommonUtil.ParseDateTimeToFormat(londonCurrentDate3, CommonConstants.DATE_DD_MM_YYYY_FORMAT);

            detailTaskPage
                .VerifyStateAtAnyRow(3, notCompletedState)
                .VerifyResolutionCodeAtAnyRow(3, "No key");
            //Step line 50: Check hover for row with state = Completed and Not Completed
            detailTaskPage
                .VerifyWhenHoverElementAtAnyRow(taskLineModelNew.resolutionCode, manualConfirmedOnWeb, taskLineModelNew.destinationSite, taskLineModelNew.siteProduct, taskLineModelNew.product, taskLineModelNew.assetType, 1)
                .VerifyWhenHoverElementAtAnyRow("No key", manualConfirmedOnWeb, "", "", "General Recycling", "", 3);
            List<TaskLineModel> allTaskLinesAfter1 = detailTaskPage
                .GetAllTaskLineInTaskLineTab();

            //Step line 51: Query DB
            List<TaskLineDBModel> thirdTaskLine = finder.GetTaskLineByTaskId(int.Parse(firstTaskLineId));

            //Step line 52: Task -> Verdict tab -> Task lines
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                .VerifyThirdTaskLineStateVerdictTab(notCompletedState, "", "General Recycling", "1", "0", "0kg", "No key", manualConfirmedOnWeb)
                .ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            //Step line 53: Double click on the taskline you updated above
            detailTaskPage
                .DoubleClickAnyTaskLine("3")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //NEED TO CHECK
            int taskLineIdThird = detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .GetTaskLineId();
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLinesAfter1[2])
                .VeriryConfirmationAndCompletedDate(timeUpdatedTaskLineNumber3, manualConfirmedOnWeb);
            //Step line 54: Click on History and verify
            detailTaskLinePage
                .ClickOnHistoryTab();

            //Step line 56: Click [Close] btn
            detailTaskLinePage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 57: Click Remove btn
            detailTaskPage
                .ClickRemoveBtnAtAnyRowOfTaskLinesTab(3)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyNumberOfRowTaskLine(2);
            //Step line 58: Run query to check
            taskLineDBModelsAfterRemoved = finder.GetTaskLine(int.Parse(taskIDWithSourceServiceTask));
            Assert.AreEqual(2, taskLineDBModelsAfterRemoved.Count);
            Assert.AreEqual(firstTaskLineId, taskLineDBModelsAfterRemoved[0].tasklineID.ToString());
            Assert.AreEqual(secondTaskLineId, taskLineDBModelsAfterRemoved[1].tasklineID.ToString());
            //Step line 59: Task -> Verdict tab -> Task lines tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                .VerifyNumberOfTaskLine(2)
                .VerifyFirstTaskLineId(firstTaskLineId.ToString())
                .VerifySecondTaskLineId(secondTaskLineId);
            //Step line 60: Change the state to pending in any taskline
            detailTaskPage
                .ClickTasklinesTab()
                .SelectAnyState(1, pendingState)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDateToPending = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeUpdatedToPending = CommonUtil.ParseDateTimeToFormat(londonCurrentDateToPending, CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyStateAtAnyRow(1, pendingState)
                .VerifyResolutionCodeAtAnyRow(1, "")
                .VerifyWhenHoverElementAtAnyRow("", "", destinationSiteNameFull, "GW1", "General Recycling", "660L", 1);
            //Step line 61: Double click on the taskline -> details tab
            detailTaskPage
                .DoubleClickAnyTaskLine("1")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .ClickOnDetailTab()
                .VerifyCurrentTaskState(pendingState)
                .VeriryConfirmationAndCompletedDate("", "");
            //Step line 62: Click on [History] tab
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();
            string[] stateAfterUpdate = { pendingState };
            string[] ActionUpdateStateInHistoryTaskLineDetail = { "State" };

            detailTaskLinePage
                .VerifyActionUpdateWithUserDisplay(timeUpdatedToPending, ActionUpdateStateInHistoryTaskLineDetail, stateAfterUpdate, AutoUser92.DisplayName, 1);

            //Step line 63: DB: Query
            List<TaskLineDBModel> taskLineDBModelsAfterUpdatedState = finder.GetTaskLineByTaskLineId(int.Parse(taskIDWithSourceServiceTask));
            Assert.AreEqual(0, taskLineDBModelsAfterUpdatedState[0].autoconfirmed);
            Assert.AreEqual("01/01/0001 00:00:00", taskLineDBModelsAfterUpdatedState[0].completeddate.ToString());
            //Step line 64: Task -> Verdict tab -> Task lines tab
            detailTaskLinePage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            detailTaskPage
                .ClickOnVerdictTab()
                //First tasks
                .VerifyFirstTaskLineStateVerdictTab(timeUpdated1, pendingState, "", taskLineModelNew.product, taskLineModelNew.actualProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.actualAssetQuantity)
                .VerifyResoluctionCodeFirstRowInTaskLineTab("");
            //Step line 65: History tab
            detailTaskPage
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();

            //Step line 66: Run query
            List<TaskHistoryDBModel> taskHistoryDBModels = finder.GetTaskHistoryByTaskId(taskIDWithSourceServiceTask);

            detailTaskPage
                .VerifyHistoryWithDB(taskHistoryDBModels);

            string taskRef = "Task Ref TC125";
            string taskNote = "Task Note TC125";
            //Step line 67: Update task ref, priority and task notes
            detailTaskPage
                .ClickOnDetailTab()
                .InputTaskRef(taskRef)
                .SelectPriority("High")
                .InputTaskNote(taskNote)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDateUpdatedTask = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string updatedTimeTask = CommonUtil.ParseDateTimeToFormat(londonCurrentDateUpdatedTask, CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Step line 68: History and verify
            detailTaskPage
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            string[] titleUpdate = { "Task notes", "Task reference", "Priority" };
            string[] expectedUpdate = { taskNote, taskRef, "High" };
            detailTaskPage
                .VerifyHistoryTabUpdate(AutoUser92.DisplayName, updatedTimeTask, titleUpdate, expectedUpdate);
        }

        //DONE
        [Category("Tasks/tasklines")]
        [Category("Chang")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task"), Order(2)]
        public void TC_125_Detail_tasks_verify_other_tabs()
        {
            string[] subcriptionColumn = { "ID", "Contact ID", "Contact", "Mobile", "Subscription State", "Start Date", "End Date", "Notes", "Subject", "Subject Description" };
            string[] notificationsColumn = { "ID", "Notification Type", "Template Type", "Contract", "Notification State", "Send To", "Send From", "Message", "Created Date", "Scheduled Date", "Scheduled Send Date", "Sent Date", "Expiry Date", "Subject Type" };
            string[] inspectionColumn = { "ID", "Inspection Type", "Created Date", "Created By User", "Assigned User", "Allocated Unit", "Status", "Valid From", "Valid To", "Completion Date", "Cancelled Date" };
            string[] billingColumn = { "ID", "Description", "PO Number", "Target ID", "Target Type", "Unit", "Unit Price", "Period Type", "No. Periods", "Eval Quantity", "Eval Price", "Eval Periods", "Override Quantity", "Override Price", "Override Periods", "Price", "Net", "Quantity", "VAT Rate" };
            string[] indicatorColumn = { "ID", "Indicator", "Indicator Abv", "Object", "Effective Date", "Expired Date", "Inherited", "Retire" };
            string[] accountStatementColumn = { "Transaction Type", "Item ID", "Date", "Debit", "Credit", "Document Number", "Document Status", "Site Address", "Reinvoiced" };

            //Login ECHO and check the detail of the task
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/tasks/" + taskIDWithSourceServiceTask);

            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage();
            //Step line 69: Subscriptions tab
            detailTaskPage
                .ClickOnSubscriptionTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyNotDisplayErrorMessage();
            detailTaskPage
                .VerifyDisplayColumnInSubscriptionTab(subcriptionColumn);
            //Step line 70: Notification Tab
            detailTaskPage
                .SwitchToDefaultContent();
            detailTaskPage
                .ClickOnNotificationTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyNotDisplayErrorMessage();
            detailTaskPage
                .VerifyDisplayColumnInNotificationTab(notificationsColumn);
            //Step line 71: Inspections tab
            detailTaskPage
                .SwitchToDefaultContent();
            detailTaskPage
                .ClickInspectionTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyNotDisplayErrorMessage();
            detailTaskPage
                .VerifyDisplayColumnInInspectionTab(inspectionColumn);
            //Step line 72: Billing
            detailTaskPage
                .ClickOnBillingTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyNotDisplayErrorMessage();
            detailTaskPage
                .VerifyDisplayColumnInBillingTab(billingColumn);
            //Step line 73: Verdict
            detailTaskPage
                .ClickOnVerdictTab()
                .VerifyDisplayTabInVerdictTab();
            //Step line 122: Indicators tab
            detailTaskPage
                .ClickOnIndicatorsTab()
                .VerifyDisplayColumnInIndicatorTab(indicatorColumn)
                .SwitchToDefaultContent();
            //Step line 123: Account statement
            detailTaskPage
                .ClickOnAccountStatementTab()
                .VerifyDisplayColumnInAccountStatementTab(accountStatementColumn);
            //Step line 125: Map tab
            detailTaskPage
                .ClickOnMapTab()
                .VerifyNotDisplayErrorMessage();
            detailTaskPage
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyTheDisplayOfLegendInMapTab();

        }

        [Category("Tasks/tasklines")]
        [Category("Chang")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task"), Order(3)]
        public void TC_125_Detail_tasks_update_state()
        {
            //Login ECHO and check the detail of the task
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/tasks/" + taskIDWithSourceServiceTask);

            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .ClickOnDetailTab()
                .IsDetailTaskPage();
            //Step line 76: Change state to COmpleted
            detailTaskPage
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd(completedState)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);

            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeCompleted = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_FORMAT);

            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyCompletedDateAtDetailTab(timeCompleted)
                .VerifyCurrentTaskState(completedState)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Step line 77: Click on Task Lines and verify
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .VerifyStateAtAnyRow(1, completedState)
                .VerifyStateAtAnyRow(2, "Cancelled")
                .VerifyAnyRowsInTaskLineAreReadonly(1)
                .VerifyAnyRowsInTaskLineAreReadonly(2)
                .VerifyWhenHoverElementAtAnyRow(manualConfirmedOnWeb, 1)
                //Step line 78: History tab - Record for task
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyTitleTaskLineFirstServiceUpdate()
                .VerifyHistoryTabUpdate(AutoUser92.DisplayName, timeCompleted, timeCompleted, completedState, timeCompleted);
            //Step line 78: History tab - Record for task line
            detailTaskPage
                .VerifyHistoryTabOfTaskLineAfterChangingTaskStatus(AutoUser92.DisplayName, timeCompleted, taskLineModelNew.scheduledAssetQty, taskLineModelNew.scheduledProductQuantity, completedState, "", timeCompleted, manualConfirmedOnWeb, firstTaskLineId.ToString());
            //Step line 79: Verdict tab - verify [Task Information]
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyTaskInformationAfterBulkUpdating2(timeCompleted, completedState, "", manualConfirmedOnWeb);
            //Step line 81: Verdict tab - verify [Task lines]
            detailTaskPage
            .ClickOnTaskLineVerdictTab()
                //First task
                .VerifyFirstTaskLineStateVerdictTab(timeCompleted, completedState, manualConfirmedOnWeb, taskLineModelNew.product, taskLineModelNew.scheduledProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.scheduledAssetQty)
                .VerifyFirstResolutionCode("")
                //Second task
                .VerifySecondTaskLineStateVerdictTab("Cancelled", "1100L", "", "1", "0", "0");
            //Step line 82: Query to check
            List<TaskLineDBModel> tasklineByTaskAfterCompletedTask = finder.GetTaskLineByTaskId(int.Parse(taskIDWithSourceServiceTask));

            //Step line 83: Change to [Not Completed]
            detailTaskPage
                .ClickOnDetailTab()
                .IsDetailTaskPage()
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd(notCompletedState)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDateNotCompleted = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeNotCompleted = CommonUtil.ParseDateTimeToFormat(londonCurrentDateNotCompleted, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyCompletedDateAtDetailTab(timeCompleted)
                .VerifyCurrentTaskState(notCompletedState)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Step line 84: Verify in [Task lines] tab
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .VerifyStateAtAnyRow(1, completedState)
                .VerifyStateAtAnyRow(2, cancelledState)
                .VerifyAnyRowsInTaskLineAreReadonly(1)
                .VerifyAnyRowsInTaskLineAreReadonly(2)
                .VerifyWhenHoverElementAtAnyRow(manualConfirmedOnWeb, 1)
                //Step line 85: History tab - Record for task
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
               .VerifyTitleTaskLineFirstServiceUpdate()
               .VerifyHistoryTabUpdate(AutoUser92.DisplayName, timeNotCompleted, timeCompleted, notCompletedState, timeCompleted)
               //Step line 86: Verdict tab/task information
               .ClickOnVerdictTab()
               .ClickOnTaskInformation()
               .VerifyTaskInformationAfterBulkUpdating2(timeCompleted, notCompletedState, "", manualConfirmedOnWeb);
            //Step line 87: Verdict tab/Task lines
            detailTaskPage
               .ClickOnTaskLineVerdictTab()
               .VerifyFirstTaskLineStateVerdictTab(timeCompleted, completedState, manualConfirmedOnWeb, taskLineModelNew.product, taskLineModelNew.scheduledProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.scheduledAssetQty)
               .VerifyFirstResolutionCode("")
                //Second task
                .VerifySecondTaskLineStateVerdictTab(cancelledState, "1100L", "", "1", "0", "0");
            //Step line 82: Query to check
            List<TaskLineDBModel> tasklineByTaskAfterNotCompletedTask = finder.GetTaskLineByTaskId(int.Parse(taskIDWithSourceServiceTask));

            //Step line 90: Change task state to Cancelled
            detailTaskPage
                .ClickOnDetailTab()
                .IsDetailTaskPage()
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd(cancelledState)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDateCancelled = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeCancelled = CommonUtil.ParseDateTimeToFormat(londonCurrentDateCancelled, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyCompletedDateAtDetailTab("")
                .VerifyCurrentTaskState(cancelledState)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Step line 91: Verify in [Task lines] tab
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            taskLineModelNew.resolutionCode = "";

            detailTaskPage
                .VerifyStateAtAnyRow(1, completedState)
                .VerifyStateAtAnyRow(2, cancelledState)
                .VerifyAnyRowsInTaskLineAreReadonly(1)
                .VerifyAnyRowsInTaskLineAreReadonly(2)
                .VerifyWhenHoverElementAtAnyRow(manualConfirmedOnWeb, 1)
                //Step line 92: History tab - Record for task
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
               .VerifyTitleTaskLineFirstServiceUpdate()
               .VerifyHistoryTabUpdate(AutoUser92.DisplayName, timeCancelled, "", cancelledState)
               //Step line 93: Verdict tab/task information
               .ClickOnVerdictTab()
               .ClickOnTaskInformation()
               .VerifyTaskInformationAfterBulkUpdating2("", cancelledState, "", "")
               //Step line 95: Verdict tab/Task lines
               .ClickOnTaskLineVerdictTab()
               .VerifyFirstTaskLineStateVerdictTab(timeCompleted, completedState, manualConfirmedOnWeb, taskLineModelNew.product, taskLineModelNew.scheduledProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.scheduledAssetQty)
                .VerifyFirstResolutionCode("")
                //Second task
                .VerifySecondTaskLineStateVerdictTab(cancelledState, "1100L", "", "1", "0", "0");
            //Step line 96: Query to check
            List<TaskLineDBModel> tasklineByTaskAfterCancelledTask = finder.GetTaskLineByTaskId(int.Parse(taskIDWithSourceServiceTask));

            //Step line 97: Update task to Inprogress
            detailTaskPage
                .ClickOnDetailTab()
                .IsDetailTaskPage()
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd(inprogressState)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDateInprogress = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeInprogress = CommonUtil.ParseDateTimeToFormat(londonCurrentDateInprogress, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);

            //Step line 98: Task lines tab
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .VerifyStateAtAnyRow(1, completedState)
                .VerifyStateAtAnyRow(2, cancelledState)
                .VerifyAnyRowsInTaskLineAreEditable(1)
                .VerifyAnyRowsInTaskLineAreEditable(2)
                .VerifyAddNewItemEnabled()
                .VerifyWhenHoverElementAtAnyRow(manualConfirmedOnWeb, 1)
                .VerifyWhenHoverElementAtAnyRow("", 2)
                //Step line 99: History tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
               .VerifyTitleTaskLineFirstServiceUpdate()
               .VerifyHistoryTabUpdateWithEndDate(AutoUser92.DisplayName, timeInprogress, "", inprogressState)
               //Step line 100: Verdict tab/task information
               .ClickOnVerdictTab()
               .ClickOnTaskInformation()
               .VerifyTaskInformationAfterBulkUpdating2("", inprogressState, taskLineModelNew.resolutionCode, "");
            //Step line 101: Run query to check
            List<TaskDBModel> taskAfterChangingToInprogress = finder.GetTaskByTaskId(taskIDWithSourceServiceTask);
            List<TaskStateDBModel> taskStateDBModels = finder.GetTaskStateByTaskId(taskIDWithSourceServiceTask);
            string currentTaskState = taskStateDBModels[0].taskstate;
            Assert.AreEqual(inprogressState, currentTaskState);

            //Step line 102: Verdict tab/Task lines
            detailTaskPage
                .ClickOnTaskLineVerdictTab()
                .VerifyFirstTaskLineStateVerdictTab(timeCompleted, completedState, manualConfirmedOnWeb, taskLineModelNew.product, taskLineModelNew.scheduledProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.scheduledAssetQty)
                .VerifyFirstResolutionCode(taskLineModelNew.resolutionCode)
                //Second task
                .VerifySecondTaskLineStateVerdictTab(cancelledState, "1100L", "", "1", "0", "0");
            //Step line 103: Query to check
            List<TaskLineDBModel> tasklineByTaskAfterInprogressTask = finder.GetTaskLineByTaskId(int.Parse(taskIDWithSourceServiceTask));
            TaskStateDBModel taskStateDBModelFirst = finder.GetTaskStateByTaskStateId(tasklineByTaskAfterInprogressTask[0].tasklinestateID.ToString());
            string firstStateTaskline = taskStateDBModelFirst.taskstate;
            TaskStateDBModel taskStateDBModelSecond = finder.GetTaskStateByTaskStateId(tasklineByTaskAfterInprogressTask[1].tasklinestateID.ToString());
            string secondStateTaskline = taskStateDBModelSecond.taskstate;

            detailTaskPage
                .VerifyFirstTaskLineWithDB(tasklineByTaskAfterInprogressTask[0], firstStateTaskline)
                .VerifySecondTaskLineWithDB(tasklineByTaskAfterInprogressTask[1], secondStateTaskline);
            //Step line 104: Fields related to tasklines (verify in [Verdict] tab)
            int scheduleAssetQtyTotal = taskLineDBModelsAfterRemoved[0].scheduledassetquantity + taskLineDBModelsAfterRemoved[1].scheduledassetquantity;
            double scheduleProductQtyTotal = taskLineDBModelsAfterRemoved[0].scheduledproductquantity + taskLineDBModelsAfterRemoved[1].scheduledproductquantity;
            detailTaskPage
                .ClickOnTaskInformation()
                .VerifyScheduledAssetQtyInTaskInformation(scheduleAssetQtyTotal.ToString())
                .VerifyScheduledProductQtyInTaskInformation(scheduleProductQtyTotal.ToString());
            //Step line 105: Update asset scheduled qty and product qty at tasklines tab
            int firstScheduleAssetQtyUpdate = 5;
            int firstScheduleProductQtyUpdate = 100;
            int secondScheduleAssetQtyUpdate = 10;
            int secondScheduleProductQtyUpdate = 50;
            detailTaskPage
                .ClickTasklinesTab()
                //=> First task line
                .InputScheduledAssetQty(1, firstScheduleAssetQtyUpdate.ToString())
                .InputSheduledProductQuantiAtAnyRow(1, firstScheduleProductQtyUpdate.ToString())
                //=> Second task line
                .InputScheduledAssetQty(2, secondScheduleAssetQtyUpdate.ToString())
                .InputSheduledProductQuantiAtAnyRow(2, secondScheduleProductQtyUpdate.ToString())
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDateUpdateSchedule = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeUpdateSchedule = CommonUtil.ParseDateTimeToFormat(londonCurrentDateCancelled, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Step line 106: History tab
            detailTaskPage
                .ClickOnHistoryTab()
                .VerifyHistoryTabAfterUpdatingSchedule(firstScheduleAssetQtyUpdate.ToString(), firstScheduleProductQtyUpdate.ToString(), firstTaskLineId, secondScheduleAssetQtyUpdate.ToString(), secondScheduleProductQtyUpdate.ToString(), secondTaskLineId);
            int allScheduleAssetQty = firstScheduleAssetQtyUpdate + secondScheduleAssetQtyUpdate;
            int allScheduleProductQty = firstScheduleProductQtyUpdate + secondScheduleProductQtyUpdate;
            //Step line 107: Verdict tab/Task information
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyScheduledAssetQtyInTaskInformation(allScheduleAssetQty.ToString())
                .VerifyScheduledProductQtyInTaskInformation(allScheduleProductQty.ToString());
            //Step line 108: Verdict tab/Task lines
            detailTaskPage
                .ClickOnTaskLineVerdictTab()
                //First task
                .VerifyScheduleAssetQtyFirstTaskLineVerdictTab(firstScheduleAssetQtyUpdate.ToString())
                //Second task
                .VerifyScheduleAssetQtySecondTaskLineVerdictTab(secondScheduleAssetQtyUpdate.ToString());
        }

        //DONE
        [Category("Chang")]
        [Category("Tasks/tasklines")]
        [Test(Description = "Tasks/tasklines - Fields related to round instance: round state,human resource(s), vehicle resource(s)"), Order(4)]
        public void TC_125_Task_tasklines_fields_related_to_round_instance_round_state_human_resource_vehicle_resource()
        {
            string statusNotDone = "Not Done";
            string statusComplete = "Complete";
            string statusReason = "Bad Weather";
            string[] debriefTestColumn = { "Test Name ", "Resolution Code", "Notes", "Debrief Result" };

            //Login ECHO and check the detail of the task
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/tasks/" + taskIDWithSourceServiceTask);

            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .ClickOnDetailTab()
                .IsDetailTaskPage();
            //Step line 111: Click on Round in the header
            detailTaskPage
                .ClickOnRoundLink()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceDetailPage roundInstanceDetailPage = PageFactoryManager.Get<RoundInstanceDetailPage>();
            roundInstanceDetailPage
                .WaitForRoundInstanceDetailPageDisplayed()
                .ClickOnDetailTab()
                //Step line 112: Change Status to [Not done] and select [Status Reason]
                .SelectStatusInDetailTab(statusNotDone)
                .SelectStatusReasonInDetailTab(statusReason)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            roundInstanceDetailPage
                .VerifyStatusInDetailTab(statusNotDone)
                .VerifyStatusReasonInDetailTab(statusReason);
            //Step line 113: Back to [Detail Task] > [Verdict] tab and verify
            roundInstanceDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            detailTaskPage
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnVerdictTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .ClickOnTaskInformation()
                .VerifyRoundStateAndHover(statusNotDone + ", " + statusReason);
            //Step line 114: [Verdict] tab and [Debrief test(s)] tab
            detailTaskPage
                .ClickOnDebriefTestTab()
                .VerifyDisplayColumnInDebriefTestTab(debriefTestColumn)
                .VerifyTableDebriefTestBlank();
            //Step line 115: Back to [Round Instance] and change state to [Complete]
            detailTaskPage
                .ClickRoundHyperLink()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            roundInstanceDetailPage
                .WaitForRoundInstanceDetailPageDisplayed()
                .ClickOnDetailTab()
                .SelectStatusInDetailTab(statusComplete)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string roundInstance = roundInstanceDetailPage
                .GetRoundInstanceId();
            roundInstanceDetailPage
                .VerifyStatusInDetailTab(statusComplete)
                .VerifyStatusReasonInDetailTab("Select...");
            //Step line 116: Click on [Debrief] btn
            roundInstanceDetailPage
                .VerifyDisplayDebriefBtn()
                .ClickOnDebriefBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            DebriefResultPage debriefResultPage = PageFactoryManager.Get<DebriefResultPage>();
            debriefResultPage
                .WaitForDebriefLoaded()
                .VerifyCurrentUrlOfDebriefDetailPage(roundInstance)
                .ClickOnRoundBtnAndNoErrorMessageDisplayed()
                .ClickOnActivityBtnAndNoErrorMessageDisplayed()
                .ClickOnWorksheetBtnAndNoErrorMessageDisplayed()
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            roundInstanceDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);

            //Step line 119: Back to the Task detail form > Verdict tab > task information
            detailTaskPage
                .Refresh()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyRoundStateAndHover("Complete");
            //Step line 120: Debrief test tab
            detailTaskPage
                .ClickOnDebriefTestTab()
                .WaitForLoadingIconToDisappear();
            //Step line 122: Run query to check
            GetTaskDebriefDBModel getTaskDebriefDBModel = finder.GetTaskDebriefDBModelByTaskId(taskIDWithSourceServiceTask);
            detailTaskPage
                .VerifyValueColumnDebriefTestTab(getTaskDebriefDBModel);
        }

    }
}
 