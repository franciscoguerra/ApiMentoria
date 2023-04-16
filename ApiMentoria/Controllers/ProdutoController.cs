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


        /// <summary>
        /// Obter todos os eventos
        /// </summary>
        /// <returns>coleção de eventos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            using (var sqlconnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Produto WHERE  STATUS_Produto = 1";

                var produto = await sqlconnection.QueryAsync(sql);
                return Ok(produto);
                    
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Dados do Evento</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não Encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        }
        /// <summary>
        /// Cadastrar um evento
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Objeto criado</returns>
        /// <responde code="201">Sucesso</responde>
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(ProdutoModel model)
        {
            var produto = new ProdutoModel(model.Nome, model.Valor, model.Quantidade);
            var paramentros = new
            {
                Nome = model.Nome,
                Valor = model.Valor,
                Quantidade = model.Quantidade,
                STATUS_Produto = model.STATUS_Produto,

            };

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Produto OUTPUT INSERTED.Id VALUES (@Id, @Nome, @Valor, @Quantidade, @STATUS_Produto)";
                var id = await sqlConnection.ExecuteScalarAsync<int>(sql, paramentros);
                return Ok(id);
            }

            
        }
        /// <summary>
        /// Atualizar umProduto
        /// </summary>
        /// <remarks>
        /// objeto JSON
        /// <\remarks>
        /// <param name="id">Identificador de Produto</param>
        /// <param name="model">Dados do Produto</param>
        /// <returns>Nada.</returns>
        /// <responde code="404">Não Encontrado<\response>
        /// <response code="204"> Sucesso</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, ProdutoModel model)
        {
            var paramentros = new
            {
                 id= id,
                 model.Nome,
                 model.Valor,
                 model.Quantidade,   

            };

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Produto SET Nome=@Nome, Valor=@Valor, Quantidade=@Quantidade  WHERE  Id = @Id";

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
                const string sql = "UPDATE Produto SET Status =0 WHERE  Id = @Id";

                await sqlConnection.ExecuteAsync(sql, paramentros);
                return NoContent();

            }
        }
    }


}
