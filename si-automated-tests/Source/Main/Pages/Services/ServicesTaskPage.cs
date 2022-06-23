using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Models.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServicesTaskPage : BasePage
    {
        private string taskLineTab = "//a[@aria-controls='tasklines-tab']";
        private string scheduleTab = "//a[@aria-controls='schedules-tab']";

        private string serviceTaskName = "//span[text()='Service Task']/following-sibling::span[contains(text(),'{0}')]";
        private string headerPartyName = "//div[@class='headers-container']//a[contains(text(), '{0}')]";

        public ServicesTaskPage WaitForTaskPageLoadedSuccessfully(String service, String partyname)
        {
            WaitUtil.WaitForElementVisible(serviceTaskName, service);
            WaitUtil.WaitForElementVisible(headerPartyName, partyname);
            return this;
        }
        public ServicesTaskPage ClickOnTaskLineTab()
        {
            ClickOnElement(taskLineTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public ServicesTaskPage ClickOnScheduleTask()
        {
            ClickOnElement(scheduleTab);
            return this;
        }
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
        public ServicesTaskPage VerifyServiceUnitAssetsNum(List<ServiceUnitAssetsDBModel> listAll, int num)
        {
            Assert.AreEqual(listAll.Count, num);
            return this;
        }
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
