using AutoMapper;
using FluentValidation;
using Florama.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Florama.Domain.Interfaces.Services;
using Florama.Domain.Abstractions;
using Florama.Application.Notifications;
using Florama.Domain.Notifications;

namespace Florama.Application
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureServices
    {
        private static readonly Assembly[] AssembliesToScan =
        [
            Assembly.GetExecutingAssembly(),
            typeof(IService).Assembly // assembly do Domain
        ];

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();

            // AppServices
            services.Scan(scan => scan
                .FromAssemblies(AssembliesToScan)
                .AddClasses(c => c.AssignableTo<IAppService>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Domain Services
            services.Scan(scan => scan
                .FromAssemblies(AssembliesToScan)
                .AddClasses(c => c.AssignableTo<IService>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // FluentValidation
            services.Scan(scan => scan
                .FromAssemblies(AssembliesToScan)
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}