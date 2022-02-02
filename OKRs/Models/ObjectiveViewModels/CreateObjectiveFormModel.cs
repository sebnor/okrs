using System;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class CreateObjectiveFormModel
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
    }
}
