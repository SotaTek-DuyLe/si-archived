using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Core.WebElements
{
    public class TableElement
    {
        private string TableXpath;
        private string RowXpath;
        private List<string> CellXpaths;
        public delegate List<IWebElement> GetDataViewHandler(List<IWebElement> originSource);
        public event GetDataViewHandler GetDataView;
        public TableElement(string tableXPath, string rowXpath, List<string> cellXpaths)
        {
            TableXpath = tableXPath;
            RowXpath = rowXpath;
            CellXpaths = cellXpaths;
        }

        public IWebElement GetTable()
        {
            return WaitUtil.WaitForElementVisible(TableXpath);
        }

        public List<IWebElement> GetRows()
        {
            int retryCount = 0;
            if (GetTable().FindElements(By.XPath(RowXpath)).Count == 0)
            {
                while (retryCount < 60)
                {
                    Thread.Sleep(500);
                    if (GetTable().FindElements(By.XPath(RowXpath)).Count != 0)
                    {
                        break;
                    }
                }
            }
            
            if (GetDataView != null)
            {
                return GetDataView?.Invoke(GetTable().FindElements(By.XPath(RowXpath)).ToList());
            }
            return GetTable().FindElements(By.XPath(RowXpath)).ToList();
        }

        public List<IWebElement> GetCells(int rowIdx)
        {
            var rows = GetRows();
            var row = rows[rowIdx];
            return CellXpaths.Select(x => row.FindElement(By.XPath(x))).ToList();
        }

        public IWebElement GetRow(int rowIdx)
        {
            return GetRows()[rowIdx];
        }

        public IWebElement GetCell(int rowIdx, int cellIdx)
        {
            return GetCells(rowIdx)[cellIdx];
        }

        public IWebElement GetCellByValue(int cellIdx, object value)
        {
            IWebElement webElement = null;
            int rowIdx = 0;
            var rowCount = GetRows().Count;
            while (rowIdx < rowCount)
            {
                if (GetRowValue(rowIdx)[cellIdx].AsString().Trim() == value.AsString().Trim())
                {
                    webElement = GetCell(rowIdx, cellIdx);
                    break;
                }
                rowIdx++;
            }
            return webElement;
        }

        public IWebElement GetRowByCellValue(int cellIdx, object value)
        {
            IWebElement webElement = null;
            int rowIdx = 0;
            var rowCount = GetRows().Count;
            while (rowIdx < rowCount)
            {
                if (GetRowValue(rowIdx)[cellIdx].AsString().Trim() == value.AsString().Trim())
                {
                    webElement = GetRow(rowIdx);
                    break;
                }
                rowIdx++;
            }
            return webElement;
        }

        public object GetCellValue(int rowIdx, int cellIdx)
        {
            return GetRowValue(rowIdx)[cellIdx];
        }

        public List<object> GetRowValue(int rowIdx)
        {
            var rows = GetRows();
            var row = rows[rowIdx];
            List<object> values = new List<object>();
            foreach (var cellXpath in CellXpaths)
            {
                IWebElement webElement = row.FindElement(By.XPath(cellXpath));
                string elementType = webElement.TagName;
                switch (elementType)
                {
                    case "select":
                        SelectElement selectedValue = new SelectElement(webElement);
                        values.Add(selectedValue.SelectedOption.Text);
                        break;
                    case "input":
                        string type = webElement.GetAttribute("type");
                        if (type == "checkbox")
                        {
                            values.Add(webElement.Selected);
                        }
                        else
                        {
                            values.Add(webElement.GetAttribute("value"));
                        }
                        break;
                    default:
                        values.Add(webElement.Text);
                        break;
                }
            }
            return values;
        }

        public List<object> GetRowValue(IWebElement row)
        {
            List<object> values = new List<object>();
            foreach (var cellXpath in CellXpaths)
            {
                IWebElement webElement = row.FindElement(By.XPath(cellXpath));
                string elementType = webElement.TagName;
                switch (elementType)
                {
                    case "select":
                        SelectElement selectedValue = new SelectElement(webElement);
                        values.Add(selectedValue.SelectedOption.Text);
                        break;
                    case "input":
                        string type = webElement.GetAttribute("type");
                        if (type == "checkbox")
                        {
                            values.Add(webElement.Selected);
                        }
                        else
                        {
                            values.Add(webElement.GetAttribute("value"));
                        }
                        break;
                    default:
                        values.Add(webElement.Text);
                        break;
                }
            }
            return values;
        }

        public bool GetCellVisibility(int rowIdx, int cellIdx)
        {
            return GetCell(rowIdx, cellIdx).Displayed;
        }

        public bool GetCellEnable(int rowIdx, int cellIdx)
        {
            return GetCell(rowIdx, cellIdx).Enabled;
        }

        public string GetCellAttribute(int rowIdx, int cellIdx, string attribute)
        {
            return GetCell(rowIdx, cellIdx).GetAttribute(attribute);
        }

        public string GetCellCss(int rowIdx, int cellIdx, string attribute)
        {
            return GetCell(rowIdx, cellIdx).GetCssValue(attribute);
        }


        public void SetCellValue(int rowIdx, int cellIdx, object value)
        {
            IWebElement webElement = GetCell(rowIdx, cellIdx);
            string elementType = webElement.TagName;
            switch (elementType)
            {
                case "select":
                    SelectElement selectedValue = new SelectElement(webElement);
                    selectedValue.SelectByText(value.AsString());
                    break;
                case "input":
                    string type = webElement.GetAttribute("type");
                    if (type == "checkbox")
                    {
                        if ((value.AsBool() != webElement.Selected))
                        {
                            ClickCell(rowIdx, cellIdx);
                        }
                    }
                    else
                    {
                        webElement.Clear();
                        webElement.SendKeys(value.AsString());
                    }
                    break;
                default:
                    break;
            }
        }

        public void ClickCell(int rowIdx, int cellIdx)
        {
            WaitUtil.WaitForElementClickable(GetCell(rowIdx, cellIdx)).Click();
        }

        public void DoubleClickCell(int rowIdx, int cellIdx)
        {
            Actions act = new Actions(IWebDriverManager.GetDriver());
            IWebElement element = GetCell(rowIdx, cellIdx);
            act.DoubleClick(element).Perform();
        }

        public void ClickRow(int rowIdx)
        {
            WaitUtil.WaitForElementClickable(GetRow(rowIdx)).Click();
        }

        public void DoubleClickRow(int rowIdx)
        {
            Actions act = new Actions(IWebDriverManager.GetDriver());
            IWebElement element = GetRow(rowIdx);
            act.DoubleClick(element).Perform();
        }
    }
}
