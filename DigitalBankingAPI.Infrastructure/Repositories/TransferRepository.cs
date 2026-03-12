using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using DigitalBankingAPI.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DigitalBankingAPI.Infrastructure.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly string _connectionString;

        public TransferRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Transfer?> CreateTransferAsync(
            string fromAccountId,
            string toAccountId,
            decimal amount,
            CancellationToken cancellationToken)
        {
            try
            {
                await using var conn = new SqlConnection(_connectionString);

                await using var cmd = new SqlCommand("sp_ExecuteTransfer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@FromAccountId", SqlDbType.NVarChar).Value = fromAccountId;
                cmd.Parameters.Add("@ToAccountId", SqlDbType.NVarChar).Value = toAccountId;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount;

                await conn.OpenAsync(cancellationToken);

                await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

                if (!await reader.ReadAsync(cancellationToken))
                    return null;

                return new Transfer
                {
                    FromAccount = reader["FromAccount"].ToString(),
                    ToAccount = reader["ToAccount"].ToString(),
                    Amount = (decimal)reader["Amount"],
                    Message = reader["Message"].ToString()
                };
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }

}
