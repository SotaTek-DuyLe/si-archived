﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Core.WebElements
{
    public class TableElement
    {
        private string TableXpath;
        private string RowXpath;
        private List<string> CellXpaths;
        public Func<IEnumerable<IWebElement>, List<IWebElement>> GetDataView;

        public TableElement(string tableXPath, string rowXpath, List<string> cellXpaths)
        {
            TableXpath = tableXPath;
            RowXpath = rowXpath;
            CellXpaths = cellXpaths;
        }

        public int GetCellIndex(string xpath)
        {
            return CellXpaths.IndexOf(xpath);
        }

        public IWebElement GetTable()
        {
            return WaitUtil.WaitForElementVisible(TableXpath);
        }

        public List<IWebElement> GetRows()
        {
            var rows = GetTable().FindElements(By.XPath(RowXpath));
            return GetDataView?.Invoke(rows) ?? rows.ToList();
        }

        public List<IWebElement> GetCells(int rowIdx)
        {
            WaitForRowLoaded(rowIdx);
            var rows = GetRows();
            var row = rows[rowIdx];
            return CellXpaths.Select(x => row.FindElement(By.XPath(x))).ToList();
        }

        private void WaitForRowLoaded(int rowIdx)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until((driver) =>
            {
                var rows = GetTable().FindElements(By.XPath(RowXpath));
                return rows.Count > rowIdx;
            });
        }

        public IWebElement GetRow(int rowIdx)
        {
            WaitForRowLoaded(rowIdx);
            return GetRows()[rowIdx];
        }

        public IWebElement GetCell(int rowIdx, int cellIdx)
        {
            WaitForRowLoaded(rowIdx);
            var rows = GetRows();
            var row = rows[rowIdx];
            return row.FindElement(By.XPath(CellXpaths[cellIdx]));
        }

        public IWebElement GetCellByValue(int cellIdx, object value)
        {
            IWebElement webElement = null;
            int rowIdx = 0;
            var rowCount = GetRows().Count;
            while (rowIdx < rowCount)
            {
                if (GetRowValue(rowIdx, cellIdx)[0].AsString().Trim() == value.AsString().Trim())
                {
                    webElement = GetCell(rowIdx, cellIdx);
                    break;
                }
                rowIdx++;
            }
            return webElement;
        }

        public List<object> GetColumnValues(int columnIdx)
        {
            var rows = GetRows();
            List<object> values = new List<object>();
            for (int i = 0; i < rows.Count; i++)
            {
                values.Add(GetCellValue(i, columnIdx));
            }
            return values;
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

        public IWebElement GetRowByCellValues(Dictionary<int, object> filterCells)
        {
            IWebElement webElement = null;
            int rowIdx = 0;
            var rowCount = GetRows().Count;
            while (rowIdx < rowCount)
            {
                bool isMatch = false;
                foreach (var filterCell in filterCells)
                {
                    if (GetRowValue(rowIdx, filterCell.Key)[0].AsString().Trim() != filterCell.Value.AsString().Trim())
                    {
                        isMatch = false;
                        break;
                    }
                    else
                    {
                        isMatch = true;
                    }
                }
                if (isMatch)
                {
                    return GetRow(rowIdx);
                }
                rowIdx++;
            }
            return webElement;
        }

        public IWebElement GetCellByCellValues(int cellIdx, Dictionary<int, object> filterCells)
        {
            IWebElement row = GetRowByCellValues(filterCells);
            return row?.FindElement(By.XPath(CellXpaths[cellIdx]));
        }

        public object GetCellValue(int rowIdx, int cellIdx)
        {
            return GetRowValue(rowIdx, cellIdx)[0];
        }

        public object GetCellValue(IWebElement row, int cellIdx)
        {
            IWebElement webElement = row.FindElement(By.XPath(CellXpaths[cellIdx]));
            string elementType = webElement.TagName;
            switch (elementType)
            {
                case "select":
                    SelectElement selectedValue = new SelectElement(webElement);
                    return selectedValue.SelectedOption.Text;
                case "input":
                    string type = webElement.GetAttribute("type");
                    if (type == "checkbox")
                    {
                        return webElement.Selected;
                    }
                    else
                    {
                        return webElement.GetAttribute("value");
                    }
                default:
                    return null;
            }
        }

        public List<object> GetRowValue(int rowIdx, int cellIdx = -1)
        {
            WaitForRowLoaded(rowIdx);
            var rows = GetRows();
            var row = rows[rowIdx];
            List<object> values = new List<object>();
            foreach (var cellXpath in CellXpaths)
            {
                if (cellIdx != -1 && CellXpaths.IndexOf(cellXpath) != cellIdx) continue;
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

        public bool GetCellVisibility(IWebElement row, int cellIdx)
        {
            return row.FindElement(By.XPath(CellXpaths[cellIdx])).Displayed;
        }

        public bool GetCellEnable(int rowIdx, int cellIdx)
        {
            return GetCell(rowIdx, cellIdx).Enabled;
        }

        public string GetCellAttribute(int rowIdx, int cellIdx, string attribute)
        {
            return GetCell(rowIdx, cellIdx).GetAttribute(attribute);
        }

        public string GetCellAttribute(IWebElement row, int cellIdx, string attribute)
        {
            return row.FindElement(By.XPath(CellXpaths[cellIdx])).GetAttribute(attribute);
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

        public string GetCellCss(int rowIdx, int cellIdx, string attribute)
        {
            return GetCell(rowIdx, cellIdx).GetCssValue(attribute);
        }


        public void ClickCell(int rowIdx, int cellIdx)
        {
            WaitUtil.WaitForElementClickable(GetCell(rowIdx, cellIdx)).Click();
        }

        public void ClickCellOnCellValue(int clickCellidx, int filterCellIdx, object value)
        {
            IWebElement row = GetRowByCellValue(filterCellIdx, value);
            IWebElement cell = row.FindElement(By.XPath(CellXpaths[clickCellidx]));
            WaitUtil.WaitForElementClickable(cell).Click();
        }

        public void DoubleClickCellOnCellValue(int clickCellidx, int filterCellIdx, object value)
        {
            IWebElement row = GetRowByCellValue(filterCellIdx, value);
            IWebElement cell = row.FindElement(By.XPath(CellXpaths[clickCellidx]));
            Actions act = new Actions(IWebDriverManager.GetDriver());
            act.DoubleClick(cell).Perform();
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