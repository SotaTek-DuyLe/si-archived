using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.DebriefResult;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.DriverDebriefTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class DriverDebriefTest : BaseTest
    {
        [Category("Driver Debrief")]
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
            debriefResultPage.DragBinLiftToTaskLine()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            debriefResultPage.VerifyTaskLineStateIsCompleted();

            //Click on different x bin lift -> Find your task you updated in previous step
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftFirstRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.VerifyTaskLineStateIsCompleted();

            //Click on x bin lift and select bin lift you used before -> Click unmatch
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftFirstRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftSecondRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.ClickOnElement(debriefResultPage.UnmatchButton);
            debriefResultPage.VerifyToastMessage(MessageSuccessConstants.SavedMessage)
                .WaitForLoadingIconToDisappear();
            //Click on x bin lift -> Find the task you are working with
            debriefResultPage.ClickOnElement(debriefResultPage.BinLiftSecondRow);
            debriefResultPage.WaitForLoadingIconToDisappear();
            debriefResultPage.VerifyFirstTaskRatio();
        }
    }
}
