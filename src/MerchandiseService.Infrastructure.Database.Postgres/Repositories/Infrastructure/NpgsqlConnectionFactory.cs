using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Infrastructure.Database.Configuration;
using MerchandiseService.Infrastructure.Database.Repositories.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;

namespace MerchandiseService.Infrastructure.Database.Postgres.Repositories.Infrastructure
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory<NpgsqlConnection>
    {
        private DatabaseConnectionOptions Options { get; }
        private NpgsqlConnection Connection { get; set; }
        
        public NpgsqlConnectionFactory(IOptions<DatabaseConnectionOptions> options) => Options = options.Value;

        public async Task<NpgsqlConnection> CreateConnection(CancellationToken token)
        {
            if (Connection != null) return Connection;

            Connection = new NpgsqlConnection(Options.ConnectionString);
            await Connection.OpenAsync(token);
            Connection.StateChange += (o, e) =>
            {
                if (e.CurrentState == ConnectionState.Closed)
                    Connection = null;
            };
            return Connection;
        }

        public void Dispose() => Connection?.Dispose();
    }
}