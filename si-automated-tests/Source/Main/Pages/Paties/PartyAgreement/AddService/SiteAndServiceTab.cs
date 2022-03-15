using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement.AddService
{
    public class SiteAndServiceTab : AddServicePage
    {
        private readonly By siteDropDown = By.Id("collection-site");
        private readonly By serviceDropDown = By.Id("contract");
        public SiteAndServiceTab IsOnSiteServiceTab()
        {
            WaitUtil.WaitForElementVisible(siteDropDown);
            WaitUtil.WaitForElementVisible(serviceDropDown);
            return this;
        }

        public SiteAndServiceTab ChooseServicedSite(int index)
        {
            SelectIndexFromDropDown(siteDropDown, index);
            return this;
        }
        public SiteAndServiceTab ChooseService(string value)
        {
            SelectTextFromDropDown(serviceDropDown, value);
            return this;
        }
        public string GetSiteAddress()
        {
            return GetFirstSelectedItemInDropdown(siteDropDown);
        }
    }
}
