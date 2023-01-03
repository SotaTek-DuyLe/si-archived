﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class CreateLeaveEntryPage : BasePageCommonActions
    {
        public readonly By ResourceDropdown = By.XPath("//button[@data-id='resource']");
        public readonly By OpenDropDown = By.XPath("//ul[@role='listbox' and @aria-expanded='true']");
        public readonly By LeaveTypeDropdown = By.XPath("//select[@id='state']");
        public readonly By ReasonDropdown = By.XPath("//select[@id='resolution-code']");
        public readonly By FromDateInput = By.XPath("//input[@id='startDate']");
        public readonly By ToDateInput = By.XPath("//input[@id='endDate']");
        public readonly By SaveButton = By.XPath("//button[text()='Save']");
    }
}
