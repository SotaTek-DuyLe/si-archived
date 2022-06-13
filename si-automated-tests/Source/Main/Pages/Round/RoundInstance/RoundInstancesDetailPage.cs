using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Round.RoundInstance
{
    public class RoundInstancesDetailPage : BasePage
    {
        private readonly By roundInstanceTitle = By.XPath("//h4[text()='Round Instance']");

        public RoundInstancesDetailPage WaitForRoundInstanceDetailPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(roundInstanceTitle);
            return this;
        }

        public RoundInstancesDetailPage VerifyCurrentUrlRoundPage(string roundInstanceId)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/round-instances/" + roundInstanceId, GetCurrentUrl());
            return this;
        }

    }
}
