﻿using System;
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
        public static readonly string[] LinkEventPopupColumn = { "Set", "Service Unit", "Service", "Schedule", " Last", "Next", "Asset Type", "Allocation", "Assured Task", "Client Reference" };
        public static readonly string CreateEventEventTitle = "Create Event - Event";        public static readonly string AllocateEventEventTitle = "Allocate Event - Event";        public static readonly string AcceptEventTitle = "Accept - Event";
        public static readonly string UpdateEventTitle = "Update Event - Event";
        public static readonly string AddNoteEventTitle = "Add Note - Event";
        public static readonly string CancelEventTitle = "Cancel - Event";
        public static readonly string[] ColumnInFindServiceUnitWindow = { "ID", "Service Unit", "Point Count", "Select" };        public static readonly string[] ServiceUpdateColumnHistoryTab = { "ActualAssetQuantity", "ActualProductQuantity", "State", "Resolution Code", "Completed Date", "Auto Confirmed" };
        public static readonly string[] ServiceUpdateColumnNotCompletedHistoryTab = { "ActualAssetQuantity", "ActualProductQuantity", "State", "Completed Date", "Auto Confirmed" };        public static readonly string[] UpdateColumnHistoryTabFirst = { "Task notes", "Completed date", "State", "End date", "Resolution code" };
        public static readonly string[] UpdateColumnHistoryTabSecond = { "Task notes", "Completed date", "State", "End date" };
        public static readonly string[] ServiceUpdateTaskLineFromCancelledToNotCompleted = { "State", "Completed Date", "Auto Confirmed" };
        public static readonly string[] HistoryStreetColumn = { "ID", "Date", "Update Type", "User", "Object Type", "Object ID" };
        public static readonly string[] ActionEventBulkUpdate = { "Add Note" };
        public static readonly string[] ActionMenuSDM = { "Add Service Task Schedule", "Set Assured", "Set Proximity Alert", "Add/Amend Crew Notes", "Retire Service Task Schedule" };
        public static readonly string[] ActionMenuSU = { "Split Service Unit", "Remove Point" };
        public static readonly string FUTURE_END_DATE = "01/01/2050";
        public static readonly string[] HistoryTitleAfterUpdateWBTicketTab = { "Party", "Auto Print", "Driver Required", "Use Stored PO", "Use Manual PO", "External Round Required", "Use Stored Round", "Use Manual Round", "Allow Manual Name", "Authorise Tipping", "License Number", "License Number Expiry", "Dormant Date", "Restrict Products" };


        //ICON IMAGE
        public static readonly string StreetIconUrl = "web/content/images/street.svg";        
        //DATE
        public static readonly string DATE_DD_MM_YYYY_FORMAT = "dd/MM/yyyy";
        public static readonly string DATE_DD_MM_YYYY_FORMAT_DB = "dd-MM-yyyy";
        public static readonly string DATE_MM_DD_YYYY_FORMAT = "MM/dd/yyyy";
        public static readonly string DATE_DD_MM_YYYY_HH_MM_FORMAT = "dd/MM/yyyy HH:mm";
        public static readonly string DATE_DDD_D_MMM = "ddd d MMM";
        public static readonly string DATE_YYYY_MM_DD_FORMAT_DB = "yyyy-MM-dd";

    }
}
