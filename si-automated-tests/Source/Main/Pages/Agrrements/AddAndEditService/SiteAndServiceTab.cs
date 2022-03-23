using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService
{
    public class SiteAndServiceTab : AddServicePage
    {
        private readonly By siteDropDown = By.Id("collection-site");
        private readonly By serviceDropDown = By.Id("contract");
        private string serviceOptions = "//select[@id='contract']/option[text()='{0}']";
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
        public SiteAndServiceTab SelectService(string value)
        {
            WaitUtil.WaitForElementVisible(serviceOptions, value);
            ClickOnElement(serviceDropDown);
            ClickOnElement(serviceOptions, value);
            return this;
        }
        public string GetSiteAddress()
        {
            return GetFirstSelectedItemInDropdown(siteDropDown);
        }
    }
}
