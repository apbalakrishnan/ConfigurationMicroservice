namespace Configuration.DataTransfer
{
    public static class StoredProcedures
    {
        public const string SQL_MANAGE_CLIENT = $"EXEC Config.Usp_ManageClient {Parameters.PARAM_CLIENTNAME},{Parameters.PARAM_VERTICALID},{Parameters.PARAM_CLIENTDESC},{Parameters.PARAM_ENDDATE},{Parameters.PARAM_EXLSPECIFICCLIENT},{Parameters.PARAM_DISABLED},{Parameters.PARAM_CREATEDBY},{Parameters.PARAM_ACTION},{Parameters.PARAM_CLIENTID}";
        public const string SP_GETMAX_CLIENTID = @"EXEC Usp_CDS_GetMaxClient_ID @ReturnValue OUT";
        public const string SQL_SP_CLIENT = $"EXEC Usp_CDS_GetClientList {Parameters.PARAM_CLIENTNAME},{Parameters.PARAM_USERID},{Parameters.PARAM_ACTIVECLIENTLIST}";
    }

    public static class Parameters
    {
        public const string PARAM_CLIENTID = "@ClientID";
        public const string PARAM_CLIENTNAME = "@ClientName";
        public const string PARAM_CLIENTDESC = "@Description";
        public const string PARAM_ENDDATE = "@EndDate";
        public const string PARAM_DISABLED = "@Disabled";
        public const string PARAM_CREATEDBY = "@CreatedBy";
        public const string PARAM_VERTICALID = "@VerticalID";
        public const string PARAM_EXLSPECIFICCLIENT = "@EXLSpecificClient";
        public const string PARAM_ACTION = "@Action";
        public const string PARAM_USERID = "@UserID";
        public const string PARAM_ACTIVECLIENTLIST = "@ActiveClientList";
    }
}
