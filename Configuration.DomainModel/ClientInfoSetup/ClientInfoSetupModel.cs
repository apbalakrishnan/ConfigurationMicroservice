using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.DomainModel.ClientInfoSetup
{
    public class ClientInfoSetupModel : DomainBaseEntity
    {
        public ClientInfoSetupModel()
        {
            SbuClientIds = new List<int>();
        }

        [Required(ErrorMessage = "NotEmpty")]
        public string ClientName { get; set; }
        public int VerticalID { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public bool EXLSpecificClient { get; set; }
        public bool Disabled { get; set; }
        public int ClientId { get; set; }
        public List<int> SbuClientIds { get; set; }
    }
}
