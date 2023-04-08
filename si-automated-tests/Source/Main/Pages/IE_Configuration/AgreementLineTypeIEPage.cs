using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class AgreementLineTypeIEPage : BasePage
    {
        private readonly By title = By.XPath("//span[contains(string(), 'AgreementLineType')]");
        private readonly By commercialName = By.XPath("//span[contains(string(), 'Commercial')]");
        private readonly By allowProductCodeInput = By.XPath("//span[text()='Allow Product Code']/parent::td/following-sibling::td//input");
        private readonly By saveBtn = By.XPath("//img[@title='Save']/parent::td");

        [AllureStep]
        public AgreementLineTypeIEPage IsAgreementLineTypeIEPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(commercialName);
            return this;
        }

        [AllureStep]
        public AgreementLineTypeIEPage ClickOnAllowProductCode()
        {
            ClickOnElement(allowProductCodeInput);
            return this;
        }

        [AllureStep]
        public AgreementLineTypeIEPage ClickOnSaveForm()
        {
            ClickOnElement(saveBtn);
            return this;
        }
    }
}
