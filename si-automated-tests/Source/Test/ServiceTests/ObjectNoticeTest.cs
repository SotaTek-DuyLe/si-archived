using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models.Services;
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
    public class ObjectNoticeTest : BaseTest
    {
        [Category("Verify if a new Object Notice can be created from Contracts")]
        [Category("Huong")]
        [Test]
        public void TC_123_1_Verify_if_a_new_Object_Notice_can_be_created_from_Contracts()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);

            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(2);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            string description = "Test for Object Notice for NSC contract";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Notice Type is required")
                .WaitUntilToastMessageInvisible("Notice Type is required");
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));
            objectNoticeTab.DoubleClickNewObjectNotice();
            objectNoticeTab
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear(false);
            string startDate = londonCurrentDate.AddDays(3).ToString("dd/MM/yyyy");
            objectNoticeForm.ClearInputValue(objectNoticeForm.StartDateInput);
            objectNoticeForm.SendKeys(objectNoticeForm.StartDateInput, startDate);
            objectNoticeForm.ClickOnElement(objectNoticeForm.EndDateInput);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "INACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, startDate);

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            finder.IsObjectNoticeExist("10", objectNoticeTab.GetIdNewObjectNotice());
        }

        [Category("Verify if a new Object Notice can be created from Region")]
        [Category("Huong")]
        [Test]
        public void TC_123_2_Verify_if_a_new_Object_Notice_can_be_created_from_Region()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .OpenOption(Region.UK)
                .SwitchNewIFrame();
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);

            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(2);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            string description = "Test for Object Notice for Region";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Notice Type is required")
                .WaitUntilToastMessageInvisible("Notice Type is required");
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));
            objectNoticeTab.DoubleClickNewObjectNotice();
            objectNoticeTab
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear(false);

            string startDate = londonCurrentDate.AddDays(3).ToString("dd/MM/yyyy");
            objectNoticeForm.ClearInputValue(objectNoticeForm.StartDateInput);
            objectNoticeForm.SendKeys(objectNoticeForm.StartDateInput, startDate);
            objectNoticeForm.ClickOnElement(objectNoticeForm.EndDateInput);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "INACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, startDate);

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            finder.IsObjectNoticeExist("47", objectNoticeTab.GetIdNewObjectNotice());
        }

        [Category("Verify if a new Object Notice can be created from Service Groups")]
        [Category("Huong")]
        [Test]
        public void TC_123_3_Verify_if_a_new_Object_Notice_can_be_created_from_Service_Groups()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Ancillary")
                .SwitchNewIFrame();
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);

            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(2);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            string description = "Test for Object Notice for Service Group";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Notice Type is required")
                .WaitUntilToastMessageInvisible("Notice Type is required");
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));
            objectNoticeTab.DoubleClickNewObjectNotice();
            objectNoticeTab
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear(false);

            string startDate = londonCurrentDate.AddDays(-3).ToString("dd/MM/yyyy");
            objectNoticeForm.ClearInputValue(objectNoticeForm.StartDateInput);
            objectNoticeForm.SendKeys(objectNoticeForm.StartDateInput, startDate);
            objectNoticeForm.ClickOnElement(objectNoticeForm.EndDateInput);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, startDate);

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            finder.IsObjectNoticeExist("64", objectNoticeTab.GetIdNewObjectNotice());
        }

        [Category("Verify if a new Object Notice can be created from Service")]
        [Category("Huong")]
        [Test]
        public void TC_123_4_Verify_if_a_new_Object_Notice_can_be_created_from_Service()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Ancillary")
                .OpenOption("Skips")
                .SwitchNewIFrame();
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);

            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(2);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            string description = "Test for Object Notice for Service";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Notice Type is required")
                .WaitUntilToastMessageInvisible("Notice Type is required");
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));
            objectNoticeTab.DoubleClickNewObjectNotice();
            objectNoticeTab
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear(false);

            string startDate = londonCurrentDate.AddDays(3).ToString("dd/MM/yyyy");
            objectNoticeForm.ClearInputValue(objectNoticeForm.StartDateInput);
            objectNoticeForm.SendKeys(objectNoticeForm.StartDateInput, startDate);
            objectNoticeForm.ClickOnElement(objectNoticeForm.EndDateInput);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "INACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, startDate);

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            finder.IsObjectNoticeExist("67", objectNoticeTab.GetIdNewObjectNotice());
        }

        [Category("Verify if a new Object Notice can be created from Round Groups")]
        [Category("Huong")]
        [Test]
        public void TC_123_5_Verify_if_a_new_Object_Notice_can_be_created_from_Round_Groups()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .ExpandOption("Round Groups")
                .OpenOption("SKIP1")
                .SwitchNewIFrame();
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);

            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(2);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            string description = "Test for Object Notice for Round Group";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Notice Type is required")
                .WaitUntilToastMessageInvisible("Notice Type is required");
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));
            objectNoticeTab.DoubleClickNewObjectNotice();
            objectNoticeTab
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear(false);

            string startDate = londonCurrentDate.AddDays(3).ToString("dd/MM/yyyy");
            objectNoticeForm.ClearInputValue(objectNoticeForm.StartDateInput);
            objectNoticeForm.SendKeys(objectNoticeForm.StartDateInput, startDate);
            objectNoticeForm.ClickOnElement(objectNoticeForm.EndDateInput);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "INACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, startDate);

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            finder.IsObjectNoticeExist("58", objectNoticeTab.GetIdNewObjectNotice());
        }

        [Category("Verify if a new Object Notice can be created from Round")]
        [Category("Huong")]
        [Test]
        public void TC_123_6_Verify_if_a_new_Object_Notice_can_be_created_from_Round()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .ExpandOption("Round Groups")
                .ExpandOption("SKIP1")
                .OpenOption("Monday")
                .SwitchNewIFrame();
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);

            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(2);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            string description = "Test for Object Notice for Round";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Notice Type is required")
                .WaitUntilToastMessageInvisible("Notice Type is required");
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));
            objectNoticeTab.DoubleClickNewObjectNotice();
            objectNoticeTab
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear(false);

            string startDate = londonCurrentDate.AddDays(3).ToString("dd/MM/yyyy");
            objectNoticeForm.ClearInputValue(objectNoticeForm.StartDateInput);
            objectNoticeForm.SendKeys(objectNoticeForm.StartDateInput, startDate);
            objectNoticeForm.ClickOnElement(objectNoticeForm.EndDateInput);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "INACTIVE")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, startDate);

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            finder.IsObjectNoticeExist("60", objectNoticeTab.GetIdNewObjectNotice());
        }

        [Category("Verify if a new Object Notice can be created from Round Instance")]
        [Category("Huong")]
        [Test]
        public void TC_123_7_Verify_if_a_new_Object_Notice_can_be_created_from_Round_Instance()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .ExpandOption("Round Groups")
                .ExpandOption("SKIP1")
                .OpenOption("Monday")
                .SwitchNewIFrame();

            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear(false);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            DateTime startDateCalendar = londonCurrentDate.AddDays(7);
            DateTime endDateCalendar = londonCurrentDate.AddYears(1);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickCalendarTab()
                .DoubleClickRoundGroup(startDateCalendar, endDateCalendar, new List<DayOfWeek>() { DayOfWeek.Monday })
                .SwitchToChildWindow(2);

            PageFactoryManager.Get<RoundInstancePage>()
                .WaitForLoadingIconToDisappear(false);
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);

            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(3);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            string description = "Test for Object Notice for Round Instance";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Notice Type is required")
                .WaitUntilToastMessageInvisible("Notice Type is required");
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "ACTIVE")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);

            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));
            objectNoticeTab.DoubleClickNewObjectNotice();
            objectNoticeTab
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear(false);

            string startDate = londonCurrentDate.AddDays(3).ToString("dd/MM/yyyy");
            objectNoticeForm.ClearInputValue(objectNoticeForm.StartDateInput);
            objectNoticeForm.SendKeys(objectNoticeForm.StartDateInput, startDate);
            objectNoticeForm.ClickOnElement(objectNoticeForm.EndDateInput);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            objectNoticeForm
                .VerifyElementText(objectNoticeForm.HeaderStatus, "INACTIVE")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, startDate);

            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            finder.IsObjectNoticeExist("135", objectNoticeTab.GetIdNewObjectNotice());
        }

        [Category("ObjectNotice")]
        [Category("Huong")]
        [Test(Description = "Verify that object notice form works correctly")]
        public void TC_188_Object_notice_issues()
        {
            PageFactoryManager.Get<LoginPage>()
                  .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Ancillary")
                .SwitchNewIFrame();
            ObjectNoticeTab objectNoticeTab = PageFactoryManager.Get<ObjectNoticeTab>();
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            objectNoticeTab.ClickOnElement(objectNoticeTab.objectNoticeTab);
            objectNoticeTab.WaitForLoadingIconToDisappear(false);
            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToChildWindow(2);

            ObjectNoticeFormPage objectNoticeForm = PageFactoryManager.Get<ObjectNoticeFormPage>();
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            objectNoticeForm.VerifyInputValue(objectNoticeForm.StartDateInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            string description = "Test for Object Notice";
            string system = "Echo OnBoard";
            objectNoticeForm
                .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system)
                .SendKeys(objectNoticeForm.DescriptionText, description);
            objectNoticeForm.SelectTextFromDropDown(objectNoticeForm.StandardNoticeTypeSelect, "Round Finish notice");
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            objectNoticeForm
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            //Refresh the grid 
            objectNoticeTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear(false);
            objectNoticeTab.VerifyNewObjectNotice(description, system, londonCurrentDate.ToString("dd/MM/yyyy"));

            //Hard refresh the form
            objectNoticeTab.SwitchToChildWindow(2);
            objectNoticeForm.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            objectNoticeForm.VerifySelectedValue(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .VerifySelectedValue(objectNoticeForm.SystemSelect, system)
                .VerifySelectedValue(objectNoticeForm.StandardNoticeTypeSelect, "Round Finish notice")
                .VerifyInputValue(objectNoticeForm.DescriptionText, description);

            //Update use standard notice field -> Save -> Hard refresh the form
            objectNoticeForm.SelectTextFromDropDown(objectNoticeForm.StandardNoticeTypeSelect, "Round Start notice");
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            objectNoticeForm.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            objectNoticeForm.VerifySelectedValue(objectNoticeForm.StandardNoticeTypeSelect, "Round Start notice");

            //Update use standard notice field to blank -> Save -> Hard refresh the form
            objectNoticeForm.SelectTextFromDropDown(objectNoticeForm.StandardNoticeTypeSelect, "");
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            objectNoticeForm.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            objectNoticeForm.VerifySelectedValue(objectNoticeForm.StandardNoticeTypeSelect, "");

            //Add new item -> Select notice type and system -> Save -> Hard refresh the form
            objectNoticeForm.ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            objectNoticeForm
               .WaitForLoadingIconToDisappear(false);
            objectNoticeForm
                .SelectTextFromDropDown(objectNoticeForm.NoticeTypeSelect, "OnBoard")
                .SelectTextFromDropDown(objectNoticeForm.SystemSelect, system);
            objectNoticeForm
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            objectNoticeForm.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            objectNoticeForm.VerifySelectedValue(objectNoticeForm.StandardNoticeTypeSelect, "");
        }
    }
}
