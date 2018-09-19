using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace WebApplication1
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcCoreBuilder AddFormatterServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpRequestStreamReaderFactory, MemoryPoolHttpRequestStreamReaderFactory>();
            services.TryAddSingleton<IHttpResponseStreamWriterFactory, MemoryPoolHttpResponseStreamWriterFactory>();
            services.TryAddSingleton(ArrayPool<byte>.Shared);
            services.TryAddSingleton(ArrayPool<char>.Shared);
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcCoreMvcOptionsSetup>());
            services.TryAddSingleton<OutputFormatterSelector, DefaultOutputFormatterSelector>();

            return new MvcCoreBuilder(services, new ApplicationPartManager());
        }
    }
}
