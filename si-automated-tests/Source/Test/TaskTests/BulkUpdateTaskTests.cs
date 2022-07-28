using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BulkUpdateTaskTests : BaseTest
    {
        //Check for one task from North Star Commercial
        [Category("Bulk update Task form")]
        [Test(Description = "Verify that a bulk update form can be opened from Tasks step 1 and 2")]
        public void TC_126_Verify_bulk_update_form_can_be_opened_from_Tasks_step_1_2()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star Commercial")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId("8866")
                .ClickCheckboxFirstTaskInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TasksBulkUpdatePage tasksBulkUpdatePage = PageFactoryManager.Get<TasksBulkUpdatePage>();
            //tasksBulkUpdatePage
            //    .IsTaskBulkUpdatePage("Commercial Collection", "1")
            //    .ClickFirstToggleArrow()
            //    .VerifyValueAfterClickAnyToggle("1")
            //    //Step 2 - Verify form:
            //    .VerifyTaskBulkUpdateForm()
            //    //Step 2 - 1. Untick [User Background Transaction] -> Click [Save] btn
            //    .ClickUserBackgroundTransactionCheckbox()
            //    .ClickSaveBtn()
            //    .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            ////Step 2 - 2. Untick [User Background Transaction] -> Click [Save and Close] btn
            //string note = "Note" + CommonUtil.GetRandomNumber(5);
            //tasksBulkUpdatePage
            //    .SendKeyInNoteInput(note)
            //    .ClickUserBackgroundTransactionCheckbox()
            //    .ClickSaveAndCloseBtn()
            //    .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .SwitchToChildWindow(1)
            //    .VerifyWindowClosed(1)
            //    .SwitchNewIFrame();
            ////Step 2 - 3.  Untick [User Background Transaction] -> Click [Close without Saving] button and confirm leaving the page 
            //PageFactoryManager.Get<TasksListingPage>()
            //    .ClickOnBulkUpdateBtn()
            //    .SwitchToLastWindow()
            //    .WaitForLoadingIconToDisappear();
            //string newNote = "New note" + CommonUtil.GetRandomNumber(5);
            Thread.Sleep(3000);
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage("Commercial Collection", "1")
                .ClickFirstToggleArrow()
                //Verify [State of the task type is auto populated]
                //.VerifyTaskStatePopulated("In Progress", "1")
                .SendKeyInNoteInput("hihi")
                //.ClickUserBackgroundTransactionCheckbox()
                .ClickCloseWithoutSavingBtn()
                .AcceptAlert()
                .SwitchToLastWindow()
                .SwitchToChildWindow(1)
                .VerifyWindowClosed(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage("Commercial Collection", "1")
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                //Verify [State of the task type is auto populated]
                .VerifyTaskStatePopulated("In Progress", "1")
                //Step 2 - 5. [Help] button
                .ClickHelpBtnAndVerify();

        }

        [Category("Bulk update Task form")]
        [Test(Description = "Verify that correct fields display on a Task Bulk Update form step 3")]
        public void TC_126_Verify_that_correct_fields_display_on_a_task_bulk_update_form_step_3()
        {
            string firstTaskId = "9088";
            string secondTaskId = "8900";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterMultipleTaskId(firstTaskId, secondTaskId)
                .ClickCheckboxMultipleTaskInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TasksBulkUpdatePage tasksBulkUpdatePage = PageFactoryManager.Get<TasksBulkUpdatePage>();
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage("Collect Communal Refuse", "2")
                .VerifyTaskTypeSecondTask("Collect Domestic Recycling")
                //Click First toggle and Verify
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                .VerifyTaskStatePopulated("Completed", "1")
                //Click Second toggle and verify
                .ScrollToBottomOfPage();
            tasksBulkUpdatePage
                .ClickSecondToggleArrow()
                .VerifyValueAfterClickAnyToggle("2")
                .VerifyTaskStatePopulated("In Progress", "2");
        }

        //[Category("Bulk update Task form")]
        //[Test(Description = "Verify that a bulk update form can be opened from Parties")]
        //public void TC_126_Verify_bulk_update_form_can_be_opened_from_Paties()
        //{
        //    int partyId = 43;
        //    string partyName = "Jaflong Tandoori";

        //    PageFactoryManager.Get<LoginPage>()
        //        .GoToURL(WebUrl.MainPageUrl);
        //    //Login
        //    PageFactoryManager.Get<LoginPage>()
        //        .IsOnLoginPage()
        //        .Login(AutoUser55.UserName, AutoUser55.Password)
        //        .IsOnHomePage(AutoUser55);
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption("Parties")
        //        .ExpandOption("North Star Commercial")
        //        .OpenOption("Parties")
        //        .SwitchNewIFrame();
        //    PageFactoryManager.Get<PartyCommonPage>()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<PartyCommonPage>()
        //        .FilterPartyById(partyId)
        //        .OpenFirstResult()
        //        .SwitchToLastWindow()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<DetailPartyPage>()
        //        .WaitForDetailPartyPageLoadedSuccessfully(partyName)
        //        //Click [Task] tab
        //        .ClickTabDropDown()
        //        .ClickTasksTab()
        //        .WaitForLoadingIconToDisappear();
        //    string taskId = "26";
        //    PageFactoryManager.Get<DetailPartyPage>()
        //        .FilterTaskId(taskId)
        //        .ClickFirstTaskCheckbox()
        //        .ClickBulkUpdateBtn()
        //        .SwitchToLastWindow()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<TasksBulkUpdatePage>()
        //        .IsTaskBulkUpdatePage("Deliver Commercial Bin", "1")
        //        .ClickToggleArrowAndVerify();
        //}

    }
}
