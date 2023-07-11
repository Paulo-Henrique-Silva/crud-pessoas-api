using ApiTeste.Data;
using ApiTeste.Interfaces;
using ApiTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTeste.Repositories
{
    /// <summary>
    /// Realiza as operações CRUD na tabela de pessoas.
    /// </summary>
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DataContext dataContext;

        public PessoaRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Pessoa>> ObterTudoAsync()
        {
            return await dataContext.Pessoas.ToListAsync();
        }

        public async Task<Pessoa?> ObterPorIdAsync(int id)
        {
            return await dataContext.Pessoas.FindAsync(id);
        }

        public async Task<Pessoa> CadastrarAsync(Pessoa pessoa)
        {
            dataContext.Pessoas.Add(pessoa);
            await dataContext.SaveChangesAsync();

            return pessoa;
        }

        public async Task<Pessoa> EditarAsync(Pessoa pessoa)
        {
            dataContext.Pessoas.Update(pessoa);
            await dataContext.SaveChangesAsync();

            return pessoa;
        }

        public async Task RemoverAsync(Pessoa pessoa)
        {
            dataContext.Pessoas.Remove(pessoa);
            await dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistePorIdAsync(int id)
        {
            return await dataContext.Pessoas.AnyAsync(pessoa => pessoa.Id == id);
        }
    }
}
