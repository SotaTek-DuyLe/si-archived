using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Pages.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceDetailPage : BasePageCommonActions
    {
        public RoundInstanceDetailPage()
        {
            unallocatedTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox, UnallocatedState, UnallocatedID });
            unallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };

            slickRoundTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", SlickRoundRow, new List<string>() { RoundDescriptionCell });
            slickRoundTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }
        public readonly By WorkSheetTab = By.XPath("//a[@aria-controls='worksheet-tab']");
        public readonly By ExpandRoundsGo = By.XPath("//button[@id='t-toggle-rounds']");
        private readonly By expandRoundLegsBtn = By.XPath("//span[text()='Expand Round Legs']/parent::button");
        public readonly By IdFilterInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]//input");
        public readonly string UnallocatedRow = "./div[contains(@class, 'assured')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string UnallocatedState = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string UnallocatedID = "./div[contains(@class, 'slick-cell l3 r3')]";
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By noteAtFirstRow = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l20')]");
        private readonly By firstRowAfterFiltering = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l4')]/parent::div");
        private readonly By selectAndDeselectBtn = By.CssSelector("div[title='Select/Deselect All']");

        public readonly string SlickRoundRow = "./div[contains(@class, 'slick-group')]";
        public readonly string RoundDescriptionCell = "./div[contains(@class, 'slick-cell l0')]";
        private readonly By title = By.XPath("//h4[text()='Round Instance']");
        private readonly By rescheduleDateTitle = By.XPath("//label[text()='Reschedule Date']");


        private TableElement slickRoundTableEle;
        public TableElement SlickRoundTableEle
        {
            get => slickRoundTableEle;
        }

        private TableElement unallocatedTableEle;
        public TableElement UnallocatedTableEle
        {
            get => unallocatedTableEle;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyRoundInstanceStatusCompleted()
        {
            IWebElement cell = UnallocatedTableEle.GetCell(0, 1);
            IWebElement img = cell.FindElement(By.XPath("./div//img"));
            Assert.IsTrue(img.GetAttribute("src").Contains("coretaskstate/s3.png"));
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnWorksheetTab()
        {
            ClickOnElement(WorkSheetTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage IsRoundInstancePage()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(rescheduleDateTitle));
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnMinimiseRoundsAndRoundLegsBtn()
        {
            ClickOnElement(ExpandRoundsGo);
            ClickOnElement(expandRoundLegsBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage SendKeyInDesc(string descValue)
        {
            SendKeys(descInput, descValue);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyDisplayNotesAfterSearchWithDesc(string noteValue)
        {
            Assert.AreEqual(noteValue.Trim(), GetElementText(noteAtFirstRow).Trim());
            return this;
        }

        [AllureStep]
        public DetailTaskPage DoubleClickOnFirstRowAfterFilteringWithDesc()
        {
            DoubleClickOnElement(firstRowAfterFiltering);
            return PageFactoryManager.Get<DetailTaskPage>();
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnSelectAndDeselectBtn()
        {
            ClickOnElement(selectAndDeselectBtn);
            return this;
        }
    }
}
