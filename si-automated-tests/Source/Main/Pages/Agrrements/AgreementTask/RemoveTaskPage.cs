using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask
{
    public class RemoveTaskPage : BasePage
    {
        private readonly By removeTaskText = By.XPath("//div[text()='Do you wish to remove this task? ']");
        private readonly By yesBtn = By.XPath("//button[text()='Yes']");
        private readonly By noBtn = By.XPath("//button[text()='No']");
        private readonly By loadingIcon = By.XPath("//div[@data-bind='shield: isLoading']");
        public RemoveTaskPage VerifyRemoveTaskPage()
        {
            WaitUtil.WaitForElementVisible(loadingIcon);
            WaitUtil.WaitForElementVisible(removeTaskText);
            Assert.IsTrue(IsControlDisplayed(removeTaskText));
            Assert.IsTrue(IsControlDisplayed(yesBtn));
            Assert.IsTrue(IsControlDisplayed(noBtn));
            return this;
        }

        public RemoveTaskPage ClickYesBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }

        public RemoveTaskPage ClickNoBtn()
        {
            ClickOnElement(noBtn);
            return this;
        }
    }
}
