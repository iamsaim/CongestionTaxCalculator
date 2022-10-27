namespace CongestionTaxCalculator.Application.Common.Dto.Response
{
    public class GenericResponse<T>
    {
        public GenericResponse()
        {
        }
        public GenericResponse(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public GenericResponse(T data, string message = null, bool isSucceeded = true)
        {
            Succeeded = isSucceeded;
            Message = message;
            Data = data;
        }
        public GenericResponse(string message)
        {
            Succeeded = true;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}