using System;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class AddKeyResultViewModel : SaveKeyResultFormModel
    {
        public string ObjectiveTitle { get; set; }
        public Guid ObjectiveId { get; set; }
    }
}