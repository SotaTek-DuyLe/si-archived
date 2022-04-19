using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder
{
    public class RemovePurchaseOrderPage : BasePage
    {
        private const string title = "//div[text()='Do you wish to remove this purchase order?']";
        private const string yesBtn = "//button[text()='Yes']";
        private const string noBtn = "//button[text()='No']";

        public RemovePurchaseOrderPage IsOnRemovePurchaseOrderPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(yesBtn);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.IsTrue(IsControlDisplayed(yesBtn));
            Assert.IsTrue(IsControlDisplayed(noBtn));
            return this;
        }
        public RemovePurchaseOrderPage ClickYesBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }
        public RemovePurchaseOrderPage ClickNoBtn()
        {
            ClickOnElement(noBtn);
            return this;
        }
    }
}
