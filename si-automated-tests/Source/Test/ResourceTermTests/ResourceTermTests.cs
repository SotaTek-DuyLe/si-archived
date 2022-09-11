using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using si_automated_tests.Source.Main.Pages.ResourceTerm;
using si_automated_tests.Source.Main.Pages.Task;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourceTermTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ResourceTermTests : BaseTest
    {
        [Category("Resource Term")]
        [Test]
        public void TC_160_Resource_term_Entitlements_state_displays_vehicle_states()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/grids/resourceterms");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser31.UserName, AutoUser31.Password);
            ResourceTermPage resourceTermPage = PageFactoryManager.Get<ResourceTermPage>();
            resourceTermPage.WaitForLoadingIconToDisappear();
            resourceTermPage.DoubleClickRow(0)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ResourceTermDetailPage resourceTermDetailPage = PageFactoryManager.Get<ResourceTermDetailPage>();
            resourceTermDetailPage.ClickOnElement(resourceTermDetailPage.EntitlementTab);
            List<string> resourceStateExpected = new List<string>() { "Allocated", "Available", "AWOL", "Holiday", "Jury Service", "Scheduled", "Sick", "TOIL", "Training" };
            resourceTermDetailPage.VerifyResourceStateValue(0, resourceStateExpected);
            CommonFinder finder = new CommonFinder(DbContext);
            var humanResourceClass = finder.GetResourceClasses().FirstOrDefault(x => x.resourceclass == "Human");
            var resourceStates = finder.GetResourceStates(humanResourceClass.resourceclassID).Select(x => x.resourcestate).ToList();
            Assert.That(resourceStates, Is.EquivalentTo(resourceStateExpected));
        }
    }
}
