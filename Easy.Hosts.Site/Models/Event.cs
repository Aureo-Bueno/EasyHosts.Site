using Easy.Hosts.Site.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Nome do evento muito extenso!")]
        public string NameEvent { get; set; }

        [MaxLength(100, ErrorMessage = "Nome do organizador muito extenso!")]
        public string Organizer { get; set; }

        [DisplayName("Data")]
        public DateTime DateEvent { get; set; }

        [MaxLength(150, ErrorMessage = "Local muito extenso!")]
        [DisplayName("Localização")]
        public string EventsPlace { get; set; }

        [MaxLength(100)]
        [DisplayName("Foto")]
        public string Picture { get; set; }
        public ICollection<AlbumEvent> AlbumEvent { get; set; }

        [MaxLength(100, ErrorMessage = "Descricao muito extensa!")]
        [DisplayName("Descrição")]
        public string DescriptionEvent { get; set; }

        [MaxLength(100, ErrorMessage = "Atracao muito extensa!")]
        [DisplayName("Atrações")]
        public string Attractions { get; set; }

        [DisplayName("Tipo")]
        public EventType TypeEvent { get; set; }
    }
}