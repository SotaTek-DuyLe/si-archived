using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskLineTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TaskLineStateTests : BaseTest
    {
        [Category("Task line State")]
        [Category("Chang")]
        [Test(Description = "Task line state being set with res code that doesn't have isdefault=true  (bug fix)")]
        public void TC_148_Task_line_state_being_set_with_res_code_that_doesn_not_have_isdefault_true()
        {
            string taskId = "13639";
            CommonFinder finder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser51.UserName, AutoUser51.Password)
                .IsOnHomePage(AutoUser51);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId(taskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                //Line 8: Click on [Task line] tab and verify
                .ClickOnTaskLineTab()
                .VerifyStateOfFirstRowInTaskLineTab("Pending")
                //Line 9: Click on [Detail] tab and set state = Completed
                .ClickOnDetailTab()
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd("Not Completed")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeUpdatedExp = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string completedDateDisplayed = detailTaskPage
                .VerifyCompletedDateNotEmpty()
                .GetCompletedDateDisplayed();
            string endDateDisplayed = detailTaskPage
                .GetEndDateDisplayed();
            //Line 10: Verify [Task line] after changing to Completed
            detailTaskPage
                .ClickOnTaskLineTab()
                .VerifyAllColumnInFirstRowDisabled()
                .VerifyStateOfFirstRowInTaskLineTab("Not Completed")
                .VerifyResoluctionCodeFirstRowInTaskLineTab("")
                //Line 11: Verify [History] tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInSeconds(1);
            detailTaskPage
                .VerifyTitleTaskLineFirstServiceUpdate()
                .VerifyTitleUpdate()
                .VerifyHistoryTabUpdate(AutoUser51.DisplayName, timeUpdatedExp, completedDateDisplayed, "Not Completed", endDateDisplayed);

           detailTaskPage
                .VerifyHistoryTabFirstAfterChangingStatus(AutoUser51.DisplayName, timeUpdatedExp, "0", "0", "Not Completed", "", completedDateDisplayed, "Manually Confirmed on Web");
            //Line 12: Run query to check => Need to config on master
            List<TaskLineDBModel> taskLineDBModels = finder.GetTaskLineByTaskId(int.Parse(taskId));
            int resolutioncodeId = taskLineDBModels[0].resolutioncodeID;
            Assert.AreEqual(resolutioncodeId, 0);
        }
    }
}
