using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.Applications;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TaskTypeEchoExtraPage : BasePageCommonActions
    {
        public readonly By TabPage = By.XPath("//div[@class='tabstrip_s1']/ul");
        private readonly By statesTab = By.XPath("//a[text()='States']/parent::li");
        private readonly By taskTypeTitle = By.XPath("//span[contains(string(), 'TaskType')]");
        private readonly By saveRoleBtn = By.CssSelector("img[title='Save']");

        //DYNAMIC
        private readonly string taskTypeName = "//span[contains(string(),'{0}')]";
        private readonly string sortOrderInputAtAnyRow = "//tr[@itemtype='TaskState'][{0}]/td[count(//tr[@class='GRID_HD_ROW']//div[contains(string(), 'Sort Order')]/parent::td/preceding-sibling::td) + 1]//input";

        public By GetCompleteTaskTypeStateCheckboxXPath(string echoId)
        {
            return By.XPath($"//div[@tpfx='TabPage_' and contains(@style, 'visibility: visible')]//table//tbody//tr[@echoid='{echoId}']//td[4]//input");
        }

        public By GetNotCompleteTaskTypeStateCheckboxXPath(string echoId)
        {
            return By.XPath($"//div[@tpfx='TabPage_' and contains(@style, 'visibility: visible')]//table//tbody//tr[@echoid='{echoId}']//td[4]//input");
        }

        [AllureStep]
        public TaskTypeEchoExtraPage IsTaskTypePage(string taskTypeNameValue)
        {
            WaitUtil.WaitForElementVisible(taskTypeTitle);
            WaitUtil.WaitForElementVisible(taskTypeName, taskTypeNameValue);
            return this;
        }

        [AllureStep]
        public TaskTypeEchoExtraPage ClickOnStatesTab()
        {
            ClickOnElement(statesTab);
            return this;
        }

        [AllureStep]
        public TaskTypeEchoExtraPage InputNumberInSortOrder(string numberOfRow, string value)
        {
            ClearInputValue(string.Format(sortOrderInputAtAnyRow, numberOfRow));
            if(!value.Equals("0"))
            {
                SendKeys(string.Format(sortOrderInputAtAnyRow, numberOfRow), value);
            }
            return this;
        }

        [AllureStep]
        public TaskTypeEchoExtraPage ClickSaveBtnToUpdateTaskType()
        {
            ClickOnElement(saveRoleBtn);
            AcceptAlertIfAny();
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage NavigateToRoundInstanceDetailPage(string roundInstanceId)
        {
            GoToURL(WebUrl.MainPageUrl + "web/round-instances/" + roundInstanceId);
            return PageFactoryManager.Get<RoundInstanceDetailPage>();
        }
    }
}
