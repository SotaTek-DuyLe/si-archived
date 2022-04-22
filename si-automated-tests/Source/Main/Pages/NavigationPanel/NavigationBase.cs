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
        private readonly string option = "//span[text()='{0}']/parent::a";

        public NavigationBase()
        {
            SwitchToDefaultContent();
        }

        public NavigationBase ClickMainOption(string optionName)
        {
            Thread.Sleep(500);
            ClickOnElement(String.Format(mainOption, optionName));
            return this;
        }
        public NavigationBase ExpandOption(string optionName)
        {
            Thread.Sleep(500);
            String expandedAttribute = GetAttributeValue(string.Format(dropdownOption, optionName), "class");
            if(!expandedAttribute.Contains("expanded"))
            {
                ClickOnElement(String.Format(dropdownOption, optionName));
            }
            return this;
        }
        public NavigationBase OpenOption(string optionName)
        {
            Thread.Sleep(500);
            ClickOnElement(String.Format(option, optionName));
            ExitNavigation();
            return this;
        }
        //Exit Navigation
        public NavigationBase ExitNavigation()
        {
            Thread.Sleep(500);
            ClickOnElement(pageTitle);
            return this;
        }
    }
}
