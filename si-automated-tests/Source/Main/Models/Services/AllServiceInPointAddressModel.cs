using System;
namespace si_automated_tests.Source.Main.Models.Services
{
    public class AllServiceInPointAddressModel
    {
        public string contract { get; set; }
        public string service { get; set; }
        public string serviceUnit { get; set; }
        public string taskCount { get; set; }
        public string scheduleCount { get; set; }
        public string status { get; set; }
        public string serviceUnitLinkToDetail { get; set; }

        public AllServiceInPointAddressModel(string contract, string service, string serviceUnit, string taskCount, string scheduleCount, string status, string serviceUnitLinkToDetail)
        {
            this.contract = contract;
            this.service = service;
            this.serviceUnit = serviceUnit;
            this.taskCount = taskCount;
            this.scheduleCount = scheduleCount;
            this.status = status;
            this.serviceUnitLinkToDetail = serviceUnitLinkToDetail;
        }
    }
}
