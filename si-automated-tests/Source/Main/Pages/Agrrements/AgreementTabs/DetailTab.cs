using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs
{
    public class DetailTab : BasePage
    {
        private readonly By subExpandBtns = By.XPath("//div[contains(@class,'panel-heading clickable')]");

        private readonly By assetAndProduct = By.XPath("//span[text()='Assets and Products']/parent::div");
        private readonly By mobilization = By.XPath("//span[text()='Mobilization']/parent::span/parent::div");
        private readonly By regular = By.XPath("//span[text()='Regular']/parent::span/parent::div");
        private readonly By deMobilization = By.XPath("//span[text()='De-Mobilization']/parent::span/parent::div");
        private readonly By adHoc = By.XPath("//span[text()='Ad-hoc']/parent::span/parent::div");

        //AssetAndProduct
        private readonly By assertType = By.XPath("//tr/td[@data-bind='text: assetType().name']");
        private readonly By quantity1 = By.XPath("//tr/td[@data-bind='text: assetQuantity']");
        private readonly By productName = By.XPath("//tr/td[@data-bind='text: product().name']");
        private readonly By eWCCode = By.XPath("//tr/td[contains(@data-bind, 'text: productCode')]");
        private readonly By productQuantity = By.XPath("//tr/td[@data-bind='text: productQuantity']");
        private readonly By unitName = By.XPath("//tr/td[@data-bind='text: unit().name']");
        private readonly By tenureName = By.XPath("//tr/td[@data-bind='text: tenure().name']");
        private const string invoiceSchedule = "//tr/td[@data-bind='text: tenure().name']/following-sibling::td[1]/select";
        private const string invoiceAddress = "//tr/td[@data-bind='text: tenure().name']/following-sibling::td[2]/select";
        private readonly By startDateAssetAndProduct = By.XPath("//tr/td[@data-bind='text: tenure().name']/following-sibling::td[3]/span[@title='Start Date']");
        private readonly By endDateAssetAndProduct = By.XPath("//tr/td[@data-bind='text: tenure().name']/following-sibling::td[3]/span[@title='End Date']");

        //Mobilization
        private const string beginLocatorMobi = "//span[text()='Mobilization']/ancestor::div[@data-toggle='collapse']/following-sibling::div";
        private readonly By indexNumb = By.XPath(beginLocatorMobi + "//td[@data-bind='text: $index() + 1']");
        private readonly By taskType = By.XPath(beginLocatorMobi + "//td[@data-bind='text: $index() + 1']/following-sibling::td[1]");
        private const string invoiceScheduleMobi = beginLocatorMobi + "//td[@data-bind='text: $index() + 1']/following-sibling::td[2]//select";
        private const string invoiceAddressMobi = beginLocatorMobi + "//td[@data-bind='text: $index() + 1']/following-sibling::td[3]//select";
        private readonly By startAndEndDate = By.XPath(beginLocatorMobi + "//td[@data-bind='text: $index() + 1']/following-sibling::td[4]//p");
        private const string beginLocatorMobiTaskLine = "//span[text()='Mobilization']/ancestor::div[@data-toggle='collapse']/following-sibling::div//tbody[@data-bind='foreach: taskLines']";
        private readonly By taskLineType = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: name']");
        private readonly By assetType = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: assetType']");
        private readonly By assetQty = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: assetQty']");
        private readonly By minAssetQty = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: minAssetQty']");
        private readonly By maxAssetQty = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: maxAssetQty']");
        private readonly By product = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: product']");
        private readonly By amountOfProduct = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: productQty']");
        private readonly By minProductQty = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: minProductQty']");
        private readonly By maxProductQty = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: maxProductQty']");
        private readonly By unit = By.XPath(beginLocatorMobiTaskLine + "//td[@data-bind='text: unit']");
        private readonly By startDateCover = By.XPath(beginLocatorMobiTaskLine + "//span[@title='Start Date']");
        private readonly By endDateCover = By.XPath(beginLocatorMobiTaskLine + "//span[@title='End Date']");

        //Regular
        private const string beginLocatorRegular = "//span[text()='Regular']/ancestor::div[@data-toggle='collapse']/following-sibling::div";
        private readonly By indexNumbRe = By.XPath(beginLocatorRegular + "//td[@data-bind='text: $index() + 1']");
        private readonly By taskTypeRe = By.XPath(beginLocatorRegular + "//td[@data-bind='text: $index() + 1']/following-sibling::td[1]");
        private const string invoiceScheduleRe = beginLocatorRegular + "//td[@data-bind='text: $index() + 1']/following-sibling::td[2]//select";
        private const string invoiceAddressRe = beginLocatorRegular + "//td[@data-bind='text: $index() + 1']/following-sibling::td[3]//select";
        private readonly By frequencyRe = By.XPath(beginLocatorRegular + "//p[@data-bind='text: scheduleRequirement().description']");
        private readonly By startEndDateRe = By.XPath(beginLocatorRegular + "//td[@data-bind='text: $index() + 1']/following-sibling::td[5]//p");
        private const string beginLocatorReTaskLine = "//span[text()='Regular']/ancestor::div[@data-toggle='collapse']/following-sibling::div//tbody[@data-bind='foreach: taskLines']";
        private readonly By taskLineTypeRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: name']");
        private readonly By assetTypeRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: assetType']");
        private readonly By assetQtyRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: assetQty']");
        private readonly By minAssetQtyRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: minAssetQty']");
        private readonly By maxAssetQtyRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: maxAssetQty']");
        private readonly By productRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: product']");
        private readonly By amountOfProductRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: productQty']");
        private readonly By minProductQtyRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: minProductQty']");
        private readonly By maxProductQtyRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: maxProductQty']");
        private readonly By unitRe = By.XPath(beginLocatorReTaskLine + "//td[@data-bind='text: unit']");
        private readonly By destinationSiteRe = By.XPath(beginLocatorReTaskLine + "//td[contains(@data-bind, 'text: destinationSite')]");
        private readonly By siteProductRe = By.XPath(beginLocatorReTaskLine + "//td[contains(@data-bind, 'text: siteProduct')]");
        private readonly By startDateCoverRe = By.XPath(beginLocatorReTaskLine + "//span[@title='Start Date']");
        private readonly By endDateCoverRe = By.XPath(beginLocatorReTaskLine + "//span[@title='End Date']");

        //De-Mobilization
        private const string beginLocatorDeMobilization = "//span[text()='De-Mobilization']/ancestor::div[@data-toggle='collapse']/following-sibling::div";
        private readonly By indexNumbDeMobi = By.XPath(beginLocatorDeMobilization + "//td[@data-bind='text: $index() + 1']");
        private readonly By taskTypeDeMobi = By.XPath(beginLocatorDeMobilization + "//td[@data-bind='text: $index() + 1']/following-sibling::td[1]");
        private const string invoiceScheduleDeMobi = beginLocatorDeMobilization + "//td[@data-bind='text: $index() + 1']/following-sibling::td[2]//select";
        private const string invoiceAddressDeMobi = beginLocatorDeMobilization + "//td[@data-bind='text: $index() + 1']/following-sibling::td[3]//select";
        private readonly By startAndEndDateDeMobi = By.XPath(beginLocatorDeMobilization + "//td[@data-bind='text: $index() + 1']/following-sibling::td[4]//p");
        private const string beginLocatorDeMobiTaskLine = "//span[text()='De-Mobilization']/ancestor::div[@data-toggle='collapse']/following-sibling::div//tbody[@data-bind='foreach: taskLines']";
        private readonly By taskLineTypeDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: name']");
        private readonly By assetTypeDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: assetType']");
        private readonly By assetQtyDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: assetQty']");
        private readonly By minAssetQtyDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: minAssetQty']");
        private readonly By maxAssetQtyDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: maxAssetQty']");
        private readonly By productDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: product']");
        private readonly By amountOfProductDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: productQty']");
        private readonly By minProductQtyDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: minProductQty']");
        private readonly By maxProductQtyDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: maxProductQty']");
        private readonly By unitDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//td[@data-bind='text: unit']");
        private readonly By startDateCoverDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//span[@title='Start Date']");
        private readonly By endDateCoverDeMobi = By.XPath(beginLocatorDeMobiTaskLine + "//span[@title='End Date']");

        //Ad-hoc
        private const string beginLocatorAdhoc = "//span[text()='Ad-hoc']/ancestor::div[@data-toggle='collapse']/following-sibling::div";
        private readonly By indexNumbAdhoc = By.XPath(beginLocatorAdhoc + "//td[@data-bind='text: $index() + 1']");
        private readonly By taskTypeAdhoc = By.XPath(beginLocatorAdhoc + "//td[@data-bind='text: $index() + 1']/following-sibling::td[1]");
        private const string invoiceScheduleAdhoc = beginLocatorAdhoc + "//td[@data-bind='text: $index() + 1']/following-sibling::td[2]//select";
        private const string invoiceAddressAdhoc = beginLocatorAdhoc + "//td[@data-bind='text: $index() + 1']/following-sibling::td[3]//select";
        private readonly By startAndEndDateAdhoc = By.XPath(beginLocatorAdhoc + "//td[@data-bind='text: $index() + 1']/following-sibling::td[4]//p");
        private const string beginLocatorAdhocTaskLine = "//span[text()='Ad-hoc']/ancestor::div[@data-toggle='collapse']/following-sibling::div//tbody[@data-bind='foreach: taskLines']";
        private readonly By taskLineTypeAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: name']");
        private readonly By assetTypeAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: assetType']");
        private readonly By assetQtyAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: assetQty']");
        private readonly By minAssetQtyAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: minAssetQty']");
        private readonly By maxAssetQtyAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: maxAssetQty']");
        private readonly By productAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: product']");
        private readonly By amountOfProductAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: productQty']");
        private readonly By minProductQtyAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: minProductQty']");
        private readonly By maxProductQtyAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: maxProductQty']");
        private readonly By unitAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//td[@data-bind='text: unit']");
        private readonly By startDateCoverAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//span[@title='Start Date']");
        private readonly By endDateCoverAdhoc = By.XPath(beginLocatorAdhocTaskLine + "//span[@title='End Date']");

        private readonly By createAdhocBtn = By.XPath("//button[text()='Create Ad-Hoc Task']");

        private readonly By assetAndProductAssetTypeStartDate = By.XPath("//tbody[contains(@data-bind,'assetProducts')]//span[@title='Start Date']");
        private readonly By regularAssertTypeStartDate = By.XPath("//span[text()='Regular']/ancestor::div[1]/following-sibling::div//span[contains(@data-bind,'displayStartDate')]");
        private readonly By serviceTaskLineTypeStartDates = By.XPath("//th[text()='Task Line Type']/ancestor::thead[1]/following-sibling::tbody//span[@title='Start Date']");


        //AssetAndProduct
        public DetailTab ClickAssetAndProductAndVerify(string expectValue)
        {
            ClickOnElement(assetAndProduct);
            //Verify expanded
            Assert.AreEqual(GetAttributeValue(assetAndProduct, "aria-expanded"), expectValue);
            return this;
        }

        public AsserAndProductModel GetAllInfoAssetAndProduct()
        {
            string assertTypeM = GetElementText(assertType);
            string quantity1M = GetElementText(quantity1);
            string productM = GetElementText(productName);
            string ewcCodeM = GetElementText(eWCCode);
            string productQuantityM = GetElementText(productQuantity);
            string unitM = GetElementText(unitName);
            string tenureM = GetElementText(tenureName);
            string[] invoiceScheduleM = new string[3];
            List<IWebElement> invoiceScheduleList = GetAllElements(invoiceSchedule + "/option");
            for (int i = 0; i < invoiceScheduleList.Count; i++)
            {
                invoiceScheduleM[i] = GetElementText(invoiceScheduleList[i]);
            }
            string[] invoiceAddressM = new string[2];
            List<IWebElement> invoiceAddressList = GetAllElements(invoiceAddress + "/option");
            for (int i = 0; i < invoiceAddressList.Count; i++)
            {
                invoiceAddressM[i] = GetElementText(invoiceAddressList[i]);
            }
            string startDateM = GetElementText(startDateAssetAndProduct);
            string endDateM = GetElementText(endDateAssetAndProduct);
            return new AsserAndProductModel(assertTypeM, quantity1M, productM, ewcCodeM, productQuantityM, unitM, tenureM, invoiceScheduleM, invoiceAddressM, startDateM, endDateM);
        }

        public AsserAndProductModel GetAllInfoAssetAndProductAgreement()
        {
            string assertTypeM = GetElementText(assertType);
            string quantity1M = GetElementText(quantity1);
            string productM = GetElementText(productName);
            string productQuantityM = GetElementText(productQuantity);
            string unitM = GetElementText(unitName);
            string tenureM = GetElementText(tenureName);
            string startDateM = GetElementText(startDateAssetAndProduct);
            string endDateM = GetElementText(endDateAssetAndProduct);
            return new AsserAndProductModel(assertTypeM, quantity1M, productM, "", productQuantityM, unitM, tenureM, new string[1], new string[1], startDateM, endDateM);
        }

        public DetailTab VerifyAssertAndProductInfo(AsserAndProductModel productModel)
        {
            Assert.AreEqual(productModel.AssetType, "1100L");
            Assert.AreEqual(productModel.Quantity1, "1");
            Assert.AreEqual(productModel.Product, "General Recycling");
            Assert.AreEqual(productModel.EwcCode, "150106");
            Assert.AreEqual(productModel.ProductQuantity, "");
            Assert.AreEqual(productModel.Unit, "Kilograms");
            Assert.AreEqual(productModel.Tenure, "Rental");
            Assert.AreEqual(productModel.InvoiceSchedule, CommonConstants.InvoiceScheduleAssetAndProduct);
            Assert.AreEqual(productModel.InvoiceAddress, CommonConstants.InvoiceAddressAssetAndProduct);
            //Default value
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceSchedule), CommonConstants.InvoiceScheduleAssetAndProduct[0]);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceAddress), CommonConstants.InvoiceAddressAssetAndProduct[0]);
            //Verify disabled
            Assert.AreEqual(GetAttributeValue(invoiceSchedule, "disabled"), "true");
            Assert.AreEqual(productModel.StartDate, "03/03/2022");
            Assert.AreEqual(productModel.EndDate, "01/01/2050");
            return this;
        }

        public DetailTab VerifyAssertAndProductInfo(AsserAndProductModel productModel, AsserAndProductModel input)
        {
            Assert.AreEqual(productModel.AssetType, input.AssetType);
            Assert.AreEqual(productModel.Quantity1, input.Quantity1);
            Assert.AreEqual(productModel.Product, input.Product);
            Assert.AreEqual(productModel.ProductQuantity, input.ProductQuantity);
            Assert.AreEqual(productModel.Unit, input.Unit);
            Assert.AreEqual(productModel.Tenure, input.Tenure);
            Assert.AreEqual(productModel.StartDate, input.StartDate);
            return this;
        }

        //mobilization
        public DetailTab ClickMobilizationAndVerify(string expectValue)
        {
            ScrollDownToElement(mobilization);
            ClickOnElement(mobilization);
            //Verify expanded
            Assert.AreEqual(GetAttributeValue(mobilization, "aria-expanded"), expectValue);
            return this;
        }

        public MobilizationModel GetAllInfoMobilization()
        {
            string indexM = GetElementText(indexNumb);
            string taskTypeM = GetElementText(taskType);
            List<IWebElement> invoiceScheduleMobiList = GetAllElements(invoiceScheduleMobi + "/option");
            string[] invoiceScheduleM = new string[3];
            for (int i = 0; i < invoiceScheduleMobiList.Count; i++)
            {
                invoiceScheduleM[i] = GetElementText(invoiceScheduleMobiList[i]);
            }
            List<IWebElement> invoiceAddressMobiList = GetAllElements(invoiceAddressMobi + "/option");
            string[] invoiceAddressM = new string[2];
            for (int i = 0; i < invoiceAddressMobiList.Count; i++)
            {
                invoiceAddressM[i] = GetElementText(invoiceAddressMobiList[i]);
            }
            string startEndDateM = GetElementText(startAndEndDate);
            string taskLineTypeM = GetElementText(taskLineType);
            string assetTypeM = GetElementText(assetType);
            string assetQtyM = GetElementText(assetQty);
            string minAssetM = GetElementText(minAssetQty);
            string maxAssetM = GetElementText(maxAssetQty);
            string productM = GetElementText(product);
            string amountOfProductM = GetElementText(amountOfProduct);
            string minProdQtyM = GetElementText(minProductQty);
            string maxProdQtyM = GetElementText(maxProductQty);
            string unitM = GetElementText(unit);
            string startDateM = GetElementText(startDateCover);
            string endDateM = GetElementText(endDateCover);
            return new MobilizationModel(indexM, taskTypeM, invoiceScheduleM, invoiceAddressM, startEndDateM, taskLineTypeM, assetTypeM,
                assetQtyM, minAssetM, maxAssetM, productM, amountOfProductM, minProdQtyM, maxProdQtyM, unitM, startDateM, endDateM);
        }

        public DetailTab VerifyMobilizationInfo(MobilizationModel mobilizationModel)
        {
            Assert.AreEqual(mobilizationModel.Index, "1");
            Assert.AreEqual(mobilizationModel.TaskType, "Deliver Commercial Bin");
            Assert.AreEqual(mobilizationModel.InvoiceSchedule, CommonConstants.InvoiceScheduleMobilization);
            Assert.AreEqual(mobilizationModel.InvoiceAddress, CommonConstants.InvoiceAddressMobilization);
            Assert.AreEqual(mobilizationModel.StartEndDate, "03/03/2022 - 01/01/2050");
            Assert.AreEqual(mobilizationModel.TaskLineType, "Deliver");
            Assert.AreEqual(mobilizationModel.AssetType, "1100L");
            Assert.AreEqual(mobilizationModel.AssetQuantity, "1");
            Assert.AreEqual(mobilizationModel.MinAssetQty, "0");
            Assert.AreEqual(mobilizationModel.MaxAssetQty, "0");
            Assert.AreEqual(mobilizationModel.Product, "General Recycling");
            Assert.AreEqual(mobilizationModel.AmountOfProduct, "0");
            Assert.AreEqual(mobilizationModel.MinProductQty, "0");
            Assert.AreEqual(mobilizationModel.MaxProductQty, "0");
            Assert.AreEqual(mobilizationModel.Unit, "Kilograms");
            Assert.AreEqual(mobilizationModel.StartDateCover, "03/03/2022");
            Assert.AreEqual(mobilizationModel.EndDateCover, "01/01/2050");
            //Default value
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceScheduleMobi), CommonConstants.InvoiceScheduleMobilization[0]);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceAddressMobi), CommonConstants.InvoiceAddressMobilization[0]);
            //Verify disabled
            Assert.AreEqual(GetAttributeValue(invoiceScheduleMobi, "disabled"), "true");
            return this;
        }
        public DetailTab VerifyMobilizationInfo(MobilizationModel mobilizationModel, MobilizationModel input)
        {
            Assert.AreEqual(mobilizationModel.TaskLineType, input.TaskLineType);
            Assert.AreEqual(mobilizationModel.AssetType, input.AssetType);
            Assert.AreEqual(mobilizationModel.AssetQuantity, input.AssetQuantity);
            Assert.AreEqual(mobilizationModel.Product, input.Product);
            Assert.AreEqual(mobilizationModel.AmountOfProduct, input.AmountOfProduct);
            Assert.AreEqual(mobilizationModel.Unit, input.Unit);
            Assert.AreEqual(mobilizationModel.StartDateCover, input.StartDateCover);
            return this;
        }

            //regular
            public DetailTab ClickRegularAndVerify(string expectValue)
        {
            ScrollDownToElement(regular);
            ClickOnElement(regular);
            //Verify expanded
            Assert.AreEqual(GetAttributeValue(regular, "aria-expanded"), expectValue);
            return this;
        }

        public RegularModel GetAllInfoRegular()
        {
            string indexR = GetElementText(indexNumbRe);
            string taskTypeR = GetElementText(taskTypeRe);
            List<IWebElement> invoiceScheduleReList = GetAllElements(invoiceScheduleRe + "/option");
            string[] invoiceScheduleR = new string[3];
            for (int i = 0; i < invoiceScheduleReList.Count; i++)
            {
                invoiceScheduleR[i] = GetElementText(invoiceScheduleReList[i]);
            }
            List<IWebElement> invoiceAddressReList = GetAllElements(invoiceAddressRe + "/option");
            string[] invoiceAddressR = new string[2];
            for (int i = 0; i < invoiceAddressReList.Count; i++)
            {
                invoiceAddressR[i] = GetElementText(invoiceAddressReList[i]);
            }
            string frequencyR = GetElementText(frequencyRe);
            string startEndDateR = GetElementText(startEndDateRe);
            string taskLineTypeR = GetElementText(taskLineTypeRe);
            string assetTypeR = GetElementText(assetTypeRe);
            string assetQtyR = GetElementText(assetQtyRe);
            string minAssetR = GetElementText(minAssetQtyRe);
            string maxAssetR = GetElementText(maxAssetQtyRe);
            string productR = GetElementText(productRe);
            string amountOfProductR = GetElementText(amountOfProductRe);
            string minProdQtyR = GetElementText(minProductQtyRe);
            string maxProdQtyR = GetElementText(maxProductQtyRe);
            string unitR = GetElementText(unitRe);
            string destinationSiteR = GetElementText(destinationSiteRe);
            string siteProductR = GetElementText(siteProductRe);
            string startDateCoverR = GetElementText(startDateCoverRe);
            string endDateCoverR = GetElementText(endDateCoverRe);
            return new RegularModel(indexR, taskTypeR, invoiceScheduleR, invoiceAddressR, frequencyR, startEndDateR, taskLineTypeR, assetTypeR, assetQtyR, minAssetR, maxAssetR, productR, amountOfProductR, minProdQtyR, maxProdQtyR, unitR, destinationSiteR,
              siteProductR, startDateCoverR, endDateCoverR);
        }

        public DetailTab VerifyRegularInfo(RegularModel regularModel)
        {
            Assert.AreEqual(regularModel.Index, "1");
            Assert.AreEqual(regularModel.TaskType, "Commercial Collection");
            Assert.AreEqual(regularModel.InvoiceSchedule, CommonConstants.InvoiceScheduleMobilization);
            Assert.AreEqual(regularModel.InvoiceAddress, CommonConstants.InvoiceAddressMobilization);
            Assert.AreEqual(regularModel.Frequency, "Once per week on any day");
            Assert.AreEqual(regularModel.StartEndDate, "03/03/2022 - 01/01/2050");
            Assert.AreEqual(regularModel.TaskLineType, "Service");
            Assert.AreEqual(regularModel.AssetType, "1100L");
            Assert.AreEqual(regularModel.AssetQuantity, "1");
            Assert.AreEqual(regularModel.MinAssetQty, "0");
            Assert.AreEqual(regularModel.MaxAssetQty, "0");
            Assert.AreEqual(regularModel.Product, "General Recycling");
            Assert.AreEqual(regularModel.AmountOfProduct, "0");
            Assert.AreEqual(regularModel.MinProductQty, "0");
            Assert.AreEqual(regularModel.MaxProductQty, "0");
            Assert.AreEqual(regularModel.Unit, "Kilograms");
            Assert.AreEqual(regularModel.DestinationSite, "");
            Assert.AreEqual(regularModel.SiteProduct, "");
            Assert.AreEqual(regularModel.StartDateCover, "03/03/2022");
            Assert.AreEqual(regularModel.EndDateCover, "01/01/2050");
            //Default value
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceScheduleRe), CommonConstants.InvoiceScheduleMobilization[0]);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceAddressRe), CommonConstants.InvoiceAddressMobilization[0]);
            //Verify disabled
            Assert.AreEqual(GetAttributeValue(invoiceScheduleRe, "disabled"), "true");
            return this;
        }

        public DetailTab VerifyRegularTaskTypeDate(string dateRange) 
        {
            Assert.AreEqual(dateRange, GetElementText(startEndDateRe));
            return this;
        }
        public DetailTab VerifyRegularTaskLineTypeStartDate(String startDate)
        {
            Assert.AreEqual(startDate, GetElementText(startDateCoverRe));
            return this;
        }
        public DetailTab VerifyRegularInfo(RegularModel regularModel, RegularModel input)
        {
            Assert.AreEqual(regularModel.TaskLineType, input.TaskLineType);
            Assert.AreEqual(regularModel.AssetType, input.AssetType);
            Assert.AreEqual(regularModel.AssetQuantity, input.AssetQuantity);
            Assert.AreEqual(regularModel.Product, input.Product);
            Assert.AreEqual(regularModel.AmountOfProduct, input.AmountOfProduct);
            Assert.AreEqual(regularModel.Unit, input.Unit);
            Assert.AreEqual(regularModel.StartDateCover, input.StartDateCover);
            return this;
        }

        //De-Mobilization
        public DetailTab ClickDeMobilizationAndVerify(string expectValue)
        {
            ScrollDownToElement(deMobilization);
            ClickOnElement(deMobilization);
            //Verify expanded
            Assert.AreEqual(GetAttributeValue(deMobilization, "aria-expanded"), expectValue);
            return this;
        }

        public MobilizationModel GetAllInfoDeMobilization()
        {
            string indexM = GetElementText(indexNumbDeMobi);
            string taskTypeM = GetElementText(taskTypeDeMobi);
            List<IWebElement> invoiceScheduleMobiList = GetAllElements(invoiceScheduleDeMobi + "/option");
            string[] invoiceScheduleM = new string[3];
            for (int i = 0; i < invoiceScheduleMobiList.Count; i++)
            {
                invoiceScheduleM[i] = GetElementText(invoiceScheduleMobiList[i]);
            }
            List<IWebElement> invoiceAddressMobiList = GetAllElements(invoiceAddressDeMobi + "/option");
            string[] invoiceAddressM = new string[2];
            for (int i = 0; i < invoiceAddressMobiList.Count; i++)
            {
                invoiceAddressM[i] = GetElementText(invoiceAddressMobiList[i]);
            }
            string startEndDateM = GetElementText(startAndEndDateDeMobi);
            string taskLineTypeM = GetElementText(taskLineTypeDeMobi);
            string assetTypeM = GetElementText(assetTypeDeMobi);
            string assetQtyM = GetElementText(assetQtyDeMobi);
            string minAssetM = GetElementText(minAssetQtyDeMobi);
            string maxAssetM = GetElementText(maxAssetQtyDeMobi);
            string productM = GetElementText(productDeMobi);
            string amountOfProductM = GetElementText(amountOfProductDeMobi);
            string minProdQtyM = GetElementText(minProductQtyDeMobi);
            string maxProdQtyM = GetElementText(maxProductQtyDeMobi);
            string unitM = GetElementText(unitDeMobi);
            string startDateM = GetElementText(startDateCoverDeMobi);
            string endDateM = GetElementText(endDateCoverDeMobi);
            return new MobilizationModel(indexM, taskTypeM, invoiceScheduleM, invoiceAddressM, startEndDateM, taskLineTypeM, assetTypeM,
                assetQtyM, minAssetM, maxAssetM, productM, amountOfProductM, minProdQtyM, maxProdQtyM, unitM, startDateM, endDateM);
        }

        public DetailTab VerifyDeMobilizationInfo(MobilizationModel mobilizationModel)
        {
            Assert.AreEqual(mobilizationModel.Index, "1");
            Assert.AreEqual(mobilizationModel.TaskType, "Remove Commercial Bin");
            Assert.AreEqual(mobilizationModel.InvoiceSchedule, CommonConstants.InvoiceScheduleMobilization);
            Assert.AreEqual(mobilizationModel.InvoiceAddress, CommonConstants.InvoiceAddressMobilization);
            Assert.AreEqual(mobilizationModel.StartEndDate, "03/03/2022 - 01/01/2050");
            Assert.AreEqual(mobilizationModel.TaskLineType, "Remove");
            Assert.AreEqual(mobilizationModel.AssetType, "1100L");
            Assert.AreEqual(mobilizationModel.AssetQuantity, "1");
            Assert.AreEqual(mobilizationModel.MinAssetQty, "0");
            Assert.AreEqual(mobilizationModel.MaxAssetQty, "0");
            Assert.AreEqual(mobilizationModel.Product, "General Recycling");
            Assert.AreEqual(mobilizationModel.AmountOfProduct, "0");
            Assert.AreEqual(mobilizationModel.MinProductQty, "0");
            Assert.AreEqual(mobilizationModel.MaxProductQty, "0");
            Assert.AreEqual(mobilizationModel.Unit, "Kilograms");
            Assert.AreEqual(mobilizationModel.StartDateCover, "03/03/2022");
            Assert.AreEqual(mobilizationModel.EndDateCover, "01/01/2050");
            //Default value
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceScheduleDeMobi), CommonConstants.InvoiceScheduleMobilization[0]);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceAddressDeMobi), CommonConstants.InvoiceAddressMobilization[0]);
            //Verify disabled
            Assert.AreEqual(GetAttributeValue(invoiceScheduleDeMobi, "disabled"), "true");
            return this;
        }

        //Ad-Hoc
        public DetailTab ClickAdHocAndVerify(string expectValue)
        {
            ScrollDownToElement(adHoc);
            ClickOnElement(adHoc);
            //Verify expanded
            Assert.AreEqual(GetAttributeValue(adHoc, "aria-expanded"), expectValue);
            ScrollToBottomOfPage();
            return this;
        }

        public List<MobilizationModel> GetAllInfoAdhoc()
        {
            List<MobilizationModel> allAdhoc = new List<MobilizationModel>();

            List<IWebElement> allIds = GetAllElements(indexNumbAdhoc);
            List<IWebElement> allTaskType = GetAllElements(taskTypeAdhoc);
            List<IWebElement> allInvoiceSchedule = GetAllElements(invoiceScheduleAdhoc + "/option");
            List<IWebElement> allInvoiceAddress = GetAllElements(invoiceAddressAdhoc + "/option");
            List<IWebElement> allStartEndDate = GetAllElements(startAndEndDateAdhoc);
            List<IWebElement> allTaskLineType = GetAllElements(taskLineTypeAdhoc);
            List<IWebElement> allAssetType = GetAllElements(assetTypeAdhoc);
            List<IWebElement> allAssetQty = GetAllElements(assetQtyAdhoc);
            List<IWebElement> allMinAsset = GetAllElements(minAssetQtyAdhoc);
            List<IWebElement> allMaxAsset = GetAllElements(maxAssetQtyAdhoc);
            List<IWebElement> allProduct = GetAllElements(productAdhoc);
            List<IWebElement> allAmount = GetAllElements(amountOfProductAdhoc);
            List<IWebElement> alMinProductQty = GetAllElements(minProductQtyAdhoc);
            List<IWebElement> allMaxProductQty = GetAllElements(maxProductQtyAdhoc);
            List<IWebElement> allUnit = GetAllElements(unitAdhoc);
            List<IWebElement> allStartDate = GetAllElements(startDateCoverAdhoc);
            List<IWebElement> allEndDate = GetAllElements(endDateCoverAdhoc);

            for (int i = 0; i < allIds.Count; i++)
            {
                string indexM = GetElementText(allIds[i]);
                string taskTypeM = GetElementText(allTaskType[i]);
                string[] invoiceScheduleM = new string[3];
                string[] invoiceAddressM = new string[2];
                if (i == 0)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        invoiceScheduleM[j] = GetElementText(allInvoiceSchedule[j]);
                    }

                    for (int j = 0; j < 2; j++)
                    {
                        invoiceAddressM[j] = GetElementText(allInvoiceAddress[j]);
                    }

                }
                else
                {
                    invoiceScheduleM[0] = GetElementText(allInvoiceSchedule[3]);
                    invoiceScheduleM[1] = GetElementText(allInvoiceSchedule[4]);
                    invoiceScheduleM[2] = GetElementText(allInvoiceSchedule[5]);
                    invoiceAddressM[0] = GetElementText(allInvoiceAddress[2]);
                    invoiceAddressM[1] = GetElementText(allInvoiceAddress[3]);
                }

                string startEndDateM = GetElementText(allStartEndDate[i]);
                string taskLineTypeM = GetElementText(allTaskLineType[i]);
                string assetTypeM = GetElementText(allAssetType[i]);
                string assetQtyM = GetElementText(allAssetQty[i]);
                string minAssetM = GetElementText(allMinAsset[i]);
                string maxAssetM = GetElementText(allMaxAsset[i]);
                string productM = GetElementText(allProduct[i]);
                string amountOfProductM = GetElementText(allAmount[i]);
                string minProdQtyM = GetElementText(alMinProductQty[i]);
                string maxProdQtyM = GetElementText(allMaxProductQty[i]);
                string unitM = GetElementText(allUnit[i]);
                string startDateM = GetElementText(allStartDate[i]);
                string endDateM = GetElementText(allEndDate[i]);
                allAdhoc.Add(new MobilizationModel(indexM, taskTypeM, invoiceScheduleM, invoiceAddressM, startEndDateM, taskLineTypeM, assetTypeM, assetQtyM, minAssetM, maxAssetM, productM, amountOfProductM, minProdQtyM, maxProdQtyM, unitM, startDateM, endDateM));
            }
            return allAdhoc;
        }

        public DetailTab VerifyAdhocInfo(List<MobilizationModel> mobilizationModelList)
        {
            for (int i = 0; i < mobilizationModelList.Count; i++)
            {
                if(i == 0)
                {
                    Assert.AreEqual(mobilizationModelList[i].TaskType, "Repair Commercial Bin");
                }
                else
                {
                    Assert.AreEqual(mobilizationModelList[i].TaskType, "Collect Missed Commercial");
                }
                Assert.AreEqual(mobilizationModelList[i].InvoiceSchedule, CommonConstants.InvoiceScheduleMobilization);
                Assert.AreEqual(mobilizationModelList[i].InvoiceAddress, CommonConstants.InvoiceAddressMobilization);
                Assert.AreEqual(mobilizationModelList[i].StartEndDate, "03/03/2022 - 01/01/2050");
                Assert.AreEqual(mobilizationModelList[i].TaskLineType, CommonConstants.TaskLineType[0]);
                Assert.AreEqual(mobilizationModelList[i].AssetType, CommonConstants.AssetType[0]);
                Assert.AreEqual(mobilizationModelList[i].AssetQuantity, "1");
                Assert.AreEqual(mobilizationModelList[i].MinAssetQty, "0");
                Assert.AreEqual(mobilizationModelList[i].MaxAssetQty, "0");
                Assert.AreEqual(mobilizationModelList[i].Product, CommonConstants.ProductName[0]);
                Assert.AreEqual(mobilizationModelList[i].AmountOfProduct, "0");
                Assert.AreEqual(mobilizationModelList[i].MinProductQty, "0");
                Assert.AreEqual(mobilizationModelList[i].MaxProductQty, "0");
                Assert.AreEqual(mobilizationModelList[i].Unit, CommonConstants.Unit[0]);
                Assert.AreEqual(mobilizationModelList[i].StartDateCover, CommonConstants.StartDateAgreement);
                Assert.AreEqual(mobilizationModelList[i].EndDateCover, CommonConstants.EndDateAgreement);
                
            }
            return this;
        }
        public DetailTab VerifyAdhocInfo(List<MobilizationModel> mobilizationModelList, List<MobilizationModel> input)
        {
            for (int i = 0; i < mobilizationModelList.Count; i++)
            {
                Assert.AreEqual(mobilizationModelList[i].TaskLineType, input[i].TaskLineType);
                Assert.AreEqual(mobilizationModelList[i].AssetType, input[i].AssetType);
                Assert.AreEqual(mobilizationModelList[i].AssetQuantity, input[i].AssetQuantity);
                Assert.AreEqual(mobilizationModelList[i].Product, input[i].Product);
                Assert.AreEqual(mobilizationModelList[i].AmountOfProduct, input[i].AmountOfProduct);
                Assert.AreEqual(mobilizationModelList[i].Unit, input[i].Unit);
                Assert.AreEqual(mobilizationModelList[i].StartDateCover, input[i].StartDateCover);

            }
            return this;
        }

        public DetailTab VerifyAdhocInfo(List<MobilizationModel> mobilizationModelList, MobilizationModel input, int num)
        {
            int n = 0;
            for (int i = 0; i < mobilizationModelList.Count; i++)
            {
                if(mobilizationModelList[i].TaskLineType == input.TaskLineType && mobilizationModelList[i].StartDateCover == input.StartDateCover)
                {
                    Assert.AreEqual(mobilizationModelList[i].TaskLineType, input.TaskLineType);
                    Assert.AreEqual(mobilizationModelList[i].AssetType, input.AssetType);
                    Assert.AreEqual(mobilizationModelList[i].AssetQuantity, input.AssetQuantity);
                    Assert.AreEqual(mobilizationModelList[i].Product, input.Product);
                    Assert.AreEqual(mobilizationModelList[i].AmountOfProduct, input.AmountOfProduct);
                    Assert.AreEqual(mobilizationModelList[i].Unit, input.Unit);
                    Assert.AreEqual(mobilizationModelList[i].StartDateCover, input.StartDateCover);
                    n++;
                }
                
            }
            Assert.AreEqual(n, num);
            return this;
        }

        public DetailTab VerifyCreateAdhocButtonsAreEnabled()
        {
            IList<IWebElement> createAdhocBtns = WaitUtil.WaitForAllElementsVisible(createAdhocBtn);
            foreach (var btn in createAdhocBtns)
            {
                Assert.AreEqual(true, btn.Enabled);
            }
            return this;
        }
        public IList<IWebElement> GetCreateAdhocBtnList()
        {
            ScrollDownToElement(createAdhocBtn);
            IList<IWebElement> createAdhocBtns = WaitUtil.WaitForAllElementsVisible(createAdhocBtn);
            Console.WriteLine("count:" + createAdhocBtns.Count);
            return createAdhocBtns;
        }
        public AgreementTaskDetailsPage ClickAdHocBtn(IWebElement e)
        {
            ClickOnElement(e);
            return new AgreementTaskDetailsPage();
        }
        public DetailTab VerifyMobilizationPanelDisappear()
        {
            Assert.IsTrue(IsControlUnDisplayed(beginLocatorMobi));
            return this;
        }
        public DetailTab VerifyDeMobilizationPanelDisappear()
        {
            Assert.IsTrue(IsControlUnDisplayed(beginLocatorDeMobilization));
            return this;
        }
        public DetailTab ExpandAllAgreementFields()
        {
            IList<IWebElement> fields = WaitUtil.WaitForAllElementsVisible(subExpandBtns);
            foreach (var field in fields)
            {
                Thread.Sleep(300);
                field.Click();
            }
            return this;
        }
        public DetailTab VerifyTaskLineTypeStartDates(string startDate)
        {
            Assert.AreEqual(startDate, GetElementText(serviceTaskLineTypeStartDates));
            IList<IWebElement> elements = WaitUtil.WaitForAllElementsVisible(serviceTaskLineTypeStartDates);
            foreach (IWebElement element in elements)
            {
                Assert.AreEqual(startDate, GetElementText(element));
            }
            return this;
        }
        public DetailTab VerifyRegularAssetTypeStartDate(string startDate)
        {
            ScrollDownToElement(regularAssertTypeStartDate);
            Assert.AreEqual(startDate, GetElementText(regularAssertTypeStartDate));
            return this;
        }
        public DetailTab VerifyAssetAndProductAssetTypeStartDate(string startDate)
        {

            Assert.AreEqual(startDate, GetElementText(assetAndProductAssetTypeStartDate));
            return this;
        }
    }

}
