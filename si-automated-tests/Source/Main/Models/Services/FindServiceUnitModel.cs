using System;
namespace si_automated_tests.Source.Main.Models.Services
{
    public class FindServiceUnitModel
    {
        public string id { get; set; }
        public string serviceUnit { get; set; }
        public string serviceUnitLocator { get; set; }
        public string pointCount { get; set; }
        public string selectLocator { get; set; }
        public string ServiceUnitName { get; }

        public FindServiceUnitModel()
        {
        }

        public FindServiceUnitModel(string id, string serviceUnitName, string serviceUnitLocator, string pointCount, string selectLocator)
        {
            this.id = id;
            this.serviceUnit = serviceUnitName;
            this.serviceUnitLocator = serviceUnitLocator;
            this.pointCount = pointCount;
            this.selectLocator = selectLocator;
        }
    }
}
