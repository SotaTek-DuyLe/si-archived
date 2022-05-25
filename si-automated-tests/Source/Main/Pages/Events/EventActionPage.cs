using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Events
{
    public class EventActionPage : BasePage
    {
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");
        private readonly By allocatedUnitDd = By.XPath("//label[text()='Allocated Unit']/following-sibling::select");
        private readonly By allocatedUserDd = By.XPath("//label[text()='Allocated User']/following-sibling::select");
        private readonly By allOptionsInAllocatedUnitDd = By.XPath("//label[text()='Allocated Unit']/following-sibling::select/option");
        private readonly By allOptionsInAllocatedUserDd = By.XPath("//label[text()='Allocated User']/following-sibling::select/option");
        private readonly By noteInput = By.XPath("//label[text()='Notes']/following-sibling::input");

        //DYNAMIC
        private const string anyOptionInAllocatedUnitDd = "//label[text()='Allocated Unit']/following-sibling::select/option[text()='{0}']";
        private const string anyOptionInAllocatedUserDd = "//label[text()='Allocated User']/following-sibling::select/option[text()='{0}']";

        public EventActionPage IsEventActionPage()
        {
            WaitUtil.WaitForElementVisible(allocatedUnitDd);
            Assert.IsTrue(IsControlDisplayed(dataTab));
            Assert.IsTrue(IsControlDisplayed(allocatedUnitDd));
            Assert.IsTrue(IsControlDisplayed(allocatedUserDd));
            return this;
        }

        public EventActionPage ClickOnAllocatedUnit()
        {
            ClickOnElement(allocatedUnitDd);
            return this;
        }

        public EventActionPage ClickOnAllocatedUser()
        {
            ClickOnElement(allocatedUserDd);
            return this;
        }

        public EventActionPage SelectAnyAllocatedUnit(string allocatedUnitOption)
        {
            ClickOnElement(anyOptionInAllocatedUnitDd, allocatedUnitOption);
            return this;
        }

        public EventActionPage SelectAnyAllocatedUser(string allocatedUserOption)
        {
            ClickOnElement(anyOptionInAllocatedUserDd, allocatedUserOption);
            return this;
        }

        public List<string> GetAllOptionsInAllocatedUnitDd()
        {
            List<string> results = new List<string>();
            List<IWebElement> allOptions = GetAllElements(allOptionsInAllocatedUnitDd);
            foreach(IWebElement e in allOptions)
            {
                results.Add(GetElementText(e));
            }
            return results;
        }

        public List<string> GetAllOptionsInAllocatedUserDd()
        {
            List<string> results = new List<string>();
            List<IWebElement> allOptions = GetAllElements(allOptionsInAllocatedUserDd);
            foreach (IWebElement e in allOptions)
            {
                results.Add(GetElementText(e));
            }
            return results;
        }

        public EventActionPage VerifyAllocatedUnitDisplayTheSameEventForm(List<string> allocatedUnitInEventForm, List<string> allocatedUnitEventActionForm)
        {
            Assert.IsTrue(allocatedUnitEventActionForm.SequenceEqual(allocatedUnitInEventForm));
            return this;
        }

        public EventActionPage VerifyAllocatedUserDisplayTheSameEventForm(List<string> allocatedUserInEventForm, List<string> allocatedUserEventActionForm)
        {
            Assert.IsTrue(allocatedUserInEventForm.SequenceEqual(allocatedUserEventActionForm));
            return this;
        }
    }
}
