using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServicesTaskPage : BasePageCommonActions
    {
        public readonly By OpenServiceUnitLink = By.XPath("//a[@data-bind='text: serviceUnitDesc, click: openServiceUnit']");
        private string taskLineTab = "//a[@aria-controls='tasklines-tab']";
        public string AnnouncementTab = "//a[@aria-controls='announcements-tab']";
        public string  DetailTab = "//a[@aria-controls='details-tab']";
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By mapTab = By.CssSelector("a[aria-controls='map-tab']");
        private readonly By risksTab = By.CssSelector("a[aria-controls='risks-tab']");
        private readonly By subscriptionTab = By.CssSelector("a[aria-controls='subscriptions-tab']");
        private readonly By notificationsTab = By.CssSelector("a[aria-controls='notifications-tab']");
        private readonly By indicatorsTab = By.CssSelector("a[aria-controls='objectIndicators-tab']");
        public string ScheduleTab = "//a[@aria-controls='schedules-tab']";
        private readonly By title = By.XPath("//span[text()='Service Task']");
        private readonly By serviceGroupTitle = By.XPath("//div[text()='SERVICE GROUP']");
        private readonly By serviceGroupName = By.XPath("//div[text()='SERVICE GROUP']/following-sibling::div");
        private readonly By servicesSite = By.XPath("//div[text()='SITE']/following-sibling::a");
        private readonly By serviceName = By.XPath("//div[text()='SERVICE']/following-sibling::div");
        private readonly By serviceTaskScheduleName = By.ClassName("typeUrl");
        private readonly By typeUrlLink = By.CssSelector("a[class='typeUrl']");

        private string serviceTaskName = "//span[text()='Service Task']/following-sibling::span[contains(text(),'{0}')]";
        private string headerPartyName = "//div[@class='headers-container']//a[contains(text(), '{0}')]";

        public readonly By TaskNoteInput = By.XPath("//div[@id='details-tab']//textarea[@id='taskNotes.id']");
        public readonly By StartDateInput = By.XPath("//div[@id='details-tab']//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//div[@id='details-tab']//input[@id='endDate.id']");
        public readonly By IndicatorStartDateInput = By.XPath("//div[@id='details-tab']//input[@id='indicatorStartDate.id']");
        public readonly By IndicatorEndDateInput = By.XPath("//div[@id='details-tab']//input[@id='indicatorEndDate.id']");
        public readonly By TaskCountInput = By.XPath("//div[@id='details-tab']//input[@id='taskCount.id']");
        public readonly By MaxTaskInput = By.XPath("//div[@id='details-tab']//input[@id='maxTasks']");
        public readonly By MaxTaskStartDateInput = By.XPath("//div[@id='details-tab']//input[@id='maxTasksStartDate.id']");
        public readonly By ReferenceInput = By.XPath("//div[@id='details-tab']//input[@id='reference.id']");
        public readonly By AssuredFromInput = By.XPath("//div[@id='details-tab']//input[@id='assuredFrom.id']");
        public readonly By AssuredToInput = By.XPath("//div[@id='details-tab']//input[@id='assuredUntil.id']");
        public readonly By ColorInput = By.XPath("//div[@id='details-tab']//input[contains(@data-bind, 'colour.id')]");
        public readonly By PrioritySelect = By.XPath("//div[@id='details-tab']//select[@id='priority.id']");
        public readonly By TagSelect = By.XPath("//div[@id='details-tab']//select[@id='tag.id']");
        public readonly By TaskIndicatorSelect = By.XPath("//div[@id='details-tab']//select[@id='taskIndicator.id']");
        public readonly By AssuredTaskCheckbox = By.XPath("//div[@id='details-tab']//input[contains(@data-bind, 'assuredTask.id')]");
        public readonly By ProximityAlertCheckbox = By.XPath("//div[@id='details-tab']//input[contains(@data-bind, 'proximityAlert.id')]");
        public readonly By RetireButton = By.XPath("//div[@class='navbar-right pull-right']//button[@title='Retire']");
        public readonly By CreateAdHocTaskButton = By.XPath("//button[contains(text(), 'Create Ad-Hoc Task')]");

        public readonly By SubscriptionTab = By.XPath("//a[@aria-controls='subscriptions-tab']");
        #region SubscriptionTab
        public readonly By AddNewSubscriptionButton = By.XPath("//button[@data-bind='click: createSubscription']");
        public readonly By SubscriptionIFrame = By.XPath("//iframe[@id='subscriptions-tab']");
        private readonly string SubcriptionTable = "//div[@class='grid-canvas']";
        private readonly string SubscriptionRow = "./div[contains(@class, 'slick-row')]";
        private readonly string SubscriptionIdCell = "./div[contains(@class, 'l0')]";
        private readonly string SubscriptionContractIdCell = "./div[contains(@class, 'l1')]";
        private readonly string SubscriptionContractCell = "./div[contains(@class, 'l2')]";
        private readonly string SubscriptionMobileCell = "./div[contains(@class, 'l3')]";
        private readonly string SubscriptionStateCell = "./div[contains(@class, 'l4')]";
        private readonly string SubscriptionStartDateCell = "./div[contains(@class, 'l5')]";
        private readonly string SubscriptionEndDateCell = "./div[contains(@class, 'l6')]";
        private readonly string SubscriptionNotesCell = "./div[contains(@class, 'l7')]";
        private readonly string SubscriptionSubjectCell = "./div[contains(@class, 'l8')]";
        private readonly string SubscriptionSubjectDesCell = "./div[contains(@class, 'l9')]";

        public TableElement SubscriptionTableEle
        {
            get => new TableElement(SubcriptionTable, SubscriptionRow,
                new List<string>() {
                    SubscriptionIdCell, SubscriptionContractIdCell, SubscriptionContractCell,
                    SubscriptionMobileCell, SubscriptionStateCell, SubscriptionStartDateCell,
                    SubscriptionEndDateCell, SubscriptionNotesCell, SubscriptionSubjectCell, SubscriptionSubjectDesCell
                });
        }

        [AllureStep]
        public ServicesTaskPage VerifyNewSubscription(string id, string firstName, string lastName, string mobile, string subjectDescription)
        {
            int newIdx = SubscriptionTableEle.GetRows().Count - 1;
            VerifyCellValue(SubscriptionTableEle, newIdx, 0, id);
            VerifyCellValue(SubscriptionTableEle, newIdx, 2, firstName + " " + lastName);
            VerifyCellValue(SubscriptionTableEle, newIdx, 3, mobile);
            string subjectDescriptionCellValue = SubscriptionTableEle.GetCellValue(newIdx, 9).AsString();
            Assert.IsTrue(subjectDescription.Contains(subjectDescriptionCellValue));
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyColumnsDisplay(List<string> columnNames)
        {
            var headerEles = GetAllElements(By.XPath("//div[contains(@class, 'slick-header-columns')]//span[@class='slick-column-name']"));
            foreach (var item in headerEles)
            {
                Assert.IsTrue(columnNames.Contains(item.Text));
            }
            return this;
        }
        #endregion

        #region Schedule Tab
        public readonly By DuplicateButton = By.XPath("(//div[@id='schedules-tab']//button[@title='Duplicate'])[1]");
        private string TaskScheduleTable = "//tbody[@data-bind='foreach: fields.serviceTaskSchedules.value']";
        private string TaskScheduleRow = "./tr";
        private string ScheduleCell = "./td[@data-bind='text: schedule.value']";
        private string ScheduleStartDateCell = "./td[@data-bind='text: startDate.value']";
        private string ScheduleEndDateCell = "./td[@data-bind='text: endDate.value']";
        private string ScheduleRolloverCell = "./td//input[@type='checkbox']";
        private string ScheduleRoundGroupCell = "./td[@data-bind='text: roundGroup.value']";
        private string ScheduleRoundCell = "./td[@data-bind='text: round.value']";
        private string ScheduleDuplicateButtonCell = "./td//button[@title='Duplicate']";
        
        public TableElement ScheduleTableEle
        {
            get => new TableElement(TaskScheduleTable, TaskScheduleRow, new List<string>() { ScheduleCell, ScheduleStartDateCell, ScheduleEndDateCell, ScheduleRolloverCell, ScheduleRoundGroupCell, ScheduleRoundCell, ScheduleDuplicateButtonCell });
        }

        [AllureStep]
        public ServicesTaskPage ClickDuplicateButton(int rowIdx)
        {
            ScheduleTableEle.ClickCell(rowIdx, 6);
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyNewSchedule(string startdate, string enddate, string round)
        {
            VerifyCellValue(ScheduleTableEle, 0, 1, startdate);
            VerifyCellValue(ScheduleTableEle, 0, 2, enddate);
            VerifyCellValue(ScheduleTableEle, 0, 5, round.Replace(":", ""));
            return this;
        }
        #endregion

        #region Risk
        public readonly By RiskTab = By.XPath("//a[@aria-controls='risks-tab']");
        public readonly By RiskIframe = By.XPath("//iframe[@id='risks-tab']");
        public readonly By BulkCreateButton = By.XPath("//button[@title='Add risk register(s)']");
        private readonly string riskTable = "//div[@id='risk-grid']//div[@class='grid-canvas']";
        private readonly string riskRow = "./div[contains(@class,'slick-row')]";
        private readonly string riskCheckboxCell = "./div[@class='slick-cell l0 r0']//input";
        private readonly string riskNameCell = "./div[@class='slick-cell l2 r2']";
        private readonly string riskStartDateCell = "./div[@class='slick-cell l9 r9']";
        private readonly string riskEndDateCell = "./div[@class='slick-cell l10 r10']";
        public TableElement RiskTableEle
        {
            get => new TableElement(riskTable, riskRow, new List<string>() { riskCheckboxCell, riskNameCell, riskStartDateCell, riskEndDateCell });
        }

        [AllureStep]
        public ServicesTaskPage VerifyRiskSelect(string riskName, string startdate, string endDate)
        {
            Assert.IsNotNull(RiskTableEle.GetCellByCellValues(0, new Dictionary<int, object>()
            {
                { RiskTableEle.GetCellIndex(riskNameCell), riskName },
                { RiskTableEle.GetCellIndex(riskStartDateCell), startdate },
                { RiskTableEle.GetCellIndex(riskEndDateCell), endDate },
            }));
            return this;
        }
        #endregion


        [AllureStep]
        public ServicesTaskPage WaitForTaskPageLoadedSuccessfully(String service, String partyname)
        {
            WaitUtil.WaitForElementVisible(serviceTaskName, service);
            WaitUtil.WaitForElementVisible(headerPartyName, partyname);
            return this;
        }

        [AllureStep]
        public ServicesTaskPage IsServiceTaskPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(serviceGroupTitle);
            WaitUtil.WaitForElementVisible(DetailTab);
            WaitUtil.WaitForElementVisible(RiskTab);

            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnTaskLineTab()
        {
            ClickOnElement(taskLineTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ServicesTaskPage ClickOnScheduleTask()
        {
            ClickOnElement(ScheduleTab);
            return this;
        }
        [AllureStep]
        public ServicesTaskPage VerifyServiceTaskInDB(List<ServiceTaskLineDBModel> listAll, int scheduledassetquantity, string assettype, int scheduledproductquantity, string unit, string product, string startdate, string enddate)
        {
            int n = 0;
            for (int i = 0; i < listAll.Count; i++)
            {
                string _startDate = CommonUtil.ParseDateTimeToFormat(listAll[i].startdate, "dd/MM/yyyy").Replace('-', '/');
                string _endDate = CommonUtil.ParseDateTimeToFormat(listAll[i].enddate, "dd/MM/yyyy").Replace('-', '/');
                
                if (listAll[i].scheduledassetquantity.Equals(scheduledassetquantity) && _startDate.Equals(startdate))
                {
                    Assert.AreEqual(listAll[i].scheduledassetquantity, scheduledassetquantity);
                    Assert.AreEqual(listAll[i].assettype, assettype);
                    Assert.AreEqual(listAll[i].scheduledproductquantity, scheduledproductquantity);
                    Assert.AreEqual(listAll[i].unit, unit);
                    Assert.AreEqual(listAll[i].product, product);
                    Assert.AreEqual(_startDate, startdate);
                    Assert.AreEqual(_endDate, enddate);
                    n = 1;
                    break;
                }
            }
            Assert.AreEqual(n, 1);
            return this;
        }
        [AllureStep]
        public ServicesTaskPage VerifyServiceUnitAssets(List<ServiceUnitAssetsDBModel> listAll, int num, string startDate, string endDate)
        {
            int n = 0;
            for (int i = 0; i < listAll.Count; i++)
            {
                string _startDate = CommonUtil.ParseDateTimeToFormat(listAll[i].startdate, "dd/MM/yyyy").Replace('-', '/');
                string _endDate = CommonUtil.ParseDateTimeToFormat(listAll[i].enddate, "dd/MM/yyyy").Replace('-', '/');
                if (_startDate.Equals(startDate) && _endDate.Equals(endDate))
                {
                    n++;
                }
            }
            Assert.AreEqual(n, num);
            return this;
        }
        [AllureStep]
        public ServicesTaskPage VerifyServiceUnitAssets(List<ServiceUnitAssetsDBModel> listAll, int num, string endDate)
        {
            int n = 0;
            for (int i = 0; i < listAll.Count; i++)
            {
                string _endDate = CommonUtil.ParseDateTimeToFormat(listAll[i].enddate, "dd/MM/yyyy").Replace('-', '/');
                if (_endDate.Equals(endDate))
                {
                    n++;
                }
            }
            Assert.AreEqual(n, num);
            return this;
        }
        [AllureStep]
        public ServicesTaskPage VerifyServiceUnitAssets(List<ServiceUnitAssetsDBModel> listAll,int num, string assetType, string _product, string startDate, string endDate)
        {
            int n = 0;
            for (int i = 0; i < listAll.Count; i++)
            {
                string _startDate = CommonUtil.ParseDateTimeToFormat(listAll[i].startdate, "dd/MM/yyyy").Replace('-', '/');
                string _endDate = CommonUtil.ParseDateTimeToFormat(listAll[i].enddate, "dd/MM/yyyy").Replace('-', '/');
                if (_startDate.Equals(startDate) && _endDate.Equals(endDate) && listAll[i].assettype.Equals(assetType) && listAll[i].product.Equals(_product))
                {
                    n++;
                }
            }
            Assert.AreEqual(n, num);
            return this;
        }
        [AllureStep]
        public ServicesTaskPage VerifyServiceUnitAssetsNum(List<ServiceUnitAssetsDBModel> listAll, int num)
        {
            Assert.AreEqual(listAll.Count, num);
            return this;
        }
        [AllureStep]
        public ServicesTaskPage VerifyServiceTaskAgreementNum(List<ServiceTaskForAgreementDBModel> listAll, int num, string startDate) 
        {
            int n = 0;
            for (int i = 0; i < listAll.Count; i++)
            {
                string _startDate = CommonUtil.ParseDateTimeToFormat(listAll[i].startdate, "dd/MM/yyyy").Replace('-', '/');
                if (_startDate.Equals(startDate) )
                {
                    n++;
                }
            }
            Assert.AreEqual(n, num);
            return this;
        }

        //DETAIL TAB
        private readonly By assuredCheckbox = By.XPath("//label[contains(string(), 'Assured Task')]/following-sibling::input");
        private readonly By proximityAlertCheckbox = By.XPath("//label[contains(string(), 'Proximity Alert')]/following-sibling::input");
        private readonly By assuredFromInput = By.XPath("//input[@id='assuredFrom.id']");
        private readonly By assuredUntilInput = By.XPath("//input[@id='assuredUntil.id']");

        [AllureStep]
        public ServicesTaskPage VerifyAssuredTaskChecked()
        {
            WaitUtil.WaitForElementVisible(assuredCheckbox);
            Assert.IsTrue(IsCheckboxChecked(assuredCheckbox));
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnAssuredTaskCheckbox()
        {
            ClickOnElement(assuredCheckbox);
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyAssuredTaskNotChecked()
        {
            WaitUtil.WaitForElementVisible(assuredCheckbox);
            Assert.IsFalse(IsCheckboxChecked(assuredCheckbox));
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyProximityAlertChecked()
        {
            WaitUtil.WaitForElementVisible(proximityAlertCheckbox);
            Assert.IsTrue(IsCheckboxChecked(proximityAlertCheckbox));
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnDetailTab()
        {
            ClickOnElement(DetailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnAnnouncementTab()
        {
            ClickOnElement(AnnouncementTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnMapTab()
        {
            ClickOnElement(mapTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnRisksTab()
        {
            ClickOnElement(risksTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnSubscriptionsTab()
        {
            ClickOnElement(subscriptionTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnNotificationsTab()
        {
            ClickOnElement(notificationsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnIndicatorsTab()
        {
            ClickOnElement(indicatorsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnDTab()
        {
            ClickOnElement(DetailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyAsseredFromAndAssuredUntil(string fromDate, string untilDate)
        {
            Assert.AreEqual(fromDate, GetAttributeValue(assuredFromInput, "value"));
            Assert.AreEqual(untilDate, GetAttributeValue(assuredUntilInput, "value"));
            return this;
        }

        [AllureStep]
        public string GetServiceGroupName()
        {
            return GetElementText(serviceGroupName);
        }
        
        [AllureStep]
        public string GetServiceSite()
        {
            return GetElementText(servicesSite);
        }

        [AllureStep]
        public string GetServiceName()
        {
            return GetElementText(serviceName);
        }
        [AllureStep]
        public string GetServiceTaskDescription()
        {
            return GetElementText(serviceTaskScheduleName);
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickOnServiceTaskDesc()
        {
            ClickOnElement(typeUrlLink);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }
        [AllureStep]
        public string GetServiceTaskId()
        {
            return GetCurrentUrl().Split("tasks/")[1];
        }

        [AllureStep]
        public ServicesTaskPage VerifyStartDateAndEndDate(string startDateExp, string endDateExp)
        {
            Assert.AreEqual(startDateExp, GetAttributeValue(StartDateInput, "value"));
            Assert.AreEqual(endDateExp, GetAttributeValue(EndDateInput, "value"));
            //End date are disabled
            //Assert.AreEqual("true", GetAttributeValue(EndDateInput, "disabled"), "End date is not disabled");
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyServiceAndServicegroupName(string serviceNameValue, string serviceGroupNameValue)
        {
            Assert.AreEqual(serviceNameValue, GetServiceName());
            Assert.AreEqual(serviceGroupNameValue, GetServiceGroupName());
            return this;
        }

        #region SCHEDULE TAB
        private readonly By schedulesTab = By.XPath("//a[@aria-controls='schedules-tab']");
        private readonly By roundInFirstRow = By.XPath("//tbody/tr[1]/td[contains(@data-bind, 'text: round.value')]");
        private readonly By startDateAtFirstRow = By.XPath("//tbody/tr[1]/td[@data-bind='text: startDate.value']");
        private readonly By endDateAtFirstRow = By.XPath("//tbody/tr[1]/td[@data-bind='text: endDate.value']");

        [AllureStep]
        public ServicesTaskPage ClickOnSchedulesTab()
        {
            ClickOnElement(schedulesTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public string GetRoundName()
        {
            return GetElementText(roundInFirstRow).Trim();
        }

        [AllureStep]
        public ServicesTaskPage VerifyNoteValueInTaskNotes(string noteValueExp)
        {
            Assert.AreEqual(noteValueExp, GetAttributeValue(TaskNoteInput, "value"));
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyServiceScheduleTaskName(string serviceScheduleTaskName)
        {
            Assert.AreEqual(serviceScheduleTaskName, GetServiceTaskDescription());
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyServiceTaskName(string serviceTaskNameValue)
        {
            Assert.IsTrue(IsControlDisplayed(serviceTaskName, serviceTaskNameValue), serviceTaskNameValue + "is not displayed");
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyStartDateAndEndDateFirstRow(string startDateValue, string endDateValue)
        {
            Assert.AreEqual(startDateValue, GetElementText(startDateAtFirstRow), "Start date is incorrect");
            Assert.AreEqual(startDateValue, GetElementText(startDateAtFirstRow), "End date is incorrect");
            return this;
        }

        #endregion


        [AllureStep]
        public ServicesTaskPage ClickCreateAdhocTaskButton()
        {
            ClickOnElement(CreateAdHocTaskButton);
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Service Task?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public ServicesTaskPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectServiceTask)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public ServicesTaskPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
