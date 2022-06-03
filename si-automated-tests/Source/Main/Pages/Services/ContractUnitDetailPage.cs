using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ContractUnitDetailPage : BasePage
    {
        private readonly By contractUnitNameInput = By.Id("contract-unit");
        private readonly By clientRefInput = By.Id("client-ref");

        public ContractUnitDetailPage InputContractUnitDetails(string name, string refer)
        {
            SendKeys(contractUnitNameInput, name);
            SendKeys(clientRefInput, refer);
            return this;
        }
    }
}
