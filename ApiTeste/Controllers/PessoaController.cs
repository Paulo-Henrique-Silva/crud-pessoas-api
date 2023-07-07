using ApiTeste.Data;
using ApiTeste.Dtos;
using ApiTeste.Exceptions;
using ApiTeste.Models;
using ApiTeste.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [Controller]
    [Route("api/v1/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService pessoaService;

        public PessoaController(PessoaService pessoaService)
        {
            this.pessoaService = pessoaService;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            try
            {
                List<Pessoa> pessoas = pessoaService.ObterTudo();

                var resposta = new RespostaSucessoAPI<List<Pessoa>>(StatusCodes.Status200OK, 
                    "Lista de pessoas obtida com sucesso!", pessoas);

                return Ok(resposta);
            }
            catch (Exception)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status500InternalServerError, 
                    "Um erro inesperado aconteceu.");

                return StatusCode(500, resposta);
            }
        }

        [HttpGet("{id}")]
        public ActionResult ObterPorId(int id)
        {
            try
            {
                var pessoa = pessoaService.ObterPorId(id);

                var resposta = new RespostaSucessoAPI<Pessoa>(StatusCodes.Status200OK,
                    $"Pessoa de ID {pessoa.Id} obtida com sucesso!", pessoa);

                return Ok(resposta);
            }
            catch (PessoaNaoExisteException ex)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status404NotFound,
                    ex.Message);

                return NotFound(resposta);
            }
            catch (Exception)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status500InternalServerError,
                    "Um erro inesperado aconteceu.");

                return StatusCode(500, resposta);
            }
        }

        [HttpPost]
        public ActionResult Cadastrar([FromBody] PessoaDTO pessoaDTO)
        {
            try
            {
                var pessoa = pessoaService.Cadastrar(pessoaDTO);

                var resposta = new RespostaSucessoAPI<Pessoa>(StatusCodes.Status201Created,
                    $"Pessoa de ID {pessoa.Id} criada com sucesso!", pessoa);

                return Created("", resposta);
            }
            catch (EntradaInvalidaException ex)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status400BadRequest,
                    ex.Message);

                return BadRequest(resposta);
            }
            catch (Exception)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status500InternalServerError,
                    "Um erro inesperado aconteceu.");

                return StatusCode(500, resposta);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Editar(int id, [FromBody] PessoaDTO pessoaDTO)
        {
            try
            {
                var pessoa = pessoaService.EditarPorId(id, pessoaDTO);

                var resposta = new RespostaSucessoAPI<Pessoa>(StatusCodes.Status200OK,
                    $"Pessoa de ID {pessoa.Id} editada com sucesso!", pessoa);

                return Ok(resposta);
            }
            catch (PessoaNaoExisteException ex)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status404NotFound,
                    ex.Message);

                return NotFound(resposta);
            }
            catch (EntradaInvalidaException ex)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status400BadRequest,
                    ex.Message);

                return BadRequest(resposta);
            }
            catch (Exception)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status500InternalServerError,
                    "Um erro inesperado aconteceu.");

                return StatusCode(500, resposta);
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            try
            {
                pessoaService.RemoverPorId(id);

                var resposta = new RespostaSucessoAPI<string?>(StatusCodes.Status200OK,
                    $"Pessoa de ID {id} deletada com sucesso!", null);

                return Ok(resposta);
            }
            catch (PessoaNaoExisteException ex)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status404NotFound,
                    ex.Message);

                return NotFound(resposta);
            }
            catch (Exception)
            {
                var resposta = new RespostaErroAPI(StatusCodes.Status500InternalServerError,
                    "Um erro inesperado aconteceu.");

                return StatusCode(500, resposta);
            }
        }
    }
}
