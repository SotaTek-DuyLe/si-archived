using System;
namespace si_automated_tests.Source.Main.Models
{
    public class PointHistoryModel
    {
        public string description { get; set; }
        public string ID { get; set; }
        public string type { get; set; }
        public string service { get; set; }
        public string address { get; set; }
        public string date { get; set; }
        public string dueDate { get; set; }
        public string state { get; set; }
        public string resolution { get; set; }

        public PointHistoryModel()
        {
        }

        public PointHistoryModel(string desc, string iD, string type, string service, string address, string date, string dueDate, string state, string resolution)
        {
            this.description = desc;
            ID = iD;
            this.type = type;
            this.service = service;
            this.address = address;
            this.date = date;
            this.dueDate = dueDate;
            this.state = state;
            this.resolution = resolution;
        }
    }
}
