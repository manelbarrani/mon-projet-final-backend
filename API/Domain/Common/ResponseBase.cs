namespace Domain.Common
{
    public class ResponseBase<T> where T : class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
    }
}
