using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Easy.Hosts.Site.Models
{
    public class AlbumBedroom
    {
        [Key]
        public int Id { get; set; }
        public int BedroomId { get; set; }
        public virtual Bedroom Bedroom { get; set; }
        [MaxLength(100)]
        [DisplayName("Fotos")]
        public string Picture { get; set; }
    }
}