using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceDetailPage : BasePageCommonActions
    {
        private readonly By serviceTitle = By.XPath("//span[text()='Service']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By attributeTab = By.CssSelector("a[aria-controls='attributes-tab']");
        private readonly By notificationActiveCheckbox = By.XPath("//label[contains(string(), 'Notifications Active')]/following-sibling::input");
        private readonly By advancedNotificationPeriodDd = By.XPath("//label[contains(string(), 'Advanced Notification Period')]/following-sibling::select");
        public readonly By DynamicOptimisationLabel = By.XPath("//label[contains(text(), 'Dynamic optimisation')]");
        public readonly By DynamicOptimisationCheckbox = By.XPath("//input[contains(@data-bind, 'isDynamicOptimisation')]");
        public readonly By DynamicOptimisationHelpButton = By.XPath("//div[contains(@data-bind, 'isDynamicOptimisation')]//span[@role='button']");
        public readonly By DynamicOptimisationTooltip = By.XPath("//div[contains(@data-bind, 'isDynamicOptimisation')]//div[@role='tooltip']");

        //DYNAMIC
        public readonly string executeDayOption = "//label[contains(string(), 'Exclude Days')]/parent::label/following-sibling::ul//span[text()='{0}']/preceding-sibling::input";

        [AllureStep]
        public ServiceDetailPage VerifyTooltip(string content)
        {
            Assert.IsTrue(GetElementText(DynamicOptimisationTooltip).Contains(content));
            return this;
        }

        [AllureStep]
        public ServiceDetailPage IsServiceDetailPage()
        {
            WaitUtil.WaitForElementVisible(serviceTitle);
            WaitUtil.WaitForElementVisible(detailTab);
            WaitUtil.WaitForElementVisible(attributeTab);
            return this;
        }

        [AllureStep]
        public ServiceDetailPage ClickOnNotificationActiveCheckbox()
        {
            ClickOnElement(notificationActiveCheckbox);
            return this;
        }

        [AllureStep]
        public ServiceDetailPage VerifyTheDisplayOfExecuteDays(string[] executeDayValues)
        {
            WaitUtil.WaitForElementVisible(advancedNotificationPeriodDd);
            foreach(string day in executeDayValues)
            {
                Assert.IsTrue(IsControlDisplayed(executeDayOption, day), day + " is not displayed");
            }
            return this;
        }
    }
}
