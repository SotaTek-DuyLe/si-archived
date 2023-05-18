using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.PartyAgreement
{
    public class AgreementListPage : BasePage
    {
        private readonly By allAgreementRows = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        [AllureStep]
        public AgreementListPage IsAgreementListPageLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(allAgreementRows);
            return this;
        }

        [AllureStep]
        public AgreementListPage VerifyDisplayVerticalScrollBarAgreementListPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }
    }
}
