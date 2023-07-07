namespace ApiTeste.Models
{
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
