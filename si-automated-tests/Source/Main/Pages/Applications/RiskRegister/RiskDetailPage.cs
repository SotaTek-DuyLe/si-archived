using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Streets;

namespace si_automated_tests.Source.Main.Pages.Applications.RiskRegister
{
    public class RiskDetailPage : BasePage
    {

        //DYNAMIC
        private const string AnyDiv = "//div[text()='{0}']";

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
