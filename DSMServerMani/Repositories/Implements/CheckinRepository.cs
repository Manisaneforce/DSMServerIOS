using System.Data;
using DSMServerMani.ApplicationDbContext;
using DSMServerMani.Models;
using DSMServerMani.Repositories;
using Microsoft.Data.SqlClient;

namespace DSMServerMani.Repositories.Implements
{
    public class CheckinRepository : ICheckinRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public CheckinRepository(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<object> UserCheckinVerification(CheckinRequestModel checkinRequestModel)
        {
            // Example: Just return model now (you can later call your stored procedure)
            return checkinRequestModel;
        }
    }
}
