using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage
{
    public class CreatePartyContactPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='CONTACT']");
        private readonly By titleInput = By.CssSelector("input#contact-title");
        private readonly By contactFistNameInput = By.CssSelector("input#contact-first-name");
        private readonly By contactLastNameInput = By.CssSelector("input#contact-last-name");
        private readonly By contactPositionInput = By.CssSelector("input#contact-position");
        private readonly By greetingInput = By.CssSelector("input#contact-greeting");
        private readonly By telephoneInput = By.CssSelector("input#contact-telephone");
        private readonly By mobileInput = By.CssSelector("input#contact-mobile");
        private readonly By emailInput = By.CssSelector("input#contact-email");
        private readonly By receiveEmailCheckbox = By.XPath("//label[text()='Receive Emails']/following-sibling::div//input");
        private readonly By contactGroups = By.CssSelector("button[data-id='contact-groups']");
        private readonly By startDate = By.CssSelector("input#start-date");
        private readonly By endDate = By.CssSelector("input#end-date");
        private readonly By selectAllBtn = By.XPath("//button[contains(@class, 'bs-select-all')]");
        private readonly By deSelectAllBtn = By.XPath("//button[contains(@class, 'bs-deselect-all')]");
        private readonly By notesTab = By.CssSelector("a[aria-controls='notes-tab']");
        private readonly By titleInNotesTab = By.XPath("//label[text()='Title']/following-sibling::input");
        private readonly By noteInNotesTab = By.XPath("//label[text()='Note']/following-sibling::textarea");

        //DYNAMIC LOCATOR
        private const string anyContactGroups = "//ul[contains(@class, 'dropdown-menu')]//a/span[text()='{0}']";

        [AllureStep]
        public CreatePartyContactPage IsCreatePartyContactPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(title);
            return this;
        }
        [AllureStep]
        public CreatePartyContactPage EnterFirstName(string firstName)
        {
            SendKeys(contactFistNameInput, firstName);
            return this;
        }
        [AllureStep]

        public CreatePartyContactPage EnterLastName(string lastName)
        {
            SendKeys(contactLastNameInput, lastName);
            return this;
        }
        [AllureStep]
        public CreatePartyContactPage EnterMobileValue(string mobile)
        {
            SendKeys(mobileInput, mobile);
            return this;
        }

        [AllureStep]
        public CreatePartyContactPage EnterContactInfo(ContactModel contactModel)
        {
            SendKeys(titleInput, contactModel.Title);
            SendKeys(contactFistNameInput, contactModel.FirstName);
            SendKeys(contactLastNameInput, contactModel.LastName);
            SendKeys(contactPositionInput, contactModel.Position);
            SendKeys(mobileInput, contactModel.Mobile);
            return this;
        }

        [AllureStep]
        public CreatePartyContactPage EnterValueRemainingFields(ContactModel contactModel)
        {
            SendKeys(titleInput, contactModel.Title);
            SendKeys(contactPositionInput, contactModel.Position);
            SendKeys(greetingInput, contactModel.Greeting);
            SendKeys(telephoneInput, contactModel.Telephone);
            SendKeys(emailInput, contactModel.Email);
            if(contactModel.ReceiveEmail)
            {
                ClickOnElement(receiveEmailCheckbox);
            }
            
            return this;
        }
        [AllureStep]
        public CreatePartyContactPage ClickAnyContactGroupsAndVerify(string contactGroup)
        {
            //Click contact groups and verify
            ClickOnElement(contactGroups);
            Assert.IsTrue(IsControlDisplayed(selectAllBtn));
            Assert.IsTrue(IsControlDisplayed(deSelectAllBtn));
            ClickOnElement(anyContactGroups, contactGroup);
            return this;
        }

        [AllureStep]
        public CreatePartyContactPage ClickOnNotesTab()
        {
            ClickOnElement(notesTab);
            return this;
        }

        [AllureStep]
        public CreatePartyContactPage VerifyDisplayNotesTab()
        {
            Assert.IsTrue(IsControlDisplayed(titleInNotesTab), "Title in Notes tab is not displayed");
            Assert.IsTrue(IsControlDisplayed(noteInNotesTab), "Note in Notes tab is not displayed");
            return this;
        }
    }

}
