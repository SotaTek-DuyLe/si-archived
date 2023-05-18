using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class AgreementLineAssetProductDBModel
    {
        public int agreementLineId { get; set; }
        public int agreementlineassetproductID { get; set; }
        public int agreementlineID { get; set; }
        public int productcodeID { get; set; }
        public AgreementLineAssetProductDBModel()
        {
        }
    }
}
