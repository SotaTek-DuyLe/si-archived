using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class ResoureDetailPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//h4[text()='RESOURCE']");

        [AllureStep]
        public ResoureDetailPage IsResourceDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }
        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Resource?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");
        private readonly By ThirdPartyCheckbox = By.XPath("//input[@id='third-party']");
        public readonly By SupplierSelect = By.XPath("//select[@id='supplier']");

        public ResoureDetailPage SelectThirdPartyCheckbox(bool isSelect)
        {
            if (isSelect)
            {
                if (!GetCheckboxValue(ThirdPartyCheckbox)) ClickOnElement(ThirdPartyCheckbox);
            }
            else
            {
                if (GetCheckboxValue(ThirdPartyCheckbox)) ClickOnElement(ThirdPartyCheckbox);
            }
            return this;
        }
        #endregion

        #region Shift Schedule tab
        public readonly By ShiftScheduleTab = By.XPath("//a[@aria-controls='shiftSchedules-tab']");
        public readonly By AddNewShiftScheduleButton = By.XPath("//div[@id='shiftSchedules-tab']//button[text()='Add New Item']");
        #endregion

        #region Required Qualification
        public readonly By QualificatoinTab = By.XPath("//a[@aria-controls='resourceQualifications-tab']");
        public readonly By AddQualificationButton = By.XPath("//div[@id='resourceQualifications-tab']//button[@title='Add New Item']");
        public readonly By QualificationTable = By.XPath("//div[@id='resourceQualifications-tab']//div[@class='grid-canvas']");
        public readonly By SelectQualification = By.XPath("//div[@id='qualification-modal']//select[@id='qualifications.id']");
        public readonly By ConfirmAddQualificationButton = By.XPath("//div[@id='qualification-modal']//button[@title='Confirm']");
        public readonly By CancelAddQualificationButton = By.XPath("//div[@id='qualification-modal']//button[@title='Cancel']");
        public readonly By EffectiveDateInput = By.Id("effectiveDate.id");
        public readonly By ExpiredDateInput = By.Id("expiryDate.id");

        private string qualificationTable = "//div[@id='resourceQualifications-tab']//div[@class='grid-canvas']";
        private string qualificationRow = "./div[contains(@class, 'slick-row')]";
        private string idQCell = "./div[contains(@class, 'slick-cell l0 r0')]";
        private string constraintCell = "./div[contains(@class, 'slick-cell l1 r1')]";
        private string startDateCell = "./div[contains(@class, 'slick-cell l2 r2')]";
        private string endDateCell = "./div[contains(@class, 'slick-cell l3 r3')]";
        private string photoCell = "./div[contains(@class, 'slick-cell l4 r4')]";
        private string retireButtonCell = "./div[contains(@class, 'slick-cell l5 r5')]";

        public TableElement QualificationTableEle
        {
            get => new TableElement(qualificationTable, qualificationRow, new System.Collections.Generic.List<string>() { idQCell, constraintCell, startDateCell, endDateCell, photoCell, retireButtonCell });
        }

        [AllureStep]
        public ResoureDetailPage VerifyNewQualification(string constraint, string startDate, string endDate)
        {
            VerifyCellValue(QualificationTableEle, 0, QualificationTableEle.GetCellIndex(constraintCell), constraint);
            VerifyCellValue(QualificationTableEle, 0, QualificationTableEle.GetCellIndex(startDateCell), startDate);
            VerifyCellValue(QualificationTableEle, 0, QualificationTableEle.GetCellIndex(endDateCell), endDate);
            return this;
        }

        [AllureStep]
        public ResoureDetailPage VerifyRetiredQualification(string id)
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
        public ResoureDetailPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectResource)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ResoureDetailPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public ResoureDetailPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public ResoureDetailPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
