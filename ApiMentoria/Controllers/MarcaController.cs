using ApiMentoria.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ApiMentoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly string _connectionString;
        public MarcaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiMentoria");

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            using (var sqlconnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Marca WHERE  Status_M=1";

                var marca = await sqlconnection.QueryAsync(sql);
                return Ok(marca);
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
                const string sql = "SELECT * FROM Marca WHERE Id = @id";

                var marca = await sqlConnection.QuerySingleOrDefaultAsync<MarcaModel>(sql, paramentro);
                if (marca is null)
                {
                    return NotFound();
                }
                return Ok(marca);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(MarcaModel model)
        {
            var paramentros = new
            {
                model.Nome,
                model.cnpj,
                model.Status_M,
                
            };
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Marca OUTPUT INSERTED.Id VALUES (@Nome, @CNPJ, @Status_M)";
                var id = await sqlConnection.ExecuteScalarAsync<int>(sql, paramentros);
                return Ok(id);
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, MarcaModel model)
        {
            var paramentros = new
            {
                Id = id,
                Nome = model.Nome,
                cnpj= model.cnpj,
                Status_M = model.Status_M,
            };

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Marca SET Nome=@Nome,cnpj=@cnpj, Status_M=@Status_M   WHERE  Id=@Id";

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

                const string sql = "DELETE FROM Marca WHERE  Id = @Id";

                await sqlConnection.ExecuteAsync(sql, paramentros);
                return NoContent();

            }
        }

    }
}
