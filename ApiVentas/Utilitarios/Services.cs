namespace ApiVentas.Utilitarios
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var assembly = typeof(IServices<>).Assembly;
            services.Scan((x) => x.FromAssemblies(assembly).AddClasses((claseService) => claseService.AssignableTo(typeof(IServices<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            return services;
        }

    }
}
