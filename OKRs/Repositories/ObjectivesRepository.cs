using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OKRs.Models;

namespace OKRs.Repositories
{
    public class ObjectivesRepository : IObjectivesRepository
    {
        private readonly IMongoDatabase _db;
        private readonly DataConfiguration _configuration;

        public ObjectivesRepository(IOptions<DataConfiguration> configuration)
        {
            _configuration = configuration.Value;
            var settings = MongoClientSettings.FromUrl(new MongoUrl(_configuration.ConnectionString));
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            _db = mongoClient.GetDatabase(_configuration.Name);
        }
        public async Task CreateObjective(Objective objective)
        {
            await ObjectivesCollection().InsertOneAsync(objective);
        }

        public async Task SaveObjective(Objective objective)
        {
            var filter = Builders<Objective>.Filter.Eq(nameof(Objective.Id), objective.Id);
            await ObjectivesCollection().FindOneAndReplaceAsync(filter, objective);
        }

        public async Task<List<Objective>> GetAllObjectives()
        {
            return (await ObjectivesCollection().FindAsync(Builders<Objective>.Filter.Empty)).ToList().OrderBy(x => x.Title).ToList(); //TODO: do orderBy using mongodb
        }

        public async Task<Objective> GetObjectiveById(Guid id)
        {
            return (await ObjectivesCollection().FindAsync(Builders<Objective>.Filter.Eq(nameof(Objective.Id), id))).FirstOrDefault();
        }

        public async Task<List<Objective>> GetObjectivesByUserId(Guid userId)
        {
            return (await ObjectivesCollection().FindAsync(Builders<Objective>.Filter.Eq(nameof(Objective.UserId), userId))).ToList();
        }

        private IMongoCollection<Objective> ObjectivesCollection()
        {
            return _db.GetCollection<Objective>("objectives");
        }
    }
}
