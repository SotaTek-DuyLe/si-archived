using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyHistory
{
    public class PartyHistoryPage : BasePage
    {
        //private readonly By firstAccountingRef = By.XPath("//div[@id='partyHistory-tab']//div[contains(@data-bind,'html: ew.renderChangesHtml($data.changes)')]");
        private readonly By firstAccountingRef = By.XPath("//div[@id='partyHistory-tab']//div[@class='pull-left']/div");

        [AllureStep]
        public PartyHistoryPage VerifyNewestAccountingReference(string expected)
        {
            var actualText = GetElementText(firstAccountingRef);
            if (expected.Equals(""))
            {
                Assert.IsFalse(actualText.Contains("Accounting Reference: " + expected + "."), "Expected " + expected + " to be inclulded in " + actualText);
            }
            else
            {
                Assert.IsTrue(actualText.Contains("Accounting Reference: " + expected + "."), "Expected " + expected + " to be inclulded in " + actualText);
            }
            return this;
        }
    }
}
