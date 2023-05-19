using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskLineDBModel
    {
        public int tasklineID { get; set; }
        public int taskID { get; set; }
        public int scheduledassetquantity { get; set; }
        public int productID { get; set; }
        public double scheduledproductquantity { get; set; }
        public int assettypeID { get; set; }
        public int assetID { get; set; }
        public string status { get; set; }
        public int tasklinetypeID { get; set; }
        public int product_unitID { get; set; }
        public int maxproductquantity { get; set; }
        public int minproductquantity { get; set; }
        public int maxassetquantity { get; set; }
        public int minassetquantity { get; set; }
        public int actualassetquantity { get; set; }
        public double actualproductquantity { get; set; }
        public int tasklinestateID { get; set; }
        public int resolutioncodeID { get; set; }
        public string tasklinestate { get; set; }

        public TaskLineDBModel()
        {
        }
    }
}
