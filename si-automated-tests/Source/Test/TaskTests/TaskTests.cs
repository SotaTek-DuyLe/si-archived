using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.DBModels.GetTaskHistory;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Round.RoundInstance;
using si_automated_tests.Source.Main.Pages.Services.ServiceTask;
using si_automated_tests.Source.Main.Pages.Services.ServiceUnit;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    public class TaskTests : BaseTest
    {
        private CommonFinder finder;
        //Task from Contract = Commercial
        private string taskIDWithSourceServiceTask = "3964";
        private string manualConfirmedOnWeb = "Manually Confirmed on Web";

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

        [Category("Tasks/tasklines")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task"), Order(1)]
        public void TC_125_Detail_tasks_Update_and_create()
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
            RoundInstancesDetailPage roundInstancesDetailPage = PageFactoryManager.Get<RoundInstancesDetailPage>();
            roundInstancesDetailPage
                .WaitForRoundInstanceDetailPageDisplayed()
                .VerifyCurrentUrlRoundPage(taskInfoById[0].roundinstanceID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 17 - Details tab
            detailTaskPage
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
            string timeCreatedSecondTaskLine = "";
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
            int taskLineId = detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .GetTaskLineId();
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLines[0]);
            //Line 30: Run query to get taskline detail and verify
            List<TaskLineDBModel> taskLineDBModelsDetail = finder.GetTaskLine(taskLineId);
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLines[0], taskLineDBModelsDetail[0]);
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
            string[] expValueHistoryTab = { "0", "1100L", "0", "2", "105", "", "General Refuse", "Kilograms", "Pending", "Unticked", "0" };
            //Step line 34: Click on [History tab]
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();
            string destinationSite = "Kingston Tip, 20 Chapel Mill Road, Kingston upon Thames, KT1 3GZ";
            string destinationSiteName = "Kingston Tip";
            TaskLineModel taskLineModelNew = new TaskLineModel("1", "660L", "1", "3", "1", "100", destinationSite, "5", "2", "6", "3", "Client Ref", "Completed", "GW1", "General Recycling", "Kilograms", "Relift");
            detailTaskLinePage
                .VerifyActionCreateWithUserDisplay("09/06/2022 02:07", CommonConstants.ActionCreateInHistoryTaskLineDetail, expValueHistoryTab, "")
                //Step line 35: Detail tab and Update all fields that are not read only and set state to completed
                .InputAllFieldInDetailTab(taskLineModelNew)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);

            string timeUpdated1 = "";
            detailTaskLinePage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskLinePage
                .SelectResolutionCode(taskLineModelNew.resolutionCode)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string timeUpdated2 = "";
            detailTaskLinePage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskLinePage
                .VerifyTaskLineInfo(taskLineModelNew)
                .VeriryConfirmationAndCompletedDate(timeUpdated2, manualConfirmedOnWeb)
                //Step line 36: Click on History tab and verify all updated
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            string[] expValueHistoryAfterUpdate = { taskLineModelNew.actualAssetQuantity, taskLineModelNew.assetType, taskLineModelNew.actualProductQuantity, taskLineModelNew.scheduledAssetQty, taskLineModelNew.scheduledProductQuantity, taskLineModelNew.state, taskLineModelNew.order, CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), taskLineModelNew.clientRef, destinationSiteName, taskLineModelNew.siteProduct, taskLineModelNew.minAssetQty, taskLineModelNew.maxAssetQty, taskLineModelNew.minProductQty, taskLineModelNew.maxProductQty, manualConfirmedOnWeb };
            string[] resolutionCodeAfterUpdate = { taskLineModelNew.resolutionCode };

            detailTaskLinePage
                .VerifyActionUpdateWithUserDisplay(timeUpdated1, CommonConstants.ActionUpdateInHistoryTaskLineDetail, expValueHistoryAfterUpdate, AutoUser92.DisplayName, 2)
                .VerifyActionUpdateWithUserDisplay(timeUpdated2, CommonConstants.ActionUpdateResolutionCodeInHistoryTaskLineDetail, resolutionCodeAfterUpdate, AutoUser92.DisplayName, 1)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 37: Tasks > Verdict tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                //First tasks
                .VerifyFirstTaskLineStateVerdictTab(timeUpdated1, taskLineModelNew.state, manualConfirmedOnWeb, taskLineModelNew.product, taskLineModelNew.actualProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.actualAssetQuantity)
                .VerifyFirstResolutionCode(taskLineModelNew.resolutionCode)
                ////Step line 44: Tasks > Verdict tab -> Task lines tab: Second tasks
                .VerifySecondTaskLineStateVerdictTab("Pending", "1100L", "", "1", "0", "0");
            //Step line 39: Double click on the taskline you have created
            detailTaskPage
                .ClickOnTaskLineTab()
                .DoubleClickAnyTaskLine("2")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            int taskLineIdNew = detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .GetTaskLineId();
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLinesAfter[1]);
            //Line 41: Run query to get new taskline detail and verify
            List<TaskLineDBModel> taskLineDBModelsDetailNew = finder.GetTaskLine(taskLineIdNew);
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLinesAfter[1], taskLineDBModelsDetailNew[0]);
            //Step line 42: Click on data tab
            detailTaskLinePage
                .ClickOnDataTab()
                .VerifyNotDisplayErrorMessage();
            //Step line 43: Click on [History] tab
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();
            string[] expValueSecondTaskLineHistotyTab = { "0", "1100L", "0", "1", "0", "", "Pending", "Unticked", "2", "", "0", "0", "0", "0", "0", "Not Certified", "0" };
            detailTaskLinePage
                .VerifyActionCreateWithUserDisplay(timeCreatedSecondTaskLine, CommonConstants.ActionCreateSecondTaskLineInHistoryTaskLineDetail, expValueSecondTaskLineHistotyTab, AutoUser92.DisplayName);
            //Step line 45: Click back on details tab and change the state to Cancelled
            detailTaskLinePage
                .ClickOnDetailTab()
                .ChangeState("Cancelled")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskLinePage
                .VerifyCurrentTaskState("Cancelled");
            string secondTaskLineId = detailTaskLinePage
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
                .VerifySecondTaskLineStateVerdictTab("Cancelled", "660L", "", "1", "0", "0", secondTaskLineId);
            //Step line 48: Verify task lines
            detailTaskPage
                .ClickTasklinesTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            List<TaskLineModel> allTaskLines1After = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyTaskLineCreated(allTaskLines1After[1], "Service", "1100L", "1", "Cancelled");
            //Step line 49: Update the state to [Not Completed] and select one resolution code for last task line created (third task line)
            detailTaskPage
                .SelectAnyState(3, "Not Completed")
                .SelectAnyResolutionCode(3, "No key")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyStateAtAnyRow(3, "Not Completed")
                .VerifyResolutionCodeAtAnyRow(3, "No key");
            //Step line 50: Check hover for row with state = Completed and Not Completed
            detailTaskPage
                .VerifyWhenHoverElementAtAnyRow(taskLineModelNew.resolutionCode, "Completed", taskLineModelNew.siteProduct, taskLineModelNew.destinationSite, taskLineModelNew.product, taskLineModelNew.assetType, 1)
                .VerifyWhenHoverElementAtAnyRow("No key", "Not Completed", "", "", "General Recycling", "", 3);
            List<TaskLineModel> allTaskLinesAfter1 = detailTaskPage
                .GetAllTaskLineInTaskLineTab();

            //Step line 51: Query DB
            List<TaskLineDBModel> thirdTaskLine = finder.GetTaskLineByTaskId(taskLineId);

            //Step line 52: Task -> Verdict tab -> Task lines
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                .VerifyThirdTaskLineStateVerdictTab("Not Completed", "", "General Recycling", "1", "0", "0", "No key", manualConfirmedOnWeb)
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
                .VeriryConfirmationAndCompletedDate("", manualConfirmedOnWeb);
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
            List<TaskLineDBModel> taskLineDBModelsAfterRemoved = finder.GetTaskLine(int.Parse(taskIDWithSourceServiceTask));
            Assert.AreEqual(2, taskLineDBModelsAfterRemoved.Count);
            Assert.AreEqual(taskLineId, taskLineDBModelsAfterRemoved[0].tasklineID);
            Assert.AreEqual(secondTaskLineId, taskLineDBModelsAfterRemoved[1].tasklineID);
            //Step line 59: Task -> Verdict tab -> Task lines tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                .VerifyNumberOfTaskLine(2)
                .VerifyFirstTaskLineId(taskLineId.ToString())
                .VerifySecondTaskLineId(secondTaskLineId);
            //Step line 60: Change the state to pending in any taskline
            detailTaskPage
                .ClickTasklinesTab()
                .SelectAnyState(1, "Pending")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string timeUpdatedToPending = "";
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyStateAtAnyRow(1, "Pending")
                .VerifyResolutionCodeAtAnyRow(1, "")
                .VerifyWhenHoverElementAtAnyRow("", "", "", "", "", "", 1);
            //Step line 61: Double click on the taskline -> details tab
            detailTaskPage
                .DoubleClickAnyTaskLine("1")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .VerifyCurrentTaskState("Pending")
                .VeriryConfirmationAndCompletedDate("", "");
            //Step line 62: Click on [History] tab
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();
            string[] stateAfterUpdate = { "Pending" };
            string[] ActionUpdateStateInHistoryTaskLineDetail = { "State" };

            detailTaskLinePage
                .VerifyActionUpdateWithUserDisplay(timeUpdatedToPending, ActionUpdateStateInHistoryTaskLineDetail, stateAfterUpdate, AutoUser92.DisplayName, 1);

            //Step line 63: DB: Query
            List<TaskLineDBModel> taskLineDBModelsAfterUpdatedState = finder.GetTaskLineByTaskLineId(taskLineId);
            Assert.AreEqual(0, taskLineDBModelsAfterUpdatedState[0].autoconfirmed);
            Assert.AreEqual(null, taskLineDBModelsAfterUpdatedState[0].completeddate.ToString());
            //Step line 64: Task -> Verdict tab -> Task lines tab
            detailTaskLinePage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            detailTaskPage
                .ClickOnVerdictTab()
                //First tasks
                .VerifyFirstTaskLineStateVerdictTab(timeUpdated1, "Pending", "", taskLineModelNew.product, taskLineModelNew.actualProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.actualAssetQuantity)
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
            string updatedTimeTask = "";
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Step line 68: History and verify
            detailTaskPage
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            string[] titleUpdate = { "Task Reference", "Task notes", "Priority" };
            string[] expectedUpdate = { taskRef, taskNote, "Hight" };
            detailTaskPage
                .VerifyHistoryTabUpdate(AutoUser92.DisplayName, updatedTimeTask, titleUpdate, expectedUpdate);

        }

        [Category("Tasks/tasklines")]
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
                .VerifyDisplayColumnInAccountStatementTab(accountStatementColumn)
                //Step line 125: Map tab
                .ClickOnMapTab()
                .VerifyNotDisplayErrorMessage();
            detailTaskPage
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyTheDisplayOfLegendInMapTab();

        }

        [Category("Tasks/tasklines")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task"), Order(3)]
        public void TC_125_Detail_tasks_update_state_to_completed()
        {
            //Update task from In Progrees -> Completed
            string completedState = "Completed";

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
            string timeCompleted = "";
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage
                .VerifyTaskCompleteDate(timeCompleted)
                .VerifyCurrentTaskState(completedState)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Click on Task Lines and verify
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .VerifyStateAtAnyRow(1, "Completed")
                .VerifyStateAtAnyRow(2, "Cancelled")
                .VerifyAnyRowsInTaskLineAreReadonly(1)
                .VerifyAnyRowsInTaskLineAreReadonly(2)
                .VerifyWhenHoverElementAtAnyRow(manualConfirmedOnWeb, 1)
                //Step line 78: History tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage


        }
    }
}
