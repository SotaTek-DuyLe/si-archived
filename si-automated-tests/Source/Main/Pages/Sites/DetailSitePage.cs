using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using CanlendarServiceTask = si_automated_tests.Source.Main.Models.Suspension.ServiceTaskModel;

namespace si_automated_tests.Source.Main.Pages.Sites
{
    public class DetailSitePage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Serviced Site']");
        private readonly By notesTab = By.CssSelector("a[aria-controls='notes-tab']");
        private readonly By nextCalendarBtn = By.XPath("//div[@class='fc-left']//button[contains(@class,'fc-next-button')]");
        private readonly By canlendarTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='calendar-tab']");
        private readonly By rowsCalendarTableInMonth = By.XPath("//div[@class='fc-content-skeleton']//table//tbody//tr");
        private readonly By titleInNotesTab = By.XPath("//label[text()='Title']/following-sibling::input");
        private readonly By noteInNotesTab = By.XPath("//label[text()='Note']/following-sibling::textarea");

        [AllureStep]
        public DetailSitePage ClickCalendarTab()
        {
            ClickOnElement(canlendarTab);
            return this;
        }
        [AllureStep]
        public List<CanlendarServiceTask> GetAllDataInMonth(DateTime fromDateTime, DateTime toDateTime)
        {
            DateTime GetStartDate()
            {
                string startDateXpath = $"//div[@class='fc-content-skeleton']//table//thead//tr/td[1]";
                IWebElement cell = GetAllElements(startDateXpath).FirstOrDefault();
                string dataDate = cell.GetAttribute("data-date");
                DateTime startDateTime = DateTime.ParseExact(dataDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                return startDateTime;
            }
            List<CanlendarServiceTask> serviceTasks = new List<CanlendarServiceTask>();
            int dayOfWeek = 7;
            int months = toDateTime.Month - fromDateTime.Month;
            int step = 0;
            while (step <= months)
            {
                if (step > 0)
                {
                    ClickOnElement(nextCalendarBtn);
                    WaitForLoadingIconToDisappear();
                }
                step++;
                Thread.Sleep(1000);
                DateTime startDate = GetStartDate();
                var rows = driver.FindElements(rowsCalendarTableInMonth);
                foreach (var row in rows)
                {
                    for (int day = 1; day <= dayOfWeek; day++)
                    {
                        string cellXpath = $"//td[{day}]//a";
                        if (row.FindElements(By.XPath(cellXpath)).Count != 0)
                        {
                            IWebElement cell = row.FindElement(By.XPath(cellXpath));
                            CanlendarServiceTask serviceTask = new CanlendarServiceTask();
                            serviceTask.Date = startDate;
                            startDate.AddDays(1);
                            serviceTask.Content = GetElementText(cell);
                            serviceTask.ImagePath = cell.GetCssValue("background");
                            serviceTasks.Add(serviceTask);
                        }
                        else
                        {
                            //Empty cell
                            CanlendarServiceTask serviceTask = new CanlendarServiceTask();
                            serviceTask.Date = startDate;
                            startDate.AddDays(1);
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