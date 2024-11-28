using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Util.Constants
{
    class ErrorMessages
    {
        // Borrower Page - Contacts
        public const string ERR_MISSING_EMAIL = "At least one Email contact is required.";
        public const string ERR_MISSING_MOBILE = "At least one Mobile contact is required.";

        // Borrower Page - Addresses
        public const string ERR_MISSING_TWOYR_RES_ADDRESS = "Minimum 2 years of Residential address history is mandatory for Individual.";
        public const string ERR_MISSING_TWOYR_ANY_ADDRESS = "Minimum 2 years address is required for any one Address Type.";
        public const string ERR_MISSING_CURRENT_RES_OR_BUS_ADDRESS = "One current Residential or Business address is required.";
        public const string ERR_MISSING_CURRENT_ADDRESS = "Please ensure one current address is added. Enter a current address by answering \"Yes\" to the \"Is Current?\" question or change the existing entry.";
        public const string ERR_WRONG_ADDRESS_DETAILS = "One current Residential or Business address is required.";
        public const string ERR_OVERLAP_DATES_RES_ADDRESS = "The dates entered for the Residential addresses overlap, please check the start and end dates.";
        public const string ERR_OVERLAP_DATES_BUS_ADDRESS = "The dates entered for the Business addresses overlap, please check the start and end dates.";
        public const string ERR_OVERLAP_DATES_MAIL_ADDRESS = "The dates entered for the Mailing addresses overlap, please check the start and end dates.";
        public const string ERR_OVERLAP_DATES_UNID_ADDRESS = "The dates entered for the undefined addresses overlap, please check the start and end dates.";

        // Borrower Page - Employment
        public const string ERR_MISSING_TWOYR_EMPLOYMENT = "Minimum 2 years of employment history is mandatory for Individual.";
        public const string ERR_MISSING_CURRENT_EMPLOYMENT = "One current employment is required.";
        public const string ERR_OVERLAP_DATES_EMPLOYMENT = "The dates entered for employments overlap, please check the start and end dates.";

        // Borrower Page - Date of Birth
        public const string ERR_MAX_AGE_100 = "Maximum age is 100";
        public const string ERR_MIN_AGE_18 = "Minimum age is 18";
    }
}
