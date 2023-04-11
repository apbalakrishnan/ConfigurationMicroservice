using System.Data;

namespace Configuration.Common.Utility
{
    public static class StringExtension
    {
        public static string DecodeHtmlString(this string strEncodeString)
        {
            string RValues = "";
            if (!string.IsNullOrEmpty(strEncodeString))
            {
                RValues = System.Web.HttpUtility.HtmlDecode(strEncodeString);
            }
            return RValues;
        }

        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

    }
}
