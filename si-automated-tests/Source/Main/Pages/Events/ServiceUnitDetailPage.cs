using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Services;

namespace si_automated_tests.Source.Main.Pages.Events
{
    public class ServiceUnitDetailPage : BasePage
    {
        private readonly By serviceUnitCommonTitle = By.XPath("//span[text()='Service Unit']");
        private readonly By inspecBtn = By.CssSelector("button[title='Inspect']");

        //POPUP
        private readonly By createTitle = By.XPath("//div[@id='inspection-modal']//h4[text()='Create ']");
        private readonly By sourceDd = By.CssSelector("div#inspection-modal select#source");
        private readonly By inspectionTypeDd = By.CssSelector("div#inspection-modal select#inspection-type");
        private readonly By validFromInput = By.CssSelector("div#inspection-modal input#valid-from");
        private readonly By validToInput = By.CssSelector("div#inspection-modal input#valid-to");
        private readonly By allocatedUnitDd = By.XPath("//label[text()=' Allocated Unit']/following-sibling::div/select");
        private readonly By assignedUserDd = By.XPath("//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select");
        private readonly By noteInput = By.CssSelector("div#inspection-modal textarea#note");
        private readonly By cancelBtn = By.XPath("//div[@id='inspection-modal']//button[text()='Cancel']");
        private readonly By createBtn = By.XPath("//div[@id='inspection-modal']//button[text()='Create']");
        private readonly By closeBtn = By.XPath("//div[@id='inspection-modal']//h4[text()='Create ']/parent::div/following-sibling::div/button[@aria-label='Close']");

        //DETAIL TAB
        private readonly By serviceUnitInput = By.CssSelector("input[name='serviceUnit']");
        private readonly By serviceUnitTypeDd = By.CssSelector("select[id='serviceUnitType.id']");
        private readonly By clientRefInput = By.CssSelector("input[name='clientReference']");
        private readonly By startDateInput = By.CssSelector("div[id='details-tab'] input[id='startDate.id']");
        private readonly By endDateInput = By.CssSelector("div[id='details-tab'] input[id='endDate.id']");
        private readonly By pointSegmentInput = By.CssSelector("div[id='details-tab'] input[name='pointSegment']");
        private readonly By lockCheckbox = By.XPath("//span[@data-original-title='Help']/parent::div/following-sibling::input");
        private readonly By serviceLevelDd = By.CssSelector("//div[@id='details-tab']//select[@id='serviceLevel.id']");
        private readonly By streetInput = By.CssSelector("div[id='details-tab'] input[name='street']");

        //SERVICE UNIT POINTS
        private readonly By serviceUnitPointsTab = By.CssSelector("a[aria-controls='serviceUnitPoints-tab']");

        //DYNAMIC
        private const string serviceUnitName = "//h5[text()='{0}']";
        private const string inspectionTypeOption = "//div[@id='inspection-modal']//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()=' Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";

        [AllureStep]
        public ServiceUnitDetailPage WaitForServiceUnitDetailPageDisplayed(string serviceName)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(serviceUnitCommonTitle);
            WaitUtil.WaitForElementVisible(serviceUnitName, serviceName);
            return this;
        }

        [AllureStep]
        public ServiceUnitDetailPage ClickOnInspectionBtn()
        {
            ClickOnElement(inspecBtn);
            return this;
        }

