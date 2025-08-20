using System.ComponentModel.DataAnnotations;

namespace OKRs.Models.ObjectiveViewModels
{
    public class SaveKeyResultFormModel
    {
        public string Description { get; set; }
        
        [Range(0.0, 1.0, ErrorMessage = "Completion rate must be between 0% and 100%")]
        public decimal CompletionRate { get; set; } = 0.0m;
    }
}