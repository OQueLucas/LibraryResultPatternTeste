using Library.API.Extensions;
using Library.Application.Interfaces;
using Library.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetAll()
    {
        var result = await bookService.GetAllAsync();

        if (result.IsFailure)
            return result.ToProblemDetails();

        return Results.Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetById(Guid id)
    {
        var result = await bookService.GetByIdAsync(id);

        if (result.IsFailure)
            return result.ToProblemDetails();

        return Results.Ok(result.Value);
    }

    [HttpGet("isbn/{isbn}")]
    public async Task<IResult> GetByIsbn(string isbn)
    {
        var result = await bookService.GetByIsbnAsync(isbn);

        if (result.IsFailure)
            return result.ToProblemDetails();

        return Results.Ok(result.Value);
    }

    [HttpPost]
    public async Task<IResult> Add(RequestBook request)
    {
        var result = await bookService.AddAsync(request);

        if (result.IsFailure)
            return result.ToProblemDetails();

        return Results.Created(nameof(GetById), new { id = request.Id, request });
    }

    [HttpPut("{id}")]
    public async Task<IResult> Update(Guid id, RequestBook request)
    {
        var result = await bookService.UpdateAsync(id, request);

        if (result.IsFailure)
            return result.ToProblemDetails();

        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        await bookService.DeleteAsync(id);
        return Results.NoContent();
    }
}
