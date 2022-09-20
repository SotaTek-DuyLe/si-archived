using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RiskRegisterPage : BasePageCommonActions
    {
        public readonly string RiskTable = "//div[@id='risks-grid']//div[@class='grid-canvas']";
        public readonly string RiskRow = "./div[contains(@class, 'slick-row ')]";
        public readonly string RiskCheckboxCell = "./div[contains(@class, 'l0')]//input";
        public readonly string RiskNameCell = "./div[contains(@class, 'l1')]";
        public readonly string RiskLevelCell = "./div[contains(@class, 'l2')]";

        public TableElement RiskTableEle
        {
            get => new TableElement(RiskTable, RiskRow, new List<string>() { RiskCheckboxCell, RiskNameCell, RiskLevelCell });
        }

        public readonly By AddSelectedButton = By.XPath("//button[text()[contains(.,'Add Selected')]]");
        public readonly By NextButtonOnEditRisk = By.XPath("//div[@id='screen2']//button[text()[contains(.,'Next')]]");
        public readonly By NextButtonOnSelectServices = By.XPath("//div[@id='screen3']//button[text()[contains(.,'Next')]]");
        public readonly By InAllServiceCheckbox = By.XPath("//div[@id='screen3']//input[@id='all-services']");
        public readonly By FinishButton = By.XPath("//div[@id='screen4']//button[text()[contains(.,'Finish')]]");
        public readonly By OKButton = By.XPath("//div[@class='modal-dialog']//button[text()[contains(.,'OK')]]");

        public readonly string ReviewRiskTable = "//div[@id='screen4']//table//tbody";
        public readonly string ReviewRiskRow = "./tr";
        public readonly string ReviewRiskNameCell = "./td[@data-bind='text: riskName']";
        public readonly string ReviewRiskLevelCell = "./td[@data-bind='text: riskLevel']";
        public readonly string ReviewRiskTypeCell = "./td[@data-bind='text: riskType']";
        public readonly string ReviewRiskProximityAlertCell = "./td//input";
        public readonly string ReviewRiskStartDateCell = "./td//input[@id='start-date']";
        public readonly string ReviewRiskEndDateCell = "./td//input[@id='end-date']";

        public TableElement ReviewRiskTableEle
        {
            get => new TableElement(ReviewRiskTable, ReviewRiskRow, new List<string>() { ReviewRiskNameCell, ReviewRiskLevelCell, ReviewRiskTypeCell, ReviewRiskProximityAlertCell, ReviewRiskStartDateCell, ReviewRiskEndDateCell });
        }

        [AllureStep]
        public RiskRegisterPage SelectRiskCheckbox(int rowIdx)
        {
            RiskTableEle.ClickCell(rowIdx, 0);
            return this;
        }
        [AllureStep]
        public RiskRegisterModel GetReviewRiskData()
        {
            return new RiskRegisterModel()
            {
                Risk = ReviewRiskTableEle.GetCellValue(0, 0).AsString(),
                Level = ReviewRiskTableEle.GetCellValue(0, 1).AsString(),
                Type = ReviewRiskTableEle.GetCellValue(0, 2).AsString(),
                ProximityType = ReviewRiskTableEle.GetCellValue(0, 3).AsBool(),
                StartDate = ReviewRiskTableEle.GetCellValue(0, 4).AsString(),
                EndDate = ReviewRiskTableEle.GetCellValue(0, 5).AsString(),
            };
        }
    }
}
