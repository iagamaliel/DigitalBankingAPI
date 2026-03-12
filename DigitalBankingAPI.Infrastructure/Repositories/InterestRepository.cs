using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DigitalBankingAPI.Infrastructure.Repositories
{
    public class InterestRepository : IInterestRepository
    {
        private readonly string _connectionString;

        public InterestRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ----------------------------
        // CalculateDailyInterest (sp_CalculateDailyInterest)
        // ----------------------------
        public async Task<(bool Success, string Message)> CalculateDailyInterestAsync(
            CancellationToken cancellationToken)
        {
            try
            {
                await using var conn = new SqlConnection(_connectionString);

                await using var cmd = new SqlCommand("sp_CalculateDailyInterest", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await conn.OpenAsync(cancellationToken);

                await cmd.ExecuteNonQueryAsync(cancellationToken);

                return (true, null);
            }
            catch (SqlException ex)
            {
                return (false, ex.Message);
            }
        }

    }

}
