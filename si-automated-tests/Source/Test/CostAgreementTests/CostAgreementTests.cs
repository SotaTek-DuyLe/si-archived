using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.CostAgreements;
using si_automated_tests.Source.Main.Pages.CostRules;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.CostAgreementTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CostAgreementTests : BaseTest
    {
        [Category("Cost agreement")]
        [Category("Chang")]
        [Test(Description = "Additional word 'Field_' displayed in UI cost rule - cost element field")]
        public void TC_212_Cost_agreement_phase_2_additional_word_field_cost_element_field_is_displayed()
        {
            //Step line 46: Verify the Additional word
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser90.UserName, AutoUser90.Password)
                .IsOnHomePage(AutoUser90);
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/costrules");
            PageFactoryManager.Get<CostRulesPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CostRulesPage>()
                .IsCostRuleGridPage()
                .ClickOnFirstCostRuleRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CostRuleDetailPage>()
                .IsCostRuleDetailPage()
                .VerifyDisplayCostElement();
        }

        [Category("Cost agreement")]
        [Category("Chang")]
        [Test(Description = "Add approve button into cost agreement")]
        public void TC_212_Cost_agreement_phase_2_add_approve_button_into_cost_agreement()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            string partyName = "North Star Environmental Services";
            string costAgreementType = "Unit price contract";
            string partySuppier = "78";

            //Step line 7: Navigate to any new cost
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser90.UserName, AutoUser90.Password)
                .IsOnHomePage(AutoUser90);
            DetailPartyPage partyDetailsTab = PageFactoryManager.Get<DetailPartyPage>();
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/parties/" + partySuppier);
            partyDetailsTab
                .WaitForLoadingIconToDisappear();
            partyDetailsTab
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            partyDetailsTab
                .ClickOnCostAgreementsTab()
                .WaitForLoadingIconToDisappear();
            partyDetailsTab
                .ClickOnAddNewItemCostAgreementTab()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CostAgreementDetailPage>()
                .IsCostAgreementDetailPage()
                //Step line 17: Click on [Save] btn and verify the display of message [Cost Agreement Type required]
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.CostAgreementTypeRequiredMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.CostAgreementTypeRequiredMessage);
            //Step line 18: Select one [Cost Agreement Type]
            PageFactoryManager.Get<CostAgreementDetailPage>()
                .SelectCostAgreementType(costAgreementType)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string startDate = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            //Step line 16: Verify [Start date] is editable
            PageFactoryManager.Get<CostAgreementDetailPage>()
                .VerifyStartdateIsEditable();

            //Step line 8: Click on [Approve] btn
            PageFactoryManager.Get<CostAgreementDetailPage>()
                .ClickOnApproveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CostAgreementDetailPage>()
                .VerifyIsApprovedCheckboxCheckedAndDisabled()
                .VerifyStartDateValueAndDisabled(startDate)
                .VerifyApprovedBtnIsNotDisplayed();
            string costAgreementNew = PageFactoryManager.Get<CostAgreementDetailPage>()
                .GetCostAgreementId();
            //Step line 19: Click on all additional tabs
            PageFactoryManager.Get<CostAgreementDetailPage>()
                .ClickOnDataTabAndVerifyDisplayNotes()
                .VerifyDisplayCostBooksTab()
                .VerifyDisplaySectorsTab();
            //Step line 9: Query in DB
            List<COSTAGREEMENTSDBModel> cOSTAGREEMENTSDBModels = commonFinder.GetCostAgreementByPartyId(partySuppier, costAgreementNew);
            Assert.AreEqual(1157, cOSTAGREEMENTSDBModels[0].approveduserID);
            Assert.IsTrue(cOSTAGREEMENTSDBModels[0].approveddatetime.ToString(CommonConstants.DATE_DD_MM_YYYY_FORMAT).Contains(startDate));
        }
    }
}
