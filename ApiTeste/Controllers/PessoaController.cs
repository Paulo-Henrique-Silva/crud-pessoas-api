using ApiTeste.Data;
using ApiTeste.Dtos;
using ApiTeste.Exceptions;
using ApiTeste.Interfaces;
using ApiTeste.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    /// <summary>
    /// Mapea as rotas e os métodos HTTP disponíveis para "pessoas".
    /// </summary>
    [Controller]
    [Route("api/v1/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            this.pessoaService = pessoaService;
        }

        [HttpGet]
        public ActionResult ObterTudo([FromQuery] string ordernarPor = "id", 
            [FromQuery] double salarioMinimo = 0, [FromQuery] double salarioMaximo = double.MaxValue)
        {
            try
            {
                List<Pessoa> pessoas = pessoaService.ObterTudo(ordernarPor, salarioMinimo, salarioMaximo);

                var resposta = new RespostaSucessoAPI<List<Pessoa>>(StatusCodes.Status200OK, 
                    "Lista de pessoas obtida com sucesso!", pessoas);

                return Ok(resposta);
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

        [HttpGet("{id}")]
        public ActionResult ObterPorId([FromRoute] int id)
        {
            try
            {
                Pessoa pessoa = pessoaService.ObterPorId(id);

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
                Pessoa pessoa = pessoaService.Cadastrar(pessoaDTO);

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
        public ActionResult EditarPorId([FromRoute] int id, [FromBody] PessoaDTO pessoaDTO)
        {
            try
            {
                Pessoa pessoa = pessoaService.Editar(id, pessoaDTO);

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
        public ActionResult Remover([FromRoute] int id)
        {
            try
            {
                pessoaService.RemoverPorId(id);

                var resposta = new RespostaSucessoAPI(StatusCodes.Status200OK, 
                    $"Pessoa de ID {id} deletada com sucesso!");

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
