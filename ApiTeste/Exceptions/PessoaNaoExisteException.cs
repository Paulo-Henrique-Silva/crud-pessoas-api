namespace ApiTeste.Exceptions
{
    public class PessoaNaoExisteException : Exception
    {
        public PessoaNaoExisteException(string? message) : base(message) { }
    }
}
