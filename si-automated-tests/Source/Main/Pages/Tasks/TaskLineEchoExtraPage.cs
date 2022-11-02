using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.Applications;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TaskLineEchoExtraPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//span[contains(string(), 'TaskLineWorkFlow')]");
        private readonly By statesTab = By.XPath("//a[text()='States']/parent::li");
        private readonly By saveRoleBtn = By.CssSelector("img[title='Save']");

        //DYNAMIC
        private readonly string taskLineName = "//span[contains(string(),'{0}')]";
        private readonly string sortOrderInputAtAnyRow = "//tr[@itemtype='TaskLineState'][{0}]/td[count(//tr[@class='GRID_HD_ROW']//div[contains(string(), 'Sort Order')]/parent::td/preceding-sibling::td) + 1]//input";

        [AllureStep]
        public TaskLineEchoExtraPage WaitForTaskLineEchoExtraPageDisplayed(string taskLineNameValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(taskLineName, taskLineNameValue);
            return this;
        }

        [AllureStep]
        public TaskLineEchoExtraPage ClickOnStatesTab()
        {
            ClickOnElement(statesTab);
            return this;
        }

        [AllureStep]
        public TaskLineEchoExtraPage InputNumberInSortOrder(string numberOfRow, string value)
        {
            ClearInputValue(string.Format(sortOrderInputAtAnyRow, numberOfRow));
            if (!value.Equals("0"))
            {
                SendKeys(string.Format(sortOrderInputAtAnyRow, numberOfRow), value);
            }
            return this;
        }

        [AllureStep]
        public TaskLineEchoExtraPage ClickSaveBtnToUpdateTaskLine()
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
