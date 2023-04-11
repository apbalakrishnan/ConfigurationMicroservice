using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.DataTransfer.ClientInfoSetup
{
    [Table("tblClientSBUMap", Schema = "Config")]
    public class Config_tblClientSBUMapDto : BaseEntity
    {
        [Key]
        public int ClientSBUId { get; set; }
        public int ClientId { get; set; }
        public int SBUId { get; set; }
        public bool Disabled { get; set; }
        public int? BId { get; set; }
    }
}
