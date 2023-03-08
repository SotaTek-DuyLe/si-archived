using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ServiceRecyclingTest : BaseTest
    {
        [Category("Recycling")]
        [Category("Chang")]
        [Test(Description = "Restrict Edit Feature on new style forms on Service Unit tab")]
        public void TC_128_Restrict_Edit_Feature_on_new_style_forms_on_Service_Unit()
        {
            string valueStreet = "COURT CLOSE AVENUE,TW2,TWICKENHAM";
            string serviceUnitId = "223695";
            string serviceName = "Communal Recycling";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOption("Recycling")
                .OpenOption(serviceName);
            ServiceRecyclingPage sectorRecycling = PageFactoryManager.Get<ServiceRecyclingPage>();
            sectorRecycling
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            sectorRecycling
                .WaitForServiceRecyclingPageLoaded(serviceName);

            //Verify that user is unable to update sections of the forms when the restrict edit is set in the service
            sectorRecycling
                .ClickOnElement(sectorRecycling.RestrictEditCheckbox);
            sectorRecycling
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            sectorRecycling
                .VerifyCheckboxIsSelected(sectorRecycling.RestrictEditCheckbox, true);

            //Service unit
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOption("Recycling")
                .ExpandOption("Communal Recycling")
                .OpenOption("Service Units");
            ServiceUnitPage serviceUnit = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnit.SwitchToFrame(serviceUnit.UnitIframe);
            serviceUnit.WaitForLoadingIconToDisappear();
            serviceUnit
                .FindServiceUnitWithId(serviceUnitId)
                .DoubleClickServiceUnit()
                .SwitchToChildWindow(2);

            ServiceUnitDetailPage serviceUnitDetail = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.DetailTab);
            //Step 2: Start and end date is read only. User is not able to reture the service unit form
            serviceUnitDetail.VerifyInputIsReadOnly(serviceUnitDetail.StartDateInput)
                .VerifyInputIsReadOnly(serviceUnitDetail.EndDateInput);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.retireBtn, false);

            //Update point segment => Verify [Street] update automatically
            serviceUnitDetail
                .ClickSearchPointSegmentBtn()
                .IsPointSegmentSearchPopup("Richmond")
                .SendKeyInStreetInput(valueStreet);

            string valuePointSegment = serviceUnitDetail
                .ClickSearchPointSegment()
                .GetValueInPointSegmentsDd();
            serviceUnitDetail
                .ClickSavePointSegmentSearchBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceUnitDetail
                .ClickRefreshHeaderBtn()
                .WaitForLoadingIconToDisappear();
            serviceUnitDetail
                .VerifyValueInPointSegmentDetailTab(valuePointSegment)
                .VerifyValueInStreetDetailTab(valueStreet);
            //Color + Client Ref
            //Step 3: Details: Update description, client reference, point segment, street, colour, service level->Save
            string valueServiceUnitInput = "service unit" + serviceUnitDetail.RandomString(3);
            serviceUnitDetail.SendKeys(serviceUnitDetail.ServiceUnitInput, valueServiceUnitInput);
            //Client ref
            string valueClientReferenceInput = "client reference" + serviceUnitDetail.RandomString(3);
            serviceUnitDetail.SendKeys(serviceUnitDetail.ClientReferenceInput, valueClientReferenceInput);
            serviceUnitDetail.SendKeys(serviceUnitDetail.ColorInput, "#752f75");
            serviceUnitDetail
                .SelectRandomServiceLevel()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            serviceUnitDetail
                .VerifyValueInServiceUnitAfterUpdating(valueServiceUnitInput);
            serviceUnitDetail
                .VerifyValueInClientRefAfterUpdating(valueClientReferenceInput);
            serviceUnitDetail
                .VerifyValueInColorAfterUpdating("#752f75");

            //step 4: Data tab: Update all data->Save => Bug: Cannot click on [Save] button
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.DataTab);
            string noteInputValue = serviceUnitDetail.RandomString(5);
            serviceUnitDetail.SendKeys(serviceUnitDetail.NoteInput, noteInputValue);
            string accessPointValue = serviceUnitDetail.RandomString(5);
            serviceUnitDetail.SendKeys(serviceUnitDetail.AccessPointInput, accessPointValue);
            serviceUnitDetail.ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            serviceUnitDetail.VerifyInputValue(serviceUnitDetail.NoteInput, noteInputValue)
                .VerifyInputValue(serviceUnitDetail.AccessPointInput, accessPointValue);

            //step 5: Scheduled service tasks tab: Click on add new item
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceTaskScheduleTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.VerifyElementEnable(serviceUnitDetail.AddNewItemButton, false);

            //step 7: Service unit points tab: Click on add points, change type and qualifier in the grid
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceUnitPointTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddPointButton);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.AddServiceUnitPointDiv, true)
                .ClickOnElement(serviceUnitDetail.AddServiceUnitPointCloseButton);
            //step 8: Double click on SUP
            serviceUnitDetail
                .VerifyStartDateAndEndDateIsReadOnly()
                .DoubleClickServiceUnitPoint(0)
                .SwitchToChildWindow(3);

            ServiceUnitPointDetailPage serviceUnitPointDetail = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitPointDetail.VerifyElementVisibility(serviceUnitPointDetail.RetireButton, false);
            serviceUnitPointDetail.ClickCloseBtn()
                .SwitchToChildWindow(2);
            //step 9: Update SUP type->Save
            serviceUnitDetail
                .EditServiceUnitPoint(0, "Point of Service", "")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //step 10: Click on Assets tab: Add new, add existing asset, delete item
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AssetsTab);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddNewAssetItemButton);
            serviceUnitDetail.SwitchToChildWindow(3);

            AssetDetailItemPage assetDetailItemPage = PageFactoryManager.Get<AssetDetailItemPage>();
            assetDetailItemPage.WaitForLoadingIconToDisappear();
            Random rnd = new Random();
            string assetValue = "Asset" + rnd.Next(10);
            string assetReferenceValue = "Asset Reference" + rnd.Next(10);
            string assetType = "660L";
            string state = "On Site";
            string product = "Food";
            assetDetailItemPage.SendKeys(assetDetailItemPage.AssetInput, assetValue);
            assetDetailItemPage
                .SelectTextFromDropDown(assetDetailItemPage.AssetTypeSelect, assetType)
                .SelectTextFromDropDown(assetDetailItemPage.ProductSelect, product)
                .SelectTextFromDropDown(assetDetailItemPage.StateSelect, state)
                .SelectTextFromDropDown(assetDetailItemPage.AgreementLineSelect, "")
                .SendKeys(assetDetailItemPage.AssetReferenceInput, assetReferenceValue);
            assetDetailItemPage.ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            serviceUnitDetail.ClickRefreshBtn();
            serviceUnitDetail.VerifyAssetAddedByAddNewItemButton(assetValue, assetType, state, product, assetReferenceValue);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddExistAssetButton);
            serviceUnitDetail.SelectByDisplayValueOnUlElement(serviceUnitDetail.BinsSelect, "660L")
                .SleepTimeInMiliseconds(1000);
            string assetType2 = "660L_19";
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.MainAssetDropDownButton);
           // serviceUnitDetail
           //     .SelectByDisplayValueOnUlElement(serviceUnitDetail.MainAssetSelect, assetType2);
            //serviceUnitDetail.ClickOnElement(serviceUnitDetail.ConfirmButton);
            //serviceUnitDetail.VerifyToastMessage("Successfully saved Asset");
            serviceUnitDetail
                .SelectAnyExistingAsset(assetType2);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ConfirmButton);
            serviceUnitDetail.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            serviceUnitDetail.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceUnitDetail.VerifyAssetAddedByAddExistItemButton("660L")
                .ClickAssetCheckBox(0)
                .ClickOnElement(serviceUnitDetail.DeleteAssetItemButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            //step 11: User can do all actions in announcement, map, risks, subspriptions, notifications and rental asset tabs
            //announcements - tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AnnouncementTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail
                .WaitForLoadingIconInAnnouncementTabDisappear();
            serviceUnitDetail
                .ClickOnElement(serviceUnitDetail.AddNewAnnouncementItemButton);
            serviceUnitDetail.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear(false);
            AnnouncementDetailPage announcementDetailPage = PageFactoryManager.Get<AnnouncementDetailPage>();
            string announcement = "Announcement text";
            string announcementType = "Collection services";
            announcementDetailPage
                .IsOnDetailPage();
            announcementDetailPage.SelectTextFromDropDown(announcementDetailPage.announcementTypeSelect, announcementType);
            announcementDetailPage.SendKeys(announcementDetailPage.announcemenTextInput, announcement);
            announcementDetailPage.SelectTextFromDropDown(announcementDetailPage.impactSelect, "Positive");
            string from = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string to = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, 1);
            announcementDetailPage.SendKeys(announcementDetailPage.validFromInput, from);
            announcementDetailPage.SendKeys(announcementDetailPage.valiToInput, to);
            announcementDetailPage.ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            serviceUnitDetail.VerifyNewAnnouncement(announcement, announcementType, from, to);
            serviceUnitDetail.ClickAnnouncementCheckbox(0);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.DeleteAnnouncementItemButton);
            serviceUnitDetail.SwitchToChildWindow(3);
            AnnouncementRemovePage announcementRemovePage = PageFactoryManager.Get<AnnouncementRemovePage>();
            announcementRemovePage.ClickOnElement(announcementRemovePage.YesButton);
            announcementRemovePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(2);
            serviceUnitDetail.VerifyAnnouncementDeleted(announcement);

            //map-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.MapTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail
                .VerifyNotDisplayErrorMessage();
            //serviceUnitDetail.ClickOnResetMapBtn();
            //serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            //serviceUnitDetail.ClickOnSaveMapBtn();
            //serviceUnitDetail.VerifyToastMessage("Success")
            //    .WaitUntilToastMessageInvisible("Success");

            //risk-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.RiskTab);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.RiskTabIframe);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.BulkCreateButton);
            serviceUnitDetail.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            RiskRegisterPage riskRegisterPage = PageFactoryManager.Get<RiskRegisterPage>();
            riskRegisterPage.SelectRiskCheckbox(0)
                .ClickOnElement(riskRegisterPage.AddSelectedButton);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnEditRisk);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnSelectServices);
            RiskRegisterModel riskRegisterModel = riskRegisterPage.GetReviewRiskData();
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButton);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(2)
                .SwitchToFrame(serviceUnitDetail.RiskTabIframe);
            serviceUnitDetail.VerifyNewRiskRegister(riskRegisterModel);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ShowAllButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ShowActiveButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.VerifyRiskRegisterNotDisplayExpiredRecord();
            serviceUnitDetail.SwitchToDefaultContent();
            //subspriptions-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SubscriptionTab);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.SubscriptionTabIframe);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddNewSubscriptionItemButton);
            serviceUnitDetail.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            SubscriptionsDetailPage subscriptionsDetailPage = PageFactoryManager.Get<SubscriptionsDetailPage>();
            string title = "Serivce Unit title";
            string firstName = "Serivce Unit FirstName";
            string lastName = "Serivce Unit LastName";
            string position = "Serivce Unit Position";
            string telephone = "+44 1274 496 0572";
            string mobile = "+4471274";
            string notes = "Service Unit Notes";
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.TitleInput, title);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.FirstNameInput, firstName);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.LastNameInput, lastName);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.PositionInput, position);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.TelephoneInput, telephone);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.MobileInput, mobile);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.NotesInput, notes);
            subscriptionsDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.SubscriptionTabIframe);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SubscriptionRefreshButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear();
            serviceUnitDetail.VerifyNewSubscription(firstName + " " + lastName, mobile);
            serviceUnitDetail.SwitchToDefaultContent();
            //notifications-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.NotificationTab);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.Notificationiframe);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.NotificationRefreshButton, true);
            serviceUnitDetail
                .CloseCurrentWindow()
                .SwitchToChildWindow(1);

            //step 12:Verify that start and end date is read only and retire button is disabled/hidden
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/111751");
            var serviceTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            serviceTaskPage.WaitForLoadingIconToDisappear();
            serviceTaskPage
                .IsServiceTaskPage();
            serviceTaskPage.ClickOnElement(serviceTaskPage.DetailTab);
            serviceTaskPage.VerifyElementEnable(serviceTaskPage.StartDateInput, false);
            serviceTaskPage.VerifyElementEnable(serviceTaskPage.EndDateInput, false);
            serviceTaskPage.VerifyElementVisibility(serviceTaskPage.RetireButton, false);
            //Verify that all other updates can be done to the service task form details tab

            string fromDate = DateTime.Now.ToString("dd/MM/yyyy");
            string toDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            string color = "#912f91";
            string reference = "Reference";
            string taskCount = "3";
            string maxTasks = "10";
            string maxTaskStartDate = DateTime.Now.ToString("dd/MM/yyyy");
            string taskIndicator = "Repeat Missed";
            string indicatorStartDate = DateTime.Now.ToString("dd/MM/yyyy");
            string indicatorEndDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            string taskNote = "Task Note";
            serviceTaskPage.SelectIndexFromDropDown(serviceTaskPage.PrioritySelect, 0)
                .ClickOnElement(serviceTaskPage.AssuredTaskCheckbox);
            serviceTaskPage.SendKeys(serviceTaskPage.AssuredFromInput, fromDate);
            serviceTaskPage.SendKeys(serviceTaskPage.AssuredToInput, toDate);
            serviceTaskPage.SendKeys(serviceTaskPage.ColorInput, color);
            serviceTaskPage.SelectIndexFromDropDown(serviceTaskPage.TagSelect, 0)
                .SendKeys(serviceTaskPage.ReferenceInput, reference);
            serviceTaskPage.ClickOnElement(serviceTaskPage.ProximityAlertCheckbox);
            serviceTaskPage.SendKeys(serviceTaskPage.TaskCountInput, taskCount);
            serviceTaskPage.SendKeys(serviceTaskPage.MaxTaskInput, maxTasks);
            serviceTaskPage.SendKeys(serviceTaskPage.MaxTaskStartDateInput, maxTaskStartDate);
            serviceTaskPage.SelectTextFromDropDown(serviceTaskPage.TaskIndicatorSelect, taskIndicator);
            serviceTaskPage.SendKeys(serviceTaskPage.IndicatorStartDateInput, indicatorStartDate);
            serviceTaskPage.SendKeys(serviceTaskPage.IndicatorEndDateInput, indicatorEndDate);
            serviceTaskPage.SendKeys(serviceTaskPage.TaskNoteInput, taskNote);
            serviceTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            //verify updated data
            serviceTaskPage.VerifySelectedValue(serviceTaskPage.PrioritySelect, "")
                .VerifyCheckboxIsSelected(serviceTaskPage.AssuredTaskCheckbox, true)
                .VerifyInputValue(serviceTaskPage.ColorInput, color)
                .VerifySelectedValue(serviceTaskPage.TagSelect, "")
                .VerifyInputValue(serviceTaskPage.ReferenceInput, reference)
                .VerifyCheckboxIsSelected(serviceTaskPage.ProximityAlertCheckbox, true)
                .VerifyInputValue(serviceTaskPage.TaskCountInput, taskCount)
                .VerifyInputValue(serviceTaskPage.MaxTaskInput, maxTasks)
                .VerifyInputValue(serviceTaskPage.MaxTaskStartDateInput, maxTaskStartDate)
                .VerifySelectedValue(serviceTaskPage.TaskIndicatorSelect, taskIndicator)
                .VerifyInputValue(serviceTaskPage.IndicatorStartDateInput, indicatorStartDate)
                .VerifyInputValue(serviceTaskPage.IndicatorEndDateInput, indicatorEndDate)
                .VerifyInputValue(serviceTaskPage.TaskNoteInput, taskNote);

            //Step line 14: Click on create adhoc task
            serviceTaskPage.ClickOnElement(serviceTaskPage.CreateAdHocTaskButton);
            serviceTaskPage.SwitchToChildWindow(2);
            var createAdHocTaskPage = PageFactoryManager.Get<CreateAdHocTaskPage>();
            createAdHocTaskPage.WaitForLoadingIconToDisappear();
            createAdHocTaskPage
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            createAdHocTaskPage
                .waitForLoadingIconDisappear();
            createAdHocTaskPage
                .VerifyAdHocTaskIsCreated()
                .CloseCurrentWindow()
                .SwitchToChildWindow(1);

            //Click on task lines tab: verify all actions and grid is read only
            serviceTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            var taskLinePage = PageFactoryManager.Get<ServiceTaskLineTab>();
            taskLinePage.VerifyTaskLineIsReadOnly()
                .VerifyElementEnable(taskLinePage.AddNewItemButton, false);

            //Announcement tab
            serviceTaskPage.ClickOnElement(serviceTaskPage.AnnouncementTab);
            serviceTaskPage.WaitForLoadingIconToDisappear();
            var announcementTaskTab = PageFactoryManager.Get<AnnouncementTaskTab>();
            announcementTaskTab.ClickOnElement(announcementTaskTab.AddNewItemButton);
            announcementTaskTab.SwitchToChildWindow(2);
            PageFactoryManager.Get<AnnouncementDetailPage>()
                .WaitForLoadingIconToDisappear();
            string text = "Test announcement " + CommonUtil.GetRandomNumber(5);
            string impact = "Positive";
            PageFactoryManager.Get<AnnouncementDetailPage>()
                .InputDetails("Collection services", text, impact, from, to)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToChildWindow(1);
            announcementTaskTab.VerifyAnnouncementTaskData(text, "Collection services", from, to);

            //Schedules tab: Add new item and retire 
            serviceTaskPage.ClickOnElement(serviceTaskPage.ScheduleTab);
            serviceTaskPage.WaitForLoadingIconToDisappear();
            var scheduleTaskTab = PageFactoryManager.Get<ScheduleTaskTab>();
            scheduleTaskTab
                .VerifyTableIsReadonly()
                .VerifyElementEnable(scheduleTaskTab.AddNewItemButton, false);

            //Double click on any schedule
            scheduleTaskTab.DoubleClickSchedule(1)
                .SwitchToChildWindow(2);
            var serviceTaskSchedulePage = PageFactoryManager.Get<ServiceTaskSchedulePage>();
            serviceTaskSchedulePage.WaitForLoadingIconToDisappear();
            serviceTaskSchedulePage.VerifyElementEnable(serviceTaskSchedulePage.StartDateInput, false)
                .VerifyElementEnable(serviceTaskSchedulePage.EndDateInput, false);
            serviceTaskSchedulePage
                .SelectTimeBand();
            serviceTaskSchedulePage
                .ClickOnElement(serviceTaskSchedulePage.UseRoundScheduleRadio);
            serviceTaskSchedulePage
                .SelectTextFromDropDown(serviceTaskSchedulePage.RoundSelect, "WCREC2:Tuesday")
                .SelectTextFromDropDown(serviceTaskSchedulePage.RoundLegSelect, "GLEBE SIDE TW1")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);

            //Data, history, risks, subscriptions, notifications and indicators tab
            //map-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.MapTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ResetMapButton);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SaveMapButton);
            serviceUnitDetail
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            //risk-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.RiskTab);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.RiskTabIframe);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.BulkCreateButton);
            serviceUnitDetail.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            riskRegisterPage.SelectRiskCheckbox(0)
                .ClickOnElement(riskRegisterPage.AddSelectedButton);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnEditRisk);
            riskRegisterPage.ClickOnElement(riskRegisterPage.NextButtonOnSelectServices);
            riskRegisterPage.ClickOnElement(riskRegisterPage.FinishButton);
            riskRegisterPage.ClickOnElement(riskRegisterPage.OKButton);
            riskRegisterPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(1)
                .SwitchToFrame(serviceUnitDetail.RiskTabIframe);
            serviceUnitDetail.VerifyNewRiskRegister(riskRegisterModel);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ShowAllButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ShowActiveButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.VerifyRiskRegisterNotDisplayExpiredRecord();
            serviceUnitDetail.SwitchToDefaultContent();
            //subspriptions-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SubscriptionTab);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.SubscriptionTabIframe);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddNewSubscriptionItemButton);
            serviceUnitDetail.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.TitleInput, title);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.FirstNameInput, firstName);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.LastNameInput, lastName);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.PositionInput, position);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.TelephoneInput, telephone);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.MobileInput, mobile);
            subscriptionsDetailPage.SendKeys(subscriptionsDetailPage.NotesInput, notes);
            subscriptionsDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.SubscriptionTabIframe);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SubscriptionRefreshButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear();
            serviceUnitDetail.VerifyNewSubscription(firstName + " " + lastName, mobile);
            serviceUnitDetail.SwitchToDefaultContent();
            //notifications-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.NotificationTab);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.Notificationiframe);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.NotificationRefreshButton, true);
            serviceUnitDetail.SwitchToDefaultContent();
            //indicator-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.IndicatorTab);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.IndicatorIframe);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.IndicatorAddNewItemButton);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SelectIndicatorButton);
            serviceUnitDetail.SelectByDisplayValueOnUlElement(serviceUnitDetail.IndicatorUl, "Assisted");
            serviceUnitDetail.SleepTimeInMiliseconds(200);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.IndicatorConfirmButton);
            serviceUnitDetail.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            serviceUnitDetail.SwitchToDefaultContent();

            //Service task line
            serviceTaskPage.ClickOnTaskLineTab()
                .WaitForLoadingIconToDisappear();
            taskLinePage.DoubleClickTaskLine(0)
                .SwitchToChildWindow(2);
            var serviceTaskLinePage = PageFactoryManager.Get<ServiceTaskLinePage>();
            serviceTaskLinePage.WaitForLoadingIconToDisappear();
            serviceTaskLinePage.VerifyElementEnable(serviceTaskLinePage.TaskLineTypeSelect, false)
                .VerifyElementEnable(serviceTaskLinePage.AssetTypeSelect, false)
                .VerifyElementEnable(serviceTaskLinePage.MinAssetQtyInput, false)
                .VerifyElementEnable(serviceTaskLinePage.MaxAssetQtyInput, false)
                .VerifyElementEnable(serviceTaskLinePage.ScheduleAssetQtyInput, false)
                .VerifyElementEnable(serviceTaskLinePage.ProductSelect, false)
                .VerifyElementEnable(serviceTaskLinePage.MinProductQtyInput, false)
                .VerifyElementEnable(serviceTaskLinePage.MaxProductQtyInput, false)
                .VerifyElementEnable(serviceTaskLinePage.ScheduleProductQtyInput, false)
                .VerifyElementEnable(serviceTaskLinePage.ProductUnitSelect, false)
                .VerifyElementEnable(serviceTaskLinePage.SerialisedCheckbox, false)
                .VerifyElementEnable(serviceTaskLinePage.DestinationSiteSelect, false)
                .VerifyElementEnable(serviceTaskLinePage.SiteProductSelect, false)
                .VerifyElementEnable(serviceTaskLinePage.StartDateInput, false)
                .VerifyElementEnable(serviceTaskLinePage.EndDateInput, false);
        }
    }
}
