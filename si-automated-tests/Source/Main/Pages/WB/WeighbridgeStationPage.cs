using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.WB
{
    public class WeighbridgeStationPage : BasePageCommonActions
    {
        public readonly By WeighbridgeStationUserTab = By.XPath("//a[@aria-controls='weighbridgeStationUsers-tab']");
        public readonly By AddNewWeighbridgeButton = By.XPath("//button[text()='Add New Item']");
    }
}
