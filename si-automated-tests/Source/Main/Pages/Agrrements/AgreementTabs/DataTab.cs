using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs
{
    public class DataTab : BasePage
    {
        private readonly By notesInput = By.XPath("//label[text()='Notes']/following-sibling::input");

        public DataTab IsOnDataTab()
        {
            WaitUtil.WaitForElementVisible(notesInput);
            Assert.IsTrue(IsControlDisplayed(notesInput));
            return this;
        }
        public DataTab InputNotes(string note)
        {
            SendKeys(notesInput, note);
            return this;
        }
        public DataTab VerifyNote(string note)
        {
            Assert.AreEqual(note, GetAttributeValue(notesInput, "value"));
            return this;
        }

    }
}
