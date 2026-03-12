using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using DigitalBankingAPI.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DigitalBankingAPI.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ----------------------------
        // Deposit (sp_CreateDeposit)
        // ----------------------------
        public async Task<Account?> CreateDepositAsync(
            string accountId,
            decimal amount,
            CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);

                await using var cmd = new SqlCommand("sp_CreateDeposit", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = accountId;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount;

                await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

                if (!await reader.ReadAsync(cancellationToken))
                    return null;

                return new Account
                {
                    AccountId = reader["AccountId"].ToString(),
                    CustomerName = reader["CustomerName"].ToString(),
                    Balance = (decimal)reader["Balance"]
                };
            }
            catch (SqlException)
            {
                return null;
            }
        }

        // ----------------------------
        // Get account info (sp_GetAccountInfo)
        // ----------------------------
        public async Task<AccountDetails?> GetAccountInfoAsync(string accountId, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("sp_GetAccountInfo", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = accountId;

            await conn.OpenAsync(cancellationToken);
            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

            if (!await reader.ReadAsync(cancellationToken))
                return null;

            var result = new AccountDetails
            {
                AccountId = reader["AccountId"].ToString(),
                Balance = (decimal)reader["Balance"]
            };

            if (await reader.NextResultAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    result.LastTransactions.Add(new Transaction
                    {
                        Type = reader["Type"].ToString(),
                        Amount = (decimal)reader["Amount"],
                        Date = (DateTime)reader["Date"],
                        Description = reader["Description"].ToString()
                    });
                }
            }

            if (await reader.NextResultAsync(cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    result.TotalInterest = (decimal)reader["TotalInterest"];
                }
            }

            return result;
        }

        // ----------------------------
        // CreateWithdrawal (sp_CreateWithdrawal)
        // ----------------------------
        public async Task<Account?> CreateWithdrawalAsync(
            string accountId,
            decimal amount,
            CancellationToken cancellationToken)
        {
            try
            {
                await using var conn = new SqlConnection(_connectionString);
                await using var cmd = new SqlCommand("sp_CreateWithdrawal", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = accountId;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount;

                await conn.OpenAsync(cancellationToken);

                await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

                if (!await reader.ReadAsync(cancellationToken))
                    return null;

                return new Account
                {
                    AccountId = reader["AccountId"].ToString(),
                    CustomerName = reader["CustomerName"].ToString(),
                    Balance = (decimal)reader["Balance"]
                };
            }
            catch (SqlException)
            {
                return null;
            }
        }

        // ----------------------------
        // GetInterestHistory (sp_GetInterestHistory)
        // ----------------------------
        public async Task<List<InterestHistory>> GetInterestHistoryAsync(
            string accountId,
            CancellationToken cancellationToken)
        {
            var result = new List<InterestHistory>();

            try
            {
                await using var conn = new SqlConnection(_connectionString);

                await using var cmd = new SqlCommand("sp_GetInterestHistory", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = accountId;

                await conn.OpenAsync(cancellationToken);

                await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    result.Add(new InterestHistory
                    {
                        Id = (int)reader["Id"],
                        AccountId = reader["AccountId"].ToString(),
                        InterestRate = (decimal)reader["InterestRate"],
                        CalculatedInterest = (decimal)reader["CalculatedInterest"],
                        CalculationDate = (DateTime)reader["CalculationDate"]
                    });
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return result;
        }
    }
}
