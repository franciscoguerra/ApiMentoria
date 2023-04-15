using ApiMentoria.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ApiMentoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly string _connectionString;
        public ProdutoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiMentoria");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using (var sqlconnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Produto WHERE STATUS_Produto=1";

                var produto = await sqlconnection.QueryAsync(sql);
                return Ok(produto);
                    
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBayId(int id)
        {
            var paramentro = new { id };
            using (var sqlConnection = new SqlConnection(_connectionString)) 
            {
                const string sql = "SELECT * FROM Produto WHERE Id = @id";

                var produto = await sqlConnection.QuerySingleOrDefaultAsync<ProdutoModel>(sql, paramentro);
                if (produto is null)
                {
                    return NotFound();
                }
                return Ok(produto);
            }
            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoModel model)
        {
            return NoContent();
        }
    }
}
