using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using Npgsql;

namespace MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchRequestRepository : IMerchRequestRepository
    {
        private IDbConnectionFactory<NpgsqlConnection> DbConnectionFactory { get; }
        private IChangeTracker ChangeTracker { get; }
        
        private const int Timeout = 5;

        public MerchRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            DbConnectionFactory = dbConnectionFactory;
            ChangeTracker = changeTracker;
        }

        public async Task<MerchRequest> CreateAsync(MerchRequest itemToCreate, CancellationToken cancellationToken)
        {
            if (itemToCreate is null)
                throw new ArgumentNullException(nameof(itemToCreate), $"{nameof(itemToCreate)} must be provided");
            
            if (!itemToCreate.IsTransient)
                throw new ArgumentException($"{nameof(itemToCreate)} entity must be transient", nameof(itemToCreate));
            
            const string sql = @"INSERT INTO merch_requests
                (employee_id, employee_notification_email, employee_clothing_size, merch_pack_id, status)
	            VALUES (@EmployeeId, @EmployeeNotificationEmail, @EmployeeClothingSize, @MerchPackId, @Status)
	            RETURNING id;";
            
            var parameters = new
            {
                EmployeeId = itemToCreate.EmployeeId.Value,
                EmployeeNotificationEmail = itemToCreate.EmployeeNotificationEmail.Value,
                EmployeeClothingSize = itemToCreate.EmployeeClothingSize.Name,
                MerchPackId = itemToCreate.MerchPack.Id,
                Status = itemToCreate.Status.Name
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await DbConnectionFactory.CreateConnection(cancellationToken);
            var id = await connection.QueryFirstAsync<int>(commandDefinition);
            itemToCreate.Id = new Id(id);
            ChangeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchRequest> FindByIdAsync(Id id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ContainsByParamsAsync(Id employeeId, MerchPack merchPack, CancellationToken cancellationToken = default)
        {
            var parameters = new
            {
                EmployeeId = employeeId?.Value ?? throw new ArgumentNullException(nameof(employeeId), $"{nameof(employeeId)} must be provided"),
                MerchPackId = merchPack?.Id ?? throw new ArgumentNullException(nameof(merchPack), $"{nameof(merchPack)} must be provided")
            };
            const string sql = @"SELECT EXISTS(SELECT id FROM merch_requests WHERE employee_id = @EmployeeId AND merch_pack_id = @MerchPackId);";
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await DbConnectionFactory.CreateConnection(cancellationToken);
            var exists = await connection.QuerySingleAsync<bool>(commandDefinition);
            return exists;
        }
    }
}