using OKRs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OKRs.Web.Repositories
{
    public interface IObjectivesRepository
    {
        Task CreateObjective(Objective objective);
        Task SaveObjective(Objective objective);
        Task<List<Objective>> GetAllObjectives();
        Task<Objective> GetObjectiveById(Guid id);
        Task<List<Objective>> GetObjectivesByUserId(Guid userId);
        Task<Objective> GetObjectiveByKeyResultId(Guid keyResultId);
        Task<Objective> DeleteObjective(Guid id);
    }
}
