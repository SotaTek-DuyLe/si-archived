using NUnit.Allure.Core;
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
        [Category("Dee")]
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
                .Login(AutoUser23.UserName, AutoUser23.Password)
                .IsOnHomePage(AutoUser23);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectService(service)
                .SelectBusinessUnit(BusinessUnit.EastCollections)
                .TickSiteRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
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
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .SelectService("Select...")
                .UntickSiteRoam()
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchToDefaultContent();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Bulky Collections")
                .ExpandOption("Round Groups")
                .ExpandOption("BULKY1 (West)")
                .OpenOption("Monday")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");

            PageFactoryManager.Get<ServiceDefaultResourceTab>()
                .IsOnServiceDefaultTab()
                .ExpandOption("Driver")
                .ClickAddResource()
                .VerifyInputIsAvailable(resourceName)
                .SwitchToDefaultContent();

            //TC-34
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Municipal)
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
                .SelectTerm("40H Mon-Fri AM")
                .IsOnTermTab()
                .VerifyExtraTabsArePresent()
                .ClickSaveBtn()
                .SwitchToTab("Calendar")
                .ClickRefreshBtn();
            PageFactoryManager.Get<ResourceCalendarTab>()
                .VerifyWorkPatternIsSet("AM 05.00 - 14.00");
        }

        [Category("Resources")]
        [Category("Dee")]
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
                .Login(AutoUser23.UserName, AutoUser23.Password)
                .IsOnHomePage(AutoUser23);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.EastCollections)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
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
        [Category("Dee")]
        [Test]
        public void TC_49_Create_Resource_In_Default_Allocation()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser23.UserName, AutoUser23.Password)
                .IsOnHomePage(AutoUser23);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
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
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resrouce", resourceName)
                .VerifyFirstResultValue("Class", "Human")
                .VerifyFirstResultValue("Type", resourceType)
                .VerifyFirstResultValue("Contract", Contract.Commercial);
        }

        [Category("Sites")]
        [Category("Huong")]
        [Test()]
        public void TC_268_Resources_3rd_Party()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser23.UserName, AutoUser23.Password)
                .IsOnHomePage(AutoUser23);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2);
            ResoureDetailPage resoureDetailPage = PageFactoryManager.Get<ResoureDetailPage>();
            resoureDetailPage.WaitForLoadingIconToDisappear();
            resoureDetailPage.VerifyElementEnable(resoureDetailPage.SupplierSelect, false);
            resoureDetailPage.SelectThirdPartyCheckbox(true)
                .VerifyElementEnable(resoureDetailPage.SupplierSelect, true)
                .SelectIndexFromDropDown(resoureDetailPage.SupplierSelect, 1);
            resoureDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>().WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2);
            resoureDetailPage.WaitForLoadingIconToDisappear();
            resoureDetailPage.VerifyElementEnable(resoureDetailPage.SupplierSelect, true);
            resoureDetailPage.SelectThirdPartyCheckbox(false)
                .VerifyElementEnable(resoureDetailPage.SupplierSelect, false);
        }

        [Category("Sites")]
        [Category("Huong")]
        [Test()]
        public void TC_269_Resources_Add_BUG()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser23.UserName, AutoUser23.Password)
                .IsOnHomePage(AutoUser23);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            ResourceListingPage resourceListingPage = PageFactoryManager.Get<ResourceListingPage>();
            resourceListingPage.VerifyBusinessUnitGroupHeaderVisible();
            resourceListingPage.SendKeys(resourceListingPage.BusinessUnitGroupHeaderInput, "Collections");
            resourceListingPage.WaitForLoadingIconToDisappear();
            resourceListingPage.WaitForLoadingIconToDisappear();
            resourceListingPage.VerifyBusinessUnitGroupColumn("Collections");

            resourceListingPage.ClickOnElement(resourceListingPage.ClearFilterButton);
            resourceListingPage.WaitForLoadingIconToDisappear();
            resourceListingPage.VerifyClientReferenceVisible();
            resourceListingPage.SendKeys(resourceListingPage.ClientReferenceHeaderInput, "E1776");
            resourceListingPage.WaitForLoadingIconToDisappear();
            resourceListingPage.WaitForLoadingIconToDisappear();
            resourceListingPage.VerifyClientReferenceColumn("E1776");
        }
    }
}
