using Library.API.Extensions;
using Library.Application.Interfaces;
using Library.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetAll()
    {
        var result = await categoryService.GetAllAsync();

        if (!result.IsSuccess)
            return result.ToProblemDetails();

        return Results.Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetById(Guid id)
    {
        var result = await categoryService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return result.ToProblemDetails();

        return Results.Ok(result.Value);
    }

    [HttpPost]
    public async Task<IResult> Add(RequestCategory request)
    {
        var result = await categoryService.AddAsync(request);

        if (!result.IsSuccess)
            return result.ToProblemDetails();

        return Results.Created(nameof(GetById), new { id = request.Id, request });
    }

    [HttpPut("{id}")]
    public async Task<IResult> Update(Guid id, RequestCategory request)
    {
        var result = await categoryService.UpdateAsync(id, request);

        if (!result.IsSuccess)
            return result.ToProblemDetails();

        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        await categoryService.DeleteAsync(id);
        return Results.NoContent();
    }
}
