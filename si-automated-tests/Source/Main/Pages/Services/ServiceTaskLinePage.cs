using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Models.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceTaskLinePage : BasePageCommonActions
    {
        public readonly By TaskLineTypeSelect = By.XPath("//div[@id='details-tab']//select[@id='taskLineType.id']");
        public readonly By AssetTypeSelect = By.XPath("//div[@id='details-tab']//echo-select[contains(@params, 'assetType')]//select");
        public readonly By MinAssetQtyInput = By.XPath("//div[@id='details-tab']//input[@id='minAssetQty']");
        public readonly By MaxAssetQtyInput = By.XPath("//div[@id='details-tab']//input[@id='maxAssetQty']");
        public readonly By ScheduleAssetQtyInput = By.XPath("//div[@id='details-tab']//input[@name='scheduledAssetQuantity']");
        public readonly By ProductSelect = By.XPath("//div[@id='details-tab']//echo-select[contains(@params, 'product')]//select");
        public readonly By MinProductQtyInput = By.XPath("//div[@id='details-tab']//input[@id='minProductQty']");
        public readonly By MaxProductQtyInput = By.XPath("//div[@id='details-tab']//input[@id='maxProductQty']");
        public readonly By ScheduleProductQtyInput = By.XPath("//div[@id='details-tab']//input[@id='scheduledProductQuantity']");
        public readonly By ProductUnitSelect = By.XPath("//div[@id='details-tab']//echo-select[contains(@params, 'unitOfMeasure')]");
        public readonly By SerialisedCheckbox = By.XPath("//div[@id='details-tab']//input[contains(@data-bind, 'isSerialised.id')]");
        public readonly By DestinationSiteSelect = By.XPath("//div[@id='details-tab']//select[@id='destinationSite.id']");
        public readonly By SiteProductSelect = By.XPath("//div[@id='details-tab']//select[@id='siteProduct.id']");
        public readonly By StartDateInput = By.XPath("//div[@id='details-tab']//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//div[@id='details-tab']//input[@id='endDate.id']");
    }
}
