using System.ComponentModel.DataAnnotations;

namespace Configuration.DomainModel.ClientInfoSetup
{
    public class UpdateClientInfoSetupRequest
    {
        public UpdateClientInfoSetupRequest()
        {
            ClientInfoSetupModel = new ClientInfoSetupModel();
        }
        public ClientInfoSetupModel ClientInfoSetupModel { get; set; }
    }
}

