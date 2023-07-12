﻿using ApiTeste.Dtos;
using ApiTeste.Exceptions;
using ApiTeste.Interfaces;
using ApiTeste.Models;
using ApiTeste.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.Services
{
    /// <summary>
    /// Oferece serviços referentes as operações disponíveis para pessoas e ecapsula as regras de negócio.
    /// </summary>
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository) 
        { 
            this.pessoaRepository = pessoaRepository;
        }

        public List<Pessoa> ObterTudo()
        {
            return pessoaRepository.ObterTudoAsync().Result;
        }

        public Pessoa ObterPorId(int id)
        {
            var pessoa = pessoaRepository.ObterPorIdAsync(id).Result;

            return pessoa ?? 
                throw new PessoaNaoExisteException("Não existe uma pessoa com o ID: " + id);
        }

        public Pessoa Cadastrar(PessoaDTO pessoaDTO)
        {
            Pessoa pessoa = ConverterDTO(pessoaDTO);

            Validar(pessoa);

            return pessoaRepository.CadastrarAsync(pessoa).Result;
        }

        public Pessoa Editar(int id, PessoaDTO pessoaDTO)
        {
            if (!ExistePorId(id))
            {
                throw new PessoaNaoExisteException("Não existe uma pessoa com o ID: " + id);
            }

            Pessoa pessoa = ConverterDTO(pessoaDTO);
            pessoa.Id = id; //adds ID to not insert a new record on DB.

            Validar(pessoa);

            return pessoaRepository.EditarAsync(pessoa).Result;
        }

        public void RemoverPorId(int id)
        {
            if (!ExistePorId(id))
            {
                throw new PessoaNaoExisteException("Não existe uma pessoa com o ID: " + id);
            }

            //espera até a operação de remoção estar concluída.
            _ = pessoaRepository.RemoverAsync(ObterPorId(id)); 
        }

        public Pessoa ConverterDTO(PessoaDTO pessoaDTO)
        {
            //Caso a DTO seja null, significa que os dados enviados não conseguem ser convertidos para os tipos primitivos.
            if (pessoaDTO == null)
            {
                throw new EntradaInvalidaException("Não foi possível converter os dados enviados.");
            }

            return new Pessoa(pessoaDTO.Nome, pessoaDTO.Cidade, pessoaDTO.Salario);
        }

        public bool ExistePorId(int id)
        {
            return pessoaRepository.ExistePorIdAsync(id).Result;
        }

        public void Validar(Pessoa pessoa)
        {
            //Validação a partir de data annotations.
            var validacaoContexto = new ValidationContext(pessoa, null, null);
            var validacaoResultados = new List<ValidationResult>();
            bool EValido = Validator.TryValidateObject(pessoa, validacaoContexto, validacaoResultados,
                true);

            if (!EValido)
            {
                //obtém a primeira mensagem de erro ao validar.
                var mensagem = validacaoResultados.Select(vr => vr.ErrorMessage).FirstOrDefault();

                throw new EntradaInvalidaException(mensagem);
            }
        }
    }
}
