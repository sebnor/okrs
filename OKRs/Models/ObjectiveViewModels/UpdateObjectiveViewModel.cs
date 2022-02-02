using System;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class UpdateObjectiveViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
