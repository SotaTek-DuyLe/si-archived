using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Services.ServiceUnit;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class DetailTaskPage : BasePage
    {
        private readonly By taskTitle = By.XPath("//span[text()='Task']");
        private readonly By inspectionBtn = By.XPath("//button[@title='Inspect']");
        private readonly By locationName = By.CssSelector("a[class='typeUrl']");
        private readonly By serviceName = By.XPath("//div[text()='Service']/following-sibling::div");
        private readonly By hyperlinkNextToTask = By.XPath("//span[text()='Task']/following-sibling::span");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");
        private readonly By sourceDataTab = By.CssSelector("a[aria-controls='sourceData-tab']");
        private readonly By taskLineTab = By.CssSelector("a[aria-controls='taskLines-tab']");

        //DETAIL TAB
        private readonly By taskReferenceInput = By.CssSelector("input[name='taskReference']");
        private readonly By resolutionCodeDd = By.CssSelector("select[name='resolutionCode']");
        private readonly By dueDateInput = By.CssSelector("input[id='dueDate.id']");
        private readonly By bookedDateInput = By.CssSelector("input[id='bookedDate.id']");
        private readonly By endDateInput = By.CssSelector("input[id='endDate.id']");
        private readonly By priorityDd = By.CssSelector("input[id='priority.id']");
        private readonly By timeBandDd = By.CssSelector("select[name='timeBand.id']");
        private readonly By taskNoteInput = By.CssSelector("textarea[id='taskNotes.id']");
        private readonly By taskStateDd = By.CssSelector("select[id='taskState.id']");
        private readonly By createdDateInput = By.CssSelector("input[id='createdDate.id']");
        private readonly By scheduledDateInput = By.CssSelector("input[id='scheduledDate.id']");
        private readonly By completionDateInput = By.CssSelector("input[id='completionDate.id']");
        private readonly By taskIndicatorDd = By.CssSelector("select[id='taskIndicator.id']");
        private readonly By slotCountInput = By.CssSelector("input[id='slotCount.id']");
        private readonly By purchaseOrderNumberInput = By.CssSelector("input[id='purchaseOrderNumber']");

        //INSPECTION POPUP
        private readonly By inspectionPopupTitle = By.XPath("//h4[text()='Create ']");
        private readonly By sourceDd = By.CssSelector("select#source");
        private readonly By inspectionTypeDd = By.CssSelector("select#inspection-type");
        private readonly By validFromInput = By.CssSelector("input#valid-from");
        private readonly By validToInput = By.CssSelector("input#valid-to");
        private readonly By allocatedUnitDd = By.CssSelector("select#allocated-unit");
        private readonly By allocatedUserDd = By.CssSelector("select#allocated-user");
        private readonly By noteInput = By.CssSelector("textarea#note");
        private readonly By createBtn = By.XPath("//button[text()='Create']");
        private readonly By cancelBtn = By.XPath("//button[text()='Create']/preceding-sibling::button");

        //INSPECTION TAB
        private readonly By inspectionTab = By.CssSelector("a[aria-controls='taskInspections-tab']");
        private readonly By addNewItemInSpectionBtn = By.XPath("//div[@id='taskInspections-tab']//button[text()='Add New Item']");
        private readonly By allRowInInspectionTabel = By.XPath("//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div");
        private readonly By firstInspectionRow = By.XPath("//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div[1]");
        private const string columnInRowInspectionTab = "//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        //DYNAMIC LOCATOR
        private const string sourceName = "//select[@id='source']/option[text()='{0}']";
        private const string inspectionTypeOption = "//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//select[@id='allocated-unit']/option[text()='{0}']";
        private const string allocatedUserOption = "//select[@id='allocated-user']/option[text()='{0}']";
        private const string fieldInHeaderReadOnly = "//div[text()='Service Group']/following-sibling::div";
        private const string fieldInHeaderWithHyperlink = "//div[text()='{0}']/following-sibling::a";

        public DetailTaskPage IsDetailTaskPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(taskTitle);
            return this;
        }

        public DetailTaskPage VerifyCurrentUrlOfDetailTaskPage(string taskId)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/tasks/" + taskId, GetCurrentUrl());
            return this;
        }

        public DetailTaskPage ClickHyperlinkNextToTask()
        {
            ClickOnElement(hyperlinkNextToTask);
            return this;
        }

        public string GetLocationName()
        {
            return GetElementText(locationName);
        }

        public DetailTaskPage ClickOnInspectionBtn()
        {
            ClickOnElement(inspectionBtn);
            return this;
        }

        public string GetServiceName()
        {
            return GetElementText(serviceName);
        }

        public DetailTaskPage VerifyTaskWithDB(TaskDBModel taskDBModel, ServiceUnitDBModel serviceUnitDBModel)
        {
            //Task
            Assert.AreEqual(taskDBModel.task, GetElementText(locationName));
            //ServiceUnitID
            Assert.AreEqual(serviceUnitDBModel.serviceunit, GetElementText(locationName));
            return this;
        }

        public ServiceUnitDetailPage ClickOnHyperlinkInADesciption()
        {
            ClickOnElement(locationName);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }

        public DetailTaskPage VerifyFieldInHeaderReadOnly(string fieldName)
        {
            Assert.IsTrue(IsControlDisplayed(fieldInHeaderReadOnly, fieldName));
            return this;
        }

        public DetailTaskPage VerifyFieldInHeaderWithLink(string fieldName)
        {
            Assert.IsTrue(IsControlDisplayed(fieldInHeaderWithHyperlink, fieldName));
            return this;
        }

        public DetailPartyPage ClickPartyHyperLink()
        {
            ClickOnElement(fieldInHeaderWithHyperlink, "Party");
            return PageFactoryManager.Get<DetailPartyPage>();
        }

        public DetailSitePage ClickSiteHyperLink()
        {
            ClickOnElement(fieldInHeaderWithHyperlink, "Site");
            return PageFactoryManager.Get<DetailSitePage>();
        }

        public DetailSitePage ClickRoundHyperLink()
        {
            ClickOnElement(fieldInHeaderWithHyperlink, "Round");
            return PageFactoryManager.Get<DetailSitePage>();
        }

        private DetailTaskPage CheckDisplayOfInputIfNullValueDB(string fieldInDb, By locatorOfField)
        {
            if(fieldInDb == null)
            {
                Assert.AreEqual("", GetAttributeValue(locatorOfField, "value"));
            } else
            {
                Assert.AreEqual(fieldInDb, GetAttributeValue(locatorOfField, "value"));
            }
            return this;
        }

        private DetailTaskPage CheckDisplayOfInputIfNullValueDB(DateTime fieldInDb, string formatDate, By locatorOfField)
        {
            if (fieldInDb == null)
            {
                Assert.AreEqual("", GetAttributeValue(locatorOfField, "value"));
            }
            else
            {
                Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(fieldInDb, formatDate), GetAttributeValue(locatorOfField, "value"));
            }
            return this;
        }

        private DetailTaskPage CheckDisplayOfInputIfNullValueDB(int fieldInDb, By locatorOfField)
        {
            if (fieldInDb == 0)
            {
                Assert.AreEqual("", GetAttributeValue(locatorOfField, "value"));
            }
            else
            {
                Assert.AreEqual(fieldInDb, GetAttributeValue(locatorOfField, "value"));
            }
            return this;
        }

        public DetailTaskPage VerifyDetailTabWithDataInDB(TaskDBModel taskDBModel)
        {
            CheckDisplayOfInputIfNullValueDB(taskDBModel.taskreference, taskReferenceInput);
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(resolutionCodeDd));
            Assert.AreEqual(taskDBModel.resolutioncodeID, null);
            Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(taskDBModel.taskduedate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT), dueDateInput);
            CheckDisplayOfInputIfNullValueDB(taskDBModel.prebookeddate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, bookedDateInput);
            CheckDisplayOfInputIfNullValueDB(taskDBModel.taskenddate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, endDateInput);
            if(taskDBModel.priorityID == null)
            {
                Assert.AreEqual("", GetFirstSelectedItemInDropdown(priorityDd));
            } else
            {
                Assert.AreEqual(taskDBModel.priorityID.ToString(), GetFirstSelectedItemInDropdown(priorityDd));
            }
            if (taskDBModel.servicetimebandID == 0)
            {
                Assert.AreEqual("", GetFirstSelectedItemInDropdown(timeBandDd));
            }
            else
            {
                Assert.AreEqual(taskDBModel.servicetimebandID.ToString(), GetFirstSelectedItemInDropdown(priorityDd));
            }
            CheckDisplayOfInputIfNullValueDB(taskDBModel.tasknotes, taskNoteInput);
            //Check - Task State
            Assert.AreEqual("In Progress", GetFirstSelectedItemInDropdown(taskStateDd));
            Assert.AreEqual("12", taskDBModel.taskstateID);
            //Created Date
            Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(taskDBModel.taskcreateddate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT), createdDateInput);
            //Scheduled Date
            Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(taskDBModel.taskscheduleddate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT), scheduledDateInput);
            //Completion Date
            CheckDisplayOfInputIfNullValueDB(taskDBModel.taskcompleteddate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, completionDateInput);
            //Slot count
            Assert.AreEqual(taskDBModel.slotcount, slotCountInput);
            //Purchase order
            CheckDisplayOfInputIfNullValueDB(taskDBModel.taskorder, purchaseOrderNumberInput);
            return this;
        }

        public DetailTaskPage ClickDataTab()
        {
            ClickOnElement(dataTab);
            return this;
        }

        public DetailTaskPage ClickSourceDataTab()
        {
            ClickOnElement(sourceDataTab);
            return this;
        }

        public DetailTaskPage ClickTasklinesTab()
        {
            ClickOnElement(taskLineTab);
            return this;
        }

        //Task lines tab
        private readonly By taskLineRows = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value']");
        private readonly By orderColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='order.id']");
        private readonly By typeColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] select[id='taskLineType.id']");
        private readonly By assetTypeColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] td:nth-child(3) select");
        private readonly By scheduledAssetQtyColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='scheduledAssetQuantity.id']");
        private readonly By actualAssetQuantityColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='actualAssetQuantity.id']");
        private readonly By minAssetQtyColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='minAssetQty.id']']");
        private readonly By maxAssetQtyColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='maxAssetQty.id']");
        private readonly By productColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] td:nth-child(8) select");
        private readonly By scheduleProductQuantityColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='scheduledProductQuantity.id']");
        private readonly By actualProductQuantityColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='actualProductQuantity.id']");
        private readonly By minProductQuantityColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='minProductQty.id']']");
        private readonly By maxProductQuantityColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] input[id='maxProductQty.id']");
        private readonly By unitColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] td:nth-child(13) select");
        private readonly By serialisedCheckbox = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] td:nth-child(14) input");
        private readonly By destinationSiteColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] select[id='destinationSite.id']");
        private readonly By siteProductColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] select[id='siteProduct.id']");
        private readonly By assetColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] div[id='asset']>input");
        private readonly By stateColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] select[id='itemState.id']");
        private readonly By resolutionCodeColumns = By.CssSelector("table>tbody[data-bind='foreach: fields.taskLines.value'] select[id='resCode.id']");
        private readonly By addNewTaskLinesBtn = By.CssSelector("div[id='taskLines-tab'] button[title='Add New Item']");

        //DYNAMIC
        private const string taskLineTypeAtAnyRows = "//tr[{0}]//select[@id='taskLineType.id']";
        private const string optionServiceTaskLineTypeAtAnyRows = "//tr[{0}]//select[@id='taskLineType.id']/option[text()='Service']";
        private const string assetTypeAtAnyRows = "//tr[{0}]/td[3]//select";
        private const string option1100LAssetTypeAtAnyRows = "//tr[{0}]/td[3]//select//option[text()='1100L']";
        private const string scheduleAssetQtyAtAnyRows = "//tr[{0}]//input[@id='scheduledAssetQuantity.id']";


        public List<TaskLineModel> GetAllTaskLineInTaskLineTab()
        {
            List<TaskLineModel> taskLineModels = new List<TaskLineModel>();
            for(int i = 0; i < GetAllElements(taskLineRows).Count; i++)
            {
                string order = GetAttributeValue(GetAllElements(orderColumns)[i], "value");
                string type = GetFirstSelectedItemInDropdown(GetAllElements(typeColumns)[i]);
                string assetType = GetFirstSelectedItemInDropdown(GetAllElements(assetTypeColumns)[i]);
                string scheduleAssetQty = GetAttributeValue(GetAllElements(scheduledAssetQtyColumns)[i], "value");
                string actualAssetQuantity = GetAttributeValue(GetAllElements(actualAssetQuantityColumns)[i], "value");
                string minAssetQty = GetAttributeValue(GetAllElements(minAssetQtyColumns)[i], "value");
                string maxAssetQty = GetAttributeValue(GetAllElements(maxAssetQtyColumns)[i], "value");
                string product = GetFirstSelectedItemInDropdown(GetAllElements(productColumns)[i]);
                string scheduledProductQuantity = GetAttributeValue(GetAllElements(scheduleProductQuantityColumns)[i], "value");
                string actualProductQuantity = GetAttributeValue(GetAllElements(actualProductQuantityColumns)[i], "value");
                string minProductQty = GetAttributeValue(GetAllElements(minProductQuantityColumns)[i], "value");
                string maxProductQty = GetAttributeValue(GetAllElements(maxProductQuantityColumns)[i], "value");
                string unit = GetFirstSelectedItemInDropdown(GetAllElements(unitColumns)[i]);
                string destinationSite = GetFirstSelectedItemInDropdown(GetAllElements(destinationSiteColumns)[i]);
                string siteProduct = GetFirstSelectedItemInDropdown(GetAllElements(siteProductColumns)[i]);
                string asset = GetAttributeValue(GetAllElements(assetColumns)[i], "value");
                string state = GetFirstSelectedItemInDropdown(GetAllElements(stateColumns)[i]);
                string resolutionCode = GetFirstSelectedItemInDropdown(GetAllElements(resolutionCodeColumns)[i]);
                taskLineModels.Add(new TaskLineModel(order, type, assetType, scheduleAssetQty, actualAssetQuantity, minAssetQty, maxAssetQty, product, scheduledProductQuantity, actualProductQuantity, minProductQty, maxProductQty, unit, destinationSite, siteProduct, asset, state, resolutionCode));
            }
            return taskLineModels;
        }

        public DetailTaskPage VerifyDataInTaskLinesTab(TaskLineDBModel taskLineDBModel, TaskLineModel taskLineModelDisplayed, TaskLineTypeDBModel taskLineTypeDBModel, AssetTypeDBModel assetTypeDBModel, ProductDBModel productDBModel)
        {
            Assert.AreEqual("0", taskLineModelDisplayed.order);
            //Type
            Assert.AreEqual(taskLineTypeDBModel.tasklinetype, taskLineModelDisplayed.type);
            //Asset type
            Assert.AreEqual(assetTypeDBModel.assettype, taskLineModelDisplayed.assetType);
            //Scheduled Asset Qty
            Assert.AreEqual(taskLineDBModel.scheduledassetquantity, taskLineModelDisplayed.scheduledAssetQty);
            //Actual Asset Quantity
            if(taskLineDBModel.actualassetquantity == null)
            {
                Assert.AreEqual(0, taskLineModelDisplayed.actualAssetQuantity);
            } else
            {
                Assert.AreEqual(taskLineDBModel.actualassetquantity, taskLineModelDisplayed.actualAssetQuantity);
            }
            //Min Asset Qty
            Assert.AreEqual(taskLineDBModel.minassetquantity, taskLineModelDisplayed.minAssetQty);
            //Max Asset Qty
            Assert.AreEqual(taskLineDBModel.maxassetquantity, taskLineModelDisplayed.maxAssetQty);
            //Product
            Assert.AreEqual(productDBModel.product, taskLineModelDisplayed.product);
            //Scheduled Product Quantity
            Assert.AreEqual(taskLineDBModel.scheduledproductquantity, taskLineModelDisplayed.scheduledProductQuantity);
            if(taskLineDBModel.actualproductquantity == null)
            {
                Assert.AreEqual(0, taskLineModelDisplayed.actualProductQuantity);
            } else
            {
                Assert.AreEqual(taskLineDBModel.actualproductquantity, taskLineModelDisplayed.actualProductQuantity);
            }

            //Min product Qty
            Assert.AreEqual(taskLineDBModel.minproductquantity, taskLineModelDisplayed.minProductQty);
            //Max product Qty
            Assert.AreEqual(taskLineDBModel.maxproductquantity, taskLineModelDisplayed.maxProductQty);
            //Unit
            Assert.AreEqual("Kilogram", taskLineModelDisplayed.unit);
            //State
            Assert.AreEqual("Pending", taskLineModelDisplayed.state);

            return this;
        }

        public DetailTaskPage ClickAddNewItemTaskLineBtn()
        {
            ClickOnElement(addNewTaskLinesBtn);
            return this;
        }

        public DetailTaskPage SelectType(int row)
        {
            ClickOnElement(taskLineTypeAtAnyRows, row.ToString());
            ClickOnElement(optionServiceTaskLineTypeAtAnyRows, row.ToString());
            return this;
        }

        public DetailTaskPage SelectAssetType(int row)
        {
            ClickOnElement(assetTypeAtAnyRows, row.ToString());
            ClickOnElement(option1100LAssetTypeAtAnyRows, row.ToString());
            return this;
        }

        public DetailTaskPage InputScheduledAssetQty(int row, string value)
        {
            SendKeys(string.Format(scheduleAssetQtyAtAnyRows, row.ToString()), value);
            return this;
        }

        public DetailTaskPage VerifyTaskLineCreated(TaskLineModel taskLineModel, string typeInput, string assetType, string scheduledAssetQty)
        {
            Assert.AreEqual(taskLineModel.type, typeInput, "Wrong type input");
            Assert.AreEqual(taskLineModel.assetType, assetType, "Wrong asset type");
            Assert.AreEqual(taskLineModel.assetType, assetType, "Wrong asset type");
            Assert.AreEqual(taskLineModel.scheduledAssetQty, scheduledAssetQty, "Wrong schedule asset quantity");
            return this;
        }

        //INSPECTION POPUP
        public DetailTaskPage IsInspectionPopup()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(inspectionPopupTitle);
            Assert.IsTrue(IsControlDisplayed(sourceDd));
            Assert.IsTrue(IsControlDisplayed(inspectionTypeDd));
            Assert.IsTrue(IsControlDisplayed(validFromInput));
            Assert.IsTrue(IsControlDisplayed(validToInput));
            Assert.IsTrue(IsControlDisplayed(allocatedUnitDd));
            Assert.IsTrue(IsControlDisplayed(allocatedUserDd));
            Assert.IsTrue(IsControlDisplayed(noteInput));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            //Create btn disabled
            Assert.AreEqual(GetAttributeValue(createBtn, "disabled"), "true");
            //Mandatory field
            Assert.AreEqual(GetCssValue(inspectionTypeDd, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(allocatedUnitDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }

        public DetailTaskPage VerifyDefaultValue(string sourceName)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceName);
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

        public DetailTaskPage ClickAndVerifySourceDd(string[] sourceNameList)
        {
            ClickOnElement(sourceDd);
            //Verify
            foreach (string source in sourceNameList)
            {
                Assert.IsTrue(IsControlDisplayed(sourceName, source));
            }
            return this;
        }

        public DetailTaskPage ClickInspectionTypeDdAndSelectValue(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }

        public DetailTaskPage ClickAllocatedUnitAndSelectValue(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }

        public DetailTaskPage ClickAllocatedUserAndSelectValue(string allocatedUserValue)
        {
            ClickOnElement(allocatedUserDd);
            ClickOnElement(allocatedUserOption, allocatedUserValue);
            return this;
        }

        public DetailTaskPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }

        public DetailTaskPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }

        public DetailTaskPage InputValidFrom(string validFromValue)
        {
            SendKeys(validFromInput, validFromValue);
            return this;
        }

        public DetailTaskPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        //INSPECTION TAB
        public DetailTaskPage ClickInspectionTab()
        {
            ClickOnElement(inspectionTab);
            return this;
        }

        public List<InspectionModel> getAllInspection()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(addNewItemInSpectionBtn);
            List<InspectionModel> allModel = new List<InspectionModel>();
            List<IWebElement> allRow = GetAllElements(allRowInInspectionTabel);
            List<IWebElement> allId = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[0]);
            List<IWebElement> allInspectionType = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[1]);
            List<IWebElement> allCreatedDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[2]);
            List<IWebElement> allCreatedByUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[3]);
            List<IWebElement> allAssignedUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[4]);
            List<IWebElement> allAllocatedUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[5]);
            List<IWebElement> allStatus = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[6]);
            List<IWebElement> allValidFrom = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[7]);
            List<IWebElement> allValidTo = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[8]);
            List<IWebElement> allCompletionDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[9]);
            List<IWebElement> allCancelledDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[10]);

            for (int i = 0; i < allRow.Count; i++)
            {
                string id = GetElementText(allId[i]);
                string inspectionType = GetElementText(allInspectionType[i]);
                string createdDate = GetElementText(allCreatedDate[i]);
                string createdByUser = GetElementText(allCreatedByUser[i]);
                string assignedUser = GetElementText(allAssignedUser[i]);
                string allocatedUnit = GetElementText(allAllocatedUser[i]);
                string status = GetElementText(allStatus[i]);
                string validFrom = GetElementText(allValidFrom[i]);
                string validTo = GetElementText(allValidTo[i]);
                string completionDate = GetElementText(allCompletionDate[i]);
                string cancelledDate = GetElementText(allCancelledDate[i]);
                allModel.Add(new InspectionModel(id, inspectionType, createdDate, createdByUser, assignedUser, allocatedUnit, status, validFrom, validTo, completionDate, cancelledDate));
            }
            return allModel;
        }

        public DetailTaskPage VerifyInspectionCreated(InspectionModel inspectionModelActual, string id, string inspectionType, string createdByUser, string assignedUser, string allocatedUnit, string status, string validFrom, string validTo)
        {
            Assert.AreEqual(id, inspectionModelActual.ID);
            Assert.AreEqual(inspectionType, inspectionModelActual.inspectionType);
            Assert.AreEqual(createdByUser, inspectionModelActual.createdByUser);
            Assert.AreEqual(assignedUser, inspectionModelActual.assignedUser);
            Assert.AreEqual(allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(status, inspectionModelActual.status);
            Assert.AreEqual(validFrom, inspectionModelActual.validFrom);
            Assert.AreEqual(validTo, inspectionModelActual.validTo);
            return this;
        }

        public DetailTaskPage ClickAddNewInspectionItem()
        {
            ClickOnElement(addNewItemInSpectionBtn);
            return this;
        }

        public DetailInspectionPage ClickOnFirstInspection()
        {
            DoubleClickOnElement(firstInspectionRow);
            return PageFactoryManager.Get<DetailInspectionPage>();
        }
    }
}
