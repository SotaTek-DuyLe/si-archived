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
        protected string TreeViewXPath;
        protected string TreeViewItemXpath;
        protected string TreeViewItemTextElementXpath;
        protected string HierarchicalXpath;
        protected string ExpandIconXpath;

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

        public void ReleaseNode()
        {
            SelectedNode = null;
        }

        public IWebElement SelectedNode { get; set; }

        public int MaxRetryCount = 10;

        public void ClickItem(string nodeName)
        {
            if (SelectedNode != null)
            {
                List<IWebElement> HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                int retryCount = 0;
                while (HierarchicalTemplates.Count == 0 && retryCount < MaxRetryCount)
                {
                    retryCount++;
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
                            bool isSelected = item.GetAttribute("aria-selected").AsBool();
                            if (isSelected) return;
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
                int retryCount = 0;
                while (HierarchicalTemplates.Count == 0 && retryCount < MaxRetryCount)
                {
                    retryCount++;
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
                            bool isSelected = item.GetAttribute("aria-selected").AsBool();
                            if (isSelected) return;
                            SelectedNode.Click();
                            return;
                        }
                    }
                }
            }
        }

        public virtual void ExpandNode(string nodeName)
        {
            if (SelectedNode != null)
            {
                List<IWebElement> HierarchicalTemplates = SelectedNode.FindElements(By.XPath(HierarchicalXpath)).ToList();
                int retryCount = 0;
                while (HierarchicalTemplates.Count == 0 && retryCount < MaxRetryCount)
                {
                    retryCount++;
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
                int retryCount = 0;
                while (HierarchicalTemplates.Count == 0 && retryCount < MaxRetryCount)
                {
                    retryCount++;
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

        public virtual void UnSelectAllNode()
        {
            IWebElement treeView = GetTreeView();
            List<IWebElement> HierarchicalTemplates = treeView.FindElements(By.XPath(HierarchicalXpath)).ToList();
            int retryCount = 0;
            while (HierarchicalTemplates.Count == 0 && retryCount < MaxRetryCount)
            {
                retryCount++;
                Thread.Sleep(100);
                HierarchicalTemplates = treeView.FindElements(By.XPath(HierarchicalXpath)).ToList();
            }
            foreach (var HierarchicalTemplate in HierarchicalTemplates)
            {
                List<IWebElement> treeViewItems = HierarchicalTemplate.FindElements(By.XPath(TreeViewItemXpath)).ToList();
                foreach (var item in treeViewItems)
                {
                    List<IWebElement> InnerHierarchicalTemplates = item.FindElements(By.XPath(HierarchicalXpath)).ToList();
                    int count = 0;
                    while (InnerHierarchicalTemplates.Count == 0 && count < 5)
                    {
                        count++;
                        Thread.Sleep(100);
                        InnerHierarchicalTemplates = item.FindElements(By.XPath(HierarchicalXpath)).ToList();
                    }
                    foreach (var InnerHierarchicalTemplate in InnerHierarchicalTemplates)
                    {
                        List<IWebElement> innertreeViewItems = InnerHierarchicalTemplate.FindElements(By.XPath(TreeViewItemXpath)).ToList();
                        foreach (var inneritem in innertreeViewItems)
                        {
                            IWebElement textElement = inneritem.FindElement(By.XPath(TreeViewItemTextElementXpath));
                            if (textElement != null && textElement.GetAttribute("class").Contains("jstree-clicked"))
                            {
                                inneritem.Click();
                            }
                        }
                    }
                }
            }
        }
    }
}
