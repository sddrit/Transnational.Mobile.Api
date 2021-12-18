using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    public class MetaData
    {

        public List<FieldDefinition> FieldDefinitions { get; set; }

        public string? LatestAppVersion { get; set; }

    }


    [Table("FieldDefinition")]
    public class FieldDefinition
    {
        [Key]
        [Column("length")]
        public int Length { get; set; }

        [Column("code")]
        [MaxLength(50)]
        public string Code { get; set; }

    }
}
