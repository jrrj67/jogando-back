using System.Collections.Generic;

namespace JogandoBack.API.Data.Models.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; }

        public Response(T data, string message = null, ICollection<string> errors = null)
        {
            Data = data;
            Message = message;
            Errors = errors;
        }
    }
}
