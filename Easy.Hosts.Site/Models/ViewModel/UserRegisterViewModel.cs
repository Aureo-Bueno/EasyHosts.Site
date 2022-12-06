using Easy.Hosts.Site.App_Start;
using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models.ViewModel
{
    public class UserRegisterViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número.Deve ser no mínimo 6 caracteres")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirma Senha")]
        public string ConfirmPassword { get; set; }

        [Required]
        [CustomValidationCPF(ErrorMessage = "Por favor, digite um CPF válido!")]
        public string Cpf { get; set; }
    }
}