using System;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class PartyNavigation : BasePage
    {
        private readonly string northStartCommercialMenu = "//span[text()='North Star Commercial']/parent::a/preceding-sibling::span[2]";
        private readonly string partiesSubSubMenu = "//span[text()='Parties']/parent::a";
        private readonly string agreementSubSubMenu = "//span[text()='Agreements']/parent::a";

        public PartyNavigation ClickNSC()
        {
            ClickOnElement(northStartCommercialMenu);
            return this;
        }
        public PartyNavigation ClickPartySubMenu()
        {
            ClickOnElement(partiesSubSubMenu);
            return this;
        }
        public PartyNavigation ClickAgreementSubMenu()
        {
            ClickOnElement(agreementSubSubMenu);
            return this;
        }
    }
}
