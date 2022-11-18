using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class SubscriptionsDetailPage : BasePageCommonActions
    {
        public readonly By TitleInput = By.XPath("//input[@id='contact-title']");
        public readonly By FirstNameInput = By.XPath("//input[@id='contact-first-name']");
        public readonly By LastNameInput = By.XPath("//input[@id='contact-last-name']");
        public readonly By PositionInput = By.XPath("//input[@id='contact-position']");
        public readonly By TelephoneInput = By.XPath("//input[@id='contact-telephone']");
        public readonly By MobileInput = By.XPath("//input[@id='contact-mobile']");
        public readonly By NotesInput = By.XPath("//textarea[@id='subcription-notes']");
        public readonly By SubjectDescriptionText = By.XPath("//p[contains(@data-bind, 'objectType')]");
        public readonly By IdTitle = By.XPath("//h4[@title='Id']");
    }
}
