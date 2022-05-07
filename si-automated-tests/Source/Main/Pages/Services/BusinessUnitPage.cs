﻿using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class BusinessUnitPage : BasePage
    {
        private By businessUnitInput = By.Id("businessUnit");

        public BusinessUnitPage()
        {
            SwitchToLastWindow();
        }
        public BusinessUnitPage InputBusinessName(string name)
        {
            SendKeys(businessUnitInput, name);
            return this;
        }
    }
}
