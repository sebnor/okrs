using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OKRs.Core.Domain;
using OKRs.Web.Models;
using OKRs.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace OKRs.Web.Repositories
{
    public class ObjectivesRepository : IObjectivesRepository
    {
        private readonly ObjectivesDbContext _context;

        public ObjectivesRepository(ObjectivesDbContext context)
        {
            _context = context;
        }
        public async Task CreateObjective(Objective objective)
        {
            _context.Add(objective);
            await _context.SaveChangesAsync();
        }

        public async Task SaveObjective(Objective objective)
        {
            _context.Update(objective);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Objective>> GetAllObjectives()
        {
            return await _context
                    .Objectives
                    .Include(o => o.KeyResults)
                    .AsNoTracking()
                    .OrderBy(o => o.Title)
                    .ToListAsync();
        }

        public async Task<Objective> GetObjectiveById(Guid id)
        {
            return await _context.Objectives
                        .Include(o => o.KeyResults)
                        .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Objective>> GetObjectivesByUserId(Guid userId)
        {
            return await _context.Objectives
                   .Include(o => o.KeyResults)
                   .AsNoTracking()
                   .Where(o => o.UserId == userId)
                   .ToListAsync();
        }

        public async Task<Objective> GetObjectiveByKeyResultId(Guid keyResultId)
        {
            return await _context.Objectives
                        .Include(o => o.KeyResults)
                        .Where(o => o.KeyResults.Any(k => k.Id == keyResultId))
                        .SingleOrDefaultAsync();
        }

        public async Task<Objective> DeleteObjective(Guid id)
        {
            var objective = await GetObjectiveById(id);
            _context.Objectives.Remove(objective);
            await _context.SaveChangesAsync();
            return objective;
        }
    }
}
