using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models
{
    public class TaskModel
    {
        private string description;
        private string service;
        private string taskReference;
        private string line;
        private string type;
        private string accountCode;
        private string party;
        private string site;
        private string street;
        private string town;
        private string postcode;
        private string startDate;
        private string endDate;
        private string schedule;
        private string scheduleRequirement;

        public string Description { get => description; set => description = value; }
        public string Service { get => service; set => service = value; }
        public string TaskReference { get => taskReference; set => taskReference = value; }
        public string Line { get => line; set => line = value; }
        public string Type { get => type; set => type = value; }
        public string AccountCode { get => accountCode; set => accountCode = value; }
        public string Party { get => party; set => party = value; }
        public string Site { get => site; set => site = value; }
        public string Street { get => street; set => street = value; }
        public string Town { get => town; set => town = value; }
        public string Postcode { get => postcode; set => postcode = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public string Schedule { get => schedule; set => schedule = value; }
        public string ScheduleRequirement { get => scheduleRequirement; set => scheduleRequirement = value; }
    }
}
