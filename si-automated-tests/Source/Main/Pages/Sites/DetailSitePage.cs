using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using CanlendarServiceTask = si_automated_tests.Source.Main.Models.Suspension.ServiceTaskModel;

namespace si_automated_tests.Source.Main.Pages.Sites
{
    public class DetailSitePage : BasePage
    {
        private readonly By servicedSiteTitle = By.XPath("//span[text()='Serviced Site']");
        private readonly By title = By.XPath("//span[text()='Serviced Site']");
        private readonly By notesTab = By.CssSelector("a[aria-controls='notes-tab']");
        private readonly By nextCalendarBtn = By.XPath("//div[@class='fc-left']//button[contains(@class,'fc-next-button')]");
        private readonly By canlendarTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='calendar-tab']");
        private readonly By rowsCalendarTableInMonth = By.XPath("//div[@class='fc-content-skeleton']//table//tbody//tr");
        private readonly By titleInNotesTab = By.XPath("//label[text()='Title']/following-sibling::input");
        private readonly By noteInNotesTab = By.XPath("//label[text()='Note']/following-sibling::textarea");
        [AllureStep]
        public DetailSitePage WaitForSiteDetailPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(servicedSiteTitle);
            return this;
        }
        [AllureStep]
        public DetailSitePage VerifyCurrentUrlSitePage(string siteId)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/sites/" + siteId, GetCurrentUrl());
            return this;
        }
        public CalendarElement SiteCalendar
        {
            get => new CalendarElement("//div[contains(@class, 'fc-month-view')]", "./div[contains(@class, 'fc-bg')]//table//tbody//tr//td", "//div[contains(@class, 'fc-week')]", "./div[contains(@class, 'fc-content-skeleton')]//table//tbody//tr//td");
        }

        [AllureStep]
        public DetailSitePage ClickCalendarTab()
        {
            ClickOnElement(canlendarTab);
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
                var allWeeks = SiteCalendar.GetWeeks();
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

        [AllureStep]
        public DetailSitePage IsDetailSitePage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        [AllureStep]
        public DetailSitePage ClickOnNotesTab()
        {
            ClickOnElement(notesTab);
            return this;
        }

        [AllureStep]
        public DetailSitePage IsNotesTab()
        {
            Assert.IsTrue(IsControlDisplayed(titleInNotesTab), "Title in Notes tab is not displayed");
            Assert.IsTrue(IsControlDisplayed(noteInNotesTab), "Note in Notes tab is not displayed");
            return this;
        }
    }
}