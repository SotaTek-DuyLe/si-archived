using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTypeTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TaskTypeTests : BaseTest
    {
        [Category("TaskTypes")]
        [Category("Chang")]
        [Test(Description = "The tasktypes.assured flag is not inherited (bug fix) assured flag TRUE"), Order(1)]
        public void TC_252_The_tasktypes_assured_flag_is_not_inherited_assured_flag_TRUE()
        {
            string agreementId = "238";
            string serviceName = "Commercial";
            string serviceTask = "Commercial Collection";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser88.UserName, AutoUser88.Password)
                .IsOnHomePage(AutoUser88);
            //Go to existing agreement and create a new agreement line
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/agreements/" + agreementId);
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .IsOnPartyAgreementPage()
                .ClickAddService()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .ChooseServicedSite(1)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .ChooseService(serviceName)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .ChooseAssetType("660L")
                .InputAssetQuantity(2)
                .ChooseTenure("Rental")
                .ChooseProduct("General Recycling")
                .ChooseEwcCode("150106")
                .InputProductQuantity(600)
                .VerifyTotalProductQuantity(2 * 600)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .VerifyAssetSummary(2, "660L", "Rental", 600, "General Recycling")
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .VerifyAssetSummary(2, "660L", 600, "General Recycling")
                .ClickAddScheduleRequirement()
                .SelectFrequencyOption("Weekly")
                .UntickAnyDayOption()
                .SelectDayOfWeek("Thu")
                .ClickDoneRequirementBtn()
                .VerifyScheduleSummary("Once Every week on any Thursday")
                .ClickNext();
            PageFactoryManager.Get<PriceTab>()
                .ClosePriceRecords()
                .ClickNext();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .VerifyInvoiceOptions("Use Agreement")
                .ClickFinish();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServicePanelPresent()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear()
                //waiting for new task are genarated
                .SleepTimeInSeconds(120);
            string todayDate = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            //Go to Tasks tab and click on Service task hyperlink that has been just generated
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .ClickOnFirstServiceUnitAfterGenerated("Deliver Commercial Bin", todayDate)
                .WaitForLoadingIconToDisappear();
            int indexOfServiceTaskScheduleByStartD = PageFactoryManager.Get<ServiceUnitDetailPage>()
                .IsServiceUnitDetailPage()
                //Step line 9: Click on [Service task schedules] tab and verify [Assured task column] has flags ticked
                .ClickOnServiceTaskSchedulesTab()
                .VerifyAllAssuredTaskTicked()
                //Step line 10: Click on [Edit service task] that has been generated
                .GetIndexOfServiceTaskScheduleByStartDate(tomorrowDate);
            Console.WriteLine(indexOfServiceTaskScheduleByStartD);
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .ClickOnEditServiceTaskBtnByIndex(indexOfServiceTaskScheduleByStartD)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyAssuredTaskChecked()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Step line 11: Click the [Add New Item] in pop out [Add Service Task]
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .IsServiceUnitDetailPage()
                .ClickOnAddNewItemServiceTaskSchedules()
                .IsAddServiceTaskPopOut()
                .SelectServiceTasks(serviceTask)
                .ClickOnCreateBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyAssuredTaskChecked()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .ClickOnAssetTypeAndSelectValue("1100L")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnDetailTab()
                .VerifyAssuredTaskChecked();
            string serviceTaskId = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceTaskId();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .VerifyServiceTaskScheduleCreated(serviceTaskId, serviceTask, "0 x 1100L");
        }

        [Category("TaskTypes")]
        [Category("Chang")]
        [Test(Description = "The tasktypes.assured flag is not inherited (bug fix)assured flag FALSE"), Order(2)]
        public void TC_252_The_tasktypes_assured_flag_is_not_inherited_assured_flag_FALSE()
        {
            string serviceTaskId = "22794";
            string serviceTask = "Collect Domestic Refuse";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser88.UserName, AutoUser88.Password)
                .IsOnHomePage(AutoUser88);
            //Step line 13: Go to existing service task Id (assured task = FALSE)
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/" + serviceTaskId);
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyAssuredTaskNotChecked();
            //Step line 14: Click on [Service Unit hyperlink] => [Service Task Schedules] tab
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnServiceTaskDesc()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .IsServiceUnitDetailPage()
                .ClickOnServiceTaskSchedulesTab()
                .VerifyAllAssuredTaskUnTicked()
                //Step line 15: Click on [Add new item]
                .ClickOnAddNewItemServiceTaskSchedules()
                .IsAddServiceTaskPopOut()
                .SelectServiceTasks(serviceTask)
                .ClickOnCreateBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyAssuredTaskNotChecked()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .ClickOnAssetTypeAndSelectValue("360L")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnDetailTab()
                .VerifyAssuredTaskNotChecked();
            string serviceTaskIdCreated = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceTaskId();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .VerifyServiceTaskScheduleCreated(serviceTaskIdCreated, serviceTask, "0 x 360L")
                .VerifyAllAssuredTaskUnTicked();
            //Step line 16: Set assured task to true
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .ClickOnEditServiceTaskBtnByServiceTaskId(serviceTaskIdCreated)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .ClickOnAssuredTaskCheckbox()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<ServicesTaskPage>()
                .VerifyAssuredTaskChecked()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .IsServiceUnitDetailPage()
                .VerifyAssuredTaskByServiceTaskIdChecked(serviceTaskIdCreated)
                //Step line 17: Set assured task to false
                .ClickOnEditServiceTaskBtnByServiceTaskId(serviceTaskIdCreated)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .ClickOnAssuredTaskCheckbox()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<ServicesTaskPage>()
                .VerifyAssuredTaskChecked()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceUnitDetailPage>()
                .IsServiceUnitDetailPage()
                .VerifyAssuredTaskByServiceTaskIdNotChecked(serviceTaskIdCreated);
        }
    }
}
