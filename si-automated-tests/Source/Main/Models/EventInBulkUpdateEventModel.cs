using System;
namespace si_automated_tests.Source.Main.Models
{
    public class EventInBulkUpdateEventModel
    {
        public string eventId { get; set; }
        public string description { get; set; }
        public string eventDate { get; set; }
        public string service { get; set; }
        public string address { get; set; }

        public EventInBulkUpdateEventModel()
        {
        }

        public EventInBulkUpdateEventModel(string eventId, string description, string eventDate, string service, string address)
        {
            this.eventId = eventId;
            this.description = description;
            this.eventDate = eventDate;
            this.service = service;
            this.address = address;
        }
    }
}
