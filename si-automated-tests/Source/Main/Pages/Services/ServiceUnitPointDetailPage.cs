using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitPointDetailPage : BasePage
    {
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
        private readonly By typeValue = By.CssSelector("//td[text()='Address']/following-sibling::td");
        private readonly By segmentValue = By.CssSelector("//td[text()='Segment']/following-sibling::td");

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
            Assert.AreEqual(serviceUnitPointDBModel.pointID, GetAttributeValue(pointIdInput, "value"));
            Assert.AreEqual(pointTypeDBModel.pointtype, GetAttributeValue(pointIdInput, "value"));
            Assert.AreEqual(serviceUnitPointDBModel.startdate.ToString(), GetAttributeValue(startDateInput, "value").Replace("-", "/") + "00:00:00.000");
            Assert.AreEqual(serviceUnitPointDBModel.enddate.ToString(), GetAttributeValue(endDateInput, "value").Replace("-", "/") + "00:00:00.000");
            return this;
        }
    }
}
