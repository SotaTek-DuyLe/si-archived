using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Services
{
    class ContractSiteDetailPage :BasePage
    {
        private readonly By addBtn = By.XPath("//button[text()='Add']");
        private readonly By searchBox = By.XPath("//input[@type='search']");
        private readonly String addressOption = "//li[@class='list-group-item' and contains(text(),'{0}')]";

        [AllureStep]
        public ContractSiteDetailPage ClickAddBtn()
        {
            ClickOnElement(addBtn);
            return this;
        }
        [AllureStep]
        public ContractSiteDetailPage SearchForAddress(string add)
        {
            SendKeys(searchBox, add);
            return this;
        }
        [AllureStep]
        public ContractSiteDetailPage SelectAddress(string add)
        {
            ClickOnElement(addressOption, add);
            return this;
        }
    }
}
