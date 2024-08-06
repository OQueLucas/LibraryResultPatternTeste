using Library.Communication.Errors;

namespace Library.Communication.MessagesError;

public class CategoryErrors
{
    public static Error NotFound(Guid id) => Error.NotFound("Categories.NotFound", $"The category with the ISBN = '{id}' was not found");
    public static Error DifferentId(Guid id, Guid requestId) => Error.Validation("Categories.Validation", $"The category {id} is different from {requestId}");
}
