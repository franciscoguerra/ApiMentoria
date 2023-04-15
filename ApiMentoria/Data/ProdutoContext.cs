using Microsoft.EntityFrameworkCore;

namespace ApiMentoria.Data
{
    public class ProdutoContext : DbContext 
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options)
            : base(options) { }
        
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("serverConnection"));
        }
        
    }
}
