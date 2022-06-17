using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Text;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ApplicationTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class RoundManagementTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO BUSINESS UNITS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser28.UserName, AutoUser28.Password)
                .IsOnHomePage(AutoUser28);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Applications")
                .OpenOption("Master Round Management")
                .SwitchNewIFrame();
        }
        [Category("BusinessUnit")]
        [Category("Dee")]
        [Test]
        public void TC_108_master_round_management()
        {
            string contract = "North Star Commercial";
            string service = "Collections";
            string date = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT,1);
            TaskModel model = PageFactoryManager.Get<MasterRoundManagementPage>()
                .IsOnPage()
                .InputSearchDetails(contract, service, date)
                .ClickFirstUnallocatedTask()
                .GetFirstTaskDetails();
            
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropFirstUnallocatedTaskToFirstRound()
                .VerifyToastMessage("1 Task allocated")
                .WaitUntilToastMessageInvisible("1 Task allocated");
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropFirstRoundToGrid()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(5000)
                .SwitchToTab("REC1-AM Friday");
            TaskModel expectedModel = new TaskModel();
            expectedModel.Description = model.Description;
            expectedModel.StartDate = date;
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .VerifyTaskModel(expectedModel);
        }
    }
}
