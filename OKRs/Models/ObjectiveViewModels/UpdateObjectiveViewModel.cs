using System;

namespace OKRs.Models.ObjectiveViewModels
{
    public class UpdateObjectiveViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
