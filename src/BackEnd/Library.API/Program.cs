using Library.API.Extensions;
using Library.Application.AutoMapper;
using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Library.Infra.Context;
using Library.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(option =>
    new AutoMapper.MapperConfiguration(options =>
    {
        options.AddProfile(new AutoMapping());
    }).CreateMapper());

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(opt => { });

app.UseAuthorization();

app.MapControllers();

app.Run();
