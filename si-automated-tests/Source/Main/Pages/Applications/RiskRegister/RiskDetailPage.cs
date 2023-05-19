using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Streets;

namespace si_automated_tests.Source.Main.Pages.Applications.RiskRegister
{
    public class RiskDetailPage : BasePageCommonActions
    {

        //DYNAMIC
        private const string AnyDiv = "//div[text()='{0}']";
        public readonly By HyperLink = By.XPath("//div[@data-bind='text: objectDescription, click: openObjectDetailsWindow']");
        public readonly By DetailTab = By.XPath("//a[@data-toggle='tab' and text()='Details']");
        public readonly By StartDateInput = By.XPath("//div[@id='details']//input[@id='start-date']");
        public readonly By EndDateInput = By.XPath("//div[@id='details']//input[@id='end-date']");
        public readonly By ProximityInput = By.XPath("//div[@id='details']//input[@id='proximity']");
        public readonly By NoteInput = By.XPath("//div[@id='details']//textarea[@id='notes']");

        [AllureStep]
        public RiskDetailPage IsRiskDetailPage(string riskDescName, string riskName)
        {
            WaitUtil.WaitForElementVisible(AnyDiv, riskDescName);
            WaitUtil.WaitForElementVisible(AnyDiv, riskName);
            Assert.IsTrue(IsControlDisplayed(AnyDiv, riskDescName));
            return this;
        }
        [AllureStep]

        public StreetDetailPage ClickOnAddressHeader(string addressName)
        {
            ClickOnElement(AnyDiv, addressName);
            return PageFactoryManager.Get<StreetDetailPage>();
        }
    }
}
