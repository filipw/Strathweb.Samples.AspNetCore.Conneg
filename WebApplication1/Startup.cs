using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApiContrib.Core.Formatter.Csv;

namespace WebApplication1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFormatterServices().AddXmlSerializerFormatters().AddJsonFormatters().AddCsvSerializerFormatters();
            //services.AddMvcCore()
            //    .AddXmlSerializerFormatters()
            //    .AddJsonFormatters()
            //    .AddCsvSerializerFormatters();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use((context, next) =>
            {
                var responseModel = new[]
                {
                    new Book
                    {
                        Author = "Eric Zweig",
                        Title = "The Toronto Maple Leafs: The Complete Oral History",
                    },
                    new Book
                    {
                        Author = "Landolf Scherzer",
                        Title = "Buenos dias, Kuba: Reise durch ein Land im Umbruch",
                    }
                };

                //return context.WriteModelAsync(responseModel);

                var (formatter, formatterContext) = context.SelectFormatter(responseModel);
                return formatter.WriteAsync(formatterContext);
            });
        }
    }
}
