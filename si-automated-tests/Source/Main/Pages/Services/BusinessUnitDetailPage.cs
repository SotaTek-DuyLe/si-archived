using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class BusinessUnitDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[contains(string(), 'Business Unit:')]");
        private readonly By detailTab = By.XPath("//a[text()='Details']");
        private readonly By resourcingTab = By.XPath("//a[text()='Resourcing']");
        private readonly By businessUnitGroupDd = By.XPath("//label[text()='Business Unit Group']/following-sibling::select");
        private readonly By businessUnitGroupOption = By.XPath("//label[text()='Business Unit Group']/following-sibling::select/option");

        [AllureStep]
        public BusinessUnitDetailPage IsBusinessUnitDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            WaitUtil.WaitForElementVisible(resourcingTab);
            return this;
        }

        [AllureStep]
        public List<string> ClickOnBusinessUnitGroupDdAndGetText()
        {
            ClickOnElement(businessUnitGroupDd);
            List<string> allBusinessGroup = GetTextFromDd(businessUnitGroupOption);
            return allBusinessGroup;
        }

        [AllureStep]
        public BusinessUnitDetailPage VerifyOnlyBusinessUnitGroupForContractDisplayed(List<string> businessGroupDisplayed, List<string> businessGroupExp)
        {
            Assert.IsTrue(Enumerable.SequenceEqual(businessGroupDisplayed.OrderBy(e => e), businessGroupExp.OrderBy(e => e)));
            return this;
        }
    }
}
