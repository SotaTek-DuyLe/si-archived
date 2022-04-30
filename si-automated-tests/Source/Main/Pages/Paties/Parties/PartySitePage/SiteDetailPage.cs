using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyWBTicketPage;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage
{
    public class SiteDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Serviced Site']");
        private readonly By siteName = By.XPath("//p[text()='Jaflong Tandoori / 16 ASHBURNHAM ROAD, HAM, RICHMOND, TW10 7NF']");
        private readonly By primaryContactDd = By.CssSelector("select#primary-contact");
        private readonly By primaryContactAddBtn = By.XPath("//select[@id='primary-contact']/following-sibling::span");
        private const string loadingData = "//div[@class='loading-data']";
        private const string frameMessage = "//div[@class='notifyjs-corner']/div";
        private const string allTabDisplayedNotContainsMapTab = "//li[@role='presentation']/a[not(contains(text(), 'Map'))]";
        private readonly By stationTab = By.XPath("//a[text()='Stations']");

        //STATION TAB
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");

        //LOCATION TAB
        private readonly By locationTab = By.XPath("//a[text()='Locations']");
        private readonly By allRowInTabel = By.XPath("//div[@id='weighbridgeSiteLocations-tab']//div[@class='grid-canvas']/div");
        private readonly By deleteItemLocationBtn = By.XPath("//div[@id='weighbridgeSiteLocations-tab']//button[text()='Delete Item']");
        private readonly By addNewItemLocationBtn = By.XPath("//div[@id='weighbridgeSiteLocations-tab']//button[text()='Add New Item']");
        private const string columnInRowLocations = "//div[@id='weighbridgeSiteLocations-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private const string selectAndDeSelectCheckboxLocations = "//div[@id='weighbridgeSiteLocations-tab']//div[@class='grid-canvas']/div//input[{0}]";

        //PRODUCT TAB
        private readonly By productTab = By.XPath("//a[text()='Products']");
        private readonly By addNewProductItem = By.XPath("//div[@id='weighbridgeSiteProductLocations-tab']//button[text()='Add New Item']");

        //DYNAMIC LOCATOR
        private const string allPrimaryContactValue = "//select[@id='primary-contact']/option";
        private const string primaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string primaryContactDisplayed = "//div[@data-bind='with: primaryContact']//span[text()='{0}']";
        private const string titleDetail = "//p[text()='{0}']";
        private const string nameDetail = "//h4[text()='{0}']";
        private const string siteNameDynamic = "//span[text()='{0}']";
        private const string allTabInScreen = "//ul[@role='tablist']//a[text()='{0}']";
        private const string messageAtMapTab = "//div[@class='notifyjs-corner']//div[text()='{0}']";
        private const string nameOfColumnInLocationTab = "//div[@id='weighbridgeSiteLocations-tab']//span[text()='{0}']/parent::div";
        private const string nameOfColumnInProductTab = "//div[@id='weighbridgeSiteProductLocations-tab']//span[text()='{0}']/parent::div";

        public SiteDetailPage WaitForSiteDetailPageLoaded()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(siteName);
            return this;
        }

        public SiteDetailPage WaitForSiteDetailPageLoaded(string titleA, string agreementNameA)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(string.Format(titleDetail, titleA));
            WaitUtil.WaitForElementVisible(string.Format(nameDetail, agreementNameA));
            return this;
        }

        public SiteDetailPage WaitForSiteDetailsLoaded(string titleA, string siteNameDisplayed)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(string.Format(siteNameDynamic, titleA));
            WaitUtil.WaitForElementVisible(string.Format(titleDetail, siteNameDisplayed));
            return this;
        }


        public SiteDetailPage ClickPrimaryContactDd()
        {
            ClickOnElement(primaryContactDd);
            return this;
        }

        public SiteDetailPage VerifyNumberOfContact(int numberOfContact)
        {
            Assert.AreEqual(numberOfContact, GetAllElements(allPrimaryContactValue).Count);
            return this;
        }

        public SiteDetailPage VerifyValueInPrimaryContactDd(string[] expectedOption)
        {
            foreach (String option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(primaryContactValue, option)));
            }
            return this;
        }

        public SiteDetailPage SelectAnyPrimaryContactAndVerify(ContactModel contactModel)
        {
            ClickOnElement(string.Format(primaryContactValue, contactModel.FirstName + " " + contactModel.LastName));
            //Verify
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Telephone));
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Mobile));
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Email));
            //Verify contact saved in primary contact dd
            Assert.AreEqual(GetFirstSelectedItemInDropdown(primaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }

        public AddPrimaryContactPage ClickPrimaryContactAddBtn()
        {
            ClickOnElement(primaryContactAddBtn);
            return PageFactoryManager.Get<AddPrimaryContactPage>();
        }

        public SiteDetailPage VerifyFirstValueInPrimaryContactDd(ContactModel contactModel)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(primaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }

        public SiteDetailPage VerifyDisplayAllTab(string[] expectedAllTab)
        {
            foreach(string tab in expectedAllTab)
            {
                Assert.IsTrue(IsControlDisplayed(allTabInScreen, tab));
            }
            return this;
        }

        public SiteDetailPage ClickDetailTab()
        {
            ClickOnElement(allTabInScreen, "Details");
            WaitUtil.WaitForElementInvisible(frameMessage);
            return this;
        }

        public SiteDetailPage ClickSomeTabAndVerifyNoErrorMessage()
        {
            List<IWebElement> allElements = GetAllElements(allTabDisplayedNotContainsMapTab);
            int clickButtonIdx = 0;
            while (clickButtonIdx < allElements.Count)
            {
                ClickOnElement(allElements[clickButtonIdx]);
                clickButtonIdx++;
                WaitUtil.WaitForElementInvisible(loadingData);
                Assert.IsFalse(IsControlDisplayedNotThrowEx(frameMessage));
                allElements = GetAllElements(allTabDisplayedNotContainsMapTab);
            }
            return this;
        }

        public SiteDetailPage ClickMapTabAndVerifyMessage(string message)
        {
            ClickOnElement(allTabInScreen, CommonConstants.MapTab);
            Assert.IsTrue(IsControlDisplayed(string.Format(messageAtMapTab, message)));
            return this;
        }

        public SiteDetailPage ClickStationTab()
        {
            ClickOnElement(stationTab);
            return this;
        }

        //STATION TAB
        public CreateStationPage ClickAddNewStationItem()
        {
            ClickOnElement(addNewItemBtn);
            return PageFactoryManager.Get< CreateStationPage>();
        }

        //LOCATION TAB
        public SiteDetailPage VerifyDisplayColumnInGrid()
        {
            foreach(string column in CommonConstants.LocationTabColumn)
            {
                WaitUtil.WaitForElementVisible(nameOfColumnInLocationTab, column);
                Assert.IsTrue(IsControlDisplayed(nameOfColumnInLocationTab, column));
            }
            return this;
        }

        public SiteDetailPage ClickOnLocationTab()
        {
            ClickOnElement(locationTab);
            return this;
        }

        public AddLocationPage ClickAddNewLocationItem()
        {
            ClickOnElement(addNewItemLocationBtn);
            return PageFactoryManager.Get<AddLocationPage>();
        }

        public List<LocationModel> GetAllLocationInGrid()
        {
            WaitUtil.WaitForElementVisible(addNewItemLocationBtn);
            List<LocationModel> allModel = new List<LocationModel>();
            List<IWebElement> allRow = GetAllElements(allRowInTabel);
            List<IWebElement> allIdSite = GetAllElements(string.Format(columnInRowLocations, CommonConstants.LocationTabColumn[0]));
            List<IWebElement> allLocation = GetAllElements(string.Format(columnInRowLocations, CommonConstants.LocationTabColumn[1]));
            List<IWebElement> allActive = GetAllElements(string.Format(columnInRowLocations, CommonConstants.LocationTabColumn[2]));
            List<IWebElement> allClient = GetAllElements(string.Format(columnInRowLocations, CommonConstants.LocationTabColumn[3]));
            for(int i = 0; i < allRow.Count; i++)
            {
                string selectAndDeselectLocator = string.Format(selectAndDeSelectCheckboxLocations, (i + 1));
                string id = GetElementText(allIdSite[i]);
                string location = GetElementText(allLocation[i]);
                string active = GetElementText(allActive[i]);
                string client = GetElementText(allClient[i]);
                allModel.Add(new LocationModel(selectAndDeselectLocator, id, location, active, client));
            }
            return allModel;
        }

        public SiteDetailPage VerifyLocationCreated(LocationModel locationModel, string locationName, bool active, string client)
        {
            Assert.AreEqual(locationName, locationModel.Location);
            if(active)
            {
                Assert.AreEqual("✓", locationModel.Active);
            }
            else
            {
                Assert.AreEqual("✗", locationModel.Active);
            }
            Assert.AreEqual(client, locationModel.Client);
            return this;
        }

        public SiteDetailPage ClickAnySelectAndDeSelectLocatorRow(string locatorCheckbox)
        {
            ClickOnElement(locatorCheckbox);
            return this;
        }

        public SiteDetailPage ClickFirstSelectAndDeSelectLocatorRow()
        {
            ClickOnElement(string.Format(selectAndDeSelectCheckboxLocations, 1));
            return this;
        }

        public DeleteWBLocation ClickDeleteItemInLocationTabBtn()
        {
            ClickOnElement(deleteItemLocationBtn);
            return PageFactoryManager.Get<DeleteWBLocation>();
        }

        //PRODUCT TAB
        public SiteDetailPage ClickProductTab()
        {
            ClickOnElement(productTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public SiteDetailPage VerifyDisplayColumnInProductTabGrid()
        {
            foreach(string column in CommonConstants.ProductTabColumn)
            {
                Assert.IsTrue(IsControlDisplayed(nameOfColumnInProductTab, column));
            }
            return this;
        }

        public AddProductPage ClickAddNewProductItem()
        {
            ClickOnElement(addNewProductItem);
            return PageFactoryManager.Get< AddProductPage>();
        }
    }
}
