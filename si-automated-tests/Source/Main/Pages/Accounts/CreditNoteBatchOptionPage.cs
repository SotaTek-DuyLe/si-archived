using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreditNoteBatchOptionPage : BasePage
    {
        private readonly By newNoteBatchOption = By.XPath("//button[text()='New Credit Note Batch']");
        private readonly By existingNoteBatchOption = By.XPath("//button[text()='Existing Credit Note Batch']");
        private readonly By cancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By confirmBtn = By.XPath("//button[text()='Confirm']");
        private readonly string radioBtn = "//td[@data-bind='text: batchId' and text()='{0}']/preceding-sibling::td/input";

        public CreditNoteBatchOptionPage()
        {
            IsOnBatchPage();
        }
        public CreditNoteBatchOptionPage IsOnBatchPage()
        {
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible(newNoteBatchOption);
            WaitUtil.WaitForElementVisible(existingNoteBatchOption);
            WaitUtil.WaitForElementVisible(cancelBtn);
            return this;
        }
        public CreditNoteBatchPage ClickNewBatch()
        {
            ClickOnElement(newNoteBatchOption);
            return new CreditNoteBatchPage();
        }
        public CreditNoteBatchOptionPage ClickExistingBatch()
        {
            ClickOnElement(existingNoteBatchOption);
            return this;
        }
        public CreditNoteBatchOptionPage SelectBatchId(string batchId)
        {
            ClickOnElement(radioBtn, batchId);
            ClickOnElement(confirmBtn);
            return this;
        }
    }
}
