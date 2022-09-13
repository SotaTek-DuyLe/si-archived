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
        public RoundInstanceForm()
        {
            roundInstanceEventTableEle = new TableElement("//div[@id='roundInstanceEvents-tab']//div[@class='grid-canvas']", RoundInstanceEventRow, new List<string>() { RoundInstanceEventCheckbox, RoundInstanceEventId, RoundInstanceEventType, RoundInstanceEventResource });
            roundInstanceEventTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        public readonly By DropDownStatusButton = By.XPath("//button[@data-id='status']");
        public readonly By DropDownSelect = By.XPath("//ul[@class='dropdown-menu inner']");
        public readonly By WorksheetTab = By.XPath("//a[@aria-controls='worksheet-tab']");
        public readonly By EventsTab = By.XPath("//a[@aria-controls='roundInstanceEvents-tab']");
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

        #region Event Tab
        public readonly By AddNewEventItemButton = By.XPath("//div[@id='roundInstanceEvents-tab']//button[text()='Add New Item']");
        public readonly By RefreshButton = By.XPath("//div[@id='roundInstanceEvents-tab']//button[@title='Refresh']");
        public readonly string RoundInstanceEventRow = "./div[contains(@class, 'slick-row')]";
        public readonly string RoundInstanceEventCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string RoundInstanceEventId = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string RoundInstanceEventType = "./div[contains(@class, 'slick-cell l2 r2')]";
        public readonly string RoundInstanceEventResource = "./div[contains(@class, 'slick-cell l3 r3')]";

        private TableElement roundInstanceEventTableEle;
        public TableElement RoundInstanceEventTableEle
        {
            get => roundInstanceEventTableEle;
        }

        public RoundInstanceForm VerifyNewRoundInstanceEventData(string eventType, string resource)
        {
            VerifyCellValue(RoundInstanceEventTableEle, 0, 2, eventType);
            VerifyCellValue(RoundInstanceEventTableEle, 0, 3, resource);
            return this;
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
