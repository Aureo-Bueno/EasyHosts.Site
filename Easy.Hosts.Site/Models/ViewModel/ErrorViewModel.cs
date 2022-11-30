using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.Hosts.Site.Models.ViewModel
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}