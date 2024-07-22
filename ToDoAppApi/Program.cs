using Microsoft.AspNetCore.Mvc;
using ToDoApp.Domain.Models;
using ToDoApp.Domain.Services.Interfaces;
using ToDoApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TodoServiceProvider(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/GetAppToDos", async (IToDoAppService _toDoAppService) =>
{
    return Results.Ok(await _toDoAppService.GetItemsAsync());
})
    .WithName("GetAppToDos")
    .WithOpenApi();

app.MapGet("/GetAppToDoById/{id}", async (IToDoAppService _toDoAppService, [FromRoute] int id) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Invalid todo id");
    }

    return Results.Ok(await _toDoAppService.GetItemByIdAsync(id));
})
    .WithName("GetAppToDoById")
    .WithOpenApi();

app.MapPost("/CreateToDoItems", async (IToDoAppService _toDoAppService, [FromBody] ToDoItem toDoItem) =>
{
    if (toDoItem == null)
    {
        return Results.BadRequest("Invalid toDoItem objects");
    }

    return Results.Ok(await _toDoAppService.CreateToDoItems(toDoItem));
})
    .WithName("CreateToDoItems")
    .WithOpenApi();


app.MapDelete("/DeleteItemAsync/{id}", async (IToDoAppService _toDoAppService, [FromRoute] int id) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Invalid todo id");
    }

    await _toDoAppService.DeleteItemAsync(id);
    return Results.Ok();
})
    .WithName("DeleteItemAsync")
    .WithOpenApi();

app.MapPut("/UpdateItemAsync/{id}", async (IToDoAppService _toDoAppService, [FromBody] ToDoItem toDoItem, [FromRoute] int id) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Invalid todo id");
    }

    if (toDoItem == null)
    {
        return Results.BadRequest("Invalid toDoItem objects");
    }

    return Results.Ok(await _toDoAppService.UpdateItemAsync(id, toDoItem));
})
    .WithName("UpdateItemAsync")
    .WithOpenApi();

app.Run();
