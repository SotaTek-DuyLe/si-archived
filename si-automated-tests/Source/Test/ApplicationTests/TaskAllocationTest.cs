using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                .ClickMainOption("Applications")
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();

            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "18/05/2022";
            string to = "20/05/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, "North Star");
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode("North Star")
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
                taskAllocationPage.VerifyOutStandingData(outstandingTasks);
            }
        }
    }
}
