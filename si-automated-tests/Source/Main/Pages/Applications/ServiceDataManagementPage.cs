using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Threading;


namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class ServiceDataManagementPage : BasePageCommonActions
    {
        public readonly By ServiceLocationTypeSelect = By.XPath("//div[@id='screen1']//select[@id='type']");
        public readonly By NextButton = By.XPath("//div[@id='screen1']//button[@id='next-button']");
        public readonly By PreviousButton = By.XPath("//div[@id='screen2']//button[contains(string(), 'Previous')]");
        public readonly By ApplyButton = By.XPath("//div[@id='screen2']//button[contains(string(), 'Apply')]");
        public readonly By TotalSpan = By.XPath("//div[@id='screen2']//div[contains(@class, 'south-panel2')]//span");
        /// <summary>
        /// SDM: Service Data Management
        /// </summary>
        private readonly string SDMTableXPath = "//div[@id='screen1']//div[@class='grid-canvas']";
        private readonly string SDMRowXPath = "./div[contains(@class, 'slick-row')]";
        private readonly string SDMCheckboxXPath = "./div[contains(@class, 'slick-cell l0')]//input[@type='checkbox']";
        private readonly string SDMPointAddressXPath = "./div[contains(@class, 'slick-cell l1')]";
        private readonly string SDMTypeXPath = "./div[contains(@class, 'slick-cell l2')]";
        private readonly string SDMDescriptionXPath = "./div[contains(@class, 'slick-cell l3')]";
        private readonly string SDMPostcodeXPath = "./div[contains(@class, 'slick-cell l4')]";
        private readonly string SDMStreetXPath = "./div[contains(@class, 'slick-cell l5')]";
        private readonly string SDMStatusXPath = "./div[contains(@class, 'slick-cell l6')]";


        public TableElement ServiceDataManagementTableElement
        {
            get
            {
                return new TableElement(
                        SDMTableXPath,
                        SDMRowXPath, 
                        new List<string>(){ SDMCheckboxXPath, SDMPointAddressXPath, SDMTypeXPath, SDMDescriptionXPath, SDMPostcodeXPath, SDMStreetXPath, SDMStatusXPath });
            }
        }

        public ServiceDataManagementPage ClickPointAddress(string poinAddress)
        {
            ClickOnElement(ServiceDataManagementTableElement.GetCellByValue(1, poinAddress));
            return this;
        }

        public ServiceDataManagementPage ClickMultiPointAddress(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                ServiceDataManagementTableElement.ClickCell(i, 0);
            }
            return this;
        }
    }
}
