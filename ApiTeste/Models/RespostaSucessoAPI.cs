namespace ApiTeste.Models
{
    public class RespostaSucessoAPI<DataType>
    {
        public bool Success { get; } = true;

        public int Code { get; set; }

        public string Message { get; set; }

        public DataType Data { get; set; }

        public RespostaSucessoAPI(int code, string message, DataType data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
