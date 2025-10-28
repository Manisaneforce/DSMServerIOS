namespace DSMServerMani.ApplicationDbContext
{
    public class AppDbConnection
    {
        public string ConnectionString { get; }

        public AppDbConnection(IConfiguration configuration)
        {
            string baseConnectionString = configuration["ConnectionStrings:DefaultConnection"]
                ?? throw new InvalidOperationException("DefaultConnection not found in secrets.");

            string databaseName = $"FMCG_SJDev"; // defaeult DB
            ConnectionString = $"{baseConnectionString};Database={databaseName};";
        }

    }

}
