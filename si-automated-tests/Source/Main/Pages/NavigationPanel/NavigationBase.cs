﻿using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.Paties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.NavigationPanel
{
    public class NavigationBase : BasePage
    {
        //title
        private readonly string pageTitle = ConfigManager.GetCurrentPlatform().Equals(WebPlatform.IE)
           ? "//div[@class='e_hdr']"
           : "//p[text()='Northstar Environmental Services – Demo 8.6.0-dev01']";

        //Main options
        private readonly string patiesMenu = "//span[text()='Parties']/parent::h4/parent::div";
        private readonly string mainOption = "//span[text()='{0}']/parent::h4/parent::div";
        private readonly string dropdownOption = "//span[text()='{0}']/parent::a/preceding-sibling::span[2]";
        private readonly string option = "//span[text()='{0}']/parent::a";


        //Sub options
        private readonly string northStarCommercialDropdownBtn = "//span[text()='North Star Commercial']/parent::a/preceding-sibling::span[2]";

        public PartyNavigation ClickParties()
        {
            ClickOnElement(patiesMenu);
            return PageFactoryManager.Get<PartyNavigation>();
        }

        public NavigationBase ExpandNSC()
        {
            ClickOnElement(northStarCommercialDropdownBtn);
            return this;
        }

        public NavigationBase ClickMainOption(string optionName)
        {
            Thread.Sleep(250);
            ClickOnElement(String.Format(mainOption, optionName));
            return this;
        }
        public NavigationBase ExpandOption(string optionName)
        {
            Thread.Sleep(250);
            ClickOnElement(String.Format(dropdownOption, optionName));
            return this;
        }
        public NavigationBase OpenOption(string optionName)
        {
            Thread.Sleep(250);
            ClickOnElement(String.Format(option, optionName));
            return this;
        }
        //Exit Navigation
        public NavigationBase ExitNavigation()
        {
            ClickOnElement(pageTitle);
            return this;
        }
    }
}
