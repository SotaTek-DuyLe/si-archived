using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TimeSheetTests : BaseTest
    {
        [Category("TimeSheet")]
        [Category("Huong")]
        [Test()]
        public void TC_263_TimeSheet()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser69.UserName, AutoUser69.Password)
                .IsOnHomePage(AutoUser69);
            TimeSheetListPage timeSheetListPage = PageFactoryManager.Get<TimeSheetListPage>();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Timesheet")
                .SwitchToFrame(timeSheetListPage.TimeSheetIframe);
            timeSheetListPage.WaitForLoadingIconToDisappear();
            timeSheetListPage.VerifyTimeSheetDisplayCorrectly()
                .FilterBussinessUnitGroup("Collections")
                .VerifyBussinessUnitGroup("Collections")
                .FilterSupplier("M & M Recruitment")
                .VerifySupplier("M & M Recruitment");
        }
    }
}
