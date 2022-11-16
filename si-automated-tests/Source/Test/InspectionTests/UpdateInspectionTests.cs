using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Inspections;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using System;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.InspectionTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class UpdateInspectionTests : BaseTest
    {
        private string inspectionIdCompleteType2;
        private string inspectionIdCancelledType2;

        //Need to confirm
        [Category("UpdateInspection"), Order(1)]
        [Category("Chang")]
        [Test(Description = "Inspection Update and states - Complete - InspectionType = 4")]
        public void TC_120_Inspection_update_and_states_complete_inspection_inspection_type_4()
        {
            string inspectionId = "323";
            string inspectionTypeValue = "Repeat Missed Assessment";
            string inpectionAddress = "4 RALEIGH ROAD, RICHMOND, TW9 2DX";
            string allocatedUnitValue = "Ancillary";

            PageFactoryManager.Get<LoginPage>()
                    .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser59.UserName)
                .SendKeyToPassword(AutoUser59.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser59);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                //Click on the hyperlink in the header and create 3 inspections of the same inspectionID = 4
                .ClickAddressLink(inpectionAddress)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage
                 .WaitForPointAddressDetailDisplayed();
            string locationValue = pointAddressDetailPage
                .GetPointAddressName();
            //Click [Inspect] btn - Create inspection 1
            pointAddressDetailPage
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            string noteValue = "InspectionNote120" + CommonUtil.GetRandomString(5);
            pointAddressDetailPage
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            //Click success link and execute line 31
            string noteUpdate = "UpdateInspectionNote120" + CommonUtil.GetRandomString(5);
            pointAddressDetailPage
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
               .VerifyStateInspection("Unallocated")
               .InputNote(noteUpdate)
               .ClickSaveBtn()
               .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .VerifyNoteValue(noteUpdate)
                //Line 32
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            string noteInDataTab = "NoteDataTab" + CommonUtil.GetRandomString(5);
            string accessPointDataTab = "AccessPoint" + CommonUtil.GetRandomString(5);
            detailInspectionPage
                .AddNotesInDataTab(noteInDataTab)
                //.UploadImage("")
                .AddAccessPointInDataTab(accessPointDataTab)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyValueInNoteInputDataTab(noteInDataTab)
                .VerifyValueInAccessPointInput(accessPointDataTab);
            //Line 33: Verify history
            detailInspectionPage
                .ClickOnHistoryTab();
            detailInspectionPage
                .VerifyRecordAfterUpdateAction(AutoUser59.DisplayName, noteUpdate, accessPointDataTab)
                .VerifyFirstNoteInHistoryTab(noteInDataTab);
            //Line 34 => Complete
            detailInspectionPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickCompleteBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string timeComplete = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailInspectionPage
                .VerifyAllFieldsInPopupDisabled()
                .VerifyStateInspection("Complete")
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            //Line 35 => Verify data tab
            detailInspectionPage
                .VerifyAllFieldsInDataTabDisabled();
            //Line 36 => Verify history tab
            detailInspectionPage
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyHistoryAfterCompleted(AutoUser59.DisplayName, timeComplete);
        }

        [Category("UpdateInspection")]
        [Category("Chang")]
        [Test(Description = "Inspection Update and states - Cancel - InspectionType = 4"), Order(2)]
        public void TC_120_Inspection_update_and_states_cancel_inspection_inspection_type_4()
        {
            string inspectionId = "323";
            string inspectionTypeValue = "Repeat Missed Assessment";
            string inpectionAddress = "4 RALEIGH ROAD, RICHMOND, TW9 2DX";
            string allocatedUnitValue = "Ancillary";

            PageFactoryManager.Get<LoginPage>()
                    .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser59.UserName)
                .SendKeyToPassword(AutoUser59.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser59);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                //Click on the hyperlink in the header and create 3 inspections of the same inspectionID = 4
                .ClickAddressLink(inpectionAddress)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage
                 .WaitForPointAddressDetailDisplayed();
            string locationValue = pointAddressDetailPage
                .GetPointAddressName();
            //Click [Inspect] btn - Create inspection 1
            pointAddressDetailPage
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            string noteValue = "InspectionNote120" + CommonUtil.GetRandomString(5);
            pointAddressDetailPage
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            //Click success link and execute line 31
            pointAddressDetailPage
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
               .VerifyStateInspection("Unallocated")
               .ClickOnDataTab()
               .WaitForLoadingIconToDisappear();
            string noteInDataTab = "NoteDataTab" + CommonUtil.GetRandomString(5);
            string accessPointDataTab = "AccessPoint" + CommonUtil.GetRandomString(5);
            detailInspectionPage
                .AddNotesInDataTab(noteInDataTab)
                .AddAccessPointInDataTab(accessPointDataTab)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            //Line 37 => Cancel
            detailInspectionPage
                .ClickCancelBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string timeCancelled = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailInspectionPage
                .VerifyAllFieldsInPopupDisabled()
                .VerifyStateInspection("Cancelled")
                //Line 38 => Verify Data tab
                .ClickOnDataTab()
                .VerifyValueInNoteInputDataTab(noteInDataTab)
                .VerifyValueInAccessPointInput(accessPointDataTab)
                .VerifyAllFieldsInDataTabDisabled()
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            //Line 39 => Verify History tab
            detailInspectionPage
                .VerifyHistoryAfterCancelled(AutoUser59.DisplayName, timeCancelled);
        }

        [Category("UpdateInspection")]
        [Category("Chang")]
        [Test(Description = "Inspection Update and states - Expire - InspectionType = 4"), Order(3)]
        public void TC_120_Inspection_update_and_states_Expire_inspection_inspection_type_4()
        {
            string inspectionId = "323";
            string inspectionTypeValue = "Repeat Missed Assessment";
            string inpectionAddress = "4 RALEIGH ROAD, RICHMOND, TW9 2DX";
            string allocatedUnitValue = "Ancillary";

            PageFactoryManager.Get<LoginPage>()
                    .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser59.UserName)
                .SendKeyToPassword(AutoUser59.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser59);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                //Click on the hyperlink in the header and create 3 inspections of the same inspectionID = 4
                .ClickAddressLink(inpectionAddress)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage
                 .WaitForPointAddressDetailDisplayed();
            string locationValue = pointAddressDetailPage
                .GetPointAddressName();
            //Click [Inspect] btn - Create inspection 1
            pointAddressDetailPage
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            string noteValue = "InspectionNote120" + CommonUtil.GetRandomString(5);
            pointAddressDetailPage
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            //Line 40 - Update time = now - 2 hours
            pointAddressDetailPage
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            string validToValue = CommonUtil.GetUtcTimeNowMinusHour(-2, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailInspectionPage
               //.VerifyStateInspection("Unallocated")
               .InputValidTo(validToValue)
               .ClickSaveBtn()
               .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .VerifyAllFieldsInPopupDisabled()
                .VerifyStateInspection("Expired")
                //Line 38 => Verify Data tab
                .ClickOnDataTab()
                .VerifyAllFieldsInDataTabDisabled()
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            //Line 39 => Verify History tab
            detailInspectionPage
                .VerifyHistoryAfterExpired(AutoUser59.DisplayName, validToValue);
        }

        [Category("UpdateInspection")]
        [Category("Chang")]
        [Test(Description = "Inspection Update and states - Complete - 1"), Order(4)]
        public void TC_120_Inspection_update_and_states_Complete_1_inspection_inspection_type_2()
        {
            string inspectionId = "4";
            string inspectionTypeValue = "Street Cleansing Assessment";
            string inpectionAddress = "Holly Road 64 To 70 Between Heath Road And Copthall Gardens";
            string allocatedUnitValue = "Ancillary";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser59.UserName)
                .SendKeyToPassword(AutoUser59.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser59);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                //Click on the hyperlink in the header and create 3 inspections of the same inspectionID = 4
                .ClickAddressLinkShowPointSegmentDetail(inpectionAddress)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed();
            string locationValue = pointSegmentDetailPage
                .GetPointSegmentName();
            //Click [Inspect] btn - Create inspection 1
            pointSegmentDetailPage
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            string noteValue = "InspectionNote120" + CommonUtil.GetRandomString(5);
            pointSegmentDetailPage
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                //Bug message
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            //Click success link and execute line 45 - Complete
            pointSegmentDetailPage
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyStateInspection("Unallocated")
                .ClickCompleteBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.FieldStreetGradeRequiredMessage);
            //line 46
            detailInspectionPage
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            inspectionIdCompleteType2 = detailInspectionPage
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", "");
            detailInspectionPage
                .VerifyStreetGradeMandatory()
                //Line 47 => Complete
                .SelectStreetGrade("A")
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickCompleteBtn();
            string timeCompleted = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailInspectionPage
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .VerifyStateInspection("Complete")
                .VerifyAllFieldsInPopupDisabled()
                .VerifyTimeInEndDateAndTimeField(timeCompleted)
                //Line 48 => Verify Data tab
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            string noteValueDataTab = "Note Data tab" + CommonUtil.GetRandomString(5);
            detailInspectionPage
                .VerifyFieldsInDataTabDisabled("A")
                .AddNotesInDataTab(noteValueDataTab)
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            //Line 49 => Verify History tab
            detailInspectionPage
                .VerifyHistoryAfterCompleted(AutoUser59.DisplayName, timeCompleted)
                .VerifyStreetGradeInHistory("A");
        }

        [Category("UpdateInspection")]
        [Category("Chang")]
        [Test(Description = "Inspection Update and states - Complete - 2"), Order(5)]
        public void TC_120_Inspection_update_and_states_Complete_2_inspection_inspection_type_2()
        {
            string inspectionId = "4";
            string inspectionTypeValue = "Street Cleansing Assessment";
            string inpectionAddress = "Holly Road 64 To 70 Between Heath Road And Copthall Gardens";
            string allocatedUnitValue = "East Waste";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser59.UserName)
                .SendKeyToPassword(AutoUser59.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser59);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                //Click on the hyperlink in the header and create 3 inspections of the same inspectionID = 4
                .ClickAddressLinkShowPointSegmentDetail(inpectionAddress)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed();
            string locationValue = pointSegmentDetailPage
                .GetPointSegmentName();
            //Click [Inspect] btn - Create inspection 1
            pointSegmentDetailPage
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            string noteValue = "InspectionNote120" + CommonUtil.GetRandomString(5);
            pointSegmentDetailPage
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                //Bug message
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            //Click success link and execute line 51 - Fill all fields in Data tab
            pointSegmentDetailPage
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyStateInspection("Unallocated")
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            string noteDataTab = "Note Data tab" + CommonUtil.GetRandomString(5);
            detailInspectionPage
                .ClickIssueFoundCheckbox()
                .AddNotesInDataTab(noteDataTab)
                .SelectStreetGrade("B")
                .ClickOnDetailTab()
                .JustClickCalenderEndDateAndTime();
            string timeCompleted = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailInspectionPage
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyStateInspection("Complete")
                .VerifyTimeInEndDateAndTimeField(timeCompleted)
                .VerifyAllFieldsInPopupDisabled();
            string newNoteDataTab = "New Note Data tab" + CommonUtil.GetRandomString(5);
            //Line 52 => Verify Data tab => Bug (Failed)
            detailInspectionPage
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyValueInNoteInputDataTab(noteDataTab)
                .VerifyIssueFoundCheckboxChecked()
                .VerifyFieldsInDataTabDisabled("B");
            //Line 53 => Verify History tab
            detailInspectionPage
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyHistoryAfterCompleted(AutoUser59.DisplayName, timeCompleted)
                .VerifyStreetGradeInHistory("B")
                .VerifyIssueFound("Ticked")
                .VerifyFirstNoteInHistoryTab(noteDataTab)
                .ClickOnDataTab()
                //Line 54 => Edit Note
                .AddNotesInDataTab(newNoteDataTab)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .ClickOnHistoryTab()
                .VerifyFirstNoteInHistoryTab(newNoteDataTab);

        }

        [Category("UpdateInspection")]
        [Category("Chang")]
        [Test(Description = "Inspection Update and states - Cancel"), Order(6)]
        public void TC_120_Inspection_update_and_states_Cancel_inspection_inspection_type_2()
        {
            string inspectionId = "4";
            string inspectionTypeValue = "Street Cleansing Assessment";
            string inpectionAddress = "Holly Road 64 To 70 Between Heath Road And Copthall Gardens";
            string allocatedUnitValue = "East Waste";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser59.UserName)
                .SendKeyToPassword(AutoUser59.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser59);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                //Click on the hyperlink in the header and create 3 inspections of the same inspectionID = 4
                .ClickAddressLinkShowPointSegmentDetail(inpectionAddress)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed();
            string locationValue = pointSegmentDetailPage
                .GetPointSegmentName();
            //Click [Inspect] btn - Create inspection 3
            pointSegmentDetailPage
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            string noteValue = "InspectionNote120" + CommonUtil.GetRandomString(5);
            pointSegmentDetailPage
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                //Bug message => Fix (07/07/2022)
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            //Click success link and execute line 56 - Click Detail tab and click cancelled date calendar icon
            pointSegmentDetailPage
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyStateInspection("Unallocated")
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            inspectionIdCancelledType2 = detailInspectionPage
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", "");
            detailInspectionPage
                .JustClickCalenderCancelledDate();
            string timeCancelled = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailInspectionPage
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            string noteDataTab = "Note data tab" + CommonUtil.GetRandomNumber(5);
            //Add note in note field
            detailInspectionPage
                .AddNotesInDataTab(noteDataTab)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.FieldStreetGradeRequiredMessage);
            detailInspectionPage
                .VerifyStreetGradeMandatory()
                //Line 57 => Complete
                .SelectStreetGrade("C")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            detailInspectionPage
                .VerifyValueInNoteInputDataTab(noteDataTab)
                .VerifyFieldsInDataTabDisabled("C")
                .VerifyStateInspection("Cancelled");
            //Line 58 => Verify History tab
            detailInspectionPage
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyHistoryAfterCancelled(AutoUser59.DisplayName, timeCancelled)
                .VerifyFirstNoteInHistoryTab(noteDataTab)
                .VerifyStreetGradeInHistory("C")
                //Line 59 => Verify Details tab
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyAllFieldsInPopupDisabled()
                .VerifyValueInCancelledDate(timeCancelled);
        }

        [Category("UpdateInspection")]
        [Category("Chang")]
        [Test(Description = "Inspection Update and states - Expired"), Order(7)]
        public void TC_120_Inspection_update_and_states_Expired_inspection_inspection_type_2()
        {
            string inspectionId = "4";
            string inspectionTypeValue = "Street Cleansing Assessment";
            string inpectionAddress = "Holly Road 64 To 70 Between Heath Road And Copthall Gardens";
            string allocatedUnitValue = "East Waste";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser59.UserName)
                .SendKeyToPassword(AutoUser59.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser59);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Inspections)
                .OpenOption("All Inspections")
                .SwitchNewIFrame();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .FilterInspectionById(inspectionId + Keys.Enter)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AllInspectionListingPage>()
                .DoubleClickFirstInspectionRow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyInspectionId(inspectionId.ToString())
                //Click on the hyperlink in the header and create 3 inspections of the same inspectionID = 4
                .ClickAddressLinkShowPointSegmentDetail(inpectionAddress)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed();
            string locationValue = pointSegmentDetailPage
                .GetPointSegmentName();
            //Click [Inspect] btn - Create inspection 3
            pointSegmentDetailPage
                .ClickInspectBtn()
                .WaitForLoadingIconToDisappear();
            string noteValue = "InspectionNote120" + CommonUtil.GetRandomString(5);
            pointSegmentDetailPage
                .IsCreateInspectionPopup()
                .VerifyDefaulValue()
                .VerifyDefaultSourceDd(locationValue)
                .ClickAndSelectInspectionType(inspectionTypeValue)
                .ClickAndSelectAllocatedUnit(allocatedUnitValue)
                .InputNote(noteValue)
                .ClickCreateBtn()
                //Bug message
                .VerifyToastMessage(MessageSuccessConstants.SaveInspectionCreatedMessage);
            //Click success link and execute line 59 - Update Valid to = now (UTC) - 2hours
            pointSegmentDetailPage
                .ClickOnInspectionCreatedLink()
                .SwitchToLastWindow();
            DetailInspectionPage detailInspectionPage = PageFactoryManager.Get<DetailInspectionPage>();
            detailInspectionPage
                .WaitForInspectionDetailDisplayed(inspectionTypeValue)
                .VerifyStateInspection("Unallocated")
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            //Get inspectionId
            string inspectionIdExpried = detailInspectionPage
                .GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/inspections/", "");
            string validToValue = CommonUtil.GetUtcTimeNowMinusHour(-2, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            detailInspectionPage
               .VerifyStateInspection("Unallocated")
               .InputValidTo(validToValue)
               .ClickSaveBtn()
               .VerifyDisplayToastMessage(MessageRequiredFieldConstants.FieldStreetGradeRequiredMessage);
            detailInspectionPage
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyStreetGradeMandatory()
                //Line 61 => Select Street Grade
                .SelectStreetGrade("D")
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify Details tab is read only
            detailInspectionPage
                .VerifyStateInspection("Expired")
                .VerifyAllFieldsInPopupDisabled()
                .VerifyValueInValidToField(validToValue)
                //Line 62 => Verify Data tab
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyFieldsInDataTabDisabled("D")
                .VerifyNotesFieldInDataTabReadOnly()
                //Line 63 => Verify History tab
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            detailInspectionPage
                .VerifyHistoryAfterExpired(AutoUser59.DisplayName, validToValue)
                .VerifyStreetGradeInHistory("D")
                .ClickCloseBtn()
                .SwitchToChildWindow(3);
            pointSegmentDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<DetailInspectionPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1);

            //Config
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.InpectionAdminRoleUser59UrlIE);
            
            PageFactoryManager.Get<UserDetailPage>()
                .IsOnUserDetailPage()
                .ClickAdminRoles()
                .ChooseInspectionAdminRole()
                .ClickSave()
                .WaitForLoadingIconDisappear();

            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.InspectionTypeUrlIE);
            PageFactoryManager.Get<SettingInspectionTypePage>()
                .WaitForInpsectionTypeSettingDisplayed("Street Cleansing Assessment")
                .ClickRolesTab()
                .SelectAllRoleInRightColumn()
                .ClickRemoveBtn()
                .ClickSaveBtnToUpdateRole();
            PageFactoryManager.Get<SettingInspectionTypePage>()
                .OpenDetailInspectionWithId(inspectionIdExpried)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed()
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            //Line 67 => Expired
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyFieldsInDataTabDisabled("D")
                .VerifyNotesFieldInDataTabReadOnly();
            //Line 69 => Cancelled
            PageFactoryManager.Get<DetailInspectionPage>()
                .OpenDetailInspectionWithId(inspectionIdCancelledType2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed()
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyFieldsInDataTabDisabled("C")
                .VerifyNotesFieldInDataTabReadOnly();
            //Line 71 => Completed
            PageFactoryManager.Get<DetailInspectionPage>()
                .OpenDetailInspectionWithId(inspectionIdCompleteType2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .WaitForInspectionDetailDisplayed()
                .ClickOnDataTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailInspectionPage>()
                .VerifyFieldsInDataTabDisabled("A")
                .VerifyNotesFieldInDataTabReadOnly();
        }

        [Category("Point address subscription")]
        [Category("Huong")]
        [Test()]
        public void TC_201_Subscriptions_grid_spelling_error()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/point-addresses/483986");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser59.UserName, AutoUser59.Password);
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage.ClickOnElement(pointAddressDetailPage.SubscriptionTab);
            pointAddressDetailPage.WaitForLoadingIconToDisappear()
                .SwitchToFrame(pointAddressDetailPage.SubscriptionIFrame);
            pointAddressDetailPage.ClickOnElement(pointAddressDetailPage.AddNewSubscriptionButton);
            pointAddressDetailPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            SubscriptionsDetailPage subscriptionsDetailPage = PageFactoryManager.Get<SubscriptionsDetailPage>();
            Random rnd = new Random();
            int number = rnd.Next(1, 13);
            string title = "Title" + number;
            string firstName = "FirstName" + number;
            string lastName = "LastName" + number;
            string mobile = "+4471274";
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.TitleInput, title);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.FirstNameInput, firstName);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.LastNameInput, lastName);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.MobileInput, mobile);
            subscriptionsDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            string subjectDescription = subscriptionsDetailPage.GetElementText(subscriptionsDetailPage.SubjectDescriptionText);
            string id = subscriptionsDetailPage.GetElementText(subscriptionsDetailPage.IdTitle);
            subscriptionsDetailPage.ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchToFrame(pointAddressDetailPage.SubscriptionIFrame);
            pointAddressDetailPage.VerifyNewSubscription(id, firstName, lastName, mobile, subjectDescription);
            string contractId = pointAddressDetailPage.GetNewContractId();
            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            var subscriptions = finder.GetSubscriptionById(id);
            Assert.IsTrue(subscriptions.Count != 0);
            var contracts = finder.GetContractById(contractId);
            Assert.IsTrue(contracts.Count != 0);

            System.Collections.Generic.List<string> columnNames = new System.Collections.Generic.List<string>() { "ID", "Contact ID", "Contact", "Mobile", "Subscription State", "Start Date", "End Date", "Notes", "Subject", "Subject Description" };
            //Navigate to point segment form URL: point - segments / 32799->Click on subscriptions tab
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/point-segments/32799");
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            pointSegmentDetailPage.WaitForLoadingIconToDisappear();
            pointSegmentDetailPage.ClickOnElement(pointSegmentDetailPage.SubscriptionTab);
            pointSegmentDetailPage.WaitForLoadingIconToDisappear()
                .SwitchToFrame(pointSegmentDetailPage.SubscriptionIFrame);
            pointSegmentDetailPage.VerifyColumnsDisplay(columnNames);

            //Navigate to point node form URL: point-nodes/5 -> Click on subscriptions tab
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/point-nodes/5");
            PointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<PointNodeDetailPage>();
            pointNodeDetailPage.WaitForLoadingIconToDisappear();
            pointNodeDetailPage.ClickOnElement(pointNodeDetailPage.SubscriptionTab);
            pointNodeDetailPage.WaitForLoadingIconToDisappear()
                .SwitchToFrame(pointNodeDetailPage.SubscriptionIFrame);
            pointNodeDetailPage.VerifyColumnsDisplay(columnNames);

            //Navigate to point area form URL: point-areas/5 -> Click on subscriptions tab
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/point-areas/5");
            PointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<PointAreaDetailPage>();
            pointAreaDetailPage.WaitForLoadingIconToDisappear();
            pointAreaDetailPage.ClickOnElement(pointAreaDetailPage.SubscriptionTab);
            pointAreaDetailPage.WaitForLoadingIconToDisappear()
                .SwitchToFrame(pointAreaDetailPage.SubscriptionIFrame);
            pointAreaDetailPage.VerifyColumnsDisplay(columnNames);

            //Navigate to service unit form URL: service-units/229980 -> Click on subscriptions tab
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-units/229980");
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage.WaitForLoadingIconToDisappear();
            serviceUnitDetailPage.ClickOnElement(serviceUnitDetailPage.SubscriptionTab);
            serviceUnitDetailPage.WaitForLoadingIconToDisappear()
                .SwitchToFrame(serviceUnitDetailPage.SubscriptionTabIframe);
            serviceUnitDetailPage.VerifyColumnsDisplay(columnNames);

            //Navigate to service task form URL: service-tasks/120785 -> Click on subscriptions tab
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120785");
            ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            servicesTaskPage.WaitForLoadingIconToDisappear();
            servicesTaskPage.ClickOnElement(servicesTaskPage.SubscriptionTab);
            servicesTaskPage.WaitForLoadingIconToDisappear()
                .SwitchToFrame(servicesTaskPage.SubscriptionIFrame);
            servicesTaskPage.VerifyColumnsDisplay(columnNames);

            //Navigate to task form URL: tasks/17075 -> Click on subscriptions tab
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/tasks/17075");
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage.WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnElement(detailTaskPage.SubscriptionTab);
            detailTaskPage.WaitForLoadingIconToDisappear()
                .SwitchToFrame(detailTaskPage.SubscriptionIFrame);
            detailTaskPage.VerifyColumnsDisplay(columnNames);
        }
    }
}
