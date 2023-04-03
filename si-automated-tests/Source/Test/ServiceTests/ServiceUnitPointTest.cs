using NTextCat;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Common;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Linq;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using SiteServiceUnitPage = si_automated_tests.Source.Main.Pages.Paties.Sites.ServiceUnitPage;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ServiceUnitPointTest : BaseTest
    {
        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "Verify that correct info displays in add Service Unit Point pop up")]
        public void TC_166_Service_Unit_Point()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password)
                .IsOnHomePage(AutoUser40);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .OpenOption("Active Service Units");
            ServiceUnitPage serviceUnit = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnit.SwitchToFrame(serviceUnit.UnitIframe);
            serviceUnit.WaitForLoadingIconToDisappear();
            serviceUnit.FindServiceUnitWithId("230038");
            serviceUnit.DoubleClickServiceUnitById("230038")
                       .SwitchToChildWindow(2);

            ServiceUnitDetailPage serviceUnitDetail = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceUnitPointTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddPointButton);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.AddServiceUnitPointDiv, true);
            serviceUnitDetail.VerifyRadioIsSelected()
                .VerifySelectedValue(serviceUnitDetail.SectorSelect, "Richmond")
                .ClickOnElement(serviceUnitDetail.AddServiceUnitPointCloseButton);
            serviceUnitDetail.ClickCloseBtn()
                .SwitchToFirstWindow();
            serviceUnit.ClickOnElement(serviceUnit.GetToogleButton(AutoUser40.DisplayName));
            serviceUnit.SelectByDisplayValueOnUlElement(serviceUnit.UserDropDown, "Locale Languages")
                .SwitchToChildWindow(2);
            LocalLanguagePage localLanguagePage = PageFactoryManager.Get<LocalLanguagePage>();
            localLanguagePage.SelectTextFromDropDown(localLanguagePage.LanguageSelect, "French")
                .ClickOnElement(localLanguagePage.SaveButton);
            localLanguagePage.SwitchToFirstWindow();
            string description = serviceUnit.GetElementText(serviceUnit.CreateDescriptionButton);
            var factory = new RankedLanguageIdentifierFactory();
            var identifier = factory.Load(@"Source\Main\Data\Core14.profile.xml");
            var languages = identifier.Identify(description);
            var mostCertainLanguage = languages.FirstOrDefault();
            Assert.IsNotNull(mostCertainLanguage);
            Assert.IsTrue(mostCertainLanguage.Item1.Iso639_2T == "fra");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "The enddate is not getting set on some PointNodes affecting search for nodes in Add service unit points")]
        public void TC_183_The_enddate_is_not_getting_set_on_some_PointNodes()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password)
                .IsOnHomePage(AutoUser40);
            var homePage = PageFactoryManager.Get<HomePage>();
            homePage.ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption("Nodes")
                .ClickAndSelectRichmondSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            var pointAddressListPage = PageFactoryManager.Get<PointAddressListingPage>();
            pointAddressListPage.DoubleClickPointAddress("2")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.ClientRefInput, "1478");
            pointAddressDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            //Click on add new item in the point node grid
            pointAddressListPage.ClickOnElement(pointAddressListPage.addNewPointAddressBtn);
            pointAddressListPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            string description = "Hiltribe, 18 Red lion street";
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.DescriptionInput, description);
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.ClientRefInput, "");
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.LatitudeInput, "51.4589021");
            pointAddressDetailPage.SendKeys(pointAddressDetailPage.LongitudeInput, "-0.3070383");
            pointAddressDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            pointAddressListPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //pointAddressListPage.VerifyPointAddressHasEndDate(description);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/230012");
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.AddPointButton);
            serviceUnitPage.ClickOnElement(serviceUnitPage.NodeRadio);
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond Commercial");
            serviceUnitPage.SendKeys(serviceUnitPage.ClientRefInput, "1478");
            serviceUnitPage.ClickOnElement(serviceUnitPage.SearchButton);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifySearchResult("2")
                .CheckSearchResult("2");
            serviceUnitPage.ClickOnElement(serviceUnitPage.AddServiceUnitButton);
            serviceUnitPage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyPointIdOnServiceUnitPointList("2");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "Service Unit map does not display serviced points")]
        public void TC_185_Service_Unit_map_does_not_display_serviced_points()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/230011");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyServiceUnitType("Both Serviced and Point of Service");
            serviceUnitPage.ClickOnElement(serviceUnitPage.MapTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.DragBluePointToAnotherPosition();
            serviceUnitPage.VerifyBlueAndRedPointVisible();

            //Click back to service unit points -> Change the type to Point of service -> Save -> Refresh the form
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectServiceUnitPointType("Point of Service")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyServiceUnitType("Point of Service");

            //Click map tab -> Drag blue pin away
            serviceUnitPage.ClickOnElement(serviceUnitPage.MapTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.DragBluePointToAnotherPosition();
            serviceUnitPage.VerifyBlueAndRedPointVisible();

            //Click back to service unit points -> Change the type to Serviced point -> Save -> Refresh the form
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectServiceUnitPointType("Serviced Point")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyServiceUnitType("Serviced Point");

            //Click map tab -> Drag blue pin away
            serviceUnitPage.ClickOnElement(serviceUnitPage.MapTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.DragBluePointToAnotherPosition();
            serviceUnitPage.VerifyBlueAndRedPointVisible();
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "selected street is cleared out from filter when changing the sectors")]
        public void TC_189_Service_Unit_form_Add_SUP()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-units/229673");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            SiteServiceUnitPage serviceUnitPage = PageFactoryManager.Get<SiteServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.ServiceUnitPointTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.AddPointButton);
            //Type the street ‘Church Terrace’ and select the street CHURCH TERRACE,TW10,RICHMOND and then select a sector East
            serviceUnitPage.SendKeys(serviceUnitPage.StreetInput, "Church Terrace");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectByDisplayValueOnUlElement(serviceUnitPage.StreetAutoCompleteTextBox, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "East");
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            //After switch back to sector from step 1 - Richmond
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "");
            //Type the street ‘Church Terrace’ and select the street CHURCH TERRACE,TW10,RICHMOND and then select a sector Richmond commercial
            serviceUnitPage.SendKeys(serviceUnitPage.StreetInput, "Church Terrace");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.SelectByDisplayValueOnUlElement(serviceUnitPage.StreetAutoCompleteTextBox, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond Commercial");
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "CHURCH TERRACE,TW10,RICHMOND");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            //After switch back to sector from step 1 - Richmond
            serviceUnitPage.SelectTextFromDropDown(serviceUnitPage.SectorSelect, "Richmond");
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyInputValue(serviceUnitPage.StreetInput, "CHURCH TERRACE,TW10,RICHMOND");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_234_Cannot_remove_indicators_start_and_end_date_when_indicator_is_unset()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120501");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            servicesTaskPage.WaitForLoadingIconToDisappear();
            servicesTaskPage.SelectTextFromDropDown(servicesTaskPage.TaskIndicatorSelect, "Repeat Missed")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "01/01/2050");
            servicesTaskPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "01/01/2050");
            //Remove indicator end date -> Save
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorEndDateInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Indicator End Date is required")
                .WaitUntilToastMessageInvisible("Indicator End Date is required");
            //Remove indicator start date -> Save 
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorEndDateInput, "01/01/2050");
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorStartDateInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage("Indicator Start Date is required")
                .WaitUntilToastMessageInvisible("Indicator Start Date is required");
            //Set different indicator start and end date -> Save 
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorStartDateInput, londonCurrentDate.AddDays(2).ToString("dd/MM/yyyy"));
            servicesTaskPage.SendKeys(servicesTaskPage.IndicatorEndDateInput, londonCurrentDate.AddDays(30).ToString("dd/MM/yyyy"));
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Deselect task indicators -> Save
            servicesTaskPage.SelectTextFromDropDown(servicesTaskPage.TaskIndicatorSelect, "")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, "");
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "");
            servicesTaskPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorStartDateInput, "");
            servicesTaskPage.VerifyInputValue(servicesTaskPage.IndicatorEndDateInput, "");
        }

        [Category("ServiceUnitPoint")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_270_AO_Intra_day_MultiRound_Optimisation()
        {
            //Verify whether Service form is updated to display new checkbox 
            PageFactoryManager.Get<LoginPage>()
              .GoToURL(WebUrl.MainPageUrl + "web/services/15");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            ServiceDetailPage serviceDetailPage = PageFactoryManager.Get<ServiceDetailPage>();
            serviceDetailPage.VerifyElementVisibility(serviceDetailPage.DynamicOptimisationLabel, true)
                .VerifyCheckboxIsSelected(serviceDetailPage.DynamicOptimisationCheckbox, false);
            //Verify whether question mark is added next to field (like on screenshot of Service Unit below)
            serviceDetailPage.ClickOnElement(serviceDetailPage.DynamicOptimisationHelpButton);
            serviceDetailPage.VerifyTooltip("Improve service efficiency by allowing automated task re-allocations between optimised round instances. If set to True, Tasks will only be re-allocated to round instances which are in the same service, are scheduled for the same day, and are configured to be able to perform that task.");
        }
    }
}
