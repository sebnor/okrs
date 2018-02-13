using System;
using System.ComponentModel.DataAnnotations;

namespace OKRs.Models.UserViewModels
{
    public class SaveUserFormModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}