using ApiVentas.Interfaces;

namespace ApiVentas.Utilitarios
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var assembly = typeof(IAssemly<>).Assembly;
            services.Scan(x => x.FromAssemblies(assembly).AddClasses(c => c.AssignableTo(typeof(IAssemly<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}
