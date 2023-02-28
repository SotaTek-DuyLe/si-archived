using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using si_automated_tests.Source.Main.Pages.Task;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.AggrementLineTest
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class AgreementDataTest : BaseTest
    {
        [Category("AgreementTask")]
        [Category("Huong")]
        [Test]
        public void TC_040_Save_Extend_Data_From_Agreement()
        {
            int agreementId = 27;
            string agreementType = "COMMERCIAL COLLECTIONS";
            string agreementName = "LA PLATA STEAKHOUSE";
            string note1 = "This is Agreement extended data type test";
            string note2 = "This is edited note for Agreementid=27";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser31.UserName, AutoUser31.Password)
                .IsOnHomePage(AutoUser31);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            //Go to agreement
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName)
                .ClickDataTab();
            PageFactoryManager.Get<DataTab>()
                .WaitForLoadingIconToDisappear();
            //Input note 1 and verify note 1 text
            PageFactoryManager.Get<DataTab>()
                .IsOnDataTab()
                .InputNotes(note1)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DataTab>()
                .VerifyNote(note1)
                .ClickRefreshBtn();
            PageFactoryManager.Get<DataTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DataTab>()
                .VerifyNote(note1);
            //Edit note 1 to note 2 and verify note 2 text
            PageFactoryManager.Get<DataTab>()
                .IsOnDataTab()
                .InputNotes(note2)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DataTab>()
                .VerifyNote(note2)
                .ClickRefreshBtn();
            PageFactoryManager.Get<DataTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DataTab>()
                .VerifyNote(note2);
        }

        [Category("AgreementLine")]
        [Category("Huong")]
        [Test(Description = "Verify that 'Save' and 'Save and Close' buttons display on an Agreemen Line form")]
        public void TC_151_Agreement_Line_form_Save_and_Save_Close_buttons()
        {
            int agreementId = 163;
            PageFactoryManager.Get<LoginPage>()
              .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser31.UserName, AutoUser31.Password)
                .IsOnHomePage(AutoUser31);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.SiteServices)
                .SwitchNewIFrame();
            //Go to agreement
            PageFactoryManager.Get<SiteServicesCommonPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterId(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            var agreementLinePage = PageFactoryManager.Get<AgreementLinePage>();
            agreementLinePage.WaitForLoadingIconToDisappear();
            agreementLinePage.ClickDetailTab();
            var detailTab = PageFactoryManager.Get<DetailTab>();
            detailTab.WaitForLoadingIconToDisappear();
            string invoiceAddress = "29 GEORGE STREET, RICHMOND, TW9 1HY";
            string billingRule = "Greater of Minimum or Actual";
            bool skipCheck = false;
            detailTab.SelectTextFromDropDown(detailTab.InvoiceAddressSelect, invoiceAddress)
                .ClickSaveBtn()
                .VerifyToastMessageOnParty("Success", skipCheck)
                .WaitUntilToastMessageInvisibleOnParty("Success", skipCheck)
                .WaitForLoadingIconToDisappear();
            detailTab.SelectTextFromDropDown(detailTab.BillingRuleSelect, billingRule)
                .ClickSaveBtn()
                .VerifyToastMessageOnParty("Success", skipCheck)
                .WaitUntilToastMessageInvisibleOnParty("Success", skipCheck)
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            //In 'ID' column filter ID=163 and double click on the record
            PageFactoryManager.Get<SiteServicesCommonPage>()
               .OpenFirstResult()
               .SwitchToLastWindow();
            agreementLinePage.WaitForLoadingIconToDisappear();
            agreementLinePage.ClickDetailTab();
            detailTab.WaitForLoadingIconToDisappear();
            detailTab.VerifySelectedValue(detailTab.InvoiceAddressSelect, invoiceAddress);
            detailTab.VerifySelectedValue(detailTab.BillingRuleSelect, billingRule);
        }
    }
}
