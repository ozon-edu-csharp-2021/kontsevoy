using System.Collections.Concurrent;
using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Base.Models;
using MerchandiseService.Infrastructure.Database.Repositories.Infrastructure.Interfaces;

namespace MerchandiseService.Infrastructure.Database.Repositories.Infrastructure
{
    public class ChangeTracker : IChangeTracker
    {
        public IEnumerable<Entity<Id>> TrackedEntities => UsedEntitiesBackingField.ToArray();

        private ConcurrentBag<Entity<Id>> UsedEntitiesBackingField { get; } = new();
        
        public void Track(Entity<Id> entity)
        {
            UsedEntitiesBackingField.Add(entity);
        }
    }
}