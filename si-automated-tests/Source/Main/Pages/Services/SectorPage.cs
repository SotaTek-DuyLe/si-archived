using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

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
        private readonly By title = By.XPath("//span[text()='Contract']");
        private readonly By retiredBtn = By.XPath("//button[@title='Retire' and not(contains(@style, 'display: none;'))]");

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Contract?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        //DYNAMIC
        private readonly string contract = "//h5[text()='{0}']";

        [AllureStep]
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

        [AllureStep]
        public SectorPage IsSectorPage(string contractName)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(contract, contractName);
            return this;
        }

        [AllureStep]
        public SectorPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach(string associateObject in CommonConstants.AssociateObject)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public SectorPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public SectorPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public SectorPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
