using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class SectorGroupLayerTypePage : BasePageCommonActions
    {
        public readonly By SectorGroupTab = By.XPath("//a[@aria-controls='sector-group-tab']");
        public readonly By SectorTab = By.XPath("//a[@aria-controls='sector-tab']");
        public readonly By SectorGroupNameInput = By.XPath("//input[@id='sectorgroup-name']");
        public readonly By SectorGroupTypeSelect = By.XPath("//select[@id='sectorgroups-type']");
        public readonly By SectorInput = By.XPath("//input[@id='sectorgroups']");

        public readonly By AddSectorButton = By.XPath("//button[@title='Add New Sector' and @data-bind='click: addSector']");

        #region Add Sector Popup
        public readonly By CreateButton = By.XPath("//div[@id='sector-creation-dialog']//button[@id='create-sector']");
        public readonly By NewSectorNameInput = By.XPath("//div[@id='sector-creation-dialog']//input[@id='new-sector-name']");
        public readonly By NewSectorColorInput = By.XPath("//div[@id='sector-creation-dialog']//input[@id='new-sector-colour']");
        public readonly By NewParentSectorInput = By.XPath("//div[@id='sector-creation-dialog']//input[@id='parent-sector']");
        public readonly By NewSectorTypeSelect = By.XPath("//div[@id='sector-creation-dialog']//select[@id='new-sector-type']");
        #endregion

        #region Left panel
        private string AddedSectorName = "//li[contains(@class, 'map-object-container')]//span[@class='map-object-name' and text()='{0}']";
        private string AddSectorButtonInAddedSectorName = "//li[contains(@class, 'map-object-container')]//span[@class='map-object-name' and text()='{0}']//following-sibling::div[@class='add-child-sector-area']//button";
        private string TextDangerInAddedSectorName = "//li[contains(@class, 'map-object-container')]//span[@class='map-object-name' and text()='{0}']//following-sibling::div[@class='map-object-polygon text-danger']";
        private string TextDangerInAddedChildSectorName = "//li[contains(@class, 'map-object-container')]//span[@class='map-object-name' and text()='{0}']//following-sibling::div[@class='map-object-polygon']";
        private string DeleteSectorButtonInAddedSectorName = "((//li[contains(@class, 'map-object-container')]//span[@class='map-object-name' and text()='{0}']//parent::div)//following-sibling::div)[4]//button[@title='Delete Sector']";
        private string AsignPolygonButtonInAddedSectorName = "((//li[contains(@class, 'map-object-container')]//span[@class='map-object-name' and text()='{0}']//parent::div)//following-sibling::div)[4]//button[@title='Assign Polygon']";

        [AllureStep]
        public SectorGroupLayerTypePage ClickAddSectorInChildSector(string sectorName)
        {
            ClickOnElement(By.XPath(string.Format(AddSectorButtonInAddedSectorName, sectorName)));
            return this;
        }

        [AllureStep]
        public SectorGroupLayerTypePage ClickDeleteSectorInChildSector(string sectorName)
        {
            ClickOnElement(By.XPath(string.Format(DeleteSectorButtonInAddedSectorName, sectorName)));
            return this;
        }

        [AllureStep]
        public SectorGroupLayerTypePage VerifySectorInLeftPanelDisappear(string sectorName)
        {
            VerifyElementVisibility(By.XPath(string.Format(AddedSectorName, sectorName)), false);
            VerifyElementVisibility(By.XPath(string.Format(AddSectorButtonInAddedSectorName, sectorName)), false);
            VerifyElementVisibility(By.XPath(string.Format(TextDangerInAddedSectorName, sectorName)), false);
            VerifyElementVisibility(By.XPath(string.Format(DeleteSectorButtonInAddedSectorName, sectorName)), false);
            VerifyElementVisibility(By.XPath(string.Format(AsignPolygonButtonInAddedSectorName, sectorName)), false);
            return this;
        }

        [AllureStep]
        public SectorGroupLayerTypePage VerifyNewSectorInLeftPanel(string sectorName, string textDanger)
        {
            VerifyElementVisibility(By.XPath(string.Format(AddedSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(AddSectorButtonInAddedSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(TextDangerInAddedSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(DeleteSectorButtonInAddedSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(AsignPolygonButtonInAddedSectorName, sectorName)), true);
            VerifyElementText(By.XPath(string.Format(TextDangerInAddedSectorName, sectorName)), textDanger);
            return this;
        }

        [AllureStep]
        public SectorGroupLayerTypePage VerifyNewChildSectorInLeftPanel(string sectorName, string textDanger)
        {
            VerifyElementVisibility(By.XPath(string.Format(AddedSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(TextDangerInAddedChildSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(DeleteSectorButtonInAddedSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(AsignPolygonButtonInAddedSectorName, sectorName)), true);
            VerifyElementText(By.XPath(string.Format(TextDangerInAddedChildSectorName, sectorName)), textDanger);
            return this;
        }

        [AllureStep]
        public SectorGroupLayerTypePage VerifyNewParentSectorInLeftPanel(string sectorName, string textDanger)
        {
            VerifyElementVisibility(By.XPath(string.Format(AddedSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(TextDangerInAddedChildSectorName, sectorName)), true);
            VerifyElementVisibility(By.XPath(string.Format(DeleteSectorButtonInAddedSectorName, sectorName)), false);
            VerifyElementVisibility(By.XPath(string.Format(AsignPolygonButtonInAddedSectorName, sectorName)), false);
            VerifyElementText(By.XPath(string.Format(TextDangerInAddedChildSectorName, sectorName)), textDanger);
            return this;
        }

        [AllureStep]
        public SectorGroupLayerTypePage VerifySectorTextDangerInLeftPanel(string sectorName, string textDanger)
        {
            VerifyElementText(By.XPath(string.Format(TextDangerInAddedSectorName, sectorName)), textDanger);
            return this;
        }

        [AllureStep]
        public SectorGroupLayerTypePage ClickOnSector(string sectorName)
        {
            ClickOnElement(By.XPath(string.Format(AddedSectorName, sectorName)));
            return this;
        }
        #endregion

        #region Sector tab
        public readonly By SectorNameInSectorTab = By.XPath("//div[@id='sector-tab']//input[@id='sector-name']");
        public readonly By SectorColourInSectorTab = By.XPath("//div[@id='sector-tab']//input[@id='sector-colour']");
        public readonly By SectorTypeInSectorTab = By.XPath("//div[@id='sector-tab']//select[@id='sector-type']");
        public readonly By ParentSectorInSectorTab = By.XPath("//div[@id='sector-tab']//div[@id='parent-sector']");
        public readonly By ParentSectorTypeInSectorTab = By.XPath("//div[@id='sector-tab']//div[@id='parent-sector-type']");
        public readonly By UpdateSectorGroupButton = By.XPath("//button[contains(@data-bind, 'updateSectorGroup')]");

        [AllureStep]
        public SectorGroupLayerTypePage VerifySectorTab(string sectorName, string colour, string sectorType, string parentSector)
        {
            VerifyInputValue(SectorNameInSectorTab, sectorName);
            VerifyInputValue(SectorColourInSectorTab, colour);
            VerifySelectedValue(SectorTypeInSectorTab, sectorType);
            VerifyElementText(ParentSectorInSectorTab, parentSector);
            VerifyElementText(ParentSectorTypeInSectorTab, "Contract Area");
            return this;
        }
        #endregion
    }
}
