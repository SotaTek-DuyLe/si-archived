using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.PartyAgreement.AddService;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Agrrements;
using System;
using System.Collections.Generic;
using System.Text;

using static si_automated_tests.Source.Main.Models.UserRegistry;
using si_automated_tests.Source.Main.Pages.Paties.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;

namespace si_automated_tests.Source.Test.AggrementLineTest
{
    public class EditAgreementLineTest : BaseTest
    {
        [Test]
        public void TC_016()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14)
                .ClickParties()
                .ClickNSC()
                .ClickAgreementSubMenu()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(27)
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
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .ClickOnEditAsset()
                .EditAssetQuantity(3)
                .ClickOnTenureText()
                .EditAssertClickDoneBtn()
                .VerifySummaryOfStep("3 x 1100L(Rental), 100kg Paper & Cardboard")
                .ClickNext()
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
                .IsOnPriceTab()
                .RemoveAllRedundantPrice()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .IsOnInvoiceDetailsTab()
                .ClickFinish()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();

            //Step 18 Go to task tab to verify editition
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .VerifyTwoNewTaskAppear();

            //Verity 1 Task Line on the Task 1 created
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .GoToFirstTask()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskPage>()
                .ClickToTaskLinesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskPage>()
                .VerifyTaskLine("Deliver", "1100L", "1", "Paper & Cardboard", "100", "Kilograms", "Unallocated")
                .InputActuaAssetQuantity(1)
                .ClickOnAcualAssetQuantityText()
                .SelectCompletedState()
                .ClickOnAcualAssetQuantityText()
                .CLickOnSaveBtn()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskPage>()
                .ClickCloseWithoutSaving()
                .SwitchToChildWindow(2);

            //Verity 1 Task Line on the Task 2 created
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .GoToSecondTask()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskPage>()
                .ClickToTaskLinesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskPage>()
                .VerifyTaskLine("Deliver", "1100L", "1", "Paper & Cardboard", "100", "Kilograms", "Unallocated")
                .InputActuaAssetQuantity(1)
                .ClickOnAcualAssetQuantityText()
                .SelectCompletedState()
                .ClickOnAcualAssetQuantityText()
                .CLickOnSaveBtn()
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskPage>()
                .ClickCloseWithoutSaving()
                .SwitchToChildWindow(2);

            ////Verify date in expand is tomorrow 
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1).Replace('-', '/');
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyAssetAndProductAssetTypeStartDate(tommorowDate)
                .VerifyRegularAssetTypeStartDate(tommorowDate)
                .VerifyTaskLineTypeStartDates(tommorowDate)
                .CloseWithoutSaving()
                .SwitchToChildWindow(1);

            PageFactoryManager.Get<HomePage>()
                .ClickParties()
                .ClickNSC()
                .ClickSiteServiceSubMenu()
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(27)
                .VerifyFirstLineAgreementResult(54, 27)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .GoToAllTabAndConfirmNoError();
        }

        [Test]
        public void TC_016()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14)
                .ClickParties()
                .ClickNSC()
                .ClickAgreementSubMenu()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(27)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
        }
        }
}
