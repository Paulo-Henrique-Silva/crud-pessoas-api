using ApiTeste.Controllers;
using ApiTeste.Dtos;
using ApiTeste.Exceptions;
using ApiTeste.Interfaces;
using ApiTeste.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiTeste.Tests.ControllerTests
{
    public class PessoaControllerTests
    {
        private readonly Mock<IPessoaService> mockPessoaService;

        public PessoaControllerTests()
        {
            mockPessoaService = new Mock<IPessoaService>();
        }

        [Fact]
        public void ObterTudo_Deve_Retornar_Lista_Corretamente()
        {
            var pessoasEsperadas = new List<Pessoa>
            {
                new Pessoa(1, "N1", "C1", 1),
                new Pessoa(2, "N2", "C2", 2),
                new Pessoa(2, "N3", "C3", 3)
            };

            mockPessoaService.Setup(service => service.ObterTudo())
                .Returns(pessoasEsperadas);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.ObterTudo();

            //Testa action result
            Assert.NotNull(resultadoObtido);
            OkObjectResult resultadoOk = Assert.IsType<OkObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaSucessoAPI<List<Pessoa>>>(resultadoOk.Value);
            Assert.NotNull(repostasAPIObtida);
            Assert.Equivalent(pessoasEsperadas, repostasAPIObtida.Data);
        }

        [Fact]
        public void ObterPorId_Deve_Retornar_Pessoa_Corretamente()
        {
            int id = 1;
            var pessoaEsperada = new Pessoa(id, "A", "C", 1);

            mockPessoaService.Setup(service => service.ObterPorId(id))
                .Returns(pessoaEsperada);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.ObterPorId(id);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoOk = Assert.IsType<OkObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaSucessoAPI<Pessoa>>(resultadoOk.Value);
            Assert.NotNull(repostasAPIObtida);
            Assert.Equivalent(pessoaEsperada, repostasAPIObtida.Data);
        }

        [Fact]
        public void ObterPorId_Deve_Retornar_Erro_404()
        {
            int id = 0; //representa IDs que não existem ou parâmetros incorretos.
            var excecao = new PessoaNaoExisteException("Não existe uma pessoa com o ID: " + id);
            mockPessoaService.Setup(service => service.ObterPorId(id))
                .Throws(excecao);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.ObterPorId(id);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoNotFound = Assert.IsType<NotFoundObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            Assert.IsType<RespostaErroAPI>(resultadoNotFound.Value);
        }

        [Fact]
        public void Cadastrar_Deve_Retornar_Pessoa_Cadastrada()
        {
            var pessoaDTO = new PessoaDTO("N1", "C1", 1);
            var pessoaEsperada = new Pessoa(1, "N1", "C1", 1);

            mockPessoaService.Setup(service => service.Cadastrar(pessoaDTO))
                .Returns(pessoaEsperada);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.Cadastrar(pessoaDTO);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoCreated = Assert.IsType<CreatedResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaSucessoAPI<Pessoa>>(resultadoCreated.Value);
            Assert.NotNull(repostasAPIObtida);
            Assert.Equivalent(pessoaEsperada, repostasAPIObtida.Data);
        }
    }
}