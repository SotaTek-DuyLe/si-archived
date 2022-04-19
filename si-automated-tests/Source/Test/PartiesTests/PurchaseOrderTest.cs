using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.PartyTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class PurchaseOrderTest : BaseTest
    {
        [Category("PurchaseOrder")]
        [Test]
        public void TC_074_Add_Purchase_Order()
        {
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string datePlus = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 30);
            int partyId = 73;
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
    } 
}
