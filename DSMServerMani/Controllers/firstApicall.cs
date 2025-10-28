using DSMServerMani.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DSMServerMani.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class firstApicall : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly AppDbConnection _dbConnection;
        public firstApicall(AppDbContext context, AppDbConnection dbConnection)
        {
            context = context;
            _dbConnection = dbConnection;
        }

        [HttpGet("getmaster")]

        public IActionResult Getmaster(string sfcode)
        {
            var result = new List<Dictionary<string, object>>();

            try
            {
                if (string.IsNullOrWhiteSpace(sfcode))
                    return BadRequest("sfcode are required.");

                using (var conn = new SqlConnection(_dbConnection.ConnectionString))
                using (var cmd = new SqlCommand(@"select * from Mas_Salesforce where Sf_Code = @sfcode", conn))
                {
                    cmd.Parameters.AddWithValue("@sfcode", sfcode);


                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var columnName = reader.GetName(i);
                                var value = reader.IsDBNull(i) ? null : reader.GetValue(i);

                                if (columnName.Equals("addr_edit_delete", StringComparison.OrdinalIgnoreCase))
                                {
                                    value = (bool)value;
                                }

                                row[columnName] = value;
                            }
                            result.Add(row);
                        }
                    }
                }

                return Ok(new
                {
                    Status = true,
                    Message = "Success",
                    Response = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

    }
}
