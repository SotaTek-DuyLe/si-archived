using System;
namespace si_automated_tests.Source.Main.Models
{
    public class ActiveSeviceModel
    {
        public string serviceUnit { get; set; }
        public string service { get; set; }
        public string eventLocator { get; set; }

        public ActiveSeviceModel(string eventLocator, string serviceUnitValue, string serviceValue)
        {
            this.eventLocator = eventLocator;
            this.serviceUnit = serviceUnitValue;
            this.service = serviceValue;
        }
    }
}
