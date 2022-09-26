using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementLine;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Task;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using TaskLineDetailPage = si_automated_tests.Source.Main.Pages.Tasks.TaskLineDetailPage;

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
            
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
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
                .WaitForAgreementPageLoadedSuccessfully("COMMERCIAL COLLECTIONS", "TWISTED FISH LIMITED");
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskId1 = PageFactoryManager.Get<TaskTab>()
                .getFirstTaskId();
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
                .ClickMainOption(MainOption.Parties)
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
            int taskId2 = PageFactoryManager.Get<TaskTab>()
                .getSecondTaskId();
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
            
            String note = "test bulk update";
            String todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
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
                .WaitForAgreementPageLoadedSuccessfully("COMMERCIAL COLLECTIONS", "TWISTED FISH LIMITED");
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Bulk Update Task
            int taskId1 = PageFactoryManager.Get<TaskTab>()
                .getFirstTaskId();
            int taskId2 = PageFactoryManager.Get<TaskTab>()
                .getSecondTaskId();
            int[] taskIdList = { taskId1, taskId2 };
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
                .VerifyToastMessage("Success")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Verify bulked update for tasks
            PageFactoryManager.Get<TaskTab>()
                .VerifyRetiredTaskWithIds(taskIdList)
                .VerifyTaskStateWithIds(taskIdList, "Completed");
            for(int i = 0; i < taskIdList.Length; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATaskById(taskIdList[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab();
                PageFactoryManager.Get<TaskDetailTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyEndDate(todayDate);
                String dueDate = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(dueDate)
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
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
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
                    .VerifyEndDate(todayDate);
                String endDate1 = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(endDate1)
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

            String note = "test bulk update";
            String todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
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
            int taskId1 = PageFactoryManager.Get<TaskTab>()
                .getThirdTaskId();
            int taskId2 = PageFactoryManager.Get<TaskTab>()
                .getFourthTaskId();
            int[] taskIdList = { taskId1, taskId2 };
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
                .VerifyToastMessage("Success")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            //Verify bulked update for tasks
            PageFactoryManager.Get<AgreementLineTaskTab>()
                .VerifyRetiredTaskWithIds(taskIdList);
            PageFactoryManager.Get<AgreementLineTaskTab>()
                .VerifyTaskStateWithIdsAgreementLine(taskIdList, "Completed");
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
                    .VerifyEndDate(todayDate);
                String dueDate = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(dueDate)
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
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
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
                    .VerifyEndDate(todayDate);
                String dueDate1 = PageFactoryManager.Get<TaskDetailTab>().GetDueDate();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyCompletionDate(dueDate1)
                    .VerifyTaskState("Completed")
                    .VerifyNote(note)
                    .CloseCurrentWindow()
                    .SwitchToFirstWindow()
                    .SwitchNewIFrame();
            }
        }

        [Category("AgreementTask")]
        [Test(Description = "Verify whether task form is loading when childtask line is there")]
        public void TC_173_Tasklines_with_details_loaded_in_Task_Form()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonTaskPage>()
                    .FilterTaskId(12243)
                    .OpenTaskWithId(12243)
                    .SwitchToLastWindow();
            var agreementTaskDetailPage = PageFactoryManager.Get<AgreementTaskDetailsPage>();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.ClickToTaskLinesTab();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.VerifyToastMessagesIsUnDisplayed();
            agreementTaskDetailPage.VerifyElementVisibility(agreementTaskDetailPage.AddNewAgreementTaskDetailButton, true);
            agreementTaskDetailPage.VerifyHeaderColumn();
        }

        [Test(Description = "Verify that 'Requested Delivery Date' displays correctly on New Agreement with Start Date <current Date")]
        public void TC_180_1_Agreements_displaying_Requested_Delivery_Date()
        {
            int agreementId = 63;

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption("Agreements")
                .SwitchNewIFrame();

            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .IsOnPartyAgreementPage()
                .ClickEditAgreementBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            var assetAndProductTab = PageFactoryManager.Get<AssetAndProducTab>();
            assetAndProductTab
                .IsOnAssetTab()
                .ClickOnEditAsset()
                .VerifyElementVisibility(assetAndProductTab.deliveryDate, false);
            assetAndProductTab.EditAssetQuantity(3)
                .ClickOnElement(assetAndProductTab.numberOfAssetOnSite);
            assetAndProductTab.VerifyElementVisibility(assetAndProductTab.deliveryDate, true);
            assetAndProductTab.VerifyDeliveryDate("26/04/2022")
                .EditAssertClickDoneBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .ClickOnNotSetLink()
                .ClickDoneRequirementBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .IsOnPriceTab()
                .RemoveAllRedundantPrices()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Fix wating time for saved agreement
            var partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            partyAgreementPage.SleepTimeInMiliseconds(10000);
            partyAgreementPage.ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Active")
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            partyAgreementPage
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            var taskTab = PageFactoryManager.Get<TaskTab>();
            taskTab.SendKeys(taskTab.TaskTypeSearch, "Deliver Commercial Bin");
            taskTab.ClickOnElement(taskTab.ApplyBtn);
            taskTab.VerifyTaskDataType("Deliver Commercial Bin");
            taskTab.VerifyTaskDueDate("26/04/2022 00:00");

            // Đang có bug ở đây, due date vẫn là 26/04/2022
            partyAgreementPage.ClickOnDetailsTab()
                .IsOnPartyAgreementPage()
                .ClickEditAgreementBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            assetAndProductTab
                .IsOnAssetTab()
                .ClickOnEditAsset()
                .EditAssetQuantity(4)
                .ClickAssetType();
            assetAndProductTab.VerifyElementVisibility(assetAndProductTab.deliveryDate, true);
            assetAndProductTab.VerifyDeliveryDate("26/04/2022")
                .EditAssertClickDoneBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .ClickOnNotSetLink()
                .ClickDoneRequirementBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .IsOnPriceTab()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            partyAgreementPage.SleepTimeInMiliseconds(10000);
            partyAgreementPage
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            taskTab.SendKeys(taskTab.TaskTypeSearch, "Deliver Commercial Bin");
            taskTab.ClickOnElement(taskTab.ApplyBtn);
            taskTab.VerifyTaskDataType("Deliver Commercial Bin");
            taskTab.VerifyTaskDueDate("26/04/2022 00:00");
        }

        [Category("AgreementTask")]
        [Test(Description = "Verify that 'Requested Delivery Date' displays when user creates new Agreement Line and Asset Qty = 1 on NEW Agreement")]
        public void TC_180_2_Agreements_displaying_Requested_Delivery_Date()
        {
            PageFactoryManager.Get<LoginPage>()
            .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(73)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
               .IsOnPartyAgreementPage()
               .SelectAgreementType("Commercial Collections")
               .ClickSaveBtn()
               .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<PartyAgreementPage>().ClickAddService();
            PageFactoryManager.Get<AddServicePage>()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .SelectServiceSite("Greggs - 8 KING STREET, TWICKENHAM, TW1 3SN")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .SelectService("Commercial")
                .ClickNext();
            var assetAndProductTab = PageFactoryManager.Get<AssetAndProducTab>();
            assetAndProductTab.WaitForLoadingIconToDisappear();
            assetAndProductTab
                .IsOnAssetTab()
                .ClickAddAsset()
                .VerifyInputValue(assetAndProductTab.assetQuantity, "1");
            assetAndProductTab.VerifyDeliveryDate(DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"))
                .ChooseAssetType("1100L")
                .ChooseTenure("Rental")
                .ChooseProduct("General Recycling")
                .ChooseEwcCode("150106")
                .EditAssertClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .ClickDoneRequirementBtn()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab();
            PageFactoryManager.Get<PriceTab>()
               .RemoveAllRedundantPrices()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(AgreementConstants.SUCCESSFULLY_SAVED_AGREEMENT)
               .WaitForLoadingIconToDisappear();

            //Fix wating time for saved agreement
            var partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            partyAgreementPage.SleepTimeInMiliseconds(10000);
            partyAgreementPage.ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Active");
            partyAgreementPage
                .ClickEditAgreementBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            assetAndProductTab
                .IsOnAssetTab()
                .ClickOnEditAsset();
            //Đang có bug ở đây, vẫn hiện Requested Delivery Date
            assetAndProductTab.VerifyElementVisibility(assetAndProductTab.deliveryDate, true);
            assetAndProductTab.EditAssetQuantity(2)
                .ClickAssetType();
            assetAndProductTab.VerifyElementVisibility(assetAndProductTab.deliveryDate, true);
            assetAndProductTab.VerifyDeliveryDate(DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"))
                .EditAssertClickDoneBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
        }

        [Category("AgreementTask")]
        [Test(Description = "Verify whether productcode field is not resetting value to 0 when user update taskline from task form")]
        public void TC_174_1_Taskline_Productcode()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonTaskPage>()
                    .FilterTaskId(15331)
                    .OpenTaskWithId(15331)
                    .SwitchToLastWindow();
            var agreementTaskDetailPage = PageFactoryManager.Get<AgreementTaskDetailsPage>();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.ClickToTaskLinesTab();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.DoubleClickTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            var serviceTaskLinePage = PageFactoryManager.Get<TaskLineDetailPage>();
            serviceTaskLinePage.WaitForLoadingIconToDisappear();
            serviceTaskLinePage.SelectTextFromDropDown(serviceTaskLinePage.ProductSelect, "General Recycling")
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Task Line")
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.VerifyTaskLineProduct(0, "General Recycling");

            //2
            agreementTaskDetailPage.DoubleClickTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceTaskLinePage.WaitForLoadingIconToDisappear();
            serviceTaskLinePage.SelectTextFromDropDown(serviceTaskLinePage.StateSelect, "Not Completed")
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Task Line")
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.VerifyTaskLineState(0, "Not Completed");
        }

        [Category("AgreementTask")]
        [Test(Description = "Verify whether any other field in taskline form clearing when user update taskline from task form")]
        public void TC_174_2_Taskline_Productcode()
        {
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonTaskPage>()
                    .FilterTaskId(15331)
                    .OpenTaskWithId(15331)
                    .SwitchToLastWindow();
            var agreementTaskDetailPage = PageFactoryManager.Get<AgreementTaskDetailsPage>();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.ClickToTaskLinesTab();
            agreementTaskDetailPage.WaitForLoadingIconToDisappear();
            agreementTaskDetailPage.DoubleClickTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            var serviceTaskLinePage = PageFactoryManager.Get<TaskLineDetailPage>();
            serviceTaskLinePage.WaitForLoadingIconToDisappear();
            serviceTaskLinePage.SendKeys(serviceTaskLinePage.MinAssetQty, "5");
            serviceTaskLinePage.SendKeys(serviceTaskLinePage.MaxAssetQty, "10");
            serviceTaskLinePage.SendKeys(serviceTaskLinePage.MinProductQty, "6");
            serviceTaskLinePage.SendKeys(serviceTaskLinePage.MaxProductQty, "8");
            serviceTaskLinePage.ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Task Line")
                .WaitForLoadingIconToDisappear();
            serviceTaskLinePage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            serviceTaskLinePage.VerifyInputValue(serviceTaskLinePage.MinAssetQty, "5")
                .VerifyInputValue(serviceTaskLinePage.MaxAssetQty, "10")
                .VerifyInputValue(serviceTaskLinePage.MinProductQty, "6")
                .VerifyInputValue(serviceTaskLinePage.MaxProductQty, "8");
        }

        [Category("AgreementTask")]
        [Test(Description = "Verify that a product code has been added to agreement line wizard")]
        public void TC_193_Add_product_code_description_to_the_AgreementLineAssetProduct()
        {
            PageFactoryManager.Get<LoginPage>()
            .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser12.UserName, AutoUser12.Password)
                .IsOnHomePage(AutoUser12);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(1136)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
               .IsOnPartyAgreementPage()
               .SelectAgreementType("Commercial Collections")
               .ClickSaveBtn()
               .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<PartyAgreementPage>().ClickAddService();
            PageFactoryManager.Get<AddServicePage>()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .SelectService("Commercial")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteAndServiceTab>().ClickNext();
            var assetAndProductTab = PageFactoryManager.Get<AssetAndProducTab>();
            assetAndProductTab.WaitForLoadingIconToDisappear();
            assetAndProductTab
                .IsOnAssetTab()
                .ClickAddAsset()
                .VerifyInputValue(assetAndProductTab.assetQuantity, "1");
            assetAndProductTab.VerifyDeliveryDate(DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"))
                .ChooseAssetType("1100L")
                .ChooseTenure("Rental")
                .ChooseProduct("General Recycling")
                .ChooseEwcCode("150106")
                .EditAssertClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .ClickDoneRequirementBtn()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .InputPrices(new List<(string title, string value)>() { ("Commercial Customers: Bin Removal", "1"), ("Commercial Customers: Bin Delivery", "1") })
               .ClickPrice("Commercial Customers: Bin Rental")
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(AgreementConstants.SUCCESSFULLY_SAVED_AGREEMENT)
               .WaitForLoadingIconToDisappear();

            var partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            partyAgreementPage.SleepTimeInMiliseconds(10000);
            partyAgreementPage.ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Active");
            partyAgreementPage
                .ClickEditAgreementBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            assetAndProductTab
                .IsOnAssetTab()
                .ClickOnEditAsset()
                .VerifyInputAssetAndProduct(1, "1100L", "Rental", "General Recycling", "150106");
        }
    }
}
