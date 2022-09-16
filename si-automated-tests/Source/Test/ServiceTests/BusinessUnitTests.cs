using System;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    [AllureNUnit]
    public class BusinessUnitTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO BUSINESS UNITS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser25.UserName, AutoUser25.Password)
                .IsOnHomePage(AutoUser25);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.RMC)
                .ExpandOption("Business Unit Groups")
                .ExpandOptionLast("Collections")
                .OpenOption("Business Units")
                .SwitchNewIFrame();
        }
        [Category("BusinessUnit")]
        [Category("Dee")]
        [Test]
        public void TC_95_business_unit_group()
        {
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem();
            PageFactoryManager.Get<BusinessUnitPage>()
                .InputBusinessName("Food")
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Business Unit")
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            var expectedUnits = PageFactoryManager.Get<CommonBrowsePage>()
                .GetListOfValueFilterBy("Name");
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RMC)
                .ExpandBusinessUnitOption("Collections")
                .VerifyBusinessUnitsAre(expectedUnits);
        }
    }
}
