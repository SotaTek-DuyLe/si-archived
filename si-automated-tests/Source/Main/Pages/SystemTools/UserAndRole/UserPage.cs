using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.UserAndRole
{
    public class UserPage : BasePage
    {
        private readonly By actionBtn = By.XPath("//span[contains(text(),'Actions')]");
        private readonly By newAction = By.XPath("//td[@id='mo_0']");
        private readonly By rightFrame = By.XPath("//iframe[@id='RightFrame']");
        private readonly By moveNextBtn = By.XPath("//img[@title='Move Next']/parent::td");
        private const string anyUserInList = "//span[text()='{0}']/parent::div";

        public UserPage IsOnUserScreen()
        {
            SwitchToFirstWindow();
            SwitchToFrame(rightFrame);
            WaitUtil.WaitForElementVisible(actionBtn);
            return this;
        }
        public UserPage ClickAction()
        {
            ClickOnElement(actionBtn);
            return this;
        }
        public UserPage ClickNew()
        {
            ClickOnElement(newAction);
            return this;
        }

        public UserPage ClickMoveNextBtn()
        {
            ClickOnElement(moveNextBtn);
            return this;
        }

        public UserDetailPage ClickAnyUserShowDetail(string userName)
        {
            DoubleClickOnElement(anyUserInList, userName);
            return PageFactoryManager.Get<UserDetailPage>();
        }
    }
}
