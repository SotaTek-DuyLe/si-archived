using System;
namespace si_automated_tests.Source.Main.Models
{
    public class VehicleModel
    {
        private string id;
        private string resource;
        private string customer;
        private string haulier;
        private string hireStart;
        private string hireEnd;
        private string suspendedDate;
        private string suspendedReason;

        public VehicleModel(string id, string resource, string customer, string haulier, string hireStart, string hireEnd, string suspendedDate, string suspendedReason)
        {
            this.id = id;
            this.resource = resource;
            this.customer = customer;
            this.haulier = haulier;
            this.hireStart = hireStart;
            this.hireEnd = hireEnd;
            this.suspendedDate = suspendedDate;
            this.suspendedReason = suspendedReason;
        }

        public string Id { get => id; set => id = value; }
        public string Resource { get => resource; set => resource = value; }
        public string Customer { get => customer; set => customer = value; }
        public string Haulier { get => haulier; set => haulier = value; }
        public string HireStart { get => hireStart; set => hireStart = value; }
        public string HireEnd { get => hireEnd; set => hireEnd = value; }
        public string SuspendedDate { get => suspendedDate; set => suspendedDate = value; }
        public string SuspendedReason { get => suspendedReason; set => suspendedReason = value; }
    }
}
