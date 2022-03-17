using System;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.NavigationPanel;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class PartyNavigation : NavigationBase
    {
        private readonly string partiesSubSubMenu = "//span[text()='Parties']/parent::a";
        private readonly string agreementSubSubMenu = "//span[text()='Agreements']/parent::a";
        private readonly string siteServiceSubSubMenu = "//span[text()='Site Services']/parent::a";
        private readonly string titleHomePage = "//p[text()='Northstar Environmental Services – Demo 8.6.0-dev01']";

        
        public PartyNavigation ClickPartySubMenu()
        {
            ClickOnElement(partiesSubSubMenu);
            ClickOnElement(titleHomePage);
            return this;
        }
        public PartyNavigation ClickAgreementSubMenu()
        {
            ClickOnElement(agreementSubSubMenu);
            return this;
        } 
        public PartyNavigation ClickSiteServiceSubMenu()
        {
            ClickOnElement(siteServiceSubSubMenu);
            return this;
        }
    }
}
