using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoApp.Data;
using ToDoApp.Domain.Models;
using ToDoApp.Domain.Services.Interfaces;

namespace ToDoApp.Service.Services;

/// <summary>
/// ToDoAppService
/// </summary>
/// <seealso cref="ToDoApp.Domain.Services.Interfaces.IToDoAppService" />
public class ToDoAppService(ILogger<ToDoAppService> _logger, ToDoDbContext _toDoDbContext) : IToDoAppService
{
    /// <summary>
    /// Creates to do items.
    /// </summary>
    /// <param name="toDoItem">To do item.</param>
    /// <returns></returns>
    public async Task<ToDoItem> CreateToDoItems(ToDoItem toDoItem)
    {
        _logger.BeginScope("CreateToDoItems");
        if (toDoItem == null)
        {
            return default!;
        }

        _toDoDbContext.ToDoItems.Add(toDoItem);
        await _toDoDbContext.SaveChangesAsync();

        return toDoItem;
    }

    /// <summary>
    /// Deletes the item asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <exception cref="System.Exception">Item not Found!</exception>
    public async Task DeleteItemAsync(int id)
    {
        _logger.BeginScope("DeleteItemAsync");
        var todoItem = await _toDoDbContext.ToDoItems.FindAsync(id);

        if (todoItem == null)
        {
            throw new Exception("Item not Found!");
        }

        _toDoDbContext.ToDoItems.Remove(todoItem);

        await _toDoDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Gets the item by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public async Task<ToDoItem> GetItemByIdAsync(int id)
    {
        _logger.BeginScope("GetItemByIdAsync");
        return await _toDoDbContext.ToDoItems.FindAsync(id);
    }

    /// <summary>
    /// Gets the item by to do filters asynchronous.
    /// </summary>
    /// <param name="toDoItem">To do item.</param>
    /// <returns></returns>
    public async Task<List<ToDoItem>> GetItemByToDoFiltersAsync(ToDoItem toDoItem)
    {
        _logger.BeginScope("GetItemByToDoFiltersAsync");
        var filters = new List<ToDoItem>();

        if (toDoItem != null && !string.IsNullOrWhiteSpace(toDoItem.ToDoTitle))
        {
            var titleFiltered = await _toDoDbContext.ToDoItems
                .Where(x => x.ToDoTitle.Contains(x.ToDoTitle, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();

            if (titleFiltered != null && titleFiltered.Any())
            {
                filters.AddRange(titleFiltered);
            }

        }

        if (toDoItem != null && !string.IsNullOrWhiteSpace(toDoItem.ToDoDescription))
        {
            var descFiltered = await _toDoDbContext.ToDoItems
            .Where(x => x.ToDoTitle.Contains(x.ToDoDescription, StringComparison.InvariantCultureIgnoreCase))
            .ToListAsync();

            if (descFiltered != null && descFiltered.Any())
            {
                filters.AddRange(descFiltered);
            }
        }


        return filters.Distinct().ToList();
    }

    /// <summary>
    /// Gets the items asynchronous.
    /// </summary>
    /// <returns></returns>
    public async Task<List<ToDoItem>> GetItemsAsync()
    {
        _logger.BeginScope("GetItemsAsync");
        return await _toDoDbContext.ToDoItems.ToListAsync();
    }

    /// <summary>
    /// Updates the item asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="toDoItem">To do item.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">Item not Found!</exception>
    public async Task<ToDoItem> UpdateItemAsync(int id, ToDoItem toDoItem)
    {
        _logger.BeginScope("UpdateItemAsync");
        var todoItemDb = await _toDoDbContext.ToDoItems.FindAsync(id);

        if (todoItemDb == null)
        {
            throw new Exception("Item not Found!");
        }

        todoItemDb.ToDoTitle = toDoItem.ToDoTitle;
        todoItemDb.ToDoDescription = toDoItem.ToDoDescription;

        _toDoDbContext.ToDoItems.Update(todoItemDb);
        await _toDoDbContext.SaveChangesAsync();

        return todoItemDb;
    }
}
