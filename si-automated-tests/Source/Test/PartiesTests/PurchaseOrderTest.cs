using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Core;
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
        [Category("Agreement")]
        [Test(Description = "Verify that Agreement with multiple agreement lines for the same Site/Service/Asset Types/ Products creates correct Mobilization and when retired then it creates correct Demobilization tasks"), Order(1)]
        public void TC_149_A_Verify_that_Agreement_with_multiple_agreement_lines()
        {
            void AddNewAgreementLine(string partyName)
            {
                PageFactoryManager.Get<AgreementTab>()
                    .IsOnAgreementTab()
                    .ClickAddNewItem()
                    .SwitchToLastWindow();
                PageFactoryManager.Get<PartyAgreementPage>()
                    .WaitForLoadingIconToDisappear();
                var partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
                string selectedValue = "Use Customer";
                string agreementType = "COMMERCIAL COLLECTIONS";
                partyAgreementPage.VerifyElementIsMandatory(partyAgreementPage.agreementTypeInput);
                partyAgreementPage.VerifyInputValue(partyAgreementPage.startDateInput, DateTime.Now.ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT).Replace("-", "/"));
                partyAgreementPage.VerifyInputValue(partyAgreementPage.endDateInput, "01/01/2050");
                partyAgreementPage.VerifySelectedValue(partyAgreementPage.primaryContract, selectedValue);
                partyAgreementPage.VerifySelectedValue(partyAgreementPage.invoiceContact, selectedValue);
                partyAgreementPage.VerifySelectedValue(partyAgreementPage.correspondenceAddressInput, selectedValue);
                partyAgreementPage.VerifySelectedValue(partyAgreementPage.invoiceAddressInput, selectedValue);
                partyAgreementPage.VerifySelectedValue(partyAgreementPage.invoiceScheduleInput, selectedValue);
                partyAgreementPage
                    .SelectAgreementType("Commercial Collections")
                    .ClickSaveBtn()
                    .VerifyToastMessage("Successfully saved agreement")
                    .WaitForLoadingIconToDisappear();
                partyAgreementPage
                    .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName)
                    .VerifyAgreementStatus("New")
                    .VerifyElementVisibility(partyAgreementPage.addServiceBtn, true)
                    .VerifyElementVisibility(partyAgreementPage.approveBtn, true)
                    .VerifyElementVisibility(partyAgreementPage.cancelBtn, true);
                partyAgreementPage
                    .ClickAddService()
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
                string assetType = AgreementConstants.ASSET_TYPE_1100L;
                int assetQty = 3;
                string product = AgreementConstants.GENERAL_RECYCLING;
                string tenure = AgreementConstants.TENURE_RENTAL;
                int productQty = 1000;
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
                    .SelectKiloGramAsUnit()
                    .ClickDoneBtn()
                    .ClickBack();
                PageFactoryManager.Get<SiteAndServiceTab>()
                    .IsOnSiteServiceTab()
                    .ClickNext();
                PageFactoryManager.Get<AssetAndProducTab>()
                    .ClickNext();
                PageFactoryManager.Get<ScheduleServiceTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<ScheduleServiceTab>()
                   .IsOnScheduleTab()
                   .ClickAddService()
                   .ClickDoneScheduleBtn()
                   .ClickOnNotSetLink()
                   .ClickOnWeeklyBtn()
                   .UntickAnyDayOption()
                   .SelectDayOfWeek("Mon")
                   .ClickDoneRequirementBtn()
                   .VerifyScheduleSummary("Once Every week on any Monday")
                   .ClickNext();
                var priceTab = PageFactoryManager.Get<PriceTab>();
                priceTab.WaitForLoadingIconToDisappear();
                priceTab.VerifyElementEnable(priceTab.nextBtn, false);
                priceTab.ClickOnRemoveButton(new List<string>() { "Commercial Customers: Collection", "Commercial Customers: Bin Removal", "Commercial Customers: Bin Delivery" })
                    .VerifyElementEnable(priceTab.nextBtn, true);
                priceTab.ClickNext();
                PageFactoryManager.Get<InvoiceDetailTab>()
                    .VerifyInvoiceOptions("Use Agreement")
                    .ClickFinish();
                partyAgreementPage
                    .VerifyServicePanelPresent()
                    .VerifyAgreementLineFormHasGreenBorder()
                    .ClickSaveBtn()
                    .VerifyToastMessage("Successfully saved agreement")
                    .WaitForLoadingIconToDisappear();
                partyAgreementPage
                    .VerifyServiceStartDate(DateTime.Now.ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT).Replace("-", "/"))
                    .VerifyElementText(partyAgreementPage.SiteName, "Greggs");
            }
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            int partyId = 73;
            string partyName = "Greggs";
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption(Contract.RMC)
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
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .OpenAgreementTab();
            var partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            AddNewAgreementLine(partyName);
            partyAgreementPage
                .ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Active")
                .ClosePartyAgreementPage();
            PageFactoryManager.Get<BasePage>().SwitchToChildWindow(2);
            AddNewAgreementLine(partyName);
            partyAgreementPage
                .ClosePartyAgreementPage()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickTabDropDown()
                .ClickTasksTab()
                .WaitForLoadingIconToDisappear();
            var taskTab = PageFactoryManager.Get<TaskTab>();
            taskTab.SendKeys(taskTab.TaskTypeSearch, "Deliver Commercial Bin");
            taskTab.ClickOnElement(taskTab.ApplyBtn);
            taskTab.VerifyTaskDataType("Deliver Commercial Bin");
            var rows = PageFactoryManager.Get<TaskTab>().TaskTableEle.GetRows();
            for (int i = 0; i < rows.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .DoubleClickTask(i)
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitingForTaskDetailsPageLoadedSuccessfully()
                    .ClickToDetailsTab();
                var taskDetailTab = PageFactoryManager.Get<TaskDetailTab>();
                PageFactoryManager.Get<TaskDetailTab>()
                    .SelectTextFromDropDown(taskDetailTab.detailTaskState, "Completed")
                    .ClickSaveBtn()
                    .VerifyToastMessage("Success")
                    .WaitUntilToastMessageInvisible("Success")
                    .ClickCloseBtn()
                    .SwitchToChildWindow(2);
            }
        }

        [Category("Agreement")]
        [Test(Description = "Verify that Service tasks were created correctly as result of creating Agreement with 2 Agreement Lines in TC 149A"), Order(2)]
        public void TC_149_B_Verify_that_Service_tasks_were_created_correctly_as_result_of_creating_Agreement_with_2_Agreement_Lines_in_TC_149A()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            //Go to Services and verify 
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.RMC)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .OpenOption("Active Service Tasks")
                .SwitchNewIFrame();
            var commonActiveServicesTaskPage = PageFactoryManager.Get<CommonActiveServicesTaskPage>();
            commonActiveServicesTaskPage
                .WaitForLoadingIconToDisappear();
            commonActiveServicesTaskPage
                .InputPartyNameToFilter("Greggs")
                .SendKeys(commonActiveServicesTaskPage.startDateInput, DateTime.Now.ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            commonActiveServicesTaskPage.ClickOnElement(commonActiveServicesTaskPage.endDateInput);
            commonActiveServicesTaskPage.SleepTimeInMiliseconds(300);
            commonActiveServicesTaskPage.ClearInputValue(commonActiveServicesTaskPage.endDateInput);
            commonActiveServicesTaskPage.SendKeysWithoutClear(commonActiveServicesTaskPage.endDateInput, "01/01/2050");
            commonActiveServicesTaskPage.ClickApplyBtn()
                .WaitForLoadingIconToDisappear();
            int rowCount = commonActiveServicesTaskPage.ServiceTaskLineTableEle.GetRows().Count;
            for (int i = 0; i < rowCount; i++)
            {
                commonActiveServicesTaskPage
                    .DoubleClickServiceTaskLine(i)
                    .SwitchToLastWindow();
                var servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
                servicesTaskPage
                    .WaitForLoadingIconToDisappear();
                servicesTaskPage
                     .ClickOnTaskLineTab();
                PageFactoryManager.Get<ServiceTaskLineTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<ServiceTaskLineTab>()
                    .VerifyTaskLine("Service", "1100L", "3", "General Recycling", "1000", "Kilograms", DateTime.Now.ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT), DateTime.Now.AddDays(1).ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT))
                    .ClickCloseBtn()
                    .SwitchToChildWindow(2);
            }
            PageFactoryManager.Get<BasePage>()
                .SwitchToFirstWindow()
                .SwitchToDefaultContent();
            //Navigate to the Agreement and edit
            int partyId = 73;
            string partyName = "Greggs";
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption(Contract.RMC)
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
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .OpenAgreementTab();
            var partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            partyAgreementPage.DoubleClickAgreement(0)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            partyAgreementPage.ClickEditAgreementBtn();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .WaitForLoadingIconToDisappear();
            string assetType = AgreementConstants.ASSET_TYPE_660L;
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickOnEditAsset()
                .SelectAssetType(assetType)
                .ClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                   .IsOnScheduleTab()
                   .ClickAddService()
                   .ClickDoneCurrentScheduleBtn()
                   .ClickOnNotSetLink()
                   .ClickOnWeeklyBtn()
                   .UntickAnyDayOption()
                   .SelectDayOfWeek("Tue")
                   .ClickDoneRequirementBtn()
                   .VerifyScheduleSummary("Once Every week on any Tuesday")
                   .ClickNext();
            var priceTab = PageFactoryManager.Get<PriceTab>();
            priceTab.WaitForLoadingIconToDisappear();
            priceTab.VerifyElementEnable(priceTab.nextBtn, false);
            priceTab.ClickOnRemoveButton(new List<string>() { "Commercial Customers: Collection", "Commercial Customers: Collection", "Commercial Customers: Bin Removal", "Commercial Customers: Bin Delivery" })
                     .VerifyElementEnable(priceTab.nextBtn, true);
            priceTab.ClickNext();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .VerifyInvoiceOptions("Use Agreement")
                .ClickFinish();
            partyAgreementPage
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            partyAgreementPage
                .ClosePartyAgreementPage()
                .SwitchToChildWindow(2);

            partyAgreementPage
                .DoubleClickAgreement(1)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            partyAgreementPage
                .ClickRemoveAgreementBtn()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            partyAgreementPage.VerifyServicePanelUnDisplay();
        }

        [Category("Agreement")]
        [Test(Description = "Verify that Demobilization tasks were created correctly as result of editing Agreement with 2 Agreement Lines in TC 149B"), Order(3)]
        public void TC_149_C_Verify_that_Demobilization_tasks_were_created_correctly_as_result_of_editing_Agreement_with_2_Agreement_Lines_in_TC_149B()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            int partyId = 73;
            string partyName = "Greggs";
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption(Contract.RMC)
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
                .ClickTabDropDown()
                .ClickTasksTab()
                .WaitForLoadingIconToDisappear();
            var taskTab = PageFactoryManager.Get<TaskTab>();
            taskTab.SendKeys(taskTab.TaskTypeSearch, "Remove Commercial Bin");
            taskTab.ClickOnElement(taskTab.ApplyBtn);
            var rows = PageFactoryManager.Get<TaskTab>().TaskTableEle.GetRows();
            for (int i = 0; i < rows.Count; i++)
            {
                if (i > 4) break;
                PageFactoryManager.Get<TaskTab>()
                    .DoubleClickTask(i)
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                   .VerifyTaskLine("Remove", "1100L", "1", "General Recycling", "1000", "Kilograms", "Unallocated")
                   .ClickCloseWithoutSaving()
                   .SwitchToChildWindow(2);
            }
        }

        [Category("PurchaseOrder")]
        [Test, Order(4)]
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
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
                .VerifyToastMessage("Number is required")
                .WaitUntilToastMessageInvisible("Number is required");
            PageFactoryManager.Get<AddPurchaseOrderPage>()
                .InputPONumber(PO_Number)
                .InputFirstDay(todayDate)
                .InputLastDay(datePlus)
                .SelectAgreement("41 - Richmond (Active from 21/03/2022 until 01/01/2050)")
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
                .WaitForAgreementPageLoadedSuccessfully("COMMERCIAL COLLECTIONS", "GREGGS");
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
                    .VerifyPurchaseOrderValue(PO_Number)
                    .CloseCurrentWindow()
                    .SwitchToChildWindow(3);
            }
        }
        [Category("PurchaseOrder")]
        [Test, Order(5)]
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
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
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
                .VerifyToastMessage("Success")
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
        [Test, Order(6)]
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

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser11.UserName, AutoUser11.Password)
                .IsOnHomePage(AutoUser11);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
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
            int taskId = PageFactoryManager.Get<TaskTab>()
                .getFirstTaskId();
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
                .VerifyToastMessage("Purchase Order # is required")
                .WaitUntilToastMessageInvisible("Purchase Order # is required");
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
