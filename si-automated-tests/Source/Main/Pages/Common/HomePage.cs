using System;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.SystemTools.SystemMonitoring;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using si_automated_tests.Source.Main.Models;
using OpenQA.Selenium;
using NUnit.Allure.Attributes;

namespace si_automated_tests.Source.Main.Pages
{
    public class HomePage : BasePage
    {
        
        private const string CreateEvenDropdownBtn = "//button[contains(text(), 'Create Agreement')]/following-sibling::button";

        //CREATE EVEN SUB MENU
        private const string PartyInCreateEvenBtn = "//button[text()='Create Agreement']/following-sibling::ul//span[text()='Party']";
        private const string AgreementInCreateEvenBtn = "//button[text()='Create Agreement']/following-sibling::ul//span[text()='Agreement']";
        private const string EventInCreateEvenBtn = "//button[text()='Create Agreement']/following-sibling::ul//span[text()='Event']";
        private const string PointAddressInCreateEvenBtn = "//button[text()='Create Agreement']/following-sibling::ul//span[text()='Point Address']";
        private const string ResourceInCreateEvenBtn = "//button[text()='Create Agreement']/following-sibling::ul//span[text()='Resource']";
        private const string ServiceUnitGroupBtn = "//button[text()='Create Agreement']/following-sibling::ul//span[text()='Service Unit Group']";
        private const string WeighbridgeTicketInCreateEvenBtn = "//button[text()='Create Agreement']/following-sibling::ul//span[text()='Weighbridge Ticket']";


        private readonly string userNameBtn = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
            ? "//a[@id='DisplayName']"
            : "//li[contains(@class, 'dropdown')]/button";
        private readonly string pageTitle = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
            ? "//div[@class='e_hdr']"
            : "//p[@class='navbar-text environment-logo']";
        private readonly string searchBtn = "//span[text()='Search']/parent::button";
        private readonly string logoutBtn = "//a[text()='Log out']";

        //SEARCH MODEL
        private readonly By searchInput = By.CssSelector("h4#search-header");
        private readonly By sectorDd = By.CssSelector("select#sector");
        private readonly By uprnInput = By.CssSelector("input#uprn");
        private readonly By postCodeInput = By.CssSelector("input#postcode");
        private readonly By propertyInput = By.CssSelector("input#property");
        private readonly By streetInput = By.CssSelector("input#street");
        private readonly By searchInPopupBtn = By.XPath("//button[text()='Search']");
        private readonly By cancelInPopupBtn = By.XPath("//button[text()='Search']/preceding-sibling::button[text()='Cancel']");
        private readonly By closeInPopupBtn = By.XPath("//h4[text()='Search for...']/parent::div/following-sibling::div//button[@aria-label='Close']");
        private const string richomndCommercialOption = "//optgroup/option[text()='Richmond Commercial']";
        private const string richomndOption = "//optgroup/option[text()='Richmond']";
        private const string searchForOption = "//input[@value='{0}']";

        //IE ONLY
        private readonly string systemTool = "//img[@title='System Tools']";
        private readonly string systemMonitoring = "//span[contains(text(),'System Monitoring')]/../../descendant::img";
        private readonly string emailOption = "//span[contains(text(),'Emails')]";
        private readonly string userAndRole = "//table[@class='ntn']/descendant::img";
        private readonly string user = "//table[@typename='User']/descendant::img";
        private readonly string group = "//table[@groupkey='t']/descendant::span";
        private readonly string groupExpand = "//table[@groupkey='t']/descendant::img";
        private readonly string userNameValue = "//span[contains(text(),'{0}')]";
        private readonly string anyGroupUser = "//table[@groupkey='{0}']/descendant::span";

        [AllureStep]
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

        [AllureStep]
        public HomePage IsOnHomePageWithoutWaitSearchBtn(User user)
        {
            Thread.Sleep(500);
            SwitchToDefaultContent();
            WaitUtil.WaitForElementVisible(pageTitle);
            Assert.AreEqual(GetElementText(userNameBtn).Trim(), user.DisplayName);
            return this;
        }

