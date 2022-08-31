using NpgsqlTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DunkSearch.Domain.Entities
{
    [Table("caption")]
    public class Caption
    {
        [Key]
        [Column("caption_id")]
        public int CaptionId { get; set; }

        [ForeignKey("video")]
        [Column("video_id")]
        public int VideoId { get; set; }

        [Column("caption_type_id")]
        public int CaptionTypeId { get; set; }

        [Column("start_seconds")]
        public int StartSeconds { get; set; }

        [Column("caption_text")]
        public string CaptionText { get; set; }

        [Column("caption_text_vector")]
        public NpgsqlTsVector CaptionTextVector { get; set; }

        [Column("caption_text_simple_vector")]
        public NpgsqlTsVector CaptionTextSimpleVector { get; set; }

        public virtual Video Video { get; set; }
        public virtual CaptionType CaptionType { get; set; }

        public string Timestamp
        {
            get
            {
                var timeSpan = TimeSpan.FromSeconds(this.StartSeconds);
                if (timeSpan.Hours > 0)
                {
                    return timeSpan.ToString(@"hh\:mm\:ss");
                }

                return timeSpan.ToString(@"m\:ss");
            }
        }
    }
}
