using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.WeighbridgeTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class WarningLimitWBTests : BaseTest
    {
        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Test ID = 1, 3, Verify that Warning limit is set up correctly for existing party and Warning limit can be modified")]
        public void TC_261_TestID_1_3_Warning_limit_existing_party()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string partyId = "30";
            string partyName = "GeoFossils";

            //Scenario 1
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser82.UserName, AutoUser82.Password)
                .IsOnHomePage(AutoUser82);
            //Open the party Id = 30 and change [Credit limit]
            PageFactoryManager.Get<BasePage>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partyId);
            PageFactoryManager.Get<DetailPartyPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .InputCreditLimt("1000")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInCreditLimt("1000")
                //Step line 9: Verify in WB settings tab
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInWarningLimit("750");
            //Step line 10: Run query to check
            List<WBPartySettingDBModel> list = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.IsNull(list[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModels = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(1000, partiesDBModels[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModels = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(750, wBPartySettingVDBModels[0].creditlimitwarning);

            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            //Step line 11: Change [Credit limit]
            PageFactoryManager.Get<DetailPartyPage>()
                .InputCreditLimt("900")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInCreditLimt("900")
                //Step line 12: Verify in WB settings tab
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
               .VerifyValueInWarningLimit("675");
            //Step line 13: Run query to check
            List<WBPartySettingDBModel> listAfterChanged = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.IsNull(listAfterChanged[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModelsAfterChanged = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(900, partiesDBModelsAfterChanged[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModelsAfterChanged = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(675, wBPartySettingVDBModelsAfterChanged[0].creditlimitwarning);
            //Step line 17: Change Warning limit
            PageFactoryManager.Get<DetailPartyPage>()
                .InputTextInWarningLimit("1000")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInWarningLimit("1000");
            //Step line 18: API run query to check
            List<WBPartySettingDBModel> listAfterChangeWarningLimit = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.AreEqual(1000, listAfterChangeWarningLimit[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModelsAfterChangeWarningLimit = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(900, partiesDBModelsAfterChangeWarningLimit[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModelsAfterChangeWarningLimit = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(1000, wBPartySettingVDBModelsAfterChangeWarningLimit[0].creditlimitwarning);
            //Step line 19 + 20: Already set up and verify in other Tcs
            //Step line 26: Click on WB tickets tab and Set Warning limit = 0
            PageFactoryManager.Get<DetailPartyPage>()
                .InputTextInWarningLimit("0")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyValueInWarningLimit("0");
            //Step line 27: Verify API
            List<WBPartySettingDBModel> listWarningLimit0 = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.AreEqual(0, listWarningLimit0[0].creditlimitwarning);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Test ID = 2, Verify that Warning limit is set up correctly for a new party")]
        public void TC_261_TestID_1_Warning_limit_a_new_party()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string partyName = "TC261PartyName";
            string address = "Twickenham";
            string siteName261 = "Site Twickenham 261" + CommonUtil.GetRandomNumber(4);
            string siteName = CommonConstants.WBSiteName;

        //Step line 14: Create a new party
        PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser82.UserName, AutoUser82.Password)
                .IsOnHomePage(AutoUser82);
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup(Contract.Commercial)
                .SendKeyToThePartyInput(partyName)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();

            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            string addressAdded45 = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(siteName261)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded45)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded45)
                .SelectCreatedAddress(addressAdded45)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            //Click on [Account] and change the credit limit]
            detailPartyPage
                .VerifyValueInCreditLimt("125")
                .InputCreditLimt("1000")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyValueInCreditLimt("1000")
                //Step line 15: Verify in WB settings tab
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyValueInWarningLimit("750");
            string partyId = detailPartyPage.GetPartyId();
            //Step line 16: Run API to check
            List<WBPartySettingDBModel> listAfterChanged = commonFinder.GetWBPartySettingByPartyId(partyId);
            Assert.IsNull(listAfterChanged[0].creditlimitwarning);
            List<PartiesDBModel> partiesDBModelsAfterChanged = commonFinder.GetPartiesByPartyId(partyId);
            Assert.AreEqual(1000, partiesDBModelsAfterChanged[0].creditlimit);
            List<WBPartySettingVDBModel> wBPartySettingVDBModelsAfterChanged = commonFinder.GetWBPartiesSettingsVByPartyId(partyId);
            Assert.AreEqual(750, wBPartySettingVDBModelsAfterChanged[0].creditlimitwarning);
        }

        [Category("WB")]
        [Category("Chang")]
        [Test(Description = "Test ID = 5, Verify that warning message is displayed when the customer’s Account Balance + WIP Balance >= Warning Limit. ")]
        public void TC_261_TestID_5_Warning_limit_a_new_party()
        {

        }
    }
}
