namespace TaskManagement.UI.DTOs
{
    public class ServiceResponseDTO<T>
    {
        public T GenericData { get; set; }

        public bool Sucess { get; set; } = true;

        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}
