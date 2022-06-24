using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DunkSearch.Domain.Entities
{
    [Table("caption_type")]
    public class CaptionType
    {
        [Key]
        [Column("caption_type_id")]
        public int CaptionTypeId { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
