using System;
using System.Collections.Generic;
using System.Linq;
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
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using si_automated_tests.Source.Main.Pages.Sites;
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

        [Category("Agreement")]
        [Category("Huong")]
        [Test]
        public void TC_311_Expose_OnStop_button_on_agreements_with_related_functionality()
        {
            //Verify that 'ON STOP' displays on an Agreement
            PageFactoryManager.Get<LoginPage>()
              .GoToURL(WebUrl.MainPageUrl + "web/agreements/40");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser31.UserName, AutoUser31.Password);
            PartyAgreementPage partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.ClickOnDetailsTab();
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.VerifyElementVisibility(partyAgreementPage.OnStopBtn, true);

            //Verify that user can set individual Agreement 'ON STOP' 
            //On Active Agreement click 'On Stop'
            partyAgreementPage.ClickOnElement(partyAgreementPage.OnStopBtn);
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.VerifyElementVisibility(partyAgreementPage.OffStopBtn, true);
            partyAgreementPage.VerifyAgreementStatus("On Stop");

            //Navigate to Tasks tab on Agreement
            TaskTab taskTab = partyAgreementPage.OpenTaskTab();
            taskTab.VerifyOnStopTaskState();

            //Navigate to the Party for this Agreement>Calendar tab>filter Site/Services for the task on the 'On Stop' Agreement. Click 'Apply'
            PageFactoryManager.Get<LoginPage>()
              .GoToURL(WebUrl.MainPageUrl + "web/parties/49");
            DetailPartyPage partyCommonPage = PageFactoryManager.Get<DetailPartyPage>();
            partyCommonPage.WaitForLoadingIconToDisappear();
            PartyCalendarPage partyCalendarPage = partyCommonPage.ClickCalendarTab();
            partyCalendarPage.WaitForLoadingIconToDisappear();
            DateTime fromDateTime = CommonUtil.GetFirstDayInMonth(DateTime.Now);
            DateTime toDateTime = CommonUtil.GetLastDayInMonth(DateTime.Now);
            var serviceTasks = PageFactoryManager.Get<PartyCalendarPage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("task-onhold.png")).ToList();
            Assert.IsTrue(serviceTasks.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() != 0);

            //Navigate to the Party for this Agreement>Sites>open Site>filter Services for the task on the 'On Stop' Agreement. Click 'Apply'
            PageFactoryManager.Get<DetailPartyPage>()
                 .ClickSiteTab()
                 .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .DoubleClickSiteRow(70)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailSitePage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            var serviceTasksInSitePage = PageFactoryManager.Get<DetailSitePage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("task-onhold.png")).ToList();
            Assert.IsTrue(serviceTasksInSitePage.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() != 0);
            PageFactoryManager.Get<DetailSitePage>().ClickCloseBtn()
                .SwitchToFirstWindow();

            //On this Agreement click 'Off Stop'
            PageFactoryManager.Get<LoginPage>()
              .GoToURL(WebUrl.MainPageUrl + "web/agreements/40");
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.ClickOnDetailsTab();
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.ClickOnElement(partyAgreementPage.OffStopBtn);
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.VerifyElementVisibility(partyAgreementPage.OnStopBtn, true);
            partyAgreementPage.VerifyAgreementStatus("Active");


            //Verify that it's not possible to take individual agreement OFF STOP if its Party is ON STOP
            partyAgreementPage.ClickOnElement(partyAgreementPage.PartyTitle);
            partyAgreementPage.SwitchToChildWindow(2);
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage.ClickAccountTab();
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage.ClickOnElement(partyCommonPage.OnStopBtnInAccountTab);
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage.VerifyElementText(partyCommonPage.PartyStatus, "On Stop");
            AgreementTab agreementTab = partyCommonPage.OpenAgreementTab();
            agreementTab.VerifyStatus(0, "On Stop");

            //Open On Stop Agreement>Click 'Off Stop' button
            agreementTab.OpenFirstAgreement()
                .SwitchToChildWindow(3);

            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.ClickOnDetailsTab();
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.ClickOnElement(partyAgreementPage.OffStopBtn);
            partyAgreementPage.VerifyToastMessage("You cannot take an Agreement 'Off Stop' when the associated Party is 'On Stop'")
                .WaitUntilToastMessageInvisible("You cannot take an Agreement 'Off Stop' when the associated Party is 'On Stop'");

            //Navigate to the Party where Party status=On stop AND party has 'On Stop' Agreements
            //In Account tab click 'Off Stop'
            //Click 'Cancel' 
            partyAgreementPage.ClickCloseBtn()
                .SwitchToChildWindow(2);
            partyCommonPage.ClickAccountTab();
            partyCommonPage.WaitForLoadingIconToDisappear();
            partyCommonPage.ClickOnElement(partyCommonPage.OffStopButton);
            partyCommonPage.VerifyElementVisibility(partyCommonPage.OffStopTitle, true);
            partyCommonPage.ClickOnElement(partyCommonPage.CancelButton);
            partyCommonPage.VerifyElementText(partyCommonPage.PartyStatus, "On Stop");

            //Click 'No' 
            partyCommonPage.ClickOnElement(partyCommonPage.OffStopButton);
            partyCommonPage.VerifyElementVisibility(partyCommonPage.OffStopTitle, true);
            partyCommonPage.ClickOnElement(partyCommonPage.NoButton);
            partyCommonPage.VerifyElementText(partyCommonPage.PartyStatus, "On Stop");

            //Click 'Yes'
            partyCommonPage.ClickOnElement(partyCommonPage.OffStopButton);
            partyCommonPage.VerifyElementVisibility(partyCommonPage.OffStopTitle, true);
            partyCommonPage.ClickOnElement(partyCommonPage.YesButton);
            partyCommonPage.VerifyElementText(partyCommonPage.PartyStatus, "Active");
        }
    }
}
