using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Core.WebElements
{
    public class TreeViewElement
    {
        private string TreeViewXPath;
        private string TreeViewItemXpath;
        private string TreeViewItemTextElementXpath;
        private string HierarchicalXpath;
        private string ExpandIconXpath;

        public TreeViewElement(string treeViewXpath, string treeViewItemXpath, string treeViewItemTextElementXpath, string hierarchicalXpath, string expandIconXpath = "")
        {
            TreeViewXPath = treeViewXpath;
            TreeViewItemXpath = treeViewItemXpath;
            TreeViewItemTextElementXpath = treeViewItemTextElementXpath;
            HierarchicalXpath = hierarchicalXpath;
            ExpandIconXpath = expandIconXpath;
        }

        public IWebElement GetTreeView()
        {
            return WaitUtil.WaitForElementVisible(TreeViewXPath);
        }

        public IWebElement SelectedNode { get; set; }

        public void ClickItem(string nodeName)
        {
            if (SelectedNode != null)
            {
                List<IWebElement> HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                while (HierarchicalTemplates.Count == 0)
                {
                    Thread.Sleep(100);
                    HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                }
                foreach (var HierarchicalTemplate in HierarchicalTemplates)
                {
                    List<IWebElement> treeViewItems = HierarchicalTemplate.FindElements(By.XPath(TreeViewItemXpath)).ToList();
                    foreach (var item in treeViewItems)
                    {
                        IWebElement textElement = item.FindElement(By.XPath(TreeViewItemTextElementXpath));
                        if (textElement != null && textElement.Text.Trim() == nodeName)
                        {
                            SelectedNode = item;
                            SelectedNode.Click();
                            return;
                        }
                    }
                }
            }
            else
            {
                IWebElement treeView = GetTreeView();
                List<IWebElement> HierarchicalTemplates = treeView.FindElements(By.XPath(HierarchicalXpath)).ToList();
                while (HierarchicalTemplates.Count == 0)
                {
                    Thread.Sleep(100);
                    HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                }
                foreach (var HierarchicalTemplate in HierarchicalTemplates)
                {
                    List<IWebElement> treeViewItems = HierarchicalTemplate.FindElements(By.XPath(TreeViewItemXpath)).ToList();
                    foreach (var item in treeViewItems)
                    {
                        IWebElement textElement = item.FindElement(By.XPath(TreeViewItemTextElementXpath));
                        if (textElement != null && textElement.Text.Trim() == nodeName)
                        {
                            SelectedNode = item;
                            SelectedNode.Click();
                            return;
                        }
                    }
                }
            }
        }

        public void ExpandNode(string nodeName)
        {
            if (SelectedNode != null)
            {
                List<IWebElement> HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                while (HierarchicalTemplates.Count == 0)
                {
                    Thread.Sleep(100);
                    HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                }
                foreach (var HierarchicalTemplate in HierarchicalTemplates)
                {
                    List<IWebElement> treeViewItems = HierarchicalTemplate.FindElements(By.XPath(TreeViewItemXpath)).ToList();
                    foreach (var item in treeViewItems)
                    {
                        IWebElement textElement = item.FindElement(By.XPath(TreeViewItemTextElementXpath));
                        if (textElement != null && textElement.Text.Trim() == nodeName)
                        {
                            SelectedNode = item;
                            bool isExpand = item.GetAttribute("aria-expanded").AsBool();
                            if (isExpand) return;
                            IWebElement expandElement = item.FindElement(By.XPath(ExpandIconXpath));
                            expandElement?.Click();
                            return;
                        }
                    }
                }
            }
            else
            {
                IWebElement treeView = GetTreeView();
                List<IWebElement> HierarchicalTemplates = treeView.FindElements(By.XPath(HierarchicalXpath)).ToList();
                while (HierarchicalTemplates.Count == 0)
                {
                    Thread.Sleep(100);
                    HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                }
                foreach (var HierarchicalTemplate in HierarchicalTemplates)
                {
                    List<IWebElement> treeViewItems = HierarchicalTemplate.FindElements(By.XPath(TreeViewItemXpath)).ToList();
                    foreach (var item in treeViewItems)
                    {
                        IWebElement textElement = item.FindElement(By.XPath(TreeViewItemTextElementXpath));
                        if (textElement != null && textElement.Text.Trim() == nodeName)
                        {
                            SelectedNode = item;
                            bool isExpand = item.GetAttribute("aria-expanded").AsBool();
                            if (isExpand) return;
                            IWebElement expandElement = item.FindElement(By.XPath(ExpandIconXpath));
                            expandElement?.Click();
                            return;
                        }
                    }
                }
            }
        }
    }
}
