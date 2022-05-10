using System;
using System.Collections.Generic;

namespace si_automated_tests.Source.Main.Models
{
    public class ActiveSeviceModel
    {
        public string serviceUnit { get; set; }
        public string service { get; set; }
        public string eventLocator { get; set; }
        public string schedule { get; set; }
        public string lastService { get; set; }
        public string nextService { get; set; }
        public string assetTypeService { get; set; }
        public string allocationService { get; set; }
        public string timeBandService { get; set; }
        public string assuredTaskService { get; set; }
        public string clientRefService { get; set; }
        public List<ChildSchedule> listSchedule;

        public ActiveSeviceModel(string eventLocator, string serviceUnitValue, string serviceValue)
        {
            this.eventLocator = eventLocator;
            this.serviceUnit = serviceUnitValue;
            this.service = serviceValue;
        }

        public class ChildSchedule
        {
            public string round { get; set; }
            public string stateRound { get; set; }
            public string lastRound { get; set; }
            public string nextRound { get; set; }
            public string assetTypeRound { get; set; }
            public string allocationRound { get; set; }
            public string timeBandRound { get; set; }
            public string assuredTaskRound { get; set; }
            public string clientRefRound { get; set; }
        }
    }
}
