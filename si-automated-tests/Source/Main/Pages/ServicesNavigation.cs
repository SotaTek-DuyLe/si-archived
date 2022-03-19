using System;
using System.Collections.Generic;
using System.Text;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages
{
    public class ServicesNavigation : BasePage
    {
        private readonly string regions = "//span[text()='Regions']";
        private readonly string london = "//span[text()='London']";
        private readonly string northStarComercial = "//span[text()='North Star Commercial']";
        private readonly string collections = "//span[text()='Collections']";
        private readonly string comercialCollection = "//span[text()='Commercial Collections']";
        private readonly string activeServiceTask = "//span[text()='Active Service Tasks']";

        public ServicesNavigation GoToActiveServiceTask()
        {
            WaitUtil.WaitForElementVisible(regions);
            ClickOnElement(regions);
            WaitUtil.WaitForElementVisible(london);
            ClickOnElement(london);
            WaitUtil.WaitForElementVisible(northStarComercial);
            ClickOnElement(northStarComercial);
            WaitUtil.WaitForElementVisible(collections);
            ClickOnElement(collections);
            WaitUtil.WaitForElementVisible(comercialCollection);
            ClickOnElement(comercialCollection);
            WaitUtil.WaitForElementVisible(activeServiceTask);
            ClickOnElement(activeServiceTask);
            return this;
        }

    }
}
