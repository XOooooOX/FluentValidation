using Models.ValueObjects;

namespace FluentValidationApp
{
    public class ApiResult
    {
        public object? Result { get; init; }
        public List<Error>? Errors { get; init; }
        public DateTime TimeGenerated { get; init; }

        private ApiResult(object? result, List<Error>? errors)
            => (Result, Errors, TimeGenerated) = (result, errors, DateTime.UtcNow);

        public static ApiResult OK(object? result = null)
            => new(result, null);

        public static ApiResult Error(List<Error>? errors)
            => new(null, errors);

        public static ApiResult Error(Error error)
            => new(null, new List<Error>() { error });
    }
}
