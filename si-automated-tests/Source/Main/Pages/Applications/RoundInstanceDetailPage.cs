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
        public readonly By OpenRoundTitle = By.XPath("//h4[@title='Open Round']");
        public readonly By WorkSheetTab = By.XPath("//a[@aria-controls='worksheet-tab']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By TaskLinesTab = By.XPath("//a[@aria-controls='taskLines-tab']");
        public readonly By AllocatedResourceTab = By.XPath("//a[@aria-controls='allocated-resources-tab']");
        public readonly By ExpandRoundsGo = By.XPath("//button[@id='t-toggle-rounds']");
        private readonly By showAllTaskTab = By.CssSelector("button[id='t-all-tasks']");
        public readonly By expandRoundLegsBtn = By.XPath("//span[text()='Expand Round Legs']/parent::button");
        public readonly By IdFilterInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]//input");
        public readonly string UnallocatedRow = "./div[contains(@class, 'assured')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string UnallocatedState = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string UnallocatedID = "./div[contains(@class, 'slick-cell l3 r3')]";
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By idInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]/input");
        private readonly By noteAtFirstRow = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l21')]");
        private readonly By statusAtFirstRow = By.XPath("//div[@class='grid-canvas']//div[contains(@class, 'l20')]");
        private readonly By descAtFirstRow = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l4')]");
        private readonly By firstRowAfterFiltering = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l4')]/parent::div");
        private readonly By selectAndDeselectBtn = By.CssSelector("div[title='Select/Deselect All']");
        private readonly By openRoundBtn = By.ClassName("header-round");
        private readonly By slotCountInput = By.XPath("//label[text()='Slots']/following-sibling::div/input");

        public readonly string SlickRoundRow = "./div[contains(@class, 'slick-group')]";
        public readonly string RoundDescriptionCell = "./div[contains(@class, 'slick-cell l0')]";
        private readonly By title = By.XPath("//h4[text()='Round Instance']");
        private readonly By rescheduleDateTitle = By.XPath("//label[text()='Reschedule Date']");
        public readonly By TaskStateSelect = By.XPath("//div[@id='details-tab']//select[@id='taskState.id']");
        public readonly By BulkUpdateButton = By.XPath("//button[@title='Bulk Update']");
        private readonly string statusOptionInFirstRowBulkUpdate = "//div[@class='grid-canvas']//div[contains(@class, 'l20')]/select/option[{0}]";
        private readonly By roundInstanceTitle = By.XPath("//h4[text()='Round Instance']");

        public RoundInstanceDetailPage WaitForRoundInstanceDetailPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(roundInstanceTitle);
            return this;
        }

        public RoundInstanceDetailPage VerifyCurrentUrlRoundPage(string roundInstanceId)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/round-instances/" + roundInstanceId, GetCurrentUrl());
            return this;
        }

        #region Bulk update
        public readonly By BulkUpdateStateSelect = By.XPath("//div[@class='bulk-confirmation']//select[1]");
        public readonly By BulkUpdateReasonSelect = By.XPath("//div[@class='bulk-confirmation']//select[2]");
        public readonly By ConfirmButton = By.XPath("//button[text()='Confirm']");
        private readonly By statusDd = By.XPath("//label[text()='Status']/following-sibling::select[1]");

        //DYNAMIC
        private readonly string statusOptionInBulkUpdate = "//label[text()='Status']/following-sibling::select[1]/option[{0}]";
        #endregion

        #region Allocated Resource
        private string AllocatedResourceTable = "//div[@id='allocated-resources-tab']//table/tbody";
        private string AllocatedResourceRow = "./tr";
        private string ResourceTypeCell = "./td[@data-bind='text: $data.resourceType']";
        private string OriginalResourceTypeCell = "./td[contains(@data-bind, 'originalResourceType')]";
        private string DefaultResourceCell = "./td[contains(@data-bind, 'defaultResource')]";
        private string ResourceCell = "./td[contains(@data-bind, 'data.resource') and @class='btn-link']";
        private string ExperienceCell = "./td[contains(@data-bind, 'getStarRating')]";
        public TableElement AllocatedResourceTableEle
        {
            get => new TableElement(AllocatedResourceTable, AllocatedResourceRow, new List<string>() { ResourceTypeCell, OriginalResourceTypeCell, DefaultResourceCell, ResourceCell, ExperienceCell });
        }

        private int ResourceTypeIndex => AllocatedResourceTableEle.GetCellIndex(ResourceTypeCell);
        private int OriginalResourceTypeCellIndex => AllocatedResourceTableEle.GetCellIndex(OriginalResourceTypeCell);
        private int ResourceCellIndex => AllocatedResourceTableEle.GetCellIndex(ResourceCell);

        [AllureStep]
        public RoundInstanceDetailPage VerifyAllocatedRoundInstance(string type, string originalResourceType)
        {
            Assert.IsTrue(AllocatedResourceTableEle.GetCellByCellValues(0, new Dictionary<int, object> { { ResourceTypeIndex, type }, { OriginalResourceTypeCellIndex, originalResourceType } }) != null);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyAllocatedRoundInstance(string type, string resourceType, string experience)
        {
            IWebElement experienceCellEle = AllocatedResourceTableEle.GetCellByCellValues(AllocatedResourceTableEle.GetCellIndex(ExperienceCell), new Dictionary<int, object> { { ResourceTypeIndex, type }, { ResourceCellIndex, resourceType } });
            Assert.IsTrue(experienceCellEle.Text.Contains(experience));
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickDefaultResource(string type, string originalResourceType)
        {
            AllocatedResourceTableEle.GetCellByCellValues(AllocatedResourceTableEle.GetCellIndex(DefaultResourceCell), new Dictionary<int, object> { { ResourceTypeIndex, type }, { OriginalResourceTypeCellIndex, originalResourceType } }).Click();
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickResource(string type, string originalResourceType)
        {
            AllocatedResourceTableEle.GetCellByCellValues(AllocatedResourceTableEle.GetCellIndex(ResourceCell), new Dictionary<int, object> { { ResourceTypeIndex, type }, { OriginalResourceTypeCellIndex, originalResourceType } }).Click();
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyDefaultResourceIsWorkingOnRI()
        {
            Assert.IsTrue(AllocatedResourceTableEle.GetCellByValue(AllocatedResourceTableEle.GetCellIndex(DefaultResourceCell), "-") != null);
            return this;
        }
        #endregion

        private TableElement slickRoundTableEle;
        public TableElement SlickRoundTableEle
        {
            get => slickRoundTableEle;
        }

        public readonly string UnallocatedDescription = "./div[contains(@class, 'slick-cell l4 r4')]";
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
            Assert.IsTrue(IsControlDisplayed("//img[@title='Closed']"));
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
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(showAllTaskTab);
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
        public RoundInstanceDetailPage DoubleClickOnTask(int taskIdx)
        {
            UnallocatedTableEle.DoubleClickRow(taskIdx);
            return this;
        }

        [AllureStep]
        public int DoubleClickNotCompletedTaskRoundLegs()
        {
            int emptyRowIdx = 0;
            List<IWebElement> taskRows = UnallocatedTableEle.GetRows().ToList();
            foreach (var row in taskRows)
            {
                if (row.FindElement(By.XPath("./div[@class='slick-cell l20 r20']//span")).Text.Trim() != "Completed")
                {
                    emptyRowIdx = taskRows.IndexOf(row);
                    DoubleClickOnElement(row);
                    break;
                }
            }
            return emptyRowIdx;
        }

        [AllureStep]
        public int ClickNotNotCompletedTaskRoundLegs()
        {
            int emptyRowIdx = 0;
            List<IWebElement> taskRows = UnallocatedTableEle.GetRows().ToList();
            foreach (var row in taskRows)
            {
                if (row.FindElement(By.XPath("./div[@class='slick-cell l20 r20']//span")).Text.Trim() != "Completed" && row.FindElement(By.XPath("./div[@class='slick-cell l20 r20']//span")).Text.Trim() != "Not Completed")
                {
                    emptyRowIdx = taskRows.IndexOf(row);
                    ClickOnElement(row);
                    break;
                }
            }
            return emptyRowIdx;
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
            for (int i = 0; i < taskStateValues.Length; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(statusOptionInBulkUpdate, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyOrderTaskStateInFirstRowInWorksheerDd(string[] taskStateValues)
        {
            for (int i = 0; i < taskStateValues.Length; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(statusOptionInFirstRowBulkUpdate, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }
        [AllureStep]
        public RoundInstanceDetailPage OpenRound()
        {
            WaitUtil.WaitForElementSize(openRoundBtn);
            ClickOnElement(openRoundBtn);
            return this;
        }

        #region Event tab
        private readonly By eventTab = By.CssSelector("a[aria-controls='roundInstanceEvents-tab']");
        private readonly By addNewItemEventTab = By.XPath("//div[@id='roundInstanceEvents-tab']//button[text()='Add New Item']");

        [AllureStep]
        public RoundInstanceDetailPage ClickOnEventTab()
        {
            ClickOnElement(eventTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnAddNewItemEventTab()
        {
            ClickOnElement(addNewItemEventTab);
            return this;
        }

        #endregion

        [AllureStep]
        public RoundInstanceDetailPage VerifyMinValueInSlotCountField()
        {
            Assert.AreEqual("0", GetAttributeValue(slotCountInput, "min"));
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage InputSlotCount(string slotCountValue)
        {
            SendKeys(slotCountInput, slotCountValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClearSlotCount()
        {
            ClearInputValue(slotCountInput);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyValueInSlotCount(string slotCountValue)
        {
            Assert.AreEqual(slotCountValue, GetAttributeValue(slotCountInput, "value"));
            return this;
        }

        #region DETAIL TABS
        private readonly By statusInDetailTab = By.XPath("//label[text()='Status']/following-sibling::div//button");
        private readonly By statusInDetailTabDd = By.Id("status");
        private readonly By statusReasonInDetailTab = By.XPath("//label[text()='Status Reason']/following-sibling::div//button");
        private readonly By statusReasonInDetailTabDd = By.Id("status-reason");
        private readonly By debriefBtn = By.CssSelector("button[title='Debrief']");

        //DYNAMIC
        private readonly string statusInDetailOption = "//select[@id='status']/option[text()='{0}']";
        private readonly string statusReasonInDetailOption = "//select[@id='status-reason']/option[text()='{0}']";

        [AllureStep]
        public RoundInstanceDetailPage ClickOnDetailTab()
        {
            ClickOnElement(DetailTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage SelectStatusInDetailTab(string statusValue)
        {
            ClickOnElement(statusInDetailTab);
            ClickOnElement(statusInDetailOption, statusValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage SelectStatusReasonInDetailTab(string statusReasonValue)
        {
            ClickOnElement(statusReasonInDetailTab);
            ClickOnElement(statusReasonInDetailOption, statusReasonValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyStatusInDetailTab(string statusValue)
        {
            Assert.AreEqual(statusValue, GetFirstSelectedItemInDropdown(statusInDetailTabDd));
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyStatusReasonInDetailTab(string statusReasonValue)
        {
            Assert.AreEqual(statusReasonValue, GetFirstSelectedItemInDropdown(statusReasonInDetailTabDd));
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyDisplayDebriefBtn()
        {
            Assert.IsTrue(IsControlDisplayed(debriefBtn), "Debrief button is not displayed");
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage ClickOnDebriefBtn()
        {
            ClickOnElement(debriefBtn);
            return this;
        }

        [AllureStep]
        public string GetRoundInstanceId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/round-instances/", "");
        }

        #endregion
    }
}
