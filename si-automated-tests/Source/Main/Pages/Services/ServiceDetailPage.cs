using System;
using System.Globalization;
using System.Text.RegularExpressions;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceDetailPage : BasePageCommonActions
    {
        public readonly By DynamicOptimisationLabel = By.XPath("//label[contains(text(), 'Dynamic optimisation')]");
        public readonly By DynamicOptimisationCheckbox = By.XPath("//input[contains(@data-bind, 'isDynamicOptimisation')]");
        public readonly By DynamicOptimisationHelpButton = By.XPath("//div[contains(@data-bind, 'isDynamicOptimisation')]//span[@role='button']");
        public readonly By DynamicOptimisationTooltip = By.XPath("//div[contains(@data-bind, 'isDynamicOptimisation')]//div[@role='tooltip']");

        [AllureStep]
        public ServiceDetailPage VerifyTooltip(string content)
        {
            Assert.IsTrue(GetElementText(DynamicOptimisationTooltip).Contains(content));
            return this;
        }
    }
}
