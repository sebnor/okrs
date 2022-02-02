using System;
using System.Collections.Generic;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class ObjectivesListViewModel
    {
        public Guid UserId { get; set; }
        public bool IsObjectivesForCurrentUser { get; set; } = true;
        public List<ObjectiveListItemViewModel> Objectives { get; set; }
    }
}
