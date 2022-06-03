using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Resources.Tabs
{
    public class VehicleCustomerHaulierPage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInput = By.CssSelector("input[title='Select/Deselect up to 100 records']");
        private const string TotalSiteRow = "//div[@class='grid-canvas']/div";
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");

        //DYNAMIC
        private const string Column = "//span[text()='{0}']/parent::div";
        private const string ColumnInRow = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        public VehicleCustomerHaulierPage VerifyVehicleCustomerHaulierPageDisplayed()
        {
            WaitForLoadingIconToDisappear();
            Assert.IsTrue(IsControlDisplayed(filterInput));
            foreach (String column in CommonConstants.ColumnInVehicleCustomerHaulierPage)
            {
                Assert.IsTrue(IsControlDisplayed(Column, column));
            }
            return this;
        }

        public VehicleCustomerHaulierPage FilterVehicleById(string id)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            SendKeys(filterInputById, id);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public CreateVehicleCustomerHaulierPage ClickAddNewItemBtn()
        {
            ClickOnElement(addNewItemBtn);
            return PageFactoryManager.Get< CreateVehicleCustomerHaulierPage>();
        }

        public List<VehicleModel> GetAllVehicleModel()
        {
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            List<IWebElement> totalRow = GetAllElements(TotalSiteRow);
            List<IWebElement> allIdVehicle = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[0]));
            List<IWebElement> allResource = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[1]));
            List<IWebElement> allCustomer = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[2]));
            List<IWebElement> allHaulier = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[3]));
            List<IWebElement> allHireStart = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[4]));
            List<IWebElement> allHireEnd = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[5]));
            List<IWebElement> allSuspendedDate = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[0]));
            List<IWebElement> allSuspendedReason = GetAllElements(string.Format(ColumnInRow, CommonConstants.ColumnInVehicleCustomerHaulierPage[0]));
            for (int i = 0; i < totalRow.Count(); i++)
            {
                string id = GetElementText(allIdVehicle[i]);
                string resource = GetElementText(allResource[i]);
                string customer = GetElementText(allCustomer[i]);
                string haulier = GetElementText(allHaulier[i]);
                string hireStart = GetElementText(allHireStart[i]);
                string hireEnd = GetElementText(allHireEnd[i]);
                string suspendedDate = GetElementText(allSuspendedDate[i]);
                string suspendedReason = GetElementText(allSuspendedReason[i]);
                vehicleModels.Add(new VehicleModel(id, resource, customer, haulier, hireStart, hireEnd, suspendedDate, suspendedReason));
            }
            return vehicleModels;
        }

        public VehicleCustomerHaulierPage VerifyVehicleCreated(VehicleModel vehicleModelDisplayed, string resource, string customer, string haulier, string hireStart, string hireEnd)
        {
            Assert.AreEqual(resource, vehicleModelDisplayed.Resource);
            Assert.AreEqual(customer, vehicleModelDisplayed.Customer);
            Assert.AreEqual(haulier, vehicleModelDisplayed.Haulier);
            Assert.AreEqual(hireStart, vehicleModelDisplayed.HireStart);
            Assert.AreEqual(hireEnd, vehicleModelDisplayed.HireEnd);
            return this;
        }
    }
}
