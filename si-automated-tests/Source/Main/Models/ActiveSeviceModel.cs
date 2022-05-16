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
        public string status { get; set; }
        public string lastService { get; set; }
        public string nextService { get; set; }
        public string nextReScheduled { get; set; }
        public string assetTypeService { get; set; }
        public string allocationService { get; set; }
        public string timeBandService { get; set; }
        public string assuredTaskService { get; set; }
        public string clientRefService { get; set; }
        public List<ChildSchedule> listChildSchedule;

        public ActiveSeviceModel(string eventLocator, string serviceUnitValue, string serviceValue)
        {
            this.eventLocator = eventLocator;
            this.serviceUnit = serviceUnitValue;
            this.service = serviceValue;
        }

        public ActiveSeviceModel(string eventLocator, string serviceUnitValue, string serviceValue, string statusDescParentValue, string scheduleParentValue, string lastParentValue, string nextParentValue, string assetTypeParentValue, List<ChildSchedule> listSchedule) : this(eventLocator, serviceUnitValue, serviceValue)
        {
            this.status = statusDescParentValue;
            this.schedule = scheduleParentValue;
            this.lastService = lastParentValue;
            this.nextService = nextParentValue;
            this.assetTypeService = assetTypeParentValue;
            this.listChildSchedule = listSchedule;
        }

        public ActiveSeviceModel(string serviceUnitValue, string serviceValue, string scheduleValue, string lastValue, string nextValue, string assetTypeValue, string allocationValue)
        {
            this.serviceUnit = serviceUnitValue;
            this.service = serviceValue;
            this.schedule = scheduleValue;
            this.lastService = lastValue;
            this.nextService = nextValue;
            this.assetTypeService = assetTypeValue;
            this.allocationService = allocationValue;
        }

        public ActiveSeviceModel(string serviceUnitValue, string serviceValue, string scheduleValue, string lastValue, string nextValue, string nextReScheduled, string assetTypeValue, string allocationValue)
        {
            this.serviceUnit = serviceUnitValue;
            this.service = serviceValue;
            this.schedule = scheduleValue;
            this.lastService = lastValue;
            this.nextService = nextValue;
            this.nextReScheduled = nextReScheduled;
            this.assetTypeService = assetTypeValue;
            this.allocationService = allocationValue;
        }

        public ActiveSeviceModel(string serviceUnitValue, string serviceValue)
        {
            this.service = serviceValue;
            this.serviceUnit = serviceUnitValue;
        }

        public class ChildSchedule
        {
            public ChildSchedule(string roundChildValue, string lastChildValue, string nextChildValue, string assertTypeChildValue, string allocationChildValue)
            {
                this.round = roundChildValue;
                this.lastRound = lastChildValue;
                this.nextRound = nextChildValue;
                this.assetTypeRound = assertTypeChildValue;
                this.allocationRound = allocationChildValue;
            }

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
