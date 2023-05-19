using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceCommonPage : BasePageCommonActions
    {
        private readonly By dropdown = By.XPath("//li[@class='dropdown']");
        private readonly By invoiceScheduleTab = By.XPath("//ul[@class='dropdown-menu']//a[@aria-controls='invoiceSchedules-tab']");
        private readonly By invoiceScheduleTabInLargeView = By.XPath("//li[@role='presentation']//a[@aria-controls='invoiceSchedules-tab']");
        #region Invoice Schedule tab
        public readonly By AddNewInvoiceSchedule = By.XPath("//div[@id='invoiceSchedules-tab']//button[contains(string(), 'Add New Item')]");
        #endregion

        [AllureStep]
        public ServiceCommonPage ClickTabDropDown()
        {
            ClickOnElement(dropdown);
            return this;
        }

        [AllureStep]
        public ServiceCommonPage ClickInvoiceScheduleTab()
        {
            if (IsControlDisplayed(invoiceScheduleTabInLargeView))
            {
                ClickOnElement(invoiceScheduleTabInLargeView);
            }
            else
            {
                ClickTabDropDown();
                ClickOnElement(invoiceScheduleTab);
            }
            return this;
        }
    }
}
