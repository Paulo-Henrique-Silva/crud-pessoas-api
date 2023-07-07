using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTeste.Dtos
{
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
