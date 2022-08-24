using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Core.WebElements
{
    public class CalendarElement
    {
        private string CalendarXpath;
        private string DataDateXpath;
        private string WeekXpath;
        private string DayContentsXpath;

        public CalendarElement(string calendarXpath, string dataDateXpath, string weekXpath, string dayContentsXpath)
        {
            CalendarXpath = calendarXpath;
            DataDateXpath = dataDateXpath;
            WeekXpath = weekXpath;
            DayContentsXpath = dayContentsXpath;
        }

        public List<WeekElement> GetWeeks()
        {
            List<WeekElement> weeks = new List<WeekElement>();
            IWebElement calendarEle = WaitUtil.WaitForElementVisible(CalendarXpath);
            List<IWebElement> weekEles = calendarEle.FindElements(By.XPath(WeekXpath)).ToList();
            int index = 0;
            foreach (var weekEle in weekEles)
            {
                Dictionary<IWebElement, int> rowSpans = new Dictionary<IWebElement, int>();
                WeekElement weekElement = new WeekElement();
                weekElement.Index = index++;
                weeks.Add(weekElement);
                List<IWebElement> daysInWeek = weekEle.FindElements(By.XPath(DataDateXpath)).ToList();
                var allContents = weekEle.FindElements(By.XPath(DayContentsXpath));
                foreach (var content in allContents)
                {
                    rowSpans.Add(content, content.GetAttribute("rowspan").AsInteger());
                }
                Queue<IWebElement> dayContents = new Queue<IWebElement>(allContents);
                foreach (var dayInWeek in daysInWeek)
                {
                    string dataDate = dayInWeek.GetAttribute("data-date");
                    weekElement.Days.Add(new DayElement()
                    {
                        Date = CommonUtil.StringToDateTime(dataDate, "yyyy-MM-dd"),
                    });
                }
                while (dayContents.Count > 0)
                {
                    foreach (var dayEle in weekElement.Days)
                    {
                        if (dayContents.Count == 0) break;
                        IWebElement content = dayEle.Contents.LastOrDefault();
                        if(content != null)
                        {
                            int rowSpan = rowSpans[content];
                            if (rowSpan == 0)
                            {
                                dayEle.Contents.Add(dayContents.Dequeue());
                                continue;
                            }
                            else
                            {
                                rowSpans[content] = --rowSpan;
                                continue;
                            }
                        }
                        else
                        {
                            dayEle.Contents.Add(dayContents.Dequeue());
                            continue;
                        }
                    }
                }
            }
            return weeks;
        }

        public DayElement GetDay(DateTime day)
        {
            var weeks = GetWeeks();
            DayElement result = null;
            foreach (var week in weeks)
            {
                result = week.Days.FirstOrDefault(x => CommonUtil.DateTimeToInt(day, "yyyyMMdd") == CommonUtil.DateTimeToInt(x.Date, "yyyyMMdd"));
                if (result != null) break;
            }
            return result;
        }
    }

    public class WeekElement
    {
        public int Index { get; set; }

        public List<DayElement> Days { get; set; }

        public WeekElement()
        {
            Days = new List<DayElement>();
        }
    }

    public class DayElement
    {
        public DateTime Date { get; set; }
        public List<IWebElement> Contents { get; set; }

        public DayElement()
        {
            Contents = new List<IWebElement>();
        }
    }
}
