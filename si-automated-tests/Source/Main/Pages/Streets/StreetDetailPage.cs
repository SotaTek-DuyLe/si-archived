using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Applications.RiskRegister;

namespace si_automated_tests.Source.Main.Pages.Streets
{
    public class StreetDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Street']");
        private readonly By saveBtn = By.CssSelector("button[title='Save']");
        private readonly By saveAndCloseBtn = By.CssSelector("button[title='Save and Close']");
        private readonly By closeBtn = By.CssSelector("button[title='Close Without Saving']");
        private readonly By refreshBtn = By.CssSelector("button[title='Refresh']");
        private readonly By historyBtn = By.CssSelector("button[title='History']");
        private readonly By helpBtn = By.CssSelector("button[title='Help']");
        private readonly By icon = By.XPath("//div[contains(@class, 'echo-header')]/img");
        private readonly By streetId = By.XPath("//h4[contains(@data-bind, 'text: displayedIdType()')]");

        //TAB
        private readonly By detailsTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");
        private readonly By postCodeOutwardsTab = By.CssSelector("a[aria-controls='postCodeOutwards-tab']");
        private readonly By sectorsTab = By.CssSelector("a[aria-controls='sectors-tab']");
        private readonly By mapTab = By.CssSelector("a[aria-controls='map-tab']");
        private readonly By risksTab = By.CssSelector("a[aria-controls='risks-tab']");

        //DETAILS TAB
        private readonly By streetTypeDd = By.CssSelector("select[id='streetType.id']");
        private readonly By usrnInput = By.CssSelector("input[name='USRN']");
        private readonly By streetNameInput = By.CssSelector("input[name='streetName']");
        private readonly By roadTypeDd = By.CssSelector("select[id='roadType.id']");
        private readonly By roadTypeOptions = By.XPath("//select[@id='roadType.id']/option");

        //DATA TAB
        private readonly By accessPointInput = By.XPath("//label[text()='Access Point']/following-sibling::input");

        //SECTORS TAB
        private readonly By sectorRows = By.XPath("//div[@id='sectors-tab']//tbody/tr");

        //POST CODE OUTWARDS TAB
        private readonly By postCodeOutwardsValue = By.XPath("//tbody[contains(@data-bind, 'foreach: postcodeOutwards()')]/tr");
        private readonly By totalValue = By.XPath("//div[contains(text(), 'Total')]");

        //MAP TAB
        private readonly By segmentText = By.XPath("//td[text()='Segment']/following-sibling::td");

