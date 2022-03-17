using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.SystemTools.SystemMonitoring;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;

namespace si_automated_tests.Source.Main.Pages
{
    public class HomePage : BasePage
    {
        //HEADER
        private const string UserNameBtn = "//li[contains(@class, 'dropdown')]/button";
        private const string PageTitle = "//p[text()='Northstar Environmental Services – Demo 8.6.0-dev01']";
        private const string SearchBtn = "//span[text()='Search']/parent::button";
        private const string LogoutBtn = "//a[text()='Log out']";
        private const string CreateEvenDropdownBtn = "//button[contains(text(), 'Create')]/following-sibling::button";

        //CREATE EVEN SUB MENU
        private const string PartyInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Party']";
        private const string AgreementInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Agreement']";
        private const string EventInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Event']";

        private const string PointAddressInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Point Address']";
        private const string ResourceInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Resource']";
        private const string WeighbridgeTicketInCreateEvenBtn = "//button[contains(text(), 'Create')]/following-sibling::ul//a[text()='Weighbridge Ticket']";

        //MENU
        private const string PatiesMenu = "//span[text()='Parties']/parent::h4/parent::div";
        private const string ServicesMenu = "//span[text()='Services']/parent::h4/parent::div";

        //SUB MENU
        private const string NorthStartCommercialMenu = "//span[text()='North Star Commercial']/parent::a/preceding-sibling::span[2]";

        //SUB SUB MENU
        private const string PartiesSubSubMenu = "//span[text()='Parties']/parent::a";
        private const string AgreementSubSubMenu = "//span[text()='Agreements']/parent::a";
        private const string SiteServicesSubSubMenu = "//span[text()='Site Services']/parent::a";
        private readonly string userNameBtn = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
            ? "//a[@id='DisplayName']"
            : "//li[contains(@class, 'dropdown')]/button";
        private readonly string pageTitle = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
            ? "//div[@class='e_hdr']"
            : "//p[text()='Northstar Environmental Services – Demo 8.6.0-dev01']";
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
        public PartyNavigation ClickParties()
        {
            ClickOnElement(PatiesMenu);
            return PageFactoryManager.Get<PartyNavigation>();
        }
        public HomePage ClickServices()
        {
            ClickOnElement(ServicesMenu);
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
        public HomePage ClickTitle()
        {
            ClickOnElement(pageTitle);
            return this;
        }






        private readonly string regions = "//span[text()='Regions']/parent::a/preceding-sibling::span[2]";
        private readonly string london = "//span[text()='London']/parent::a/preceding-sibling::span[2]";
        private readonly string northStarComercial = "//span[text()='North Star Commercial']/parent::a/preceding-sibling::span[2]";
        private readonly string collections = "//span[text()='Collections']/parent::a/preceding-sibling::span[2]";
        private readonly string comercialCollection = "//span[text()='Commercial Collections']/parent::a/preceding-sibling::span[2]";
        private readonly string activeServiceTask = "//span[text()='Active Service Tasks']";

        public HomePage GoToActiveServiceTask()
        {
            WaitUtil.WaitForElementVisible(regions);
            ClickOnElement(regions);
            WaitUtil.WaitForElementVisible(london);
            ClickOnElement(london);
            WaitUtil.WaitForElementVisible(northStarComercial);
            ClickOnElement(northStarComercial);
            WaitUtil.WaitForElementVisible(collections);
            ClickOnElement(collections);
            WaitUtil.WaitForElementVisible(comercialCollection);
            ClickOnElement(comercialCollection);
            WaitUtil.WaitForElementVisible(activeServiceTask);
            ClickOnElement(activeServiceTask);
            return this;
        }
    }
}
