namespace ApiTeste.Exceptions
{
    /// <summary>
    /// Exceção gerada caso não exista o registro da pessoa selecionada.
    /// </summary>
    public class PessoaNaoExisteException : Exception
    {
        public PessoaNaoExisteException(string? message) : base(message) { }
    }
}
