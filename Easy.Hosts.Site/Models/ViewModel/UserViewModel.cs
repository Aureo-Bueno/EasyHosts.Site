using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Easy.Hosts.Site.Models.ViewModel
{
    public class Register
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirma Senha")]
        public string ConfirmPassword { get; set; }

        public string Cpf { get; set; }
    }

    public class Access
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

    public class Mensagem
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Assunto { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Mensagem")]
        public string CorpoMsg { get; set; }
    }

    public class EsqueceuSenha
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }

    public class RedefinirSenha
    {
        public string Email { get; set; }
        public string Hash { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[AZ]).{6,12})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número.Deve ser no mínimo 6 caracteres")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Senha")]
        [Display(Name = "Confirma Senha")]
        public string ConfirmPassword { get; set; }
    }

    public class AtivarConta
    {
        [Required]
        public string Email { get; set; }
        public string Hash { get; set; }

        [Required]
        public string Password { get; set; }
    }

}