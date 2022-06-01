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


        public ContractSiteDetailPage ClickAddBtn()
        {
            ClickOnElement(addBtn);
            return this;
        }
        public ContractSiteDetailPage SearchForAddress(string add)
        {
            SendKeys(searchBox, add);
            return this;
        }
        public ContractSiteDetailPage SelectAddress(string add)
        {
            ClickOnElement(addressOption, add);
            return this;
        }
    }
}
