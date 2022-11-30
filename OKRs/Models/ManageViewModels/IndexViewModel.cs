using System.ComponentModel.DataAnnotations;

namespace OKRs.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public string StatusMessage { get; set; }
    }
}
