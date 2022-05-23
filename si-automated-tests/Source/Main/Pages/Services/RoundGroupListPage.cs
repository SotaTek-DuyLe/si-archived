using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundGroupListPage : BasePage
    {
        private readonly By roundGroupRows = By.XPath("//div[@class='slick-viewport']//div[@class='grid-canvas']//div[contains(@class,'ui-widget-content')]");

        public RoundGroupListPage DoubleClickRoundGroup(string name)
        {
            List<IWebElement> rows = GetAllElements(roundGroupRows);
            foreach (var row in rows)
            {
                IWebElement nameCell = row.FindElement(By.XPath("./div[contains(@class,'l2')]"));
                if (GetElementText(nameCell) == name)
                {
                    DoubleClickOnElement(row);
                    break;
                }
            }
            return this;
        }

        public List<RoundGroupModel> GetAllRoundGroup()
        {
            List<RoundGroupModel> receiptLines = new List<RoundGroupModel>();
            List<IWebElement> rows = GetAllElements(roundGroupRows);
            foreach (var row in rows)
            {
                IWebElement nameCell = row.FindElement(By.XPath("./div[contains(@class,'l2')]"));
                IWebElement sortCell = row.FindElement(By.XPath("./div[contains(@class,'l3')]"));
                IWebElement startDateCell = row.FindElement(By.XPath("./div[contains(@class,'l4')]"));
                IWebElement endDateCell = row.FindElement(By.XPath("./div[contains(@class,'l5')]"));
                receiptLines.Add(new RoundGroupModel()
                {
                    Name = GetElementText(nameCell),
                    SortOrder = GetElementText(sortCell),
                    StartDate = GetElementText(startDateCell),
                    EndDate = GetElementText(endDateCell),
                });
            }
            return receiptLines;
        }

    }
}
