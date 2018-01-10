using System;
using System.Collections.Generic;

namespace OKRs.Models.ObjectiveViewModels
{
    public class ObjectivesListViewModel
    {
        public List<ObjectiveListItemViewModel> Objectives { get; set; }
    }

    public class ObjectiveListItemViewModel
    {
        public string Title { get; set; }
        public Guid Id { get; internal set; }
    }
}
