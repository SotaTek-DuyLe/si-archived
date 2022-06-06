using System;
namespace si_automated_tests.Source.Main.Constants
{
    public class MessageRequiredFieldConstants
    {
        public static readonly string ProductRequiredMessage = "Product is required";
        public static readonly string TicketTypeRequiredMessage = "Ticket Type is required";
        public static readonly string NameRequiredMessage = "Name is required";
        public static readonly string LocationRequiredMessage = "Location is required";
        public static readonly string CorrespondenceAddressRequiredMessage = "Correspondence Address is required";
        public static readonly string FieldStreetGradeRequiredMessage = "Field Street Grade is required";

        //WARNING MESSAGES
        public static readonly string LocationRestrictWarningMessage = "Default location must be selected as a restricted location.";
        public static readonly string ContactDetailsWarningMessage =  "Please enter at least one of the contact details.";
        public static readonly string WBMapTabWarningMessage = "No Service Unit(s) associated to this Site ";
        public static readonly string NoEventsAvailableWarningMessage = "No Events Available";

    }
}
