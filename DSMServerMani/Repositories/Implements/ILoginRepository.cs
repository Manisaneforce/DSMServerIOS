using System.Data;
using DSMServerMani.ApplicationDbContext;
using DSMServerMani.Models;
using DSMServerMani.Repositories.Implements;
using Microsoft.Data.SqlClient;

namespace DSMServerMani.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public LoginRepository(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<object> UserLoginVerification(LoginRequestModel loginRequestModel)
        {
            const string procedureName = "DSM_UserLogin";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@User_Id", loginRequestModel?.Username ?? string.Empty),
                new SqlParameter("@Password", loginRequestModel?.Password ?? string.Empty),
                new SqlParameter("@Version", loginRequestModel?.App_Version ?? string.Empty),
                new SqlParameter("@Device_Name", loginRequestModel?.Device_Name ?? string.Empty),
                new SqlParameter("@Device_ID", loginRequestModel?.Device_ID ?? string.Empty),
                new SqlParameter("@Device_Version", loginRequestModel?.Device_Version ?? string.Empty),
                new SqlParameter("@App_Token", loginRequestModel?.App_Token ?? string.Empty),
                new SqlParameter("@LoginType", loginRequestModel?.LoginType ?? string.Empty)
            };

            var dataTable = await _dbContext.ExecuteStoredProcedureAsync(procedureName, parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                var firstRow = dataTable.Rows[0];
                var response = dataTable.Columns.Cast<DataColumn>().ToDictionary(column => column.ColumnName, column => firstRow[column]);
                return new
                {
                    Status = true,
                    Message = "Login successful",
                    Data = response
                };
            }
            else
            {
                return new
                {
                    Status = false,
                    Message = "Invalid username or password"
                };
            }
        }
    }
}