        [AllureStep]
        public HomePage ClickOnSearchBtn()
        {
            ClickOnElement(searchBtn);
            return this;
        }
        [AllureStep]
        public HomePage ClickUserNameDd()
        {
            ClickOnElement(userNameBtn);
            return this;
        }
        [AllureStep]
        public HomePage ClickLogoutBtn()
        {
            ClickOnElement(logoutBtn);
            return this;
        }
        [AllureStep]
        public HomePage ClickSystemTool()
        {
            ClickOnElement(systemTool);
            return this;
        }
        [AllureStep]
        public HomePage ClickUserAndRole()
        {
            ClickOnElement(userAndRole);
            return this;
        }
        [AllureStep]
        public HomePage ClickUser()
        {
            ClickOnElement(user);
            return this;
        }
        [AllureStep]
        public HomePage ClickCreateEventDropdownAndVerify()
        {
            ClickOnElement(CreateEvenDropdownBtn);
            Assert.IsTrue(IsControlDisplayed(PartyInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(AgreementInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(EventInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(PointAddressInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(ResourceInCreateEvenBtn));
            Assert.IsTrue(IsControlDisplayed(ServiceUnitGroupBtn));
            Assert.IsTrue(IsControlDisplayed(WeighbridgeTicketInCreateEvenBtn));
            return this;
        }
        [AllureStep]
        public CreatePartyPage GoToThePatiesByCreateEvenDropdown()
        {
            ClickOnElement(PartyInCreateEvenBtn);
            return new CreatePartyPage();
        }
        [AllureStep]
        public UserPage ClickGroup()
        {
            ClickOnElement(group);
            return PageFactoryManager.Get<UserPage>();
        }
        [AllureStep]
        public HomePage ExpandGroup()
        {
            ClickOnElement(groupExpand);
            return PageFactoryManager.Get<HomePage>();
        }
        [AllureStep]
        public HomePage ClickSysMonitoring()
        {
            ClickOnElement(systemMonitoring);
            return this;
        }
        [AllureStep]
        public EmailPage ClickEmail()
        {
            ClickOnElement(emailOption);
            return PageFactoryManager.Get<EmailPage>();
        }
        [AllureStep]
        public UserDetailPage ClickUserName(string userName)
        {
            ClickOnElement(String.Format(userNameValue, userName));
            return PageFactoryManager.Get<UserDetailPage>();
        }
        [AllureStep]
        public UserPage ClickAnyGroup(string groupName)
        {
            ClickOnElement(anyGroupUser, groupName);
            return PageFactoryManager.Get<UserPage>();
        }

        //SEARCH MODEL
        [AllureStep]
        public HomePage IsSearchModel()
        {
            WaitUtil.WaitForElementVisible(searchInput);
            Assert.IsTrue(IsControlDisplayed(searchInput));
            Assert.IsTrue(IsControlDisplayed(sectorDd));
            Assert.IsTrue(IsControlDisplayed(uprnInput));
            Assert.IsTrue(IsControlDisplayed(postCodeInput));
            Assert.IsTrue(IsControlDisplayed(propertyInput));
            Assert.IsTrue(IsControlDisplayed(streetInput));
            Assert.IsTrue(IsControlEnabled(searchInPopupBtn));
            Assert.IsTrue(IsControlEnabled(cancelInPopupBtn));
            Assert.IsTrue(IsControlDisplayed(closeInPopupBtn));
            return this;
        }
        [AllureStep]
        public HomePage ClickAnySearchForOption(string option)
        {
            ClickOnElement(searchForOption, option);
            return this;
        }
        [AllureStep]
        public HomePage ClickAndSelectRichmondCommercialSectorValue()
        {
            ClickOnElement(sectorDd);
            ClickOnElement(richomndCommercialOption);
            return this;
        }
        [AllureStep]
        public HomePage ClickAndSelectRichmondSectorValue()
        {
            ClickOnElement(sectorDd);
            ClickOnElement(richomndOption);
            return this;
        }
        [AllureStep]
        public HomePage ClickOnSearchBtnInPopup()
        {
            ClickOnElement(searchInPopupBtn);
            return this;
        }

    }
}
