﻿
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.DebriefResult;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.DriverDebriefTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class DriverDebriefTest : BaseTest
    {
        [Category("Driver Debrief")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test(Description = "Driver Debrief - GPS event matching - tasks are not immediately updated to show changes to task and task lines after matching/unmatching (bug fix) ")]
        public void TC_194_Driver_Debrief_GPS_event_matching()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/round-instances/6113");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser60.UserName, AutoUser60.Password);
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundInstanceForm.WaitForLoadingIconToDisappear();
            roundInstanceForm
               .ClickOnDetailTab()
               .ClickOnStatusDdAndSelectValue("Complete")
               .ClickSaveBtn()
               .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            roundInstanceForm.ClickOnElement(roundInstanceForm.DebriefButton);
            roundInstanceForm.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DebriefResultPage debriefResultPage = PageFactoryManager.Get<DebriefResultPage>();
            //Click on Activity -> confirmations -> select X bin lift -> Drag and drop it to the task line 
            debriefResultPage.ClickOnElement(debriefResultPage.ActivityHeader);
            debriefResultPage.ClickOnElement(debriefResultPage.ConfirmationHeader);
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftSecondRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.DragSecondBinLiftToTaskLine()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            debriefResultPage.SleepTimeInMiliseconds(10000);
            debriefResultPage.VerifyTaskLineStateIsCompleted();

            //Click on different x bin lift -> Find your task you updated in previous step
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftFirstRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.SleepTimeInMiliseconds(10000);
            debriefResultPage.VerifyTaskLineStateIsCompleted();

            //Click on x bin lift and select bin lift you used before -> Click unmatch
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftFirstRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftSecondRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.WaitForDebriefLoaded();
            debriefResultPage.ClickOnElement(debriefResultPage.UnmatchButton);
            debriefResultPage.VerifyDisplayToastMessage(MessageSuccessConstants.SavedMessage)
                .WaitForLoadingIconToDisappear();
            //Click on x bin lift -> Find the task you are working with
            debriefResultPage.SleepTimeInMiliseconds(10000);
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftSecondRow);
            debriefResultPage.VerifyFirstTaskRatio();
        }

        [Category("Driver Debrief")]
        [Category("Huong")]
        [Test(Description = "Verify whether Debrief form's GPS events - Unmatch is working as expected")]
        public void TC_172_1_Debrief_Results_screen()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/debrief-results/5059");
            PageFactoryManager.Get<LoginPage>()
               .IsOnLoginPage()
               .Login(AutoUser60.UserName, AutoUser60.Password);
            
            DebriefResultPage debriefResultPage = PageFactoryManager.Get<DebriefResultPage>();
            debriefResultPage.WaitForLoadingIconToDisappear();
            // At Debrief-result > Activity > Confirations > Click a bin lift with tick icon - Select Unmatch
            debriefResultPage.ClickOnElement(debriefResultPage.ActivityHeader);
            debriefResultPage.ClickOnElement(debriefResultPage.ConfirmationHeader);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftFirstRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.ClickOnElement(debriefResultPage.UnmatchButton);
            debriefResultPage.VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            debriefResultPage.VerifyBinLiftStateIsNotCompleted();

            //Verify whether Debrief form's GPS events - Unmatch is working as expected
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftFirstRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.DragFirstBinLiftToTaskLine()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            debriefResultPage.SleepTimeInMiliseconds(10000);
            debriefResultPage.VerifyBinLiftStateIsCompleted();
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftFirstRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.WaitForDebriefLoaded();
            debriefResultPage.ClickOnElement(debriefResultPage.UnknownButton);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.WaitForDebriefLoaded();
            debriefResultPage.ClickOnElement(debriefResultPage.MatchButton);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.WaitForDebriefLoaded();
            debriefResultPage.DragFirstBinLiftToTaskLine()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            debriefResultPage.SleepTimeInMiliseconds(10000);
            debriefResultPage.VerifyBinLiftStateIsCompleted();
        }
    }
}
