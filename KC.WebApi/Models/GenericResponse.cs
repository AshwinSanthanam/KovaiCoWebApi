namespace KC.WebApi.Models
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }
    }
}
