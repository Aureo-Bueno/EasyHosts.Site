using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models.ViewModel
{
    public class UserAccessViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Senha:")]
        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número.Deve ser no mínimo 6 caracteres")]
        public string Password { get; set; }
    }
}