using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.IE_Configuration;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties.PartyAgreement;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.AgreementLineTest
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class AgreementLineProductCodeTests : BaseTest
    {
        [Category("RI Events")]
        [Category("Chang")]
        [Test(Description = "Product Code - Agreement Line Wizard and TaskLines - Allow Product Code = TRUE"), Order(1)]
        public void TC_259_product_code_agreement_line_wizard_and_task_line_allow_product_code_true()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            string servicedSite = "Richmond Guitar Workshop - 24 Ormond Road, Richmond, TW10 6TH";
            string service = Contract.Commercial;
            string assetType = "660L";
            string tenure = "Rental";
            string product = "General Recycling";
            string ewcCode = "150106";
            string productQtyAsset = "50";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser67.UserName, AutoUser67.Password)
                .IsOnHomePage(AutoUser67);

            //Go to any agreement
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/agreements/111");
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForDetailAgreementLoaded("COMMERCIAL COLLECTIONS", "RICHMOND GUITAR WORKSHOP")
                .ClickOnDetailTab()
                .ClickOnAddServiceBtn()
                .IsAddServicePopup()
                //Step line 9: Select site and service in Step 1
                .SelectServicedSite(servicedSite)
                .SelectService(service)
                .ClickOnNextBtn()
                //Step line 10: Add [Asset] and [Product] in Step 2
                .ClickOnAddBtnStep2()
                .SelectAssetTypeInStep2(assetType)
                .SelectTenureInStep2(tenure)
                .SelectProductInStep2(product)
                .VerifyDoneBtnDisabled()
                .VerifyCancelBtnDisabled()
                .SelectEWCCodeInStep2(ewcCode)
                .InputProductQtyPerAsset(productQtyAsset)
                .SelectUnitInStep2("Kilograms")
                .ClickDoneBtnInStep2()
                .ClickOnNextBtn()
                .ClickOnAddRegularServiceStep3()
                .VerifyValueInStep3(assetType, product, "Kilograms", productQtyAsset)
                .VerifyValueEWCCodeInStep3(ewcCode)
                .ClickDoneBtnInStep3();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickOnNotSetLink()
               .ClickOnDoneScheduleBtn2()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .ClosePriceRecords()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
               .ClickSaveBtn()
               //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
                .SleepTimeInMiliseconds(5);
            //GET agreement line id
            //Find agreement line new created
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            PageFactoryManager.Get<AgreementDetailPage>()
                .ClickNewAgreementLineCreatedLink(tommorowDate)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            string agreementLineCreated = PageFactoryManager.Get<AgreementLinePage>()
                .IsAgreementLinePage()
                .GetAgreementLineId();
            //API: Verify data in queries
            List<AgreementLineAssetProductDBModel> agreementLineAssetProductDBModels = commonFinder.GetAgreementLineAssetProductByAgreementLineId(agreementLineCreated);
            Assert.AreEqual(5, agreementLineAssetProductDBModels[0].productcodeID);

            PageFactoryManager.Get<AgreementLinePage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Click on [ASsets and Products] and verify the value of EWC code column
            PageFactoryManager.Get<AgreementDetailPage>()
                .ClickToExpandAgreementLineByStartDate(tommorowDate)
                .ClickToExpandAssetsAndProducts()
                .VerifyDisplayEWCCodeColumn(ewcCode);

        }

        [Category("RI Events")]
        [Category("Chang")]
        [Test(Description = "Product Code - Agreement Line Wizard and TaskLines - Allow Product Code = FALSE"), Order(2)]
        public void TC_259_product_code_agreement_line_wizard_and_task_line_allow_product_code_false()
        {
            string servicedSite = "Putney Town Rowing Club - PUTNEY TOWN ROWING CLUB BOATHOUSE NO, 2 KEW MEADOW PATH, RICHMOND, TW9 4EN";
            string service = Contract.Commercial;
            string assetType = "660L";
            string tenure = "Rental";
            string product = "General Recycling";
            string productQtyAsset = "20";

            //Config allow product code = false
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser67.UserName, AutoUser67.Password)
                .IsOnHomePage(AutoUser67);
            PageFactoryManager.Get<BasePage>()
                .GoToURL(string.Format(WebUrl.AgreementLineTypeIE, "1"));
            PageFactoryManager.Get<AgreementLineTypeIEPage>()
                .IsAgreementLineTypeIEPage()
                .ClickOnAllowProductCode()
                .SleepTimeInMiliseconds(3);
            PageFactoryManager.Get<AgreementLineTypeIEPage>()
                .ClickOnSaveForm()
                .SleepTimeInSeconds(5);
            //Go to any agreement
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/agreements/203");
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForDetailAgreementLoaded("COMMERCIAL COLLECTIONS", "PUTNEY TOWN ROWING CLUB")
                .ClickOnDetailTab()
                .ClickOnAddServiceBtn()
                .IsAddServicePopup()
                //Step line 9: Select site and service in Step 1
                .SelectServicedSite(servicedSite)
                .SelectService(service)
                .ClickOnNextBtn()
                //Step line 10: Add [Asset] and [Product] in Step 2
                .ClickOnAddBtnStep2()
                .SelectAssetTypeInStep2(assetType)
                .SelectTenureInStep2(tenure)
                .SelectProductInStep2(product)
                //Verify [Product code] is hidden
                .VerifyEWCCodeInStep2Hidden()
                .InputProductQtyPerAsset(productQtyAsset)
                .ClickDoneBtnInStep2()
                .ClickOnNextBtn()
                .ClickOnAddRegularServiceStep3()
                .VerifyValueInStep3(assetType, product, "Kilograms", productQtyAsset)
                .VerifyNotDisplayEWCCodeInStep3()
                .ClickDoneBtnInStep3();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickOnNotSetLink()
               .ClickOnDoneScheduleBtn2()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .ClosePriceRecords()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementDetailPage>()
               .ClickSaveBtn()
               //.VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
               .SleepTimeInSeconds(5);
            //Click on [ASsets and Products] and verify the value of EWC code column
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            PageFactoryManager.Get<AgreementDetailPage>()
                .ClickToExpandAgreementLineByStartDate(tommorowDate)
                .ClickToExpandAssetsAndProducts()
                .VerifyNotDisplayEWCCodeColumn();
            //Update ewc code

            PageFactoryManager.Get<BasePage>()
                .GoToURL(string.Format(WebUrl.AgreementLineTypeIE, "1"));
            PageFactoryManager.Get<AgreementLineTypeIEPage>()
                .IsAgreementLineTypeIEPage()
                .ClickOnAllowProductCode()
                .SleepTimeInMiliseconds(3);
            PageFactoryManager.Get<AgreementLineTypeIEPage>()
                .ClickOnSaveForm()
                .SleepTimeInSeconds(5);
        }
    }
}
