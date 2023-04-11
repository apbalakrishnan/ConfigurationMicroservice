namespace Configuration.DomainModel.ClientInfoSetup
{
    public class GetBESBUInfoByClientIdResponseModel
    {
        public int ClientSBUId { get; set; }
        public int SBUId { get; set; }
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public string? description { get; set; }
        public bool? IsClientSBU { get; set; }
        public bool Disabled { get; set; }
        public int? CreatedBy { get; set; }
    }
}
