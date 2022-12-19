using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundCalendarPage : BasePageCommonActions
    {
        public readonly By SelectContact = By.XPath("//select[@id='contract']");
        public readonly By SelectShiftGroup = By.XPath("//select[@id='shift-group']");
        public readonly By InputService = By.XPath("//input[@id='services']");
        public readonly By ButtonGo = By.XPath("//button[text()='Go']");
        public readonly By ButtonWeek = By.XPath("//button[text()='Week']");
        public readonly By ButtonDay = By.XPath("//button[text()='Day']");
        public readonly By ButtonMonth = By.XPath("//button[text()='Month']");
        public readonly By ButtonLegend = By.XPath("//button[@id='calendarLegend']");
        public readonly By ButtonSchedule = By.XPath("//a[contains(text(), 'Reschedule')]");
        public readonly By ButtonRoundFinder = By.XPath("//a[@id='round-finder']");
        public readonly By ButtonFind = By.XPath("//button[text()='Find']");
        public readonly By InputRound = By.XPath("//div[@id='services-finder']//input");
        public readonly By InputOriginDate = By.XPath("//input[@id='date']");

        private TreeViewElement _treeViewElement = new TreeViewElement("//div[contains(@class, 'jstree-1')]", "./li[contains(@role, 'treeitem')]", "./a", "./ul[contains(@class, 'jstree-children')]");
        private TreeViewElement ServicesTreeView
        {
            get => _treeViewElement;
        }

        private TreeViewElement _searchRoundtreeViewElement = new TreeViewElement("//div[contains(@class, 'jstree-2')]", "./li[contains(@role, 'treeitem')]", "./a", "./ul[contains(@class, 'jstree-children')]", "./i[contains(@class, 'jstree-ocl')][1]");
        private TreeViewElement SearchRoundTreeView
        {
            get => _searchRoundtreeViewElement;
        }

        public CalendarElement RoundCalendar
        {
            get => new CalendarElement("//div[contains(@class, 'fc-month-view')]", "./div[contains(@class, 'fc-bg')]//table//tbody//tr//td", "//div[contains(@class, 'fc-week')]", "./div[contains(@class, 'fc-content-skeleton')]//table//tbody//tr//td");
        }

        [AllureStep]
        public RoundCalendarPage VerifyScheduleButtonEnable(bool isEnable)
        {
            Assert.IsTrue(isEnable ? !GetElement(ButtonSchedule).GetAttribute("class").Contains("disabled") : GetElement(ButtonSchedule).GetAttribute("class").Contains("disabled"));
            return this;
        }
        [AllureStep]
        public RoundCalendarPage ClickInputService()
        {
            IWebElement element = this.driver.FindElement(InputService);
            element.Click();
            return this;
        }
        [AllureStep]
        public RoundCalendarPage ClickInputRound()
        {
            IWebElement element = this.driver.FindElements(InputRound).FirstOrDefault(x => x.Displayed);
            element?.Click();
            return this;
        }
        [AllureStep]
        public RoundCalendarPage SendInputOriginDate(string value)
        {
            IWebElement element = this.driver.FindElements(InputOriginDate).FirstOrDefault(x => x.Displayed);
            SendKeys(element, value);
            return this;
        }
        [AllureStep]
        public RoundCalendarPage ClickButtonFind()
        {
            IWebElement element = this.driver.FindElements(ButtonFind).FirstOrDefault(x => x.Displayed);
            element?.Click();
            return this;
        }
        [AllureStep]
        public RoundCalendarPage SelectServiceNode(string nodeName)
        {
            ServicesTreeView.ClickItem(nodeName);
            return this;
        }
        [AllureStep]
        public RoundCalendarPage SelectRoundNode(string nodeName)
        {
            SearchRoundTreeView.ClickItem(nodeName);
            return this;
        }
        [AllureStep]
        public RoundCalendarPage ExpandRoundNode(string nodeName)
        {
            SearchRoundTreeView.ExpandNode(nodeName);
            return this;
        }
        [AllureStep]
        public List<RoundCalendarModel> GetAllDataRoundCalendarInMonth()
        {
            List<RoundCalendarModel> roundCalendars = new List<RoundCalendarModel>();
            var allWeeks = RoundCalendar.GetWeeks();
            var allDays = new List<DayElement>();
            foreach (var week in allWeeks)
            {
                allDays.AddRange(week.Days);
            }
            foreach (var dayInstance in allDays)
            {
                RoundCalendarModel roundCalendarModel = new RoundCalendarModel();
                roundCalendarModel.Instances = new List<RoundInstanceSchedule>();
                roundCalendarModel.Day = dayInstance.Date;
                roundCalendars.Add(roundCalendarModel);
                foreach (var content in dayInstance.Contents)
                {
                    string className = content.GetAttribute("class");
                    if (className.Contains("fc-event-container"))
                    {
                        List<IWebElement> details = content.FindElements(By.XPath("./a")).ToList();
                        if (details.Count > 0)
                        {
                            string bgColor = details[0].GetCssValue("background-color");
                            roundCalendarModel.Instances.Add(new RoundInstanceSchedule()
                            {
                                Instance = details[0],
                                IsGrey = bgColor == "rgba(245, 245, 245, 1)",
                                IsGreen = ColorHelper.IsGreenColor(bgColor),
                            });
                        }
                    }
                    else if (className.Contains("fc-more-cell"))
                    {
                        List<IWebElement> details = content.FindElements(By.XPath("./div/a[@class='fc-more']")).ToList();
                        if (details.Count > 0)
                        {
                            roundCalendarModel.ButtonMore = details[0];
                        }
                    }
                }
            }
            return roundCalendars;
        }
        [AllureStep]
        public List<RoundCalendarModel> GetAllDataRoundCalendarInWeek()
        {
            List<RoundCalendarModel> roundCalendars = new List<RoundCalendarModel>();
            List<IWebElement> dayOfWeeks = GetAllElements(By.XPath("//div[contains(@class, 'fc-basicWeek-view')]//table//thead//tr//th[contains(@class, 'fc-day-header')]"));
            foreach (var dayOfWeek in dayOfWeeks)
            {
                string dataDate = dayOfWeek.GetAttribute("data-date");
                RoundCalendarModel roundCalendarModel = new RoundCalendarModel();
                roundCalendarModel.Instances = new List<RoundInstanceSchedule>();
                roundCalendarModel.Day = CommonUtil.StringToDateTime(dataDate, "yyyy-MM-dd");
                roundCalendars.Add(roundCalendarModel);
            }
            List<IWebElement> instanceEles = GetAllElements(By.XPath("//div[contains(@class, 'fc-basicWeek-view')]//div[contains(@class, 'fc-content-skeleton')]//table//tbody//tr")).ToList();
            for (int i = 0; i < 7; i++)
            {
                foreach (var instanceOfWeek in instanceEles)
                {
                    List<IWebElement> cellInstances = instanceOfWeek.FindElements(By.XPath("./td[@class='fc-event-container']//a")).ToList();
                    if (cellInstances.Count > i)
                    {
                        string bgColor = cellInstances[i].GetCssValue("background-color");
                        roundCalendars[i].Instances.Add(new RoundInstanceSchedule()
                        {
                            Instance = cellInstances[i],
                            IsGrey = bgColor == "rgba(245, 245, 245, 1)",
                        });
                    }
                }
            }
            return roundCalendars;
        }
        [AllureStep]
        public List<RoundCalendarModel> GetAllDataRoundCalendarInDay()
        {
            List<RoundCalendarModel> roundCalendars = new List<RoundCalendarModel>();
            List<IWebElement> dayOfWeeks = GetAllElements(By.XPath("//div[contains(@class, 'fc-basicDay-view')]//table//thead//tr//th[contains(@class, 'fc-day-header')]"));
            foreach (var dayOfWeek in dayOfWeeks)
            {
                string dataDate = dayOfWeek.GetAttribute("data-date");
                RoundCalendarModel roundCalendarModel = new RoundCalendarModel();
                roundCalendarModel.Instances = new List<RoundInstanceSchedule>();
                roundCalendarModel.Day = CommonUtil.StringToDateTime(dataDate, "yyyy-MM-dd");
                roundCalendars.Add(roundCalendarModel);
            }
            List<IWebElement> instanceEles = GetAllElements(By.XPath("//div[contains(@class, 'fc-basicDay-view')]//div[contains(@class, 'fc-content-skeleton')]//table//tbody//tr")).ToList();
            for (int i = 0; i < 7; i++)
            {
                foreach (var instanceOfWeek in instanceEles)
                {
                    List<IWebElement> cellInstances = instanceOfWeek.FindElements(By.XPath("./td[@class='fc-event-container']//a")).ToList();
                    if (cellInstances.Count > i)
                    {
                        string bgColor = cellInstances[i].GetCssValue("background-color");
                        roundCalendars[i].Instances.Add(new RoundInstanceSchedule()
                        {
                            Instance = cellInstances[i],
                            IsGrey = bgColor == "rgba(245, 245, 245, 1)",
                        });
                    }
                }
            }
            return roundCalendars;
        }
        [AllureStep]
        public RoundCalendarPage RoundInstanceHasGreenBackground(DateTime dateTime)
        {
            List<RoundCalendarModel> roundCalendars = GetAllDataRoundCalendarInMonth();
            RoundCalendarModel dayRoundInstance = roundCalendars.FirstOrDefault(x => CommonUtil.DateTimeToInt(x.Day, "yyyyMMdd") == CommonUtil.DateTimeToInt(dateTime, "yyyyMMdd"));
            Assert.IsTrue(dayRoundInstance.Instances.Any(x => x.IsGreen));
            return this;
        }
        [AllureStep]
        public RoundCalendarPage IsRoundCalendarInWeekDisplayed()
        {
            List<RoundCalendarModel> rounds = GetAllDataRoundCalendarInWeek();
            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            // If we started on Sunday, we should actually have gone *back*
            // 6 days instead of forward 1...
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
            foreach (var day in dates)
            {
                Assert.IsTrue(rounds.Any(x => CommonUtil.DateTimeToInt(x.Day, "yyyyMMdd") == CommonUtil.DateTimeToInt(day, "yyyyMMdd")));
            }
            foreach (var item in rounds)
            {
                Assert.IsTrue(item.Instances.Any(x => x.Instance.Text.Contains("+6 more")) == false);
            }
            return this;
        }
        [AllureStep]
        public RoundCalendarPage IsRoundCalendarInDayDisplayed()
        {
            List<RoundCalendarModel> rounds = GetAllDataRoundCalendarInDay();
            var now = DateTime.Now;
            foreach (var item in rounds)
            {
                Assert.IsTrue(rounds.FirstOrDefault(x => CommonUtil.DateTimeToInt(x.Day, "yyyyMMdd") == CommonUtil.DateTimeToInt(now, "yyyyMMdd")) != null);
                Assert.IsTrue(item.Instances.Any(x => x.Instance.Text.Contains("+6 more")) == false);
            }
            return this;
        }
        [AllureStep]
        public RoundCalendarPage ClickMoreButton(bool clickGreyInstance)
        {
            List<RoundCalendarModel> roundCalendars = GetAllDataRoundCalendarInMonth();
            RoundCalendarModel roundCalendar = roundCalendars.FirstOrDefault(x => x.Instances.Any(e => e.IsGrey == clickGreyInstance));
            roundCalendar?.ButtonMore.Click();
            return this;
        }
        [AllureStep]
        public RoundCalendarPage IsListOfRoundInstanceScheduleDisplayed()
        {
            List<IWebElement> rounds = GetAllElements(By.XPath("//div[contains(@class, 'fc-popover')]//div[contains(@class, 'fc-body')]//a"));
            Assert.IsTrue(rounds.Any(x => !x.Displayed) == false);
            foreach (var round in rounds)
            {
                Assert.IsTrue(round.GetCssValue("background-image").Contains("content/images/coreroundstate"));
            }
            return this;
        }
        [AllureStep]
        public RoundCalendarPage ClickRoundInstance(bool clickGreyInstance)
        {
            List<RoundCalendarModel> roundCalendars = GetAllDataRoundCalendarInMonth();
            RoundCalendarModel roundCalendar = roundCalendars.FirstOrDefault(x => x.Instances.Any(e => e.IsGrey == clickGreyInstance));
            roundCalendar?.Instances.FirstOrDefault().Instance.Click();
            return this;
        }
        [AllureStep]
        public RoundCalendarPage ClickRoundInstance(DateTime dateTime)
        {
            List<RoundCalendarModel> roundCalendars = GetAllDataRoundCalendarInMonth();
            RoundCalendarModel roundCalendar = roundCalendars.FirstOrDefault(x => CommonUtil.DateTimeToInt(x.Day, "yyyyMMdd") == CommonUtil.DateTimeToInt(dateTime, "yyyyMMdd"));
            roundCalendar?.Instances.FirstOrDefault().Instance.Click();
            return this;
        }
        [AllureStep]
        public RoundCalendarPage VerifyRoundInstanceBackground(DateTime dateTime, string backgroundColor)
        {
            List<RoundCalendarModel> roundCalendars = GetAllDataRoundCalendarInMonth();
            RoundCalendarModel roundCalendar = roundCalendars.FirstOrDefault(x => CommonUtil.DateTimeToInt(x.Day, "yyyyMMdd") == CommonUtil.DateTimeToInt(dateTime, "yyyyMMdd"));
            string bgColor = roundCalendar?.Instances.FirstOrDefault().Instance.GetCssValue("background-color");
            Assert.IsTrue(bgColor == backgroundColor);
            return this;
        }
        [AllureStep]
        public RoundCalendarPage DoubleClickRoundInstance(bool clickGreyInstance)
        {
            List<RoundCalendarModel> roundCalendars = GetAllDataRoundCalendarInMonth();
            RoundCalendarModel roundCalendar = roundCalendars.FirstOrDefault(x => x.Instances.Any(e => e.IsGrey == clickGreyInstance));
            DoubleClickOnElement(roundCalendar?.Instances.FirstOrDefault().Instance);
            return this;
        }
        [AllureStep]
        public RoundCalendarPage IsCalendarScheduleDisplayed()
        {
            List<(string imgPath, string legendItemText)> expectedResult = new List<(string imgPath, string legendItemText)>()
            {
                ("/content/images/coreroundstate/1.svg", "Scheduled"),
                ("", "Rescheduled"),
                ("/content/images/coreroundstate/2.svg", "In Progress"),
                ("/content/images/coreroundstate/3.svg", "Complete"),
                ("/content/images/coreroundstate/5.svg", "Delayed"),
                ("/content/images/coreroundstate/4.svg", "Cancelled"),
            };
            List<IWebElement> legendItems = GetAllElements(By.XPath("//div[@id='fc-legend-content']//div[@class='legend-item']"));
            foreach (var legendItem in legendItems)
            {
                IWebElement icon = legendItem.FindElement(By.XPath("./div[@class='legend-icon']"));
                Assert.IsTrue(expectedResult.Any(x => icon.GetCssValue("background-image").Contains(x.imgPath)));
                IWebElement text = legendItem.FindElement(By.XPath("./div[@class='legend-item-text']"));
                Assert.IsTrue(expectedResult.Any(x => text.Text == x.legendItemText));
            }
            return this;
        }
        [AllureStep]
        public RoundCalendarPage IsCanlendarScheduleUnDisplayed()
        {
            Assert.IsTrue(IsControlUnDisplayed(By.XPath("//div[@id='fc-legend-content']//div[@class='legend-item']")));
            return this;
        }
    }
}
