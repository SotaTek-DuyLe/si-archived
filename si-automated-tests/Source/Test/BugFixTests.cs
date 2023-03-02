﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.IE_Configuration;
using si_automated_tests.Source.Main.Pages.Inspections;
using si_automated_tests.Source.Main.Pages.Maps;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccountStatement;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties.Sites;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;
using si_automated_tests.Source.Main.Pages.WB;
using si_automated_tests.Source.Main.Pages.WB.Tickets;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using ServiceUnitDetailPage = si_automated_tests.Source.Main.Pages.Services.ServiceUnitDetailPage;
using ServiceUnitPage = si_automated_tests.Source.Main.Pages.Services.ServiceUnitPage;

namespace si_automated_tests.Source.Test
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Author("Dee", "duy.le@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BugFixTests : BaseTest
    {
        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "Asset form - data and trail tab (bug fix)")]
        public void TC_162_Asset_form_data_and_trail_tab()
        {
            string assetTypeValue = "1100L";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46)
                //Open link [web/service-units/223643]
                .GoToURL(WebUrl.MainPageUrl + "/web/service-units/223643");
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForLoadingIconToDisappear();
            string serviceUnitName = serviceUnitDetailPage
                .GetServiceUnitName();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                //Line 8: Click on [Asset] tab
                .ClickOnAssetTab()
                .ClickOnAddNewItemBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            AssetDetailItemPage assetDetailItemPage = PageFactoryManager.Get<AssetDetailItemPage>();
            assetDetailItemPage
                .IsOnPage()
                //Line 9: Click on [Data] tab
                .ClickOnDataTab()
                .VerifyNotDisplayMessageAttributeConfig(MessageRequiredFieldConstants.WhoopsSomethingIsWrongAttributesConfig)
                //Line 10: Click on [Detail] tab
                .ClickOnDetailTab()
                .ClickAndSelectAssetType(assetTypeValue)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            assetDetailItemPage
                .VerifyCurrentUrl()
                //Line 11: Click on [Trail] tab
                .ClickOnTrailTab()
                .VerifyServiceUnitAtFirstRowTrailTab(serviceUnitName)
                .ClickOnServiceUnitLinkAtFirstRowTrailTab()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .VerifyCurrentUrl("223643")
                .VerifyToastMessagesIsUnDisplayed();

        }

        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "Asset - Trail - opens incorrect service unit (bug fix)")]
        public void TC_168_Asset_trail_opens_incorrect_Service_unit()
        {
            string serviceUnitId = "230016";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOptionLast("Commercial Collections")
                .OpenOption("Active Service Units")
                .SwitchNewIFrame();
            ServiceUnitPage serviceUnitPage = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnitPage
                .FindServiceUnitWithId(serviceUnitId)
                .DoubleClickServiceUnit()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage();

            string serviceUnitName = serviceUnitDetailPage.GetServiceUnitName();
            serviceUnitDetailPage
                .ClickOnAssetTab()
                .DoubleClickOnFirstRowAtAssetTab()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            AssetDetailItemPage assetDetailItemPage = PageFactoryManager.Get<AssetDetailItemPage>();
            assetDetailItemPage
                .IsOnPage()
                .ClickOnTrailTab()
                .VerifyServiceUnitAtFirstRowTrailTab(serviceUnitName)
                .ClickOnServiceUnitLinkAtFirstRowTrailTab()
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .VerifyCurrentUrl(serviceUnitId)
                .VerifyToastMessagesIsUnDisplayed();
        }

        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "Loading of tasks without partyID is failing (bug fix)")]
        public void TC_167_Loading_of_tasks_without_partyId_is_failing()
        {
            string taskIdRM = "19099";
            string taskIdRMC = "19657";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame();
            //Double click on row where [Party] column is blank [RM]
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId(taskIdRM)
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyCurrentUrl(taskIdRM)
                .VerifyToastMessagesIsUnDisplayed()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Doube click on row where [Party] column is populated [RMC]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
               .WaitForTaskListinPageDisplayed()
               .FilterByTaskId(taskIdRMC)
               .ClickOnFirstRecord()
               .SwitchToChildWindow(2)
               .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyCurrentUrl(taskIdRMC)
                .VerifyToastMessagesIsUnDisplayed();
        }

        [Category("Bug fix")]
        [Category("Dee")]
        [Test(Description = "Unable to add a new Resolution code (bug fix)")]
        public void TC_178_verify_that_new_resolution_can_be_added()
        {
            string url = WebUrl.MainPageUrl + "web/grids/resolutioncodes";
            string resoName = "Test resolution " + CommonUtil.GetRandomNumber(5);
            string clientRef = "Test ref " + CommonUtil.GetRandomNumber(5);
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(url);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password);
            PageFactoryManager.Get<CommonGridPage>()
                .IsOnGrid()
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResolutionCodeDetailPage>()
                .IsOnResolutionCodeDetailPage()
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CommonGridPage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResolutionCodeDetailPage>()
               .IsOnResolutionCodeDetailPage()
               .VerifyNoIdIsGenerated()
               .InputResolutionCodeDetails(resoName, clientRef)
               .SaveResolutionCode()
               .WaitForLoadingIconToDisappear()
               .CloseCurrentWindow()
               .SwitchToLastWindow()
               .ClickRefreshBtn();
            PageFactoryManager.Get<CommonGridPage>()
                .IsOnGrid()
                .VerifyFirstResultValue("Name", resoName);
        }

        [Category("Bug fix")]
        [Category("Dee")]
        [Test(Description = "The AdHoc tasks don't inherit the PartyID from ServiceTask (bug fix)")]
        public void TC_179_verify_that_adhoc_task_can_inherit_partyID()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstServiceTaskLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string serviceTaskDescription = PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .GetServiceTaskDescription();

            string serviceTaskSite = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceSite();

            string serviceTaskGroup = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceGroupName();

            string serviceTask = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceName();

            string serviceTaskId = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceTaskId();

            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCreateAdhocTaskButton()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            string taskDescription = PageFactoryManager.Get<DetailTaskPage>()
                .GetLocationName();

            string site = PageFactoryManager.Get<DetailTaskPage>()
                .GetSite();

            string group = PageFactoryManager.Get<DetailTaskPage>()
                .GetServiceGroup();

            string service = PageFactoryManager.Get<DetailTaskPage>()
                .GetServiceName();

            string taskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();

            Assert.AreEqual(serviceTaskDescription, taskDescription);
            Assert.AreEqual(serviceTaskSite, site);
            Assert.AreEqual(serviceTaskGroup, group);
            Assert.AreEqual(serviceTask, service);

            TaskDBModel firstTask = finder.GetTask(int.Parse(taskId))[0];
            ServiceTaskDBModel firstServiceTask = finder.GetTaskService(int.Parse(serviceTaskId))[0];

            Assert.AreEqual(firstTask.PartyId, firstServiceTask.PartyId);
            Assert.AreEqual(firstTask.AgreementId, firstServiceTask.AgreementId);
            Assert.AreEqual(firstTask.AgreementlinetasktypeId, firstServiceTask.AgreementlinetasktypeId);
            Assert.AreEqual(firstTask.ServiceTaskId, int.Parse(serviceTaskId));
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Service Unit point map showing incorrect data (bug fix) - Point Segment")]
        public void TC_205_Service_Unit_point_map_showing_incorrect_data_point_segment()
        {
            string idSegment = "32807";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOptionLast(Contract.RM)
                .OpenOption("Point Segments")
                .SwitchNewIFrame();
            //Step line 8: Open a point segment
            PageFactoryManager.Get<PointSegmentListingPage>()
                .WaitForPointSegmentsPageDisplayed()
                .FilterSegmentById(idSegment)
                .DoubleClickFirstPointSegmentRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            string segmentDesc = pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed()
                .ClickOnMapTab()
                .GetDescInMapTab();
            pointSegmentDetailPage
                .ClickOnActiveServiceTab()
                .WaitForLoadingIconToDisappear();

            //Step line 9: Open [Service unit]
            pointSegmentDetailPage
                .ClickOnFirstServiceUnit()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            //Step line 10: At [Service Unit] detail click on [Service Unit Points]
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .DoubleClickOnServiceUnitPointId(idSegment)
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();

            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(segmentDesc)
                .ClickOnMapTab()
                .VerifyValueInMapTabSegmentType(segmentDesc);
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Service Unit point map showing incorrect data (bug fix) - Point Note")]
        public void TC_205_Service_Unit_point_map_showing_incorrect_data_point_note()
        {
            string idNode = "3";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOptionLast(Contract.RM)
                .OpenOption("Point Nodes")
                .SwitchNewIFrame();
            //Step line 8: Open a point node
            PageFactoryManager.Get<PointNodeListingPage>()
                .WaitForPointNodeListingPageDisplayed()
                .FilterNodeById(idNode)
                .DoubleClickFirstPointNodeRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<PointNodeDetailPage>();
            pointNodeDetailPage
                .WaitForPointNodeDetailDisplayed();
            string nodeDesc = pointNodeDetailPage
                .ClickOnMapTab()
                .GetDescInMapTab();
            pointNodeDetailPage
                .ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();

            //Step line 9: Open [Service unit]
            pointNodeDetailPage
                .ClickOnFirstServiceUnit()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            //Step line 10: At [Service Unit] detail click on [Service Unit Points]
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .DoubleClickOnServiceUnitPointId(idNode)
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();

            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(nodeDesc)
                .ClickOnMapTab()
                .VerifyValueInMapTabNoteType(nodeDesc);
        }

        [Category("Dee")]
        [Test(Description = "Add a hyperlink to Round form for easier access of round group form")]
        public void TC_207_hyper_link_for_round_group()
        {
            string url = WebUrl.MainPageUrl + "web/rounds/37";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(url);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .WaitForLoadingIconToDisappear();
            var roundName = PageFactoryManager.Get<RoundDetailPage>()
                .GetRoundName();
            PageFactoryManager.Get<RoundDetailPage>()
                .ClickRoundGroupHyperLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            var roundGroupName = PageFactoryManager.Get<RoundGroupPage>()
                .GetRoundGroupName();
            Assert.IsTrue(roundName.Contains(roundGroupName), "Expected " + roundName + " to contain " + roundGroupName);
        }

        [Category("Dee")]
        [Test(Description = "Daily Allocation - Prompt user with resolution code dropdown when resolution code is mandatory for Resource State")]
        public void TC_219_daily_allocation_prompt_user_with_resolution_code()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 5);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
                .SelectShift("AM")
                .InsertDate(dateInFutre + Keys.Enter)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.EastCollections)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Verify popup
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .SelectResourceState("SICK")
                .IsReasonPopupDisplayed()
                .VerifyConfirmButtonEnabled(false)
                .CloseReasonPopup()
                .ClickAllocatedResource(resourceName)
                .SelectResourceState("TRAINING")
                .IsReasonPopupDisplayed()
                .VerifyConfirmButtonEnabled(false)
                .SelectReason(ResourceReason.Paid)
                .VerifyConfirmButtonEnabled(true);
        }

        [Category("ServiceUnitPoint")]
        [Category("Chang")]
        [Test(Description = "Service Unit point map showing incorrect data (bug fix) - Point Area")]
        public void TC_205_Service_Unit_point_map_showing_incorrect_data_point_area()
        {
            string idArea = "6";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOptionLast(Contract.RM)
                .OpenOption("Point Areas")
                .SwitchNewIFrame();
            //Step line 8: Open a point node
            PageFactoryManager.Get<PointAreaListingPage>()
                .WaitForPointAreaListingPageDisplayed()
                .FilterAreaById(idArea)
                .DoubleClickFirstPointAreaRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<PointAreaDetailPage>();
            pointAreaDetailPage
                .WaitForAreaDetailDisplayed();
            string areaDesc = pointAreaDetailPage
                .ClickOnMapTab()
                .GetDescInMapTab();
            pointAreaDetailPage
                .ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();

            //Step line 9: Open [Service unit]
            pointAreaDetailPage
                .ClickOnFirstServiceUnit()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            //Step line 10: At [Service Unit] detail click on [Service Unit Points]
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .DoubleClickOnServiceUnitPointId(idArea)
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();

            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(areaDesc)
                .ClickOnMapTab()
                .VerifyValueInMapTabAreaType(areaDesc);
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Name is required error displays and party form cannot be updated (bug fix)")]
        public void TC_211_Name_is_required_error_displays_and_party_form_cannot_be_updated()
        {
            string partyId = "1090";
            string partyName = "Network Rail";
            PartyModel partyModel = new PartyModel("AutoPartyy " + CommonUtil.GetRandomNumber(4), Contract.Commercial, CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PartyCommonPage partyCommonPage = PageFactoryManager.Get<PartyCommonPage>();
            CreatePartyPage createPartyPage = PageFactoryManager.Get<CreatePartyPage>();
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Missing name when creating a party
            createPartyPage
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .SleepTimeInSeconds(4);
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyModel.PartyName)
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Missing name when updating a party
            partyCommonPage
                .FilterPartyById(partyId)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            string partyNameUpdated = "Update party " + CommonUtil.GetRandomNumber(4);
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .InputPartyNameInput(partyNameUpdated)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyPartyNameAfterUpdated(partyNameUpdated);
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Service task form - tabs are not loading on first time (bug fix)")]
        public void TC_214_Service_task_form_tabs_are_not_loading_on_first_time()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120851");
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            servicesTaskPage
                .IsServiceTaskPage()
                //Click on [Detail] tab and verify
                .ClickOnDetailTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Data] tab and verify
            servicesTaskPage
                .ClickOnDataTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Task Lines] tab and verify
            servicesTaskPage
                .ClickOnTaskLineTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Announcements] tab and verify
            servicesTaskPage
                .ClickOnAnnouncementTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Schedules] tab and verify
            servicesTaskPage
                .ClickOnSchedulesTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Schedules] tab and verify
            servicesTaskPage
                .ClickOnSchedulesTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [History] tab and verify
            servicesTaskPage
                .ClickOnHistoryTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Map] tab and verify
            servicesTaskPage
                .ClickOnMapTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Risks] tab and verify
            servicesTaskPage
                .ClickOnRisksTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Subscriptions] tab and verify
            servicesTaskPage
                .ClickOnSubscriptionsTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Notifications] tab and verify
            servicesTaskPage
                .ClickOnNotificationsTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
            //Click on [Indicators] tab and verify
            servicesTaskPage
                .ClickOnIndicatorsTab()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessagesIsUnDisplayed();
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "The read only images are black & white (bug fix) - Update inspection Unallocated to Completed")]
        public void TC_209_The_read_only_images_are_black_and_white_unallocated_to_completed()
        {
            string unallocatedStatus = "Unallocated";
            string relLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Resources/echo.jpeg");
            string newPath = new Uri(relLogo).LocalPath;

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<HomePage>()
                 .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionByStatus(unallocatedStatus)
                .WaitForLoadingIconToDisappear();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .getAllInspectionInList(2);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionModels[0].ID + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionModels[0].inspectionType)
                .VerifyInspectionId(inspectionModels[0].ID)
                //Click on [Data] tab
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .UploadImage(newPath)
                .SelectStreetGrade("A")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Update the state to completed
            detailInspectionPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickCompleteBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .VerifyAllFieldsInPopupDisabled()
                .VerifyStateInspection("Complete")
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            //Line 35 => Verify data tab
            detailInspectionPage
                .VerifyTheImageIsReadOnly();
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "The read only images are black & white (bug fix) - Update inspection Unallocated to Cancelled")]
        public void TC_209_The_read_only_images_are_black_and_white_unallocated_to_cancelled()
        {
            string unallocatedStatus = "Unallocated";
            string relLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Resources/echo.jpeg");
            string newPath = new Uri(relLogo).LocalPath;

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<HomePage>()
                 .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionByStatus(unallocatedStatus)
                .WaitForLoadingIconToDisappear();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .getAllInspectionInList(2);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionModels[0].ID + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionModels[0].inspectionType)
                .VerifyInspectionId(inspectionModels[0].ID)
                //Click on [Data] tab
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .UploadImage(newPath)
                .SelectStreetGrade("B")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Update the state to cancelled
            detailInspectionPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickCancelBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .VerifyAllFieldsInPopupDisabled()
                .VerifyStateInspection("Cancelled")
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            //Line 35 => Verify data tab
            detailInspectionPage
                .VerifyTheImageIsReadOnly();
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "The read only images are black & white (bug fix) - Update inspection Unallocated to Expired")]
        public void TC_209_The_read_only_images_are_black_and_white_unallocated_to_Expired()
        {
            string unallocatedStatus = "Unallocated";
            string relLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Resources/echo.jpeg");
            string newPath = new Uri(relLogo).LocalPath;

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<HomePage>()
                 .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionByStatus(unallocatedStatus)
                .WaitForLoadingIconToDisappear();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .getAllInspectionInList(2);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionModels[0].ID + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionModels[0].inspectionType)
                .VerifyInspectionId(inspectionModels[0].ID)
                //Click on [Data] tab
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .UploadImage(newPath)
                .SelectStreetGrade("C")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Update the state to cancelled
            detailInspectionPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            string validFromValue = CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, -2);
            string validToValue = CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, -1);
            detailInspectionPage
               .InputValidFrom(validFromValue)
               .InputValidTo(validToValue)
               .ClickSaveBtn()
               .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .VerifyAllFieldsInPopupDisabled()
                .VerifyStateInspection("Expired")
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            //Line 35 => Verify data tab
            detailInspectionPage
                .VerifyTheImageIsReadOnly();
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Site form - update with validation is not handled correctly (bug fix)")]
        public void TC_210_Site_form_update_with_validation_is_not_handed_correctly()
        {
            string siteId1 = "1200";
            string siteId2 = "1199";
            string accountingRefTheSame = "1234";
            string accountingRefDifferent = CommonUtil.GetRandomNumber(5);
            string partyName = "PureGym";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/1121");
            DetailPartyPage partyDetailsTab = PageFactoryManager.Get<DetailPartyPage>();
            partyDetailsTab
                .WaitForLoadingIconToDisappear();
            partyDetailsTab
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            partyDetailsTab
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            partyDetailsTab
                .FilterBySiteId(siteId2)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            SitePage sitePage = PageFactoryManager.Get<SitePage>();
            sitePage
                .IsSiteDetailPage()
                .ClickOnDetailTab()
                .InputAccountingRef(accountingRefTheSame)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            partyDetailsTab
                .ClickOnClearBtn()
                .WaitForLoadingIconToDisappear();
            partyDetailsTab
                .FilterBySiteId(siteId1)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Step line 8: Update the accounting ref to the same number with others
            sitePage
                .IsSiteDetailPage()
                .ClickOnDetailTab()
                .InputAccountingRef(accountingRefTheSame)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.TheAccountingRefAlreadyUsed)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.TheAccountingRefAlreadyUsed);
            sitePage
                .VerifyAccountingRefAfterSaving(accountingRefTheSame)
                //Step line 9: Update the accouting ref to the different number
                .InputAccountingRef(accountingRefDifferent)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            sitePage
                .VerifyAccountingRefAfterSaving(accountingRefDifferent)
                //Step line 10: Remove accounting ref and Save
                .RemoveAccountingRef()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            sitePage
                .VerifyAccountingRefAfterSaving("")
                //Step line 11: Update the accounting ref to the same number with others
                .InputAccountingRef(accountingRefTheSame)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.TheAccountingRefAlreadyUsed)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 12: Go back to sites grid and Check the value of the accounting value
            partyDetailsTab
                .ClickOnClearBtn()
                .WaitForLoadingIconToDisappear();
            partyDetailsTab
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            partyDetailsTab
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            partyDetailsTab
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            partyDetailsTab
                .VerifyAccountingRefAnyRow("1", accountingRefTheSame)
                .VerifyAccountingRefAnyRow("2", accountingRefTheSame);
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Endless loading of statistics on sector group (bug fix)")]
        public void TC_216_Endless_loading_of_statistics_on_sector_group()
        {
            string firstSectorId = "3";
            string secondSectorId = "4";
            string firstSectorGroupName = "Richmond Waste Collection";
            string secondSectorGroupName = "Richmond Recycling Collection";
            string[] sectorName = { "Clinical Waste", "Bulky Collections" };

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<HomePage>()
                 .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .ExpandOption(Contract.Municipal)
                .OpenOption("Sector Groups")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            SectorGroupPage sectorGroupPage = PageFactoryManager.Get<SectorGroupPage>();
            sectorGroupPage
                .VerifyElementVisibility(sectorGroupPage.AddNewItemButton, true)
                .VerifyElementVisibility(sectorGroupPage.CopyItemButton, true);

            sectorGroupPage
                .FilterSectorById(firstSectorId)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailSectorGroupPage detailSectorGroupPage = PageFactoryManager.Get<DetailSectorGroupPage>();
            //Step line 7
            detailSectorGroupPage
                .IsDetailSectorGroupPage(firstSectorGroupName)
                .ClickOnStatisticTab()
                .SelectSector(sectorName)
                .ClickOnLoadInStatisticTabBtn()
                .WaitForLoadingIconToDisappear();
            detailSectorGroupPage
                .VerifyDisplayDataAfterSelectSection()
                .VerifyNotDisplayErrorMessage()
                .CloseCurrentWindow()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();

            //Step line 8
            sectorGroupPage
                .ClickOnClearBtn()
                .WaitForLoadingIconToDisappear();
            sectorGroupPage
                .FilterSectorById(secondSectorId)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailSectorGroupPage
                .IsDetailSectorGroupPage(secondSectorGroupName)
                .ClickOnStatisticTab()
                .SelectSector(sectorName)
                .ClickOnLoadInStatisticTabBtn()
                .WaitForLoadingIconToDisappear();
            detailSectorGroupPage
                .VerifyDisplayDataAfterSelectSection()
                .VerifyNotDisplayErrorMessage();
            //Step line 9: Unselect some service and select diferent one
            string[] otherSectors = { "Bring Banks", "Domestic Recycling" };
            detailSectorGroupPage
                .UnselectAllSectorService()
                .SelectSector(otherSectors)
                .ClickOnLoadInStatisticTabBtn()
                .WaitForLoadingIconToDisappear();
            detailSectorGroupPage
                .VerifyDisplayDataAfterSelectSection()
                .VerifyNotDisplayErrorMessage();
        }

        //BUG => Need to confirm when selecting Allocated Unit = [Select...] and User listing
        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Invalid users listed in the users list in contract unit form (bug fix)")]
        public void TC_215_Invalid_users_listed_in_the_users_list_in_contract_unit_form()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string creditId = "7";
            string partyName = "PREMIER INN";
            string eventIdTypeComplaint = "13";

            //API: Get all assigned user
            List<UserDBModel> userDBModels = commonFinder.GetUserActive();
            List<string> allDisplayUserNameDB = new List<string>();
            allDisplayUserNameDB.Add("Select...");
            foreach (UserDBModel userDBModel in userDBModels)
            {
                allDisplayUserNameDB.Add(userDBModel.displayname);
            }
            //userDBModels.Select(q => q.displayname).ToList();

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<HomePage>()
                 .IsOnHomePage(AutoUser46);
            //Step line 7:
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .OpenOption("Credit Notes")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreditNotePage>()
                .FilterByCreditId(creditId)
                .ClickOnFirstCreditRow()
                .DoubleClickOnFirstCreditRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            List<string> allAssignedUser = PageFactoryManager.Get<DetailCreditNotePage>()
                .IsCreditNoteDetailPage(partyName)
                .ClickOnDetailTab()
                .GetAllAssignedUser();
            //Assert.AreEqual(allDisplayUserNameDB, allAssignedUser, "Display name is not the same in the list");
            //Step line 9
            PageFactoryManager.Get<DetailCreditNotePage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventsListingPage>()
                .FilterByEventId(eventIdTypeComplaint)
                //Click row with icon
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed();
            //API: Get all allocated unit
            List<ContractUnitDBModel> contractUnitDBModels = commonFinder.GetContractUnitByContractId("1");
            List<string> allContractUnitName = contractUnitDBModels.Select(p => p.contractunit).ToList();
            List<string> assignedUserDB1 = commonFinder.GetContractUnitUserListVByContractUnit(contractUnitDBModels[0].contractunitID.ToString());
            List<string> assignedUserDB2 = commonFinder.GetContractUnitUserListVByContractUnit(contractUnitDBModels[1].contractunitID.ToString());
            List<string> assignedUserDB3 = commonFinder.GetContractUnitUserListVByContractUnit(contractUnitDBModels[2].contractunitID.ToString());
            List<string> assignedUserDB4 = commonFinder.GetContractUnitUserListVByContractUnit(contractUnitDBModels[3].contractunitID.ToString());
            List<string> assignedUserDB5 = commonFinder.GetContractUnitUserListVByContractUnit(contractUnitDBModels[4].contractunitID.ToString());
            List<string> assignedUserDB6 = commonFinder.GetContractUnitUserListVByContractUnit(contractUnitDBModels[5].contractunitID.ToString());
            List<string> assignedUserDB7 = commonFinder.GetContractUnitUserListVByContractUnit(contractUnitDBModels[6].contractunitID.ToString());
            List<string> allAssignedUserDB = commonFinder.GetUserWithFunction();

            List<string> allAllocatedUnitDetailSubTab = PageFactoryManager.Get<EventDetailPage>()
                .ExpandDetailToggle()
                .ClickOnAllocatedUnit()
                .GetAllOptionInAllocatedUnitDetailSubTab();
            Assert.AreEqual(allContractUnitName, allAllocatedUnitDetailSubTab, "Allocated Unit is not matching with DB");
            //No select [Allocated Unit] and click on [Assigned User]
            List<string> allAssignedUserDisplayed = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(allAssignedUserDB, allAssignedUserDisplayed, "Assigned User is not matching with DB");

            //Select and allocated unit and verify combos are matching
            eventDetailPage
                .SelectAnyAllocatedUnit(allContractUnitName[0]);
            List<string> assignerUserOfFirstAllocatedUnit = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(assignedUserDB1.Select(x => x.AsString()).ToList(), assignerUserOfFirstAllocatedUnit);
            //Second allocated unit
            eventDetailPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[1]);
            List<string> assignerUserOfSecondAllocatedUnit = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(assignedUserDB2.Select(x => x.AsString()).ToList(), assignerUserOfSecondAllocatedUnit);
            //Third allocated unit
            eventDetailPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[2]);
            List<string> assignerUserOfThirdAllocatedUnit = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(assignedUserDB3.Select(x => x.AsString()).ToList(), assignerUserOfThirdAllocatedUnit);
            //4th allocated unit
            eventDetailPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[3]);
            List<string> assignerUserOfFourthAllocatedUnit = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(assignedUserDB4.Select(x => x.AsString()).ToList(), assignerUserOfFourthAllocatedUnit);
            //5th allocated unit
            eventDetailPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[4]);
            List<string> assignerUserOfFifthAllocatedUnit = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(assignedUserDB5.Select(x => x.AsString()).ToList(), assignerUserOfFifthAllocatedUnit);
            //6th allocated unit
            eventDetailPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[5]);
            List<string> assignerUserOfSixthAllocatedUnit = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(assignedUserDB6.Select(x => x.AsString()).ToList(), assignerUserOfSixthAllocatedUnit);
            //7th allocated unit
            eventDetailPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[6]);
            List<string> assignerUserOfSeventhAllocatedUnit = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(assignedUserDB7.Select(x => x.AsString()).ToList(), assignerUserOfSeventhAllocatedUnit);
            List<string> assignerUserLast = eventDetailPage
                .ClickOnAllocatedUnit()
                .ClickOnFirstAllocatedUnit()
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            Assert.AreEqual(allAssignedUserDB, allAssignedUserDisplayed, "Assigned User is not matching with DB");

            //Step line 11: Click on [Allocated unit]
            eventDetailPage
                .ClickAllocateEventInEventActionsPanel()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            EventActionPage eventActionPage = PageFactoryManager.Get<EventActionPage>();
            //EVENT ACTION => [Allocated Unit]
            List<string> allAllocatedUnitEventAction = eventActionPage
                .IsEventActionPage()
                .ClickOnAllocatedUnit()
                .GetAllOptionsInAllocatedUnitDd();
            Assert.AreEqual(allContractUnitName, allAllocatedUnitEventAction, "Allocated Unit in the [Even action] page is not matching with DB");
            //EVENT ACTION => [Allocated User]
            List<string> allAllocatedUserEventAction = eventActionPage
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            eventActionPage
                .VerifyAllocatedUserDisplayTheSameEventForm(allAssignedUserDB, allAllocatedUserEventAction);
            //EVENT ACTION => Select [Allocated Unit] and Allocated user is matching with each other
            List<string> allocatedUserOfFirstAllocatedUnit = eventActionPage
                .SelectAnyAllocatedUnit(allContractUnitName[0])
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            Assert.AreEqual(assignedUserDB1.Select(x => x.AsString()).ToList(), allocatedUserOfFirstAllocatedUnit);
            List<string> allocatedUserOfSecondAllocatedUnit = eventActionPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[1])
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            Assert.AreEqual(assignedUserDB2.Select(x => x.AsString()).ToList(), allocatedUserOfSecondAllocatedUnit);
            List<string> allocatedUserOfThirdAllocatedUnit = eventActionPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[2])
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            Assert.AreEqual(assignedUserDB3.Select(x => x.AsString()).ToList(), allocatedUserOfThirdAllocatedUnit);
            List<string> allocatedUserOfFourthAllocatedUnit = eventActionPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[3])
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            Assert.AreEqual(assignedUserDB4.Select(x => x.AsString()).ToList(), allocatedUserOfFourthAllocatedUnit);
            List<string> allocatedUserOfFifthAllocatedUnit = eventActionPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[4])
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            Assert.AreEqual(assignedUserDB5.Select(x => x.AsString()).ToList(), allocatedUserOfFifthAllocatedUnit);
            List<string> allocatedUserOfSixthAllocatedUnit = eventActionPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[5])
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            Assert.AreEqual(assignedUserDB6.Select(x => x.AsString()).ToList(), allocatedUserOfSixthAllocatedUnit);
            List<string> allocatedUserOfSeventhAllocatedUnit = eventActionPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allContractUnitName[6])
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            Assert.AreEqual(assignedUserDB7.Select(x => x.AsString()).ToList(), allocatedUserOfSeventhAllocatedUnit);
            eventActionPage
                .ClickOnAllocatedUnitLabel()
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            eventDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Step line 15: Verify in [Inspections]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionByStatus("Unallocated")
                .WaitForLoadingIconToDisappear();
            List<InspectionModel> inspectionModels = PageFactoryManager.Get<AllInspectionListingPage>()
                .getAllInspectionInList(1);
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionModels[0].ID + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionModels[0].inspectionType)
                .VerifyInspectionId(inspectionModels[0].ID)
                .ClickOnDetailTab()
                .ClickOnSelectionOptionInAllocatedUnit()
                .ClickOnDataTab()
                .SelectStreetGrade("A")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            List<string> allAllocatedUnitInspectionDetail = detailInspectionPage
                .ClickOnDetailTab()
                .ClickOnSelectionOptionInAllocatedUnit()
                .GetAllOptionInAllocatedUnit();
            Assert.AreEqual(allContractUnitName, allAllocatedUnitInspectionDetail, "Allocated Unit in the [Inpsection detail] is not mapping with DB");
            List<string> allAssignedUserInspectionDetail = detailInspectionPage
                .ClickOnAllocatedUnitLabel()
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUser();
            Assert.AreEqual(allDisplayUserNameDB, allAssignedUserInspectionDetail, "Display name is not the same in the list [Assigned User] of the Inspection Detail page");
            //Click on each [Allocated Unit]
            detailInspectionPage
                .ClickOnAllocatedUnitLabel()
                .ClickOnAllocatedUnitInDetailTab()
                .SelectAnyAllocatedUnit(allContractUnitName[0])
                .WaitForLoadingIconToDisappear();
            List<string> assignedUserOfFirstAllocatedUnitInspection = detailInspectionPage
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUserNoSelected();
            Assert.AreEqual(assignedUserDB1.Select(x => x.AsString()).ToList(), assignedUserOfFirstAllocatedUnitInspection);
            detailInspectionPage
                .ClickOnAllocatedUnitLabel()
                .ClickOnAllocatedUnitInDetailTab()
                .SelectAnyAllocatedUnit(allContractUnitName[1])
                .WaitForLoadingIconToDisappear();
            List<string> assignedUserOfSecondAllocatedUnitInspection = detailInspectionPage
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUserNoSelected();
            Assert.AreEqual(assignedUserDB2.Select(x => x.AsString()).ToList(), assignedUserOfSecondAllocatedUnitInspection);
            detailInspectionPage
                .ClickOnAllocatedUnitLabel()
                .ClickOnAllocatedUnitInDetailTab()
                .SelectAnyAllocatedUnit(allContractUnitName[2])
                .WaitForLoadingIconToDisappear();
            List<string> assignedUserOfThirdAllocatedUnitInspection = detailInspectionPage
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUserNoSelected();
            Assert.AreEqual(assignedUserDB3.Select(x => x.AsString()).ToList(), assignedUserOfThirdAllocatedUnitInspection);
            detailInspectionPage
                .ClickOnAllocatedUnitLabel()
                .ClickOnAllocatedUnitInDetailTab()
                .SelectAnyAllocatedUnit(allContractUnitName[3])
                .WaitForLoadingIconToDisappear();
            List<string> assignedUserOfFourthAllocatedUnitInspection = detailInspectionPage
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUserNoSelected();
            Assert.AreEqual(assignedUserDB4.Select(x => x.AsString()).ToList(), assignedUserOfFourthAllocatedUnitInspection);
            detailInspectionPage
                 .ClickOnAllocatedUnitLabel()
                 .ClickOnAllocatedUnitInDetailTab()
                 .SelectAnyAllocatedUnit(allContractUnitName[4])
                 .WaitForLoadingIconToDisappear();
            List<string> assignedUserOfFifthAllocatedUnitInspection = detailInspectionPage
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUserNoSelected();
            Assert.AreEqual(assignedUserDB5.Select(x => x.AsString()).ToList(), assignedUserOfFifthAllocatedUnitInspection);
            detailInspectionPage
                 .ClickOnAllocatedUnitLabel()
                 .ClickOnAllocatedUnitInDetailTab()
                 .SelectAnyAllocatedUnit(allContractUnitName[5])
                 .WaitForLoadingIconToDisappear();
            List<string> assignedUserOfSixthAllocatedUnitInspection = detailInspectionPage
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUserNoSelected();
            Assert.AreEqual(assignedUserDB6.Select(x => x.AsString()).ToList(), assignedUserOfSixthAllocatedUnitInspection);
            detailInspectionPage
                 .ClickOnAllocatedUnitLabel()
                 .ClickOnAllocatedUnitInDetailTab()
                 .SelectAnyAllocatedUnit(allContractUnitName[2])
                 .WaitForLoadingIconToDisappear();
            List<string> assignedUserOfSeventhAllocatedUnitInspection = detailInspectionPage
                .ClickOnAssignedUserInDetailTab()
                .GetAllOptionInAssignedUserNoSelected();
            Assert.AreEqual(assignedUserDB7.Select(x => x.AsString()).ToList(), assignedUserOfSeventhAllocatedUnitInspection);
            detailInspectionPage
                .ClickOnAllocatedUnitLabel()
                .CloseCurrentWindow()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Step line 18
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOptionLast("Contract Units")
                .OpenLastOption("Clinical")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ContractUnitDetailPage>()
                .IsContractUnit("Clinical")
                .ClickOnUsersTab()
                .ClickOnAddNewItemInUsersTab()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            List<string> allActiveUsers = PageFactoryManager.Get<CreateContractUnitUserPage>()
                .IsCreateContractUnitUserPage()
                .ClickOnUserDd()
                .GetAllUserInDd();
            //Assert.AreEqual(allDisplayUserNameDB, allActiveUsers, "Active user is not matching in the [Contract Unit User] page");
            List<string> userDBNotActive = commonFinder.GetUserInActive();
            PageFactoryManager.Get<CreateContractUnitUserPage>()
                .VerifyUserIsNotDiplayedInUserDd(userDBNotActive);
        }

        //BUG => Need to confirm when regenerating status is not updated
        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Regeneration of sales invoice batch is not recorded (bug fix)")]
        public void TC_208_Regeneration_of_sales_invoice_batch_is_not_recorded()
        {
            string saleBatchIdGeneratedStatus = "7";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<HomePage>()
                 .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.SalesInvoiceBatches)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .IsSalesInvoiceBatchesPage()
                .FilterBySaleInvoiceBatchId(saleBatchIdGeneratedStatus)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            SalesInvoiceBatchesDetailPage salesInvoiceBatchesDetailPage = PageFactoryManager.Get<SalesInvoiceBatchesDetailPage>();
            string periodTo = salesInvoiceBatchesDetailPage
                .IsSalesInvoiceBatchesDetailPage("GENERATED", saleBatchIdGeneratedStatus)
                .ClickOnDetailsTab()
                .GetFirstPeriodTo();
            string periodFrom = salesInvoiceBatchesDetailPage
                .GetFirstPeriodFrom();
            salesInvoiceBatchesDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .ClickOnFirstRegenerateBatchBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceBatchRegeneratePage>()
                .IsSaleInvoiceBatchRegeneratePage()
                .ClickAndSelectPeriodTo(periodTo)
                .ClickAndSelectPeriodFrom(periodFrom)
                .ClickOnYesBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            string timeUpdated = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            //Refresh the form after 2 minutes
            PageFactoryManager.Get<SalesInvoiceBatchRegeneratePage>()
                .SleepTimeInSeconds(120);
            PageFactoryManager.Get<SalesInvoiceBatchRegeneratePage>()
                .ClickOnRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .FilterBySaleInvoiceBatchId(saleBatchIdGeneratedStatus)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            salesInvoiceBatchesDetailPage
                .IsSalesInvoiceBatchesDetailPage("GENERATED", saleBatchIdGeneratedStatus)
                .ClickOnHistoryBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<HistorySalesInvoiceBatchPage>()
                .ClickOnExpandToggleAtFirstRow()
                .VerifyValueInHistoryTableAfterUpdated("GenerationScheduledDate", timeUpdated, "Updated");

        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "The agreementaction not created when update Billing Rules, Invoice Address, Invoice Contact and Invoice Schedule on agreementlines (bug fix)")]
        public void TC_217_The_agreement_action_not_created_when_update_billing_rules_invoice_address_invoice_contact_and_invoice_schedule_on_agreement_lines()
        {
            string partyId = "1119";
            string partyName = "Pret a Manger";
            string agreementLineId = "168";
            string billingRule = "Bill as scheduled";
            string invoiceAddress = "37 GEORGE STREET, RICHMOND, TW9 1HY";
            string invoiceSchedule = "Daily in Arrears";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<HomePage>()
                 .IsOnHomePage(AutoUser46);
            //Precondition: Open a party and add contact for a party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickOnContactTab()
                .ClickAddNewItemAtContactTab()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ContactModel contactModel = new ContactModel();
            PageFactoryManager.Get<CreatePartyContactPage>()
                .IsCreatePartyContactPage()
                .EnterFirstName(contactModel.FirstName)
                .EnterLastName(contactModel.LastName)
                .EnterMobileValue(contactModel.Mobile)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .OpenAgreementTab()
                .IsOnAgreementTab()
                .OpenFirstAgreementRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            AgreementDetailPage agreementDetailPage = PageFactoryManager.Get<AgreementDetailPage>();
            string invoiceContact = contactModel.FirstName + " " + contactModel.LastName;
            agreementDetailPage
                .WaitForDetailAgreementLoaded("COMMERCIAL COLLECTIONS", "PRET A MANGER")
                //Update [Invoice Schedule] in first Serviced
                .ClickOnFirstInvoiceScheduleAndSelectAnyOption("Daily in Arrears")
                //Update [Invoice Contact] in first Serviced
                .ClickOnFirstInvoiceContactAndSelectAnyOption(invoiceContact)
                //Update [Invoice Address] in first Serviced
                .ClickOnFirstInvoiceAddressAndSelectAnyOption(invoiceAddress)
                //Update [Billing Rule] in first Serviced
                .ClickOnFirstBillingRuleAndSelectAnyOption(billingRule)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            string[] valueExp = { invoiceSchedule, invoiceAddress, contactModel.FirstName + contactModel.LastName, billingRule };
            //Click on [History] tab and verify
            agreementDetailPage
                .ClickOnHistoryTab()
                .VerifyTitleUpdateInHistoryTab("Update - AgreementLine")
                .VerifyHistoryAfterUpdateFirstServiced(CommonConstants.HistoryInAgreementDetail, valueExp, AutoUser46.DisplayName)
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .CloseCurrentWindow()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();

            //TC217
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                //.ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.SiteServices)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementLineId)
                .OpenSecondResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementLineId)
                .ClickDetailTab()
                //Verify default value in [Invoice Schedule], [Invoice Contact], [Invoice Address] and [Billing Rule]
                .VerifyDefaulValueInISICIABR(invoiceSchedule, invoiceContact, invoiceAddress, billingRule)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Step line 8: Update [Billing Rules]
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementLineId)
                .ClickDetailTab()
                //Step line 8: Update [Invoice Schedule]
                .ClickOnInvoiceSchedule()
                .SelectAnyInvoiceSchedule(invoiceSchedule)
                //Step line 8: Update [Invoice Contact]
                .ClickOnInvoiceContact()
                .SelectAnyInvoiceContact(invoiceContact)
                //Step line 8: Update [Invoice Address]
                .ClickOnInvoiceAddress()
                .SelectAnyInvoiceAddress(invoiceAddress)
                //Step line 8: Update [Billint Rule]
                .ClickOnBillingRuleDd()
                .SelectAnyBillingRuleOption("Bill as actual")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            string[] secondValueExp = { invoiceSchedule, invoiceAddress, contactModel.FirstName + contactModel.LastName, "Bill as actual" };
            //Step line 9: Click on [History tab]
            PageFactoryManager.Get<AgreementLinePage>()
                .ClickOnHistoryTab()
                .VerifyHistoryAfterUpdatingAgreementLine(CommonConstants.HistoryInAgreementDetail, secondValueExp, AutoUser46.DisplayName);
            //Step line 10: Click on [Agreement ID] > History tab
            PageFactoryManager.Get<AgreementLinePage>()
                .ClickOnAgreementLineHyperlink(agreementLineId)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            string[] firstNewValueExp = { invoiceSchedule, invoiceAddress, contactModel.FirstName + " " + contactModel.LastName, billingRule };
            string[] secondNewValueExp = { invoiceSchedule, invoiceAddress, contactModel.FirstName + " " + contactModel.LastName, "Bill as actual" };
            agreementDetailPage
                .WaitForDetailAgreementLoaded("COMMERCIAL COLLECTIONS", "PRET A MANGER")
                //Detail tab
                .ClickOnDetailTab()
                .VerifyValueSelectedInSeviced(firstNewValueExp, secondNewValueExp)
                //History tab
                .ClickOnHistoryTab()
                .VerifyTitleUpdateInHistoryTab("Update - AgreementLine")
                .VerifyHistoryAfterUpdateSecondServiced(CommonConstants.HistoryInAgreementDetail, secondValueExp, AutoUser46.DisplayName);
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Map screen - Remove the option Show road trail for selected resource (bug fix)")]
        public void TC_227_Map_screen_remove_the_option_show_road_trail_for_selected_resource()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .WaitForMapsTabDisplayed()
                .SendKeyInFromDate("13/10/2022 00:00")
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .ClickOnFirstRoundInRightHand()
                .ClickOnOptionsTab()
                .VerifyOptionIsNotDisplay("Show Road Trail For Selected Resource");
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "The notes tab is not loading correctly (bug fix)")]
        public void TC_228_The_notes_tab_is_not_loading_correctly()
        {
            string partyName = "Ham Food Centre";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                //Party form
                .GoToURL(WebUrl.MainPageUrl + "web/parties/1131");
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .waitForLoadingIconDisappear();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickOnNotesTab()
                .VerifyDisplayNotesTab()
                //Contact form
                .GoToURL(WebUrl.MainPageUrl + "web/contacts/8");
            PageFactoryManager.Get<CreatePartyContactPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreatePartyContactPage>()
                .IsCreatePartyContactPage()
                .ClickOnNotesTab()
                .VerifyDisplayNotesTab()
                //Contract site form
                .GoToURL(WebUrl.MainPageUrl + "web/contract-sites/3");
            PageFactoryManager.Get<DetailContractSitePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailContractSitePage>()
                .WaitForDetailContractSiteDisplayed()
                .ClickOnNotesTab()
                .IsNotesTab()
                //Site form
                .GoToURL(WebUrl.MainPageUrl + "web/sites/3");

            PageFactoryManager.Get<DetailSitePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailSitePage>()
                .IsDetailSitePage()
                .ClickOnNotesTab()
                .IsNotesTab()
                //WB Station
                .GoToURL(WebUrl.MainPageUrl + "web/weighbridge-stations/1");
            PageFactoryManager.Get<DetailWBStationPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailWBStationPage>()
                .IsDetailWBStationPage("Townmead Weighbridge")
                .ClickOnNotesTab()
                .IsNotesTab()
                //WB Ticket
                .GoToURL(WebUrl.MainPageUrl + "web/weighbridge-tickets/1");

            PageFactoryManager.Get<WeighbridgeTicketDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<WeighbridgeTicketDetailPage>()
                .IsWBTicketDetailPage("1")
                .ClickOnNotesTab()
                .IsNotesTab()
                //Credit Notes
                .GoToURL(WebUrl.MainPageUrl + "web/credit-notes/1");
            PageFactoryManager.Get<DetailCreditNotePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .IsCreditNoteDetailPage("TAPAS BRINDISA")
                .ClickOnNotesTab()
                .IsNotesTab();
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Error loading Credit notes (bug fix)")]
        public void TC_229_Error_loading_Credit_notes()
        {
            string creditId = "1";
            string partyName = "TAPAS BRINDISA";
            //Step line 1: Login TF user
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .OpenOption("Credit Notes")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreditNotePage>()
                .FilterByCreditId(creditId)
                .ClickOnFirstCreditRow()
                .DoubleClickOnFirstCreditRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .IsCreditNoteDetailPage(partyName)
                .ClickOnDetailTab()
                .VerifyNotDisplayErrorMessage();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .ClickOnLinesTab()
                .VerifyNotDisplayErrorMessage();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .ClickOnNotesTab()
                .VerifyNotDisplayErrorMessage();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Step line 2: Login another account
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser46)
                .ClickUserNameDd()
                .ClickLogoutBtn();
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login("testdj", "test123*");
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .OpenOption("Credit Notes")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CreditNotePage>()
                .FilterByCreditId(creditId)
                .ClickOnFirstCreditRow()
                .DoubleClickOnFirstCreditRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .IsCreditNoteDetailPage(partyName)
                .ClickOnDetailTab()
                .VerifyNotDisplayErrorMessage();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .ClickOnLinesTab()
                .VerifyNotDisplayErrorMessage();
            PageFactoryManager.Get<DetailCreditNotePage>()
                .ClickOnNotesTab()
                .VerifyNotDisplayErrorMessage();
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "The sales receipt form is not loading (bug fix)")]
        public void TC_239_The_sales_receipt_form_is_not_loading()
        {
            string partyName = "PureGym";
            string receiptId = "3";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/1121");
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .waitForLoadingIconDisappear();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickOnAccountStatement()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AccountStatementPage>()
                //Line: Click [Take payment] btn
                .ClickTakePayment()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<SalesReceiptPage>()
                .VerifyInfoInSaleReceiptScreen(partyName)
                .VerifyNotDisplayErrorMessage()
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            detailPartyPage
                .GoToURL(WebUrl.MainPageUrl);
            //Step line 8: Accounts > Receipt
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Receipts")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ReceiptListPage>()
                .FilterReceiptById(receiptId)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .IsSalesReceiptDetailPage()
                .VerifyNotDisplayErrorMessage();
            PageFactoryManager.Get<SalesReceiptPage>()
                .ClickLinesTab()
                .waitForLoadingIconDisappear();
            //Step line 8: Accounts > Receipt > Sale receipt line
            PageFactoryManager.Get<SalesReceiptPage>()
                .DoubleClickOnFirstLineRow()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .IsSalesReceiptLinesPage("Sales Invoice", "2")
                .VerifyNotDisplayErrorMessage();
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Task form - issues in task form when taskID=0 (bug fix)")]
        public void TC_240_Task_form_issues_in_task_form_when_task_id_0()
        {
            string serviceTaskId = "120747";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120747");
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickCreateAdhocTaskButton()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .VerifyCurrentUrlWithId0(serviceTaskId)
                //Step line 8: Click on [Task line] tab
                .ClickOnTaskLineTab()
                .ClickOnAddNewItemBtnTaskLinesTab()
                .SelectInfoSecondTaskLineRow("Service", "1100L")
                .VerifyProductOfSecondRowMappingFirstRow()
                .ClickCloseBtn()
                .AcceptAlert()
                .SwitchToChildWindow(1);
            //Step line 10: Click on [Detail] tab and update status
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCreateAdhocTaskButton()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd("Completed")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);

            string[] titleValues = { "ActualAssetQuantity", "Asset Type", "ActualProductQuantity", "ScheduledAssetQuantity", "ScheduledProductQuantity", "Object Tag", "Product", "Product Unit", "State", "Is Serialised", "Order", "Completed Date", "ClientReference" };
            string[] expValues = { "3.", "1100L.", "80.", "3.", "80.", ".", "General Recycling.", "Kilograms.", "Completed.", "Unticked.", "0.", CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT + "."), "." };


            PageFactoryManager.Get<DetailTaskPage>()
                //Step line 10: Verify [Task lines]
                .ClickOnTaskLineTab()
                .VerifyFirstTaskLineAfterUpdateStatus("3", "Completed", "80")
                //Step line 12: Verify [History]
                .ClickOnHistoryTab()
                .VerifyTitleCreate()
                .VerifyTaskLineAfterCreatedState(titleValues, expValues)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);

            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCreateAdhocTaskButton()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DetailTaskPage>()
                .ClickOnDetailTab()
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd("Completed")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);

            string[] titleValuesAfter = { "ActualAssetQuantity", "ActualProductQuantity", "State", "Completed Date", "Auto Confirmed" };
            string[] expValuesAfter = { "3.", "80.", "Completed.", CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT + "."), "Manually Confirmed on Web." };
            PageFactoryManager.Get<DetailTaskPage>()
                //Step line 10: Verify [Task lines]
                .ClickOnTaskLineTab()
                .VerifyFirstTaskLineAfterUpdateStatus("3", "Completed", "80")
                //Step line 12: Verify [History]
                .ClickOnHistoryTab()
                .VerifyTitleCreate()
                .VerifyTaskLineAfterUpdatedState(titleValuesAfter, expValuesAfter);
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Some of the objects cannot be retired (bug fix) (Step 1-22)")]
        public void TC_241_1_Some_of_the_objects_cannot_be_retired()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            //Step line 7
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SectorPage>()
                .IsSectorPage(Contract.Commercial)
                .ClickOnRetiredBtn();

            PageFactoryManager.Get<SectorPage>()
                .IsRetiredPopup()
                //Step line 8: Verify in [Contract]
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<SectorPage>()
                .IsRetiredPopup()
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                //Step line 9: Verify in [Service group]
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Ancillary")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceGroupListPage>()
                .IsServiceGroupPage("Ancillary")
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceGroupListPage>()
                .IsRetiredPopup()
                //Step line 10: Click x/cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceGroupListPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear()
                //Step line 11: Verify in [Service]
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Ancillary")
                .OpenOption("Skips")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceListPage>()
                .IsServiceListPage("Skips")
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceListPage>()
                .IsRetiredPopup()
                //Step line 12: Click x/cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceListPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear()
                //Step line 13: Verify in [Round group]
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Skips")
                .ExpandOption("Round Groups")
                .OpenOption("SKIP1")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .IsRoundGroupPage("SKIP1")
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RoundGroupPage>()
                .IsRetiredPopup()
                //Step line 14: Click x/cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RoundGroupPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear()
                //Step line 15: Verify in [Round]
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("SKIP1")
                .OpenLastOption("Monday")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundListPage>()
                .IsRoundListPage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RoundListPage>()
                .IsRetiredPopup()
                //Step line 16: Click x/cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RoundListPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear()
                //Step line 17: Verify in [Service task schedules ]
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-task-schedules/236677");

            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsServiceTaskSchedule()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsRetiredPopup()
                //Step line 18: Click x/cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear()
                //Step line 19: Verify in [Service task]
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120892");
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsRetiredPopup()
                //Step line 20: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear()
                //Step line 21: Verify in [Service units]
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-units/230018");
            PageFactoryManager.Get<ServiceUnitPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitPage>()
                .IsServiceUnitPage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceUnitPage>()
                .IsRetiredPopup()
                //Step line 22: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ServiceUnitPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            
        }

        [Category("BugFix")]
        [Category("Chang")]
        [Test(Description = "Some of the objects cannot be retired (bug fix) (Step 23-41)")]
        public void TC_241_2_Some_of_the_objects_cannot_be_retired()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            //Step line 23: Verify in [Round schedules]
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/round-schedules/177");
            PageFactoryManager.Get<RoundSchedulePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundSchedulePage>()
                .IsRoundSchedulePage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RoundSchedulePage>()
                .IsRetiredPopup()
                //Step line 24: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RoundSchedulePage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 25: Verify in [Business unit group]
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/business-unit-groups/1");
            PageFactoryManager.Get<BusinessUnitGroupDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BusinessUnitGroupDetailPage>()
                .IsBusinessUnitGroupPage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<BusinessUnitGroupDetailPage>()
                .IsRetiredPopup()
                //Step line 26: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<BusinessUnitGroupDetailPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 27: Verify in [Business unit]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/business-units/5");
            PageFactoryManager.Get<BusinessUnitPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BusinessUnitPage>()
                .IsBusinessUnitPage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<BusinessUnitPage>()
                .IsRetiredPopup()
                //Step line 28: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<BusinessUnitPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 29: Verify in [Contract Unit]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/contract-units/8");
            PageFactoryManager.Get<ContractUnitDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ContractUnitDetailPage>()
                .IsContractUnit("Commercial")
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ContractUnitDetailPage>()
                .IsRetiredPopup()
                //Step line 30: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ContractUnitDetailPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 31: Verify in [Contract Site]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/contract-sites/3");
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .IsContractSiteDetailPage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .IsRetiredPopup()
                //Step line 32: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 33: Verify in [Resource]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/resources/100");
            PageFactoryManager.Get<ResoureDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResoureDetailPage>()
                .IsResourceDetailPage()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ResoureDetailPage>()
                .IsRetiredPopup()
                //Step line 34: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<ResoureDetailPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 35: Verify in [Party]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/parties/72");
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully("Michael Jones Architects")
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .IsRetiredPopup("72")
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 37: Verify in [Site]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/sites/1200");
            PageFactoryManager.Get<SiteDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteDetailPage>()
                .WaitForSiteDetailPageLoaded("PureGym Twickenham / 35 HEATH ROAD, TWICKENHAM, TW1 4AW", "PUREGYM")
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<SiteDetailPage>()
                .IsRetiredPopup()
                //Step line 38: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<SiteDetailPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 39: Verify in [Agreement]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/agreements/142");
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForDetailAgreementLoaded("COMMERCIAL COLLECTIONS", "CLAREMONT CATTERY")
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<AgreementDetailPage>()
                .IsRetiredPopup()
                //Step line 40: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<AgreementDetailPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();
            //Step line 41: Verify in [Regions]
            PageFactoryManager.Get<NavigationBase>()
               .GoToURL(WebUrl.MainPageUrl + "web/regions/1");
            PageFactoryManager.Get<RegionDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RegionDetailPage>()
                .IsRegionDetailPage(Region.UK)
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RegionDetailPage>()
                .IsRetiredPopup()
                //Step line 42: Click x/Cancel
                .ClickOnXBtn()
                .VerifyPopupIsDisappear()
                .ClickOnRetiredBtn();
            PageFactoryManager.Get<RegionDetailPage>()
                .IsRetiredPopup()
                .ClickOnCancelBtn()
                .VerifyPopupIsDisappear();

        }
        [Category("BugFix")]
        [Category("Dee")]
        [Test]
        public void TC_138_task_line_mapping()
        {
            string taskType = "Commercial Collection";
            string url = WebUrl.MainPageUrl+ "Echo2/Echo2Extra/PopupDefault.aspx?VName=ConfigView&VNodeID=79&CPath=T245R1M3350M3350T181R3&ObjectID=3&TypeName=TaskType&RefTypeName=none&ReferenceName=none";

            CommonFinder commonFinder = new CommonFinder(DbContext);
            var oldVersion = commonFinder.GetTaskTypeByName(taskType)[0];
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password);
            PageFactoryManager.Get<TaskTypePage>()
                .SwitchToTabNamed("TaskLines to Task Mapping")
                .IsOnTaskLineToTaskMappingTab()
                .SelectAllRandom()
                .ClickSaveButton()
                .SleepTimeInSeconds(5);
            var newVersion = commonFinder.GetTaskTypeByName(taskType)[0];
            Assert.IsFalse(oldVersion.Equals(newVersion));
        }
    }
}
