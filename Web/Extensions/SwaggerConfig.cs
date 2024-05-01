using Microsoft.OpenApi.Models;

namespace Web.Extensions
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(d =>
            {
                d.SwaggerDoc("escolas", new OpenApiInfo
                {
                    Title = "Escolas",
                    Version = "v1",
                    Description =
                        "Documentaçao Referente a criação de funcionalidades de gestão de escola",
                });

            });




        }
    }
}
