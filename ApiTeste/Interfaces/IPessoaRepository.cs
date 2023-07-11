using ApiTeste.Data;
using ApiTeste.Models;

namespace ApiTeste.Interfaces
{
    public interface IPessoaRepository
    {
        public Task<List<Pessoa>> ObterTudoAsync();

        public Task<Pessoa?> ObterPorIdAsync(int id);

        public Task<Pessoa> CadastrarAsync(Pessoa pessoa);

        public Task<Pessoa> EditarAsync(Pessoa pessoa);

        public Task RemoverAsync(Pessoa pessoa);

        public Task<bool> ExistePorIdAsync(int id);
    }
}
