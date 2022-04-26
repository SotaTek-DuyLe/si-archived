using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder
{
    //Purchase Order Tab inside a Party
    public class PartyPurchaseOrderPage : BasePage
    {
        private static string purchaseOrderTab = "//div[@id='purchaseOrders-tab']";
        private readonly string addNewItem = purchaseOrderTab + "//button[text()='Add New Item']";
        private readonly string deleteItem = purchaseOrderTab + "//button[text()='Delete Item']";

        private readonly By purchaseNumerColumn = By.XPath("//div[@class='grid-canvas']/div/div[count(//span[text()='Number']/parent::div/preceding-sibling::div) + 1]");
        private readonly By fromDateColumn = By.XPath("//div[@class='grid-canvas']/div/div[10]/div");
        private readonly By toDateColumn = By.XPath("//div[@class='grid-canvas']/div/div[11]/div");

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
        public PurchaseOrderDetailsPage OpenPurchaseOrder(String poNumber)
        {
            DoubleClickOnElement(purchaseOrderNumber, poNumber);
            return new PurchaseOrderDetailsPage();
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

        public List<PartyPurchaseOrdersModel> GetAllPurchaseOrderInpage()
        {
            
            List<PartyPurchaseOrdersModel> purchaseOrderList = new List<PartyPurchaseOrdersModel>();
            List<IWebElement> orderList = GetAllElements(purchaseNumerColumn);
            List<IWebElement> fromDateList = GetAllElements(fromDateColumn);
            List<IWebElement> toDateList = GetAllElements(toDateColumn);
            for(int i = 0; i < orderList.Count; i++)
            {
                PartyPurchaseOrdersModel purchaseOrder = new PartyPurchaseOrdersModel(GetElementText(orderList[i]), GetElementText(fromDateList[i]), GetElementText(toDateList[i]));
                purchaseOrderList.Add(purchaseOrder);
            }
            return purchaseOrderList;
        }
        public PartyPurchaseOrderPage VerifyPurchaseOrder(string po, string fromDate, string toDate)
        {
            List<PartyPurchaseOrdersModel> purchaseOrderList = this.GetAllPurchaseOrderInpage();
            PartyPurchaseOrdersModel poInList = purchaseOrderList.Find(p => p.number.Equals(po));
            Assert.AreEqual(poInList.number, po);
            Assert.AreEqual(poInList.fromDate, fromDate);
            Assert.AreEqual(poInList.toDate, toDate);
            return this;
        }
    }
}
