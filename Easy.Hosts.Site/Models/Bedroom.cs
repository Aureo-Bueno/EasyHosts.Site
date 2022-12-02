using Easy.Hosts.Site.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models
{
    public class Bedroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Nome")]
        public string NameBedroom { get; set; }

        [Required]
        [DisplayName("Valor")]
        public decimal Value { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Foto")]
        public byte[] Picture { get; set; }

        [Required]
        [DisplayName("Status do Quarto")]
        public BedroomStatus Status { get; set; }
        public int TypeBedroomId { get; set; }
        public virtual TypeBedroom TypeBedroom { get; set; }
    }
}