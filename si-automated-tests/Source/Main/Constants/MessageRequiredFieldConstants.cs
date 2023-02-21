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
        public static readonly string ResourceRequiredMessage = "Resource is required";
        public static readonly string MissingTimezoneMessage = "Time Zone is required";
        public static readonly string StreetNameRequiredMessage = "Street Name is required";
        public static readonly string GreylistCodeIsRequiredMessage = "Greylist Code is required";
        public static readonly string LicenceNumberExIsRequiredMessage = "Licence Number Expiry is required";
        public static readonly string LicenceNumberIsRequiredMessage = "Licence Number is required";
        public static readonly string DriverNameIsRequiredMessage = "Driver Name is required";
        public static readonly string PONumberIsRequiredMessage = "PO Number is required";

        //WARNING MESSAGES
        public static readonly string LocationRestrictWarningMessage = "Default location must be selected as a restricted location.";
        public static readonly string ContactDetailsWarningMessage =  "Please enter at least one of the contact details.";
        public static readonly string WBMapTabWarningMessage = "No Service Unit(s) associated to this Site ";
        public static readonly string NoEventsAvailableWarningMessage = "No Events Available";
        public static readonly string TooManyTaskItemsSelectedDeletedMessage = "Too many items selected. Please select exactly 1 item.";
        public static readonly string YouCannotCreateServiceTaskMessage = "You cannot create service tasks under own schedule header!";
        public static readonly string WhoopsSomethingIsWrongAttributesConfig = "Whoops something is wrong in the Attributes config";
        public static readonly string TheAccountingRefAlreadyUsed = "The Accounting Reference already used";
        public static readonly string HaulierLicenceNumberHasExpiredMessage = "Haulier Licence number has expired";
        public static readonly string CustomterBalanceExceededMessage = "Customer {0} 's balance of {1} exceeded warnining limit of {2}";
        public static readonly string CustomerAuthoriseTippingMessage = "Customer {0} is not authorised to tip";
        public static readonly string CustomerIsOnStopMessage = "Customer {0} is on stop since {1}";
    }
}
