using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Paties.Sites
{
    public class SitePage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//span[text()='Serviced Site']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By RequiredQualificationTab = By.XPath("//a[@aria-controls='requiredQualifications-tab']");
        public readonly By LockCheckbox = By.XPath("//input[@id='locked']");
        public readonly By LockReferenceInput = By.XPath("//input[@id='lockReference']");
        public readonly By LockHelpButton = By.XPath("//span[contains(@class, 'lock-help')]");
        public readonly By LockHelpContent = By.XPath("//div[contains(@class, 'popover-content')]");
        public readonly By SiteAbvInput = By.XPath("//input[@id='site-abv']");
        public readonly By SiteAddressTitle = By.XPath("//p[@class='object-name']");
        private readonly By accountingRefInput = By.CssSelector("input[id='account-reference']");

        #region Required Qualification
        public readonly By AddQualificationButton = By.XPath("//div[@id='requiredQualifications-tab']//button[@title='Add New Item']");
        public readonly By QualificationTable = By.XPath("//div[@id='requiredQualifications-tab']//div[@class='grid-canvas']");
        public readonly By ToogleSelectQualificationButton = By.XPath("//div[@id='requiredQualifications-tab']//button[@data-id='qualification-constraints']");
        public readonly By ConfirmAddQualificationButton = By.XPath("//div[@id='requiredQualifications-tab']//button[@title='Confirm']");
        public readonly By CancelAddQualificationButton = By.XPath("//div[@id='requiredQualifications-tab']//button[@title='Cancel']");
        public readonly By ExpandedListbox = By.XPath("//ul[@role='listbox' and @aria-expanded='true']");

        private string qualificationTable = "//div[@id='requiredQualifications-tab']//div[@class='grid-canvas']";
        private string qualificationRow = "./div[contains(@class, 'slick-row')]";
        private string idQCell = "./div[@class='slick-cell l0 r0']";
        private string constraintCell = "./div[@class='slick-cell l1 r1']";
        private string startDateCell = "./div[@class='slick-cell l2 r2']";
        private string endDateCell = "./div[@class='slick-cell l3 r3']";
        private string retireButtonCell = "./div[@class='slick-cell l4 r4']";

        public TableElement QualificationTableEle
        {
            get => new TableElement(qualificationTable, qualificationRow, new System.Collections.Generic.List<string>() { idQCell, constraintCell, startDateCell, endDateCell, retireButtonCell });
        }

        [AllureStep]
        public SitePage VerifyNewQualification(string constraint, string startDate, string endDate)
        {
            VerifyCellValue(QualificationTableEle, 0, QualificationTableEle.GetCellIndex(constraintCell), constraint);
            VerifyCellValue(QualificationTableEle, 0, QualificationTableEle.GetCellIndex(startDateCell), startDate);
            VerifyCellValue(QualificationTableEle, 0, QualificationTableEle.GetCellIndex(endDateCell), endDate);
            return this;
        }

        [AllureStep]
        public SitePage VerifyRetiredQualification(string id)
        {
            Assert.IsNull(QualificationTableEle.GetCellByValue(QualificationTableEle.GetCellIndex(idQCell), id));
            return this;
        }

        [AllureStep]
        public string ClickRetireQualification(int rowIdx)
        {
            string id = QualificationTableEle.GetCellValue(rowIdx, QualificationTableEle.GetCellIndex(idQCell)).ToString();
            QualificationTableEle.ClickCell(rowIdx, QualificationTableEle.GetCellIndex(retireButtonCell));
            return id;
        }
        #endregion

        [AllureStep]
        public SitePage IsSiteDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        [AllureStep]
        public SitePage ClickOnDetailTab()
        {
            ClickOnElement(DetailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public SitePage InputAccountingRef(string accountingRefValue)
        {
            SendKeys(accountingRefInput, accountingRefValue);
            return this;
        }

        [AllureStep]
        public SitePage RemoveAccountingRef()
        {
            ClearInputValue(accountingRefInput);
            return this;
        }

        [AllureStep]
        public SitePage VerifyAccountingRefAfterSaving(string accountingRefValue)
        {
            Assert.AreEqual(accountingRefValue, GetAttributeValue(accountingRefInput, "value"));
            return this;
        }
    }
}
