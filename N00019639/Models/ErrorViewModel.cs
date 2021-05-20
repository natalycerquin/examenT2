using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
