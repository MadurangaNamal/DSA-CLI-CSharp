using practice.practiceFiles.CustomExceptions;
using System.Data.SqlClient;

namespace practice.practiceFiles;

public class DatabaseService
{
    private string _connectionString;
    private string _databaseName;

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
