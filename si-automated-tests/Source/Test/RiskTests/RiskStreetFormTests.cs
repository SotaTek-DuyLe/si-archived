using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications.RiskRegister;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Streets;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.RiskTests
{
    public class RiskStreetFormTests : BaseTest
    {
        [Category("Risk Street Form")]
        [Test(Description = "Street Form")]
        public void TC_131_Street_Form()
        {
            CommonFinder finder = new CommonFinder(DbContext);

            string riskId = "19";
            string riskDesc = "TREE CLOSE";
            string riskName = "Proximity to School";
            string streetId = "6059";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser54.UserName, AutoUser54.Password)
                .IsOnHomePage(AutoUser54);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Applications")
                .OpenOption("Risk Register")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RiskRegisterListingPage>()
                .IsRiskStreetForm()
                .FilterByRiskId(riskId)
                .DoubleClickAtFirstRisk()
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
                .VerifyRoadTypeOptionDisplayOrderAlphabet()
                //Step 12
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
            string[] allContracts = { Contract.NS, Contract.NSC };
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
    }
}
