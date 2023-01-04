using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
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
        [Test(Description = "Product Code - Agreement Line Wizard and TaskLines")]
        public void TC_259_product_code_agreement_line_wizard_and_task_line()
        {
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
                .SelectEWCCodeInStep2(ewcCode)
                .InputProductQtyPerAsset(productQtyAsset)
                .SelectUnitInStep2("Kilograms")
                .ClickDoneBtnInStep2()
                .ClickOnNextBtn()
                .ClickOnAddRegularServiceStep3()
                .VerifyValueInStep3(assetType, product, "Kilograms", ewcCode, productQtyAsset);
        }
    }
}
