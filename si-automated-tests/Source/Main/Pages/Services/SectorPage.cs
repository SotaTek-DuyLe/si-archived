using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class SectorPage : BasePageCommonActions
    {
        public readonly By ButtonRefresh = By.XPath("//button[@title='Refresh']");
        public readonly By ButtonSave = By.XPath("//button[@title='Save']");
        public readonly By ButtonHistory = By.XPath("//button[@title='History']");
        public readonly By ButtonHelp = By.XPath("//button[@title='Help']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By MapTab = By.XPath("//a[@aria-controls='map-tab']");
        public readonly By InputSector = By.XPath("//input[@name='sector']");
        public readonly By SelectContract = By.XPath("//select[@id='contract.id']");
        public readonly By SelectParentSector = By.XPath("//select[@id='parentSector.id']");
        public readonly By SelectSectorType = By.XPath("//select[@id='sectorType.id']");
        public readonly By DivMap = By.XPath("//div[@id='map-tab']");
        public readonly By IconSector = By.XPath("//div[contains(@class, 'echo-header')]//img");
        public readonly By TitleSectorType = By.XPath("//div[@class='headers-container']//h4");
        public readonly By TitleSectorName = By.XPath("//div[@class='headers-container']//h5");
        public readonly By SectorId = By.XPath("//h4[@class='id']");

        public SectorPage VerifyBorderColorIsRed(By element)
        {
            string rgb = GetCssValue(element, "border-color");
            string[] colorsOnly = rgb.Replace("rgb", "").Replace("(", "").Replace(")", "").Split(',');
            int R = colorsOnly[0].AsInteger();
            int G = colorsOnly[1].AsInteger();
            int B = colorsOnly[2].AsInteger();
            Assert.IsTrue(R > 100 && R > G * 2 && R > B * 2);
            return this;
        }
    }
}
