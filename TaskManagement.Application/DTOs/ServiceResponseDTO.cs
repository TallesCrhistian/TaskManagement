using TaskManagement.Application.Messages;

namespace TaskManagement.Application.DTOs
{
    public class ServiceResponseDTO<T>
    {
        public T GenericData { get; set; }

        public bool Sucess { get; set; } = true;

        public string Message { get; set; } = GenericMessages.OkMessage;

        public int StatusCode { get; set; }
    }
}
