using System;
using System.Collections.Generic;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.SiteServiceTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BillingRuleAgreementLineTests : BaseTest
    {
        [Category("Billing rule")]
        [Category("Chang")]
        [Test(Description = "Billing rule in agreement line")]
        public void TC_154_billing_rule_in_agreement_line()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string agreementLineId = "187";
            string billingOption = "Bill as scheduled";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser49.UserName, AutoUser49.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser49);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.SiteServices)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementLineId)
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementLineId)
                //Line 9: Select any [Billing rule] -> Save
                .ClickOnBillingRuleDd()
                .SelectAnyBillingRuleOption(billingOption)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string timeUpdatedExp = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            string[] billingRuleTitleExp = { "Billing Rule", "Notes", "Photo Required" };
            string[] billingRuleExp = { billingOption, "", "Unticked" };
            PageFactoryManager.Get<AgreementLinePage>()
                //Line 10: Verify [History] tab
                .ClickOnHistoryTab()
                .VerifyHistoryAfterUpdatingAgreementLine(billingRuleExp, billingRuleTitleExp, AutoUser49.DisplayName, timeUpdatedExp);
            //RUN QUERY CHECK
            List<AgreementLineActionDBModel> agreementLines = finder.GetAgreementLineActionById(int.Parse(agreementLineId));
            Assert.AreEqual(3, agreementLines[0].billingrule);
        }
    }
}

