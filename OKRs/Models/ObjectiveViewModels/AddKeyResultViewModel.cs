using System;

namespace OKRs.Models.ObjectiveViewModels
{
    public class AddKeyResultViewModel : SaveKeyResultFormModel
    {
        public string ObjectiveTitle { get; set; }
        public Guid ObjectiveId { get; set; }
    }
}