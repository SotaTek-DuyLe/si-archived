using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class ServiceDataManagementPage : BasePageCommonActions
    {
        public readonly By ServiceLocationTypeSelect = By.XPath("//div[@id='screen1']//select[@id='type']");
        public readonly By NextButton = By.XPath("//div[@id='screen1']//button[@id='next-button']");
        public readonly By PreviousButton = By.XPath("//div[@id='screen2']//button[contains(string(), 'Previous')]");
        public readonly By ApplyButton = By.XPath("//div[@id='screen2']//button[contains(string(), 'Apply')]");
        public readonly By TotalSpan = By.XPath("//div[@id='screen2']//div[contains(@class, 'south-panel2')]//span");
        public readonly By OkButton = By.XPath("//div[@class='modal-dialog']//button[contains(string(), 'OK')]");
        public readonly By StatusExpandButton = By.XPath("//div[@id='screen1']//div[contains(@class, 'slick-headerrow-column l6')]//button");
        public readonly By StatusSelect = By.XPath("//div[contains(@class, 'bs-container')]//ul");
        public readonly By ApplyFilterBtn = By.XPath("//div[@id='screen1']//button[@id='filter-button']");
        public readonly By ClearFilterBtn = By.XPath("//div[@id='screen1']//button[@id='clear-filters-button']");

        public readonly By SelectAndDeselectAllCheckbox = By.XPath("//div[@id='point-grid']//div[@title='Select/Deselect All']//input");

        /// <summary>
        /// SDM: Service Data Management
        /// </summary>
        private readonly string SDMTableXPath = "//div[@id='screen1']//div[@class='grid-canvas']";
        private readonly string SDMRowXPath = "./div[contains(@class, 'slick-row')]";
        private readonly string SDMCheckboxXPath = "./div[contains(@class, 'slick-cell l0')]//input[@type='checkbox']";
        private readonly string SDMPointAddressXPath = "./div[contains(@class, 'slick-cell l1')]";
        private readonly string SDMTypeXPath = "./div[contains(@class, 'slick-cell l2')]";
        private readonly string SDMDescriptionXPath = "./div[contains(@class, 'slick-cell l3')]";
        private readonly string SDMPostcodeXPath = "./div[contains(@class, 'slick-cell l4')]";
        private readonly string SDMStreetXPath = "./div[contains(@class, 'slick-cell l5')]";
        private readonly string SDMStatusXPath = "./div[contains(@class, 'slick-cell l6')]";

        private readonly string SDMDescriptioinTableXPath = "//div[@id='screen2']//table[@id='description-table']";
        private readonly string SDMDescriptioinRowXPath = "./tbody//tr[@data-bind]";
        private readonly string SDMDescriptioinCheckboxCellXPath = "./td//input[@type='checkbox']";
        private readonly string SDMDescriptioinRecordStatusCellXPath = "./td//img";
        private readonly string SDMDescriptioinDetailCellXPath = "./td[3]";

        private readonly string SDMMasterTableXPath = "//div[@id='screen2']//table[@id='master-table']";
        private readonly string SDMMasterRowXPath = "./tbody//tr";
        private readonly string SDMMasterServiceUnitCellXPath = "./td//img";
        private readonly string SDMMasterServiceTaskCellXPath = "./td[2]//span";
        private readonly string popup300PointsDisplayedMessage = "//div[text()='Only first 300 points will be displayed on the next screen']";
        private readonly string okBtn = "//button[text()='OK']";
        private readonly string cancelBtn = "//button[text()='Cancel']";

        private TableElement serviceDataManagementTableElement;
        public TableElement ServiceDataManagementTableElement
        {
            get
            {
                if (serviceDataManagementTableElement == null)
                {
                    serviceDataManagementTableElement = new TableElement(
                        SDMTableXPath,
                        SDMRowXPath,
                        new List<string>() { SDMCheckboxXPath, SDMPointAddressXPath, SDMTypeXPath, SDMDescriptionXPath, SDMPostcodeXPath, SDMStreetXPath, SDMStatusXPath });
                    serviceDataManagementTableElement.GetDataView = (IEnumerable<IWebElement> rows) =>
                    {
                        return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
                    }; ;
                }
                return serviceDataManagementTableElement;
            }
        }

        [AllureStep]
        public ServiceDataManagementPage SelectStatusOption(string status)
        {
            List<IWebElement> options = GetElement(StatusSelect).FindElements(By.XPath("option")).ToList();
            foreach (var item in options)
            {
                if (item.Text.Trim() == status)
                {
                    item.Click();
                    break;
                }
            }
            return this;
        }

        public TableElement DescriptionTableElement
        {
            get => new TableElement(SDMDescriptioinTableXPath, SDMDescriptioinRowXPath, new List<string>() { SDMDescriptioinCheckboxCellXPath, SDMDescriptioinRecordStatusCellXPath, SDMDescriptioinDetailCellXPath });
        }

        public TableElement MasterTableElement
        {
            get => new TableElement(SDMMasterTableXPath, SDMMasterRowXPath, new List<string>() { SDMMasterServiceUnitCellXPath, SDMMasterServiceTaskCellXPath });
        }

        public ServiceDataManagementPage ClickPointAddress(string poinAddress)
        {
            ClickOnElement(ServiceDataManagementTableElement.GetCellByValue(1, poinAddress));
            return this;
        }

        public Dictionary<int, List<object>> ClickMultiPointAddress(int rowCount)
        {
            Dictionary<int, List<object>> rows = new Dictionary<int, List<object>>();
            int i = 0;
            while (i < rowCount)
            {
                List<IWebElement> unselectedRows = ServiceDataManagementTableElement.GetRows().Where(x => !x.FindElement(By.XPath(SDMCheckboxXPath)).Selected).ToList();
                if (unselectedRows.Count == 0)
                {
                    return rows;
                }

                int j = 0;
                while (j < unselectedRows.Count)
                {
                    try
                    {
                        rows.Add(i, ServiceDataManagementTableElement.GetRowValue(unselectedRows[j]));
                        unselectedRows[j].FindElement(By.XPath(SDMCheckboxXPath)).Click();
                        j++;
                        i++;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException ex)
                    {
                        SleepTimeInMiliseconds(200);
                        unselectedRows = ServiceDataManagementTableElement.GetRows().Where(x => !x.FindElement(By.XPath(SDMCheckboxXPath)).Selected).ToList();
                        j = 0;
                        rows.Add(i, ServiceDataManagementTableElement.GetRowValue(unselectedRows[j]));
                        unselectedRows[j].FindElement(By.XPath(SDMCheckboxXPath)).Click();
                        j++;
                        i++;
                    }
                }
                if (i >= 299) WaitForLoadingIconToDisappear();
                WaitUtil.WaitForElementClickable(By.XPath($"(//div[@id='screen1']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]//div[@class='slick-cell l0 r0']//input[not(@checked)])[1]"));
            }
            return rows;
        }

        public List<object> GetPointAddressCellValues(string pointAddress)
        {
            IWebElement rowEle = ServiceDataManagementTableElement.GetRowByCellValue(1, pointAddress);
            int rowIdx = ServiceDataManagementTableElement.GetRows().IndexOf(rowEle);
            return ServiceDataManagementTableElement.GetRowValue(rowIdx);
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyDescriptionLayout(List<object> pointValues, int selectedRowIdx = 0, bool checkMaster = false)
        {
            ScrollDownToElement(DescriptionTableElement.GetRow(selectedRowIdx), false);
            Assert.IsTrue(DescriptionTableElement.GetCellVisibility(selectedRowIdx, 0));
            Assert.IsTrue(DescriptionTableElement.GetCellValue(selectedRowIdx, 2).AsString() == pointValues[3].AsString());
            Assert.IsTrue(GetDescriptionStatus(DescriptionTableElement.GetCellAttribute(selectedRowIdx, 1, "src")).Contains(pointValues[6].AsString()));
            if (checkMaster)
            {
                Assert.IsTrue(MasterTableElement.GetCellAttribute(selectedRowIdx, 0, "src").Contains("service-unit.png"));
                Assert.IsTrue(MasterTableElement.GetCellCss(selectedRowIdx, 1, "background-color") == "rgba(153, 204, 204, 1)");
            }
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyDescriptionLayout(Dictionary<int, List<object>> rowDatas)
        {
            var rows = DescriptionTableElement.GetRows();
            int rowIdx = 0;
            foreach (var row in rows)
            {
                if (rowIdx > rowDatas.Count) break;
                List<object> pointValues = rowDatas[rowIdx];
                ScrollDownToElement(row, false);
                Assert.IsTrue(DescriptionTableElement.GetCellVisibility(row, 0));
                Assert.IsTrue(DescriptionTableElement.GetCellValue(row, 2).AsString() == pointValues[3].AsString());
                Assert.IsTrue(GetDescriptionStatus(DescriptionTableElement.GetCellAttribute(row, 1, "src")).Contains(pointValues[6].AsString()));
                rowIdx++;
            }
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyDescriptionLayout(List<object> pointValues, string imgName, int selectedRowIdx = 0)
        {
            ScrollDownToElement(DescriptionTableElement.GetRow(selectedRowIdx));
            Assert.IsTrue(DescriptionTableElement.GetCellVisibility(selectedRowIdx, 0));
            Assert.IsTrue(DescriptionTableElement.GetCellValue(selectedRowIdx, 2).AsString() == pointValues[3].AsString());
            Assert.IsTrue(DescriptionTableElement.GetCellAttribute(selectedRowIdx, 1, "src").Contains(imgName));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyTheDisplayOfPopupOver300Point()
        {
            Assert.IsTrue(IsControlDisplayed(popup300PointsDisplayedMessage));
            Assert.IsTrue(IsControlEnabled(okBtn));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOnOkBtn()
        {
            ClickOnElement(okBtn);
            return this;
        }

        private List<string> GetDescriptionStatus(string imgPath)
        {
            if (imgPath.Contains("yellow"))
            {
                return new List<string>() { "New", "Updated" };
            }
            else if (imgPath.Contains("red"))
            {
                return new List<string>() { "Retired" };
            }
            else if (imgPath.Contains("green"))
            {
                return new List<string>() { "Verified" };
            }
            else if (imgPath.Contains("transparent"))
            {
                return new List<string>() { "" };
            }
            return new List<string>() {};
        }
    }
}
