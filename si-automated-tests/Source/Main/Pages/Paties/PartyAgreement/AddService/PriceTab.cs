using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement.AddService
{
    public class PriceTab : AddServicePage
    {
        private readonly By closeBtns = By.XPath("//tr[contains(@data-bind,'placeholderText') and not(@style='display: none;')]/descendant::button[@title='Retire/Remove']");

        public PriceTab ClosePriceRecords()
        {
            IList<IWebElement> priceRecords = WaitUtil.WaitForAllElementsVisible(closeBtns);
            for(int i = 0; i < 2; i++)
            {
                ClickOnElement(priceRecords[i]);
            }
            return this;
        }
    }
}
