using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
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

        private readonly By purchaseNumberColumn = By.XPath("//div[@class='grid-canvas']//div[contains(@class,'r2')]");
        private readonly By fromDateColumn = By.XPath("//div[@class='grid-canvas']//div[contains(@class,'r9')]/div");
        private readonly By toDateColumn = By.XPath("//div[@class='grid-canvas']//div[contains(@class,'r10')]/div");

        private string purchaseOrderNumber = "//div[text()='{0}']";

        [AllureStep]
        public PartyPurchaseOrderPage IsOnPartyPurchaseOrderPage()
        {
            WaitUtil.WaitForElementVisible(purchaseOrderTab);
            Assert.IsTrue(IsControlDisplayed(purchaseOrderTab));    
            Assert.IsTrue(IsControlDisplayed(addNewItem));
            return this;
        }
        [AllureStep]
        public AddPurchaseOrderPage ClickAddNewItem()
        {
            ClickOnElement(addNewItem);
            return new AddPurchaseOrderPage();
        }
        [AllureStep]
        public PartyPurchaseOrderPage SelectPurchaseOrder(String poNumber)
        {
            ClickOnElement(purchaseOrderNumber, poNumber);
            return this;
        }
        [AllureStep]
        public PurchaseOrderDetailsPage OpenPurchaseOrder(String poNumber)
        {
            DoubleClickOnElement(purchaseOrderNumber, poNumber);
            return new PurchaseOrderDetailsPage();
        }
        [AllureStep]
        public PartyPurchaseOrderPage VerifyPurchaseOrderAppear(String poNumber)
        {
            WaitUtil.WaitForElementVisible(purchaseOrderNumber, poNumber);
            Assert.IsTrue(IsControlDisplayed(purchaseOrderNumber, poNumber));
            return this;
        }
        [AllureStep]
        public PartyPurchaseOrderPage VerifyPurchaseOrderDisappear(String poNumber)
        {
            WaitUtil.WaitForElementInvisible(purchaseOrderNumber, poNumber);
            Assert.IsTrue(IsControlUnDisplayed(purchaseOrderNumber, poNumber));
            return this;
        }
        [AllureStep]
        public RemovePurchaseOrderPage ClickDeletePurchaseOrder()
        {
            ClickOnElement(deleteItem);
            return new RemovePurchaseOrderPage();
        }
        [AllureStep]
        public List<PartyPurchaseOrdersModel> GetAllPurchaseOrderInpage()
        {
            
            List<PartyPurchaseOrdersModel> purchaseOrderList = new List<PartyPurchaseOrdersModel>();
            List<IWebElement> fromDateList = GetAllElements(fromDateColumn);
            List<IWebElement> orderList = GetAllElementsNotWait(purchaseNumberColumn);
            List<IWebElement> toDateList = GetAllElementsNotWait(toDateColumn);
            for(int i = 0; i < fromDateList.Count; i++)
            {
                PartyPurchaseOrdersModel purchaseOrder = new PartyPurchaseOrdersModel(GetElementText(orderList[i]), GetElementText(fromDateList[i]), GetElementText(toDateList[i]));
                purchaseOrderList.Add(purchaseOrder);
            }
            return purchaseOrderList;
        }
        [AllureStep]
        public PartyPurchaseOrderPage VerifyPurchaseOrder(string po, string _fromDate, string toDate)
        {
            List<PartyPurchaseOrdersModel> purchaseOrderList = this.GetAllPurchaseOrderInpage();
            PartyPurchaseOrdersModel poInList = purchaseOrderList.Find(p => p.number.Equals(po));
            Assert.AreEqual(poInList.number, po);
            Assert.AreEqual(poInList.fromDate, _fromDate);
            Assert.AreEqual(poInList.toDate, toDate);
            return this;
        }
    }
}
