using System;

namespace OKRs.Models.ObjectiveViewModels
{
    public class EditKeyResultViewModel : SaveKeyResultFormModel
    {
        public string ObjectiveTitle { get; set; }
        public Guid ObjectiveId { get; set; }
        public Guid Id { get; set; }
    }
}