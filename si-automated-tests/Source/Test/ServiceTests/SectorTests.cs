﻿using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class SectorTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO POINT ADDRESS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser27.UserName, AutoUser27.Password)
                .IsOnHomePage(AutoUser27);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Richmond Commercial");
        }
        [Category("PointNode")]
        [Category("Dee")]
        [Test]
        public void TC_103_Create_point_node()
        {
            string _des = "The Quadrant Richmond";
            string _lat = "-51.462496441865326";
            string _long = "-0.30280400159488435";
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Point Nodes")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PointNodeDetailPage>()
                .InputPointNodeDetails(_des, _lat, _long)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Point Node")
                .WaitUntilToastMessageInvisible("Successfully saved Point Node")
                .GoToAllTabAndConfirmNoError();
        }
        [Category("PointArea")]
        [Category("Dee")]
        [Test]
        public void TC_104_Create_point_area()
        {
            string name = "Test Area";
            string latLong = "51.4312054715491,-0.322336068148961 51.4316068058406,-0.32111298083817 51.4320482694892,-0.320619454379429 51.4325031063625,-0.319632401461949 51.4316603168131,-0.318409314151158 51.4311920936787,-0.316950192447056 51.4307104877343,-0.317036023135533 51.4293880322037,-0.313577171528743 51.4286003417574,-0.314161820693024 51.4298275303176,-0.317422261233677 51.4305499512914,-0.320319046969761";
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Point Areas")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PointAreaDetailPage>()
                .InputAreaName(name)
                .InputLatLong(latLong)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Point Area")
                .WaitUntilToastMessageInvisible("Successfully saved Point Area")
                .GoToAllTabAndConfirmNoError();
        }
        [Category("PointArea")]
        [Category("Dee")]
        [Test]
        public void TC_107_Create_announcement()
        {
            //VERIFY ON CONTRACTS
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("North Star Commercial")
                .SwitchNewIFrame()
                .SwitchToTab("Announcements")
                .WaitForLoadingIconToDisappear();
            CreateAnnouncementAndVerify();
            //VERIFY ON GROUP AND SERVICES
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Ancillary")
                .OpenOption("Skips")
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame()
                .SwitchToTab("Announcements");
            CreateAnnouncementAndVerify();
            //VERIFY ON ROUND GROUPS AND ROUND
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Skips")
                .ExpandOption("Round Groups")
                .OpenOption("SKIP1")
                .SwitchNewIFrame()
                .SwitchToTab("Announcements");
            CreateAnnouncementAndVerify();
            //VERIFY ON CONTRACT UNIT
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Contract Units")
                .OpenOption("Commercial")
                .SwitchNewIFrame()
                .SwitchToTab("Announcements");
            CreateAnnouncementAndVerify();
            //VERIFY ON POINT ADDRESS
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .OpenOption("Point Addresses")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .WaitForLoadingIconToDisappear()
                .SwitchToLastWindow()
                .SwitchToTab("Announcements");
            CreateAnnouncementAndVerifyInTabSection();
            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            //VERIFY ON POINT Segments
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .OpenOption("Point Segments")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .WaitForLoadingIconToDisappear()
                .SwitchToLastWindow()
                .SwitchToTab("Announcements");
            CreateAnnouncementAndVerifyInTabSection();
            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            //VERIFY ON POINT Nodes
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .OpenOption("Point Nodes")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .WaitForLoadingIconToDisappear()
                .SwitchToLastWindow()
                .SwitchToTab("Announcements");
            CreateAnnouncementAndVerifyInTabSection();
            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            //VERIFY ON POINT Nodes
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .OpenOption("Point Areas")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .WaitForLoadingIconToDisappear()
                .SwitchToLastWindow()
                .SwitchToTab("Announcements");
            CreateAnnouncementAndVerifyInTabSection();
            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow();

        }
        private void CreateAnnouncementAndVerify()
        {
            string type = "Collection services";
            string text = "Test announcement " + CommonUtil.GetRandomNumber(5);
            string impact = "Positive";
            string from = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string to = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, 1);
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AnnouncementDetailPage>()
                .IsOnDetailPage()
                .InputDetails(type, text, impact, from, to)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Announcement")
                .WaitUntilToastMessageInvisible("Successfully saved Announcement")
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Name", text)
                .VerifyFirstResultValue("Type", type)
                .VerifyFirstResultValue("Valid From", from)
                .VerifyFirstResultValue("Valid To", to)
                .SwitchToDefaultContent();
        }
        private void CreateAnnouncementAndVerifyInTabSection()
        {
            string type = "Collection services";
            string text = "Test announcement " + CommonUtil.GetRandomNumber(5);
            string impact = "Positive";
            string from = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string to = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, 1);
            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToLastWindow();
            PageFactoryManager.Get<AnnouncementDetailPage>()
                .IsOnDetailPage()
                .InputDetails(type, text, impact, from, to)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Announcement")
                .WaitUntilToastMessageInvisible("Successfully saved Announcement")
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValueInTab("Name", text)
                .VerifyFirstResultValueInTab("Type", type)
                .VerifyFirstResultValueInTab("Valid From", from)
                .VerifyFirstResultValueInTab("Valid To", to)
                .SwitchToDefaultContent();
        }

        [Category("Sectors")]
        [Test(Description = "Verify that a new sector form is opened ")]
        public void TC_129_Verify_that_a_new_sector_form_is_opened()
        {
            SectorPage sectorPage = PageFactoryManager.Get<SectorPage>();
            sectorPage.WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();

            sectorPage.ClickOnElement(sectorPage.DetailTab);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyElementVisibility(sectorPage.InputSector, true)
                .VerifyElementVisibility(sectorPage.SelectContract, true)
                .VerifyElementVisibility(sectorPage.SelectSectorType, true)
                .VerifyElementVisibility(sectorPage.SelectParentSector, true);

            sectorPage.ClickOnElement(sectorPage.MapTab);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyElementVisibility(sectorPage.DivMap, true);

            //Verify that the top bar actions display and correctly
            sectorPage.VerifyElementVisibility(sectorPage.ButtonSave, true)
                .VerifyElementVisibility(sectorPage.ButtonRefresh, true)
                .VerifyElementVisibility(sectorPage.ButtonHistory, true)
                .VerifyElementVisibility(sectorPage.ButtonHelp, true);

            //Test the buttons functionality
            sectorPage.VerifyElementEnable(sectorPage.ButtonSave, false);
            string sector = "Richmond Commercial123";
            sectorPage.SendKeys(sectorPage.InputSector, sector);
            sectorPage.ClickOnElement(sectorPage.ButtonSave);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");

            sectorPage.ClickOnElement(sectorPage.ButtonRefresh);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyInputValue(sectorPage.InputSector, sector);

            sectorPage.ClickOnElement(sectorPage.ButtonHelp);
            sectorPage.SwitchToChildWindow(2);
            HelpPage helpPage = PageFactoryManager.Get<HelpPage>();
            helpPage.VerifyElementVisibility(helpPage.HelpTitle, true)
                .VerifyElementVisibility(helpPage.EchoWikiLink, true)
                .VerifyElementVisibility(helpPage.ButtonClose, true)
                .ClickOnElement(helpPage.ButtonClose);
            helpPage.SwitchToFirstWindow()
                .SwitchNewIFrame();

            //Object header
            sectorPage.VerifyElementText(sectorPage.TitleSectorName, sectorPage.GetInputValue(sectorPage.InputSector))
                .VerifyElementVisibility(sectorPage.SectorId, true)
                .VerifyElementVisibility(sectorPage.IconSector, true)
                .VerifyElementText(sectorPage.TitleSectorType, "Sector - " + sectorPage.GetFirstSelectedItemInDropdown(sectorPage.SelectSectorType));

            //Verify  that a last tab selected is remembered for the user
            sectorPage.ClickOnElement(sectorPage.MapTab);
            sectorPage.WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Richmond Commercial")
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            sectorPage.VerifyElementVisibility(sectorPage.DivMap, true);

            sectorPage.ClickOnElement(sectorPage.DetailTab);
            sectorPage.WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Richmond Commercial")
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            sectorPage.VerifyElementVisibility(sectorPage.InputSector, true)
                .VerifyElementVisibility(sectorPage.SelectContract, true)
                .VerifyElementVisibility(sectorPage.SelectSectorType, true)
                .VerifyElementVisibility(sectorPage.SelectParentSector, true);

            //can update
            sector = "Richmond Commercial12";
            string contract = "North Star";
            string parentSector = "Hampton Tip (West)";
            string sectorType = "Ward";
            sectorPage.SendKeys(sectorPage.InputSector, sector);
            sectorPage.SelectTextFromDropDown(sectorPage.SelectContract, contract)
                .SelectTextFromDropDown(sectorPage.SelectParentSector, parentSector)
                .SelectTextFromDropDown(sectorPage.SelectSectorType, sectorType)
                .ClickOnElement(sectorPage.ButtonSave);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");

            sectorPage.ClickOnElement(sectorPage.ButtonRefresh);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyInputValue(sectorPage.InputSector, sector)
                .VerifySelectedValue(sectorPage.SelectContract, contract)
                .VerifySelectedValue(sectorPage.SelectParentSector, parentSector)
                .VerifySelectedValue(sectorPage.SelectSectorType, sectorType);

            //Details tab
            //Verify that mandatory fields are highlighted in red and warning message is displayed
            sectorPage.SendKeys(sectorPage.InputSector, "");
            sectorPage.SendKeys(sectorPage.InputSector, Keys.Enter);
            sectorPage.VerifyElementContainCssAttributeValue(sectorPage.InputSector, "border-color", "rgb(169, 68, 66)");
            sectorPage.ClickOnElement(sectorPage.ButtonSave);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyToastMessage("Sector is required")
                .WaitUntilToastMessageInvisible("Sector is required");
            sectorPage.SendKeys(sectorPage.InputSector, sector);

            sectorPage.SelectTextFromDropDown(sectorPage.SelectSectorType, "");
            sectorPage.ClickOnElement(sectorPage.ButtonSave);
            sectorPage.WaitForLoadingIconToDisappear();
            sectorPage.VerifyToastMessage("SectorType is required")
                .WaitUntilToastMessageInvisible("SectorType is required");
            sectorPage.VerifyElementContainCssAttributeValue(sectorPage.SelectSectorType, "border-color", "rgb(169, 68, 66)");
        }
    }
}
