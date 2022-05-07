using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension
{
    public class PartySuspensionPage : BasePage
    {
        private readonly By suspensionBtn = By.XPath("//button[contains(string(), 'Add New Suspension')]");
        private readonly By suspensionCells = By.XPath("//div[@id='suspensions-tab']//div[@class='grid-canvas']//div//*");

        public PartySuspensionPage ClickAddNewSuspension()
        {
            ClickOnElement(suspensionBtn);
            return this;
        }
        public SuspensionModel GetNewSuspension()
        {
            return GetAllSuspension().LastOrDefault();
        }

        public List<SuspensionModel> GetAllSuspension()
        {
            List<IWebElement> cells = GetAllElements(suspensionCells);
            List<SuspensionModel> suspensions = new List<SuspensionModel>();
            for (int i = 0; i < cells.Count; i = i + 5)
            {
                SuspensionModel suspension = new SuspensionModel();
                suspension.Sites = cells.Count > i ? GetElementText(cells[i]) : "";
                suspension.Services = cells.Count > i + 1 ? GetElementText(cells[i + 1]) : "";
                suspension.FromDate = cells.Count > i + 2 ? GetElementText(cells[i + 2]) : "";
                suspension.LastDate = cells.Count > i + 3 ? GetElementText(cells[i + 3]) : "";
                suspension.SuspensedDay = cells.Count > i + 4 ? GetElementText(cells[i + 4]) : "";
                suspensions.Add(suspension);
            }
            return suspensions;
        }
    }
}
