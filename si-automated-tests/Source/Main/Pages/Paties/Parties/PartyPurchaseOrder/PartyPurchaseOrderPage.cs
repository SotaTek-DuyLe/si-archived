using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder
{
    //Purchase Order Tab inside a Party
    public class PartyPurchaseOrderPage : BasePage
    {
        private static string purchaseOrderTab = "//div[@id='purchaseOrders-tab']";
        private readonly string addNewItem = purchaseOrderTab + "//button[text()='Add New Item']";
        private readonly string deleteItem = purchaseOrderTab + "//button[text()='Delete Item']";

        private string purchaseOrderNumber = "//div[text()='{0}']";
        public PartyPurchaseOrderPage IsOnPartyPurchaseOrderPage()
        {
            WaitUtil.WaitForElementVisible(purchaseOrderTab);
            Assert.IsTrue(IsControlDisplayed(purchaseOrderTab));    
            Assert.IsTrue(IsControlDisplayed(addNewItem));
            return this;
        }

        public AddPurchaseOrderPage ClickAddNewItem()
        {
            ClickOnElement(addNewItem);
            return new AddPurchaseOrderPage();
        }

        public PartyPurchaseOrderPage SelectPurchaseOrder(String poNumber)
        {
            ClickOnElement(purchaseOrderNumber, poNumber);
            return this;
        }
        public PartyPurchaseOrderPage VerifyPurchaseOrderAppear(String poNumber)
        {
            WaitUtil.WaitForElementVisible(purchaseOrderNumber, poNumber);
            Assert.IsTrue(IsControlDisplayed(purchaseOrderNumber, poNumber));
            return this;
        }
        public PartyPurchaseOrderPage VerifyPurchaseOrderDisappear(String poNumber)
        {
            WaitUtil.WaitForElementInvisible(purchaseOrderNumber, poNumber);
            Assert.IsTrue(IsControlUnDisplayed(purchaseOrderNumber, poNumber));
            return this;
        }
        public RemovePurchaseOrderPage ClickDeletePurchaseOrder()
        {
            ClickOnElement(deleteItem);
            return new RemovePurchaseOrderPage();
        }
    }
}
