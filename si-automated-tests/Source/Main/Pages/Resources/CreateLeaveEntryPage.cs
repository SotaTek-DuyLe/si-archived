using System;
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
        public readonly By OpenDropDownOptions = By.XPath("//ul[@role='listbox' and @aria-expanded='true']//li");
        public readonly By LeaveTypeDropdown = By.XPath("//select[@id='state']");
        public readonly By ReasonDropdown = By.XPath("//select[@id='resolution-code']");
        public readonly By FromDateInput = By.XPath("//input[@id='startDate']");
        public readonly By ToDateInput = By.XPath("//input[@id='endDate']");
        public readonly By SaveButton = By.XPath("//button[text()='Save']");
        public readonly By AllResourceTab = By.XPath("//a[@aria-controls='all-resources-tab']");
        public readonly By BUSelect = By.XPath("//select[@id='business-unit']");

        [AllureStep]
        public CreateLeaveEntryPage VerifyResourceIsDisable(bool isDisable)
        {
            if (isDisable)
            {
                Assert.IsTrue(GetElement(ResourceDropdown).GetCssValue("cursor") == "not-allowed");
            }
            else
            {
                Assert.IsTrue(GetElement(ResourceDropdown).GetCssValue("cursor") == "pointer");
            }
            return this;
        }

        [AllureStep]
        public CreateLeaveEntryPage VerifyBUSelectValues(List<string> values)
        {
            Assert.That(GetSelectDisplayValues(GetElement(BUSelect)), Is.EquivalentTo(values));
            return this;
        }

        [AllureStep]
        public CreateLeaveEntryPage VerifyResourceHasValue()
        {
            Assert.IsTrue(GetAllElements(OpenDropDownOptions).Count != 0);
            return this;
        }
    }
}
