using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DunkSearch.Domain.Entities
{
    [Table("unsupported_video")]
    public class UnsupportedVideo
    {
        [Key]
        [Column("unsupported_video_id")]
        public int UnsupportedVideoId { get; set; }

        [Column("channel_id")]
        public int ChannelId { get; set; }

        [Column("yt_video_id")]
        public string YTVideoId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("reason")]
        public string Reason { get; set; }

        public virtual Channel Channel { get; set; }
    }
}
