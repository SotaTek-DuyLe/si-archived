using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages
{
    public class CommonBrowsePage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly string customBtn = "//button[text()='{0}']";
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[contains(@class,'ui-widget-content slick-row even')]");
        private readonly By headers = By.XPath("//div[contains(@class,'ui-state-default slick-header-column')]/span[1]");
        private readonly By firstResultFields = By.XPath("//div[contains(@class,'ui-widget-content slick-row even')][1]/div");
        private readonly By secondResultFields = By.XPath("//div[contains(@class,'ui-widget-content slick-row odd')][1]/div");
        private readonly By availableRows = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]");
        private readonly String resultFields = "//div[contains(@class,'ui-widget-content slick-row')][{0}]/div";

        public CommonBrowsePage()
        {
        }
        public CommonBrowsePage FilterItem(int id)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForAllElementsVisible(addNewItemBtn);
            SendKeys(filterInputById, id.ToString());
            ClickOnElement(applyBtn);
            return this;
        }
        public CommonBrowsePage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<CommonBrowsePage>();
        }
        public CommonBrowsePage ClickAddNewItem()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }
        public CommonBrowsePage VerifyFirstResultValue(string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
                    Assert.AreEqual(expected, _firstResultFields[i].Text);
                }
            }
            return this;
        }
        public CommonBrowsePage VerifySecondResultValue(string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _secondResultFields = WaitUtil.WaitForAllElementsVisible(secondResultFields);
                    Assert.AreEqual(expected, _secondResultFields[i].Text);
                }
            }
            return this;
        }
        public CommonBrowsePage ClickButton(string _buttonName)
        {
            ClickOnElement(customBtn, _buttonName);
            return this;
        }
        public CommonBrowsePage ClickFirstItem()
        {
            ClickOnElement(firstResult);
            return this;
        }
        public string GetFirstResultValueOfField(string field)
        {
            string value = "";
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
                    value = _firstResultFields[i].Text;
                }
            }
            return value;
        }
        public List<string> GetListOfValueFilterBy(string filterValue)
        {
            List<string> result = new List<string>();
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            IList<IWebElement> rows = WaitUtil.WaitForAllElementsVisible(availableRows);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                {
                    for(int j = 0; j < rows.Count; j++)
                    {
                        IList<IWebElement> _resultFields = WaitUtil.WaitForAllElementsVisible(resultFields,(j+1).ToString());
                        result.Add(_resultFields[i].Text);
                        Console.WriteLine(result.ToString());
                    }
                }
            }
            return result;
        }

    }
}
