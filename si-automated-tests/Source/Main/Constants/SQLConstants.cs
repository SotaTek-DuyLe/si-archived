using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Constants
{
    public class SQLConstants
    {
        public const string SQL_ServiceTask = "select u.unit, a.assettype, p.product,s.scheduledassetquantity, CAST(s.scheduledproductquantity as int) as scheduledproductquantity, s.startdate, s.enddate from servicetasklines s join units u on s.product_unitId = u.unitID join assettypes a on s.assettypeID = a.assettypeID join products p on s.productID = p.productID where servicetaskID = ";
        public const string SQL_ServiceUnitAssets = "SELECT a.assettype, p.product, s.* FROM serviceunitassets s JOIN assettypes a ON s.assettypeID = a.assettypeID JOIN products p ON s.productID = p.productID WHERE s.agreementid = ";
        public const string SQL_ServiceTaskForAgreement = "select * from servicetasks where agreementid = ";
    }
}
