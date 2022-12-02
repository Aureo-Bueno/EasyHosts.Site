using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.Hosts.Site.Models.ViewModel
{
    public class SiteViewModel
    {
        private Context db = new Context();
        public IEnumerable<Bedroom> Bedroom { get; set; }
        public Booking Booking { get; set; }

        public SiteViewModel()
        {
            Bedroom = db.Bedroom.ToList();
        }
    }
}