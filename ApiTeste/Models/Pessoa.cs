using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTeste.Models
{
    /// <summary>
    /// Classe básica que encapsula e valida os dados enviados pelo cliente e obtidos no banco de dados.
    /// </summary>
    [Table("tb_pessoas")]
    public class Pessoa
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo de nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo de nome precisa ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Column("cidade")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo de cidade é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo de cidade precisa ter no máximo 50 caracteres.")]
        public string Cidade { get; set; }

        [Column("salario")]
        [Required(ErrorMessage = "O campo de salário é obrigatório.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "O salário deve ser maior do que zero.")]
        public double Salario { get; set; }

        public Pessoa(string nome, string cidade, double salario)
        {
            Nome = nome;
            Cidade = cidade;
            Salario = salario;
        }

        public Pessoa(int id, string nome, string cidade, double salario)
        {
            Id = id;
            Nome = nome;
            Cidade = cidade;
            Salario = salario;
        }
    }
}
