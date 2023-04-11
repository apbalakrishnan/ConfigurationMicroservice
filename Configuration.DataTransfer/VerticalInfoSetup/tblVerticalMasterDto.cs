using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.DataTransfer.VerticalInfoSetup
{
    [Table("tblVerticalMaster", Schema = "Config")]
    public class Config_tblVerticalMaster : BaseEntity
    {
        [Key]
        public int VerticalID { get; set; }
        public int? ERPID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Disabled { get; set; }
        public int? BId { get; set; }
    }
}
