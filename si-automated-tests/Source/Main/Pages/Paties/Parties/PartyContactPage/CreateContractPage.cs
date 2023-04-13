using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage
{
    public class CreateContractPage : BasePageCommonActions
    {
        public readonly By ContractSelect = By.Id("party");
        public readonly By NextButton = By.XPath("//button[text()='Next']");
    }
}
