using System;
using System.Collections.Generic;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class ObjectiveViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public List<KeyResultListItemViewModel> KeyResults { get; set; }
    }
}
