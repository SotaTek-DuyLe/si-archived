using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundSchedulePage : BasePageCommonActions
    {
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By ScheduleTab = By.XPath("//a[@aria-controls='schedule-tab']");
        public readonly By StartDateInput = By.XPath("//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//input[@id='endDate.id']");
        public readonly By SeasonSelect = By.XPath("//select[@id='season.id']");
        private readonly By periodTimeButtons = By.XPath("//div[@id='schedule-tab']//div[@role='group']//button");
        public readonly By weeklyFrequencySelect = By.XPath("//div[@id='schedule-tab']//select[@id='weekly-frequency']");
        public readonly By RoundScheduleTitle = By.XPath("//h5[@data-bind='text: roundSchedule']");
        public readonly By RoundScheduleStatus = By.XPath("//h5[@id='header-status']//span[contains(@data-bind, \"visible: displayedStatusType() === 'status'\")]");
        public readonly By RetireButton = By.XPath("//button[@title='Retire']");
        public readonly By OKButton = By.XPath("//div[@class='modal-dialog']//button[contains(string(), 'OK')]");
        public readonly By RetireConfirmTitle = By.XPath("//div[@class='modal-dialog']//h4");

        public RoundSchedulePage ClickPeriodTimeButton(string period)
        {
            IWebElement webElement = GetAllElements(periodTimeButtons).FirstOrDefault(x => x.Text.Contains(period));
            ClickOnElement(webElement);
            return this;
        }

        public RoundSchedulePage ClickDayButtonOnWeekly(string day)
        {
            string xpath = $"//div[@id='schedule-tab']//div[contains(@data-bind, 'foreach: dayButtons')]//button[contains(string(), '{day}')]";
            ClickOnElement(By.XPath(xpath));
            Thread.Sleep(200);
            return this;
        }
    }
}
