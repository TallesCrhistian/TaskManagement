namespace TaskManagement.UI.DTOs
{
    public class ListResponseDTO<T>
    {
        public int TotalPages { get; set; }

        public List<T> Data { get; set; }
    }
}
