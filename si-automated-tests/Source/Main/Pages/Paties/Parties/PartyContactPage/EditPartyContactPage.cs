using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage
{
    public class EditPartyContactPage : BasePage
    {
        private readonly By contactTitle = By.XPath("//h4[text()='CONTACT']");
        private readonly By detailTab = By.XPath("//a[text()='Details']");
        private readonly By notesTab = By.XPath("//a[text()='Notes']");

        //DETAIL TAB LOCATOR
        private readonly By titleInput = By.CssSelector("input#contact-title");
        private readonly By firstNameInput = By.CssSelector("input#contact-first-name");
        private readonly By lastNameInput = By.CssSelector("input#contact-last-name");
        private readonly By position = By.CssSelector("input#contact-position");
        private readonly By greeting = By.CssSelector("input#contact-greeting");
        private readonly By telephone = By.CssSelector("input#contact-telephone");
        private readonly By mobile = By.CssSelector("input#contact-mobile");
        private readonly By email = By.CssSelector("input#contact-email");
        private readonly By receivedEmail = By.XPath("//label[text()='Receive Emails']/following-sibling::div//input");
        private readonly By contactGroups = By.CssSelector("button[data-id='contact-groups']");
        private readonly By startDate = By.CssSelector("input#start-date");
        private readonly By endDate = By.CssSelector("input#end-date");
        private readonly By headingNote = By.XPath("//div[@class='panel-heading']/p");
        private readonly By bodyNote = By.XPath("//div[@class='panel-body']/p");

        //NOTES TAB LOCATOR
        private readonly By titleNotes = By.CssSelector("input#note-title");
        private readonly By noteArea = By.CssSelector("textarea#new-note");
        private readonly By addBtn = By.XPath("//button[text()='Add']");

        //DYNAMIC LOCATOR
        private const string fullName = "//p[text()='{0}']";

        public EditPartyContactPage IsEditPartyContactPage(ContactModel contactModel)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(contactTitle);
            WaitUtil.WaitForElementVisible(string.Format(fullName, contactModel.FirstName + " " + contactModel.LastName));
            //Verify default value
            Assert.AreEqual(contactModel.Title, GetAttributeValue(titleInput, "value"));
            Assert.AreEqual(contactModel.FirstName, GetAttributeValue(firstNameInput, "value"));
            Assert.AreEqual(contactModel.LastName, GetAttributeValue(lastNameInput, "value"));
            Assert.AreEqual(contactModel.Position, GetAttributeValue(position, "value"));
            //Assert.AreEqual(contactModel.Greeting, GetAttributeValue(greeting, "value"));
            Assert.AreEqual(contactModel.Telephone, GetAttributeValue(telephone, "value"));
            //Assert.AreEqual(contactModel.Mobile, GetAttributeValue(mobile, "value"));
            Assert.AreEqual(contactModel.Email, GetAttributeValue(email, "value"));
            Assert.AreEqual(contactModel.ReceiveEmail, GetElement(receivedEmail).Selected);
            Assert.AreEqual(contactModel.ContactGroups, GetElementText(contactGroups).Trim());
            Assert.AreEqual(contactModel.StartDate, GetAttributeValue(startDate, "value") + " 00:00");
            Assert.AreEqual(contactModel.EndDate, GetAttributeValue(endDate, "value") + " 00:00");
            return this;
        }

        public EditPartyContactPage EnterAllValueFields(ContactModel contactModel)
        {
            SendKeys(firstNameInput, contactModel.FirstName);
            SendKeys(lastNameInput, contactModel.LastName);
            SendKeys(titleInput, contactModel.Title);
            SendKeys(position, contactModel.Position);
            SendKeys(greeting, contactModel.Greeting);
            SendKeys(telephone, contactModel.Telephone);
            SendKeys(mobile, contactModel.Mobile);
            SendKeys(email, contactModel.Email);
            if (!contactModel.ReceiveEmail)
            {
                ClickOnElement(receivedEmail);
            }
            SendKeys(startDate, CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2));
            return this;
        }

        public EditPartyContactPage NavigateToNotesTab()
        {
            ClickOnElement(notesTab);
            return this;
        }

        public EditPartyContactPage NavigateToDetailsTab()
        {
            ClickOnElement(detailTab);
            return this;
        }

        //NOTE TAB
        public EditPartyContactPage IsNotesTab()
        {
            Assert.IsTrue(IsControlDisplayed(titleNotes));
            Assert.IsTrue(IsControlDisplayed(noteArea));
            //Add Btn disabled
            Assert.AreEqual(GetAttributeValue(addBtn, "disabled"), "true");
            return this;
        }

        public EditPartyContactPage EnterTitleAndNoteField(string title, string note)
        {
            SendKeys(titleNotes, title);
            SendKeys(noteArea, note);
            ClickOnElement(addBtn);
            return this;
        }

        public EditPartyContactPage VerifyTitleAndNoteAfter()
        {
            Assert.AreEqual(GetAttributeValue(titleNotes, "value"), "");
            Assert.AreEqual(GetAttributeValue(noteArea, "value"), "");
            //Add Btn disabled
            Assert.AreEqual(GetAttributeValue(addBtn, "disabled"), "true");
            return this;
        }

        public EditPartyContactPage GetAndVerifyNoteAfterAdding(string title, string body, string userLogin)
        {
            Assert.AreEqual(title + CommonUtil.GetUtcTimeNow("dd/MM/yyyy hh:mm"), GetElementText(headingNote));
            List<IWebElement> allContent = GetAllElements(bodyNote);
            Assert.AreEqual("User " + userLogin + " wrote", GetElementText(allContent[0]));
            Assert.AreEqual(body, GetElementText(allContent[1]));
            return this;
        }
    }
}
