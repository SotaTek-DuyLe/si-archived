using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage
{
    public class AddProductPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Weighbridge Site Product']");
        private readonly By productBtn = By.CssSelector("button[data-id='product']");
        private readonly By productDescInput = By.CssSelector("input[id='site-Product']");
        private readonly By ticketTypeBtn = By.CssSelector("button[data-id='ticket-type']");
        private readonly By defaultLocation = By.CssSelector("select[id='default-location']");
        private readonly By isLocationMandatoryCheckbox = By.XPath("//label[text()='Is Location Mandatory']/following-sibling::div/input");
        private readonly By isRestricLocationCheckbox = By.XPath("//label[text()='Is Restrict Location']/following-sibling::div/input");
        private readonly By startDateInput = By.CssSelector("input[id='start-date']");
        private readonly By endDateInput = By.CssSelector("input[id='end-date']");
        private readonly By clientRefInput = By.CssSelector("input[id='clientReference']");
        private readonly By productBtnExpanded = By.XPath("//button[@data-id='product' and @aria-expanded='true']");
        private readonly By ticketTypeBtnExpanded = By.XPath("//button[@data-id='ticket-type' and @aria-expanded='true']");
        private readonly By defaultLocationDd = By.CssSelector("select[id='default-location']");

        //DYNAMIC LOCATOR
        private const string anyProductOption = "//select[@id='product']/option[text()='{0}']";
        private const string anyTicketTypeOption = "//select[@id='ticket-type']/option[text()='{0}']";
        private const string anyLocationOption = "//select[@id='default-location']/option[text()='{0}']";

        public AddProductPage WaitForAddProductPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        public AddProductPage IsAddProductPage()
        {
            Assert.IsTrue(IsControlDisplayed(productBtn));
            Assert.IsTrue(IsControlDisplayed(productDescInput));
            Assert.IsTrue(IsControlDisplayed(ticketTypeBtn));
            Assert.IsTrue(IsControlDisplayed(defaultLocation));
            Assert.IsTrue(IsControlDisplayed(isLocationMandatoryCheckbox));
            Assert.IsTrue(IsControlDisplayed(isRestricLocationCheckbox));
            Assert.IsTrue(IsControlDisplayed(startDateInput));
            Assert.IsTrue(IsControlDisplayed(endDateInput));
            Assert.IsTrue(IsControlDisplayed(clientRefInput));
            //Default value
            Assert.AreEqual(GetAttributeValue(startDateInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(endDateInput, "value"), CommonConstants.EndDateAgreement);
            //Mandatory field
            Assert.AreEqual(GetCssValue(ticketTypeBtn, "border-color"), "rgb(217, 83, 79)");
            Assert.AreEqual(GetCssValue(productBtn, "border-color"), "rgb(217, 83, 79)");
            return this;
        }

        public AddProductPage ClickAnyProduct(string productName)
        {
            ClickOnElement(productBtn);
            WaitUtil.WaitForElementVisible(productBtnExpanded);
            ClickOnElement(anyProductOption, productName);
            return this;
        }

        public AddProductPage ClickAnyTicketType(string ticketTypeName)
        {
            ClickOnElement(ticketTypeBtn);
            WaitUtil.WaitForElementVisible(ticketTypeBtnExpanded);
            ClickOnElement(anyTicketTypeOption, ticketTypeName);
            return this;
        }

        public AddProductPage ClickDefaultLocationDdAndSelectAnyOption(string locationName)
        {
            ClickOnElement(defaultLocationDd);
            //Select location
            ClickOnElement(anyLocationOption, locationName);
            return this;
        }

        public AddProductPage ClickOnIsLocationMandatoryCheckbox()
        {
            ClickOnElement(isLocationMandatoryCheckbox);
            return this;
        }
    }
}
