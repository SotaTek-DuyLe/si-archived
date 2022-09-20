using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Models.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServicesTaskPage : BasePageCommonActions
    {
        private string taskLineTab = "//a[@aria-controls='tasklines-tab']";
        public string AnnouncementTab = "//a[@aria-controls='announcements-tab']";
        public string  DetailTab = "//a[@aria-controls='details-tab']";
        public string ScheduleTab = "//a[@aria-controls='schedules-tab']";

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
    }
}
