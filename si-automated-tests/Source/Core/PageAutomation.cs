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
    public class PageAutomation : BasePage
    {
        public PageAutomation VerifyElementVisibility(string xpath, bool isVisible)
        {
            VerifyElementVisibility(GetElement(xpath), isVisible);
            return this;
        }

        public PageAutomation VerifyElementVisibility(By xpath, bool isVisible)
        {
            VerifyElementVisibility(GetElement(xpath), isVisible);
            return this;
        }

        public PageAutomation VerifyElementVisibility(IWebElement webElement, bool isVisible)
        {
            Assert.IsTrue(isVisible ? webElement.Displayed : !webElement.Displayed);
            return this;
        }

        public PageAutomation VerifyElementEnable(string xpath, bool isEnable)
        {
            VerifyElementEnable(GetElement(xpath), isEnable);
            return this;
        }

        public PageAutomation VerifyElementEnable(By xpath, bool isEnable)
        {
            VerifyElementEnable(GetElement(xpath), isEnable);
            return this;
        }

        public PageAutomation VerifyElementEnable(IWebElement webElement, bool isEnable)
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

        public PageAutomation VerifyCheckboxIsSelected(string xpath, bool isSelected)
        {
            Assert.IsTrue(isSelected ? GetCheckboxValue(xpath) : !GetCheckboxValue(xpath));
            return this;
        }

        public PageAutomation VerifyCheckboxIsSelected(By xpath, bool isSelected)
        {
            Assert.IsTrue(isSelected ? GetCheckboxValue(xpath) : !GetCheckboxValue(xpath));
            return this;
        }

        public PageAutomation VerifyCheckboxIsSelected(IWebElement webElement, bool isSelected)
        {
            Assert.IsTrue(isSelected ? GetCheckboxValue(webElement) : !GetCheckboxValue(webElement));
            return this;
        }

        public string GetInputValue(By xpath)
        {
            return GetElement(xpath).GetAttribute("value");
        }

        public string GetInputValue(string xpath)
        {
            return GetElement(xpath).GetAttribute("value");
        }

        public string GetInputValue(IWebElement webElement)
        {
            return webElement.GetAttribute("value");
        }

        public PageAutomation SetInputValue(By xpath, string value)
        {
            SendKeys(xpath, value);
            return this;
        }

        public PageAutomation SetInputValue(string xpath, string value)
        {
            SendKeys(xpath, value);
            return this;
        }

        public PageAutomation SetInputValue(IWebElement webElement, string value)
        {
            SendKeys(webElement, value);
            return this;
        }

        public PageAutomation VerifyInputValue(string xpath, string expectedValue)
        {
            Assert.IsTrue(GetInputValue(xpath) == expectedValue);
            return this;
        }

        public PageAutomation VerifyInputValue(By xpath, string expectedValue)
        {
            Assert.IsTrue(GetInputValue(xpath) == expectedValue);
            return this;
        }

        public PageAutomation VerifyInputValue(IWebElement webElement, string expectedValue)
        {
            Assert.IsTrue(GetInputValue(webElement) == expectedValue);
            return this;
        }

        public PageAutomation VerifyElementText(string xpath, string expectedValue, bool ignoreEmpty = false)
        {
            Assert.IsTrue((ignoreEmpty ? GetElementText(xpath).Trim() : GetElementText(xpath)) == expectedValue);
            return this;
        }

        public PageAutomation VerifyElementText(By xpath, string expectedValue, bool ignoreEmpty = false)
        {
            Assert.IsTrue((ignoreEmpty ? GetElementText(xpath).Trim() : GetElementText(xpath)) == expectedValue);
            return this;
        }

        public PageAutomation VerifyElementText(IWebElement webElement, string expectedValue, bool ignoreEmpty = false)
        {
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

        public PageAutomation VerifySelectContainDisplayValue(string selectXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ? 
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) != null : 
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public PageAutomation VerifySelectContainDisplayValue(By selectXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) != null :
                GetSelectDisplayValues(selectXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public PageAutomation VerifySelectContainDisplayValue(IWebElement webElement, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetSelectDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) != null :
                GetSelectDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public PageAutomation VerifySelectContainDisplayValues(string selectXpath, List<string> expectedValues, bool checkContain = true)
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

        public PageAutomation VerifySelectContainDisplayValues(By selectXpath, List<string> expectedValues, bool checkContain = true)
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

        public PageAutomation VerifySelectContainDisplayValues(IWebElement webElement, List<string> expectedValues, bool checkContain = true)
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

        public PageAutomation VerifyElementContainAttributeValue(string xpath, string attribute, string expectedValue, bool checkContain = true)
        {
            VerifyElementContainAttributeValue(GetElement(xpath), attribute, expectedValue, checkContain);
            return this;
        }

        public PageAutomation VerifyElementContainAttributeValue(By xpath, string attribute, string expectedValue, bool checkContain = true)
        {
            VerifyElementContainAttributeValue(GetElement(xpath), attribute, expectedValue, checkContain);
            return this;
        }

        public PageAutomation VerifyElementContainAttributeValue(IWebElement webElement, string attribute, string expectedValue, bool checkContain = true)
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

        public PageAutomation SelectByDisplayValueOnUlElement(string ulXpath, string selectValue)
        {
            return SelectByDisplayValueOnUlElement(GetElement(ulXpath), selectValue);
        }

        public PageAutomation SelectByDisplayValueOnUlElement(By ulXpath, string selectValue)
        {
            return SelectByDisplayValueOnUlElement(GetElement(ulXpath), selectValue);
        }

        public PageAutomation SelectByDisplayValueOnUlElement(IWebElement webElement, string selectValue)
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

        public PageAutomation VerifyUlContainDisplayValue(string ulXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) != null :
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public PageAutomation VerifyUlContainDisplayValue(By ulXpath, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) != null :
                GetUlDisplayValues(ulXpath).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public PageAutomation VerifyUlContainDisplayValue(IWebElement webElement, string expectedValue, bool checkContain = true)
        {
            Assert.IsTrue(checkContain ?
                GetUlDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) != null :
                GetUlDisplayValues(webElement).FirstOrDefault(x => x == expectedValue) == null);
            return this;
        }

        public PageAutomation VerifyUlContainDisplayValues(string ulXpath, List<string> expectedValues, bool checkContain = true)
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

        public PageAutomation VerifyUlContainDisplayValues(By ulXpath, List<string> expectedValues, bool checkContain = true)
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

        public PageAutomation VerifyUlContainDisplayValues(IWebElement webElement, List<string> expectedValues, bool checkContain = true)
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

        public PageAutomation VerifyCellValue(TableElement tableElement, int rowIdx, int cellIdx, object expectedValue, bool checkEqual = true)
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
        public PageAutomation VerifyRowValue(TableElement tableElement, int rowIdx, List<object> expectedValues, bool checkEqual = true)
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
