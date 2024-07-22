using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Data;
using ToDoApp.Domain.Services.Interfaces;
using ToDoApp.Service.Services;

namespace ToDoApp.Service;

public static class ToDoAppServiceProvider
{
    /// <summary>
    /// Todoes the service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="configuration">The configuration.</param>
    public static void TodoServiceProvider(this IServiceCollection serviceProvider, IConfiguration configuration)
    {
        serviceProvider.AddDbContext<ToDoDbContext>(options => options.UseInMemoryDatabase("ToDoAppDb"));
        serviceProvider.AddScoped<ToDoDbContext>();
        serviceProvider.AddScoped<IToDoAppService, ToDoAppService>();

    }
}
