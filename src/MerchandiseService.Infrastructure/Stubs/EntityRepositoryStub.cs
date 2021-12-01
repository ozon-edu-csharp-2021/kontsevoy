using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.Base.Contracts;
using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Infrastructure.Stubs
{
    public abstract class EntityRepositoryStub<TAggregationRoot, TAggregationRootId> 
        : IRepository<TAggregationRoot, TAggregationRootId> 
        where TAggregationRoot : Entity<TAggregationRootId>
    {
        protected static Dictionary<TAggregationRootId, TAggregationRoot> Dictionary { get; } = new();
        protected abstract TAggregationRootId GenerateId();
        public Task<TAggregationRoot> CreateAsync(TAggregationRoot itemToCreate, CancellationToken cancellationToken = default)
        {
            lock (Dictionary)
            {
                var id = itemToCreate.IsTransient ? GenerateId() : itemToCreate.Id;
                
                if (!itemToCreate.IsTransient && Dictionary.ContainsKey(id))
                    throw new ArgumentException(
                        $"{typeof(TAggregationRoot)} with {nameof(itemToCreate.Id)} = {itemToCreate.Id} already exists",
                        nameof(itemToCreate));
                
                if (itemToCreate.IsTransient)
                    itemToCreate.Id = id;
                
                Dictionary[id] = itemToCreate;
            }

            return Task.FromResult(itemToCreate);
        }

        public Task<TAggregationRoot> UpdateAsync(TAggregationRoot itemToUpdate, CancellationToken cancellationToken = default)
        {
            lock (Dictionary)
            {
                if (itemToUpdate.IsTransient || !Dictionary.ContainsKey(itemToUpdate.Id))
                    throw new ArgumentException(
                        $"{typeof(TAggregationRoot)} with {nameof(itemToUpdate.Id)} = {itemToUpdate.Id} not exists",
                        nameof(itemToUpdate));
                Dictionary[itemToUpdate.Id] = itemToUpdate;
            }
            
            return Task.FromResult(itemToUpdate);
        }

        public Task<TAggregationRoot> FindByIdAsync(TAggregationRootId id, CancellationToken cancellationToken = default)
        {
            TAggregationRoot result;
            lock (Dictionary)
            {
                if (!Dictionary.ContainsKey(id))
                    return Task.FromResult<TAggregationRoot>(default);
                result = Dictionary[id];
            }
            return Task.FromResult(result);
        }
    }
}