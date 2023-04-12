using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Common
{
    public class EventComplaintPage : BasePageCommonActions
    {
        public readonly By ServiceTab = By.XPath("//a[@aria-controls='services-tab']");
        private readonly By ServiceRows = By.XPath("//div[@id='services-tab']//div[@class='parent-row']//div[@data-bind='foreach: $data.asset']");

        [AllureStep]
        public EventComplaintPage VerifyPointAddress(List<string> assetTypes)
        {
            var serviceRows = GetAllElements(ServiceRows);
            for (int i = 0; i < serviceRows.Count; i++)
            {
                string[] serviceAssetTypes = serviceRows[i].FindElements(By.XPath("./div[@data-bind='text: $data']")).Select(x => x.Text).ToArray();
                if (serviceAssetTypes.Length == 6)
                {
                    foreach (var assetType in assetTypes)
                    {
                        Assert.IsTrue(serviceAssetTypes.Any(x => x.Contains(assetType)));
                    }
                }
            }
            return this;
        }
    }
}
