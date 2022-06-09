using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Services.ServiceTask;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    public class TaskTests : BaseTest
    {
        private CommonFinder finder;
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
                .Login(AutoUser58.UserName, AutoUser58.Password)
                .IsOnHomePage(AutoUser58);
        }

        [Category("Tasks/tasklines")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task")]
        public void TC_125_Detail_tasks()
        {
            string taskIDWithSourceServiceTask = "3957";
            //Run query to get task's information
            List<TaskDBModel> taskInfoById = finder.GetTask(int.Parse(taskIDWithSourceServiceTask));

            //Login ECHO and check the detail of the task
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId(taskIDWithSourceServiceTask)
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                .VerifyCurrentUrlOfDetailTaskPage(taskIDWithSourceServiceTask)
                //Verify response returned from DB

                //Line 9: Click on the hyperlink next to Task
                .ClickHyperlinkNextToTask()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            ServiceTaskDetailPage serviceTaskDetailPage = PageFactoryManager.Get<ServiceTaskDetailPage>();
            serviceTaskDetailPage
                .WaitForServiceTaskDetailPageDisplayed()
                .VerifyCurrentUrlServiceTaskDetail(taskInfoById[0].servicetaskID.ToString());

        }
    }
}
