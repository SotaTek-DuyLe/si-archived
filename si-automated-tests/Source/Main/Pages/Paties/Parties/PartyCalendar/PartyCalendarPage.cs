using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
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
       
        public PartyCalendarPage ClickSiteCombobox()
        {
            IWebElement siteCombobox = GetAllElements(comboboxInCalendars).FirstOrDefault();
            ClickOnElement(siteCombobox);
            return this;
        }

        public PartyCalendarPage ClickSellectAllSites()
        {
            WaitUtil.WaitForElementVisible(selectAllSitesBtn);
            ClickOnElement(selectAllSitesBtn);
            return this;
        }

        public PartyCalendarPage ClickServiceCombobox()
        {
            IWebElement serviceCombobox = GetAllElements(comboboxInCalendars)[1];
            ClickOnElement(serviceCombobox);
            return this;
        }

        public PartyCalendarPage ClickSellectAllServices()
        {
            WaitUtil.WaitForElementVisible(selectAllSitesBtn);
            ClickOnElement(selectAllSitesBtn);
            return this;
        }

        public PartyCalendarPage ClickApplyCalendarButton()
        {
            ClickOnElement(applyBtn);
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
