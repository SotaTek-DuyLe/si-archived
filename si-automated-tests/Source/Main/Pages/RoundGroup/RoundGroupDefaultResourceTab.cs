using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Round
{
    public class RoundGroupDefaultResourceTab : BasePage
    {
        private readonly By syncResourceBtn = By.XPath("//button[contains(text(),'Sync Round Resources')]");
        public RoundGroupDefaultResourceTab IsOnDefaultResourceTab()
        {
            WaitUtil.WaitForElementVisible(syncResourceBtn);
            return this;
        }
    }
}
