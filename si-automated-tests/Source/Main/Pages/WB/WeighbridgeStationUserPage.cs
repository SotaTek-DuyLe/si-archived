using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.WB
{
    public class WeighbridgeStationUserPage : BasePageCommonActions
    {
        public readonly By UserToggleDropdownButton = By.XPath("//button[@data-id='users']");
        public readonly By SearchUserInput = By.XPath("//div[@class='dropdown-menu open']//input");
        public readonly By NoResultLabel = By.XPath("//div[@class='dropdown-menu open']//li[@class='no-results']");
    }
}
