namespace PublicApi.EndpointExtensions
{
    public static class IEndpointExtension
    {
        /// <summary>
        /// 获取所有实现了IEndpoint接口的类，并将它们注册到依赖注入容器中，以便在应用程序中使用这些端点。
        /// 不包含接口本身。
        /// </summary>
        /// <param name="services"></param>
        public static void AddEndpoint(this IServiceCollection services)
        {
            var endpoints = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IEndpoint)))
                .Where(t => !t.IsInterface);

            foreach (var endpoint in endpoints)
            {
                services.AddScoped(typeof(IEndpoint), endpoint);
            }
        }
        /// <summary>
        ///  获取所有实现了IEndpoint接口的类，并将它们注册到应用程序的路由中，以便在应用程序中使用这些端点。
        /// </summary>
        /// <param name="app"></param>
        public static void MapEndpoints(this WebApplication app)
        {
            var scope=app.Services.CreateScope();
            var endpoints = scope.ServiceProvider.GetServices<IEndpoint>();
            foreach (var endpoint in endpoints)
            {
                endpoint.AddRoute(app);
            }
        }
    }
}
