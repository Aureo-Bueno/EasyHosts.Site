using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models
{
    public class Perfil
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
    }
}