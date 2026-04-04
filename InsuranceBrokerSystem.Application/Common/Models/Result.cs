namespace InsuranceBrokerSystem.Application.Common.Models
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public T? Data { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();
        public string? Message { get; set; }

        public static Result<T> Success(T data, string? message = null)
        {
            return new Result<T> { Succeeded = true, Data = data, Message = message };
        }

        public static Result<T> Failure(IEnumerable<string> errors, string? message = null)
        {
            return new Result<T> { Succeeded = false, Errors = errors as string[] ?? new List<string>(errors).ToArray(), Message = message };
        }

        public static Result<T> Failure(string error, string? message = null)
        {
            return new Result<T> { Succeeded = false, Errors = new[] { error }, Message = message };
        }
    }
}
