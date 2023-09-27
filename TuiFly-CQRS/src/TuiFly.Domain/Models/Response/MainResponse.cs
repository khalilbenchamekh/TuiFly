namespace TuiFly.Domain.Models.Response
{
    public class MainResponse<T>
    {
        public T? Response { get; set; }
        public string? Message = string.Empty;
        public bool Error = false;
    }
}
