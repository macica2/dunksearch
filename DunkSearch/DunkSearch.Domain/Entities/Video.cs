using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DunkSearch.Domain.Entities
{
    [Table("video")]
    public class Video
    {
        [Key]
        [Column("video_id")]
        public int VideoId { get; set; }

        [Column("channel_id")]
        public int ChannelId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("yt_video_id")]
        public string YTVideoId { get; set; }

        [Column("upload_date")]
        public DateTime UploadDate { get; set; }

        public Channel Channel { get; set; }
    }
}
