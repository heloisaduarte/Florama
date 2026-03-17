using Florama.Application.AutoMapper;
using Florama.Application.Interfaces;
using FluentValidation;
using System.Reflection;

namespace Florama.Api.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var presentationAssembly = Assembly.GetExecutingAssembly();
            
            services.Scan(scan => scan
                .FromAssemblies(presentationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddHttpContextAccessor();
            services.AddScoped<IUsuarioLogado, UsuarioLogado>();
            return services;
        }
    }
}