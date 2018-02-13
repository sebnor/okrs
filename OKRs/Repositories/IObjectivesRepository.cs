using OKRs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OKRs.Repositories
{
    public interface IObjectivesRepository
    {
        Task CreateObjective(Objective objective);
        Task SaveObjective(Objective objective);
        Task<List<Objective>> GetAllObjectives();
        Task<Objective> GetObjectiveById(Guid id);
        Task<List<Objective>> GetObjectivesByUserId(Guid userId);
    }
}
