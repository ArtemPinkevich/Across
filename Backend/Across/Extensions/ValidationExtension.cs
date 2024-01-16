using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UseCases.PipelineBehaviours;

namespace Across.Extensions
{
    public static class ValidationExtension
    {
        public static void AddValidation(this IServiceCollection services, Assembly useCasesAssembly)
        {
            services.AddValidatorsFromAssembly(useCasesAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        }
    }
}
