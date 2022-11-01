using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class CommonGridPage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By deleteItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By mainColumns = By.XPath("//div[contains(@class,'slick-header-column') and not(@unselectable='on')]");

        private readonly By headerCollumns = By.XPath("//span[@class='slick-column-name']");
        private readonly By filterInputBoxes = By.XPath("//input[@class='value form-control']");
        private readonly By firstResult = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]");
        private readonly By firstResultValues = By.XPath("//div[contains(@class,'ui-widget-content slick-row')][1]/div");

        public CommonGridPage IsOnResolutionCodeGrid()
        {
            WaitUtil.WaitForElementVisible(deleteItemBtn);
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            WaitUtil.WaitForAllElementsVisible(mainColumns);
            return this;
        }

        public CommonGridPage FilterItem(string filterName, string filterValue)
        {
            var headers = WaitUtil.WaitForAllElementsVisible(headerCollumns);
            var filterBoxes = WaitUtil.WaitForAllElementsVisible(filterInputBoxes);
            for (int i = 0; i < headers.Count; i++)
            {
                if (headers[i].Text.Equals(filterName, StringComparison.OrdinalIgnoreCase))
                {
                    SendKeys(filterBoxes[i - 1], filterValue);
                    break;
                }
            }
            return this;
        }
        public CommonGridPage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return this;
        }
        public CommonGridPage VerifyFistResultValue(string field, string expected)
        {
            var hds = WaitUtil.WaitForAllElementsVisible(headerCollumns);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    var _firstResultValues = WaitUtil.WaitForAllElementsVisible(firstResultValues);
                    Assert.AreEqual(expected, _firstResultValues[i].Text);
                }
            }
            return this;
        }
    }
}
