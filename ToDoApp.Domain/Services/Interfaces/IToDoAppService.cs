using ToDoApp.Domain.Models;

namespace ToDoApp.Domain.Services.Interfaces;

public interface IToDoAppService
{
    /// <summary>
    /// Gets the items asynchronous.
    /// </summary>
    /// <returns></returns>
    Task<List<ToDoItem>> GetItemsAsync();

    /// <summary>
    /// Gets the item by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<ToDoItem> GetItemByIdAsync(int id);

    /// <summary>
    /// Creates to do items.
    /// </summary>
    /// <param name="toDoItem">To do item.</param>
    /// <returns></returns>
    Task<ToDoItem> CreateToDoItems(ToDoItem toDoItem);

    /// <summary>
    /// Deletes the item asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task DeleteItemAsync(int id);

    /// <summary>
    /// Updates the item asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="toDoItem">To do item.</param>
    /// <returns></returns>
    Task<ToDoItem> UpdateItemAsync(int id, ToDoItem toDoItem);
}
