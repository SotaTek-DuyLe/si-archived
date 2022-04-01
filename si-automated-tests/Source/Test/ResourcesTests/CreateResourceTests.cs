using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope:ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateResourceTests : BaseTest
    {
        [Category("Resources")]
        [Test]
        public void TC_30_31_32_33_34_Create_Human_Resource()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string startDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string defaultEndDate = "01/01/2050";
            string resourceType = "Driver";
            string service = "Clinical Waste";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectService(service)
                .TickSiteRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Name", resourceName)
                .VerifyFirstResultValue("Resource Type", resourceType)
                .VerifyFirstResultValue("Start Date", startDate)
                .VerifyFirstResultValue("End Date", defaultEndDate)
                .SwitchToDefaultContent();

            //TC-31
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star")
                .ExpandOption("Ancillary")
                .ExpandOption("Clinical Waste")
                .ExpandOption("Round Groups")
                .ExpandOption("CLINICAL1")
                .OpenOption("Monday")
                .SwitchNewIFrame()
                .SleepTimeInMiliseconds(3000)
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<ServiceDefaultResourceTab>()
                .IsOnServiceDefaultTab()
                .ExpandOption("Driver")
                .ClickAddResource()
                .VerifyInputIsAvailable(resourceName)
                .SwitchToDefaultContent();

            //TC-32-33
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("North Star")
                .AcceptAlert()
                .AcceptAlert()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .SelectService("Select...")
                .UntickSiteRoam()
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchToDefaultContent();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Bulky Collections")
                .ExpandOption("Round Groups")
                .ExpandOption("BULKY1")
                .OpenOption("Monday")
                .SwitchNewIFrame()
                .SwitchToTab("Default Resources");

            PageFactoryManager.Get<ServiceDefaultResourceTab>()
                .IsOnServiceDefaultTab()
                .ExpandOption("Driver")
                .ClickAddResource()
                .VerifyInputIsAvailable(resourceName)
                .SwitchToDefaultContent();

            //TC-34
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("North Star")
                .AcceptAlert()
                .AcceptAlert()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .SwitchToTab("Calendar");
            PageFactoryManager.Get<ResourceCalendarTab>()
                .VerifyWorkPatternNotSet()
                .SwitchToTab("Resource Terms");
            PageFactoryManager.Get<ResourceTermTab>()
                .IsOnTermTab()
                .SelectTerm("40H Mon-Fri")
                .IsOnTermTab()
                .VerifyExtraTabsArePresent()
                .ClickSaveBtn()
                .SwitchToTab("Calendar")
                .ClickRefreshBtn();
            PageFactoryManager.Get<ResourceCalendarTab>()
                .VerifyWorkPatternIsSet("AM 05.00 - 14.00");
        }

        [Category("Resources")]
        [Test]
        public void TC_35_36_Create_Vehicle_Resource_Test()
        {
            string resourceName = "Cage " + CommonUtil.GetRandomNumber(5);
            string startDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string defaultEndDate = "01/01/2050";
            string resourceType = "Cage";

            //TC-35
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Name", resourceName)
                .VerifyFirstResultValue("Resource Type", resourceType)
                .VerifyFirstResultValue("Start Date", startDate)
                .VerifyFirstResultValue("End Date", defaultEndDate)
                .SwitchToDefaultContent();
            //TC-36
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star")
                .ExpandOption("Ancillary")
                .ExpandOption("Clinical Waste")
                .ExpandOption("Round Groups")
                .ExpandOption("CLINICAL1")
                .OpenOption("Monday")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<ServiceDefaultResourceTab>()
                .IsOnServiceDefaultTab()
                .ExpandOption("Cage")
                .ClickAddResource()
                .VerifyInputIsAvailable(resourceName)
                .SwitchToDefaultContent();
        }
        [Category("Resources")]
        [Test]
        public void TC_49_Create_Resource_In_Default_Allocation()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract("North Star Commercial")
                .SelectBusinessUnit("North Star Commercial")
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resrouce", resourceName)
                .VerifyFirstResultValue("Class", "Human")
                .VerifyFirstResultValue("Type", resourceType)
                .VerifyFirstResultValue("Contract", "North Star Commercial");
        }
    }
}
