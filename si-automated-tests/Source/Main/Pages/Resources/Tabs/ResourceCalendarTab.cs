using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Resources.Tabs
{
    public class ResourceCalendarTab : BasePage
    {
        private readonly By workPattern = By.XPath("//span[@class='fc-title']");
        //date view option when in Year tab:
        private readonly string dateViewOptionYear = "//div[@id='calendar-year']//button[text()='{0}']";
        private readonly string hiddenYearDateView = "//div[@class='calendar-year-component' and @style='display: none;']";
        //date view option when in other tab:
        private readonly string dateViewOptionOthers = "//div[contains(@data-bind,'echoCalendar')]//button[text()='{0}']";
        private readonly By todayInYearView = By.XPath("//td[contains(@class,'day today')]");

        public CalendarElement ResourceCalendar
        {
            get => new CalendarElement("//div[@id='calendar-tab']//div[contains(@class, 'fc-month-view')]", "./div[contains(@class, 'fc-bg')]//table//tbody//tr//td", "//div[contains(@class, 'fc-week')]", "./div[contains(@class, 'fc-content-skeleton')]//table//tbody//tr//td");
        }

        public ResourceCalendarTab ClickRoundIntansceDetail(int idx)
        {
            var roundInstances = GetAllDataInMonth().FindAll(x => !x.IsFontBold);
            roundInstances.ElementAtOrDefault(idx)?.WebElement.Click();
            return this;
        }

        [AllureStep]
        public List<CalendarResourceModel> GetAllDataInMonth()
        {
            List<CalendarResourceModel> resourceModels = new List<CalendarResourceModel>();
            var allWeeks = ResourceCalendar.GetWeeks();
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
                        CalendarResourceModel serviceTask = new CalendarResourceModel();
                        serviceTask.Date = dayInstance.Date;
                        serviceTask.WebElement = details[0];
                        serviceTask.Content = GetElementText(details[0]);
                        serviceTask.ImagePath = details[0].GetCssValue("background");
                        serviceTask.IsFontBold = details[0].GetAttribute("class").Contains("shift-instance");
                        resourceModels.Add(serviceTask);
                    }
                }
            }
            return resourceModels;
        }

        [AllureStep]
        public ResourceCalendarTab VerifyWorkPatternIsSet(string _pattern)
        {
            IList<IWebElement> patternList = WaitUtil.WaitForAllElementsVisible(workPattern);
            foreach(var pattern in patternList)
            {
                Assert.AreEqual(_pattern, pattern.Text);
            }
            return this;
        }
        [AllureStep]
        public ResourceCalendarTab VerifyWorkPatternNotSet()
        {
            WaitUtil.WaitForElementInvisible(workPattern);
            return this;
        }
        [AllureStep]
        public bool IsOnYearDateView()
        {
            //if hidden year view is found -> not on year view, not found -> on year view
            return IsControlUnDisplayed(hiddenYearDateView);
        }
        [AllureStep]
        public ResourceCalendarTab SwitchDateView(string viewOption)
        {
            //if is year view -> use other view and vice versa
            if (IsOnYearDateView())
            {
                ClickOnElement(dateViewOptionYear, viewOption);
            }
            else
            {
                ClickOnElement(dateViewOptionOthers, viewOption);
            }
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ResourceCalendarTab OpenTodayDateInYearView()
        {
            DoubleClickOnElement(todayInYearView);
            return this;
        }
    }
}
