using System;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.SystemTools.SystemMonitoring;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages
{
    public class HomePage : BasePage
    {
        
        private const string CreateEvenDropdownBtn = "//button[contains(text(), 'Create')]/following-sibling::button";

        //CREATE EVEN SUB MENU
        private const string PartyInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Party']";
        private const string AgreementInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Agreement']";
        private const string EventInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Event']";

        private const string PointAddressInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Point Address']";
        private const string ResourceInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Resource']";
        private const string WeighbridgeTicketInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Weighbridge Ticket']";


        private readonly string userNameBtn = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
            ? "//a[@id='DisplayName']"
            : "//li[contains(@class, 'dropdown')]/button";
        private readonly string pageTitle = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
            ? "//div[@class='e_hdr']"
            : "//p[text()='Northstar Environmental Services – Demo 8.6.0']";
        private readonly string searchBtn = "//span[text()='Search']/parent::button";
        private readonly string logoutBtn = "//a[text()='Log out']";

        //IE ONLY
        private readonly string systemTool = "//img[@title='System Tools']";
        private readonly string systemMonitoring = "//span[contains(text(),'System Monitoring')]/../../descendant::img";
        private readonly string emailOption = "//span[contains(text(),'Emails')]";
        private readonly string userAndRole = "//table[@class='ntn']/descendant::img";
        private readonly string user = "//table[@typename='User']/descendant::img";
        private readonly string group = "//table[@groupkey='t']/descendant::span";
        private readonly string groupExpand = "//table[@groupkey='t']/descendant::img";
        private readonly string userNameValue = "//span[contains(text(),'{0}')]";

        public HomePage IsOnHomePage(User user)
        {
            Thread.Sleep(500);
            SwitchToDefaultContent();
            WaitUtil.WaitForElementVisible(pageTitle);
            if (!ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE))
            {
                WaitUtil.WaitForElementVisible(searchBtn);
            }
            Assert.AreEqual(GetElementText(userNameBtn).Trim(), user.DisplayName);
            return this;
        }
        public HomePage ClickUserNameDd()
        {
            ClickOnElement(userNameBtn);
            return this;
        }
        public HomePage ClickLogoutBtn()
        {
            ClickOnElement(logoutBtn);
            return this;
        }
        public HomePage ClickSystemTool()
        {
            ClickOnElement(systemTool);
            return this;
        }
        public HomePage ClickUserAndRole()
        {
            ClickOnElement(userAndRole);
            return this;
        }
        public HomePage ClickUser()
        {
            ClickOnElement(user);
            return this;
        }
        public HomePage ClickCreateEventDropdownAndVerify()
        {
            ClickOnElement(CreateEvenDropdownBtn);
            Assert.IsTrue(IsControlDisplayed(PartyInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(AgreementInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(EventInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(PointAddressInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(ResourceInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(WeighbridgeTicketInCreateEvenBtn));
            return this;
        }
        public CreatePartyPage GoToThePatiesByCreateEvenDropdown()
        {
            ClickOnElement(PartyInCreateEvenBtn);
            return new CreatePartyPage();
        }
        public UserPage ClickGroup()
        {
            ClickOnElement(group);
            return PageFactoryManager.Get<UserPage>();
        }
        public HomePage ExpandGroup()
        {
            ClickOnElement(groupExpand);
            return PageFactoryManager.Get<HomePage>();
        }
        public HomePage ClickSysMonitoring()
        {
            ClickOnElement(systemMonitoring);
            return this;
        }
        public EmailPage ClickEmail()
        {
            ClickOnElement(emailOption);
            return PageFactoryManager.Get<EmailPage>();
        }
        public UserDetailPage ClickUserName(string userName)
        {
            ClickOnElement(String.Format(userNameValue, userName));
            return PageFactoryManager.Get<UserDetailPage>();
        }
    }
}
