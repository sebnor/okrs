using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using OKRs.Models;

namespace OKRs.Repositories
{
    public class ObjectivesRepository : IObjectivesRepository
    {
        private readonly IMongoDatabase _db;
        private readonly AppConfiguration _configuration;

        public ObjectivesRepository(IOptions<AppConfiguration> configuration)
        {
            _configuration = configuration.Value;
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_configuration.DataConnectionString));
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            _db = mongoClient.GetDatabase(_configuration.Database);
        }
        public async Task CreateObjective(Objective objective)
        {
            var collection = _db.GetCollection<Objective>("objectives");
            await collection.InsertOneAsync(objective);
        }

        public async Task SaveObjective(Objective objective)
        {
            var collection = _db.GetCollection<Objective>("objectives");
            var filter = Builders<Objective>.Filter.Eq(nameof(Objective.Id), objective.Id);
            await collection.FindOneAndReplaceAsync(filter, objective);
        }

        public async Task<List<Objective>> GetAllObjectives()
        {
            var collection = _db.GetCollection<Objective>("objectives");
            return (await collection.FindAsync(Builders<Objective>.Filter.Empty)).ToList().OrderBy(x => x.Title).ToList(); //TODO: do orderBy using mongodb
        }

        public async Task<Objective> GetObjectiveById(Guid id)
        {
            var collection = _db.GetCollection<Objective>("objectives");
            return (await collection.FindAsync(Builders<Objective>.Filter.Eq(nameof(Objective.Id), id))).FirstOrDefault();
        }
    }
}
