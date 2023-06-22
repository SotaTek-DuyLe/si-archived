using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using TaskAllocationPage = si_automated_tests.Source.Main.Pages.Applications.TaskAllocationPage;

namespace si_automated_tests.Source.Test.ApplicationTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TaskAllocationTest : BaseTest
    {
        [Category("TaskAllocationTests")]
        [Category("Huong")]
        [Test(Description = "152_Task Allocation>Outstanding Tasks")]
        public void TC_152_Verify_that_Outstanding_tasks_display_in_the_Task_Allocation()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();

            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "18/05/2022";
            string to = "20/05/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .SelectRoundNode("Recycling");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ShowOutstandingTaskButton);
            taskAllocationPage.VerifyElementVisibility(taskAllocationPage.OutstandingTab, true);

            SqlCommand command = new SqlCommand("GetOutstandingTasks", DbContext.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ServiceIDs", SqlDbType.Int).Value = 6;
            command.Parameters.Add("@start ", SqlDbType.VarChar).Value = "18 May 2022";
            command.Parameters.Add("@finish", SqlDbType.VarChar).Value = "20 May 2022";
            command.Parameters.Add("@mode ", SqlDbType.Int).Value = 1;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                var outstandingTasks = ObjectExtention.DataReaderMapToList<OutstandingTaskModel>(reader);
                foreach(var item in outstandingTasks)
                {
                    Console.WriteLine("xxxx" + outstandingTasks.Count);
                    Console.WriteLine(item.ID);
                }
                taskAllocationPage.VerifyOutStandingData(outstandingTasks);
            }
        }

        [Category("TaskAllocationTests")]
        [Category("Huong")]
        [Test(Description = "156_Reallocation Reasons-Round Legs and Tasks")]
        public void TC_156_1_Verify_that_Reallocation_reasons_display_correctly_and_are_set_when_user_reallocates_item_by_dragging_abd_dropping_them_TASKS()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();

            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "02/08/2022";
            string to = "03/08/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Commercial);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Commercial)
                .ExpandRoundNode("Collections")
                .SelectRoundNode("Commercial Collections");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Control + "a");
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Delete);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.SendKeys(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("REC1-AM", "Tuesday")
                .WaitForLoadingIconToDisappear(false);
            taskAllocationPage.ClickUnallocatedRow();
            taskAllocationPage.SleepTimeInMiliseconds(200);
            taskAllocationPage.ClickUnallocatedRow(1);
            taskAllocationPage.SleepTimeInMiliseconds(200);
            taskAllocationPage.ClickUnallocatedRow(2);
            taskAllocationPage.SleepTimeInMiliseconds(200);
            taskAllocationPage.DragUnallocatedRowToRoundInstance("REC1-AM", "Wednesday")
                .VerifyElementVisibility(taskAllocationPage.GetAllocatingConfirmMsg(3), true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            List<string> reasons = new List<string>() { "Select...", "Bad Weather", "Breakdown", "Roadworks", "Out of Time", "Incident", "Access Blocked" };
            taskAllocationPage.VerifyElementVisibility(taskAllocationPage.ReasonConfirmMsg, true)
                .VerifySelectContainDisplayValues(taskAllocationPage.ReasonSelect, reasons)
                .SelectTextFromDropDown(taskAllocationPage.ReasonSelect, "Breakdown")
                .ClickOnElement(taskAllocationPage.ReasonConfirmButton);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.VerifyToastMessage("Task(s) Allocated");

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            var taskReallocations = finder.GetTaskReAllocationModels(new List<int>() { 9174, 9173, 9175 });
            Assert.IsTrue(taskReallocations.Count == 3);
            foreach (var taskReallocation in taskReallocations)
            {
                Assert.IsNotNull(finder.GetResolutionCodeModels(taskReallocation.reason_resolutioncodeID).FirstOrDefault(x => x.resolutioncode == "Breakdown"));
            }
        }

        [Category("TaskAllocationTests")]
        [Category("Huong")]
        [Test(Description = "156_Reallocation Reasons-Round Legs and Tasks")]
        public void TC_156_2_Verify_that_Reallocation_reasons_display_correctly_and_are_set_when_user_reallocates_item_by_using_REALLOCATE_button()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();

            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "02/08/2022";
            string to = "02/08/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .ExpandRoundNode("Recycling")
                .SelectRoundNode("Communal Recycling");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Control + "a");
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Delete);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.SendKeys(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.DoubleClickRI("ECREC1", "Tuesday")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundInstanceForm.ClickOnElement(roundInstanceForm.WorksheetTab);
            roundInstanceForm.SwitchToFrame(roundInstanceForm.WorkSheetIframe);
            roundInstanceForm.WaitForLoadingIconToDisappear();
            roundInstanceForm.ClickOnElement(roundInstanceForm.ToggleRoundButton);
            roundInstanceForm.SleepTimeInMiliseconds(1000);
            var descriptions = roundInstanceForm.SelectRLI(new List<string>() { "-4764", "-4813", "-4814" });
            roundInstanceForm.ClickOnElement(roundInstanceForm.ReallocateButton);
            roundInstanceForm.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Control + "a");
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Delete);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.SendKeys(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            List<RoundInstanceModel> roundInstances = taskAllocationPage.SelectExpandedUnallocated(3);
            taskAllocationPage.DragUnallocatedRowToRoundInstance("ECREC1", "Wednesday")
                .VerifyElementVisibility(taskAllocationPage.GetAllocatingConfirmMsg(5), true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            List<string> reasons = new List<string>() { "Select...", "Breakdown", "Bad Weather", "Incident", "Parked Cars", "Out of Time" };
            taskAllocationPage.VerifyElementVisibility(taskAllocationPage.ReasonConfirmMsg, true)
                .VerifySelectContainDisplayValues(taskAllocationPage.ReasonSelect, reasons)
                .SelectTextFromDropDown(taskAllocationPage.ReasonSelect, "Bad Weather")
                .ClickOnElement(taskAllocationPage.ReasonConfirmButton);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.VerifyToastMessage("Allocated 3 round leg(s) (with 5 service task(s))")
                .WaitUntilToastMessageInvisible("Allocated 3 round leg(s) (with 5 service task(s))")
                .SleepTimeInMiliseconds(300);
            taskAllocationPage.DragRoundInstanceToReallocattedGrid("ECREC1", "Wednesday", 4)
                .WaitForLoadingIconToDisappear(false);
            var RLIIds = taskAllocationPage.GetRoundLegInstanceIds(descriptions);
            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            var RLIReallocations = finder.GetRoundLegInstanceReallocationsModel(RLIIds);
            Assert.IsTrue(RLIReallocations.Count == 3);
            foreach (var RLIReallocation in RLIReallocations)
            {
                Assert.IsNotNull(finder.GetResolutionCodeModels(RLIReallocation.reason_resolutioncodeID).FirstOrDefault(x => x.resolutioncode == "Bad Weather"));
            }
        }

        [Category("TaskAllocationTests")]
        [Category("Huong")]
        [Test(Description = "182_Round Instance Events grid")]
        public void TC_182_Round_Instance_Events_grid()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "07/09/2022";
            string to = "07/09/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Commercial);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Commercial)
                .ExpandRoundNode("Collections")
                .SelectRoundNode("Commercial Collections");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Control + "a");
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Delete);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.SendKeys(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.DoubleClickRI("REC1-AM", "Wednesday")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundInstanceForm.ClickOnElement(roundInstanceForm.EventsTab);
            roundInstanceForm.WaitForLoadingIconToDisappear();
            roundInstanceForm.WaitForLoadingIconToDisappear();
            roundInstanceForm.ClickOnElement(roundInstanceForm.AddNewEventItemButton);
            roundInstanceForm.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            RoundInstanceEventPage roundInstanceEventPage = PageFactoryManager.Get<RoundInstanceEventPage>();
            roundInstanceEventPage.SelectTextFromDropDown(roundInstanceEventPage.RoundEventTypeSelect, "Tipping")
                .SelectTextFromDropDown(roundInstanceEventPage.ResourceSelect, "COM2 NST")
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            roundInstanceForm.ClickOnElement(roundInstanceForm.RefreshButton);
            roundInstanceForm.WaitForLoadingIconToDisappear();
            roundInstanceForm.VerifyNewRoundInstanceEventData("Tipping", "COM2 NST");
        }

        [Category("TaskAllocationTests")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test(Description = "190_A negative slot count appears on round from which the tasks were reallocated")]
        public void TC_190_A_negative_slot_count_appears_on_round_from_which_the_tasks_were_reallocated()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "06/09/2022";
            string to = "06/09/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .SelectRoundNode("Recycling");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Control + "a");
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Delete);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.SendKeys(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("EDREC3", "Tuesday");
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.ClickOnElement(taskAllocationPage.ToggleRoundLegsButton);
            taskAllocationPage.SleepTimeInMiliseconds(300);
            List<RoundInstanceModel> roundInstanceDetails = taskAllocationPage.SelectRoundLegs(2);
            taskAllocationPage.DragRoundLegRowToRoundInstance("EDREC1", "Wednesday", 4)
                .VerifyElementVisibility(taskAllocationPage.GetAllocatingConfirmMsg(roundInstanceDetails.Count), true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton);
            taskAllocationPage.WaitForLoadingIconToDisappear()
                .VerifyToastMessages(new List<string>() { "Task(s) Allocated" });
            taskAllocationPage.VerifyTaskAllocated("EDREC1", "Wednesday");
        }

        [Category("TaskAllocationTests")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_244_Task_Confirmation()
        {
            //Verify whether the order of buttons in Task Confirmation page is changed to Popup, Save,Refresh, Help
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "06/09/2022";
            string to = "06/09/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .SelectRoundNode("Recycling");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Control + "a");
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, Keys.Delete);
            taskAllocationPage.SendKeysWithoutClear(taskAllocationPage.FromInput, from);
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.SendKeys(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.ClickPopoutButton()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            taskAllocationPage.IsTaskAllocationPage();
            taskAllocationPage.CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            taskAllocationPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();

            taskAllocationPage.ClickHelp()
                .WaitForLoadingIconToDisappear()
                .IsInformationModalDisplay()
                .ClickCloseInformationModal()
                .WaitForLoadingIconToDisappear();

            //Verify whether new functionality for Save button is added so that user can save screen parameter selection and Hover over message Save Selection
            taskAllocationPage.ClickSaveSelectionButton()
                .VerifyToastMessage("Saved")
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            taskAllocationPage.IsSelectionCorrect(Contract.Municipal);
        }
    }
}
