using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class AnnouncementDetailPage : BasePage
    {
        private readonly By announcementTypeSelect = By.Id("announcement-type");
        private readonly By announcemenTextInput = By.Id("announcement");
        private readonly By impactSelect = By.Id("impact-code");
        private readonly By validFromInput = By.Id("validFrom");
        private readonly By valiToInput = By.Id("validTo");

        public AnnouncementDetailPage IsOnDetailPage()
        {
            WaitUtil.WaitForElementVisible(announcementTypeSelect);
            WaitUtil.WaitForElementVisible(announcemenTextInput);
            WaitUtil.WaitForElementVisible(impactSelect);
            WaitUtil.WaitForElementVisible(validFromInput);
            WaitUtil.WaitForElementVisible(valiToInput);
            return this;
        }
        public AnnouncementDetailPage InputDetails(string type, string text, string impact, string from, string to)
        {
            
            SelectTextFromDropDown(announcementTypeSelect, type);
            SendKeys(announcemenTextInput, text);
            SelectTextFromDropDown(impactSelect, impact);
            SendKeys(validFromInput, from);
            SendKeys(valiToInput, to);
            return this;
        }
    }
}
