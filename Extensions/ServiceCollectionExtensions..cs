using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
        
        .AddScoped<IEmployeeRepository, EmployeeRepository>()
        .AddScoped<IEmployeeService, EmployeeService>();
    
                        
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<EmployeeContext>(options =>
            options.UseMySQL(connectionString));

        return services;
    }
}