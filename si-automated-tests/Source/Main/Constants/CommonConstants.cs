using System;
namespace si_automated_tests.Source.Main.Constants
{
    public class CommonConstants
    {
        public static readonly string[] InvoiceScheduleAssetAndProduct = {"Use Agreement", "Daily in Arrears", "Monthly in Arrears"};
        public static readonly string[] InvoiceAddressAssetAndProduct = { "Use Agreement", "15 HILL STREET, RICHMOND, TW9 1SX" };
        public static readonly string[] InvoiceScheduleMobilization = { "Use Agreement Line", "Daily in Arrears", "Monthly in Arrears" };
        public static readonly string[] InvoiceAddressMobilization = { "Use Agreement Line", "15 HILL STREET, RICHMOND, TW9 1SX" };
        public static readonly string[] TaskLineType = { "Service" };
        public static readonly string[] AssetType = { "1100L" };
        public static readonly string[] ProductName = { "General Recycling" };
        public static readonly string[] Unit = { "Kilograms" };
        public static readonly string StartDateAgreement = "03/03/2022";
        public static readonly string EndDateAgreement = "01/01/2050";
        public static readonly string BoderColorMandatory = "rgb(169, 68, 66)";
        public static readonly string BoderColorMandatory_Rbga = "rgba(169, 68, 66, 1)";
        public static readonly string ColorBlue = "rgba(51, 122, 183, 1)"; //#337AB7
        public static readonly string[] ContactGroupsOptions = { "Invoicing", "Operational" };
        public static readonly string[] ContactTable = { "ID", "Title", "First Name", "Last Name", "Position", "Telephone", "Mobile", "Email", "Receive Emails", "Contact Groups", "Start Date", "End Date" };
        public static readonly string WBSiteName = "Weighbridge";
        public static readonly string[] AllSiteTabCase46 = { "Details", "Data", "Assets", "Map", "Indicators", "Stations", "Locations", "Products", "Weighbridge", "Notes", "Time Restrictions", "Calendar" };
        public static readonly string[] AllSiteTabCase47 = { "Details", "Data", "Assets", "Map", "Indicators", "Notes", "Time Restrictions", "Calendar" };
        public static readonly string[] SomeSiteTabNoMessage = { "Details", "Data", "Assets", "Indicators", "Stations", "Locations", "Products", "Weighbridge", "Notes", "Time Restrictions", "Calendar" };
        public static readonly string MapTab = "Map";
        public static readonly string[] ColumnInVehicleCustomerHaulierPage = { "ID", "Resource", "Customer", "Haulier", "Hire Start", "Hire End", "Suspended Date", "Suspended Reason" };
        public static readonly string BoderColorResourceInputWB = "rgb(160, 65, 63)";
        public static readonly string BoderColorHaulierInputWB = "rgb(156, 63, 61)";
        public static readonly string[] LocationTabColumn = { "ID", "Location", "Active", "Client #" };
        public static readonly string[] ProductTabColumn = { "ID", "Product", "Ticket Type", "Default Location", "Is Location Mandatory", "Is Restrict Location", "Start Date", "End Date" };
        public static readonly string[] TicketType = { "Select...", "Incoming", "Neutral", "Outbound" };
        public static readonly string[] InspectionTabColumn = { "ID", "Inspection Type", "Created Date", "Created By User", "Assigned User", "Allocated Unit", "Status", "Valid From", "Valid To", "Completion Date", "Cancelled Date" };
        public static readonly string[] InspectionColumnInListingPage = { "ID", "Point", "Inspection Type", "Created Date", "Created By User", "Inspection Scheme", "Assigned User", "Allocated Unit", "Status", "Client Reference", "Contract", "Valid From", "Valid To", "Completion Date", "Last Updated", "Cancelled Date", "GPS Description", "Source", "Service" };
        public static readonly string[] PointHistoryTabColumn = { "Description", "ID", "Type", "Service", "Address", "Date", "Due Date", "State", "Resolution Code" };
        public static readonly string[] PointAddressColumn = { "ID", "Name", "UPRN", "Postcode", "Property Name", "Property", "To Property", "Sub Building", "Property Suffix", "Point Segment ID", "Point Address Type", "Start Date", "End Date" };
        public static readonly string[] PointSegmentColumn = { "ID", "Name", "Point Segment Type", "Point Segment Class", "Start Date", "End Date" };

        //DATE
        public static readonly string DATE_DD_MM_YYYY_FORMAT = "dd/MM/yyyy";
        public static readonly string DATE_MM_DD_YYYY_FORMAT = "MM/dd/yyyy";
        public static readonly string DATE_DD_MM_YYYY_HH_MM_FORMAT = "dd/MM/yyyy HH:mm";

    }
}
