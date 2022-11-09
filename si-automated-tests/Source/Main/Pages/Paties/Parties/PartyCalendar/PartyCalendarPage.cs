using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using CanlendarServiceTask = si_automated_tests.Source.Main.Models.Suspension.ServiceTaskModel;
namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar
{
    public class PartyCalendarPage : BasePage
    {
        private readonly By comboboxInCalendars = By.XPath("//div[@id='calendar-tab']//button[@data-toggle='dropdown']");
        private readonly By selectAllSitesBtn = By.XPath("//div[not(contains(@style,'display: none;')) and contains(@class, 'bs-container')]//button[text()='Select All']");
        private readonly By applyBtn = By.XPath("//div[@id='calendar-tab']//button[text()='Apply']");
        private readonly By rowsCalendarTableInMonth = By.XPath("//div[@class='fc-content-skeleton']//table//tbody//tr");
        private readonly By nextCalendarBtn = By.XPath("//div[@class='fc-left']//button[contains(@class,'fc-next-button')]");
        public readonly By LastMonthBtn = By.XPath("//button[@title='Last month']");
        public readonly By CalendarTitle = By.XPath("//h2[text()='August 2022']");
        public readonly By ServicesDropdownButton = By.XPath("//div[@id='calendar-tab']//button[@data-id='services-list']");
        public readonly By ProductDropdownButton = By.XPath("//div[@id='calendar-tab']//button[@data-id='products-list']");

        public CalendarElement PartyCalendar
        {
            get => new CalendarElement("//div[contains(@class, 'fc-month-view')]", "./div[contains(@class, 'fc-bg')]//table//tbody//tr//td", "//div[contains(@class, 'fc-week')]", "./div[contains(@class, 'fc-content-skeleton')]//table//tbody//tr//td");
        }

        [AllureStep]
        public PartyCalendarPage GoToAugust()
        {
            while (!IsControlDisplayedNotThrowEx(CalendarTitle))
            {
                ClickOnElement(LastMonthBtn);
                WaitForLoadingIconToDisappear();
            }
            return this;
        }

        [AllureStep]
        public PartyCalendarPage ClickDayInstance(DateTime date)
        {
            var dayElement = PartyCalendar.GetDay(date);
            ClickOnElement(dayElement.Contents.FirstOrDefault());
            return this;
        }
        [AllureStep]
        public PartyCalendarPage VerifyDayInstanceHasRaiseHandStatus(DateTime date, bool isContain)
        {
            var dayElement = PartyCalendar.GetDay(date);
            var content = dayElement.Contents.FirstOrDefault();
            IWebElement innerContent = content.FindElement(By.XPath("./a"));
            if (isContain)
            {
                Assert.IsTrue(innerContent.GetCssValue("background-image").Contains("task-onhold.png"));
            }
            else
            {
                Assert.IsFalse(innerContent.GetCssValue("background-image").Contains("task-onhold.png"));
            }
            return this;
        }
        [AllureStep]
        public PartyCalendarPage ClickSiteCombobox()
        {
            IWebElement siteCombobox = GetAllElements(comboboxInCalendars).FirstOrDefault();
            ClickOnElement(siteCombobox);
            return this;
        }
        [AllureStep]
        public PartyCalendarPage ClickSellectAllSites()
        {
            WaitUtil.WaitForElementVisible(selectAllSitesBtn);
            ClickOnElement(selectAllSitesBtn);
            return this;
        }
        [AllureStep]
        public PartyCalendarPage ClickServiceCombobox()
        {
            IWebElement serviceCombobox = GetAllElements(comboboxInCalendars)[1];
            ClickOnElement(serviceCombobox);
            return this;
        }
        [AllureStep]
        public PartyCalendarPage ClickSellectAllServices()
        {
            WaitUtil.WaitForElementVisible(selectAllSitesBtn);
            ClickOnElement(selectAllSitesBtn);
            return this;
        }
        [AllureStep]
        public PartyCalendarPage ClickApplyCalendarButton()
        {
            ClickOnElement(applyBtn);
            return this;
        }
        [AllureStep]
        public List<CanlendarServiceTask> GetAllDataInMonth(DateTime fromDateTime, DateTime toDateTime)
        {
            List<CanlendarServiceTask> serviceTasks = new List<CanlendarServiceTask>();
            int months = ((toDateTime.Year - fromDateTime.Year) * 12) + toDateTime.Month - fromDateTime.Month;
            int step = 0;
            int diffMonths = ((fromDateTime.Year - DateTime.Now.Year) * 12) + fromDateTime.Month - DateTime.Now.Month;
            while (diffMonths > 0)
            {
                ClickOnElement(nextCalendarBtn);
                WaitForLoadingIconToDisappear();
                Thread.Sleep(1000);
                diffMonths--;
            }
            while (step <= months)
            {
                if (step > 0)
                {
                    ClickOnElement(nextCalendarBtn);
                    WaitForLoadingIconToDisappear();
                }
                step++;
                Thread.Sleep(1000);
                var allWeeks = PartyCalendar.GetWeeks();
                var allDays = new List<DayElement>();
                foreach (var week in allWeeks)
                {
                    allDays.AddRange(week.Days);
                }
                foreach (var dayInstance in allDays)
                {
                    foreach (var contentEle in dayInstance.Contents)
                    {
                        List<IWebElement> details = contentEle.FindElements(By.XPath("./a")).ToList();
                        if (details.Count > 0)
                        {
                            CanlendarServiceTask serviceTask = new CanlendarServiceTask();
                            serviceTask.Date = dayInstance.Date;
                            serviceTask.Content = GetElementText(details[0]);
                            serviceTask.ImagePath = details[0].GetCssValue("background");
                            serviceTasks.Add(serviceTask);
                        }
                    }
                }
            }
            return serviceTasks;
        }
    }
}
