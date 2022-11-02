using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TaskLineDetailPage : BasePageCommonActions
    {
        private readonly By titleTask = By.XPath("//h4[text()='Task']");
        private readonly By titleTaskLine = By.XPath("//h4[text()='Task Lines']");
        public readonly By DetailTab = By.CssSelector("a[aria-controls='details-tab']");
        public readonly By HistoryTab = By.CssSelector("a[aria-controls='history-tab']");
        public readonly By ProductSelect = By.XPath("//div[@id='details-tab']//echo-select[contains(@params, 'product')]//select");
        public readonly By MinAssetQty = By.XPath("//div[@id='details-tab']//input[@id='min-asset-qty']");
        public readonly By MaxAssetQty = By.XPath("//div[@id='details-tab']//input[@id='max-asset-qty']");
        public readonly By MinProductQty = By.XPath("//div[@id='details-tab']//input[@id='min-product-qty']");
        public readonly By MaxProductQty = By.XPath("//div[@id='details-tab']//input[@id='max-product-qty']");
        public readonly By StateSelect = By.XPath("//div[@id='details-tab']//select[@id='state']");
        public readonly By HistoryDetail = By.XPath("//div[@id='history-tab']//div[@data-bind='html: ew.renderChangesHtml(changes)'][1]");
        public readonly By CompleteDateInput = By.XPath("//div[@id='details-tab']//input[@id='completed-date']");

        #region DETAIL TAB
        private readonly By stateDd = By.XPath("//div[@id='details-tab']//select[@id='state']");
        private readonly string stateOption = "//div[@id='details-tab']//select[@id='state']/option[{0}]";

        [AllureStep]
        public TaskLineDetailPage IsTaskLineDetailPage()
        {
            WaitUtil.WaitForElementVisible(titleTask);
            WaitUtil.WaitForElementVisible(titleTaskLine);
            return this;
        }

        [AllureStep]
        public TaskLineDetailPage ClickOnDetailTab()
        {
            ClickOnElement(DetailTab);
            return this;
        }

        [AllureStep]
        public TaskLineDetailPage VerifyStatusInStateDropdown(string stateValue)
        {
            Assert.AreEqual(stateValue, GetFirstSelectedItemInDropdown(stateDd), "State in [State] dd is not correct");
            return this;
        }

        [AllureStep]
        public TaskLineDetailPage ClickOnStateDdAndVerify(string[] taskStateValues)
        {
            ClickOnElement(stateDd);
            //Verify
            for (int i = 0; i < taskStateValues.Length; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(stateOption, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }
        #endregion

        [AllureStep]
        public string GetTaskLineId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/task-lines/", "");
        }

    }
}
