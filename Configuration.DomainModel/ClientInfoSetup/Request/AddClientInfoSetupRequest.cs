using System.ComponentModel.DataAnnotations;

namespace Configuration.DomainModel.ClientInfoSetup
{
    public class AddClientInfoSetupRequest
    {
        public AddClientInfoSetupRequest()
        {
            ClientInfoSetupModel = new ClientInfoSetupModel();
        }
        public ClientInfoSetupModel ClientInfoSetupModel { get; set; }
    }
}

