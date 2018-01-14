using System.Collections.Generic;

namespace OKRs.Models.ObjectiveViewModels
{
    public class ObjectivesListByUserViewModel
    {
        public ObjectiveUserViewModel User { get; set; }
        public List<ObjectiveListItemViewModel> Objectives { get; set; }
    }
}
