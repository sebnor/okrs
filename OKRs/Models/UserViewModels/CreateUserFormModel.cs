using System.ComponentModel.DataAnnotations;

namespace OKRs.Models.UserViewModels
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