namespace Application.DTOs
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string? Message { get; set; }
        public DateTime Date { get; } = DateTime.Now;

        public T? Data { get; set; }

    }
}
