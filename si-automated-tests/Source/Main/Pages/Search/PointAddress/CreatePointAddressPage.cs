using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Search.PointAddress
{
    public class CreatePointAddressPage : BasePage
    {
        //First screen
        private readonly By searchInput = By.Id("search");
        private readonly string btnNamed = "//div[@style='display: block;']//button[contains(text(),'{0}')]";
        private readonly By searchResultScreen1 = By.XPath("//div[@id='screen1']//div[@data-bind='foreach: geocodedAddresses']/div");

        //Second Screen
        private readonly By searchResultScreen2 = By.XPath("//div[@id='screen2']//div[@data-bind='foreach: filterExistingAddresses']/div");

        //Third Screen
        private readonly By propertyName = By.Id("property-name");
        private readonly By property = By.Id("property");
        private readonly By toProperty = By.Id("to-property");
        private readonly By pointSegmentBtn = By.XPath("//button[@data-id='point-segmenets']");
        private readonly By pointAddressTypeBtn = By.XPath("//button[@data-id='point-address-types']");
        private readonly string options = "//span[text()='{0}']";

        //Pop-up
        private readonly By confirmBtn = By.XPath("//button[@data-bb-handler='confirm']");



        [AllureStep]
        public CreatePointAddressPage IsOnFirstScreen()
        {
            WaitUtil.WaitForElementVisible(searchInput);
            WaitUtil.WaitForElementVisible(btnNamed, "Search");
            WaitUtil.WaitForElementVisible(btnNamed, "Create Manually");
            WaitUtil.WaitForElementVisible(btnNamed, "Next");
            WaitUtil.WaitForElementVisible(btnNamed, "Cancel");
            return this;
        }
        [AllureStep]
        public CreatePointAddressPage IsOnSecondScreen()
        {
            WaitUtil.WaitForElementVisible(searchResultScreen2);
            WaitUtil.WaitForElementVisible(btnNamed, "Back");
            WaitUtil.WaitForElementVisible(btnNamed, "Next");
            WaitUtil.WaitForElementVisible(btnNamed, "Cancel");
            return this;
        }
        [AllureStep]
        public CreatePointAddressPage IsOnThirdScreen()
        {
            WaitUtil.WaitForElementVisible(propertyName);
            WaitUtil.WaitForElementVisible(property);
            WaitUtil.WaitForElementVisible(toProperty);
            WaitUtil.WaitForElementVisible(pointSegmentBtn);
            WaitUtil.WaitForElementVisible(pointAddressTypeBtn);
            WaitUtil.WaitForElementVisible(btnNamed, "Back");
            WaitUtil.WaitForElementVisible(btnNamed, "Create");
            WaitUtil.WaitForElementVisible(btnNamed, "Cancel");
            return this;
        }
        [AllureStep]
        public CreatePointAddressPage SearchPostCode(string value)
        {
            SendKeys(searchInput, value);
            ClickOnElement(btnNamed, "Search");
            return this;
        }
        [AllureStep]
        public CreatePointAddressPage SelectResultInScreen1(string value)
        {
            IList<IWebElement> list = WaitUtil.WaitForAllElementsVisible(searchResultScreen1);
            foreach(var result in list)
            {
                if(GetElementText(result).Contains(value))
                {
                    ClickOnElement(result);
                    break;
                }
            }
            ClickOnElement(btnNamed, "Next");
            return this;
        }
        [AllureStep]
        public CreatePointAddressPage SelectResultInScreen2(string value)
        {
            IList<IWebElement> list = WaitUtil.WaitForAllElementsVisible(searchResultScreen2);
            foreach(var result in list)
            {
                if(GetElementText(result).Contains(value))
                {
                    ClickOnElement(result);
                    break;
                }
            }
            ClickOnElement(btnNamed, "Next");
            return this;
        }
        [AllureStep]
        public CreatePointAddressPage InputValuesInScreen3(string _propertyName, string _property, string _toProperty, string _pointSegment, string _pointAddType)
        {
            SendKeys(propertyName, _propertyName);
            SendKeys(property, _property);
            SendKeys(toProperty, _toProperty);
            ClickOnElement(pointSegmentBtn);
            ClickOnElement(options, _pointSegment);
            ClickOnElement(pointAddressTypeBtn);
            ClickOnElement(options, _pointAddType);
            ClickOnElement(btnNamed, "Create");
            ClickOnElement(confirmBtn);
            return this;
        }


    }
}
