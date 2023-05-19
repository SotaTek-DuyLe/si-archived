using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agreements;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.Contract;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Round;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.FormTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class VariousFormTests : BaseTest
    {
        [Category("Various Form")]
        [Category("Chang")]
        [Test(Description = "Limit the slots, defaultslots and slotcounts to 0 or greater than 0 - [Tasks] form "), Order(1)]
        public void TC_237_Various_Form_Limit_slots_defaults_slot_and_slot_counts_to_0_or_greater_than_0_tasks_form()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser89.UserName, AutoUser89.Password)
                .IsOnHomePage(AutoUser89)
                //Step line 7: Go to any [tasks] to verify
                .GoToURL(WebUrl.MainPageUrl + "/web/tasks/40562");

            PageFactoryManager.Get<DetailTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .VerifyMinValueInSlotCountField()
                //Step line 8: Input [-5] into slot aount -> Save
                .InputSlotCount("-5")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage);
            //Step line 9: Remove the number in slot count and Save
            PageFactoryManager.Get<DetailTaskPage>()
                .ClearSlotCount()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<DetailTaskPage>()
                .VerifyValueInSlotCount("0");
        }

        [Category("Various Form")]
        [Category("Chang")]
        [Test(Description = "Limit the slots, defaultslots and slotcounts to 0 or greater than 0 - [Round-instances] form "), Order(2)]
        public void TC_237_Various_Form_Limit_slots_defaults_slot_and_slot_counts_to_0_or_greater_than_0_Round_instances_form()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser89.UserName, AutoUser89.Password)
                .IsOnHomePage(AutoUser89)
                //Step line 11: Go to any [round-instances] to verify
                .GoToURL(WebUrl.MainPageUrl + "/web/round-instances/9642");

            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .VerifyMinValueInSlotCountField()
                //Step line 12: Input [-2] into slot aount -> Save
                .InputSlotCount("-2")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage);
            //Step line 13: Remove the number in slot count and Save
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClearSlotCount()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .VerifyValueInSlotCount("0");
        }

        [Category("Various Form")]
        [Category("Chang")]
        [Test(Description = "Limit the slots, defaultslots and slotcounts to 0 or greater than 0 - [Rounds] form "), Order(3)]
        public void TC_237_Various_Form_Limit_slots_defaults_slot_and_slot_counts_to_0_or_greater_than_0_Rounds_form()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser89.UserName, AutoUser89.Password)
                .IsOnHomePage(AutoUser89)
                //Step line 15: Go to any [rounds] to verify
                .GoToURL(WebUrl.MainPageUrl + "/web/rounds/35");

            PageFactoryManager.Get<RoundDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundDetailPage>()
                .IsRoundDetailPage()
                .VerifyMinValueInSlotCountField()
                //Step line 16: Input [-3] into slot aount -> Save
                .InputSlotCount("-3")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage);
            //Step line 17: Remove the number in slot count and Save
            PageFactoryManager.Get<RoundDetailPage>()
                .ClearSlotCount()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<RoundDetailPage>()
                .VerifyValueInSlotCount("");
        }

        [Category("Various Form")]
        [Category("Chang")]
        [Test(Description = "Limit the slots, defaultslots and slotcounts to 0 or greater than 0 - [Round-groups] form "), Order(4)]
        public void TC_237_Various_Form_Limit_slots_defaults_slot_and_slot_counts_to_0_or_greater_than_0_Round_groups_form()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser89.UserName, AutoUser89.Password)
                .IsOnHomePage(AutoUser89)
                //Step line 19: Go to any [round-groups] to verify
                .GoToURL(WebUrl.MainPageUrl + "/web/round-groups/11");

            PageFactoryManager.Get<RoundGroupDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupDetailPage>()
                .IsRoundGroupDetailPage()
                //Step line 19: Go to [Rounds] tab and verify slots 
                .ClickOnRoundTab()
                .VerifyMinValueAtAllSlotsInput()
                .InputValueIntoSlotAtRoundTab("-6", "2")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage);

            PageFactoryManager.Get<RoundGroupDetailPage>()
                //Step line 22: Remove value
                .ClearValueIntoSlotAtRoundTab("2")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<RoundGroupDetailPage>()
                .VerifyValueInSlotCountSlotAtRoundTab("", "2")
                .ClickOnArrowInAnyRow("1")
                .IsRightRoundPanel()
                //Step line 23: Verify Min value of [Slot] round detail
                .VerifyMinValueInSlotFieldRightRoundPanel()
                //Step line 24: Input [-8] into slots fiels -> Save 
                .InputSlotRightRoundPanel("-8")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.SlotCannotBeNegativeMessage);
            //Step line 21: Remove the number in slot count and Save
            PageFactoryManager.Get<RoundGroupDetailPage>()
                .ClickOnArrowInAnyRow("1")
                .IsRightRoundPanel()
                .ClearSlotRightRoundPanel()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<RoundGroupDetailPage>()
                .ClickOnArrowInAnyRow("1")
                .IsRightRoundPanel()
                .VerifyValueInSlotRightRoundPanel("");
        }

        //[Category("Various Form")]
        //[Category("Chang")]
        //[Test(Description = "Verify that the fix didn't break any other functionality"), Order(5)]
        //public void TC_237_Verify_that_the_fix_did_not_break_any_other_functionality()
        //{
        //    string taskNote = "Task note" + CommonUtil.GetRandomNumber(5);
        //    PageFactoryManager.Get<LoginPage>()
        //        .GoToURL(WebUrl.MainPageUrl);
        //    //Login
        //    PageFactoryManager.Get<LoginPage>()
        //        .IsOnLoginPage()
        //        .Login(AutoUser89.UserName, AutoUser89.Password)
        //        .IsOnHomePage(AutoUser89)
        //        //Step line 27: Tasks
        //        .GoToURL(WebUrl.MainPageUrl + "/web/tasks/40562");

        //    PageFactoryManager.Get<DetailTaskPage>()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<DetailTaskPage>()
        //        .IsDetailTaskPage()
        //        .InputTaskNotes(taskNote)
        //        .ClickSaveBtn()
        //        .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
        //        .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        //    PageFactoryManager.Get<DetailTaskPage>()
        //        .VerifyTaskNotesValue(taskNote);
        //    //Step line 28: Contracts
        //    PageFactoryManager.Get<NavigationBase>()
        //        .GoToURL(WebUrl.MainPageUrl + "/web/contracts/2");

        //    PageFactoryManager.Get<ContractDetailPage>()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<ContractDetailPage>()
        //        .IsContractDetailPage();
        //    string firstValueInPID = PageFactoryManager.Get<ContractDetailPage>()
        //        .GetValueInPIDRententionDays();
        //    PageFactoryManager.Get<ContractDetailPage>()
        //        .InputPIDRententionDays("31")
        //        .ClickSaveBtn()
        //        .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
        //        .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        //    PageFactoryManager.Get<ContractDetailPage>()
        //        .VerifyValuePIDRententionDays("31");
        //    //Update to the first value of PID
        //    PageFactoryManager.Get<ContractDetailPage>()
        //        .InputPIDRententionDays(firstValueInPID)
        //        .ClickSaveBtn()
        //        .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
        //        .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
        //    PageFactoryManager.Get<ContractDetailPage>()
        //        .VerifyValuePIDRententionDays(firstValueInPID);
        //    //Step line 29: party supplier > cost agreement
        //    //PageFactoryManager.Get<NavigationBase>()
        //    //    .GoToURL(WebUrl.MainPageUrl + "/web/parties/78");
        //    //PageFactoryManager.Get<DetailPartyPage>()
        //    //    .WaitForDetailPartyPageLoadedSuccessfully("North Star Environmental Services")
        //    //    .ClickOnCostAgreementsTab();
        //    PageFactoryManager.Get<NavigationBase>()
        //        .GoToURL(WebUrl.MainPageUrl + "/web/cost-agreements/2");
        //    PageFactoryManager.Get<CostAgreementDetailPage>()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<CostAgreementDetailPage>()
        //        .IsCostAgreementDetailPage()
        //        .ClickOnCostBookTab();

        //}
        
    }
}
