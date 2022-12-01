using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesInvoiceBatchesConfirmPostPage : BasePage
    {
        private readonly By textInfo = By.XPath("//div[@class='text-info']");
        private readonly By yesbtn = By.XPath("//div[contains(@class, 'bootbox modal fade in')]//button[contains(string(), 'Yes')]");
        
        [AllureStep]
        public SalesInvoiceBatchesConfirmPostPage VerifyTextInfo(string title)
        {
            Assert.IsTrue(GetElementText(textInfo) == title);
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesConfirmPostPage ClickYesBtn()
        {
            ClickOnElement(yesbtn);
            return this;
        }
    }
}
