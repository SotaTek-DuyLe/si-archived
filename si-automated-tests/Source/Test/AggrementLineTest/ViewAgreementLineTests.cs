using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using static si_automated_tests.Source.Main.Models.UserRegistry;

using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;

namespace si_automated_tests.Source.Test.AggrementLineTest
{
    public class ViewAgreementLineTests : BaseTest
    {
        [Test]
        public void TC_015()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser6.UserName, AutoUser6.Password)
                .IsOnHomePage(AutoUser6)
                //Filter id
                .ClickParties()
                .ExpandNSC();
            PageFactoryManager.Get<PartyNavigation>()
                .ClickSiteServiceSubMenu()
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterId(57)
                .VerifyFirstLineAgreementResult(57, 30)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess("30")
                .GoToAllTabAndConfirmNoError();
            PageFactoryManager.Get<AgreementLinePage>()
                .ClickDetailTab();
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .ClickAssetAndProductAndVerify("true")
                .GetAllInfoAssetAndProduct();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel)
                .ClickAssetAndProductAndVerify("false");
            //Mobilization
            MobilizationModel mobilizationModel = PageFactoryManager.Get<DetailTab>()
               .ClickMobilizationAndVerify("true")
               .GetAllInfoMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationInfo(mobilizationModel)
                .ClickMobilizationAndVerify("false");
            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .ClickRegularAndVerify("true")
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel)
                .ClickRegularAndVerify("false");
            //De-Mobilization
            MobilizationModel deMobilizationModel = PageFactoryManager.Get<DetailTab>()
                .ClickDeMobilizationAndVerify("true")
                .GetAllInfoDeMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyDeMobilizationInfo(deMobilizationModel)
                .ClickDeMobilizationAndVerify("false");
            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .ClickAdHocAndVerify("true")
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc)
                .ClickAdHocAndVerify("false");
            PageFactoryManager.Get<AgreementLinePage>()
                .CloseWithoutSaving()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .VerifyAgreementWindowClosed();
        }

    }
}
