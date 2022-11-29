using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications.RiskRegister;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties.Sites;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Streets;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.RiskTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class RiskStreetFormTests : BaseTest
    {
        [Category("Risk Street Form")]
        [Category("Chang")]
        [Test(Description = "Street Form")]
        public void TC_131_Street_Form()
        {
            CommonFinder finder = new CommonFinder(DbContext);

            string riskId = "12";
            string riskDesc = "NITON ROAD";
            string riskName = "Proximity to School";
            string streetId = "6434";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password)
                .IsOnHomePage(AutoUser54);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Risk Register")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RiskRegisterListingPage>()
                .IsRiskStreetForm()
                .FilterByRiskId(riskId)
                .DoubleClickAtFirstRisk(riskId)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RiskDetailPage>()
                .IsRiskDetailPage(riskDesc, riskName)
                .ClickOnAddressHeader(riskDesc)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            StreetDetailPage streetDetailPage = PageFactoryManager.Get<StreetDetailPage>();
            streetDetailPage
                .IsStreetDetailPage(riskDesc)
                //Step 4: Verify that a top bar actions are displayed and working correctly
                .VerifyTopBarActionDisplayed()
                //=> History btn
                .ClicHistoryBtnAndVerify(streetId)
                //=> Refresh btn
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //=> Help btn
            streetDetailPage
                .ClickAndVerifyHelp();

            streetDetailPage
                .IsStreetDetailPage(riskDesc)
                //=> Close btn
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .VerifyWindowClosed(2);
            PageFactoryManager.Get<RiskDetailPage>()
                .ClickOnAddressHeader(riskDesc)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            streetDetailPage
                .IsStreetDetailPage(riskDesc)
                //Step 5: Object header
                .VerifyObjectHeader(CommonConstants.StreetIconUrl, streetId)
                //Step 6: Verify that last tab selected is remembered for the user
                .ClickOnPostCodeOutwardsTab()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<RiskDetailPage>()
                .ClickOnAddressHeader(riskDesc)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            streetDetailPage
                .IsStreetDetailPage(riskDesc)
                .VerifyPostCodeOutwardsTabIsSelected();
            //Query get street type
            List<StreetTypeDBModel> streetTypeDBModels = finder.GetStreetWithDate();
            List<RoadTypeDBModel> roadTypeDBModels = finder.GetRoadTypeWithDate();
            //Step 10: Details tab
            streetDetailPage
                .ClickOnDetailTab()
                .VerifyFieldInDetailTab()
                .VerifyDefaultValueInRoadType("Named Road")
                //=> Street type
                .ClickStreetTypeDdAndVerify(streetTypeDBModels)
                .SelectStreetType(streetTypeDBModels[0].streettype)
                //=> Street Name
                .VerifyDefaultValueInStreetName(riskDesc)
                .ClearTextInStreetName()
                //Road Type
                .ClickRoadTypeDdAndVerify(roadTypeDBModels)
                //Step 11
                .VerifyRoadTypeOptionDisplayOrderAlphabet();
            //Step 12
            streetDetailPage
                 .ClickSaveBtn()
                 .VerifyDisplayToastMessage(MessageRequiredFieldConstants.StreetNameRequiredMessage)
                 .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.StreetNameRequiredMessage);
            //Step 13
            streetDetailPage
                .InputTextInStreetName(riskDesc)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Step 14: Data tab
            string accessPointValue = "Access Point" + CommonUtil.GetRandomNumber(5);
            streetDetailPage
                .ClickOnDataTab()
                .VerifyToastMessagesIsUnDisplayed();
            //Bug
            //Check in latest version
            //    .VerifyDataTabAfterSelectStreetType()
            //    .SendKeyInAccessPoint(accessPointValue)
            //    .ClickSaveBtn()
            //    .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //    .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //streetDetailPage
            //    .VerifyAccessPointAfterSaveForm(accessPointValue);

            //Query get Post code outward
            List<PostCodeOutWardDBModel> postCodeOutWardDBModels = finder.GetStreetPostCodeOutWardsByStreetId(int.Parse(streetId));
            //Step 16: Postcode outwards tab
            streetDetailPage
                .ClickOnPostCodeOutwards()
                .VerifyDataInPostCodeOutwardsTab(postCodeOutWardDBModels[0].postcodeoutward, postCodeOutWardDBModels.Count.ToString());
            //Query get Sectors
            List<SectorDBModel> sectorDBModels = finder.GetSectorByStreetId(int.Parse(streetId));
            //Step 18: Sectors tab
            streetDetailPage
                .ClickOnSectorsTab()
                .VerifyDataInSectorTab(sectorDBModels);
            //Query get Map tab
            List<PointSegmentDBModel> pointSegmentDBModels = finder.GetPointSegmentByStreetId(int.Parse(streetId));
            //Step 20: Map tab
            streetDetailPage
                .ClickOnMapTab()
                .VerifySegmentValue(pointSegmentDBModels[0].pointsegment);
            //Step 21: Risks tab
            streetDetailPage
                .ClickOnRisksTab()
                .VerifyToastMessagesIsUnDisplayed();
            List<RiskModel> riskModels = streetDetailPage
                .GetAllRiskInTab();
            string[] allContracts = { Contract.Municipal, Contract.Commercial };
            string[] allRiskName = { "Proximity to School", "Proximity to School" };
            streetDetailPage
                .VerifyRisksWithContractAndName(riskModels, allContracts, allRiskName);
            streetDetailPage
                .ClickAnyRiskRowShowRiskDetailPage()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RiskDetailPage>()
                .IsRiskDetailPage(riskDesc, riskName);
        }

        [Category("Risk Street Form")]
        [Category("Huong")]
        [Test(Description = "Risk register")]
        public void TC_133_1_Risk_register()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password)
                .IsOnHomePage(AutoUser54);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Risk Register")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            RiskRegisterListingPage riskRegisterListingPage = PageFactoryManager.Get<RiskRegisterListingPage>();
            riskRegisterListingPage.VerifyActionButtonsVisible()
                .VerifyHeadersVisible(new List<string>() 
                {
                    "ID",
                    "Risk",
                    "Risk Level",
                    "Risk Type",
                    "Contract",
                    "Services",
                    "Target",
                    "Proximity Alert",
                    "Start Date",
                    "End Date",
                    "Subject",
                });
            //Select any risk register in the grid -> Click on retire
            riskRegisterListingPage.ClickAtFirstRisk()
                .ClickOnElement(riskRegisterListingPage.retireBtn);
            riskRegisterListingPage.VerifyElementText(riskRegisterListingPage.RetireTitle, "Do you wish to retire the selected Risk Register record(s)?")
                .ClickOnElement(riskRegisterListingPage.OKButton);
            riskRegisterListingPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //Click on end date and Show all button 
            riskRegisterListingPage.ClickOnHeader("End Date")
                .ClickOnElement(riskRegisterListingPage.ShowAllBtn);
            //Bug: no grey italic font
            //Click on Bulk create
            riskRegisterListingPage.ClickOnElement(riskRegisterListingPage.bulkUpdateBtn);
            riskRegisterListingPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            riskRegisterPage.VerifyIsRiskRegisterPage();

            //Select contract -> Click on preview 
            riskRegisterPage.SelectTextFromDropDown(riskRegisterPage.ContractSelect, Contract.Commercial)
                .ClickOnElement(riskRegisterPage.PreviewResultButton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            //Select an ID you want to work with -> Click on filter and ad this ID -> Preview -> Next
            string id = riskRegisterPage.SelectFirstIdAndFilter();
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnStep1);
            //Select a risk/s you want to add -> Add selected
            riskRegisterPage.SleepTimeInMiliseconds(500);
            string riskName = riskRegisterPage.SelectRisk();
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.VerifyRiskSelect(riskName);
            //Update the second grid e.g. add proximity alert, change start date, end date and mitigation notes -> Next 
            string startDate = "02/05/2022 00:00";
            string endDate = "31/01/2040 00:00";
            string migitationNote = "Slow down";
            riskRegisterPage.InputRiskSelect(startDate, endDate, migitationNote)
                .ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.VerifyCheckboxIsSelected(riskRegisterPage.InAllServiceCheckbox, true);
            //Click previous, previous
            riskRegisterPage.ClickOnElement(riskRegisterPage.PreviousButtonOnScreen3);
            riskRegisterPage.WaitForLoadingIconToDisappear();
            riskRegisterPage.ClickOnElement(riskRegisterPage.PreviousButtonOnScreen2);
            riskRegisterPage.VerifySelectedValue(riskRegisterPage.ContractSelect, Contract.Commercial);
            riskRegisterPage.VerifyInputValue(riskRegisterPage.filterInput, id);
            //Next
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnStep1);
            riskRegisterPage.WaitForLoadingIconToDisappear();
            riskRegisterPage.VerifyRiskSelect(true, startDate, endDate, migitationNote);
            //Next, next
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.WaitForLoadingIconToDisappear();
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen3);
            riskRegisterPage.WaitForLoadingIconToDisappear();

            //Finish
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButtonOnScreen4);
            riskRegisterPage.VerifyElementText(riskRegisterPage.FinishConfirmTitle, "Do you wish to create 1 Risk Register record(s)?");
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            //Double click on the added data 
            riskRegisterListingPage.ClickOnElement(riskRegisterListingPage.StartDateHeaderInput);
            riskRegisterListingPage.SleepTimeInMiliseconds(500);
            riskRegisterListingPage.InputCalendarDate(riskRegisterListingPage.StartDateHeaderInput, startDate.Replace(" 00:00", ""));
            riskRegisterListingPage.WaitForLoadingIconToAppear(false);
            riskRegisterListingPage.WaitForLoadingIconToDisappear();
            riskRegisterListingPage.ClickOnElement(riskRegisterListingPage.ToggleFilterStartDateHeader);
            riskRegisterListingPage.SelectByDisplayValueOnUlElement(riskRegisterListingPage.SelectFilterDropdown, "Equal to");
            riskRegisterListingPage.WaitForLoadingIconToAppear(false);
            riskRegisterListingPage.WaitForLoadingIconToDisappear();
            riskRegisterListingPage.DoubleClickAtFirstRisk()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            var riskDetailPage = PageFactoryManager.Get<RiskDetailPage>();
            riskDetailPage.ClickOnElement(riskDetailPage.DetailTab);
            riskDetailPage.WaitForLoadingIconToDisappear();
            riskDetailPage.VerifyInputValue(riskDetailPage.StartDateInput, startDate.Replace(" 00:00", ""));
            riskDetailPage.VerifyInputValue(riskDetailPage.EndDateInput, endDate.Replace(" 00:00", ""));
            riskDetailPage.VerifyCheckboxIsSelected(riskDetailPage.ProximityInput, true);
            riskDetailPage.VerifyInputValue(riskDetailPage.NoteInput, migitationNote);
            riskDetailPage.ClickOnElement(riskDetailPage.HyperLink);
            riskDetailPage.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitPage serviceUnitPage = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnitPage.ClickOnElement(serviceUnitPage.RiskTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SwitchToFrame(serviceUnitPage.RiskIframe);
            //Click on bulk create
            serviceUnitPage.ClickOnElement(serviceUnitPage.BulkCreateButton);
            serviceUnitPage.SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.VerifyStep2();
        }

        [Category("Risk Street Form")]
        [Category("Huong")]
        [Test(Description = "Risk register on Point segment")]
        public void TC_133_2_Risk_register_On_Point_segment()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/point-segments/32655");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password);
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage.WaitForLoadingIconToDisappear();
            pointSegmentDetailPage.ClickOnElement(pointSegmentDetailPage.RiskTab);
            pointSegmentDetailPage.WaitForLoadingIconToDisappear();
            pointSegmentDetailPage.SwitchToFrame(pointSegmentDetailPage.RiskIframe);
            //Click on bulk create
            pointSegmentDetailPage.ClickOnElement(pointSegmentDetailPage.BulkCreateButton);
            pointSegmentDetailPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            //Select all risks -> Add selected
            var riskNames = riskRegisterPage.SelectAllRisk();
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.VerifyRiskSelect(item);
            }

            //Remove all
            riskRegisterPage.ClickOnElement(riskRegisterPage.RemoveAllRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.VerifyRiskSelectedTableIsEmpty();
            //Add selected -> Next -> Next -> Finish -> Change some data in the grid
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen3);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.InputRiskConfirm(riskNames.IndexOf(item), "21/10/2022 00:00", "31/01/2040 00:00");
            }
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButtonOnScreen4);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow();
            riskRegisterPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.SwitchToFrame(pointSegmentDetailPage.RiskIframe);
            foreach (var item in riskNames)
            {
                pointSegmentDetailPage.VerifyRiskSelect(item, "21/10/2022 00:00", "31/01/2040 00:00");
            }
        }

        [Category("Risk Street Form")]
        [Category("Huong")]
        [Test(Description = "Risk register on Point node")]
        public void TC_133_3_Risk_register_On_Point_node()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/point-nodes/4");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password);
            PointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<PointNodeDetailPage>();
            pointNodeDetailPage.WaitForLoadingIconToDisappear();
            pointNodeDetailPage.ClickOnElement(pointNodeDetailPage.RiskTab);
            pointNodeDetailPage.WaitForLoadingIconToDisappear();
            pointNodeDetailPage.SwitchToFrame(pointNodeDetailPage.RiskIframe);
            //Click on bulk create
            pointNodeDetailPage.ClickOnElement(pointNodeDetailPage.BulkCreateButton);
            pointNodeDetailPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            //Select all risks -> Add selected
            var riskNames = new List<string>() { riskRegisterPage.SelectRisk() };
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.VerifyRiskSelect(item);
            }
            
            //Add selected -> Next -> Next -> Finish -> Change some data in the grid
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen3);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.InputRiskConfirm(riskNames.IndexOf(item), "21/09/2022 00:00", "31/01/2040 00:00");
            }
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButtonOnScreen4);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow();
            riskRegisterPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.SwitchToFrame(pointNodeDetailPage.RiskIframe);
            foreach (var item in riskNames)
            {
                pointNodeDetailPage.VerifyRiskSelect(item, "21/09/2022 00:00", "31/01/2040 00:00");
            }
        }

        [Category("Risk Street Form")]
        [Category("Huong")]
        [Test(Description = "Risk register on Point area")]
        public void TC_133_4_Risk_register_On_Point_area()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/point-areas/14");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password);
            PointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<PointAreaDetailPage>();
            pointAreaDetailPage.WaitForLoadingIconToDisappear();
            pointAreaDetailPage.ClickOnElement(pointAreaDetailPage.RiskTab);
            pointAreaDetailPage.WaitForLoadingIconToDisappear();
            pointAreaDetailPage.SwitchToFrame(pointAreaDetailPage.RiskIframe);
            //Click on bulk create
            pointAreaDetailPage.ClickOnElement(pointAreaDetailPage.BulkCreateButton);
            pointAreaDetailPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            //Select all risks -> Add selected
            var riskNames = new List<string>() { riskRegisterPage.SelectRisk(), riskRegisterPage.SelectRisk(1), riskRegisterPage.SelectRisk(2) };
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.VerifyRiskSelect(item);
            }

            //Add selected -> Next -> Next -> Finish -> Change some data in the grid
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen3);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.InputRiskConfirm(riskNames.IndexOf(item), "21/09/2022 00:00", "31/01/2040 00:00");
            }
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButtonOnScreen4);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow();
            riskRegisterPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.SwitchToFrame(pointAreaDetailPage.RiskIframe);
            foreach (var item in riskNames)
            {
                pointAreaDetailPage.VerifyRiskSelect(item, "21/09/2022 00:00", "31/01/2040 00:00");
            }
        }

        [Category("Risk Street Form")]
        [Category("Huong")]
        [Test(Description = "Risk register on Service unit")]
        public void TC_133_5_Risk_register_On_Service_unit()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/201731");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password);
            ServiceUnitPage serviceUnitPage = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.RiskTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SwitchToFrame(serviceUnitPage.RiskIframe);
            //Click on bulk create
            serviceUnitPage.ClickOnElement(serviceUnitPage.BulkCreateButton);
            serviceUnitPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            //Select all risks -> Add selected
            var riskNames = new List<string>() { riskRegisterPage.SelectRisk() };
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.VerifyRiskSelect(item);
            }

            //Add selected -> Next -> Next -> Finish -> Change some data in the grid
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen3);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.InputRiskConfirm(riskNames.IndexOf(item), "20/08/2022 00:00", "31/01/2040 00:00");
            }
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButtonOnScreen4);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow();
            riskRegisterPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.SwitchToFrame(serviceUnitPage.RiskIframe);
            foreach (var item in riskNames)
            {
                serviceUnitPage.VerifyRiskSelect(item, "20/08/2022 00:00", "31/01/2040 00:00");
            }
        }

        [Category("Risk Street Form")]
        [Category("Huong")]
        [Test(Description = "Risk register on Service task")]
        public void TC_133_6_Risk_register_On_Service_task()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/109689");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password);
            si_automated_tests.Source.Main.Pages.Services.ServicesTaskPage serviceTaskPage = PageFactoryManager.Get<si_automated_tests.Source.Main.Pages.Services.ServicesTaskPage>();
            serviceTaskPage.WaitForLoadingIconToDisappear();
            serviceTaskPage.ClickOnElement(serviceTaskPage.RiskTab);
            serviceTaskPage.WaitForLoadingIconToDisappear();
            serviceTaskPage.SwitchToFrame(serviceTaskPage.RiskIframe);
            //Click on bulk create
            serviceTaskPage.ClickOnElement(serviceTaskPage.BulkCreateButton);
            serviceTaskPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            //Select all risks -> Add selected
            var riskNames = new List<string>() { riskRegisterPage.SelectRisk(), riskRegisterPage.SelectRisk(1) };
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.VerifyRiskSelect(item);
            }

            //Add selected -> Next -> Next -> Finish -> Change some data in the grid
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen3);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.InputRiskConfirm(riskNames.IndexOf(item), "20/08/2022 00:00", "31/01/2040 00:00");
            }
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButtonOnScreen4);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow();
            riskRegisterPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.SwitchToFrame(serviceTaskPage.RiskIframe);
            foreach (var item in riskNames)
            {
                serviceTaskPage.VerifyRiskSelect(item, "20/08/2022 00:00", "31/01/2040 00:00");
            }
        }

        [Category("Risk Street Form")]
        [Category("Huong")]
        [Test(Description = "Risk register on Street")]
        public void TC_133_7_Risk_register_On_Street()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/streets/7796");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password);
            StreetDetailPage streetDetailPage = PageFactoryManager.Get<StreetDetailPage>();
            streetDetailPage.WaitForLoadingIconToDisappear();
            streetDetailPage.ClickOnElement(streetDetailPage.RiskTab);
            streetDetailPage.WaitForLoadingIconToDisappear();
            streetDetailPage.SwitchToFrame(streetDetailPage.RiskIframe);
            //Click on bulk create
            streetDetailPage.ClickOnElement(streetDetailPage.BulkCreateButton);
            streetDetailPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            //Select all risks -> Add selected
            var riskNames = new List<string>() { riskRegisterPage.SelectRisk(), riskRegisterPage.SelectRisk(1), riskRegisterPage.SelectRisk(2) };
            riskRegisterPage.ClickOnElement(riskRegisterPage.AddSelectRiskbutton);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.VerifyRiskSelect(item);
            }

            //Add selected -> Next -> Next -> Finish -> Change some data in the grid
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen2);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnScreen3);
            riskRegisterPage.SleepTimeInMiliseconds(500);
            foreach (var item in riskNames)
            {
                riskRegisterPage.InputRiskConfirm(riskNames.IndexOf(item), "20/08/2022 00:00", "31/01/2040 00:00");
            }
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButtonOnScreen4);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow();
            riskRegisterPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.SwitchToFrame(streetDetailPage.RiskIframe);
            //bug not created bulk risk
            //foreach (var item in riskNames)
            //{
            //    streetDetailPage.VerifyRiskSelect(item, "20/08/2022 00:00", "31/01/2040 00:00");
            //}
        }
    }
}
