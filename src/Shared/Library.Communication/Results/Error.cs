namespace Library.Communication.Results;

public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("Error.NullValue", "Null Value was providad", ErrorType.Failure);

    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Descriptions = [description];
        Type = type;
    }

    public Error(string code, List<string> descriptions, ErrorType type)
    {
        Code = code;
        Descriptions = descriptions;
        Type = type;
    }

    public string Code { get; }
    public List<string> Descriptions { get; }
    public ErrorType Type { get; }

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    public static Error Validation(string code, List<string> descriptions) =>
        new(code, descriptions, ErrorType.Validation);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);
}
