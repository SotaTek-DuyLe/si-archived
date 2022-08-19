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

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceForm : BasePageCommonActions
    {
        public readonly By DropDownStatusButton = By.XPath("//button[@data-id='status']");
        public readonly By DropDownSelect = By.XPath("//ul[@class='dropdown-menu inner']");
        public readonly By WorksheetTab = By.XPath("//a[@aria-controls='worksheet-tab']");
        public readonly By WorkSheetIframe = By.XPath("//iframe[@id='worksheet-tab']");

        #region WorkSheetTab
        public readonly By ToggleRoundButton = By.XPath("//button[@id='t-toggle-rounds']");
        public readonly By ReallocateButton = By.XPath("//button[@id='t-bulk-reallocate']");

        private readonly string RLITable = "//div[@id='grid']//div[@class='grid-canvas']";
        private readonly string RLIRow = "./div[contains(@class, 'slick-row') and not(contains(@class,'slick-group'))]";
        private readonly string RLICheckboxCell = "./div[contains(@class, 'l0')]//input";
        private readonly string RLIIdCell = "./div[contains(@class, 'l3')]";
        private readonly string RLIDescriptionCell = "./div[contains(@class, 'l4')]";
        private readonly string RLIServiceCell = "./div[contains(@class, 'l5')]";

        public TableElement RLITableEle
        {
            get => new TableElement(RLITable, RLIRow, new List<string>() { RLICheckboxCell, RLIIdCell, RLIDescriptionCell, RLIServiceCell });
        }

        public List<string> SelectRLI(List<string> RLIIDs)
        {
            List<string> descriptions = new List<string>();
            foreach (var RLIID in RLIIDs)
            {
                IWebElement descriptionCell = RLITableEle.GetCellByCellValues(2, new Dictionary<int, object>() { { 1, RLIID } });
                descriptions.Add(descriptionCell.Text);
                RLITableEle.ClickCellOnCellValue(0, 1, RLIID);
            }
            return descriptions;
        }
        #endregion

        public RoundInstanceForm SelectStatus(string status)
        {
            SleepTimeInMiliseconds(300);
            IWebElement webElement = this.driver.FindElements(DropDownSelect).FirstOrDefault(x => x.Displayed);
            SelectByDisplayValueOnUlElement(webElement, status);
            return this;
        }
    }
}
