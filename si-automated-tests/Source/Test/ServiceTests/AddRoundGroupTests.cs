using System;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAddress;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class AddRoundGroupTests : BaseTest
    {
        [Category("109_Add a Round Group")]
        [Test]
        public void TC_109_Add_Round_Group()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser26.UserName, AutoUser26.Password)
                .IsOnHomePage(AutoUser26);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToLastWindow();
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDefaultDataOnAddForm()
                .ClickSaveBtn()
                .VerifyToastMessage("Field is required")
                .WaitUntilToastMessageInvisible("Field is required");
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .EnterRoundGroupValue("SKIP 3")
                .ClickSaveBtn()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyServiceButtonsVisible()
                .ClickOnDispatchSiteAndVerifyData()
                .SelectDispatchSite("Kingston Tip")
                .EnterRoundGroupValue("SKIP 3ABC")
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRoundGroup("SKIP 3ABC");
        }

        [Category("110_Add Round on a Round Group")]
        [Test]
        public void TC_110_Add_Round_on_a_Round_Group()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<RoundGroupListPage>()
                .DoubleClickRoundGroup("SKIP2 Daily")
                .SwitchToLastWindow();
            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickRoundTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickAddNewItem()
                .ClickSaveBtn()
                .VerifyToastMessages(new System.Collections.Generic.List<string>() { "Shift is required" , "Dispatch Site is required" , "Round Type is required" , "Round is required" });

            int newRowIdx = PageFactoryManager.Get<RoundGroupPage>().GetIndexNewRoundRow();
            PageFactoryManager.Get<RoundGroupPage>()
                .EnterRoundValue(newRowIdx, "Test Round")
                .EnterRoundTypeValue(newRowIdx, "Skips")
                .EnterDispatchSiteValue(newRowIdx, "Townmead Tip & Depot (East)")
                .EnterShiftValue(newRowIdx, "PM: 14.00 - 21.30")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            Thread.Sleep(1000);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRoundColor(newRowIdx, "#000000")
                .DoubleClickRound(newRowIdx)
                .SwitchToLastWindow();

            PageFactoryManager.Get<RoundDetailPage>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<RoundDetailPage>()
                .VerifyRoundInput("Test Round")
                .VerifyRoundType("Skips")
                .VerifyDispatchSite("Townmead Tip & Depot (East)")
                .VerifyShift("PM: 14.00 - 21.30")
                .ClickAllTabAndVerify()
                .ClickCloseBtn()
                .AcceptAlert();
        }
    }
}
