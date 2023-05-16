using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SaleInvoicesListPage : BasePage
    {
        private readonly By allSiteRows = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        [AllureStep]
        public SaleInvoicesListPage IsSaleInvoicesListPageLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(allSiteRows);
            return this;
        }

        [AllureStep]
        public SaleInvoicesListPage VerifyDisplayVerticalScrollBarSaleInvoicesListPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }
    }
}
