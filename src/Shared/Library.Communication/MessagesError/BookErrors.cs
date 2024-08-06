using Library.Communication.Errors;

namespace Library.Communication.MessagesError;

public class BookErrors
{
    public static Error NotFound(Guid id) => Error.NotFound("Books.NotFound", $"The book with the ISBN = '{id}' was not found");
    public static Error DifferentId(Guid id, Guid requestId) => Error.Validation("Books.Validation", $"The book {id} is different from {requestId}");
    public static Error NotFoundByISBN(string isbn) => Error.NotFound("Books.NotFoundByISBN", $"The book with the ISBN = '{isbn}' was not found");
    public static Error IsbnNotUnique => Error.Conflict("Books.IsbnNotUnique", $"The provided isbn is not unique");
}
