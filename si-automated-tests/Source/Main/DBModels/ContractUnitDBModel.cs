using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class ContractUnitDBModel
    {
        public int contractunitID { get; set; }
        public int contractID { get; set; }
        public string contractunit { get; set; }
        public DateTime enddate { get; set; }
    }
}
