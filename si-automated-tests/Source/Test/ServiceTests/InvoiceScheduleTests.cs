using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Common;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Threading;
using System.Linq;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class InvoiceScheduleTests : BaseTest
    {
        [Category("Invoice")]
        [Category("Huong")]
        [Test]
        public void TC_302_Custom_Invoice_Schedules_Set_Regular_Custom_Schedule()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser61.UserName, AutoUser61.Password)
                .IsOnHomePage(AutoUser61);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            ServiceCommonPage serviceCommonPage = PageFactoryManager.Get<ServiceCommonPage>();
            serviceCommonPage.WaitForLoadingIconToDisappear();
            serviceCommonPage.ClickInvoiceScheduleTab()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            serviceCommonPage.ClickOnElement(serviceCommonPage.AddNewInvoiceSchedule);
            serviceCommonPage.SwitchToChildWindow(2);
            InvoiceSchedulePage invoiceSchedulePage = PageFactoryManager.Get<InvoiceSchedulePage>();
            invoiceSchedulePage.WaitForLoadingIconToDisappear();
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.NameInput, "Custom schedule Regular")
                .ClickOnElement(invoiceSchedulePage.SchedulePicker("Custom"));
            invoiceSchedulePage.SleepTimeInMiliseconds(500);
            invoiceSchedulePage.ClickYearButton();
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.CustomScheduleDescription, false)
                .VerifyElementEnable(invoiceSchedulePage.SetRegularCustomScheduleButton, false)
                .VerifyElementEnable(invoiceSchedulePage.SetCustomScheduleButton, false);
            invoiceSchedulePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.ContractSelect, false)
                .VerifyElementEnable(invoiceSchedulePage.SetRegularCustomScheduleButton, true)
                .VerifyElementEnable(invoiceSchedulePage.SetCustomScheduleButton, true);

            //Click on 'Set Regular Custom Schedule'
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetRegularCustomScheduleButton);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            invoiceSchedulePage.VerifyInputValue(invoiceSchedulePage.PatternNoWeekInput, "1")
                .VerifyInputValue(invoiceSchedulePage.EffectiveDateInput, londonCurrentDate.AddDays(1).ToString("dd/MM/yyyy"))
                .VerifyInputValue(invoiceSchedulePage.PatternEndDateInput, "01/01/2050")
                .VerifyElementIsMandatory(invoiceSchedulePage.CustomScheduleNameTextarea, true);

            //Click 'Confirm' button
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ConfirmSetRegularScheduleButton);
            invoiceSchedulePage.VerifyEffectiveDate(1, londonCurrentDate.AddDays(1));
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.ClearRegCustomScheduleButton, true)
                .VerifyElementEnable(invoiceSchedulePage.ApplyRegCustomScheduleButton, false);

            //Select any dates in calendar and click 'Apply'
            invoiceSchedulePage.ClickScheduleDate(londonCurrentDate.AddDays(1).ToString("dd MMM"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ApplyRegCustomScheduleButton);
            invoiceSchedulePage.VerifyToastMessage("Custom Schedule Description is required");
            invoiceSchedulePage.WaitUntilToastMessageInvisible("Custom Schedule Description is required");

            ///In Pop up, set following:
            //Pattern Num of Weeks = 3
            //Effective Date = tomorrow's date
            //Pattern End Date = tomorrow + 5 months
            //Custom Schedule Description
            //Click 'Confirm'
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.PatternNoWeekInput, "3");
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.EffectiveDateInput);
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.PatternEndDateInput, londonCurrentDate.AddMonths(5).ToString("dd/MM/yyyy"))
                .SetInputValue(invoiceSchedulePage.CustomScheduleNameTextarea, "Custom description");
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ConfirmSetRegularScheduleButton);
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.ClearRegCustomScheduleButton, true)
               .VerifyElementEnable(invoiceSchedulePage.ApplyRegCustomScheduleButton, false);
            invoiceSchedulePage.VerifyEffectiveDate(3, londonCurrentDate.AddDays(1), londonCurrentDate.AddMonths(5));

            //Select days in each week in the table
            invoiceSchedulePage.ClickScheduleDate(londonCurrentDate.AddDays(1).ToString("dd MMM"))
                .VerifyScheduleDateIsSelected(londonCurrentDate.AddDays(1).ToString("dd MMM"));

            //Click 'Clear' button
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ClearRegCustomScheduleButton);
            invoiceSchedulePage.SleepTimeInMiliseconds(300);
            invoiceSchedulePage.VerifyScheduleDateIsDeselected(londonCurrentDate.AddDays(1).ToString("dd MMM"));

            //Click 'Cancel' button
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.CancelSetRegularScheduleButton);
            invoiceSchedulePage.WaitForLoadingIconToDisappear();
            invoiceSchedulePage.VerifyElementVisibility(invoiceSchedulePage.CancelSetRegularScheduleButton, false);

            //Click again on 'Set Regular Custom Schedule'
            /// In pop up, set:
            //Pattern Num of Weeks = 2
            //Effective Date = tomorrow's date
            //Pattern End Date = tomorrow + 2 months
            //Custom Schedule Description
            //Click 'Confirm'
            //Select days in each week in the table
            //Click 'Apply'
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetRegularCustomScheduleButton);
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.PatternNoWeekInput, "2");
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.EffectiveDateInput);
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.PatternEndDateInput, londonCurrentDate.AddDays(1).AddMonths(2).ToString("dd/MM/yyyy"))
                .SetInputValue(invoiceSchedulePage.CustomScheduleNameTextarea, "Custom description");
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ConfirmSetRegularScheduleButton);
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.ClearRegCustomScheduleButton, true)
               .VerifyElementEnable(invoiceSchedulePage.ApplyRegCustomScheduleButton, false);
            invoiceSchedulePage.SleepTimeInMiliseconds(1000);
            invoiceSchedulePage.VerifyEffectiveDate(2, londonCurrentDate.AddDays(1), londonCurrentDate.AddDays(1).AddMonths(2));

            //Select days in each week in the table
            invoiceSchedulePage.ClickScheduleDate(londonCurrentDate.AddDays(1).ToString("dd MMM"))
                .VerifyScheduleDateIsSelected(londonCurrentDate.AddDays(1).ToString("dd MMM"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ApplyRegCustomScheduleButton);
            invoiceSchedulePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);

            string url = invoiceSchedulePage.GetCurrentUrl();
            string[] tempUrls = url.Split('/');
            string id = tempUrls[tempUrls.Length - 1];
            CommonFinder finder = new CommonFinder(DbContext);
            var invoiceSchedule = finder.GetInvoiceSchedule(id);
            var scheduleDates = finder.GetScheduleDateModel(invoiceSchedule.scheduleID);
            Assert.IsNull(scheduleDates.FirstOrDefault(x => x.scheduledate < londonCurrentDate.GetDateZeroTime().AddDays(1) || x.scheduledate > londonCurrentDate.GetDateZeroTime().AddDays(1).AddMonths(2)));
        }

        [Category("Invoice")]
        [Category("Huong")]
        [Test]
        public void TC_303_Custom_Invoice_Schedules_Set_Custom_Schedule_with_dates()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser61.UserName, AutoUser61.Password)
                .IsOnHomePage(AutoUser61);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            ServiceCommonPage serviceCommonPage = PageFactoryManager.Get<ServiceCommonPage>();
            serviceCommonPage.WaitForLoadingIconToDisappear();
            serviceCommonPage.ClickInvoiceScheduleTab()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            serviceCommonPage.ClickOnElement(serviceCommonPage.AddNewInvoiceSchedule);
            serviceCommonPage.SwitchToChildWindow(2);
            InvoiceSchedulePage invoiceSchedulePage = PageFactoryManager.Get<InvoiceSchedulePage>();
            invoiceSchedulePage.WaitForLoadingIconToDisappear();
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.NameInput, "Custom schedule with dates")
                .ClickOnElement(invoiceSchedulePage.SchedulePicker("Custom"));
            invoiceSchedulePage.SleepTimeInMiliseconds(500);
            invoiceSchedulePage.ClickYearButton();
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.CustomScheduleDescription, false)
                .VerifyElementEnable(invoiceSchedulePage.SetRegularCustomScheduleButton, false)
                .VerifyElementEnable(invoiceSchedulePage.SetCustomScheduleButton, false);
            invoiceSchedulePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.ContractSelect, false)
                .VerifyElementEnable(invoiceSchedulePage.SetRegularCustomScheduleButton, true)
                .VerifyElementEnable(invoiceSchedulePage.SetCustomScheduleButton, true);

            //Click on 'Set Custom Schedule with dates'
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetCustomScheduleButton);
            invoiceSchedulePage.VerifyElementIsMandatory(invoiceSchedulePage.CustomScheduleDescription2, true);
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.ApplyScheduleWithDateButton, false);
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.ResetScheduleWithDateButton, true);
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.CancelScheduleWithDateButton, true);
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.CustomScheduleDescription2, "random custom dates");

            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            invoiceSchedulePage.SetScheduleDate(londonCurrentDate.AddDays(1).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ApplyScheduleWithDateButton);
            invoiceSchedulePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            string url = invoiceSchedulePage.GetCurrentUrl();
            string[] tempUrls = url.Split('/');
            string id = tempUrls[tempUrls.Length - 1];
            CommonFinder finder = new CommonFinder(DbContext);
            var invoiceSchedule = finder.GetInvoiceSchedule(id);
            var scheduleDates = finder.GetScheduleDateModel(invoiceSchedule.scheduleID);
            Assert.IsNull(scheduleDates.FirstOrDefault(x => x.scheduledate < londonCurrentDate.GetDateZeroTime().AddDays(1)));

            //On the Invoice Scheudle click again 'Set Custom Schedule with dates'
            //In the pop up, select more dates and edit Description
            //Click 'Reset'
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetCustomScheduleButton);
            invoiceSchedulePage.SetScheduleDate(londonCurrentDate.AddDays(2).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.SetScheduleDate(londonCurrentDate.AddDays(3).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.ResetScheduleWithDateButton);
            invoiceSchedulePage.SleepTimeInMiliseconds(500);
            invoiceSchedulePage.VerifyDateIsDeselected(londonCurrentDate.AddDays(2).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.VerifyDateIsDeselected(londonCurrentDate.AddDays(3).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.CloseScheduleWithDateButton);
            invoiceSchedulePage.SleepTimeInMiliseconds(500);

            //On the Invoice Scheudle click again 'Set Custom Schedule with dates'
            //In the pop up, select more dates and edit Description
            //Click 'Cancel'
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetCustomScheduleButton);
            invoiceSchedulePage.SetScheduleDate(londonCurrentDate.AddDays(2).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.SetScheduleDate(londonCurrentDate.AddDays(3).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.CancelScheduleWithDateButton);
            invoiceSchedulePage.SleepTimeInMiliseconds(500);
            invoiceSchedulePage.VerifyElementVisibility(invoiceSchedulePage.CancelScheduleWithDateButton, false);

            //On the Invoice Scheudle click again 'Set Custom Schedule with dates'
            //dang co bug
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetCustomScheduleButton);
            invoiceSchedulePage.VerifyDateIsDeselected(londonCurrentDate.AddDays(2).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.VerifyDateIsDeselected(londonCurrentDate.AddDays(3).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.CloseScheduleWithDateButton);
            //In the pop up, select more dates and edit Description
            //Click 'x'
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetCustomScheduleButton);
            invoiceSchedulePage.SetScheduleDate(londonCurrentDate.AddDays(2).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.SetScheduleDate(londonCurrentDate.AddDays(3).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.CloseScheduleWithDateButton);
            invoiceSchedulePage.SleepTimeInMiliseconds(500);
            invoiceSchedulePage.ClickOnElement(invoiceSchedulePage.SetCustomScheduleButton);
            invoiceSchedulePage.VerifyDateIsDeselected(londonCurrentDate.AddDays(2).ToString("yyyy-MM-dd"));
            invoiceSchedulePage.VerifyDateIsDeselected(londonCurrentDate.AddDays(3).ToString("yyyy-MM-dd"));
        }

        [Category("Invoice")]
        [Category("Huong")]
        [Test]
        public void TC_307_Create_an_Invoice_Schedule_from_Contract()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser61.UserName, AutoUser61.Password)
                .IsOnHomePage(AutoUser61);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            ServiceCommonPage serviceCommonPage = PageFactoryManager.Get<ServiceCommonPage>();
            serviceCommonPage.WaitForLoadingIconToDisappear();
            serviceCommonPage.ClickInvoiceScheduleTab()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            serviceCommonPage.ClickOnElement(serviceCommonPage.AddNewInvoiceSchedule);
            serviceCommonPage.SwitchToChildWindow(2);
            InvoiceSchedulePage invoiceSchedulePage = PageFactoryManager.Get<InvoiceSchedulePage>();
            invoiceSchedulePage.WaitForLoadingIconToDisappear();
            invoiceSchedulePage.SetInputValue(invoiceSchedulePage.NameInput, "Custom schedule with dates")
                .ClickOnElement(invoiceSchedulePage.SchedulePicker("Custom"));
            invoiceSchedulePage.SleepTimeInMiliseconds(500);
            invoiceSchedulePage.ClickYearButton();
            invoiceSchedulePage.VerifyElementEnable(invoiceSchedulePage.CustomScheduleDescription, false)
                .VerifyElementEnable(invoiceSchedulePage.SetRegularCustomScheduleButton, false)
                .VerifyElementEnable(invoiceSchedulePage.SetCustomScheduleButton, false);
            invoiceSchedulePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        }
    }
}
