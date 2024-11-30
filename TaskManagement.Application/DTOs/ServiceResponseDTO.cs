using TaskManagement.Utils.Messages;

namespace TaskManagement.Application.DTOs
{
    public class ServiceResponseDTO<T>
    {
        public T GenericData { get; set; }

        public bool Sucess { get; set; } = true;

        public string Message { get; set; } = Messages.OkMessage;

        public int StatusCode { get; set; }
    }
}
