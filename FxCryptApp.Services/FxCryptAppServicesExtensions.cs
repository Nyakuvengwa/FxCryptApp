using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FxCryptApp.Services
{
	public static class FxCryptAppServicesExtensions
	{
		public static void RegisterFxCryptAppServices(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
		}
	}
}