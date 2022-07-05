using NUnit.Framework;
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
    public class RoundCalendarTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Applications")
                .OpenOption("Round Calendar")
                .SwitchNewIFrame();
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
        }

        [Category("RoundCalendarTests")]
        [Test(Description = "Verify that Round Calendar displays correctly")]
        public void TC_135_1_Verify_that_Round_Calendar_displays_correctly()
        {
            RoundCalendarPage roundCalendarPage = PageFactoryManager.Get<RoundCalendarPage>();
            roundCalendarPage.ClickOnElement(roundCalendarPage.InputService);
            roundCalendarPage
                .SelectNode("North Star")
                .SelectNode("Recycling")
                .SelectNode("Domestic Recycling");
        }
    }
}
