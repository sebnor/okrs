namespace OKRs.Models.ObjectiveViewModels
{
    public class ObjectiveViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public List<KeyResultListItemViewModel> KeyResults { get; set; }
    }
}
