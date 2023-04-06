using System;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class ShiftSchedulePage : BasePageCommonActions
    {
        public readonly By ShiftDropdown = By.XPath("//button[@data-id='shift']");
        public readonly By ShiftMenu = By.XPath("//ul[@aria-expanded='true']");
        public readonly By EndDateInput = By.XPath("//input[@id='end-date']");
        public readonly By CustomButton = By.XPath("//button//span[text()='Custom']//parent::button");
        private readonly By CustomYearButton = By.XPath("//button[text()='Year']");
        public readonly By YearDatePicker = By.XPath("//div[@data-bind='yearDatePicker: customScheduleDays']//table//tbody");
        public readonly By SetRegularCustomScheduleButton = By.XPath("//button[text()='Set Regular Custom Schedule']");
        public readonly By SetCustomScheduleDatesButton = By.XPath("//button[text()='Set Custom Schedule with dates']");

        #region Regular custom schedule 
        public readonly By RegularCustomScheduleForm = By.Id("regular-custom-schedule-modal");
        public readonly By PatternWeekInput = By.XPath("//div[@id='regular-custom-schedule-modal']//input[@id='selectedWeek']");
        public readonly By EffectiveDateInput = By.XPath("//div[@id='regular-custom-schedule-modal']//input[@id='effective-date']");
        public readonly By PatternEndDateInput = By.XPath("//div[@id='regular-custom-schedule-modal']//input[@id='pattern-endDate']");
        public readonly By CustomScheduleDescriptionInput = By.XPath("//div[@id='regular-custom-schedule-modal']//textarea[@id='customScheduleName']");
        public readonly By ConfirmCustomScheduleButton = By.XPath("//div[@id='regular-custom-schedule-modal']//button[@data-bind='click: generateWeekPatternClick']");
        public readonly By CancelCustomScheduleButton = By.XPath("//div[@id='regular-custom-schedule-modal']//button[@data-bind='click: clear']");
        public readonly By ApplyCustomScheduleButton = By.XPath("//div[@id='regular-custom-schedule-modal']//button[@data-bind='click:ApplyRegCustomSchedule,disable:isApply()']");
        public readonly By ClearCustomScheduleButton = By.XPath("//div[@id='regular-custom-schedule-modal']//button[@data-bind='click: clearSelection']");
        private readonly By DayInPatternWeeks = By.XPath("//div[@id='regular-custom-schedule-modal']//div[@id='weekly-schedule']//tbody//td[not(@class) and @data-bind]");

        public ShiftSchedulePage VerifyPatternOfWeekInWeek(DateTime effectiveDate, int week)
        {
            var days = GetAllElements(DayInPatternWeeks);
            int totalDay = week * 7;
            Assert.IsTrue(days.Count == totalDay);
            for (int i = 0; i < totalDay; i++)
            {
                Assert.IsTrue(days[i].Text == effectiveDate.AddDays(i).ToString("dd MMM"));
            }
            return this;
        }

        public ShiftSchedulePage ClickScheduleDate(DateTime date)
        {
            var days = GetAllElements(DayInPatternWeeks);
            foreach (var item in days)
            {
                if (item.Text == date.ToString("dd MMM"))
                {
                    item.Click();
                    break;
                }
            }
            return this;
        }

        public ShiftSchedulePage VerifySelectedScheduleDate(DateTime date, bool isSelected)
        {
            var days = GetAllElements(DayInPatternWeeks);
            foreach (var item in days)
            {
                if (item.Text == date.ToString("dd MMM"))
                {
                    string backgroundColor = item.GetCssValue("background-color");
                    if (isSelected)
                    {
                        Assert.IsTrue(backgroundColor == "rgba(255, 255, 0, 1)");
                    }
                    else
                    {
                        Assert.IsFalse(backgroundColor == "rgba(255, 255, 0, 1)");
                    }
                    break;
                }
            }
            return this;
        }
        #endregion

        #region Custom schedule with dates
        public readonly By CustomScheduleModal = By.XPath("//div[@id='custom-schedule-dates-modal']");
        public readonly By CustomScheduleWithDateDescriptionInput = By.XPath("//div[@id='custom-schedule-dates-modal']//textarea");
        public readonly By ApplyCustomScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[text()='Apply']");
        public readonly By ResetCustomScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[text()='Reset']");
        public readonly By CancelCustomScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[text()='Cancel']");
        private readonly By datesInYear = By.XPath("//div[@id='custom-schedule-dates-modal']//tbody//td[@data-date]");
        public readonly By CloseCustomScheduleWithDateButton = By.XPath("//div[@id='custom-schedule-dates-modal']//button[@title='Close']");

        public ShiftSchedulePage ClickCustomSchedule(DateTime date)
        {
            var dateEles = this.driver.FindElements(datesInYear);
            string dateAsString = date.ToString("yyyy-MM-dd");
            foreach (var item in dateEles)
            {
                string dataDate = item.GetAttribute("data-date");
                if (dataDate == dateAsString)
                {
                    item.Click();
                    break;
                }
            }
            return this;
        }

        public ShiftSchedulePage VerifyCustomSchedule(DateTime date, bool isSelected)
        {
            var dateEles = this.driver.FindElements(datesInYear);
            string dateAsString = date.ToString("yyyy-MM-dd");
            foreach (var item in dateEles)
            {
                string dataDate = item.GetAttribute("data-date");
                if (dataDate == dateAsString)
                {
                    string backgroundColor = item.GetCssValue("background-color");
                    if (isSelected)
                    {
                        Assert.IsTrue(backgroundColor == "rgba(58, 135, 173, 1)");
                    }
                    else
                    {
                        Assert.IsFalse(backgroundColor == "rgba(58, 135, 173, 1)");
                    }
                    break;
                }
            }
            return this;
        }
        #endregion

        public ShiftSchedulePage ClickYearButton()
        {
            ClickOnElement(this.driver.FindElements(CustomYearButton).ToList().FirstOrDefault(x => x.Displayed));
            return this;
        }
    }
}
