using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.NavigationPanel
{
    public class NavigationBase : BasePage
    {
        //title
        private readonly string pageTitle = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
           ? "//div[@class='e_hdr']"
           : "//p[@class='navbar-text environment-logo']";

        //Main options
        private readonly string mainOption = "//span[text()='{0}']/parent::h4/parent::div";
        private readonly string dropdownOption = "//span[text()='{0}']/parent::a/preceding-sibling::span[2]";
        private readonly string dropdownOptionLast = "(//span[text()='{0}']/parent::a/preceding-sibling::span[2])[last()]";
        private readonly string option = "//span[text()='{0}']/parent::a";
        private readonly string optionLast = "(//span[text()='{0}']/parent::a)[last()]";

        public NavigationBase()
        {
            SwitchToDefaultContent();
        }
        [AllureStep]
        public NavigationBase ClickMainOption(string optionName)
        {
            Thread.Sleep(500);
            ClickOnElement(string.Format(mainOption, optionName));
            return this;
        }
        [AllureStep]
        public NavigationBase ExpandOption(string optionName)
        {
            WaitForLoadingIconToDisappear();
            Thread.Sleep(500);
            String expandedAttribute = GetAttributeValue(string.Format(dropdownOption, optionName), "class");
            if(!expandedAttribute.Contains("expanded"))
            {
                ClickOnElement(String.Format(dropdownOption, optionName));
            }
            return this;
        }
        [AllureStep]
        public NavigationBase ExpandOptionLast(string optionName)
        {
            Thread.Sleep(500);
            String expandedAttribute = GetAttributeValue(string.Format(dropdownOptionLast, optionName), "class");
            if(!expandedAttribute.Contains("expanded"))
            {
                ClickOnElement(String.Format(dropdownOptionLast, optionName));
            }
            return this;
        }
        [AllureStep]
        public NavigationBase OpenOption(string optionName)
        {
            Thread.Sleep(500);
            ClickOnElement(String.Format(option, optionName));
            AcceptAlertIfAny();
            ExitNavigation();
            return this;
        }
        [AllureStep]
        public NavigationBase OpenLastOption(string optionName)
        {
            Thread.Sleep(500);
            ClickOnElement(String.Format(optionLast, optionName));
            AcceptAlertIfAny();
            ExitNavigation();
            return this;
        }
        //Exit Navigation
        [AllureStep]
        public NavigationBase ExitNavigation()
        {
            Thread.Sleep(500);
            ClickOnElement(pageTitle);
            return this;
        }
        
    }
}