        //INSPECTION MODEL
        [AllureStep]
        public ServiceUnitDetailPage IsCreateInspectionPopup()
        {
            WaitUtil.WaitForElementVisible(createTitle);
            Assert.IsTrue(IsControlDisplayed(sourceDd));
            Assert.IsTrue(IsControlDisplayed(inspectionTypeDd));
            Assert.IsTrue(IsControlDisplayed(validFromInput));
            Assert.IsTrue(IsControlDisplayed(validToInput));
            Assert.IsTrue(IsControlDisplayed(allocatedUnitDd));
            Assert.IsTrue(IsControlDisplayed(assignedUserDd));
            Assert.IsTrue(IsControlDisplayed(noteInput));
            Assert.IsTrue(IsControlDisplayed(closeBtn));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            //Disabled
            Assert.AreEqual(GetAttributeValue(createBtn, "disabled"), "true");
            //Mandatory field
            Assert.AreEqual(GetCssValue(inspectionTypeDd, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(allocatedUnitDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyDefaulValue()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(inspectionTypeDd), "Select... ...");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(allocatedUnitDd).Contains("Select..."));
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), "Select... ...");
            //Date
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickClosePopupBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyPopupDisappears()
        {
            WaitUtil.WaitForElementInvisible(createTitle);
            Assert.IsTrue(IsControlUnDisplayed(createTitle));
            Assert.IsTrue(IsControlUnDisplayed(sourceDd));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }
        [AllureStep]
        public string GetServiceUnitId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/service-units/", "");
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyServiceUnitId(string serviceUnitIdEx, string serviceUnitIdDisplayed)
        {
            Assert.AreEqual(serviceUnitIdEx, serviceUnitIdDisplayed);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyServiceUnitId(string serviceUnitIdExp)
        {
            Assert.AreEqual(serviceUnitIdExp, GetServiceUnitId());
            return this;
        }
        [AllureStep]        public ServiceUnitDetailPage VerifyServiceUnitDetailTab(ServiceUnitDBModel serviceUnitDBModel, ServiceUnitTypeDBModel serviceUnitTypeDBModel, PointSegmentDBModel pointSegmentDBModel, StreetDBModel streetDBModel)
        {
            Assert.AreEqual(serviceUnitDBModel.serviceunit, GetAttributeValue(serviceUnitInput, "value"));
            Assert.AreEqual(serviceUnitTypeDBModel.serviceunittype, GetFirstSelectedItemInDropdown(serviceUnitTypeDd));
            //client ref
            if(serviceUnitDBModel.clientreference == null)
            {
                Assert.AreEqual("", GetAttributeValue(clientRefInput, "value"));
            } else
            {
                Assert.AreEqual(serviceUnitDBModel.clientreference, GetAttributeValue(clientRefInput, "value"));
            }
            //Start + end date
            Assert.IsTrue(serviceUnitDBModel.startdate.ToString("dd/MM/yyyy").Contains(GetAttributeValue(startDateInput, "value")) || serviceUnitDBModel.startdate.ToString("MM/dd/yyyy").Contains(GetAttributeValue(startDateInput, "value")));
            Assert.IsTrue(serviceUnitDBModel.endDate.ToString("dd/MM/yyyy").Contains(GetAttributeValue(endDateInput, "value")) ||serviceUnitDBModel.endDate.ToString("MM/dd/yyyy").Contains(GetAttributeValue(endDateInput, "value")));
            //Point segment
            Assert.AreEqual(pointSegmentDBModel.pointsegment, GetAttributeValue(pointSegmentInput, "value"));
            //Clocked
            if(!serviceUnitDBModel.islocked)
            {
                Assert.IsFalse(IsCheckboxChecked(lockCheckbox));
            } else
            {
                Assert.IsTrue(IsCheckboxChecked(lockCheckbox));
            }
            //Service level
            if(serviceUnitDBModel.servicelevelID.Equals(null))
            {
                Assert.AreEqual("", GetFirstSelectedItemInDropdown(serviceLevelDd));
            }
            //Street
            Assert.AreEqual(streetDBModel.street, GetAttributeValue(streetInput, "value"));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickOnServiceUnitPointsTab()
        {
            ClickOnElement(serviceUnitPointsTab);
            return this;
        }

        public const string allServiceUnitPointRow = "//tbody[@data-bind='foreach: fields.serviceUnitPoints.value']/tr";
        public const string serviceUnitPointRow = "//tbody/tr[{0}]//td[@data-bind='text: id.value']";
        public const string pointIdRow = "//tr[{0}]//td[@data-bind='text: pointId.value']";
        public const string descRow = "//tr[{0}]//a[contains(@data-bind, 'text: point.value')]";
        public const string typeRow = "//tr[{0}]//select[@name='serviceUnitPointType']";
        public const string qualifierRow = "//tr[{0}]//select[@name='serviceUnitPointQualifier']";
        public const string startDateRow = "//tr[{0}]//input[@name='startDate']";
        public const string endDateRow = "//tr[{0}]//input[@name='endDate']";

        [AllureStep]
        public List<ServiceUnitPointModel> GetAllServiceUnitPointInTab()
        {
            List<ServiceUnitPointModel> result = new List<ServiceUnitPointModel>();
            List<IWebElement> allRows = GetAllElements(allServiceUnitPointRow);

            for (int i = 0; i < allRows.Count; i++)
            {
                string id = GetElementText(serviceUnitPointRow, (i + 1).ToString());
                string pointId = GetElementText(pointIdRow, (i + 1).ToString());
                string desc = GetElementText(descRow, (i + 1).ToString());
                string type = GetFirstSelectedItemInDropdown(string.Format(typeRow, (i + 1).ToString()));
                string qualifier = GetFirstSelectedItemInDropdown(string.Format(qualifierRow, (i + 1).ToString()));
                string startDate = GetAttributeValue(string.Format(startDateRow, (i + 1).ToString()), "value");
                string endDate = GetAttributeValue(string.Format(endDateRow, (i + 1).ToString()), "value");
                result.Add(new ServiceUnitPointModel(id, pointId, desc, type, qualifier, startDate, endDate));
            }
            return result;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyFirstServiceUnitPoint(ServiceUnitPointModel serviceUnitPointModel, string unitpointId, string desc, string type, string endDate)
        {
            Assert.AreEqual(unitpointId, serviceUnitPointModel.serviceUnitPointID);
            Assert.AreEqual(desc, serviceUnitPointModel.desc);
            Assert.AreEqual(type, serviceUnitPointModel.type);
            Assert.AreEqual(endDate, serviceUnitPointModel.endDate);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifySecondServiceUnitPoint(ServiceUnitPointModel serviceUnitPointModel, string unitpointId)
        {
            Assert.AreEqual(unitpointId, serviceUnitPointModel.serviceUnitPointID);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyServiceUnitPointAddressWithDB(List<ServiceUnitPointModel> serviceUnitPointModels, List<ServiceUnitPointDBModel> serviceUnitPointAllDataDBModel, PointAddressModel pointAddressFirstRow, string pointAddressSecondRow)
        {
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].serviceunitpointID.ToString(), serviceUnitPointModels[0].serviceUnitPointID);
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].serviceunitpointID.ToString(), serviceUnitPointModels[1].serviceUnitPointID);
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].pointID.ToString(), serviceUnitPointModels[0].pointID);
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].pointID.ToString(), serviceUnitPointModels[1].pointID);
            Assert.AreEqual(pointAddressFirstRow.Sourcedescription, serviceUnitPointModels[0].desc);
            Assert.AreEqual(pointAddressSecondRow, serviceUnitPointModels[1].desc);
            //Start date - end date
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].startDate) ||
            serviceUnitPointAllDataDBModel[0].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].startDate) ||
            serviceUnitPointAllDataDBModel[1].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].endDate) ||
            serviceUnitPointAllDataDBModel[0].enddate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].endDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].endDate) ||
            serviceUnitPointAllDataDBModel[1].enddate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].endDate));

            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyServiceUnitPointSegmentWithDB(List<ServiceUnitPointModel> serviceUnitPointModels, List<ServiceUnitPointDBModel> serviceUnitPointAllDataDBModel, PointSegmentDBModel pointSegmentDBModelFirstRow, string addressSecondRow)
        {
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].serviceunitpointID.ToString(), serviceUnitPointModels[0].serviceUnitPointID, "Wrong serviceUnitPointID");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].serviceunitpointID.ToString(), serviceUnitPointModels[1].serviceUnitPointID, "Wrong serviceUnitPointID");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].pointID.ToString(), serviceUnitPointModels[0].pointID, "Wrong pointId");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].pointID.ToString(), serviceUnitPointModels[1].pointID, "Wrong pointId");
            Assert.AreEqual(pointSegmentDBModelFirstRow.pointsegment, serviceUnitPointModels[0].desc, "Wrong desc");
            Assert.AreEqual(addressSecondRow, serviceUnitPointModels[1].desc, "Wrong desc");
            //Start date - end date
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].startDate) ||
            serviceUnitPointAllDataDBModel[0].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].startDate) ||
            serviceUnitPointAllDataDBModel[1].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].endDate) ||
            serviceUnitPointAllDataDBModel[0].enddate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].endDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].endDate) ||
            serviceUnitPointAllDataDBModel[1].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].endDate));

            return this;
        }

        [AllureStep]
        public ServiceUnitDetailPage VerifyServiceUnitPointAreaWithDB(List<ServiceUnitPointModel> serviceUnitPointModels, List<ServiceUnitPointDBModel> serviceUnitPointAllDataDBModel, PointAreaDBModel pointAreaDBModel, string addressSecondRow)
        {
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].serviceunitpointID.ToString(), serviceUnitPointModels[0].serviceUnitPointID, "Wrong serviceUnitPointID");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].serviceunitpointID.ToString(), serviceUnitPointModels[1].serviceUnitPointID, "Wrong serviceUnitPointID");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].pointID.ToString(), serviceUnitPointModels[0].pointID, "Wrong pointId");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].pointID.ToString(), serviceUnitPointModels[1].pointID, "Wrong pointId");
            Assert.AreEqual(pointAreaDBModel.areaname, serviceUnitPointModels[0].desc, "Wrong desc");
            Assert.AreEqual(addressSecondRow, serviceUnitPointModels[1].desc, "Wrong desc");
            //Start date - end date
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].startDate) ||
            serviceUnitPointAllDataDBModel[0].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].startDate) ||
            serviceUnitPointAllDataDBModel[1].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].endDate) ||
            serviceUnitPointAllDataDBModel[0].enddate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].endDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].endDate) ||
            serviceUnitPointAllDataDBModel[1].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].endDate));

            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyServiceUnitPointNodeWithDB(List<ServiceUnitPointModel> serviceUnitPointModels, List<ServiceUnitPointDBModel> serviceUnitPointAllDataDBModel, PointNodeDBModel pointNodeDBModel, string addressSecondRow)
        {
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].serviceunitpointID.ToString(), serviceUnitPointModels[0].serviceUnitPointID, "Wrong serviceUnitPointID");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].serviceunitpointID.ToString(), serviceUnitPointModels[1].serviceUnitPointID, "Wrong serviceUnitPointID");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[0].pointID.ToString(), serviceUnitPointModels[0].pointID, "Wrong pointId");
            Assert.AreEqual(serviceUnitPointAllDataDBModel[1].pointID.ToString(), serviceUnitPointModels[1].pointID, "Wrong pointId");
            Assert.AreEqual(pointNodeDBModel.pointnode, serviceUnitPointModels[0].desc, "Wrong desc");
            Assert.AreEqual(addressSecondRow, serviceUnitPointModels[1].desc, "Wrong desc");
            //Start date - end date
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].startDate) ||
            serviceUnitPointAllDataDBModel[0].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].startdate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].startDate) ||
            serviceUnitPointAllDataDBModel[1].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].startDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[0].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[0].endDate) ||
            serviceUnitPointAllDataDBModel[0].enddate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[0].endDate));
            Assert.IsTrue(serviceUnitPointAllDataDBModel[1].enddate.ToString("dd/MM/yyyy").Contains(serviceUnitPointModels[1].endDate) ||
            serviceUnitPointAllDataDBModel[1].startdate.ToString("MM/dd/yyyy").Contains(serviceUnitPointModels[1].endDate));

            return this;
        }

        private readonly By inpsectionCreatedLink = By.XPath("//a[text()='Inspection Created']/parent::div/parent::div");
        [AllureStep]
        public ServiceUnitDetailPage VerifyBackGroundColorInspectionLink()
        {
            Assert.AreEqual("rgba(64, 159, 90, 1)", GetCssValue(inpsectionCreatedLink, "color"));
            Assert.AreEqual("rgba(248, 254, 241, 1)", GetCssValue(inpsectionCreatedLink, "background-color"));
            Assert.IsTrue(GetCssValue(inpsectionCreatedLink, "border").Contains("rgb(225, 247, 201)"));
            return this;
        }
    }
}
