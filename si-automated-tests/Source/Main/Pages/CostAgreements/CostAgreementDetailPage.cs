using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.CostAgreements
{
    public class CostAgreementDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Cost Agreement']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");
        private readonly By costBooksTab = By.CssSelector("a[aria-controls='costBooks-tab']");
        private readonly By sectorsTab = By.CssSelector("a[aria-controls='costAgreementSectors-tab']");
        private readonly By costAgreementTypeDd = By.CssSelector("select[id='agreementType.id']");
        private readonly By approveBtn = By.XPath("//button[text()='Approve']");
        private readonly By isApprovedCheckbox = By.XPath("//label[contains(string(), 'Is Approved')]/following-sibling::input");
        private readonly By startDateInput = By.CssSelector("input[id='startDate.id']");

        //Data tab
        private readonly By notesInput = By.XPath("//div[@id='data-tab']//label[text()='Notes']/following-sibling::input");

        //DYNAMIC
        private readonly string costAgreementTypeOption = "//select[@id='agreementType.id']/option[text()='{0}']";

        [AllureStep]
        public CostAgreementDetailPage IsCostAgreementDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage SelectCostAgreementType(string costAgreementTypeValue)
        {
            ClickOnElement(costAgreementTypeDd);
            ClickOnElement(costAgreementTypeOption, costAgreementTypeValue);
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage ClickOnApproveBtn()
        {
            ClickOnElement(approveBtn);
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage VerifyIsApprovedCheckboxCheckedAndDisabled()
        {
            Assert.IsTrue(IsCheckboxChecked(isApprovedCheckbox), "[Is Approved] is not checked");
            Assert.AreEqual(GetAttributeValue(isApprovedCheckbox, "disabled"), "true", "[Is Approved] is not disabled");
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage VerifyStartDateValueAndDisabled(string startDateValue)
        {
            Assert.AreEqual(GetAttributeValue(startDateInput, "disabled"), "true", "[Start Date] is not disabled");
            Assert.AreEqual(GetAttributeValue(startDateInput, "value"), startDateValue);
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage VerifyApprovedBtnIsNotDisplayed()
        {
            Assert.IsTrue(IsControlUnDisplayed(approveBtn), "[Approved] btn is displayed");
            return this;
        }

        [AllureStep]
        public string GetCostAgreementId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/cost-agreements/", "");
        }

        [AllureStep]
        public CostAgreementDetailPage ClickOnDataTabAndVerifyDisplayNotes()
        {
            ClickOnElement(dataTab);
            WaitForLoadingIconToDisappear();
            Assert.IsTrue(IsControlDisplayed(notesInput));
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage VerifyDisplayCostBooksTab()
        {
            Assert.IsTrue(IsControlDisplayed(costBooksTab));
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage VerifyDisplaySectorsTab()
        {
            Assert.IsTrue(IsControlDisplayed(sectorsTab));
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage VerifyStartdateIsEditable()
        {
            Assert.IsTrue(IsControlEnabled(startDateInput));
            return this;
        }
    }
}
