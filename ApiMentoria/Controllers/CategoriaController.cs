using ApiMentoria.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ApiMentoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly string _connectionString;
        public CategoriaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiMentoria");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            using (var sqlconnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Categoria WHERE  Status_C=1";

                var categoria = await sqlconnection.QueryAsync(sql);
                return Ok(categoria);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBayId(int id)
        {
            var paramentro = new { id };
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Categoria WHERE Id = @id";

                var categoria = await sqlConnection.QuerySingleOrDefaultAsync<CategoriaModel>(sql, paramentro);
                if (categoria is null)
                {
                    return NotFound();
                }
                return Ok(categoria);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoriaModel model)
        {
            var paramentros = new
            {
                model.Nome,
                model.Status_C,
            };
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Categoria OUTPUT INSERTED.Id VALUES (@Nome, @Status_C)";
                var id = await sqlConnection.ExecuteScalarAsync<int>(sql, paramentros);
                return Ok(id);
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, CategoriaModel model)
        {
            var paramentros = new
            {
                Id = id,
                Nome = model.Nome,
                Status_C = model.Status_C,
            };

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Categoria SET Nome=@Nome, Status_C=@Status_C   WHERE  Id=@Id";

                await sqlConnection.ExecuteAsync(sql, paramentros);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paramentros = new
            {
                id
            };

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
               
                const string sql = "DELETE FROM Categoria WHERE  Id = @Id";

                await sqlConnection.ExecuteAsync(sql, paramentros);
                return NoContent();

            }
        }
    }
}