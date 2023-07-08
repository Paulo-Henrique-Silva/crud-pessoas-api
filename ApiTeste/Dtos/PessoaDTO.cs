using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTeste.Dtos
{
    /// <summary>
    /// DTO para encapsular dados enviados no cadastro e edição de pessoas.
    /// </summary>
    public class PessoaDTO
    {
        public string Nome { get; set; }

        public string Cidade { get; set; }

        public double Salario { get; set; }

        public PessoaDTO(string nome, string cidade, double salario)
        {
            Nome = nome;
            Cidade = cidade;
            Salario = salario;
        }
    }
}
