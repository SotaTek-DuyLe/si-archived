using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.NavigationPanel
{
    public class NavigationBase : BasePage
    {
        //Main options
        private const string patiesMenu = "//span[text()='Parties']/parent::h4/parent::div";
        private const string resourceMenu = "//span[text()='Resources']/parent::h4/parent::div";


        //Sub options
        private readonly string northStarCommercialDropdownBtn = "//span[text()='North Star Commercial']/parent::a/preceding-sibling::span[2]";
        private readonly string northStarDropdownBtn = "//span[text()='North Star']/parent::a/preceding-sibling::span[2]";
        private readonly string northStarCommercial = "//span[text()='North Star Commercial']/parent::a";
        private readonly string northStar = "//span[text()='North Star']/parent::a";

        public PartyNavigation ClickParties()
        {
            ClickOnElement(patiesMenu);
            return PageFactoryManager.Get<PartyNavigation>();
        }
        public ResourceNavigation ClickResources()
        {
            ClickOnElement(resourceMenu);
            return PageFactoryManager.Get<ResourceNavigation>();
        }

        public NavigationBase ExpandNSC()
        {
            ClickOnElement(northStarCommercialDropdownBtn);
            return this;
        }public NavigationBase ExpandNS()
        {
            ClickOnElement(northStarDropdownBtn);
            return this;
        }public NavigationBase OpenNSC()
        {
            ClickOnElement(northStarCommercial);
            return this;
        }public NavigationBase OpenNS()
        {
            ClickOnElement(northStar);
            return this;
        }
    }
}
