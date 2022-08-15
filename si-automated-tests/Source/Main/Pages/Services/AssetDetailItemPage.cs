using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class AssetDetailItemPage : BasePageCommonActions
    {
        public readonly By AssetInput = By.XPath("//input[@name='asset']");
        public readonly By AssetReferenceInput = By.XPath("//input[@name='assetReference']");
        public readonly By AssetTypeSelect = By.XPath("//select[@id='assetType.id']");
        public readonly By ProductSelect = By.XPath("//echo-select[contains(@params, 'product')]//select");
        public readonly By StateSelect = By.XPath("//select[@id='state.id']");
        public readonly By AgreementLineSelect = By.XPath("//select[@id='agreementLine.id']");
    }
}
