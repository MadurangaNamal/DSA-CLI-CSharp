using Practice.Exceptions;
using System.Data.SqlClient;

namespace Practice.Database;

public class DatabaseService
{
    private readonly string _connectionString;
    private readonly string _databaseName;

    public DatabaseService(string connectionString, string databaseName)
    {
        _connectionString = connectionString;
        _databaseName = databaseName;
    }

    public async Task ExecuteQueryAsync(string query)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        catch (SqlException ex)
        {
            throw new DatabaseException($"Failed to execute query: {ex.Message}", _databaseName, query, ex);
        }
    }
}
