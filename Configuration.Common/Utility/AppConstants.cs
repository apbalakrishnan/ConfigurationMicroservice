using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Common.Utility
{
    public static class AppConstants
    {
        public const string ApiVersion = "api/v1/";

        public const string SuccessCode = "0000";
        public const string ValidationCode = "5000";
        public const string RuntimeErrorCode = "6000";
        public const string DatabaseErrorCode = "7000";
        public const string ValidationDeveloperMessage = "See validationErrors in details";

        public enum SuccessMessage
        {
            Inserted,
            Updated,
            Deleted,
            List
        }

        public enum Status
        {
            Success,
            Error
        }

        public enum IsActive
        {
            Yes = 0,
            No = 1
        }

        public enum ExceptionType
        {
            UnexpectedError,
            DatabaseException,
            NotFound,
            NotImplemented,
            Unauthorized
        }
    }
}
