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
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Master Round Management")
                .SwitchNewIFrame();
        }
        [Category("BusinessUnit")]
        [Category("Dee")]
        [Test]
        public void TC_108_master_round_management()
        {
            string contract = Contract.Municipal;
            string service = "Waste";
            string subService = "Domestic Refuse";
            string date = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            TaskModel model = PageFactoryManager.Get<MasterRoundManagementPage>()
                .IsOnPage()
                .InputSearchDetails(contract, service, subService, date)
                .ClickFirstUnallocatedTask()
                .GetFirstTaskDetails();

            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropFirstUnallocatedTaskToFirstRound()
                .VerifyToastMessage("1 Task allocated")
                .WaitUntilToastMessageInvisible("1 Task allocated");
            string firstRound = PageFactoryManager.Get<MasterRoundManagementPage>()
                .GetFirstRoundName();
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropFirstRoundToGrid()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(5000)
                .SwitchToTab(firstRound);
            TaskModel expectedModel = new TaskModel();
            expectedModel.Description = model.Description;
            expectedModel.StartDate = date;
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .VerifyTaskModelDescriptionAndStartDate(expectedModel);
        }
        [Category("BusinessUnit")]
        [Category("Dee")]
        [Test]
        public void TC_124_master_round_management()
        {
            string contract = Contract.Commercial;
            string service = "Collections";
            string initDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            string date = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .IsOnPage()
                .InputSearchDetails(contract, service, date);

            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropFirstRoundToGrid();
            TaskModel model = PageFactoryManager.Get<MasterRoundManagementPage>()
                .GetFirstTaskDetailsInActiveRoundTab();

            PageFactoryManager.Get<MasterRoundManagementPage>()
                .ClickFirstAllocatedTask();
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropSelectedAllocatedTaskToSecondRound()
                .VerifyToastMessage("1 Task allocated")
                .WaitUntilToastMessageInvisible("1 Task allocated");
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropSecondRoundToGrid()
                .SwitchToTab("REC1-AM Monday");

            TaskModel expectedModel = new TaskModel();
            expectedModel.Description = model.Description;
            expectedModel.StartDate = date;
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .VerifyTaskModelDescriptionAndStartDate(expectedModel);

            //Verify on init date
            TaskModel expectedModel2 = new TaskModel();
            expectedModel2.Description = model.Description;
            expectedModel2.EndDate = date;

            PageFactoryManager.Get<MasterRoundManagementPage>()
                .IsOnPage()
                .InputSearchDetails(initDate);
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropFirstRoundToGrid()
                .VerifyTaskModelDescriptionAndEndDate(expectedModel2);
        }
    }
}
