using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder
{
    public class PurchaseOrderDetailsPage : BasePage
    {
        private string poHeader = "//div[@class='headers-container']/h5[text()='{0}']";

        private readonly By poNumber = By.Id("number");
        private readonly By firstDay = By.Id("start-date");
        private readonly By lastDay = By.Id("end-date");
        private readonly By agreement = By.Id("agreement");
        private readonly By site = By.Id("site");
        private readonly By serviceType = By.Id("serviceType");
        private readonly By objectType = By.XPath("//select[contains(@data-bind, 'objTypes')]");
        private readonly By objectId = By.XPath("//span[contains(@data-bind, 'objectId')]");
        private readonly By removeBtn = By.XPath("//button[text()='Remove']");
        private readonly By addBtn = By.XPath("//button[text()='Add']");

        [AllureStep]
        public PurchaseOrderDetailsPage WaitingForPurchaseOrderPageLoadedSuccessfully(string po)
        {
            WaitUtil.WaitForElementVisible(poHeader, po);
            return this;
        }
        //All fields are disabled apart from 'First Day' and 'Last Day'
        //In Criteria section, Object=Task, ObjectID= Task ID
        [AllureStep]
        public PurchaseOrderDetailsPage VerifyDetailsPageWithDateEnabled(string startDate, string endDate, string taskID)
        {
            Assert.IsFalse(IsControlEnabled(poNumber));
            Assert.IsFalse(IsControlEnabled(agreement));
            Assert.IsFalse(IsControlEnabled(site));
            Assert.IsFalse(IsControlEnabled(serviceType));
            Assert.IsFalse(IsControlEnabled(objectType));
            Assert.IsFalse(IsControlEnabled(removeBtn));
            Assert.IsFalse(IsControlEnabled(addBtn));
            Assert.IsTrue(IsControlEnabled(firstDay));
            Assert.IsTrue(IsControlEnabled(lastDay));
            Assert.AreEqual(GetAttributeValue(firstDay, "value"), startDate);
            Assert.AreEqual(GetAttributeValue(lastDay, "value"), endDate);
            Assert.AreEqual(GetElementText(objectId), taskID);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(objectType), "Task");
            return this;
        }
    }
}
