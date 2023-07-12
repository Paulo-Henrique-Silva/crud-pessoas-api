using ApiTeste.Data;
using ApiTeste.Models;

namespace ApiTeste.Interfaces
{
    public interface IPessoaRepository
    {
        Task<List<Pessoa>> ObterTudoAsync();

        Task<Pessoa?> ObterPorIdAsync(int id);

        Task<Pessoa> CadastrarAsync(Pessoa pessoa);

        Task<Pessoa> EditarAsync(Pessoa pessoa);

        Task RemoverAsync(Pessoa pessoa);

        Task<bool> ExistePorIdAsync(int id);
    }
}
