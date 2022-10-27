using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
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
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By TaskLinesTab = By.XPath("//a[@aria-controls='taskLines-tab']");
        public readonly By ExpandRoundsGo = By.XPath("//button[@id='t-toggle-rounds']");
        private readonly By expandRoundLegsBtn = By.XPath("//span[text()='Expand Round Legs']/parent::button");
        public readonly By IdFilterInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]//input");
        public readonly string UnallocatedRow = "./div[contains(@class, 'assured')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string UnallocatedState = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string UnallocatedID = "./div[contains(@class, 'slick-cell l3 r3')]";
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By idInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]/input");
        private readonly By noteAtFirstRow = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l20')]");
        private readonly By statusAtFirstRow = By.XPath("//div[@class='grid-canvas']//div[contains(@class, 'l18')]");
        private readonly By descAtFirstRow = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l4')]");
        private readonly By firstRowAfterFiltering = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l4')]/parent::div");
        private readonly By selectAndDeselectBtn = By.CssSelector("div[title='Select/Deselect All']");

        public readonly string SlickRoundRow = "./div[contains(@class, 'slick-group')]";
        public readonly string RoundDescriptionCell = "./div[contains(@class, 'slick-cell l0')]";
        private readonly By title = By.XPath("//h4[text()='Round Instance']");
        private readonly By rescheduleDateTitle = By.XPath("//label[text()='Reschedule Date']");
        public readonly By TaskStateSelect = By.XPath("//div[@id='details-tab']//select[@id='taskState.id']");
        public readonly By BulkUpdateButton = By.XPath("//button[@title='Bulk Update']");
        #region Bulk update
        public readonly By BulkUpdateStateSelect = By.XPath("//div[@class='bulk-confirmation']//select[1]");
        public readonly By BulkUpdateReasonSelect = By.XPath("//div[@class='bulk-confirmation']//select[2]");
        public readonly By ConfirmButton = By.XPath("//button[text()='Confirm']");
        private readonly By statusDd = By.XPath("//label[text()='Status']/following-sibling::select[1]");

        //DYNAMIC
        private readonly string statusOptionInBulkUpdate = "//label[text()='Status']/following-sibling::select[1]/option[{0}]";
        #endregion

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

        private readonly string TaskLineTable = "//div[@id='taskLines-tab']//table";
        private readonly string TaskLineRow = "./tbody//tr[contains(@data-bind,'with: $data.getFields()')]";
        private readonly string TaskLineOrderCell = "./td//input[@id='order.id']";
        private readonly string TaskLineTypeCell = "./td//select[@id='taskLineType.id']";
        private readonly string AssetTypeCell = "./td//echo-select[contains(@params, 'assetType')]//select";
        private readonly string AssetActualCell = "./td//input[@id='actualAssetQuantity.id']";
        private readonly string AssetScheduleCell = "./td//input[@id='scheduledAssetQuantity.id']";
        private readonly string ProductCell = "./td//echo-select[contains(@params, 'product')]//select";
        private readonly string ProductActualCell = "./td//input[@id='actualProductQuantity.id']";
        private readonly string ProductScheduleCell = "./td//input[@id='scheduledProductQuantity.id']";
        private readonly string UnitCell = "./td//echo-select[contains(@params, 'unitOfMeasure')]//select";
        private readonly string SiteDestinationCell = "./td//select[@id='destinationSite.id']";
        private readonly string SiteProductCell = "./td//select[@id='siteProduct.id']";
        private readonly string StateCell = "./td//select[@id='itemState.id']";
        private readonly string ResolutionCodeCell = "./td//select[@id='resCode.id']";

        public TableElement TaskLineTableEle
        {
            get => new TableElement(TaskLineTable, TaskLineRow, new List<string>()
            {
                TaskLineOrderCell, TaskLineTypeCell, AssetTypeCell,
                AssetActualCell, AssetScheduleCell, ProductCell,
                ProductActualCell, ProductScheduleCell, UnitCell,
                SiteDestinationCell, SiteProductCell, StateCell, ResolutionCodeCell
            });
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyTaskLineState(string state)
        {
            VerifyCellValue(TaskLineTableEle, 0, 11, state);
            return this;
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
            //WaitUtil.WaitForElementVisible(rescheduleDateTitle);
            //Assert.IsTrue(IsControlDisplayed(rescheduleDateTitle));
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
        public RoundInstanceDetailPage VerifyDisplayRowAfterSearchWithDesc(string noteValue)
        {
            Assert.AreEqual(noteValue.Trim(), GetElementText(descAtFirstRow).Trim());
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

        [AllureStep]
        public RoundInstanceDetailPage SendKeyInId(string idValue)
        {
            SendKeys(idInput, idValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnStatusFirstRow()
        {
            ClickOnElement(statusAtFirstRow);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage DoubleClickOnTask()
        {
            UnallocatedTableEle.DoubleClickRow(0);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnFirstRound()
        {
            ClickOnElement(By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[2]//input[@type='checkbox']"));
            SleepTimeInMiliseconds(3000);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnSecondAfterClickingFirstRound()
        {
            ClickOnElement(By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[1]//input[@type='checkbox']"));
            SleepTimeInMiliseconds(3000);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnBulkUpdateBtn()
        {
            ClickOnElement(BulkUpdateButton);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnStatusDdInBulkUpdatePopup()
        {
            ClickOnElement(statusDd);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyOrderInTaskStateDd(string[] taskStateValues)
        {
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(statusOptionInBulkUpdate, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }
    }
}
