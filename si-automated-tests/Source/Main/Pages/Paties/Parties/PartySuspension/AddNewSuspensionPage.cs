using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension
{
    public class AddNewSuspensionPage : BasePage
    {
        private readonly By modal = By.XPath("//div[@id='add-service-suspensions']");
        private readonly string titleXpath = "//div[@id='add-service-suspensions']//h4";
        private readonly By serviceSuspensionModal = By.XPath("//div[@class='modal-content service-suspension-content']");
        private readonly By siteItems = By.XPath("//div[@id='step-1']//li");
        private readonly By serviceItems = By.XPath("//div[@id='step-2']//li");
        private readonly By nextBtn = By.XPath("//div[@id='add-service-suspensions']//div[@class='modal-footer']//button[text()='Next']");
        private readonly By selectAllSiteCheckbox = By.XPath("//div[@id='step-1']//span[text()='Select All']/preceding-sibling::input[@type='checkbox']");
        private readonly By selectAllServiceCheckbox = By.XPath("//div[@id='step-2']//span[text()='Select All']/preceding-sibling::input[@type='checkbox']");
        private readonly By fromDateInput = By.XPath("//input[@id='fromDateField.id']");
        private readonly By untilDateInput = By.XPath("//input[@id='untilDateField.id']");
        private readonly By dateDiffLabel = By.XPath("//div[@id='step-3']//label[text()='0 days']");
        private readonly By everydayRadio = By.XPath("//div[@id='step-3']//span[text()='Everyday']/preceding-sibling::input[@type='radio']");
        private readonly By selectedDayRadio = By.XPath("//div[@id='step-3']//span[text()='Selected Days']/preceding-sibling::input[@type='radio'][1]");
        private readonly By finishBtn = By.XPath("//div[@id='add-service-suspensions']//div[@class='modal-footer']//button[text()='Finish']");
        private const string AnyMessage = "//div[text()='{0}']";

        private string SuspensionTable = "//div[@id='suspensions-grid']//div[@class='grid-canvas']";
        private string SuspensionRow = "./div[contains(@class, 'slick-row')]";
        private string SiteCell = "./div[contains(@class, 'l0')]";
        private string ServiceCell = "./div[contains(@class, 'l1')]";
        private string FromCell = "./div[contains(@class, 'l2')]";
        private string ToCell = "./div[contains(@class, 'l3')]";
        private string SuspendsDay = "./div[contains(@class, 'l4')]";
        private string DeleteButtonCell = "./div[contains(@class, 'l5')]//button"; 

        public TableElement SuspensionTableEle
        {
            get => new TableElement(SuspensionTable, SuspensionRow, new List<string>() { SiteCell, ServiceCell, FromCell, ToCell, SuspendsDay, DeleteButtonCell });
        }

        [AllureStep]
        public AddNewSuspensionPage ClickDeleteSuspension(int idx)
        {
            SuspensionTableEle.ClickCell(idx, 5);
            return this;
        }

        [AllureStep]
        public AddNewSuspensionPage ClickDeleteNewSuspension()
        {
            int newIdx = SuspensionTableEle.GetRows().Count - 1;
            SuspensionTableEle.ClickCell(newIdx, 5);
            return this;
        }

        [AllureStep]
        public AddNewSuspensionPage WaitServiceSuspensionVisible()
        {
            WaitUtil.WaitForElementVisible(serviceSuspensionModal);
            return this;
        }
        [AllureStep]
        public List<string> GetSiteNames()
        {
            List<IWebElement> checkboxs = GetAllElements(siteItems);
            return checkboxs.Select(x => GetElementText(x)).ToList();
        }
        [AllureStep]
        public List<string> GetServiceNames()
        {
            List<IWebElement> checkboxs = GetAllElements(serviceItems);
            return checkboxs.Select(x => GetElementText(x)).ToList();
        }

        [AllureStep]
        public AddNewSuspensionPage VerifyServiceNames()
        {
            Assert.That(GetServiceNames(), Is.EquivalentTo(new List<string>() { "Waste", "Recycling", "Ancillary", "Streets" }));
            return this;
        }

        [AllureStep]
        public AddNewSuspensionPage VerifySuspensionTitle(string title)
        {
            List<IWebElement> titles = GetAllElements(titleXpath);
            string viewTitle = string.Join(" ", titles.Select(x => GetElementText(x)).ToArray());
            Assert.IsTrue(viewTitle == title);
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage VerifyNextButtonIsDisable()
        {
            Assert.IsFalse(IsControlEnabled(nextBtn));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage ClickSelectAllSiteCheckbox()
        {
            ClickOnElement(selectAllSiteCheckbox);
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage ClickSelectAllServiceCheckbox()
        {
            ClickOnElement(selectAllServiceCheckbox);
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage VerifyNextButtonIsEnable()
        {
            Assert.IsTrue(IsControlEnabled(nextBtn));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage ClickNextButton()
        {
            ClickOnElement(nextBtn);
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage IsFirstDayInputVisible()
        {
            Assert.IsTrue(IsControlDisplayed(fromDateInput));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage IsLastDayInputVisible()
        {
            Assert.IsTrue(IsControlDisplayed(untilDateInput));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage IsDateDiffLabelVisible()
        {
            Assert.IsTrue(IsControlDisplayed(dateDiffLabel));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage IsEveryDayRadioVisible()
        {
            Assert.IsTrue(IsControlDisplayed(everydayRadio));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage IsSelectedDayRadioVisible()
        {
            Assert.IsTrue(IsControlDisplayed(selectedDayRadio));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage ClickFinish()
        {
            ClickOnElement(finishBtn);
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage VerifyWarningMessage(string errorMessage)
        {
            Assert.IsTrue(IsControlDisplayed(String.Format(AnyMessage, errorMessage)));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage InputDaysAndVerifyDaysCalculationLbl(string fromDate, string lastDay, string diffDate = "30 days")
        {
            IWebElement dateDiffEle = GetElement(dateDiffLabel);
            IWebElement fromDateInputEle = GetElement(fromDateInput);
            SendKeys(fromDateInputEle, fromDate);
            IWebElement untilDateInputEle = GetElement(untilDateInput);
            SendKeys(untilDateInputEle, lastDay);
            ClickOnElement(everydayRadio);
            Thread.Sleep(200);
            Assert.IsTrue(GetElementText(dateDiffEle) == diffDate);
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage VerifySaveMessage(string saveMsg)
        {
            Assert.IsTrue(IsControlDisplayed(String.Format(AnyMessage, saveMsg)));
            return this;
        }
        [AllureStep]
        public AddNewSuspensionPage IsAddSuspensionModalInVisible()
        {
            WaitUtil.WaitForElementInvisible(modal);
            Assert.IsTrue(IsControlUnDisplayed(modal));
            return this;
        }
    }
}
