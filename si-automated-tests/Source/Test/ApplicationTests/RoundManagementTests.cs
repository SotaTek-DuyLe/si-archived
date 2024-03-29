﻿using NUnit.Framework;
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
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab(firstRound);
            TaskModel expectedModel = new TaskModel();
            expectedModel.Description = model.Description;
            expectedModel.StartDate = date;
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .FilterDescription(expectedModel.Description)
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
            string firstRound = PageFactoryManager.Get<MasterRoundManagementPage>()
                .GetFirstRoundName();
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .DragAndDropFirstRoundToGrid()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab(firstRound);
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .FilterDescription(expectedModel2.Description)
                .VerifyTaskModelDescriptionAndEndDate(expectedModel2);
        }

        [Category("RoundInstance")]
        [Category("Huong")]
        [Test]
        public void TC_199_1_tasks_and_round_legs_can_be_reallocated()
        {
            //Verify that tasks and round legs can be reallocated
            string contract = Contract.Commercial;
            string service = "Collections";
            string subService = "Commercial Collections";
            string date = "";
            MasterRoundManagementPage masterRoundManagementPage = PageFactoryManager.Get<MasterRoundManagementPage>();
            masterRoundManagementPage
                .IsOnPage()
                .InputSearchDetails(contract, service, subService, date)
                .WaitForLoadingIconToDisappear();
            masterRoundManagementPage.DragRoundToGrid()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(1000);

            //Select couple of rows and click Subcontract (make a note of the descriptions) -> Select a subcontract reason and click Confirm 
            masterRoundManagementPage.SelectFirstAndSecondTask();
            masterRoundManagementPage.ClickOnElement(masterRoundManagementPage.Subcontract);
            masterRoundManagementPage.SelectTextFromDropDown(masterRoundManagementPage.SubcontractSelect, "No Service")
                .ClickOnElement(masterRoundManagementPage.SubcontractConfirmButton);
            masterRoundManagementPage.WaitForLoadingIconToDisappear();
            masterRoundManagementPage.ClickRefreshBtn();
            masterRoundManagementPage.WaitForLoadingIconToDisappear();
            //Scroll right
            masterRoundManagementPage.ScrollToSubcontractHeader();
            masterRoundManagementPage.WaitForLoadingIconToDisappear();
            masterRoundManagementPage.VerifyFirstAndSecondConfirmedTask("No Service");
            //Navigate to task confirmation screen->Filter the same contract, service and round

            TaskConfirmationPage taskConfirmationPage = PageFactoryManager.Get<TaskConfirmationPage>();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Confirmation")
                .SwitchToFrame(taskConfirmationPage.TaskConfirmationIframe);
            taskConfirmationPage.SelectTextFromDropDown(taskConfirmationPage.ContractSelect, contract);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ServiceInput);
            taskConfirmationPage.SleepTimeInMiliseconds(1000);
            taskConfirmationPage.ExpandRoundNode("Commercial")
                .ExpandRoundNode(service)
                .ExpandRoundNode(subService)
                .ExpandRoundNode("REF1-AM")
                .SelectRoundNode("Monday");
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ScheduleDateInput);
            taskConfirmationPage.SleepTimeInMiliseconds(1000);
            taskConfirmationPage.InputNextMonday();
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ContractSelect);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ButtonGo);
            taskConfirmationPage.ClickOnElementIfItVisible(taskConfirmationPage.ButtonConfirm);
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ExpandRoundsGo);
            taskConfirmationPage.SleepTimeInMiliseconds(2000);
            //Select the 2 service tasks you updated earlier and click reallocate
            taskConfirmationPage.ClickServiceTask(0)
                .ClickServiceTask(1)
                .ClickOnElement(taskConfirmationPage.BulkReallocateButton);
            taskConfirmationPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.MaximumWindow();
            taskConfirmationPage.VerifyReallocatedTask("No Service");

            //Select the 2 service tasks in the grid -> Update the from filter -> Go 
            List<string> descriptions = taskConfirmationPage.SelectServiceTaskAllocation();
            taskConfirmationPage.InputNextMondayOnReallocatedGrid();
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ContractSelect);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ButtonGo);
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            //Drag and drop the service tasks to a different round (confirm the pop up modal if allocating to a different day) 
            taskConfirmationPage.DragDropTaskAllocationToRoundGrid("REC1-AM", "Monday")
                .VerifyContainToastMessage("Task(s) Allocated");
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.VerifyTaskAllocated("REC1-AM", "Monday");
            //Drag and drop the round you allocated the tasks to to the grid
            taskConfirmationPage.DragRoundInstanceToReallocattedGrid("REC1-AM", "Monday");
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            //Scroll down and right to find your tasks
            taskConfirmationPage.VerifyTaskSubcontract(descriptions, "No Service");
        }

        [Category("RoundInstance")]
        [Category("Huong")]
        [Test]
        public void TC_199_2_tasks_and_round_legs_can_be_reallocated()
        {
            string contract = Contract.Municipal;
            string service = "Recycling";
            string subService = "Communal Recycling";
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Confirmation")
                .SwitchNewIFrame();
            TaskConfirmationPage taskConfirmationPage = PageFactoryManager.Get<TaskConfirmationPage>();
            taskConfirmationPage.SelectTextFromDropDown(taskConfirmationPage.ContractSelect, contract);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ServiceInput);
            taskConfirmationPage.SleepTimeInMiliseconds(1000);
            taskConfirmationPage.ExpandRoundNode("Municipal")
                .ExpandRoundNode(service)
                .SelectRoundNode(subService);
            DateTime start = DateTime.Now;
            DayOfWeek day = DayOfWeek.Monday;
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            //input next monday
            string filterDate = start.AddDays(daysToAdd).ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            taskConfirmationPage
                .SendDateInScheduledDate(filterDate)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            taskConfirmationPage.ClickOnElementIfItVisible(taskConfirmationPage.ButtonConfirm);
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            string roundGroupName = taskConfirmationPage
                .GetRoundName();
            taskConfirmationPage
                .SelectRoundLegsOnSecondRoundGroup()
                .ClickOnElement(taskConfirmationPage.BulkReallocateButton);
            taskConfirmationPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            taskConfirmationPage.InputNextMondayOnReallocatedGrid();
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ContractSelect);
            taskConfirmationPage.ClickOnElement(taskConfirmationPage.ButtonGo);
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.SelectAllRoundLeg()
                .DragDropRoundLegToRoundInstance("WCREC2", roundGroupName);
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.ConfirmationNeeded();
            taskConfirmationPage.SelectReasonNeeded();
            taskConfirmationPage.VerifyToastMessage("Allocated 2 round leg(s)");
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.VerifyRoundLegNoLongerDisplay();
            taskConfirmationPage.CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            taskConfirmationPage.ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            taskConfirmationPage.ClickOnElementIfItVisible(taskConfirmationPage.ButtonConfirm);
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.ExpandThirdRoundGroup();
            taskConfirmationPage.ExpandRoundLegAndSelectTask()
                .ClickOnElement(taskConfirmationPage.BulkReallocateButton);
            taskConfirmationPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            taskConfirmationPage.SelectAllVirtualTask()
                .DragDropRoundLegToRoundInstance("WCREC2", roundGroupName);
            taskConfirmationPage.SelectTextFromDropDown(taskConfirmationPage.SelectReason, "Bad Weather");
            taskConfirmationPage.ClickOnElementIfItVisible(taskConfirmationPage.ButtonConfirm);
            taskConfirmationPage.VerifyToastMessage("2 Task(s) Allocated");
            taskConfirmationPage.WaitForLoadingIconToDisappear();
            taskConfirmationPage.VerifyRoundLegNoLongerDisplay();
        }
    }
}
