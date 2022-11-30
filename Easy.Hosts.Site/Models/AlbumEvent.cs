using Easy.Hosts.Site.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models
{
    public class AlbumEvent
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        [MaxLength(100)]
        [DisplayName("Fotos")]
        public string Picture { get; set; }
    }
}