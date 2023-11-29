using core.Entities.Core;
using database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace database.Config;

public static class ServiceExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        IEnumerable<Type> entities = typeof(Entity).Assembly.GetTypes().Where(t =>
            t.IsSubclassOf(typeof(Entity)) && t.Namespace != "core.Entities.Core");

        IEnumerable<Type> repositories = typeof(Repository<>).Assembly.GetTypes().Where(t =>
            t.BaseType is { IsGenericType: true } && t.BaseType.GetGenericTypeDefinition() == typeof(Repository<>));

        IEnumerable<Type> entitiesWithoutRepository = entities.Where(x => !repositories.Any(repo =>
            repo.BaseType!.GetGenericArguments().Any(gTp => gTp == x)));

        foreach (Type entity in entitiesWithoutRepository)
        {
            services.AddScoped(typeof(IRepository<>).MakeGenericType(entity),
                typeof(Repository<>).MakeGenericType(entity));
        }

        foreach (Type repository in repositories)
            services.AddScoped(repository.GetInterfaces().FirstOrDefault(d => !d.IsGenericType) ?? repository,
                repository);
    }

    public static void ConfigureContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }
}