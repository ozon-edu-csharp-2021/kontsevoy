using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Database.Postgres.Repositories.Models;
using MerchandiseService.Infrastructure.Database.Repositories.Infrastructure.Interfaces;
using Npgsql;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Database.Postgres.Repositories.Implementation
{
    public class MerchRequestRepository : IMerchRequestRepository
    {
        private ITracer Tracer { get; }
        private IDbConnectionFactory<NpgsqlConnection> DbConnectionFactory { get; }
        private IChangeTracker ChangeTracker { get; }
        
        private const int Timeout = 5;

        public MerchRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker, ITracer tracer)
        {
            DbConnectionFactory = dbConnectionFactory;
            ChangeTracker = changeTracker;
            Tracer = tracer;
        }

        private static T Build<T>(MerchRequestDto dto) => (T)Activator.CreateInstance(typeof(T),
            BindingFlags.Instance | BindingFlags.NonPublic, null, new object[]
            {
                (Id)dto.Id,
                (CreationTimestamp)dto.CreatedAt,
                (Email)dto.EmployeeEmail,
                (Name)dto.EmployeeName,
                (Email)dto.ManagerEmail,
                (Name)dto.ManagerName,
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

        private async Task<IReadOnlyCollection<MerchRequest>> QueryAllAndTrack(string sql, object parameters,
            CancellationToken cancellationToken)
        {
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await DbConnectionFactory.CreateConnection(cancellationToken);
            var dtos = await connection.QueryAsync<MerchRequestDto>(commandDefinition);
            var merchRequests = new List<MerchRequest>();
            foreach (var dto in dtos)
            {
                var merchRequest = Build<MerchRequest>(dto);
                merchRequests.Add(merchRequest);
                ChangeTracker.Track(merchRequest); 
            }
            return merchRequests.AsReadOnly();
        }

        public async Task<MerchRequest> CreateAsync(MerchRequest itemToCreate, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(CreateAsync)).WithTag(nameof(itemToCreate), itemToCreate?.EmployeeEmail).StartActive();
            
            if (itemToCreate is null)
                throw new ArgumentNullException(nameof(itemToCreate), $"{nameof(itemToCreate)} must be provided");
            
            if (!itemToCreate.IsTransient)
                throw new ArgumentException($"{nameof(itemToCreate)} entity must be transient", nameof(itemToCreate));
            
            const string sql = @"INSERT INTO merch_requests
                (created_at, employee_email, employee_name, manager_email, manager_name, employee_clothing_size, merch_pack_id, status, try_handout_at, handout_at, handout)
	            VALUES (@CreatedAt, @EmployeeEmail, @EmployeeName, @ManagerEmail, @ManagerName, @EmployeeClothingSize, @MerchPackId, @Status, @TryHandoutAt, @HandoutAt, @Handout::jsonb)
	            RETURNING *;";
            
            return await QuerySingleAndTrack(sql, MerchRequestDto.From(itemToCreate), cancellationToken);
        }

        public async Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            using var span = Tracer.BuildSpan(nameof(UpdateAsync)).WithTag(nameof(itemToUpdate), itemToUpdate?.Id?.Value.ToString()).StartActive();
            
            if (itemToUpdate is null)
                throw new ArgumentNullException(nameof(itemToUpdate), $"{nameof(itemToUpdate)} must be provided");
            
            if (itemToUpdate.IsTransient)
                throw new ArgumentException($"{nameof(itemToUpdate)} entity must not be transient", nameof(itemToUpdate));
            
            const string sql = @"UPDATE merch_requests SET
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
            using var span = Tracer.BuildSpan(nameof(FindByIdAsync)).WithTag(nameof(id), id?.Value.ToString()).StartActive();
            
            if (id is null)
                throw new ArgumentNullException(nameof(id), $"{nameof(id)} must be provided");
            
            const string sql = @"SELECT * FROM merch_requests WHERE id = @Id;";
            
            return await QuerySingleAndTrack(sql, new { Id = id.Value }, cancellationToken);
        }

        public async Task<IReadOnlyCollection<MerchRequest>> FindByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken = default)
        {
            using var span = Tracer.BuildSpan(nameof(FindByEmployeeEmailAsync)).WithTag(nameof(employeeEmail), employeeEmail?.Value).StartActive();

            var parameters = new
            {
                EmployeeEmail = employeeEmail?.Value ?? throw new ArgumentNullException(nameof(employeeEmail), $"{nameof(employeeEmail)} must be provided")
            };
            const string sql = @"SELECT * FROM merch_requests WHERE employee_email = @EmployeeEmail ORDER BY created_at DESC;";
            
            return await QueryAllAndTrack(sql, parameters, cancellationToken);
        }

        public async Task<IReadOnlyCollection<MerchRequest>> FindByStatus(MerchRequestStatus status, CancellationToken cancellationToken = default)
        {
            //Внутрисервисный метод, трассировку не добавляем, иначе их будет много
            
            var parameters = new
            {
                Status = status.Name ??
                         throw new ArgumentNullException(nameof(status), $"{nameof(status)} must be provided")
            };
            const string sql = @"SELECT * FROM merch_requests WHERE status = @Status;";

            return await QueryAllAndTrack(sql, parameters, cancellationToken);
        }
    }
}