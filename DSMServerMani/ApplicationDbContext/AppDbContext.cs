using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace DSMServerMani.ApplicationDbContext
{
    public class AppDbContext : DbContext 
    {
        private readonly string _connectionString;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext(AppDbConnection dbConnection, DbContextOptions<AppDbContext> options)
            : base(options)
        {
            _connectionString = dbConnection.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
       
        public async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, List<SqlParameter> parameters)
        {
            var dataTable = new DataTable();

            using (var connection = (SqlConnection)Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using var command = new SqlCommand(procedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parameters != null && parameters.Count > 0)
                    command.Parameters.AddRange(parameters.ToArray());

                using var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
            }

            return dataTable;
        }
    }
}
