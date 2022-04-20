using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreditNoteBatchPage : BasePage
    {
        private readonly By noteInput = By.Id("notes");

        public CreditNoteBatchPage()
        {
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible(noteInput);
        }
        public CreditNoteBatchPage InputNotes(string _noteContent)
        {
            SendKeys(noteInput, _noteContent);
            return this;
        }
    }
}
