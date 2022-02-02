using System;

namespace OKRs.Web.Models.ObjectiveViewModels
{
    public class ObjectiveUserViewModel
    {

        public ObjectiveUserViewModel(Guid? id, string name, bool? inactive)
        {
            Id = id ?? Guid.Empty;
            Name = name ?? string.Empty;
            Inactive = inactive ?? true;
        }

        public bool Inactive { get; }
        public Guid Id { get; }
        public string Name { get; }
    }
}