using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementLine;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using si_automated_tests.Source.Main.Pages.Task;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.AggrementLineTest
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class AgreementLineTaskTest : BaseTest
    {
        [Category("AgreementTask")]
        [Test]
        public void TC_072_Delete_Agreement_Line_Task()
        {
            int agreementId = 38;
            
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            //Verify that user can delete a task from an Agreement
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully("COMMERCIAL COLLECTIONS", "TWISTED FISH LIMITED");
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskId1 = PageFactoryManager.Get<TaskTab>()
                .getFirstTaskId();
            PageFactoryManager.Get<TaskTab>()
                .SelectATask(taskId1)
                .ClickDeleteItem()
                .SwitchToLastWindow();
            //Verify Remove Task Page and Click No
            PageFactoryManager.Get<RemoveTaskPage>()
                .VerifyRemoveTaskPage()
                .ClickNoBtn()
                .SwitchToChildWindow(2);
            //Back To Task Tab and verify the task is not deleted 
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskAppearWithID(taskId1)
                .SelectATask(taskId1)
                .ClickDeleteItem()
                .SwitchToLastWindow();
            //Delete the task
            PageFactoryManager.Get<RemoveTaskPage>()
                .VerifyRemoveTaskPage()
                .ClickYesBtn()
                .SwitchToChildWindow(2);
            //Verify the task is deleted 
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskDisappearWithID(taskId1)
                .CloseCurrentWindow()
                .SwitchToFirstWindow();

            //Verify that user can delete a task from an Agreement Line
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .ClickTasksTab();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskId2 = PageFactoryManager.Get<TaskTab>()
                .getSecondTaskId();
            PageFactoryManager.Get<TaskTab>()
                .SelectATask(taskId2)
                .ClickDeleteItem()
                .SwitchToLastWindow();
            //Verify Remove Task Page and Click No
            PageFactoryManager.Get<RemoveTaskPage>()
                .VerifyRemoveTaskPage()
                .ClickNoBtn()
                .SwitchToChildWindow(2);
            //Back To Task Tab and verify the task is not deleted 
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskAppearWithID(taskId2)
                .SelectATask(taskId2)
                .ClickDeleteItem()
                .SwitchToLastWindow();
            //Delete the task
            PageFactoryManager.Get<RemoveTaskPage>()
                .VerifyRemoveTaskPage()
                .ClickYesBtn()
                .SwitchToChildWindow(2);
            //Verify the task is deleted 
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskDisappearWithID(taskId2)
                .SwitchToFirstWindow();
        }

        [Category("AgreementTask")]
        [Test]
        public void TC_073_Bulk_Update_Tasks_A()
        {
            //Verify that user can bulk update tasks from an Agreement
            int agreementId = 38;
            
            String note = "test bulk update";
            String todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            //Filter Agreement
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully("COMMERCIAL COLLECTIONS", "TWISTED FISH LIMITED");
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Bulk Update Task
            int taskId1 = PageFactoryManager.Get<TaskTab>()
                .getFirstTaskId();
            int taskId2 = PageFactoryManager.Get<TaskTab>()
                .getSecondTaskId();
            int[] taskIdList = { taskId1, taskId2 };
            PageFactoryManager.Get<TaskTab>()
                .SelectMultipleTask(taskIdList)
                .ClickBulkUpdateItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<BulkUpdatePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BulkUpdatePage>()
                .VerifyBulkUpdatePage(taskIdList.Length)
                .ExpandStandardCommercialCollection()
                .SelectCompletedState()
                .ClickResolutionText()
                .ClickTaskCompletedDate()
                .ClickResolutionText()
                .VerifyTaskCompletedDateValue(todayDate)
                .ClickTaskEndDate()
                .ClickResolutionText()
                .VerifyTaskEndDateValue(todayDate)
                .InputNote(note)
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Verify bulked update for tasks
            PageFactoryManager.Get<TaskTab>()
                .VerifyRetiredTaskWithIds(taskIdList)
                .VerifyTaskStateWithIds(taskIdList, "Completed");
            for(int i = 0; i < taskIdList.Length; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATaskById(taskIdList[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab();
                PageFactoryManager.Get<TaskDetailTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyEndDate(todayDate);
                String dueDate = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(dueDate)
                    .VerifyTaskState("Completed")
                    .VerifyNote(note)
                    .CloseCurrentWindow()
                    .SwitchToChildWindow(2);
            }
            PageFactoryManager.Get<TaskTab>()
                .CloseCurrentWindow()
                .SwitchToFirstWindow();

            //Go to Task Page and Verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonTaskPage>()
                .WaitForLoadingIconToDisappear();
            foreach (int i in taskIdList)
            {
                PageFactoryManager.Get<CommonTaskPage>()
                    .FilterTaskId(i)
                    .OpenTaskWithId(i)
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab();
                PageFactoryManager.Get<TaskDetailTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyEndDate(todayDate);
                String endDate1 = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(endDate1)
                    .VerifyTaskState("Completed")
                    .VerifyNote(note)
                    .CloseCurrentWindow()
                    .SwitchToFirstWindow()
                    .SwitchNewIFrame();
            }
        }

        [Category("AgreementTask")]
        [Test]
        public void TC_073_Bulk_Update_Tasks_B()
        {
            //Verify that user can bulk update tasks from an Agreement Line
            int agreementId = 38;

            String note = "test bulk update";
            String todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            //Filter Agreement
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementId.ToString())
                .ClickTasksTab();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Bulk Update Task
            int taskId1 = PageFactoryManager.Get<TaskTab>()
                .getThirdTaskId();
            int taskId2 = PageFactoryManager.Get<TaskTab>()
                .getFourthTaskId();
            int[] taskIdList = { taskId1, taskId2 };
            PageFactoryManager.Get<TaskTab>()
                .SelectMultipleTask(taskIdList)
                .ClickBulkUpdateItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<BulkUpdatePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BulkUpdatePage>()
                .VerifyBulkUpdatePage(taskIdList.Length)
                .ExpandStandardCommercialCollection()
                .SelectCompletedState()
                .ClickResolutionText()
                .ClickTaskCompletedDate()
                .ClickResolutionText()
                .VerifyTaskCompletedDateValue(todayDate)
                .ClickTaskEndDate()
                .ClickResolutionText()
                .VerifyTaskEndDateValue(todayDate)
                .InputNote(note)
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Verify bulked update for tasks
            PageFactoryManager.Get<AgreementLineTaskTab>()
                .VerifyRetiredTaskWithIds(taskIdList);
            PageFactoryManager.Get<AgreementLineTaskTab>()
                .VerifyTaskStateWithIdsAgreementLine(taskIdList, "Completed");
            foreach (int i in taskIdList)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATaskById(i)
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab();
                PageFactoryManager.Get<TaskDetailTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyEndDate(todayDate);
                String dueDate = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(dueDate)
                    .VerifyTaskState("Completed")
                    .VerifyNote(note)
                    .CloseCurrentWindow()
                    .SwitchToChildWindow(2);
            }
            PageFactoryManager.Get<TaskTab>()
                .CloseCurrentWindow()
                .SwitchToFirstWindow();

            //Go to Task Page and Verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonTaskPage>()
                .WaitForLoadingIconToDisappear();
            foreach (int i in taskIdList)
            {
                PageFactoryManager.Get<CommonTaskPage>()
                    .FilterTaskId(i)
                    .OpenTaskWithId(i)
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab();
                PageFactoryManager.Get<TaskDetailTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyEndDate(todayDate);
                String dueDate1 = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(dueDate1)
                    .VerifyTaskState("Completed")
                    .VerifyNote(note)
                    .CloseCurrentWindow()
                    .SwitchToFirstWindow()
                    .SwitchNewIFrame();
            }
        }

        [Category("AgreementTask")]
        [Test(Description = "Verify whether task form is loading when childtask line is there")]
        public void TC_173_Tasklines_with_details_loaded_in_Task_Form()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonTaskPage>()
                    .FilterTaskId(12243)
                    .OpenTaskWithId(12243)
                    .SwitchToLastWindow();
            var agreementTaskDetailPage = PageFactoryManager.Get<AgreementTaskDetailsPage>();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.ClickToTaskLinesTab();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.VerifyToastMessagesIsUnDisplayed();
            agreementTaskDetailPage.VerifyElementVisibility(agreementTaskDetailPage.AddNewAgreementTaskDetailButton, true);
            agreementTaskDetailPage.VerifyHeaderColumn();
        }
    }
}
