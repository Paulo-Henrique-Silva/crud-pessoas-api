using ApiTeste.Exceptions;
using ApiTeste.Interfaces;
using ApiTeste.Models;
using ApiTeste.Services;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTeste.Tests.ServicesTests
{
    public class PessoaServiceTests
    {
        private readonly Mock<IPessoaRepository> mockPessoaRepository;

        public PessoaServiceTests()
        {
            mockPessoaRepository = new Mock<IPessoaRepository>();
        }

        [Theory]
        [InlineData(null, "C1", 500)] //nome inválido.
        [InlineData("N1", null, 500)] //cidade inválida.
        [InlineData("N1", "C1", 0)] //salario deve ser maior que zero.
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque scelerisque condimentum diam. Sed eu congue nisi. Maecenas ut felis lorem. Etiam malesuada varius massa.", 
            "C1", 500)] //nome ultrapassa limite máximo.
        [InlineData("N1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Pellentesque scelerisque condimentum diam.Sed eu congue nisi.Maecenas ut felis lorem. Etiam malesuada varius massa.", 
            500)] //cidade ultrapassa limite máximo.
        [InlineData(null, null, -500)] //todos os dados estão incorretos.
        public void Validar_Deve_Jogar_Excecao_Com_Dados_Invalidos(string nome, string cidade, double salario)
        {
            var pessoa = new Pessoa(1, nome, cidade, salario);
            var pessoaService = new PessoaService(mockPessoaRepository.Object);

            Assert.Throws<EntradaInvalidaException>(() => pessoaService.ValidarPessoa(pessoa));
        }
    }
}
