using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages
{
    public class NoteTabPage : BasePageCommonActions
    {
        public readonly By NoteTab = By.XPath("//a[@aria-controls='notes-tab']");
        public readonly By TitleInput = By.XPath("//div[@id='notes-tab']//input[@id='note-title']");
        public readonly By NoteTextArea = By.XPath("//div[@id='notes-tab']//textarea[@id='new-note']");
        public readonly By AddNoteButton = By.XPath("//div[@id='notes-tab']//button[text()='Add']");

        public readonly By NewTitleLabel = By.XPath("(//div[@data-bind='foreach: visibleNotes']//p[contains(@data-bind, 'title')])[1]");
        public readonly By NewNotesLabel = By.XPath("(//div[@data-bind='foreach: visibleNotes']//p[@data-bind='text: note'])[1]");

        public NoteTabPage VerifyNewNote(string title, string note)
        {
            Assert.IsTrue(GetElementText(NewTitleLabel).Contains(title));
            Assert.IsTrue(GetElementText(NewNotesLabel) == note);
            return this;
        }
    }
}
