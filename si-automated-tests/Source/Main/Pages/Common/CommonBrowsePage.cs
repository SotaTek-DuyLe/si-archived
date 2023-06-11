using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace si_automated_tests.Source.Main.Pages
{
    public class CommonBrowsePage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By addNewItemLoading = By.XPath("//button[text()='Add New Item' and contains(@class, 'echo-disabled')]");
        private readonly string customBtn = "//button[text()='{0}']";
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By inputs = By.XPath("//div[contains(@class,'ui-state-default slick-headerrow-column')]/descendant::input");
        private readonly string filterInput = "//div[contains(@class,'ui-state-default slick-headerrow-column')][{0}]/input";
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[contains(@class,'ui-widget-content slick-row even')]");
        private readonly By results = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]");
        private readonly By headers = By.XPath("//div[contains(@class,'ui-state-default slick-header-column')]/span[1]");
        private readonly By headersInTabSection = By.XPath("//div[contains(@class,'tab-pane') and contains(@class,'active')]//div[contains(@class,'ui-state-default slick-header-column')]/span[1]");
        private readonly string fieldColumns = "//div[contains(@class,'ui-widget-content slick-row') and not(contains(@class,'slick-group'))]/div[{0}]"; //with {0} being number of column
        private readonly By checkboxes = By.XPath("//div[contains(@class,'ui-widget-content slick-row') and not(contains(@class,'slick-group'))]/div[1]/input"); //with {0} being number of column
        private readonly By firstResultFields = By.XPath("//div[contains(@class,'ui-widget-content slick-row even') and not(contains(@class,'slick-group'))][1]/div");
        private readonly By secondResultFields = By.XPath("//div[contains(@class,'ui-widget-content slick-row odd') and not(contains(@class,'slick-group'))][1]/div");
        private readonly By activeResultFields = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]/div[contains(@class,'selected')]"); //fields from row that is selected
        private readonly By checkboxFromActiveField = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]/div[contains(@class,'selected')]/input"); //fields from row that is selected
        private readonly By firstResultFieldsInTabSection = By.XPath("//div[contains(@class,'tab-pane') and contains(@class,'active')]//div[contains(@class,'ui-widget-content slick-row even')][1]/div");
        private readonly By availableRows = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]");
        private readonly String resultFields = "//div[contains(@class,'ui-widget-content slick-row')][{0}]/div";
        private readonly By allRowInTabel = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        public CommonBrowsePage()
        {
        }
        [AllureStep]
        public CommonBrowsePage FilterItem(int id, bool clickApply = true)
        {
            WaitForLoadingIconToDisappear();
            //WaitUtil.WaitForAllElementsVisible(addNewItemBtn);
            SendKeys(filterInputById, id.ToString());
            if(clickApply) ClickOnElement(applyBtn);
            return this;
        }

        [AllureStep]
        public CommonBrowsePage FilterItemByField(string field, string value, bool clickApply = true)
        {
            WaitForLoadingIconToDisappear();
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(headers);
            for (int i = 0; i < hds.Count; i++) //i=3
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(i);
                    var tmp = WaitUtil.WaitForElementVisible(By.XPath(String.Format(filterInput, i+1)));
                    SendKeys(tmp, value);
                    break;
                }
            }
            if (clickApply) ClickOnElement(applyBtn);
            return this;
        }


        [AllureStep]
        public CommonBrowsePage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return this;
        }
        [AllureStep]
        public CommonBrowsePage OpenResultNumber(int resultNumber)
        {
            DoubleClickOnElement(GetAllElementsNotWait(results)[resultNumber-1]);
            return this;
        }
        [AllureStep]
        public CommonBrowsePage ClickAddNewItem()
        {
            WaitUtil.WaitForElementInvisible(addNewItemLoading);
            ClickOnElement(addNewItemBtn);
            return this;
        }
        [AllureStep]
        public CommonBrowsePage SelectItemWithField(string field, string valueOfField)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(headers);
            for (int i = 0; i < hds.Count; i++) //i=3
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    var xpath = String.Format(fieldColumns, (i + 1).ToString());
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsPresent(By.XPath(xpath));
                    var count = _firstResultFields.Count;
                    for(int j = 0; j < count; j++)
                    {
                        var text2 = _firstResultFields[j].Text;
                        if (text2.Equals(valueOfField, StringComparison.OrdinalIgnoreCase))
                        {
                            ClickOnElement(_firstResultFields[j]);
                            break;
                        }
                    }
                }
            }
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public CommonBrowsePage VerifyDateValueInActiveRow(int numberOfResultToBeVerify, string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(headers);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(activeResultFields);
                    var countPerLine = _firstResultFields.Count / numberOfResultToBeVerify;
                    for(int j = 0; j < numberOfResultToBeVerify; j++)
                    {
                        DateTime expectedDate = DateTime.ParseExact(expected, "dd/MM/yyyy HH:mm", null);
                        Console.WriteLine(i+j * countPerLine);
                        DateTime actualDate = DateTime.ParseExact(_firstResultFields[i + j * countPerLine].Text, "dd/MM/yyyy HH:mm", null);
                        Assert.AreEqual(expectedDate.Year, actualDate.Year);
                        Assert.AreEqual(expectedDate.Month, actualDate.Month);
                        Assert.AreEqual(expectedDate.Day, actualDate.Day);
                        Assert.AreEqual(expectedDate.Hour, actualDate.Hour);
                        Assert.IsTrue(expectedDate.Minute - actualDate.Minute == 0 | actualDate.Minute - expectedDate.Minute <= 2, "Expected " + expectedDate.Minute + " but found " + actualDate.Minute);
                    }
                }
            }
            return this;
        }
        [AllureStep]
        public CommonBrowsePage VerifyFirstResultValueInTab(string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headersInTabSection);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFieldsInTabSection);
                    Assert.AreEqual(expected, _firstResultFields[i].Text);
                }
            }
            return this;
        }
        [AllureStep]
        public CommonBrowsePage VerifySecondResultValue(string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(headers);
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

        [AllureStep]
        public CommonBrowsePage FilterItemWithoutClickApply(int id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id.ToString());
            WaitForLoadingIconToDisappear();
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public CommonBrowsePage ClickButton(string _buttonName)
        {
            ClickOnElement(customBtn, _buttonName);
            return this;
        }
        [AllureStep]
        public CommonBrowsePage ClickFirstItem()
        {
            ClickOnElement(firstResult);
            return this;
        }
        [AllureStep]
        public string GetFirstResultValueOfField(string field)
        {
            string value = "";
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(headers);
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
        public string GetSecondResultValueOfField(string field)
        {
            string value = "";
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(headers);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _secondResultFields = WaitUtil.WaitForAllElementsVisible(secondResultFields);
                    value = _secondResultFields[i].Text;
                }
            }
            return value;
        }
        [AllureStep]
        public string GetFirstResultValueOfFieldInTab(string field)
        {
            string value = "";
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headersInTabSection);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFieldsInTabSection);
                    value = _firstResultFields[i].Text;
                }
            }
            return value;
        }
        [AllureStep]
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
                    }
                }
            }
            return result;
        }
        [AllureStep]
        public CommonBrowsePage FilterItemBy(string field, string value) {

            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            IList<IWebElement> filterInputs = WaitUtil.WaitForAllElementsVisible(inputs);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    SendKeys(filterInputs[i - 1], value);
                    break;
                }
            }
            ClickOnElement(applyBtn);

            return this;
        }
        //For task grid only
        [AllureStep]
        public CommonBrowsePage OpenFirstServiceTaskLink()
        {
            var firstServiceTaskLink = By.XPath("//a[text()='ServiceTask']");
            ClickOnElement(firstServiceTaskLink);
            return this;
        }
        [AllureStep]
        public CommonBrowsePage SelectFirstNumberOfItem(int numberOfItem)
        {
            IList<IWebElement> eList = WaitUtil.WaitForAllElementsPresent(checkboxes);
            for(int i = 0; i< numberOfItem; i++)
            {
                ClickOnElement(eList[i]);
            }
            return this;
        }

        [AllureStep]
        public CommonBrowsePage IsListPageLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(allRowInTabel);
            return this;
        }

        [AllureStep]
        public CommonBrowsePage VerifyDisplayVerticalScrollBarInListingPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }


        [AllureStep]
        public CommonBrowsePage DeselectActiveItem()
        {
            ClickOnElement(checkboxFromActiveField);
            return this;
        }
    }
}
