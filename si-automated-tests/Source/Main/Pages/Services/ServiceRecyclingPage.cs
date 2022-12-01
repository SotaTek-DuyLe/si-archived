using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceRecyclingPage : BasePageCommonActions
    {
        public readonly By PointTypeSelect = By.XPath("//select[@id='pointType.id']");
        public readonly By RestrictEditCheckbox = By.XPath("//input[contains(@data-bind, 'restrictEdit.id')]");
        private readonly By titleService = By.XPath("//span[text()='Service']");
        private readonly By serviceInput = By.CssSelector("input[name='service']");

        //DYNAMIC
        private readonly string serviceName = "//h5[text()='{0}']";

        [AllureStep]
        public ServiceRecyclingPage WaitForServiceRecyclingPageLoaded(string serviceNameValue)
        {
            WaitUtil.WaitForElementVisible(titleService);
            WaitUtil.WaitForElementVisible(serviceInput);
            WaitUtil.WaitForElementVisible(serviceName, serviceNameValue);
            return this;
        }

        [AllureStep]
        public ServiceRecyclingPage SelectRandomPointType()
        {
            string selectedPointType = GetFirstSelectedItemInDropdown(PointTypeSelect);
            List<string> selections = GetSelectDisplayValues(PointTypeSelect);
            int index = 0;
            while (true)
            {
                Random rnd = new Random();
                index = rnd.Next(selections.Count);
                if(selectedPointType != selections[index] && !string.IsNullOrEmpty(selections[index]))
                {
                    break;
                }
            }
            SelectTextFromDropDown(PointTypeSelect, selections[index]);
            return this;
        }
    }
}
