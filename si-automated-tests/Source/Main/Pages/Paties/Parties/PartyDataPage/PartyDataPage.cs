using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyDataPage
{
    public class PartyDataPage : BasePage
    {
        private readonly By customer = By.XPath("//button[text()='Customer']");
        private const string collapseIn = "//div[@class='collapse in']";
        private const string collapse = "//div[@class='collapse']";
        private readonly By imageInput = By.XPath(collapseIn + "//label[text()='Customer Logo']/following-sibling::div[@class='img-thumbnail']");
        private readonly By notesInput = By.XPath(collapseIn + "//label[text()='Notes']/following-sibling::input");


        [AllureStep]
        public PartyDataPage VerifyPartyDataCustomer()
        {
            WaitUtil.WaitForElementClickable(customer);
            Assert.IsTrue(IsControlDisplayed(customer));
            Assert.AreEqual(GetCssValue(customer, "color"), CommonConstants.ColorBlue);
            return this;
        }
        [AllureStep]
        public PartyDataPage ClickCustomer()
        {
            ClickOnElement(customer);
            return this;
        }
        [AllureStep]
        public PartyDataPage VerifyCustomerPath()
        {
            WaitUtil.WaitForElementVisible(collapseIn);
            WaitUtil.WaitForElementVisible(imageInput);
            Assert.IsTrue(IsControlDisplayed(imageInput));
            Assert.IsTrue(IsControlDisplayed(notesInput));
            return this;
        }
        [AllureStep]
        public PartyDataPage InputNote(string note)
        {
            SendKeys(notesInput,note);
            return this;
        }
        [AllureStep]
        public PartyDataPage VerifyCustomerPathWithNote(string note)
        {
            WaitUtil.WaitForElementVisible(collapseIn);
            WaitUtil.WaitForElementVisible(imageInput);
            Assert.IsTrue(IsControlDisplayed(collapseIn));
            Assert.IsTrue(IsControlDisplayed(imageInput));
            Assert.IsTrue(IsControlDisplayed(notesInput));
            Assert.AreEqual(GetAttributeValue(notesInput, "value"), note);
            return this;
        }
        [AllureStep]
        public PartyDataPage VerifyCustomerPathHidden()
        {
            WaitUtil.WaitForElementInvisible(collapseIn);
            Assert.IsTrue(IsControlUnDisplayed(collapseIn));
            return this;
        }

    }
}
