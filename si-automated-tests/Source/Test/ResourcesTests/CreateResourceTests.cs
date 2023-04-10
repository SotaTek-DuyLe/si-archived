using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Linq;
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
                .WaitForLoadingIconToDisappear()
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
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Calendar")
                .WaitForLoadingIconToDisappear();
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

        [Category("InvoiceShedules")]
        [Category("Huong")]
        [Test()]
        public void TC_305_Custom_Invoice_Schedules_Resource_Shift_Schedule_Set_Regular_Custom_Schedule()
        {
            //Verify that 'Custom' option is added on Resource Shift Schedule form and user can set Regular Custom Schedule
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
                .FilterItem(119, false);
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2);
            ResoureDetailPage resoureDetailPage = PageFactoryManager.Get<ResoureDetailPage>();
            resoureDetailPage.WaitForLoadingIconToDisappear();
            resoureDetailPage.ClickOnElement(resoureDetailPage.ShiftScheduleTab);
            resoureDetailPage.WaitForLoadingIconToDisappear();
            resoureDetailPage.WaitForLoadingIconToDisappear();
            resoureDetailPage.ClickOnElement(resoureDetailPage.AddNewShiftScheduleButton);
            resoureDetailPage.SwitchToChildWindow(3);
            resoureDetailPage.WaitForLoadingIconToDisappear();
            ShiftSchedulePage shiftSchedulePage = PageFactoryManager.Get<ShiftSchedulePage>();
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.ShiftDropdown);
            shiftSchedulePage.SelectByDisplayValueOnUlElement(shiftSchedulePage.ShiftMenu, "06.00 - 14.30 AM");
            DateTime endDate = DateTime.Now.AddYears(3);
            shiftSchedulePage.SendKeys(shiftSchedulePage.EndDateInput, endDate.ToString("dd/MM/yyyy"));
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.CustomButton);
            shiftSchedulePage.ClickYearButton();
            shiftSchedulePage.VerifyElementVisibility(shiftSchedulePage.YearDatePicker, true);
            //Click 'Save' on Shift Schedule form
            shiftSchedulePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            shiftSchedulePage.VerifyElementEnable(shiftSchedulePage.SetRegularCustomScheduleButton, true)
                .VerifyElementEnable(shiftSchedulePage.SetCustomScheduleDatesButton, true);

            //Click on 'Set Regular Custom Schedule'
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.SetRegularCustomScheduleButton);
            shiftSchedulePage.SleepTimeInMiliseconds(200);
            DateTime tomorrow = DateTime.Now.AddDays(1);
            shiftSchedulePage.VerifyInputValue(shiftSchedulePage.PatternWeekInput, "1")
                .VerifyInputValue(shiftSchedulePage.EffectiveDateInput, tomorrow.ToString("dd/MM/yyyy"))
                .VerifyInputValue(shiftSchedulePage.PatternEndDateInput, endDate.ToString("dd/MM/yyyy"))
                .VerifyElementIsMandatory(shiftSchedulePage.CustomScheduleDescriptionInput, true);
            //Click 'Confirm' button
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.ConfirmCustomScheduleButton);
            shiftSchedulePage.SleepTimeInMiliseconds(200);
            shiftSchedulePage.VerifyElementEnable(shiftSchedulePage.ApplyCustomScheduleButton, false)
                .VerifyElementEnable(shiftSchedulePage.ClearCustomScheduleButton, true);
            shiftSchedulePage.VerifyPatternOfWeekInWeek(tomorrow, 1);
            //Select any dates in calendar and click 'Apply'
            shiftSchedulePage.ClickScheduleDate(tomorrow.AddDays(1));
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.ApplyCustomScheduleButton);
            shiftSchedulePage.VerifyToastMessage("Custom Schedule Description is required");

            //In Pop up, set following:
            //Pattern Num of Weeks = 3
            //Effective Date = tomorrow's date
            //Pattern End Date = tomorrow + 5 months
            //Custom Schedule Description
            //Click 'Confirm'
            shiftSchedulePage.SetInputValue(shiftSchedulePage.PatternWeekInput, "3")
                .SetInputValue(shiftSchedulePage.PatternEndDateInput, tomorrow.AddMonths(5).ToString("dd/MM/yyyy"))
                .SetInputValue(shiftSchedulePage.CustomScheduleDescriptionInput, "Custom Schedule Description")
                .ClickOnElement(shiftSchedulePage.ConfirmCustomScheduleButton);
            shiftSchedulePage.VerifyElementEnable(shiftSchedulePage.ApplyCustomScheduleButton, false)
                .VerifyElementEnable(shiftSchedulePage.ClearCustomScheduleButton, true);
            shiftSchedulePage.VerifyPatternOfWeekInWeek(tomorrow, 3);

            //Select days in each week in the table
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)tomorrow.DayOfWeek + 7) % 7;
            //if today is monday, add seven days
            if (daysUntilMonday == 0)
                daysUntilMonday = 7;
            shiftSchedulePage.ClickScheduleDate(tomorrow.AddDays(1));
            shiftSchedulePage.ClickScheduleDate(tomorrow.AddDays(daysUntilMonday));
            shiftSchedulePage.VerifySelectedScheduleDate(tomorrow.AddDays(1), true)
                .VerifySelectedScheduleDate(tomorrow.AddDays(daysUntilMonday), true);
            //Click 'Clear' button
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.ClearCustomScheduleButton);
            shiftSchedulePage.SleepTimeInMiliseconds(200);
            shiftSchedulePage.VerifySelectedScheduleDate(tomorrow.AddDays(1), false)
                .VerifySelectedScheduleDate(tomorrow.AddDays(daysUntilMonday), false);
            //Click 'Cancel' button
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.CancelCustomScheduleButton);
            shiftSchedulePage.SleepTimeInMiliseconds(200);
            shiftSchedulePage.VerifyElementVisibility(shiftSchedulePage.RegularCustomScheduleForm, false);

            //1) Click again on 'Set Regular Custom Schedule'
            // 2) In pop up, set:
            // Pattern Num of Weeks = 2
            //Effective Date = tomorrow's date
            //Pattern End Date = tomorrow + 2 months
            //Custom Schedule Description
            //Click 'Confirm'
            //Select days in each week in the table
            //Click 'Apply'
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.SetRegularCustomScheduleButton);
            shiftSchedulePage.SetInputValue(shiftSchedulePage.PatternWeekInput, "2")
                .SetInputValue(shiftSchedulePage.PatternEndDateInput, tomorrow.AddMonths(2).ToString("dd/MM/yyyy"))
                .SetInputValue(shiftSchedulePage.CustomScheduleDescriptionInput, "Custom Schedule Description1")
                .ClickOnElement(shiftSchedulePage.ConfirmCustomScheduleButton);
            shiftSchedulePage.VerifyPatternOfWeekInWeek(tomorrow, 2);
            shiftSchedulePage.VerifyElementEnable(shiftSchedulePage.ApplyCustomScheduleButton, false)
                .VerifyElementEnable(shiftSchedulePage.ClearCustomScheduleButton, true);
            shiftSchedulePage.ClickScheduleDate(tomorrow.AddDays(1));
            shiftSchedulePage.ClickScheduleDate(tomorrow.AddDays(daysUntilMonday));
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.ApplyCustomScheduleButton);
            shiftSchedulePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);

            //1) In DB, run following query: 
            // select* from resourceshiftschedules where resourceID = x(resource to which schedule is being added) - and take note of ScheduleID from this query
            //2) Next, run:
            // select* from scheduledates where scheduleID = y(from query above)
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string resourceshiftscheduleID = shiftSchedulePage.GetCurrentUrl().Split('/').Last();
            var resourceShift = commonFinder.GetResourceShiftSchedules(resourceshiftscheduleID).FirstOrDefault();
            var scheduleDates = commonFinder.GetScheduleDateModel(resourceShift.scheduleID);
            Assert.IsTrue(scheduleDates.OrderBy(x => x.scheduledate).First().scheduledate > tomorrow);
        }
    }
}
