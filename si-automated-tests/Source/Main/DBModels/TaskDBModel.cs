using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskDBModel
    {
        public int taskId { get; set; }
        public int tasktypeID { get; set; }
        public string task { get; set; }
        public string tasknotes { get; set; }
        public bool proximityalert { get; set; }
        public DateTime taskcompleteddate { get; set; }
        public DateTime taskcreateddate { get; set; }
        public DateTime taskduedate { get; set; }
        public DateTime taskenddate { get; set; }
        public int PartyId { get => partyId; set => partyId = value; }
        public int AgreementId { get => agreementId; set => agreementId = value; }
        public int AgreementlinetasktypeId { get => agreementlinetasktypeId; set => agreementlinetasktypeId = value; }
        public int ServiceTaskId { get => serviceTaskId; set => serviceTaskId = value; }

        private int partyId;
        private int agreementId;
        private int agreementlinetasktypeId;
        private int serviceTaskId;

        public TaskDBModel()
        {
        }
    }
}
