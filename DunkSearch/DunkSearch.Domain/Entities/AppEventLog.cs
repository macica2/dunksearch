using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace DunkSearch.Domain.Entities
{
    [Table("app_event_log")]
    public class AppEventLog
    {
        [Key]
        [Column("app_event_log_id")]
        public int AppEventLogId { get; set; }

        [Column("event_type")]
        public string EventType { get; set; }

        [Column("event_details")]
        public string EventDetails { get; set; }

        [Column("ip_address")]
        public IPAddress IPAddress { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }
    }
}
