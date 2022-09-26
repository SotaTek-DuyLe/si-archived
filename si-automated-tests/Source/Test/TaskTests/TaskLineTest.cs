using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TaskLineTest : BaseTest
    {
        [Category("TaskLine")]
        [Test(Description = "Verify whether the history in TaskLine, is updating the Min and Max Asset and Product Qty correctly")]
        public void TC_170_Verify_whether_the_history_in_TaskLine_is_updating_the_Min_and_Max_Asset_and_Product_Qty_correctly()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password)
                .IsOnHomePage(AutoUser56);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId("13837")
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.DoubleClickFirstTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            TaskLineDetailPage taskLineDetailPage = PageFactoryManager.Get<TaskLineDetailPage>();
            taskLineDetailPage.ClickOnElement(taskLineDetailPage.DetailTab);
            taskLineDetailPage.WaitForLoadingIconToDisappear();
            taskLineDetailPage.SendKeys(taskLineDetailPage.MinAssetQty, "10");
            taskLineDetailPage.SendKeys(taskLineDetailPage.MaxAssetQty, "30");
            taskLineDetailPage.SendKeys(taskLineDetailPage.MinProductQty, "10");
            taskLineDetailPage.SendKeys(taskLineDetailPage.MaxProductQty, "40");
            taskLineDetailPage.ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Task Line")
                .WaitUntilToastMessageInvisible("Successfully saved Task Line");
            taskLineDetailPage.ClickOnElement(taskLineDetailPage.HistoryTab);
            taskLineDetailPage.WaitForLoadingIconToDisappear();
            string historyDetailContent = taskLineDetailPage.GetElementText(taskLineDetailPage.HistoryDetail);
            string[] updatedFields = historyDetailContent.Split(Environment.NewLine);
            string minAssetQtyStatus = updatedFields.FirstOrDefault(x => x.Contains("Minimum Asset Quantity: 10."));
            Assert.IsFalse(string.IsNullOrEmpty(minAssetQtyStatus));

            string maxAssetQtyStatus = updatedFields.FirstOrDefault(x => x.Contains("Maximum Asset Quantity: 30."));
            Assert.IsFalse(string.IsNullOrEmpty(maxAssetQtyStatus));

            string minProductQtyStatus = updatedFields.FirstOrDefault(x => x.Contains("Minimum Product Quantity: 10."));
            Assert.IsFalse(string.IsNullOrEmpty(minProductQtyStatus));

            string maxProductQtyStatus = updatedFields.FirstOrDefault(x => x.Contains("Maximum Product Quantity: 40."));
            Assert.IsFalse(string.IsNullOrEmpty(maxProductQtyStatus));

            //Verify whether the history in TaskLine, is updating the Min and Max Asset and Product Qty correctly to 0 if user update any other field in taskline form
            taskLineDetailPage.ClickOnElement(taskLineDetailPage.DetailTab);
            taskLineDetailPage.WaitForLoadingIconToDisappear();
            taskLineDetailPage.SelectTextFromDropDown(taskLineDetailPage.StateSelect, "Cancelled");
            taskLineDetailPage.ClickSaveBtn()
               .VerifyToastMessage("Successfully saved Task Line")
               .WaitUntilToastMessageInvisible("Successfully saved Task Line");
            taskLineDetailPage.ClickOnElement(taskLineDetailPage.HistoryTab);
            taskLineDetailPage.WaitForLoadingIconToDisappear();
            string historyDetailContent2 = taskLineDetailPage.GetElementText(taskLineDetailPage.HistoryDetail);
            string[] updatedFields2 = historyDetailContent2.Split(Environment.NewLine).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            Assert.IsTrue(updatedFields2.Length == 1);
            Assert.IsTrue(updatedFields2.FirstOrDefault().Contains("State: Cancelled."));
        }

        [Category("AgreementTask")]
        [Test(Description = "Verify whether Taskline form is inheriting Completed Date from Task form when user set Not Completed Status on Task form(when core state default is tick)")]
        public void TC_204_Taskline_is_not_inheriting_Completed_Date()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=TaskType&ObjectID=3#");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password);
            var taskTypePage = PageFactoryManager.Get<TaskTypeEchoExtraPage>();
            taskTypePage.WaitForLoadingIconToDisappear();
            taskTypePage.SelectByDisplayValueOnUlElement(taskTypePage.TabPage, "States");
            taskTypePage.VerifyCheckboxIsSelected(taskTypePage.GetNotCompleteTaskTypeStateCheckboxXPath("14"), true);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId("15218")
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string completedDateDisplayed = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Not Completed")
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyTaskLineState("Not Completed");
            detailTaskPage.DoubleClickFirstTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            TaskLineDetailPage taskLineDetailPage = PageFactoryManager.Get<TaskLineDetailPage>();
            taskLineDetailPage.ClickOnElement(taskLineDetailPage.DetailTab);
            taskLineDetailPage.WaitForLoadingIconToDisappear();
            //Bug
            taskLineDetailPage.VerifyInputValue(taskLineDetailPage.CompleteDateInput, "")
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyTaskCompleteDate(completedDateDisplayed);
        }

        [Category("AgreementTask")]
        [Test(Description = "")]
        public void TC_150_Taskstate_Overrides_Tasklinestate()
        {
            //Verify whether user able to open the Task Line tab

            //Verify whether None of the tasklines updated and taskstate set to Completed - all tasklines get tasklinestate Completed of the same coretaskstate and actual and product qty updated to the 
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=TaskType&ObjectID=4");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password);
            var taskTypePage = PageFactoryManager.Get<TaskTypeEchoExtraPage>();
            taskTypePage.WaitForLoadingIconToDisappear();
            taskTypePage.SelectByDisplayValueOnUlElement(taskTypePage.TabPage, "States");
            taskTypePage.VerifyCheckboxIsSelected(taskTypePage.GetNotCompleteTaskTypeStateCheckboxXPath("14"), true);
            taskTypePage.VerifyCheckboxIsSelected(taskTypePage.GetCompleteTaskTypeStateCheckboxXPath("13"), true);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId("15025")
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Completed")
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyTaskLineState("Completed");

            //Verify whether if user select a Taskline State Not Completed and Task State unallocated , and save the update, then the Taskline State is saved with Not Completed state. 
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=TaskType&ObjectID=2#");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password);
            taskTypePage.WaitForLoadingIconToDisappear();
            taskTypePage.SelectByDisplayValueOnUlElement(taskTypePage.TabPage, "States");
            taskTypePage.VerifyCheckboxIsSelected(taskTypePage.GetNotCompleteTaskTypeStateCheckboxXPath("9"), true);
            taskTypePage.VerifyCheckboxIsSelected(taskTypePage.GetCompleteTaskTypeStateCheckboxXPath("8"), true);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId("1990")
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Unallocated");
            string completeDate = detailTaskPage.GetInputValue(detailTaskPage.completionDateInput);
            DateTime completeDateTime = CommonUtil.StringToDateTime(completeDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailTaskPage.SendKeys(detailTaskPage.endDateInput, completeDateTime.AddDays(10).ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT));
            detailTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyTaskLineState("Not Completed");

            //Verify whether if user select a Taskline State Not Completed and Task State Completed , and save the update, then the Taskline State is not saved with Completed
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=TaskType&ObjectID=3");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password);
            taskTypePage.WaitForLoadingIconToDisappear();
            taskTypePage.SelectByDisplayValueOnUlElement(taskTypePage.TabPage, "States");
            taskTypePage.VerifyCheckboxIsSelected(taskTypePage.GetNotCompleteTaskTypeStateCheckboxXPath("14"), true);
            taskTypePage.VerifyCheckboxIsSelected(taskTypePage.GetCompleteTaskTypeStateCheckboxXPath("13"), true);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId("231")
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.InputTaskLineState("Not Completed");
            detailTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnDetailTab()
               .WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Completed");
            detailTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyTaskLineState("Not Completed");

            //Verify whether user able to update Taskstate using Bulk Update from Task Grid

        }
    }
}
