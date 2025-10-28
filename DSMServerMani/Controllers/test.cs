using DSMServerMani.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DSMServerMani.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class test : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly AppDbConnection _dbConnection;


        public test(AppDbContext context, AppDbConnection dbConnection)
        {
            context = context;
            _dbConnection = dbConnection;
        }

       


        [HttpGet("getretaddress")]
        public IActionResult GetRetAddress([FromQuery] string listedDrCode, [FromQuery] string divisionCode)
        {
            var result = new List<Dictionary<string, object>>();

            try
            {
                if (string.IsNullOrWhiteSpace(listedDrCode) || string.IsNullOrWhiteSpace(divisionCode))
                    return BadRequest("ListedDrCode and DivisionCode are required.");

                using (var conn = new SqlConnection(_dbConnection.ConnectionString))
                using (var cmd = new SqlCommand(@"SELECT * FROM Mas_RetDeliveryAddress WITH (NOLOCK) WHERE ListedDrCode = @ListedDrCode AND flag = '0' AND Division_code = @DivisionCode", conn))
                {
                    cmd.Parameters.AddWithValue("@ListedDrCode", listedDrCode);
                    cmd.Parameters.AddWithValue("@DivisionCode", divisionCode);

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




