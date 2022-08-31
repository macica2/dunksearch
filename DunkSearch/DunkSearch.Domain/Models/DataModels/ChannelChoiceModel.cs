using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DunkSearch.Domain.Models.DataModels
{
    public class ChannelChoiceModel
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public bool Selected { get; set; }
    }
}
