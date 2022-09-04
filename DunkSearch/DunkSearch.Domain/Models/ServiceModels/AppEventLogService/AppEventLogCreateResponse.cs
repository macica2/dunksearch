using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DunkSearch.Domain.Models.ServiceModels.AppEventLogService
{
    public class AppEventLogCreateResponse
    {
        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
