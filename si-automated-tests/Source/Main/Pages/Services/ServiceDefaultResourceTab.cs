using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceDefaultResourceTab : BasePage
    {
        private readonly string expandOption = "(//div[@id='toggle-actions'])[{0}]";
        private readonly By addResourceBtn = By.XPath("//tr[contains(@id,'child-target') and @aria-expanded='true']/descendant::button");
        private readonly By resourceInput = By.XPath("//tr[contains(@id,'child-target') and @aria-expanded='true']/descendant::tbody/tr[2]/td/select");

        //select elements
        private readonly By typeSelects = By.XPath("//select[@id='type.id']");

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
        public ServiceDefaultResourceTab ExpandOption(string option)
        {
            IList<IWebElement> _typeSelects = WaitUtil.WaitForAllElementsVisible(typeSelects);
            for (int i = 0; i < _typeSelects.Count; i++)
            {
                SelectElement select = new SelectElement(_typeSelects[i]);
                if (select.SelectedOption.Text.Equals(option))
                {
                    Console.WriteLine(i);
                    ClickOnElement(String.Format(expandOption, i + 1));
                    return this;
                }
            }
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
