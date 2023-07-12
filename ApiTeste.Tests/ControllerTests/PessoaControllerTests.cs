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
                new Pessoa(3, "N3", "C3", 3)
            };

            mockPessoaService.Setup(service => service.ObterTudo("id"))
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
        public void ObterPorId_Deve_Retornar_Erro_Pessoa_Nao_Existe()
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

        [Fact]
        public void Cadastrar_Deve_Retornar_Erro_Dados_Invalidos()
        {
            var pessoaDTO = new PessoaDTO("N1", "C1", 1);
            var excecao = new EntradaInvalidaException("Dados inválidos!");

            mockPessoaService.Setup(service => service.Cadastrar(pessoaDTO))
                .Throws(excecao);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.Cadastrar(pessoaDTO);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaErroAPI>(resultadoBadRequest.Value);
            Assert.NotNull(repostasAPIObtida);
        }

        [Fact]
        public void Editar_Deve_Retornar_Pessoa_Alterada()
        {
            int id = 1;
            var pessoaDTO = new PessoaDTO("N1", "C1", 1);
            var pessoaEsperada = new Pessoa(1, "N1", "C1", 1);

            mockPessoaService.Setup(service => service.Editar(id, pessoaDTO))
                .Returns(pessoaEsperada);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.EditarPorId(id, pessoaDTO);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoOk = Assert.IsType<OkObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaSucessoAPI<Pessoa>>(resultadoOk.Value);
            Assert.NotNull(repostasAPIObtida);
            Assert.Equivalent(pessoaEsperada, repostasAPIObtida.Data);
        }

        [Fact]
        public void Editar_Deve_Retornar_Erro_Pessoa_Nao_Existe()
        {
            int id = 0;
            var pessoaDTO = new PessoaDTO("N1", "C1", 1);
            var excecao = new PessoaNaoExisteException("Não existe uma pessoa de ID: " + id);

            mockPessoaService.Setup(service => service.Editar(id, pessoaDTO))
                .Throws(excecao);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.EditarPorId(id, pessoaDTO);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoNotFound = Assert.IsType<NotFoundObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaErroAPI>(resultadoNotFound.Value);
            Assert.NotNull(repostasAPIObtida);
        }

        [Fact]
        public void Editar_Deve_Retornar_Erro_Dados_Invalidos()
        {
            int id = 0;
            var pessoaDTO = new PessoaDTO("N1", "C1", 1);
            var excecao = new EntradaInvalidaException("Não existe uma pessoa de ID: " + id);

            mockPessoaService.Setup(service => service.Editar(id, pessoaDTO))
                .Throws(excecao);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.EditarPorId(id, pessoaDTO);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaErroAPI>(resultadoBadRequest.Value);
            Assert.NotNull(repostasAPIObtida);
        }

        [Fact]
        public void Remover_Deve_Remover_Pessoa_Corretamente()
        {
            int id = 1;

            mockPessoaService.Setup(service => service.RemoverPorId(id));

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.Remover(id);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoOk = Assert.IsType<OkObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaSucessoAPI>(resultadoOk.Value);
            Assert.NotNull(repostasAPIObtida);
        }

        [Fact]
        public void Remover_Deve_Retornar_Erro_Pessoa_Nao_Existe()
        {
            int id = 0;
            var excecao = new PessoaNaoExisteException("Não existe uma pessoa de ID: " + id);

            mockPessoaService.Setup(service => service.RemoverPorId(id))
                .Throws(excecao);

            var pessoaController = new PessoaController(mockPessoaService.Object);

            var resultadoObtido = pessoaController.Remover(id);

            //Testa action result
            Assert.NotNull(resultadoObtido);
            var resultadoNotfound = Assert.IsType<NotFoundObjectResult>(resultadoObtido);

            //testa objeto contido no action result
            var repostasAPIObtida = Assert.IsType<RespostaErroAPI>(resultadoNotfound.Value);
            Assert.NotNull(repostasAPIObtida);
        }
    }
}