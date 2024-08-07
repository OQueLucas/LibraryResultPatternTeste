namespace Library.Communication.Results;

public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("Error.NullValue", "Null Value was providad", ErrorType.Failure);

    public Error(string code, string error, ErrorType type)
    {
        Code = code;
        Errors = [error];
        Type = type;
    }

    public Error(string code, List<string> errors, ErrorType type)
    {
        Code = code;
        Errors = errors;
        Type = type;
    }

    public string Code { get; }
    public List<string> Errors { get; }
    public ErrorType Type { get; }

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    public static Error Validation(string code, List<string> errors) =>
        new(code, errors, ErrorType.Validation);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);
}
