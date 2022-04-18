using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements;
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
            int taskId1 = 408;
            int taskId2 = 415;
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
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
                .ClickMainOption("Parties")
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
            int[] taskIdList = {524, 483};
            String note = "test bulk update";
            String todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Bulk Update Task
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
                .VerifyToastMessage("Successfully saved Task")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .SleepTimeInMiliseconds(2000);
            //Verify bulked update for tasks
            PageFactoryManager.Get<TaskTab>()
                .VerifyRetiredTaskWithIds(taskIdList)
                .VerifyTaskStateWithIds(taskIdList, "Completed");
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
                    .VerifyCompletionDate(todayDate)
                    .VerifyEndDate(todayDate)
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
                .ClickMainOption("Tasks")
                .OpenOption("North Star Commercial")
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
                    .VerifyCompletionDate(todayDate)
                    .VerifyEndDate(todayDate)
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
            int[] taskIdList = {478, 481};
            String note = "test bulk update";
            String todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved Task")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .ClickRefreshBtn()
                .ClickRefreshBtn();
            //Verify bulked update for tasks
            PageFactoryManager.Get<TaskTab>()
                .VerifyRetiredTaskWithIds(taskIdList)
                .VerifyTaskStateWithIds(taskIdList, "Completed");
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
                    .VerifyCompletionDate(todayDate)
                    .VerifyEndDate(todayDate)
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
                .ClickMainOption("Tasks")
                .OpenOption("North Star Commercial")
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
                    .VerifyCompletionDate(todayDate)
                    .VerifyEndDate(todayDate)
                    .VerifyTaskState("Completed")
                    .VerifyNote(note)
                    .CloseCurrentWindow()
                    .SwitchToFirstWindow()
                    .SwitchNewIFrame();
            }
        }
    }
}
