using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models
{
    public class ServiceTaskModel
    {
        public string taskId { get; set; }
        public string partyName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public ServiceTaskModel(string id, string name, string start, string end)
        {
            this.taskId = id;
            this.partyName = name;
            this.startDate = start;
            this.endDate = end;
        }
    }
}
