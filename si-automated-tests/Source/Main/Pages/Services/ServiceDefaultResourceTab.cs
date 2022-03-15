using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceDefaultResourceTab : BasePage
    {
        //Temporary hard coded for tc31, need an upgrade if other tests use
        private readonly By expandOption = By.XPath("(//div[@id='toggle-actions'])[2]");
        private readonly By addResourceBtn = By.XPath("//tr[contains(@id,'child-target') and @aria-expanded='true']/descendant::button");
        private readonly By resourceInput = By.XPath("//tr[contains(@id,'child-target') and @aria-expanded='true']/descendant::tbody/tr[2]/td/select");

        private readonly By tableHeader1 = By.XPath("//div[@id='default-resources']/descendant::th[text()='Type']");
        private readonly By tableHeader2 = By.XPath("//div[@id='default-resources']/descendant::th[text()='Quantity']");
        private readonly By tableHeader3 = By.XPath("//div[@id='default-resources']/descendant::th[text()='Start Date']");
        private readonly By tableHeader4 = By.XPath("//div[@id='default-resources']/descendant::th[text()='End Date']");

        public ServiceDefaultResourceTab IsOnServiceDefaultTab()
        {
            WaitUtil.WaitForElementVisible(tableHeader1);
            WaitUtil.WaitForElementVisible(tableHeader2);
            WaitUtil.WaitForElementVisible(tableHeader3);
            WaitUtil.WaitForElementVisible(tableHeader4);
            return this;
        }
        public ServiceDefaultResourceTab ExpandDriverType()
        {
            ClickOnElement(expandOption);
            return this;
        }
        public ServiceDefaultResourceTab ClickAddResource()
        {
            ClickOnElement(addResourceBtn);
            return this;
        }
        public ServiceDefaultResourceTab VerifyInputIsAvailable(string option)
        {
            SelectTextFromDropDown(resourceInput, option);
            Assert.AreEqual(option, GetFirstSelectedItemInDropdown(resourceInput));
            return this;
        }
    }
}
