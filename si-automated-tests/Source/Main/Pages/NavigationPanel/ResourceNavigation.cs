using OpenQA.Selenium;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class ResourceNavigation : NavigationBase
    {
        private readonly By dailyAllocation = By.XPath("//span[text()='Daily Allocation']"); 
        private readonly By defaultyAllocation = By.XPath("//span[text()='Default Allocation']"); 
        private readonly By externalResources = By.XPath("//span[text()='External Resources']");
    }
}
