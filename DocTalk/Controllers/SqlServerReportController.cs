using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; // Correct namespace

namespace DocTalk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        // This connection string connects to your local DocTalk database using Windows Authentication.
        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=DocTalk;Integrated Security=True;TrustServerCertificate=True;";

        [HttpGet("patients")]
        public IActionResult GetPatients()
        {
            var patients = new List<object>();

            // The 'using' statement ensures the database connection is closed properly.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    // This SQL query selects data from the 'Patients' table you created.
                    string sql = "SELECT PatientID, Name, LastVisit FROM Patients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patients.Add(new
                                {
                                    id = reader.GetString(0),
                                    name = reader.GetString(1),
                                    lastVisit = reader.GetDateTime(2).ToString("yyyy-MM-dd")
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // This error is sent if the database connection or query fails.
                    return StatusCode(500, new { error = "Database connection error.", details = ex.Message });
                }
            }
            // If everything is successful, it sends the patient data back to the web page.
            return Ok(patients);
        }
    }
}
