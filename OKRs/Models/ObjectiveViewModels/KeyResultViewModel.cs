namespace OKRs.Models.ObjectiveViewModels
{
    public class KeyResultDetailsViewModel
    {
        public string ObjectiveTitle { get; set; }
        public string Description { get; set; }
        public decimal CompletionRate { get; set; }
        public Guid Id { get; set; }
        public Guid ObjectiveId { get; set; }
        public DateTime Created { get; set; }
    }
}