using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Infrastructure.Database.Postgres.Repositories.Infrastructure.Exceptions;
using MerchandiseService.Infrastructure.Database.Repositories.Infrastructure.Interfaces;
using Npgsql;

namespace MerchandiseService.Infrastructure.Database.Postgres.Repositories.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private NpgsqlTransaction NpgsqlTransaction { get; set; }
        
        private IDbConnectionFactory<NpgsqlConnection> DbConnectionFactory { get; }
        private IPublisher Publisher { get; }
        private IChangeTracker ChangeTracker { get; }

        public UnitOfWork(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IPublisher publisher,
            IChangeTracker changeTracker)
        {
            DbConnectionFactory = dbConnectionFactory;
            Publisher = publisher;
            ChangeTracker = changeTracker;
        }

        public async ValueTask StartTransaction(CancellationToken token)
        {
            if (NpgsqlTransaction is not null) return;
            
            var connection = await DbConnectionFactory.CreateConnection(token);
            NpgsqlTransaction = await connection.BeginTransactionAsync(token);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (NpgsqlTransaction is null) throw new NoActiveTransactionStartedException();

            var domainEvents = new Queue<INotification>(
                ChangeTracker.TrackedEntities
                    .SelectMany(x =>
                    {
                        var events = x.DomainEvents.ToList();
                        x.ClearDomainEvents();
                        return events;
                    }));
            while (domainEvents.TryDequeue(out var notification))
                await Publisher.Publish(notification, cancellationToken);

            await NpgsqlTransaction.CommitAsync(cancellationToken);
        }

        void IDisposable.Dispose()
        {
            NpgsqlTransaction?.Dispose();
            DbConnectionFactory?.Dispose();
        }
    }
}