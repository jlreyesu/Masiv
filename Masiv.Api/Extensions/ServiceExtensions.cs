using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Masiv.Api.Extensions
{
	public static class ServiceExtensions
    {
		#region Implementación de Swagger
		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "JOrge API", Version = "V1" });
			});
		}
		#endregion
	}
}
