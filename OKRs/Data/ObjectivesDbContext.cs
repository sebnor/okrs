using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OKRs.Core.Domain;
using OKRs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OKRs.Web.Data
{
    public class ObjectivesDbContext : DbContext
    {
        private readonly ITextSerializer _textSerializer;

        public ObjectivesDbContext(DbContextOptions<ObjectivesDbContext> options, ITextSerializer textSerializer)
            : base(options)
        {
            _textSerializer = textSerializer;
        }

        public DbSet<Objective> Objectives { get; set; }
        public DbSet<KeyResult> KeyResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objective>(objective =>
            {
                objective
                .Property(properties =>
                    properties.KeyResults)
                .HasConversion(
                    keyResults =>
                        _textSerializer.Serialize(keyResults),
                    serializedCollection => DeserializeKeyResults(serializedCollection),
                    new ValueComparer<KeyResults>(
                        (leftCollection, rightCollection)
                            => leftCollection.SequenceEqual(rightCollection),
                        collection =>
                            collection.Aggregate(0, (currentHashCode, valueToAggregate) =>
                                HashCode.Combine(currentHashCode, valueToAggregate.GetHashCode())),
                            collection => collection));
            });

            MapKeyResults(modelBuilder);
        }

        private KeyResults DeserializeKeyResults(string serializedCollection)
        {
            if (string.IsNullOrWhiteSpace(serializedCollection))
            {
                return new KeyResults(new HashSet<KeyResult>());
            }

            return new KeyResults(_textSerializer.Deserialize<IEnumerable<KeyResult>>(serializedCollection));
        }

        private static void MapKeyResults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KeyResult>(keyResult =>
            {
                keyResult
                .Property(properties =>
                    properties.Description)
                .HasConversion<string>(
                    description =>
                        description.Value,
                    descriptionString =>
                        new Description(descriptionString));
            });
        }
    }
}
