using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class DetailSectorGroupPage : BasePage
    {
        private readonly By statisticsTab = By.CssSelector("ul[role='tablist'] a[aria-controls='statistics-tab']");
        private readonly By serviceTree = By.CssSelector("input[id='services-ddl']");
        private readonly By loadInStaticsTabBtn = By.XPath("//div[@id='statistics-tab']//button[text()='Load']");
        private readonly By allCheckboxSector = By.XPath("//div[contains(@class, 'ui-tree-select') and not(contains(@style, 'display: none;'))]//ul[@class='jstree-children']/li[@role='treeitem']/a/i[1]");

        //DYNAMIC
        private readonly string anyServiceInTree = "//li/a[text()='{0}']";
        private readonly string sectorNameInLayerPanel = "//div[@id='layers-panel']//div[text()='{0}']";
        private readonly string sectorNameInSectorGroup = "//div[@id='selection-panel']//div[text()='{0}']";
        private readonly string sectorColumnInStatisticTab = "//span[text()='{0}']/parent::td/parent::tr";
        private readonly string parentColumnInStatisticTab = "//span[text()='{0}']/parent::td/following-sibling::td/span[text()='Richmond']";

        [AllureStep]
        public DetailSectorGroupPage IsDetailSectorGroupPage(string segtorValue)
        {
            WaitUtil.WaitForElementVisible(sectorNameInLayerPanel, segtorValue);
            WaitUtil.WaitForElementVisible(sectorNameInSectorGroup, segtorValue);
            return this;
        }

        [AllureStep]
        public DetailSectorGroupPage ClickOnStatisticTab()
        {
            ClickOnElement(statisticsTab);
            return this;
        }

        [AllureStep]
        public DetailSectorGroupPage SelectService(string[] serviceName)
        {
            string sectorValue = GetAttributeValue(serviceTree, "value");
            if(sectorValue.Equals("")) {
                ClickOnElement(serviceTree);
                foreach(string service in serviceName)
                {
                    ClickOnElement(anyServiceInTree, service);
                }
            }
            
            return this;
        }

        [AllureStep]
        public DetailSectorGroupPage UnselectAllSectorService()
        {
            ClickOnElement(serviceTree);
            List<IWebElement> list = GetAllElements(allCheckboxSector);
            foreach(IWebElement webElement in list)
            {
                if(IsCheckboxChecked(webElement))
                {
                    ClickOnElement(webElement);
                }
            }
            return this;

        }

        [AllureStep]
        public DetailSectorGroupPage ClickOnLoadInStatisticTabBtn()
        {
            ClickOnElement(loadInStaticsTabBtn);
            return this;
        }

        [AllureStep]
        public DetailSectorGroupPage VerifyDisplayDataAfterSelectSection()
        {
            foreach(string column in CommonConstants.SectorInStatisticTab)
            {
                Assert.IsTrue(IsControlDisplayed(sectorColumnInStatisticTab, column), column + "is not displayed");
                Assert.IsTrue(IsControlDisplayed(parentColumnInStatisticTab, column), column + "is not displayed");
            }
            return this;
        }
    }

}
