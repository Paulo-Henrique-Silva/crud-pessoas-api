﻿using ApiTeste.Data;
using ApiTeste.Dtos;
using ApiTeste.Exceptions;
using ApiTeste.Models;
using ApiTeste.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.Services
{
    public class PessoaService
    {
        private readonly PessoaRepository pessoaRepository;

        public PessoaService(PessoaRepository pessoaRepository) 
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

        public Pessoa EditarPorId(int id, PessoaDTO pessoaDTO)
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
