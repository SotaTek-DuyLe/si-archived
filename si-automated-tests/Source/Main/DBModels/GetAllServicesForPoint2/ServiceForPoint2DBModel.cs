using System;
namespace si_automated_tests.Source.Main.DBModels.GetAllServicesForPoint2
{
    public class ServiceForPoint2DBModel
    {
        public string Contract { get; set; }
        public int ContractID { get; set; }
        public string ServiceGroup { get; set; }
        public int ServiceID { get; set; }
        public int ServiceUnitTypeID { get; set; }
        public string Service { get; set; }
        public string ServiceUnit { get; set; }
        public int ServiceUnitID { get; set; }
        public int ServiceUnitPointID { get; set; }
        public int SUPPointID { get; set; }
        public int SUPPointTypeID { get; set; }
        public int ActiveState { get; set; }
        public int STCount { get; set; }
        public int STSCount { get; set; }

        public ServiceForPoint2DBModel()
        {
        }
    }
}
