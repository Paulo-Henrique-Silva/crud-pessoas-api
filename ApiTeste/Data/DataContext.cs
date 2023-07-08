using ApiTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTeste.Data
{
    /// <summary>
    /// Representa o banco de dados da aplicação.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /// <summary>
        /// Representa a tabela de pessoas; "tb_pessoas".
        /// </summary>
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
