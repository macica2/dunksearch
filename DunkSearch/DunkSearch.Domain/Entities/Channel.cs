using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DunkSearch.Domain.Entities
{
    [Table("channel")]
    public class Channel
    {
        [Key]
        [Column("channel_id")]
        public int ChannelId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("yt_channel_id")]
        public string YTChannelId { get; set; }

        [Column("uploads_playlist_id")]
        public string UploadsPlaylistId { get; set; }
    }
}
