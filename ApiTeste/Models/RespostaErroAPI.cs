namespace ApiTeste.Models
{
    /// <summary>
    /// Encapsula dados das respostas mal sucedidas da API.
    /// </summary>
    public class RespostaErroAPI
    {
        public bool Success { get; } = false;

        public int Code { get; set; }

        public string Message { get; set; }

        public RespostaErroAPI(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
