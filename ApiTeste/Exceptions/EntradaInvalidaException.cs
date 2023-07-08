namespace ApiTeste.Exceptions
{
    /// <summary>
    /// Exceção gerada caso os dados enviados pelo cliente não sejam válidos.
    /// </summary>
    public class EntradaInvalidaException : Exception
    {
        public EntradaInvalidaException(string? message) : base(message) { }
    }
}
