using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.Hosts.Site.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {

        }
    }
}