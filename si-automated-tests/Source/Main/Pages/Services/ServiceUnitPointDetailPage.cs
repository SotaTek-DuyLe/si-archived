using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using System;
using System.Collections.Generic;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitPointDetailPage : BasePageCommonActions
    {
        public readonly By RetireButton = By.XPath("//button[@title='Retire']");
        public readonly By LastUpdatedInput = By.XPath("//input[@id='lastUpdated']");
        private readonly By title = By.XPath("//span[text()='Service Unit Point']");
        private readonly By serviceUnitPointName = By.XPath("//h5[@data-bind='text: pointAddress']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By mapTab = By.CssSelector("a[aria-controls='map-tab']");

        //DETAIL TAB
        private readonly By pointIdInput = By.CssSelector("input[id='pointId.id']");
        private readonly By pointTypeSelect = By.CssSelector("select[id='pointType.id']");
        private readonly By startDateInput = By.CssSelector("input[name='startDate']");
        private readonly By endDateInput = By.CssSelector("input[name='endDate']");
        private readonly By serviceUnitPointTypeDd = By.CssSelector("select[name='serviceUnitPointType']");

        //MAP TAB
        private readonly By typeValue = By.XPath("//td[text()='Address']/following-sibling::td");
        private readonly By segmentValue = By.XPath("//td[text()='Segment']/following-sibling::td");

        public ServiceUnitPointDetailPage IsServiceUnitPointDetailPage(string serviceUnitPointNameExp)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.AreEqual(GetElementText(serviceUnitPointName), serviceUnitPointNameExp, "Wrong service unit point name");
            return this;
        }

        public ServiceUnitPointDetailPage VerifyValuesInDetailTab(string pointIdExp, string pointTypeExp)
        {
            Assert.AreEqual(GetAttributeValue(pointIdInput, "value"), pointIdExp, "Wrong pointId");
            Assert.AreEqual(GetFirstSelectedItemInDropdown(pointTypeSelect), pointTypeExp, "Wrong pointType");
            Assert.AreEqual(GetAttributeValue(startDateInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(endDateInput, "value"), "01/01/2050");
            Assert.AreEqual(GetFirstSelectedItemInDropdown(serviceUnitPointTypeDd), "", "Wrong serviceUnitPointType");
            //Read only
            Assert.AreEqual(GetAttributeValue(pointIdInput, "disabled"), "true", "Point Id is not disabled");
            Assert.AreEqual(GetAttributeValue(pointTypeSelect, "disabled"), "true", "Point Type is not disabled");
            return this;
        }

        public ServiceUnitPointDetailPage ClickOnMapTab()
        {
            ClickOnElement(mapTab);
            return this;
        }

        public ServiceUnitPointDetailPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            return this;
        }

        public ServiceUnitPointDetailPage VerifyValueInMapTab(string typeExp, string segmentExp)
        {
            Assert.AreEqual(GetElementText(typeValue), typeExp);
            Assert.AreEqual(GetElementText(segmentValue), segmentExp);
            return this;
        }

        public ServiceUnitPointDetailPage VerifyServiceUnitPointTypeAfter(string expValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(serviceUnitPointTypeDd), expValue);
            return this;
        }

        public string GetServiceUnitPointId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/service-unit-points/", "");
        }

        public ServiceUnitPointDetailPage VerifyUIWithDB(ServiceUnitPointDBModel serviceUnitPointDBModel, PointTypeDBModel pointTypeDBModel)
        {
            Assert.AreEqual(serviceUnitPointDBModel.pointID.ToString(), GetAttributeValue(pointIdInput, "value"));
            Assert.AreEqual(pointTypeDBModel.pointtype.ToString(), GetFirstSelectedItemInDropdown(pointTypeSelect));
            Assert.IsTrue(serviceUnitPointDBModel.startdate.ToString().Replace("-", "/").Contains(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT) + " 00:00:00") || serviceUnitPointDBModel.startdate.ToString().Replace("-", "/").Contains(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_MM_DD_YYYY_FORMAT) + " 00:00:00"), "Wrong inpsection valid Date");
            Console.WriteLine(serviceUnitPointDBModel.enddate.ToString());
            Assert.IsTrue(serviceUnitPointDBModel.enddate.ToString().Contains("01/01/2050 00:00:00"));
            return this;
        }
    }
}
