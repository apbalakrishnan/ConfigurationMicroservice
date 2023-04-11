using System.ComponentModel.DataAnnotations;

namespace Configuration.DomainModel.ClientInfoSetup.Request
{
    public class GetBEClientInfoRequest
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "NotEmpty")]
        public string ClientName { get; set; }
    }
}
