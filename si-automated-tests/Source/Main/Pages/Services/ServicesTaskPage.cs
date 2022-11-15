using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServicesTaskPage : BasePageCommonActions
    {
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
            Assert.AreEqual("true", GetAttributeValue(EndDateInput, "disabled"), "End date is not disabled");
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
    }
}
