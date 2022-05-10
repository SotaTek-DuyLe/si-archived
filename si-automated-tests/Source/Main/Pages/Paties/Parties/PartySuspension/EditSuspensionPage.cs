using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;


namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension
{
    public class EditSuspensionPage : BasePage
    {
        private readonly By modal = By.XPath("//div[@id='add-service-suspensions']");
        private readonly By siteItems = By.XPath("//div[@id='step-1']//li");
        private readonly By serviceItems = By.XPath("//div[@id='step-2']//li");
        private readonly By nextBtn = By.XPath("//div[@id='add-service-suspensions']//div[@class='modal-footer']//button[text()='Next']");
        private readonly By serviceCheckboxs = By.XPath("//div[@id='step-1']//ul[contains(@class, 'list-group-services')]//li//input");
        private readonly By serviceTypeCheckboxs = By.XPath("//div[@id='step-2']//ul[contains(@class, 'list-group-services')]//li//input");
        private readonly By fromDateInput = By.XPath("//input[@id='fromDateField.id']");
        private readonly By untilDateInput = By.XPath("//input[@id='untilDateField.id']");
        private readonly By dateDiffLabel = By.XPath("//div[@id='step-3']//label[text()='30 days']");
        private readonly By everydayRadio = By.XPath("//div[@id='step-3']//span[text()='Everyday']/preceding-sibling::input[@type='radio']");
        private readonly By finishBtn = By.XPath("//div[@id='add-service-suspensions']//div[@class='modal-footer']//button[text()='Finish']");
        private const string AnyMessage = "//div[text()='{0}']";

        public EditSuspensionPage VerifyNextButtonIsEnable()
        {
            Assert.IsTrue(IsControlEnabled(nextBtn));
            return this;
        }

        public EditSuspensionPage VerifyServiceCheckboxsAreSelected()
        {
            List<IWebElement> checkboxs = GetAllElements(serviceCheckboxs);
            foreach (var item in checkboxs)
            {
                Assert.IsTrue(item.GetAttribute("data-bind").Contains("checked: selected"));
            }
            return this;
        }

        public EditSuspensionPage VerifyServiceTypeCheckboxsAreSelected()
        {
            List<IWebElement> checkboxs = GetAllElements(serviceTypeCheckboxs);
            foreach (var item in checkboxs)
            {
                Assert.IsTrue(item.GetAttribute("data-bind").Contains("checked: selected"));
            }
            return this;
        }

        public EditSuspensionPage ClickServiceCheckbox()
        {
            GetAllElements(serviceCheckboxs).ElementAtOrDefault(1)?.Click();
            return this;
        }

        public EditSuspensionPage ClickNextButton()
        {
            ClickOnElement(nextBtn);
            return this;
        }

        public List<string> GetSiteNames()
        {
            List<IWebElement> checkboxs = GetAllElements(siteItems);
            checkboxs.RemoveAt(1);
            return checkboxs.Select(x => GetElementText(x)).ToList();
        }

        public List<string> GetServiceNames()
        {
            List<IWebElement> checkboxs = GetAllElements(serviceItems);
            return checkboxs.Select(x => GetElementText(x)).ToList();
        }


        public EditSuspensionPage InputDaysAndVerifyDaysCalculationLbl(string fromDate, string lastDay)
        {
            IWebElement dateDiffEle = GetElement(dateDiffLabel);
            IWebElement fromDateInputEle = GetElement(fromDateInput);
            SendKeys(fromDateInputEle, fromDate);
            IWebElement untilDateInputEle = GetElement(untilDateInput);
            SendKeys(untilDateInputEle, lastDay);
            ClickOnElement(everydayRadio);
            Thread.Sleep(200);
            Assert.IsTrue(GetElementText(dateDiffEle) == "15 days");
            return this;
        }

        public EditSuspensionPage VerifySaveMessage(string saveMsg)
        {
            Assert.IsTrue(IsControlDisplayed(String.Format(AnyMessage, saveMsg)));
            return this;
        }

        public EditSuspensionPage ClickFinish()
        {
            ClickOnElement(finishBtn);
            return this;
        }

        public EditSuspensionPage IsEditSuspensionModalInVisible()
        {
            Assert.IsTrue(IsControlUnDisplayed(modal));
            return this;
        }
    }
}
