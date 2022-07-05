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
        private string HierarchicalXpath;

        public TreeViewElement(string treeViewXpath, string treeViewItemXpath, string hierarchicalXpath)
        {
            TreeViewXPath = treeViewXpath;
            TreeViewItemXpath = treeViewItemXpath;
            HierarchicalXpath = hierarchicalXpath;
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
                List<IWebElement> treeViewItems = HierarchicalTemplates.FirstOrDefault().FindElements(By.XPath(TreeViewItemXpath)).ToList();
                foreach (var item in treeViewItems)
                {
                    if (item.Text == nodeName)
                    {
                        SelectedNode = item;
                        SelectedNode.Click();
                        return;
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
                        if (item.Text == nodeName)
                        {
                            SelectedNode = item;
                            SelectedNode.Click();
                            return;
                        }
                    }
                }
            }
        }
    }
}
