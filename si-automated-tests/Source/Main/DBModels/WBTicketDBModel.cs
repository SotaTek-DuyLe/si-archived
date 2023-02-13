using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class WBTicketDBModel
    {
        public string driver { get; set; }
        public int ticketID { get; set; }
        public string destination_party { get; set; }
        public string source_party { get; set; }

        public WBTicketDBModel()
        {
        }
    }
}
