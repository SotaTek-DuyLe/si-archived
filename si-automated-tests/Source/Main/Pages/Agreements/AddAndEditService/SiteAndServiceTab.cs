using NUnit.Allure.Attributes;
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
        private string serviceSiteOptions = "//select[@id='collection-site']/option[text()='{0}']";
        private readonly By serviceDropDown = By.Id("contract");
        private string serviceOptions = "//select[@id='contract']/option[text()='{0}']";
        
        [AllureStep]
        public SiteAndServiceTab IsOnSiteServiceTab()
        {
            WaitUtil.WaitForElementVisible(siteDropDown);
            WaitUtil.WaitForElementVisible(serviceDropDown);
            return this;
        }
        [AllureStep]
        public SiteAndServiceTab ChooseServicedSite(int index)
        {
            SelectIndexFromDropDown(siteDropDown, index);
            return this;
        }
        [AllureStep]
        public SiteAndServiceTab SelectServiceSite(string value)
        {
            WaitUtil.WaitForElementVisible(serviceSiteOptions, value);
            ClickOnElement(siteDropDown);
            ClickOnElement(serviceSiteOptions, value);
            return this;
        }
        [AllureStep]
        public SiteAndServiceTab ChooseService(string value)
        {
            SelectTextFromDropDown(serviceDropDown, value);
            return this;
        }
        [AllureStep]
        public SiteAndServiceTab SelectService(string value)
        {
            WaitUtil.WaitForElementVisible(serviceOptions, value);
            ClickOnElement(serviceDropDown);
            ClickOnElement(serviceOptions, value);
            return this;
        }
        [AllureStep]
        public string GetSiteAddress()
        {
            return GetFirstSelectedItemInDropdown(siteDropDown);
        }
    }
}
