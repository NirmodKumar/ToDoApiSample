using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Domain.Models;

public class ToDoItem
{
    /// <summary>
    /// Converts to doid.
    /// </summary>
    /// <value>
    /// To do identifier.
    /// </value>
    [Key]
    public int ToDoId { get; set; }

    /// <summary>
    /// Converts to dotitle.
    /// </summary>
    /// <value>
    /// To do title.
    /// </value>
    public string ToDoTitle { get; set; } = string.Empty;

    /// <summary>
    /// Converts to dodescription.
    /// </summary>
    /// <value>
    /// To do description.
    /// </value>
    public string ToDoDescription { get; set; } = string.Empty;

}
