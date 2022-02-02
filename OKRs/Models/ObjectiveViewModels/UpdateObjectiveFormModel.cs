using System.ComponentModel.DataAnnotations;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class UpdateObjectiveFormModel
    {
        [Required]
        public string Title { get; set; }
    }
}
