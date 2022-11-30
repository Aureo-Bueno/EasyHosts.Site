using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Easy.Hosts.Site.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Confirmação de senha")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Status do Usuario")]
        public int Status { get; set; }

        [DisplayName("CPF do Usuário:")]
        public string Cpf { get; set; }

        public string Hash { get; set; }

        [DisplayName("Nome do perfil")]
        public int PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}