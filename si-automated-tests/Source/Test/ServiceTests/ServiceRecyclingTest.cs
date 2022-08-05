using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
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
        [Test(Description = "Restrict Edit Feature on new style forms on Service Unit tab")]
        public void TC_128_1_Restrict_Edit_Feature_on_new_style_forms_on_Service_Unit()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star")
                .ExpandOption("Recycling")
                .OpenOption("Communal Recycling");
            ServiceRecyclingPage sectorRecycling = PageFactoryManager.Get<ServiceRecyclingPage>();
            sectorRecycling.WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();

            //Verify that user is unable to update sections of the forms when the restrict edit is set in the service
            sectorRecycling.SelectRandomPointType()
                .ClickOnElement(sectorRecycling.RestrictEditCheckbox);
            sectorRecycling
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            sectorRecycling.VerifyCheckboxIsSelected(sectorRecycling.RestrictEditCheckbox, true);

            //Service unit
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star")
                .ExpandOption("Recycling")
                .ExpandOption("Communal Recycling")
                .OpenOption("Service Units");
            ServiceUnitPage serviceUnit = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnit.WaitForLoadingIconToDisappear(false)
                .SwitchNewIFrame();
            string serviceUnitId = "223695";
            serviceUnit
                .FindServiceUnitWithId(serviceUnitId)
                .DoubleClickServiceUnit()
                .SwitchToChildWindow(2);

            ServiceUnitDetailPage serviceUnitDetail = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.DetailTab);
            serviceUnitDetail.VerifyInputIsReadOnly(serviceUnitDetail.StartDateInput)
                .VerifyInputIsReadOnly(serviceUnitDetail.EndDateInput);

            //Details: Update description, client reference, point segment, street, colour, service level->Save
            //Description
            string valueServiceUnitInput = serviceUnitDetail.GetInputValue(serviceUnitDetail.ServiceUnitInput) + serviceUnitDetail.RandomString(3);
            serviceUnitDetail.SendKeys(serviceUnitDetail.ServiceUnitInput, valueServiceUnitInput);
            //Client ref
            string valueClientReferenceInput = serviceUnitDetail.GetInputValue(serviceUnitDetail.ClientReferenceInput) + serviceUnitDetail.RandomString(3);
            serviceUnitDetail.SendKeys(serviceUnitDetail.ClientReferenceInput, valueClientReferenceInput);
            //point segment
            string valuePointSegmentInput = serviceUnitDetail.GetInputValue(serviceUnitDetail.PointSegmentInput);
            serviceUnitDetail.SendKeys(serviceUnitDetail.PointSegmentInput, valuePointSegmentInput);
            //street
            string valueStreetInput = "BEAUMONT AVENUE";
            serviceUnitDetail.SendKeys(serviceUnitDetail.StreetInput, valueStreetInput);

            serviceUnitDetail.SendKeys(serviceUnitDetail.ColorInput, "#752f75");
            serviceUnitDetail.SelectRandomServiceLevel()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage("Success");

            serviceUnitDetail.VerifyInputValue(serviceUnitDetail.ServiceUnitInput, valueServiceUnitInput)
                .VerifyInputValue(serviceUnitDetail.ClientReferenceInput, valueClientReferenceInput)
                .VerifyInputValue(serviceUnitDetail.PointSegmentInput, valuePointSegmentInput)
                .VerifyInputValue(serviceUnitDetail.StreetInput, valueStreetInput)
                .VerifyInputValue(serviceUnitDetail.ColorInput, "#752f75");

            // Data tab: Update all data->Save
            valueStreetInput = "BEAUMONT AVENUE1";
            serviceUnitDetail.SendKeys(serviceUnitDetail.StreetInput, valueStreetInput);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.DataTab);
            string noteInputValue = serviceUnitDetail.RandomString(5);
            serviceUnitDetail.SendKeys(serviceUnitDetail.NoteInput, noteInputValue);
            string accessPointValue = serviceUnitDetail.RandomString(5);
            serviceUnitDetail.SendKeys(serviceUnitDetail.AccessPointInput, accessPointValue);
            serviceUnitDetail.ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage("Success");
            serviceUnitDetail.VerifyInputValue(serviceUnitDetail.NoteInput, noteInputValue)
                .VerifyInputValue(serviceUnitDetail.AccessPointInput, accessPointValue);

            //Scheduled service tasks tab: Click on add new item
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceTaskScheduleTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.VerifyElementEnable(serviceUnitDetail.AddNewItemButton, false);

            //Service unit points tab: Click on add points, change type and qualifier in the grid
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceUnitPointTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddPointButton);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.AddServiceUnitPointDiv, true)
                .ClickOnElement(serviceUnitDetail.AddServiceUnitPointCloseButton);
            //Double click on SUP
            serviceUnitDetail
                .VerifyStartDateAndEndDateIsReadOnly()
                .DoubleClickServiceUnitPoint(0)
                .SwitchToChildWindow(3);

            ServiceUnitPointDetailPage serviceUnitPointDetail = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitPointDetail.VerifyElementVisibility(serviceUnitPointDetail.RetireButton, false);
            serviceUnitPointDetail.ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Update SUP type->Save
            serviceUnitDetail.EditServiceUnitPoint(0, "Serviced Point", "")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage("Success");
            //Click on Assets tab: Add new, add existing asset, delete item
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AssetsTab);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddNewAssetItemButton);
            serviceUnitDetail.SwitchToChildWindow(3);

            AssetDetailItemPage assetDetailItemPage = PageFactoryManager.Get<AssetDetailItemPage>();
            assetDetailItemPage.WaitForLoadingIconToDisappear();
            Random rnd = new Random();
            string assetValue = "Asset" + rnd.Next(10);
            string assetReferenceValue = "Asset Reference" + rnd.Next(10);
            string assetType = "360L";
            string state = "On Site";
            string product = "Food";
            assetDetailItemPage.SendKeys(assetDetailItemPage.AssetInput, assetValue);
            assetDetailItemPage.SelectTextFromDropDown(assetDetailItemPage.AssetTypeSelect, assetType)
                .SelectTextFromDropDown(assetDetailItemPage.ProductSelect, product)
                .SelectTextFromDropDown(assetDetailItemPage.StateSelect, state)
                .SelectTextFromDropDown(assetDetailItemPage.AgreementLineSelect, "")
                .SendKeys(assetDetailItemPage.AssetReferenceInput, assetReferenceValue);
            assetDetailItemPage.ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage("Success")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            serviceUnitDetail.ClickRefreshBtn();
            serviceUnitDetail.VerifyAssetAddedByAddNewItemButton(assetValue, assetType, state, product, assetReferenceValue);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddExistAssetButton);
            serviceUnitDetail.SelectByDisplayValueOnUlElement(serviceUnitDetail.BinsSelect, "660L")
                .SleepTimeInMiliseconds(1000);
            string assetType2 = "660L_19";
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.MainAssetDropDownButton);
            serviceUnitDetail.SelectByDisplayValueOnUlElement(serviceUnitDetail.MainAssetSelect, assetType2);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ConfirmButton);
            serviceUnitDetail.VerifyToastMessage("Successfully saved Asset");
            serviceUnitDetail.VerifyAssetAddedByAddExistItemButton("660L")
                .ClickAssetCheckBox(0)
                .ClickOnElement(serviceUnitDetail.DeleteAssetItemButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage("Success");

            //User can do all actions in announcement, map, risks, subspriptions, notifications and rental asset tabs
            //announcements - tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AnnouncementTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false)
                .ClickOnElement(serviceUnitDetail.AddNewAnnouncementItemButton);
            serviceUnitDetail.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear(false);
            AnnouncementDetailPage announcementDetailPage = PageFactoryManager.Get<AnnouncementDetailPage>();
            string announcement = "Announcement text";
            string announcementType = "Collection services";
            announcementDetailPage.SelectTextFromDropDown(announcementDetailPage.announcementTypeSelect, announcementType);
            announcementDetailPage.SendKeys(announcementDetailPage.announcemenTextInput, announcement);
            announcementDetailPage.SelectTextFromDropDown(announcementDetailPage.impactSelect, "Positive");
            string from = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string to = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, 1);
            announcementDetailPage.SendKeys(announcementDetailPage.validFromInput, from);
            announcementDetailPage.SendKeys(announcementDetailPage.valiToInput, to);
            announcementDetailPage.ClickSaveBtn()
                .WaitForLoadingIconToDisappear(false)
                .VerifyToastMessage("Successfully saved Announcement")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            serviceUnitDetail.VerifyNewAnnouncement(announcement, announcementType, from, to);
            serviceUnitDetail.ClickAnnouncementCheckbox(0);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.DeleteAnnouncementItemButton);
            serviceUnitDetail.SwitchToChildWindow(3);
            AnnouncementRemovePage announcementRemovePage = PageFactoryManager.Get<AnnouncementRemovePage>();
            announcementRemovePage.ClickOnElement(announcementRemovePage.YesButton);
            announcementRemovePage.VerifyToastMessage("Success")
                .SwitchToChildWindow(2);
            serviceUnitDetail.VerifyAnnouncementDeleted(announcement);

            //map-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.MapTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ResetMapButton);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SaveMapButton);
            serviceUnitDetail.VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");

            //risk-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.RiskTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.RiskTabIframe);
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
            riskRegisterPage.VerifyToastMessage("Successfully saved Risk Register")
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
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.SubscriptionTabIframe);
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
                .VerifyToastMessage("Successfully saved Subscription")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.SubscriptionTabIframe);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.SubscriptionRefreshButton);
            serviceUnitDetail.WaitForLoadingIconToDisappear();
            serviceUnitDetail.VerifyNewSubscription(firstName + " " + lastName, mobile);
            serviceUnitDetail.SwitchToDefaultContent();
            //notifications-tab
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.NotificationTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.SwitchToFrame(serviceUnitDetail.Notificationiframe);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.NotificationRefreshButton, true);
        }
    }
}
