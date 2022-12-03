using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.Hosts.Site.Models.ViewModel
{
    public class SiteViewModel
    {
        private Context db = new Context();
        public Booking Booking { get; set; }

        public IEnumerable<Event> Event { get; set; }

        public SiteViewModel()
        {
            Event = db.Event.ToList();
        }
    }
}