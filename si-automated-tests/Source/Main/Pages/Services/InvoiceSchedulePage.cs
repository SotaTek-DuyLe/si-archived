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
    public class InvoiceSchedulePage : BasePageCommonActions
    {
        public readonly By NameInput = By.XPath("//input[contains(@data-bind,'name.value')]");
        public readonly By ContractSelect = By.XPath("//select[@id='contract.id']");
        public readonly By YearButtons = By.XPath("//button[text()='Year']");
        public readonly By CustomScheduleDescription = By.Id("CustomScheduleName");
        public readonly By SetRegularCustomScheduleButton = By.XPath("//button[contains(@data-bind, 'regularCustomScheduleClick')]");
        public readonly By SetCustomScheduleButton = By.XPath("//button[contains(@data-bind, 'click: CustomScheduleClick')]");

        #region Regular custom schedule
        public readonly By PatternNoWeekInput = By.XPath("//input[@id='selectedWeek']");
        public readonly By EffectiveDateInput = By.XPath("//input[@id='effective-date']");
        public readonly By PatternEndDateInput = By.XPath("//input[@id='pattern-endDate']");
        public readonly By CustomScheduleNameTextarea = By.XPath("//textarea[@id='customScheduleName']");
        public readonly By ConfirmSetRegularScheduleButton = By.XPath("//button[@data-bind='click: generateWeekPatternClick']");
        public readonly By CancelSetRegularScheduleButton = By.XPath("//div[@id='regular-custom-schedule-modal']//button[@data-bind='click: clear' and text()='Cancel']");
        public readonly By ApplyRegCustomScheduleButton = By.XPath("//button[contains(@data-bind, 'click:ApplyRegCustomSchedule')]");
        public readonly By ClearRegCustomScheduleButton = By.XPath("//button[contains(@data-bind, 'click: clearSelection')]");

        public InvoiceSchedulePage VerifyEffectiveDate(int weeks, DateTime effectiveDate)
        {
            var days = GetAllElements(By.XPath("//div[@id='weekly-schedule']//td[contains(@data-bind, 'click: $parent.selectDate')]//span")).Select(x => x.Text);
            for (int i = 0; i < weeks; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Assert.IsTrue(days.Contains(effectiveDate.ToString("dd MMM")));
                    effectiveDate = effectiveDate.AddDays(1);
                }
            }
            return this;
        }

        public InvoiceSchedulePage VerifyEffectiveDate(int weeks, DateTime effectiveDate, DateTime endDate)
        {
            var days = GetAllElements(By.XPath("//div[@id='weekly-schedule']//td[contains(@data-bind, 'click: $parent.selectDate')]//span")).Select(x => x.Text).ToList();
            for (int i = 0; i < weeks; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Assert.IsTrue(days.Contains(effectiveDate.ToString("dd MMM")));
                    effectiveDate = effectiveDate.AddDays(1);
                    if (endDate < effectiveDate) break;
                }
            }
            return this;
        }

        public InvoiceSchedulePage ClickScheduleDate(string date)
        {
            var days = GetAllElements(By.XPath("//div[@id='weekly-schedule']//td[contains(@data-bind, 'click: $parent.selectDate')]//span"));
            var day = days.FirstOrDefault(x => x.Text == date);
            day.Click();
            SleepTimeInMiliseconds(300);
            return this;
        }

        public InvoiceSchedulePage VerifyScheduleDateIsSelected(string date)
        {
            var days = GetAllElements(By.XPath("//div[@id='weekly-schedule']//td[contains(@data-bind, 'click: $parent.selectDate')]"));
            var day = days.FirstOrDefault(x => x.Text == date);
            Assert.IsTrue(day.GetCssValue("background-color") == "rgba(255, 255, 0, 1)");
            return this;
        }

        public InvoiceSchedulePage VerifyScheduleDateIsDeselected(string date)
        {
            var days = GetAllElements(By.XPath("//div[@id='weekly-schedule']//td[contains(@data-bind, 'click: $parent.selectDate')]"));
            var day = days.FirstOrDefault(x => x.Text == date);
            Assert.IsFalse(day.GetCssValue("background-color") == "rgba(255, 255, 0, 1)");
            return this;
        }
        #endregion

        #region Custom schedule with date
        public readonly By CustomScheduleDescription2 = By.Id("scheduleName");
        public readonly By ApplyScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[text()='Apply']");
        public readonly By ResetScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[text()='Reset']");
        public readonly By CancelScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[text()='Cancel']");
        public readonly By CloseScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[@title='Close']");

        public InvoiceSchedulePage SetScheduleDate(string date)
        {
            string xpath = $"//div[@id='custom-schedule-dates-modal']//table//tbody//td[@data-date='{date}']";
            ClickOnElement(By.XPath(xpath));
            return this;
        }

        public InvoiceSchedulePage VerifyDateIsDeselected(string date)
        {
            string xpath = $"//div[@id='custom-schedule-dates-modal']//table//tbody//td[@data-date='{date}']";
            IWebElement cell = GetElement(xpath);
            Console.WriteLine(cell.GetCssValue("background-color"));
            Assert.IsTrue(cell.GetCssValue("background-color") != "rgba(58, 135, 173, 1)");
            return this;
        }

        public InvoiceSchedulePage VerifyDateIsSelected(string date)
        {
            string xpath = $"//div[@id='custom-schedule-dates-modal']//table//tbody//td[@data-date='{date}']";
            IWebElement cell = GetElement(xpath);
            Console.WriteLine(cell.GetCssValue("background-color"));
            Assert.IsTrue(cell.GetCssValue("background-color") == "rgba(58, 135, 173, 1)");
            return this;
        }
        #endregion

        public InvoiceSchedulePage ClickYearButton()
        {
            this.driver.FindElements(YearButtons).FirstOrDefault(x => x.Displayed).Click();
            return this;
        }

        public By SchedulePicker(string schedule)
        {
            return By.XPath($"//div[contains(@data-bind, 'schedulePicker')]//button//span[text()='{schedule}']//parent::button");
        }
    }
}
