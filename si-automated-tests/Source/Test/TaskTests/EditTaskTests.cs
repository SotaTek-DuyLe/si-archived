using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models.ServiceStatus;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.DebriefResult;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class EditTaskTests : BaseTest
    {
        [Category("EditTask")]
        [Category("Chang")]
        [Test(Description = "Can edit a task after the round instance is posted (bug fix)")]
        public void TC_200_Can_Edit_a_task_after_the_round_instance_is_posted()
        {
            CommonFinder common = new CommonFinder(DbContext);
            string roundInstanceId = "12568";
            string completedStatus = "Completed";
            string notCompletedStaus = "Not Completed";
            string cancelledStatus = "Cancelled";
            string taskIdNotInvoiced = "52901";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser93.UserName, AutoUser93.Password)
                .IsOnHomePage(AutoUser93);
            //Go to round instance detail
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/round-instances/" + roundInstanceId);
            PageFactoryManager.Get<RoundInstanceForm>()
                .WaitForLoadingIconToDisappear();
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundInstanceForm
                .IsRoundInstanceForm()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            List<TaskInWorksheetModel> listTasks = roundInstanceForm
                .ClickOnExpandRoundBtn()
                .GetAllTaskInWorksheetTab(2);
            //Step line 10: Update most of the tasks to [Completed] [Not Completed] and [Cancelled]
            //Completed
            roundInstanceForm
                .SelectOneTaskInGrid(listTasks[0].checkboxLocator)
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdateForm(completedStatus)
                .ClickOnConfirmBtnBulkUpdateForm()
                .VerifyDisplayToastMessage(MessageSuccessConstants.TaskSavedSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.TaskSavedSuccessMessage);
            //Not Completed
            roundInstanceForm
                .UncheckedAnyTask("1")
                .SelectOneTaskInGrid(listTasks[0].checkboxLocator)
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdateForm(notCompletedStaus)
                .ClickOnConfirmBtnBulkUpdateForm()
                .VerifyDisplayToastMessage(MessageSuccessConstants.TaskSavedSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.TaskSavedSuccessMessage);
            //Cancelled
            roundInstanceForm
                .ClickOnSelectAllBtn()
                .UncheckedAnyTask("1")
                .UncheckedAnyTask("1")
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdateForm(cancelledStatus)
                .ClickOnConfirmBtnBulkUpdateForm()
                .VerifyDisplayToastMessage(MessageSuccessConstants.TaskSavedSuccessMessage)
                .WaitUntilAllToastMessageInvisible();
            //Step line 13: Click on [Debrief] btn
            roundInstanceForm
                .SwitchToDefaultContent();
            roundInstanceForm
                .ClickOnDebriefBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DebriefResultPage debriefResultPage = PageFactoryManager.Get<DebriefResultPage>();
            debriefResultPage
                .WaitForDebriefLoaded()
                .ClickOnActivityTab()
                .ClickOnEventsTab()
                .ClickOnImgInEventTab()
                .SelectReasonTaken("Out of Time")
                .ClickOnInspectionsTab()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SavedMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SavedMessage);
            //Step line 14: Click on [Post] btn
            debriefResultPage
                .CLickOnPostBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.PostedSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.PostedSuccessMessage);
            debriefResultPage
                .CloseCurrentWindow()
                .SwitchToChildWindow(1);
            roundInstanceForm
                .Refresh()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .IsRoundInstanceForm()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .ClickOnExpandRoundBtn()
                .WaitForAllTasksVisibled()
                .FilterTaskByStatusInWorksheetTab(completedStatus)
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .SrollLeftToCheckboxTask()
                .DoubleClickOnFirstTaskInGrid()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Step line 15: Double click on any task in completed state - Details tab
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyAllDataInDetailsTabAreLocked()
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 16: Double click on any task in Not Completed state - Details tab
            roundInstanceForm
                .Refresh()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .IsRoundInstanceForm()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .ClickOnExpandRoundBtn()
                .WaitForAllTasksVisibled()
                .FilterTaskByStatusInWorksheetTab(notCompletedStaus)
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .SrollLeftToCheckboxTask()
                .DoubleClickOnFirstTaskInGrid()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyAllDataInDetailsTabAreLocked()
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 16: Double click on any task in Cancelled state - Details tab
            roundInstanceForm
                .Refresh()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .IsRoundInstanceForm()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .ClickOnExpandRoundBtn()
                .WaitForAllTasksVisibled()
                .FilterTaskByStatusInWorksheetTab(cancelledStatus)
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .SrollLeftToCheckboxTask()
                .DoubleClickOnFirstTaskInGrid()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyAllDataInDetailsTabAreLocked()
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 20: CLick on [Debrief] btn again
            roundInstanceForm
                .ClickOnDebriefBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            debriefResultPage
                .WaitForDebriefLoaded()
                .CLickOnUnPostBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.UnPostedSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.UnPostedSuccessMessage);
            debriefResultPage
                .CloseCurrentWindow()
                .SwitchToChildWindow(1);
            //Step line 21: Back to [Detail] tab and verify [Round instance] is in completed state
            roundInstanceForm
                 .Refresh()
                 .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .IsRoundInstanceForm()
                .ClickOnDetailTab()
                .VerifyStatusDetailTab("Complete");
            roundInstanceForm
                 .Refresh()
                 .WaitForLoadingIconToDisappear();
            //Step line 22: Double click on any task in completed state and verify Task is locked
            roundInstanceForm
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .ClickOnExpandRoundBtn()
                .WaitForAllTasksVisibled()
                .FilterTaskByStatusInWorksheetTab(completedStatus)
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .SrollLeftToCheckboxTask()
                .DoubleClickOnFirstTaskInGrid()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyAllDataInDetailsTabAreLocked();
            string taskCompleted = detailTaskPage
                .GetTaskId();
            detailTaskPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            roundInstanceForm
                .Refresh()
                .WaitForLoadingIconToDisappear();
            //Step line 23: Double click on any task in not completed state and verify Task is locked
            roundInstanceForm
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .ClickOnExpandRoundBtn()
                .WaitForAllTasksVisibled()
                .FilterTaskByStatusInWorksheetTab(notCompletedStaus)
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .SrollLeftToCheckboxTask()
                .DoubleClickOnFirstTaskInGrid()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyAllDataInDetailsTabAreLocked();
            string taskNotCompleted = detailTaskPage
                .GetTaskId();
            detailTaskPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            roundInstanceForm
                .Refresh()
                .WaitForLoadingIconToDisappear();
            //Step line 24: Double click on any task in cancelled state and verify Task is locked
            roundInstanceForm
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .ClickOnExpandRoundBtn()
                .WaitForAllTasksVisibled()
                .FilterTaskByStatusInWorksheetTab(cancelledStatus)
                .WaitForLoadingIconToDisappear();
            roundInstanceForm
                .SrollLeftToCheckboxTask()
                .DoubleClickOnFirstTaskInGrid()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyAllDataInDetailsTabAreLocked();
            string taskCancelled = detailTaskPage
                .GetTaskId();
            detailTaskPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 26: Query DB - Completed
            List<SaleInvoicePriceLinesDBModel> saleInvoicePriceLinesDBModelCompleted = common.GetSaleInvoicePriceLineByPriceEchoTypeAndPriceEcho("177", taskCompleted);
            List<SaleInvoicePriceLinesDBModel> saleInvoicePriceLinesDBModelNotCompleted = common.GetSaleInvoicePriceLineByPriceEchoTypeAndPriceEcho("177", taskNotCompleted);
            List<SaleInvoicePriceLinesDBModel> saleInvoicePriceLinesDBModelCancelled = common.GetSaleInvoicePriceLineByPriceEchoTypeAndPriceEcho("177", taskCancelled);
            Assert.AreEqual(11, saleInvoicePriceLinesDBModelCompleted[0].salesinvoicebatchID);
            Assert.AreEqual(11, saleInvoicePriceLinesDBModelNotCompleted[0].salesinvoicebatchID);
            Assert.AreEqual(11, saleInvoicePriceLinesDBModelCancelled[0].salesinvoicebatchID);
            //Step line 27: Open task is not invoiced

            //Step line 29: Change task from Not Completed to Inprogress
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/tasks/" + taskIdNotInvoiced);
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyAllDataInDetailsTabAreEditabled()
                .SelectAnyTaskStateInDd(completedStatus)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Step line 28: Change task from Completed to Not Completed
            detailTaskPage
                .VerifyAllDataInDetailsTabAreEditabled()
                .SelectAnyTaskStateInDd(notCompletedStaus)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            //Verify that task is not invoiced
            List<SaleInvoicePriceLinesDBModel> saleInvoicePriceLinesDBModelNotInvoiced = common.GetSaleInvoicePriceLineByPriceEchoTypeAndPriceEcho("177", taskIdNotInvoiced);
            Assert.AreEqual(0, saleInvoicePriceLinesDBModelNotInvoiced.Count);
        }
    }
}
