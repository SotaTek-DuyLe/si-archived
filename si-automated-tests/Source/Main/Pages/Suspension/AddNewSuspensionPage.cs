using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Suspension
{
    public class AddNewSuspensionPage : BasePage
    {
        private readonly By modal = By.XPath("//div[@id='add-service-suspensions']");
        private readonly string titleXpath = "//div[@id='add-service-suspensions']//h4";
        private readonly By serviceSuspensionModal = By.XPath("//div[@class='modal-content service-suspension-content']");
        private readonly By siteItems = By.XPath("//div[@id='step-1']//li");
        private readonly By serviceItems = By.XPath("//div[@id='step-2']//li");
        private readonly By nextBtn = By.XPath("//div[@id='add-service-suspensions']//div[@class='modal-footer']//button[text()='Next']");
        private readonly By selectAllSiteCheckbox = By.XPath("//div[@id='step-1']//span[text()='Select All']/preceding-sibling::input[@type='checkbox']");
        private readonly By selectAllServiceCheckbox = By.XPath("//div[@id='step-2']//span[text()='Select All']/preceding-sibling::input[@type='checkbox']");
        private readonly By fromDateInput = By.XPath("//input[@id='fromDateField.id']");
        private readonly By untilDateInput = By.XPath("//input[@id='untilDateField.id']");
        private readonly By dateDiffLabel = By.XPath("//div[@id='step-3']//label[text()='0 days']");
        private readonly By everydayRadio = By.XPath("//div[@id='step-3']//span[text()='Everyday']/preceding-sibling::input[@type='radio']");
        private readonly By selectedDayRadio = By.XPath("//div[@id='step-3']//span[text()='Selected Days']/preceding-sibling::input[@type='radio'][1]");
        private readonly By finishBtn = By.XPath("//div[@id='add-service-suspensions']//div[@class='modal-footer']//button[text()='Finish']");
        private const string AnyMessage = "//div[text()='{0}']";

        public AddNewSuspensionPage WaitServiceSuspensionVisible()
        {
            WaitUtil.WaitForElementVisible(serviceSuspensionModal);
            return this;
        }

        public List<string> GetSiteNames()
        {
            List<IWebElement> checkboxs = GetAllElements(siteItems);
            return checkboxs.Select(x => GetElementText(x)).ToList();
        }

        public List<string> GetServiceNames()
        {
            List<IWebElement> checkboxs = GetAllElements(serviceItems);
            return checkboxs.Select(x => GetElementText(x)).ToList();
        }

        public AddNewSuspensionPage VerifySuspensionTitle(string title)
        {
            List<IWebElement> titles = GetAllElements(titleXpath);
            string viewTitle = string.Join(" ", titles.Select(x => GetElementText(x)).ToArray());
            Assert.IsTrue(viewTitle == title);
            return this;
        }

        public AddNewSuspensionPage VerifyNextButtonIsDisable()
        {
            Assert.IsFalse(IsControlEnabled(nextBtn));
            return this;
        }

        public AddNewSuspensionPage ClickSelectAllSiteCheckbox()
        {
            ClickOnElement(selectAllSiteCheckbox);
            return this;
        }

        public AddNewSuspensionPage ClickSelectAllServiceCheckbox()
        {
            ClickOnElement(selectAllServiceCheckbox);
            return this;
        }

        public AddNewSuspensionPage VerifyNextButtonIsEnable()
        {
            Assert.IsTrue(IsControlEnabled(nextBtn));
            return this;
        }

        public AddNewSuspensionPage ClickNextButton()
        {
            ClickOnElement(nextBtn);
            return this;
        }

        public AddNewSuspensionPage IsFirstDayInputVisible()
        {
            Assert.IsTrue(IsControlDisplayed(fromDateInput));
            return this;
        }

        public AddNewSuspensionPage IsLastDayInputVisible()
        {
            Assert.IsTrue(IsControlDisplayed(untilDateInput));
            return this;
        }

        public AddNewSuspensionPage IsDateDiffLabelVisible()
        {
            Assert.IsTrue(IsControlDisplayed(dateDiffLabel));
            return this;
        }

        public AddNewSuspensionPage IsEveryDayRadioVisible()
        {
            Assert.IsTrue(IsControlDisplayed(everydayRadio));
            return this;
        }

        public AddNewSuspensionPage IsSelectedDayRadioVisible()
        {
            Assert.IsTrue(IsControlDisplayed(selectedDayRadio));
            return this;
        }

        public AddNewSuspensionPage ClickFinish()
        {
            ClickOnElement(finishBtn);
            return this;
        }

        public AddNewSuspensionPage VerifyWarningMessage(string errorMessage)
        {
            Assert.IsTrue(IsControlDisplayed(String.Format(AnyMessage, errorMessage)));
            return this;
        }

        public AddNewSuspensionPage InputDaysAndVerifyDaysCalculationLbl(string fromDate, string lastDay)
        {
            IWebElement dateDiffEle = GetElement(dateDiffLabel);
            IWebElement fromDateInputEle = GetElement(fromDateInput);
            SendKeys(fromDateInputEle, fromDate);
            IWebElement untilDateInputEle = GetElement(untilDateInput);
            SendKeys(untilDateInputEle, lastDay);
            ClickOnElement(everydayRadio);
            Thread.Sleep(200);
            Assert.IsTrue(GetElementText(dateDiffEle) == "30 days");
            return this;
        }

        public AddNewSuspensionPage VerifySaveMessage(string saveMsg)
        {
            Assert.IsTrue(IsControlDisplayed(String.Format(AnyMessage, saveMsg)));
            return this;
        }

        public AddNewSuspensionPage IsAddSuspensionModalInVisible()
        {
            Assert.IsTrue(IsControlUnDisplayed(modal));
            return this;
        }
    }
}
