using System;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc
{
    public class CreateAdHocTaskPage : BasePage
    {
        private readonly By modal = By.XPath("//div[@id='po-number-requirement-editor-modal']");
        private readonly By title = By.XPath("//div[@id='po-number-requirement-editor-modal']//h4");
        private readonly By input = By.XPath("//div[@id='po-number-requirement-editor-modal']//input[@id='poNumber']");
        private readonly By doneBtn = By.XPath("//div[@id='po-number-requirement-editor-modal']//button[text()='Done']");

        [AllureStep]
        public CreateAdHocTaskPage VerifyAdHocTaskIsCreated()
        {
            string url = GetCurrentUrl();
            string id = url.Split('/').LastOrDefault();
            Console.WriteLine(id);
            Assert.IsTrue(id.AsInteger() > 0);
            return this;
        }
        [AllureStep]
        public CreateAdHocTaskPage VerifyTitle(string expectedTitle)
        {
            Assert.IsTrue(GetElementText(title) == expectedTitle);
            return this;
        }
        [AllureStep]
        public CreateAdHocTaskPage InputPoNumber(string inputStr)
        {
            IWebElement webElement = GetElement(input);
            SendKeys(webElement, inputStr);
            return this;
        }
        [AllureStep]
        public CreateAdHocTaskPage ClickDone()
        {
            ClickOnElement(doneBtn);
            return this;
        }
        [AllureStep]
        public CreateAdHocTaskPage IsCreateAdhocTaskInVisible()
        {
            Assert.IsTrue(IsControlUnDisplayed(modal));
            return this;
        }
    }
}
