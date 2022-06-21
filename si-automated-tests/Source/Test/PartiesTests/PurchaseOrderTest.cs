using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccount;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Task;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.PartyTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class PurchaseOrderTest : BaseTest
    {
        [Category("EditAgreement")]
        [Test]
        public void TC_028A()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string tommorowDueDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);

            int agreementId = 41;
            string partyName = "Greggs";
            string agreementType = "COMMERCIAL COLLECTIONS";

            string assetType = AgreementConstants.ASSET_TYPE_1100L;
            int assetQty = 4;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string tenure = AgreementConstants.TENURE_RENTAL;
            int productQty = 1000;

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser11.UserName, AutoUser11.Password)
                .IsOnHomePage(AutoUser11);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName);
            //Add service 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickAddService();
            PageFactoryManager.Get<AddServicePage>()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .SelectServiceSite("Greggs - 8 KING STREET, TWICKENHAM, TW1 3SN")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .SelectService("Commercial")
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .SelectAssetType(assetType)
                .InputAssetQuantity(assetQty)
                .ChooseTenure(tenure)
                .TickAssetOnSite()
                .InputAssetOnSiteNum(1)
                .ChooseProduct(product)
                .ChooseEwcCode("150106")
                .InputProductQuantity(productQty)
                .ClickDoneBtn()
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

            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(20000); //waiting for new task are genarated
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            List<IWebElement> allTasks = PageFactoryManager.Get<TaskTab>()
              .VerifyNewTaskAppearWithNum(3, "Unallocated", "Deliver Commercial Bin", tommorowDueDate, "");

            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .InputActuaAssetQuantity(1)
                    .ClickOnAcualAssetQuantityText()
                    .SelectCompletedState()
                    .ClickOnAcualAssetQuantityText()
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab()
                    .ClickStateDetais()
                    .ChooseTaskState("Completed")
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success");
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .CloseCurrentWindow()
                    .SwitchToChildWindow(2);
            }
        }
        [Category("EditAgreement")]
        [Test]
        public void TC_028B()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string tommorowDueDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);

            string partyName = "Greggs";

            string assetType = AgreementConstants.ASSET_TYPE_1100L;
            int assetQty = 4;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string defautEndDate = AgreementConstants.DEFAULT_END_DATE;
            string unit = AgreementConstants.KILOGRAMS;

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser11.UserName, AutoUser11.Password)
                .IsOnHomePage(AutoUser11);
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption("Services")
               .ExpandOption("Regions")
               .ExpandOption("London")
               .ExpandOption("North Star Commercial")
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            //Verify at Active Service Task
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate(partyName, tommorowDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo(assetType, assetQty.ToString(), product, unit, tommorowDate, defautEndDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .verifyScheduleEndDate(defautEndDate);
        }

        [Category("PurchaseOrder")]
        [Test]
        public void TC_074_Add_Purchase_Order()
        {
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string datePlus = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 30);
            int partyId = 73;
            string partyName = "Greggs";
            string PO_Number = "test PON 1";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser11.UserName, AutoUser11.Password)
                .IsOnHomePage(AutoUser11);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .GoToATab("Purchase Orders");
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .WaitForLoadingIconToDisappear();
            //Add purchase order
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .IsOnPartyPurchaseOrderPage()
                .ClickAddNewItem()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AddPurchaseOrderPage>()
                .IsOnAddPurchaseOrderPage()
                .ClickSaveBtn()
                .VerifyToastMessage("Number is required");
            PageFactoryManager.Get<AddPurchaseOrderPage>()
                .InputPONumber(PO_Number)
                .InputFirstDay(todayDate)
                .InputLastDay(datePlus)
                .SelectAgreement("41 (Active from 21/03/2022 until 01/01/2050)")
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Purchase Order")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTab>()
                .IsOnAgreementTab()
                .OpenAgreementWithId(41)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields();
            //Verify Create Adhoc btn 
            IList<IWebElement> createAdhocBtns = PageFactoryManager.Get<DetailTab>()
                .GetCreateAdhocBtnList();
            foreach (var btn in createAdhocBtns)
            {
                PageFactoryManager.Get<DetailTab>()
                    .ClickAdHocBtn(btn)
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab();
                PageFactoryManager.Get<TaskDetailTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskDetailTab>()
                    .IsOnTaskDetailTab()
                    .VerifyPurchaseOrderValue(PO_Number)
                    .CloseCurrentWindow()
                    .SwitchToChildWindow(3);
            }
        }
        [Category("PurchaseOrder")]
        [Test]
        public void TC_075_Remove_Purchase_Order()
        {
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string datePlus = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 30);
            int partyId = 73;
            string partyName = "Greggs";
            string PO_Number = "test PON 1";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser11.UserName, AutoUser11.Password)
                .IsOnHomePage(AutoUser11);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .GoToATab("Purchase Orders");
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .WaitForLoadingIconToDisappear();
            //Remove purchase order 
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .IsOnPartyPurchaseOrderPage()
                .SelectPurchaseOrder(PO_Number)
                .ClickDeletePurchaseOrder()
                .SwitchToLastWindow();
            PageFactoryManager.Get<RemovePurchaseOrderPage>()
                .IsOnRemovePurchaseOrderPage()
                .ClickNoBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .VerifyPurchaseOrderAppear(PO_Number)
                .ClickDeletePurchaseOrder()
                .SwitchToLastWindow();
            PageFactoryManager.Get<RemovePurchaseOrderPage>()
                .IsOnRemovePurchaseOrderPage()
                .ClickYesBtn()
                //.VerifyToastMessage("Success")
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .VerifyPurchaseOrderDisappear(PO_Number);
            //Go to agreement and verify 
            PageFactoryManager.Get<DetailPartyPage>()
               .OpenAgreementTab()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTab>()
                .IsOnAgreementTab()
                .OpenAgreementWithId(41)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                 .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .ClickRefreshBtn();
            List<IWebElement> allNewTask =
            PageFactoryManager.Get<TaskTab>()
                .GetTasksAppear("Unallocated", todayDate, todayDate);
            for(int i = 0; i < allNewTask.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allNewTask[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<TaskDetailTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskDetailTab>()
                    .VerifyPurchaseOrderValueNotPresent(PO_Number)
                    .CloseCurrentWindow()
                    .SwitchToChildWindow(3);
            }
           
        }
        [Category("PurchaseOrder")]
        [Test]
        public void TC_076_PO_Number_Require_Is_True()
        {
            string todayUTCDate = CommonUtil.GetUtcTimeNow("dd/MM/yyyy");
            int partyId = 73;
            string partyName = "Greggs";
            string refValue = "test12 of PO 00011";
            string refValue1 = "upate012 test of PO";
            string PO_Number = "PO00111 test 12345";
            string refUpdateValue = "Task reference: " + refValue;
            string refUpdateValue1 = "Task reference: " + refValue1;
            string PONumberCreatedValue = "PurchaseOrder = " + PO_Number;

            int taskId = 703;

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser11.UserName, AutoUser11.Password)
                .IsOnHomePage(AutoUser11);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            //Go to Account Tab and Set 'PO Number Required'  = true
            PageFactoryManager.Get<DetailPartyPage>()
                .GoToATab("Account");
            PageFactoryManager.Get<PartyAccountPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .CheckOnAccountType("PO Number Required")
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved party.")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAccountPage>()
                .VerifyAccountTypeChecked("PO Number Required");
            //Verify "Purchase Order # is required"
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickTabDropDown()
                .ClickTasksTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .GoToATaskById(taskId)
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitingForTaskDetailsPageLoadedSuccessfully()
                .ClickToDetailsTab();
            PageFactoryManager.Get<TaskDetailTab>()
                .InputReferenceValue(refValue)
                .ClickSaveBtn()
                .VerifyToastMessage("Purchase Order # is required");
            //Input purchase order -> successfully saved the task
            PageFactoryManager.Get<TaskDetailTab>()
                .InputPurchaseOrderValue(PO_Number);

            string savedUTCTime = PageFactoryManager.Get<TaskDetailTab>()
                .ClickSaveBtnGetUTCTime();
            string updatedUTCTime = CommonUtil.GetTimeMinusHour(savedUTCTime, "dd/MM/yyyy hh:mm", 1);

            PageFactoryManager.Get<BasePage>()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyPurchaseOrderValueAtInput(PO_Number)
                .VerifyPurchaseOrderValue(PO_Number);
            //Refresh the task and confirm purchase order again 
            PageFactoryManager.Get<TaskDetailTab>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyPurchaseOrderValueAtInput(PO_Number)
                .VerifyPurchaseOrderValue(PO_Number);
            //Go to History task and confirm task Update & Created
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<HistoryTab>()
                .VerifyUpdateTaskTimeAndValue(refUpdateValue, updatedUTCTime)
                .VerifyCreatedTaskTimeAndValue(PONumberCreatedValue, savedUTCTime)
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            //Back to Party and Verify Purchase order 
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .GoToATab("Purchase Orders");
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .WaitForLoadingIconToDisappear();
            //Confirm purchase order appear 
            PageFactoryManager.Get<PartyPurchaseOrderPage>()
                .IsOnPartyPurchaseOrderPage()
                .VerifyPurchaseOrder(PO_Number, todayUTCDate, "31/12/2049")
                .OpenPurchaseOrder(PO_Number)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PurchaseOrderDetailsPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PurchaseOrderDetailsPage>()
                .WaitingForPurchaseOrderPageLoadedSuccessfully(PO_Number)
                .VerifyDetailsPageWithDateEnabled(todayUTCDate, "31/12/2049", taskId.ToString())
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            //Back to party and Set 'PO Number Required'  = false
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            //Go to Account Tab and Set 'PO Number Required'  = false
            PageFactoryManager.Get<DetailPartyPage>()
                .GoToATab("Account");
            PageFactoryManager.Get<PartyAccountPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .UncheckOnAccountType("PO Number Required")
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved party.")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAccountPage>()
                .VerifyAccountTypeUnchecked("PO Number Required");
            //Verify "Purchase Order # is not required"
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickTabDropDown()
                .ClickTasksTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .GoToATaskById(taskId)
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitingForTaskDetailsPageLoadedSuccessfully()
                .ClickToDetailsTab();
            PageFactoryManager.Get<TaskDetailTab>()
                .InputReferenceValue(refValue1);
            string savedUTCTime1 = PageFactoryManager.Get<TaskDetailTab>()
                .ClickSaveBtnGetUTCTime();
            string updatedUTCTime1 = CommonUtil.GetTimeMinusHour(savedUTCTime1, "dd/MM/yyyy hh:mm", 1);
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyToastMessage("Success");
            //Go to History task and confirm task Update for Ref
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<HistoryTab>()
                .VerifyUpdateTaskTimeAndValue(refUpdateValue1, updatedUTCTime1);
        }
    } 
}
