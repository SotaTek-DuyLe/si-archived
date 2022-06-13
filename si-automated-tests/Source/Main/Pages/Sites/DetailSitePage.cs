﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using CanlendarServiceTask = si_automated_tests.Source.Main.Models.Suspension.ServiceTaskModel;

namespace si_automated_tests.Source.Main.Pages.Sites
{
    public class DetailSitePage : BasePage
    {
        private readonly By servicedSiteTitle = By.XPath("//span[text()='Serviced Site']");
        private readonly By nextCalendarBtn = By.XPath("//div[@class='fc-left']//button[contains(@class,'fc-next-button')]");
        private readonly By canlendarTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='calendar-tab']");
        private readonly By rowsCalendarTableInMonth = By.XPath("//div[@class='fc-content-skeleton']//table//tbody//tr");

        public DetailSitePage WaitForSiteDetailPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(servicedSiteTitle);
            return this;
        }

        public DetailSitePage VerifyCurrentUrlSitePage(string siteId)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/sites/" + siteId, GetCurrentUrl());
            return this;
        }

        public DetailSitePage ClickCalendarTab()
        {
            ClickOnElement(canlendarTab);
            return this;
        }

        public List<CanlendarServiceTask> GetAllDataInMonth(DateTime fromDateTime, DateTime toDateTime)
        {
            int GetStartDate()
            {
                string startDateXpath = $"//div[@class='fc-content-skeleton']//table//thead//tr/td[1]";
                IWebElement cell = GetAllElements(startDateXpath).FirstOrDefault();
                string dataDate = cell.GetAttribute("data-date");
                DateTime startDateTime = DateTime.ParseExact(dataDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                return startDateTime.ToString("ddMMyyyy").AsInteger();
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
                int startDate = GetStartDate();
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
                            startDate++;
                            serviceTask.Content = GetElementText(cell);
                            serviceTask.ImagePath = cell.GetCssValue("background");
                            serviceTasks.Add(serviceTask);
                        }
                        else
                        {
                            //Empty cell
                            CanlendarServiceTask serviceTask = new CanlendarServiceTask();
                            serviceTask.Date = startDate;
                            startDate++;
                            serviceTasks.Add(serviceTask);
                        }
                    }
                }
            }
            return serviceTasks;
        }
    }
}
