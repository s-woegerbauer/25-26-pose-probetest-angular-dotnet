using Microsoft.AspNetCore.Mvc;

using WebAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
var app = builder.Build();
app.UseCors("AllowAngular");
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/api/books/titleExists", async ([FromQuery]string title, [FromServices]IBookService service) =>
{
    try
    {
        return Results.Ok(await service.TitleExistsAsync(title));
    }
    catch (Exception ex) when (ex is ArgumentOutOfRangeException or ArgumentException)
    {
        return Results.BadRequest();
    }
});

app.MapGet("/api/books", async ([FromServices]IBookService service) =>
{
    try
    {
        return Results.Ok(await service.GetAllBooksAsync());
    }
    catch (Exception ex) when (ex is ArgumentOutOfRangeException or ArgumentException)
    {
        return Results.BadRequest();
    }
});

app.MapGet("/api/books/{id}", async (int id, [FromServices]IBookService service) =>
{
    try
    {
        var book = await service.GetBookByIdAsync(id);
        return book != null ? Results.Ok(book) : Results.NotFound();
    }
    catch (Exception ex) when (ex is ArgumentOutOfRangeException or ArgumentException)
    {
        return Results.BadRequest();
    }
});

app.MapPost("/api/books", async ([FromBody]BookDto book, [FromServices]IBookService service) =>
{
    try
    {
        if (book.Price < 0)
        {
            return Results.BadRequest("The price cannot be negative");
        }
        
        var created = await service.CreateBookAsync(book);
        return Results.Created($"/api/books/{created.Id}", created);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/api/books/{id}", async (int id, [FromBody]BookDto book, [FromServices]IBookService service) =>
{
    try
    {
        if (book.Price < 0)
        {
            return Results.BadRequest("The price cannot be negative");
        }
        
        var updated = await service.UpdateBookAsync(id, book);
        return updated != null ? Results.Ok(updated) : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/api/books/{id}", async (int id, [FromServices]IBookService service) =>
{
    try
    {
        var deleted = await service.DeleteBookAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();