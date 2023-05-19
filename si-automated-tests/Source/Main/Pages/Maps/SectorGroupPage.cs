using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class SectorGroupPage : BasePageCommonActions
    {
        public readonly By AddNewItemButton = By.XPath("//button[text()='Add New Item']");
        public readonly By CopyItemButton = By.XPath("//button[text()='Copy Item']");
        private readonly By sectorIdInput = By.XPath("//div[contains(@class, 'l1')]//input");
        private readonly By applyBtn = By.XPath("//button[@title='Apply Filters']");
        private readonly By firstCheckboxAtRow = By.XPath("//div[@class='grid-canvas']//div[contains(@class, 'l0')]");
        private readonly By firstSectorRow = By.XPath("//div[@class='grid-canvas']/div[1]");
        private readonly By clearBtn = By.CssSelector("button[title='Clear Filters']");

        [AllureStep]
        public SectorGroupPage FilterSectorById(string sectorIdValue)
        {
            SendKeys(sectorIdInput, sectorIdValue);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            ClickOnElement(firstCheckboxAtRow);
            DoubleClickOnElement(firstSectorRow);
            return this;
        }

        [AllureStep]
        public SectorGroupPage ClickOnClearBtn()
        {
            ClickOnElement(clearBtn);
            return this;
        }
        
    }
}
