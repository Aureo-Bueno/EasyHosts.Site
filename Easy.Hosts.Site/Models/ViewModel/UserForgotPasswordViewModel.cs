using System.ComponentModel.DataAnnotations;

namespace Easy.Hosts.Site.Models.ViewModel
{
    public class UserForgotPasswordViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}