using System;
namespace si_automated_tests.Source.Main.Models.ServiceStatus
{
    public class RoundGroupModel
    {
        public string service { get; set; }
        public string roundGroup { get; set; }
        public string round { get; set; }
        public string[] listlocatorRoundDate { get; set; }

        public RoundGroupModel(string service, string roundGroup, string round, string[] listlocatorRoundDate)
        {
            this.service = service;
            this.roundGroup = roundGroup;
            this.round = round;
            this.listlocatorRoundDate = listlocatorRoundDate;
        }
    }
}
