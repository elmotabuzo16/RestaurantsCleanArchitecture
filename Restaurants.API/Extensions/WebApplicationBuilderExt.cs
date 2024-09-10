using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExt
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers();

            /// Swagger
            builder.Services.AddSwaggerGen(config =>
            {
                // Adds the Authorize button from Swagger
                config.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                // This will add header Authorization Header from responses in Swagger
                // You can test it when executing the [HttpGet] Restaurant
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        []
                    }
                });
            });

            builder.Services.AddEndpointsApiExplorer();

            /// Global Exception Handling
            builder.Services.AddScoped<ErrorHandlingMiddleware>();

        }
    }
}
