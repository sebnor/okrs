using System.ComponentModel.DataAnnotations;

namespace OKRs.Web.Models.UserViewModels
{
    public class CreateUserFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}