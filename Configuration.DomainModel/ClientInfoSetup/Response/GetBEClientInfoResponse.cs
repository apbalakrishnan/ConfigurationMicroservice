namespace Configuration.DomainModel.ClientInfoSetup.Response
{
    public class GetBEClientInfoResponse //: DomainBaseEntity
    {
        public int ClientID { get; set; }
        public string? ClientName { get; set; }
        public string? Description { get; set; }      
        public DateTime? EndDate { get; set; }       
        public bool Disabled { get; set; }
        public int CreatedBy { get; set; }
    }
}
