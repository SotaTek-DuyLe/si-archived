using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Applications.RiskRegister
{
    public class RiskRegisterPage : BasePageCommonActions
    {
        private readonly string stepTitle = "//span[@class='step-title' and text()='{0}'] ";
        public readonly By ObjectTypeSelect = By.XPath("//select[@id='object-type']");
        public readonly By ContractSelect = By.XPath("//select[@id='contract']");
        public readonly By FilterSelect = By.XPath("//div[@id='filter']//select");
        public readonly By PreviewResultButton = By.XPath("//button[text()='Preview Results']");

        private string previewTable = "//div[@data-bind='objectsPreview: preview.sample']//div[@class='grid-canvas']";
        private string previewRow = "./div[contains(@class, 'slick-row')]";
        private string idCell = "./div[@class='slick-cell l0 r0']";
        private string addressCell = "./div[@class='slick-cell l1 r1']";
        private readonly By addFilterButton = By.XPath("//button//span[@class='glyphicon glyphicon-plus']");
        public readonly By filterInput = By.XPath("//div[@id='filter']//input");
        public readonly By NextButtonOnStep1 = By.XPath("//div[@id='screen1']//button[text()='Next']");

        public RiskRegisterPage()
        {
            previewTableEle = new TableElement(previewTable, previewRow, new List<string>() { idCell, addressCell });
            previewTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };

            riskTableEle = new TableElement(riskTable, riskRow, new List<string>() { riskCheckboxCell, riskCell, levelCell });
            riskTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        #region Step 1
        private TableElement previewTableEle;
        public TableElement PreviewTableEle
        {
            get => previewTableEle;
        }

        [AllureStep]
        public string SelectFirstIdAndFilter()
        {
            WaitUtil.WaitForElementInvisible("//div[@role='progressbar']");
            string id = PreviewTableEle.GetCellValue(0, 0).AsString();
            ClickOnElement(addFilterButton);
            SleepTimeInMiliseconds(300);
            SendKeys(filterInput, id);
            ClickOnElement(PreviewResultButton);
            return id;
        }
        #endregion

        #region Step 2
        public readonly By AddSelectRiskbutton = By.XPath("//div[@id='screen2']//button[text()='Add Selected']");
        public readonly By RemoveAllRiskbutton = By.XPath("//div[@id='screen2']//button[text()='Remove All']");
        public readonly By NextButtonOnScreen2 = By.XPath("//div[@id='screen2']//button[text()='Next']");
        public readonly By PreviousButtonOnScreen2 = By.XPath("//div[@id='screen2']//button[text()='Previous']");
        private readonly By selectAllRiskCheckbox = By.XPath("//div[@id='screen2']//div[@title='Select/Deselect All']//input");
        private string riskSelectTable = "//div[@id='screen2']//tbody[@data-bind='foreach: selectedRisks']";
        private string riskSelectRow = "./tr[@class='text-info']";
        private string riskNameSelectCell = "./td[@data-bind='text: riskName']";
        private string riskProximityAlertCell = "./td//input[@data-bind='checked: proximityAlert']";
        private string riskSelectStateDate = "./td//input[@id='start-date']";
        private string riskSelectEndDate = "./td//input[@id='end-date']";
        private string mitigationNoteCell = "./td//input[@data-bind='value: mitigation']";

        public TableElement RiskSelectTableEle
        {
            get => new TableElement(riskSelectTable, riskSelectRow, new List<string>() { riskNameSelectCell, riskProximityAlertCell, riskSelectStateDate, riskSelectEndDate, mitigationNoteCell });
        }

        private string riskTable = "//div[@id='risks-grid']//div[@class='grid-canvas']";
        private string riskRow = "./div[contains(@class, 'slick-row')]";
        private string riskCheckboxCell = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        private string riskCell = "./div[contains(@class, 'slick-cell l1 r1')]";
        private string levelCell = "./div[contains(@class, 'slick-cell l2 r2')]";

        private TableElement riskTableEle;
        public TableElement RiskTableEle
        {
            get => riskTableEle;
        }

        [AllureStep]
        public RiskRegisterPage VerifyRiskSelect(string riskName)
        {
            Assert.IsNotNull(RiskSelectTableEle.GetCellByCellValues(
                RiskSelectTableEle.GetCellIndex(riskNameSelectCell), 
                new Dictionary<int, object>() { { RiskSelectTableEle.GetCellIndex(riskNameSelectCell), riskName } }
            ));
            return this;
        }

        [AllureStep]
        public RiskRegisterPage InputRiskSelect(string startdate, string endDate, string mitigationNote)
        {
            RiskSelectTableEle.ClickCell(0, RiskSelectTableEle.GetCellIndex(riskProximityAlertCell));
            RiskSelectTableEle.SetCellValue(0, RiskSelectTableEle.GetCellIndex(riskSelectStateDate), startdate);
            RiskSelectTableEle.SetCellValue(0, RiskSelectTableEle.GetCellIndex(riskSelectEndDate), endDate);
            RiskSelectTableEle.SetCellValue(0, RiskSelectTableEle.GetCellIndex(mitigationNoteCell), mitigationNote);
            return this;
        }

        [AllureStep]
        public RiskRegisterPage VerifyRiskSelect(bool proximityAlert, string startdate, string endDate, string mitigationNote)
        {
            VerifyCellValue(RiskSelectTableEle, 0, RiskSelectTableEle.GetCellIndex(riskProximityAlertCell), proximityAlert);
            VerifyCellValue(RiskSelectTableEle, 0, RiskSelectTableEle.GetCellIndex(riskSelectStateDate), startdate);
            VerifyCellValue(RiskSelectTableEle, 0, RiskSelectTableEle.GetCellIndex(riskSelectEndDate), endDate);
            VerifyCellValue(RiskSelectTableEle, 0, RiskSelectTableEle.GetCellIndex(mitigationNoteCell), mitigationNote);
            return this;
        }

        [AllureStep]
        public string SelectRisk(int idx = 0)
        {
            RiskTableEle.ClickCell(idx, RiskTableEle.GetCellIndex(riskCheckboxCell));
            SleepTimeInMiliseconds(300);
            return RiskTableEle.GetCellValue(idx, RiskTableEle.GetCellIndex(riskCell)).AsString();
        }

        [AllureStep]
        public RiskRegisterPage VerifyStep2()
        {
            VerifyElementVisibility(riskTable, true);
            return this;
        }

        [AllureStep]
        public List<string> SelectAllRisk()
        {
            ClickOnElement(selectAllRiskCheckbox);
            SleepTimeInMiliseconds(1000);
            List<string> riskNames = new List<string>();
            int rowCount = RiskTableEle.GetRows().Count;
            for (int i = 0; i < rowCount; i++)
            {
                riskNames.Add(RiskTableEle.GetCellValue(i, RiskTableEle.GetCellIndex(riskCell)).AsString());
            }
            return riskNames;
        }

        [AllureStep]
        public RiskRegisterPage VerifyRiskSelectedTableIsEmpty()
        {
            IsControlUnDisplayed("//div[@id='screen2']//tbody[@data-bind='foreach: selectedRisks']//tr");
            return this;
        }
        #endregion

        #region Step 3
        public readonly By InAllServiceCheckbox = By.XPath("//div[@id='screen3']//input[@id='all-services']");
        public readonly By PreviousButtonOnScreen3 = By.XPath("//div[@id='screen3']//button[text()='Previous']");
        public readonly By NextButtonOnScreen3 = By.XPath("//div[@id='screen3']//button[text()='Next']");
        #endregion

        #region Step 4
        public readonly By FinishButtonOnScreen4 = By.XPath("//div[@id='screen4']//button[text()='Finish']");
        public readonly By FinishConfirmTitle = By.XPath("//div[@role='dialog']//div[@class='bootbox-body']");
        public readonly By OKButton = By.XPath("//div[@role='dialog']//button[text()='OK']");

        private string riskConfirmTable = "//div[@id='screen4']//tbody[@data-bind='foreach: selectedRisks']";
        private string riskConfirmRow = "./tr[@class='text-info']";
        private string riskNameConfirmCell = "./td[@data-bind='text: riskName']";
        private string riskProximityAlertConfirmCell = "./td//input[@data-bind='checked: proximityAlert']";
        private string riskSelectStateDateConfirmCell = "./td//input[@id='start-date']";
        private string riskSelectEndDateConfirmCell = "./td//input[@id='end-date']";
        private string mitigationNoteConfirmCell = "./td//input[@data-bind='value: mitigation']";

        public TableElement RiskConfirmTableEle
        {
            get => new TableElement(riskConfirmTable, riskConfirmRow, new List<string>() { riskNameConfirmCell, riskProximityAlertConfirmCell, riskSelectStateDateConfirmCell, riskSelectEndDateConfirmCell, mitigationNoteConfirmCell });
        }

        [AllureStep]
        public RiskRegisterPage InputRiskConfirm(int rowIdx, string startdate, string endDate)
        {
            RiskConfirmTableEle.SetCellValue(rowIdx, RiskConfirmTableEle.GetCellIndex(riskSelectStateDateConfirmCell), startdate);
            RiskConfirmTableEle.SetCellValue(rowIdx, RiskConfirmTableEle.GetCellIndex(riskSelectEndDateConfirmCell), endDate);
            return this;
        }
        #endregion

        [AllureStep]
        public RiskRegisterPage VerifyIsRiskRegisterPage()
        {
            VerifyElementVisibility(By.XPath(string.Format(stepTitle, "Select Subjects")), true);
            VerifyElementVisibility(By.XPath(string.Format(stepTitle, "Select & Edit Risks")), true);
            VerifyElementVisibility(By.XPath(string.Format(stepTitle, "Select Services (optional)")), true);
            VerifyElementVisibility(By.XPath(string.Format(stepTitle, "Review Risk")), true);
            VerifyElementVisibility(ObjectTypeSelect, true);
            VerifyElementVisibility(ContractSelect, true);
            VerifyElementVisibility(FilterSelect, true);
            return this;
        }
    }
}