        //RISKS TAB
        private readonly By riskIframe = By.XPath("//div[@id='risks-tab']/iframe");
        private readonly By numberOfRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div");
        private readonly By idRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='ID']/parent::div/preceding-sibling::div) + 1]");
        private readonly By riskNameRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='Risk']/parent::div/preceding-sibling::div) + 1]");
        private readonly By riskLevelRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='Risk Level']/parent::div/preceding-sibling::div) + 1]");
        private readonly By riskTypeRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='Risk Type']/parent::div/preceding-sibling::div) + 1]");
        private readonly By contractRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='Contract']/parent::div/preceding-sibling::div) + 1]");
        private readonly By serviceRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='Services']/parent::div/preceding-sibling::div) + 1]");
        private readonly By targetRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='Target']/parent::div/preceding-sibling::div) + 1]");
        private readonly By startDateRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='Start Date']/parent::div/preceding-sibling::div) + 1]");
        private readonly By endDateRows = By.XPath("//div[@id='risk-grid']//div[@class='grid-canvas']/div/div[count(//span[text()='End Date']/parent::div/preceding-sibling::div) + 1]");
        private readonly By firstCheckboxRiskRow = By.XPath("(//div[@class='grid-canvas']/div/div[1])[1]");
        private readonly By selectedRow = By.XPath("//div[@id='risk-grid']//div[contains(@class, 'selected')]/parent::div");

        //DYNAMIC
        private const string streetName = "//h5[text()='{0}']";
        private const string streetTypeOption = "//select[@id='streetType.id']/option[text()='{0}']";
        private const string roadTypeOption = "//select[@id='roadType.id']/option[text()='{0}']";
        private const string anySectorRow = "//div[@id='sectors-tab']//td[text()='{0}']";

        #region Risk
        public readonly By RiskTab = By.XPath("//a[@aria-controls='risks-tab']");
        public readonly By RiskIframe = By.XPath("//div[@id='risks-tab']//iframe");
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
        public StreetDetailPage VerifyRiskSelect(string riskName, string startdate, string endDate)
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
        public StreetDetailPage IsStreetDetailPage(string streetNameValue)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(title), "Title [Street] is not displayed");
            Assert.IsTrue(IsControlDisplayed(streetName, streetNameValue), "Street name " + streetNameValue + " is not displayed");
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyTopBarActionDisplayed()
        {
            Assert.IsTrue(IsControlDisplayed(saveBtn), "Save button is not displayed");
            Assert.IsTrue(IsControlDisplayed(saveAndCloseBtn), "Save and Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(refreshBtn), "Refresh button is not displayed");
            Assert.IsTrue(IsControlDisplayed(historyBtn), "History button is not displayed");
            Assert.IsTrue(IsControlDisplayed(helpBtn), "Help button is not displayed");
            //Disabled
            Assert.AreEqual("true", GetAttributeValue(saveBtn, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(saveAndCloseBtn, "disabled"));
            return this;
        }
        [AllureStep]
        public StreetDetailPage ClicHistoryBtnAndVerify(string streetId)
        {
            ClickOnElement(historyBtn);
            SwitchToLastWindow();
            WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<HistoryStreetPage>()
                .IsHistoryStreetPage()
                .VerifyCurrentUrlHistoryStreet(streetId)
                .CloseCurrentWindow()
                .SwitchToChildWindow(3);
            return this;
        }
        [AllureStep]
        public StreetDetailPage ClickAndVerifyHelp()
        {
            ClickOnElement(helpBtn);
            SwitchToLastWindow();
            WaitForLoadingIconToDisappear();
            Assert.AreEqual(WebUrl.MainPageUrl + "web/help", GetCurrentUrl());
            CloseCurrentWindow();
            SwitchToChildWindow(3);
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyObjectHeader(string urlIconValue, string streetIdValue)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + urlIconValue, GetAttributeValue(icon, "src"));
            Assert.AreEqual(streetIdValue, GetElementText(streetId));
            return this;
        }
        [AllureStep]
        public StreetDetailPage ClickOnPostCodeOutwardsTab()
        {
            ClickOnElement(postCodeOutwardsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyPostCodeOutwardsTabIsSelected()
        {
            Assert.AreEqual("true", GetAttributeValue(postCodeOutwardsTab, "aria-expanded"));
            return this;
        }

        //DETAILS TAB
        [AllureStep]
        public StreetDetailPage ClickOnDetailTab()
        {
            ClickOnElement(detailsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyFieldInDetailTab()
        {
            WaitUtil.WaitForElementVisible(streetTypeDd);
            Assert.IsTrue(IsControlDisplayed(streetTypeDd), "Street Type dd is not displayed");
            Assert.IsTrue(IsControlDisplayed(usrnInput), "USRN input is not displayed");
            Assert.IsTrue(IsControlDisplayed(streetNameInput), "Street Name input is not displayed");
            Assert.IsTrue(IsControlDisplayed(roadTypeDd), "Road Type dd is not displayed");
            
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyDefaultValueInRoadType(string roadTypeValue)
        {
            Assert.AreEqual(roadTypeValue, GetFirstSelectedItemInDropdown(roadTypeDd));
            return this;
        }
        [AllureStep]
        public StreetDetailPage ClickStreetTypeDdAndVerify(List<StreetTypeDBModel> streetTypeDBModels)
        {
            ClickOnElement(streetTypeDd);
            foreach(StreetTypeDBModel type in streetTypeDBModels)
            {
                Assert.IsTrue(IsControlDisplayed(streetTypeOption, type.streettype), type.streettype + " is not displayed");
            }
            return this;
        }
        [AllureStep]
        public StreetDetailPage SelectStreetType(string streetTypeValue)
        {
            ClickOnElement(streetTypeOption, streetTypeValue);
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyDefaultValueInStreetName(string streetNameValue)
        {
            Assert.AreEqual(streetNameValue, GetAttributeValue(streetNameInput, "value"));
            return this;
        }
        [AllureStep]
        public StreetDetailPage ClickRoadTypeDdAndVerify(List<RoadTypeDBModel> roadTypeDBModels)
        {
            ClickOnElement(roadTypeDd);
            foreach (RoadTypeDBModel type in roadTypeDBModels)
            {
                Assert.IsTrue(IsControlDisplayed(roadTypeOption, type.roadtype), type.roadtype + " is not displayed");
            }
            return this;
        }
        [AllureStep]
        private List<string> GetAllRoadTypeOptionInList()
        {
            List<string> allTypes = new List<string>();
            List<IWebElement> allOptions = GetAllElements(roadTypeOptions);
            for(int i = 0; i < allOptions.Count; i++)
            {
                allTypes.Add(GetElementText(allOptions[i]));
            }
            return allTypes;
        }
        [AllureStep]
        public StreetDetailPage VerifyRoadTypeOptionDisplayOrderAlphabet()
        {
            List<string> allRoadTypeValue = GetAllRoadTypeOptionInList();

            //Sort all road type name
            List<string> newRoadType = allRoadTypeValue.OrderBy(x => x).ToList();
            //Verify
            CollectionAssert.AreEqual(newRoadType, allRoadTypeValue);
            return this;
        }
        [AllureStep]
        public StreetDetailPage ClearTextInStreetName()
        {
            ClearInputValue(streetNameInput);
            return this;
        }
        [AllureStep]
        public StreetDetailPage InputTextInStreetName(string streetNameValue)
        {
            SendKeys(streetNameInput, streetNameValue);
            return this;
        }

        //DATA TAB
        [AllureStep]
        public StreetDetailPage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyDataTabAfterSelectStreetType()
        {
            WaitUtil.WaitForElementVisible(accessPointInput);
            Assert.IsTrue(IsControlDisplayed(accessPointInput), "Access Point is not displayed");
            return this;
        }
        [AllureStep]
        public StreetDetailPage SendKeyInAccessPoint(string accessPointValue)
        {
            SendKeys(accessPointInput, accessPointValue);
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyAccessPointAfterSaveForm(string accessPointValue)
        {
            Assert.AreEqual(accessPointValue, GetAttributeValue(accessPointInput, "value"));
            return this;
        }

        //Post Code Outwards tab
        [AllureStep]
        public StreetDetailPage ClickOnPostCodeOutwards()
        {
            ClickOnElement(postCodeOutwardsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyDataInPostCodeOutwardsTab(string valueInDb, string totalInDb)
        {
            Assert.AreEqual(valueInDb, GetElementText(postCodeOutwardsValue));
            Assert.AreEqual("Total = " + totalInDb, GetElementText(totalValue));
            return this;
        }

        //Sectors tab
        [AllureStep]
        public StreetDetailPage ClickOnSectorsTab()
        {
            ClickOnElement(sectorsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifyDataInSectorTab(List<SectorDBModel> sectorDBModels)
        {
            for(int i = 0; i < sectorDBModels.Count; i++)
            {
                Assert.IsTrue(IsControlDisplayed(anySectorRow, sectorDBModels[i].sector), "Sector " + sectorDBModels[i].sector + " is not displayed");
                Console.WriteLine(string.Format(anySectorRow, sectorDBModels[i].sector));
                Console.WriteLine(IsControlEnabled(string.Format(anySectorRow, sectorDBModels[i].sector)).ToString());
                //Assert.IsFalse(IsControlEnabled(string.Format(anySectorRow, sectorDBModels[i].sector)));
            }
            return this;
        }

        //Map tab
        [AllureStep]
        public StreetDetailPage ClickOnMapTab()
        {
            ClickOnElement(mapTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public StreetDetailPage VerifySegmentValue(string segmentInDb)
        {
            Assert.AreEqual(segmentInDb, GetElementText(segmentText));
            return this;
        }

        //Risks tab

        [AllureStep]
        public StreetDetailPage ClickOnRisksTab()
        {
            ClickOnElement(risksTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public List<RiskModel> GetAllRiskInTab()
        {
            SwitchNewIFrame(riskIframe);
            WaitForLoadingIconToDisappear();
            List<RiskModel> riskModels = new List<RiskModel>();
            List<IWebElement> allRows = GetAllElements(numberOfRows);
            List<IWebElement> allIds = GetAllElements(idRows);
            List<IWebElement> allRiskName = GetAllElements(riskNameRows);
            List<IWebElement> allRiskLevel = GetAllElements(riskLevelRows);
            List<IWebElement> allRiskType = GetAllElements(riskTypeRows);
            List<IWebElement> allContract = GetAllElements(contractRows);
            List<IWebElement> allService = GetAllElements(serviceRows);
            List<IWebElement> allTarget = GetAllElements(targetRows);
            List<IWebElement> allStartDate = GetAllElements(startDateRows);
            List<IWebElement> allEndDate = GetAllElements(endDateRows);
            for (int i = 0; i < allRows.Count; i++)
            {
                string id = GetElementText(allIds[i]);
                string riskName = GetElementText(allRiskName[i]);
                string riskLevel = GetElementText(allRiskLevel[i]);
                string riskType = GetElementText(allRiskType[i]);
                string contract = GetElementText(allContract[i]);
                string service = GetElementText(allService[i]);
                string target = GetElementText(allTarget[i]);
                string startDate = GetElementText(allStartDate[i]);
                string endDate = GetElementText(allEndDate[i]);
                riskModels.Add(new RiskModel(id, riskName, riskLevel, riskType, contract, service, target, startDate, endDate));
            }
            return riskModels;
        }
        [AllureStep]
        public StreetDetailPage VerifyRisksWithContractAndName(List<RiskModel> riskModelsDisplayed, string[] contractName, string[] riskName) 
        {
            for(int i = 0; i < riskModelsDisplayed.Count; i++)
            {
                Assert.AreEqual(contractName[i], riskModelsDisplayed[i].contract);
                Assert.AreEqual(riskName[i], riskModelsDisplayed[i].riskName);
            }
            return this;
        }
        [AllureStep]
        public RiskDetailPage ClickAnyRiskRowShowRiskDetailPage()
        {
            ClickOnElement(firstCheckboxRiskRow);
            SleepTimeInMiliseconds(1000);
            //Click
            DoubleClickOnElement(selectedRow);
            return PageFactoryManager.Get<RiskDetailPage>();

        }
    }
}
