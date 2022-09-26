using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceRecyclingPage : BasePageCommonActions
    {
        public readonly By PointTypeSelect = By.XPath("//select[@id='pointType.id']");
        public readonly By RestrictEditCheckbox = By.XPath("//input[contains(@data-bind, 'restrictEdit.id')]");

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
