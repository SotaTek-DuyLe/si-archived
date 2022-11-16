using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Suspension;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.Inspections;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccount;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using OpenQA.Selenium;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.Services;

namespace si_automated_tests.Source.Test.AdHocTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateAdHocTest : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            Login();
        }

        public void Login()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser35.UserName, AutoUser35.Password)
                .IsOnHomePage(AutoUser35);
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test, Order(1)]
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
                .SelectKiloGramAsUnit()
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
        [Category("Huong")]
        [Test, Order(2)]
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

            //PageFactoryManager.Get<LoginPage>()
            //   .GoToURL(WebUrl.MainPageUrl);
            //PageFactoryManager.Get<LoginPage>()
            //    .IsOnLoginPage()
            //    .Login(AutoUser13.UserName, AutoUser13.Password)
            //    .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.RMC)
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

        [Category("Create ad-hoc task")]
        [Category("Huong")]
        [Test(Description = "Create ad-hoc task"), Order(3)]
        public void TC_091_CreateAdHocTask()
        {
            int partyId = 73;
            string partyName = "Greggs";
            string inputPO = "PO ad hoc task 1";
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
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .UncheckOnAccountType("PO Number Required")
                .CheckOnAccountType("PO Number Required")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>().ClickAdHocTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AdhocPage>()
                .ClickCreateAdHocTask("Repair Commercial Bin");
            Thread.Sleep(200);
            PageFactoryManager.Get<CreateAdHocTaskPage>()
                .VerifyTitle("PO Number Required for Party")
                .InputPoNumber(inputPO)
                .ClickDone()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            AdhocTaskDetailPage adhocTaskDetailPage = PageFactoryManager.Get<AdhocTaskDetailPage>();
            adhocTaskDetailPage.ClickOnElement(adhocTaskDetailPage.DetailTab);
            adhocTaskDetailPage.WaitForLoadingIconToDisappear();
            adhocTaskDetailPage
                .VerifyPoNumber()
                .VerifyPurchaseOrderField(inputPO)
                .ClickTaskLinesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskLinesPage>()
                .VerifyTaskLine(new Main.Models.Adhoc.TaskLinesModel() 
                { 
                    Type = "Service",
                    AssetType = "660L",
                    ScheduledAssetQty = "1",
                    Product = "General Refuse",
                    ScheduledProductQuantity = "0",
                    Unit = "Kilograms",
                    State = "Unallocated"
                });
        }

        [Category("Create Ad-Hoc Task from an Agreement form")]
        [Category("Huong")]
        [Test(Description = "Create ad-hoc task from an Agreement form"), Order(4)]
        public void TC_092_CreateAdHocTask()
        {
            int partyId = 73;
            string partyName = "Greggs";
            string inputPO = "PO ad hoc task 2";
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
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAccountPage>()
                .IsOnAccountPage()
                .UncheckOnAccountType("PO Number Required")
                .CheckOnAccountType("PO Number Required")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .OpenAgreementWithId(41)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAdhocOnAgreementFields()
                .CreateAdhocTaskBtnInAgreementLine("Collect Missed Commercial");
            Thread.Sleep(200);

            PageFactoryManager.Get<CreateAdHocTaskPage>()
               .VerifyTitle("PO Number Required for Party")
               .InputPoNumber(inputPO)
               .ClickDone()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            AdhocTaskDetailPage adhocTaskDetailPage = PageFactoryManager.Get<AdhocTaskDetailPage>();
            adhocTaskDetailPage.ClickOnElement(adhocTaskDetailPage.DetailTab);
            adhocTaskDetailPage.WaitForLoadingIconToDisappear();
            adhocTaskDetailPage
                .VerifyPoNumber()
                .VerifyPurchaseOrderField(inputPO)
                .ClickTaskLinesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskLinesPage>()
                .VerifyTaskLine(new Main.Models.Adhoc.TaskLinesModel()
                {
                    Type = "Service",
                    AssetType = "660L",
                    ScheduledAssetQty = "1",
                    Product = "General Refuse",
                    ScheduledProductQuantity = "0",
                    Unit = "Kilograms",
                    State = "Unallocated"
                });
        }
    }
}
