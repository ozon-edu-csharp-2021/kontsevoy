using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Database.Repositories.Infrastructure.Interfaces;
using MerchandiseService.Infrastructure.Repositories.Models;
using Npgsql;

namespace MerchandiseService.Infrastructure.Database.Postgres.Repositories.Implementation
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

        private static T Build<T>(MerchRequestDto dto) => (T)Activator.CreateInstance(typeof(T),
            BindingFlags.Instance | BindingFlags.NonPublic, null, new object[]
            {
                (Id)dto.Id,
                (CreationTimestamp)dto.CreatedAt,
                (Id)dto.EmployeeId,
                (Email)dto.EmployeeNotificationEmail,
                (ClothingSize)dto.EmployeeClothingSize,
                (MerchPack)dto.MerchPackId,
                (MerchRequestStatus)dto.Status,
                (HandoutTimestamp)dto.TryHandoutAt,
                (HandoutTimestamp)dto.HandoutAt,
                (Handout)dto.Handout
            }, null);

        private async Task<MerchRequest> QuerySingleAndTrack(string sql, object parameters,
            CancellationToken cancellationToken)
        {
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await DbConnectionFactory.CreateConnection(cancellationToken);
            var dto = await connection.QuerySingleAsync<MerchRequestDto>(commandDefinition);
            var merchRequest = Build<MerchRequest>(dto);
            ChangeTracker.Track(merchRequest);
            return merchRequest;
        }

        public async Task<MerchRequest> CreateAsync(MerchRequest itemToCreate, CancellationToken cancellationToken)
        {
            if (itemToCreate is null)
                throw new ArgumentNullException(nameof(itemToCreate), $"{nameof(itemToCreate)} must be provided");
            
            if (!itemToCreate.IsTransient)
                throw new ArgumentException($"{nameof(itemToCreate)} entity must be transient", nameof(itemToCreate));
            
            const string sql = @"INSERT INTO merch_requests
                (created_at, employee_id, employee_notification_email, employee_clothing_size, merch_pack_id, status, try_handout_at, handout_at, handout)
	            VALUES (@CreatedAt, @EmployeeId, @EmployeeNotificationEmail, @EmployeeClothingSize, @MerchPackId, @Status, @TryHandoutAt, @HandoutAt, @Handout::jsonb)
	            RETURNING *;";
            
            return await QuerySingleAndTrack(sql, MerchRequestDto.From(itemToCreate), cancellationToken);
        }

        public async Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            if (itemToUpdate is null)
                throw new ArgumentNullException(nameof(itemToUpdate), $"{nameof(itemToUpdate)} must be provided");
            
            if (itemToUpdate.IsTransient)
                throw new ArgumentException($"{nameof(itemToUpdate)} entity must not be transient", nameof(itemToUpdate));
            
            const string sql = @"UPDATE merch_requests SET
                employee_notification_email = @EmployeeNotificationEmail,
                employee_clothing_size = @EmployeeClothingSize,
                status = @Status,
                try_handout_at = @TryHandoutAt,
                handout_at = @HandoutAt,
                handout = @Handout::jsonb
                WHERE id = @Id
	            RETURNING *;";
            
            return await QuerySingleAndTrack(sql, MerchRequestDto.From(itemToUpdate), cancellationToken);
        }

        public async Task<MerchRequest> FindByIdAsync(Id id, CancellationToken cancellationToken = default)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id), $"{nameof(id)} must be provided");
            
            const string sql = @"SELECT * FROM merch_requests WHERE id = @Id;";
            
            return await QuerySingleAndTrack(sql, new { Id = id.Value }, cancellationToken);
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