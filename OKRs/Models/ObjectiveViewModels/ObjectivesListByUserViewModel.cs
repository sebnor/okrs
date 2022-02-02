using System.Collections.Generic;

namespace OKRs.Web.Models.ObjectiveViewModels
{

    public class ObjectivesListByUserViewModel
    {
        public ObjectiveUserViewModel User { get; set; }
        public List<ObjectiveListItemViewModel> Objectives { get; set; }
    }
}
