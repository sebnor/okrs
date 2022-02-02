using System;
using System.Collections.Generic;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class ObjectiveListItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<KeyResultListItemViewModel> KeyResults { get; set; } = new List<KeyResultListItemViewModel>();
    }
}
