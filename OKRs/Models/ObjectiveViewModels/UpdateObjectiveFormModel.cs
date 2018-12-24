using System.ComponentModel.DataAnnotations;

namespace OKRs.Models.ObjectiveViewModels
{
    public class UpdateObjectiveFormModel
    {
        [Required]
        public string Title { get; set; }
    }
}
