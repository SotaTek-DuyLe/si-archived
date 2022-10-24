using OpenQA.Selenium;
using si_automated_tests.Source.Core.WebElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class ServiceTreeViewElement : TreeViewElement
    {
        public ServiceTreeViewElement(string treeViewXpath, string treeViewItemXpath, string treeViewItemTextElementXpath, string hierarchicalXpath, string expandIconXpath = "") : base(treeViewXpath, treeViewItemXpath, treeViewItemTextElementXpath, hierarchicalXpath, expandIconXpath)
        {

        }

        public override void ExpandNode(string nodeName)
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
                            IWebElement expandElement = item.FindElement(By.XPath(ExpandIconXpath));
                            bool isExpand = expandElement.GetAttribute("src").Contains("minus.png");
                            if (isExpand) return;
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
                    HierarchicalTemplates = treeView.FindElements(By.XPath(HierarchicalXpath)).ToList();
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
                            IWebElement expandElement = item.FindElement(By.XPath(ExpandIconXpath));
                            bool isExpand = expandElement.GetAttribute("src").Contains("minus.png");
                            if (isExpand) return;
                            expandElement?.Click();
                            return;
                        }
                    }
                }
            }
        }
    }
}
