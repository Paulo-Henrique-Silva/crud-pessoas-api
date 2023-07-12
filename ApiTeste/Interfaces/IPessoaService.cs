using ApiTeste.Dtos;
using ApiTeste.Models;

namespace ApiTeste.Interfaces
{
    public interface IPessoaService
    {
        List<Pessoa> ObterTudo();

        Pessoa ObterPorId(int id);

        Pessoa Cadastrar(PessoaDTO pessoa);

        Pessoa Editar(int id, PessoaDTO pessoaDTO);

        void RemoverPorId(int id);

        bool ExistePorId(int id);
    }
}
