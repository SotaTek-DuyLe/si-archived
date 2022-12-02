using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.EventTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CommonEventTests : BaseTest
    {
        [Category("CommonEvent")]
        [Category("Huong")]
        [Test(Description = "The invalid contract units are listed on events")]
        public void TC_203_The_invalid_contract_units_are_listed_on_events()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            var contractUnits = finder.GetContractUnits();
            Assert.IsTrue(contractUnits.Any(x => x.contractunit == "Ancillary" && x.enddate < DateTime.Now));
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser53.UserName, AutoUser53.Password)
                .IsOnHomePage(AutoUser53);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            EventsListingPage eventListingPage = PageFactoryManager.Get<EventsListingPage>();
            eventListingPage.FilterByEventId("21")
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage.ExpandDetailToggle()
                .ClickOnAllocatedUnit()
                .VerifySelectValueNotInDetailAllocatedUnit("Ancillary")
                .ClickAllocateEventInEventActionsPanel()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            //EVENT ACTION
            EventActionPage eventActionPage = PageFactoryManager.Get<EventActionPage>();
            eventActionPage
                .IsEventActionPage()
                .ClickOnAllocatedUnit()
                .VerifySelectValueNotInAllocatedUnit("Ancillary")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            eventDetailPage.ClickInspectionBtn()
                .IsCreateInspectionPopup(false)
                .ClickAndSelectInspectionType("Site Inspection")
                .ClickAndVerifyAllocatedUnitNotContainValue("Ancillary");
        }
    }
}
