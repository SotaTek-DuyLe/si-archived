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
        private readonly By roundGroupCells = By.XPath("//div[@class='slick-viewport']//div[@class='grid-canvas']//div//*");

        public List<RoundGroupModel> GetAllRoundGroup()
        {
            List<IWebElement> cells = GetAllElements(roundGroupCells);
            List<RoundGroupModel> roundGroups = new List<RoundGroupModel>();
            for (int i = 0; i < cells.Count; i = i + 5)
            {
                RoundGroupModel roundGroup = new RoundGroupModel();
                roundGroup.Name = cells.Count > i ? GetElementText(cells[i]) : "";
                roundGroup.SortOrder = cells.Count > i + 1 ? GetElementText(cells[i + 1]) : "";
                roundGroup.StartDate = cells.Count > i + 2 ? GetElementText(cells[i + 2]) : "";
                roundGroup.EndDate = cells.Count > i + 3 ? GetElementText(cells[i + 3]) : "";
                roundGroups.Add(roundGroup);
            }
            return roundGroups;
        }

    }
}
