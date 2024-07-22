using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Models;

namespace ToDoApp.Data;

public class ToDoDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ToDoItem> ToDoItems { get; set; }
}
