using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Resources.Tabs
{
    public class VehicleCustomerHaulierPage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInput = By.CssSelector("input[title='Select/Deselect up to 100 records']");

        //DYNAMIC
        private const string Column = "//span[text()='{0}']/parent::div";

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

        public CreateVehicleCustomerHaulierPage ClickAddNewItemBtn()
        {
            ClickOnElement(addNewItemBtn);
            return PageFactoryManager.Get< CreateVehicleCustomerHaulierPage>();
        }
    }
}
