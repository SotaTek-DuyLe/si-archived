using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Services;

namespace si_automated_tests.Source.Main.Pages.Common
{
    public class UserDetailPage : BasePageCommonActions
    {
        public readonly By HomeContractSelect = By.XPath("//td//select");
        public readonly By DataAccessRoleTab = By.XPath("//a[text()='Data Access Roles']");
        public readonly By tfUserCheckbox = By.XPath("(//td//span[contains(text(), 'tfuser')]//parent::td)//following-sibling::td//input");
        public readonly By RichmondCheckbox = By.XPath("(//td//span[contains(text(), 'Richmond')]//parent::td)[1]//following-sibling::td//input");
    }
}
