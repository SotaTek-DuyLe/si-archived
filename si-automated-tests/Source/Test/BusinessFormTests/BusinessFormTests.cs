using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.BusinessFormTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BusinessFormTests : BaseTest
    {
        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "BU form - Business unit group dropdown showing groups from other contracts - Contact Id = 1")]
        public void TC_226_BU_form_business_unit_group_dropdown_showing_groups_from_other_contracts_contract_id_1()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser91.UserName, AutoUser91.Password)
                .IsOnHomePage(AutoUser91);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOption("Business Unit Groups")
                .ExpandOption("East")
                .ExpandOptionLast("Business Units")
                .OpenOption("Collections")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            
            //Step 7: 
            List<string> allBuninessGroup = PageFactoryManager.Get<BusinessUnitDetailPage>()
                .IsBusinessUnitDetailPage()
                .ClickOnBusinessUnitGroupDdAndGetText();
            //DB: Get all business unit group
            List<BusinessUnitGroupDBModel> businessUnitGroupDBModels = commonFinder.GetBusinessUnitGroupByContractId(1);
            List<string> allBusinessUnitGroupName = businessUnitGroupDBModels.Select(p => p.businessunitgroup).ToList();
            allBusinessUnitGroupName.Add("Select...");
            PageFactoryManager.Get<BusinessUnitDetailPage>()
                .VerifyOnlyBusinessUnitGroupForContractDisplayed(allBuninessGroup, allBusinessUnitGroupName);

            //Step 8:
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .OpenOption("Business Units")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BusinessUnitListPage>()
                .IsBusinessUnitListPage()
                .ClickOnAddNewItemBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            List<string> allBuninessGroupInBusinessUnitDetail = PageFactoryManager.Get<BusinessUnitDetailPage>()
                .IsBusinessUnitDetailPage()
                .ClickOnBusinessUnitGroupDdAndGetText();
            PageFactoryManager.Get<BusinessUnitDetailPage>()
                .VerifyOnlyBusinessUnitGroupForContractDisplayed(allBuninessGroupInBusinessUnitDetail, allBusinessUnitGroupName);
        }

        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "BU form - Business unit group dropdown showing groups from other contracts - Contact Id = 2")]
        public void TC_226_BU_form_business_unit_group_dropdown_showing_groups_from_other_contracts_contract_id_2()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser91.UserName, AutoUser91.Password)
                .IsOnHomePage(AutoUser91);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Business Unit Groups")
                .ExpandOptionLast("Collections")
                .ExpandOptionLast("Business Units")
                .OpenOption("Recycling")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            //Step 9: 
            List<string> allBuninessGroup = PageFactoryManager.Get<BusinessUnitDetailPage>()
                .IsBusinessUnitDetailPage()
                .ClickOnBusinessUnitGroupDdAndGetText();
            //DB: Get all business unit group
            List<BusinessUnitGroupDBModel> businessUnitGroupDBModels = commonFinder.GetBusinessUnitGroupByContractId(2);
            List<string> allBusinessUnitGroupName = businessUnitGroupDBModels.Select(p => p.businessunitgroup).ToList();
            allBusinessUnitGroupName.Add("Select...");
            PageFactoryManager.Get<BusinessUnitDetailPage>()
                .VerifyOnlyBusinessUnitGroupForContractDisplayed(allBuninessGroup, allBusinessUnitGroupName);

            //Step 10:
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .OpenOption("Business Units")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<BusinessUnitListPage>()
                .IsBusinessUnitListPage()
                .ClickOnAddNewItemBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            List<string> allBuninessGroupInBusinessUnitDetail = PageFactoryManager.Get<BusinessUnitDetailPage>()
                .IsBusinessUnitDetailPage()
                .ClickOnBusinessUnitGroupDdAndGetText();
            PageFactoryManager.Get<BusinessUnitDetailPage>()
                .VerifyOnlyBusinessUnitGroupForContractDisplayed(allBuninessGroupInBusinessUnitDetail, allBusinessUnitGroupName);
        }
    }
}
