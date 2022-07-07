using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core.WebElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Core
{
    public class BasePageCommonActions : BasePage
    {
        public BasePageCommonActions VerifyElementVisibility(string xpath, bool isVisible)
        {
            VerifyElementVisibility(GetElement(xpath), isVisible);
            return this;
        }

        public BasePageCommonActions VerifyElementVisibility(By xpath, bool isVisible)
        {
            VerifyElementVisibility(GetElement(xpath), isVisible);
            return this;
        }

        public BasePageCommonActions VerifyElementVisibility(IWebElement webElement, bool isVisible)
        {
            Assert.IsTrue(isVisible ? webElement.Displayed : !webElement.Displayed);
            return this;
        }

        public BasePageCommonActions VerifyElementEnable(string xpath, bool isEnable)
        {
            VerifyElementEnable(GetElement(xpath), isEnable);
            return this;
        }

        public BasePageCommonActions VerifyElementEnable(By xpath, bool isEnable)
        {
            VerifyElementEnable(GetElement(xpath), isEnable);
            return this;
        }

        public BasePageCommonActions VerifyElementEnable(IWebElement webElement, bool isEnable)
        {
            Assert.IsTrue(isEnable ? webElement.Enabled : !webElement.Enabled);
            return this;
        }

        public bool GetCheckboxValue(By xpath)
        {
            return GetElement(xpath).Selected;
        }

        public bool GetCheckboxValue(string xpath)
        {
            return GetElement(xpath).Selected;
        }

        public bool GetCheckboxValue(IWebElement webElement)
        {
            return webElement.Selected;
        }

        public BasePageCommonActions VerifyCheckboxIsSelected(string xpath, bool isSelected)
        {
            Assert.IsTrue(isSelected ? GetCheckboxValue(xpath) : !GetCheckboxValue(xpath));
            return this;
        }

        public BasePageCommonActions VerifyCheckboxIsSelected(By xpath, bool isSelected)
        {
            Assert.IsTrue(isSelected ? GetCheckboxValue(xpath) : !GetCheckboxValue(xpath));
            return this;
        }

        public BasePageCommonActions VerifyCheckboxIsSelected(IWebElement webElement, bool isSelected)
        {
            Assert.IsTrue(isSelected ? GetCheckboxValue(webElement) : !GetCheckboxValue(webElement));
            return this;
        }

        public string GetInputValue(By xpath, string text)
        {
            WaitUtil.TextToBePresentInElementValue(GetElement(xpath), text);
            return GetElement(xpath).GetAttribute("value");
        }

        public string GetInputValue(string xpath, string text)
        {
            WaitUtil.TextToBePresentInElementValue(GetElement(xpath), text);
            return GetElement(xpath).GetAttribute("value");
        }

        public string GetInputValue(IWebElement webElement, string text)
        {
            WaitUtil.TextToBePresentInElementValue(webElement, text);
            return webElement.GetAttribute("value");
        }

        public BasePageCommonActions SetInputValue(By xpath, string value)
        {
            SendKeys(xpath, value);
            return this;
        }

        public BasePageCommonActions SetInputValue(string xpath, string value)
        {
            SendKeys(xpath, value);
            return this;
        }

        public BasePageCommonActions SetInputValue(IWebElement webElement, string value)
        {
            SendKeys(webElement, value);
            return this;
        }

        public BasePageCommonActions VerifyInputValue(string xpath, string expectedValue)
        {
            Assert.IsTrue(GetInputValue(xpath, expectedValue) == expectedValue);
            return this;
        }

        public BasePageCommonActions VerifyInputValue(By xpath, string expectedValue)
        {
            Assert.IsTrue(GetInputValue(xpath, expectedValue) == expectedValue);
            return this;
        }

        public BasePageCommonActions VerifyInputValue(IWebElement webElement, string expectedValue)
        {
            Assert.IsTrue(GetInputValue(webElement, expectedValue) == expectedValue);
            return this;
        }

        public BasePageCommonActions VerifyElementText(string xpath, string expectedValue, bool ignoreEmpty = false, bool toLowerCase = false)
        {
            if (toLowerCase)
            {
                Assert.IsTrue((ignoreEmpty ? GetElementText(xpath).Trim() : GetElementText(xpath)).ToLower() == expectedValue.ToLower());
                return this;
            }
            Assert.IsTrue((ignoreEmpty ? GetElementText(xpath).Trim() : GetElementText(xpath)) == expectedValue);
            return this;
        }

        public BasePageCommonActions VerifyElementText(By xpath, string expectedValue, bool ignoreEmpty = false, bool toLowerCase = false)
        {
            if (toLowerCase)
            {
                Assert.IsTrue((ignoreEmpty ? GetElementText(xpath).Trim() : GetElementText(xpath)).ToLower() == expectedValue.ToLower());
                return this;
            }
            Assert.IsTrue((ignoreEmpty ? GetElementText(xpath).Trim() : GetElementText(xpath)) == expectedValue);
            return this;
        }

        public BasePageCommonActions VerifyElementText(IWebElement webElement, string expectedValue, bool ignoreEmpty = false, bool toLowerCase = false)
        {
            if (toLowerCase)
            {
                Assert.IsTrue((ignoreEmpty ? GetElementText(webElement).Trim() : GetElementText(webElement)).ToLower() == expectedValue.ToLower());
                return this;
            }
            Assert.IsTrue((ignoreEmpty ? GetElementText(webElement).Trim() : GetElementText(webElement)) == expectedValue);
            return this;
        }

        public List<string> GetSelectDisplayValues(string selectXpath)
        {
            return GetSelectDisplayValues(GetElement(selectXpath));
        }

        public List<string> GetSelectDisplayValues(By selectXpath)
        {
            return GetSelectDisplayValues(GetElement(selectXpath));
        }

        public List<string> GetSelectDisplayValues(IWebElement webElement)
        {
            SelectElement select = new SelectElement(webElement);
            return select.Options.Select(x => x.Text).ToList();
        }

        public BasePageCommonActions VerifySelectContainDisplayValue(string selectXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ? 
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) != null : 
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public BasePageCommonActions VerifySelectedValue(string selectXpath, string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(selectXpath) == expectedValue);
            return this;
        }

        public BasePageCommonActions VerifySelectedValue(By selectXpath, string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(selectXpath) == expectedValue);
            return this;
        }

        public BasePageCommonActions VerifySelectContainDisplayValue(By selectXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) != null :
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public BasePageCommonActions VerifySelectContainDisplayValue(IWebElement webElement, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetSelectDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) != null :
                GetSelectDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public BasePageCommonActions VerifySelectContainDisplayValues(string selectXpath, List<string> expectedValues, bool checkContain = true)
        {
            if (checkContain)
            {
                Assert.AreEqual(expectedValues, GetSelectDisplayValues(selectXpath));
            }
            else
            {
                Assert.AreNotEqual(expectedValues, GetSelectDisplayValues(selectXpath));
            }
            return this;
        }

        public BasePageCommonActions VerifySelectContainDisplayValues(By selectXpath, List<string> expectedValues, bool checkContain = true)
        {
            if (checkContain)
            {
                Assert.AreEqual(expectedValues, GetSelectDisplayValues(selectXpath));
            }
            else
            {
                Assert.AreNotEqual(expectedValues, GetSelectDisplayValues(selectXpath));
            }
            return this;
        }

        public BasePageCommonActions VerifySelectContainDisplayValues(IWebElement webElement, List<string> expectedValues, bool checkContain = true)
        {
            if (checkContain)
            {
                Assert.AreEqual(expectedValues, GetSelectDisplayValues(webElement));
            }
            else
            {
                Assert.AreNotEqual(expectedValues, GetSelectDisplayValues(webElement));
            }
            return this;
        }

        public BasePageCommonActions VerifyElementContainAttributeValue(string xpath, string attribute, string expectedValue, bool checkContain = true)
        {
            VerifyElementContainAttributeValue(GetElement(xpath), attribute, expectedValue, checkContain);
            return this;
        }

        public BasePageCommonActions VerifyElementContainAttributeValue(By xpath, string attribute, string expectedValue, bool checkContain = true)
        {
            VerifyElementContainAttributeValue(GetElement(xpath), attribute, expectedValue, checkContain);
            return this;
        }

        public BasePageCommonActions VerifyElementContainAttributeValue(IWebElement webElement, string attribute, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ? webElement.GetAttribute(attribute).Contains(expectedValue) : !webElement.GetAttribute(attribute).Contains(expectedValue));
            return this;
        }

        public List<string> GetUlDisplayValues(string ulXpath)
        {
            return GetUlDisplayValues(GetElement(ulXpath));
        }

        public List<string> GetUlDisplayValues(By ulXpath)
        {
            return GetUlDisplayValues(GetElement(ulXpath));
        }

        public List<string> GetUlDisplayValues(IWebElement webElement)
        {
            return webElement.FindElements(By.XPath("./li")).Select(x => x.Text).ToList();
        }

        public BasePageCommonActions SelectByDisplayValueOnUlElement(string ulXpath, string selectValue)
        {
            return SelectByDisplayValueOnUlElement(GetElement(ulXpath), selectValue);
        }

        public BasePageCommonActions SelectByDisplayValueOnUlElement(By ulXpath, string selectValue)
        {
            return SelectByDisplayValueOnUlElement(GetElement(ulXpath), selectValue);
        }

        public BasePageCommonActions SelectByDisplayValueOnUlElement(IWebElement webElement, string selectValue)
        {
            List<IWebElement> options = webElement.FindElements(By.XPath("./li")).ToList();
            foreach (var item in options)
            {
                if (item.Text == selectValue)
                {
                    ClickOnElement(item);
                    break;
                }
            }
            return this;
        }

        public BasePageCommonActions VerifyUlContainDisplayValue(string ulXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) != null :
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public BasePageCommonActions VerifyUlContainDisplayValue(By ulXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) != null :
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public BasePageCommonActions VerifyUlContainDisplayValue(IWebElement webElement, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetUlDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) != null :
                GetUlDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public BasePageCommonActions VerifyUlContainDisplayValues(string ulXpath, List<string> expectedValues, bool checkContain = true)
        {
            if (checkContain)
            {
                Assert.AreEqual(expectedValues, GetUlDisplayValues(ulXpath));
            }
            else
            {
                Assert.AreNotEqual(expectedValues, GetUlDisplayValues(ulXpath));
            }
            return this;
        }

        public BasePageCommonActions VerifyUlContainDisplayValues(By ulXpath, List<string> expectedValues, bool checkContain = true)
        {
            if (checkContain)
            {
                Assert.AreEqual(expectedValues, GetUlDisplayValues(ulXpath));
            }
            else
            {
                Assert.AreNotEqual(expectedValues, GetUlDisplayValues(ulXpath));
            }
            return this;
        }

        public BasePageCommonActions VerifyUlContainDisplayValues(IWebElement webElement, List<string> expectedValues, bool checkContain = true)
        {
            if (checkContain)
            {
                Assert.AreEqual(expectedValues, GetUlDisplayValues(webElement));
            }
            else
            {
                Assert.AreNotEqual(expectedValues, GetUlDisplayValues(webElement));
            }
            return this;
        }

        public BasePageCommonActions VerifyCellValue(TableElement tableElement, int rowIdx, int cellIdx, object expectedValue, bool checkEqual = true)
        {
            if (checkEqual)
            {
                Assert.AreEqual(expectedValue, tableElement.GetCellValue(rowIdx, cellIdx));
            }
            else
            {
                Assert.AreNotEqual(expectedValue, tableElement.GetCellValue(rowIdx, cellIdx));
            }
            return this;
        }

        /// <summary>
        /// VerifyRowValue
        /// </summary>
        /// <param name="tableElement"></param>
        /// <param name="rowIdx"></param>
        /// <param name="expectedValues">ordered list</param>
        /// <returns></returns>
        public BasePageCommonActions VerifyRowValue(TableElement tableElement, int rowIdx, List<object> expectedValues, bool checkEqual = true)
        {
            if (checkEqual)
            {
                Assert.AreEqual(expectedValues, tableElement.GetRowValue(rowIdx));
            }
            else
            {
                Assert.AreNotEqual(expectedValues, tableElement.GetRowValue(rowIdx));
            }
            return this;
        }
    }
}
