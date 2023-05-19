using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension
{
    public class PartySuspensionPage : BasePage
    {
        private readonly By suspensionBtn = By.XPath("//button[contains(string(), 'Add New Suspension')]");
        private readonly By suspensionCells = By.XPath("//div[@id='suspensions-tab']//div[@class='grid-canvas']//div//*");

        private string suspensionTable = "//div[@id='suspensions-tab']//div[@class='grid-canvas']";
        private string suspensionRow = "./div[contains(@class, 'slick-row')]";
        private string sitesCell = "./div[contains(@class, 'l0')]";
        private string servicesCell = "./div[contains(@class, 'l1')]";
        private string fromDateCell = "./div[contains(@class, 'l2')]";
        private string lastDateCell = "./div[contains(@class, 'l3')]";
        private string suspensionDateCell = "./div[contains(@class, 'l4')]";

        private TableElement suspensionTableEle;
        public TableElement SuspensionTableEle
        {
            get => suspensionTableEle;
        } 

        public PartySuspensionPage()
        {
            suspensionTableEle = new TableElement(suspensionTable, suspensionRow, new List<string>() { sitesCell, servicesCell, fromDateCell, lastDateCell, suspensionDateCell });
            suspensionTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        [AllureStep]
        public PartySuspensionPage ClickAddNewSuspension()
        {
            ClickOnElement(suspensionBtn);
            return this;
        }
        [AllureStep]
        public SuspensionModel GetNewSuspension()
        {
            return GetAllSuspension().LastOrDefault();
        }
        [AllureStep]
        public List<SuspensionModel> GetAllSuspension()
        {
            List<SuspensionModel> suspensions = new List<SuspensionModel>();
            int count = SuspensionTableEle.GetRows().Count;
            for (int i = 0; i < count; i++)
            {
                SuspensionModel suspensionModel = new SuspensionModel();
                suspensionModel.Sites = SuspensionTableEle.GetCellValue(i, 0).AsString();
                suspensionModel.Services = SuspensionTableEle.GetCellValue(i, 1).AsString();
                suspensionModel.FromDate = SuspensionTableEle.GetCellValue(i, 2).AsString();
                suspensionModel.LastDate = SuspensionTableEle.GetCellValue(i, 3).AsString();
                suspensionModel.SuspensedDay = SuspensionTableEle.GetCellValue(i, 4).AsString();
                suspensions.Add(suspensionModel);
            }
            return suspensions;
        }
        [AllureStep]
        public PartySuspensionPage ClickNewSuspension()
        {
            int newIdx = SuspensionTableEle.GetRows().Count - 1;
            SuspensionTableEle.ClickRow(newIdx);
            return this;
        }
    }
}
