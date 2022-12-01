using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService
{ 
    public class EditAgreementServicePage : BasePage
    {
        private readonly By editServiceTitle = By.XPath("//h4[text()='Edit Service']");
        private readonly By Page1SiteAndServiceText = By.XPath("//span[text()='1']/following-sibling::p[text()='Site and Service']");
        private readonly By nextBtn = By.XPath("//button[text()='Next']");

        [AllureStep]
        public EditAgreementServicePage IsOnEditAgreementServicePage()
        {
            WaitUtil.WaitForElementVisible(editServiceTitle);
            WaitUtil.WaitForElementVisible(nextBtn);
            Assert.IsTrue(IsControlDisplayed(editServiceTitle));
            Assert.IsTrue(IsControlDisplayed(Page1SiteAndServiceText));
            Assert.IsTrue(IsControlDisplayed(nextBtn));
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ClickOnNextBtn()
        {
            WaitUtil.WaitForElementClickable(nextBtn);
            ScrollDownToElement(nextBtn);
            ClickOnElement(nextBtn);
            return new AssetAndProducTab();
        }
    }
}
