using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.DataTransfer.ClientInfoSetup
{
    [Table("tblSBUMaster",Schema = "Config")]
    public class Config_tblSBUMasterDto : BaseEntity
    {
        [Key]
        public int SBUID { get; set; }
        public int? ERPID { get; set; }
        public string? Name { get; set; }
        public string? description { get; set; }
        public bool? IsClientSBU { get; set; }
        public bool Disabled { get; set; }
        public int? BId { get; set; }
    }
}
