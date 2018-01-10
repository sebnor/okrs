using System;
using System.Collections.Generic;

namespace OKRs.Models.ObjectiveViewModels
{
    public class ObjectiveViewModel
    {
        public List<KeyResult> KeyResults { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
