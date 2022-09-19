using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundInstancePage : BasePage
    {
        private readonly By allocatedResourceTab = By.XPath("//a[@aria-controls='allocated-resources-tab']");
        private readonly By allocatedResourceRows = By.XPath("//div[@id='allocated-resources-tab']//table//tbody//tr");

        [AllureStep]
        public RoundInstancePage ClickAllocatedResourcesTab()
        {
            ClickOnElement(allocatedResourceTab);
            return this;
        }
        [AllureStep]
        public List<AllocatedResourceModel> GetAllocatedResources()
        {
            List<AllocatedResourceModel> allocatedResources = new List<AllocatedResourceModel>();
            List<IWebElement> rows = GetAllElements(allocatedResourceRows);
            foreach (var row in rows)
            {
                var cells = row.FindElements(By.XPath("./td")).Select(x => x.Text).ToList();
                allocatedResources.Add(new AllocatedResourceModel()
                {
                    Type = cells[0],
                    Resource = cells[1],
                });
            }
            return allocatedResources;
        }
        [AllureStep]
        public RoundInstancePage VerifyAllocateResourceContainType(string type, string resource)
        {
            var resources = GetAllocatedResources();
            Assert.IsNotNull(resources.FirstOrDefault(x => x.Type == type && x.Resource == resource));
            return this;
        }
    }
}
