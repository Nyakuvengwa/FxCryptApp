using System.Reflection;
using AutoMapper;
using FxCryptApp.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace FxCryptApp.Common
{
	public static class FxCryptAppCommonExtensions
	{
		public static void RegisterFxCryptAppCommon(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
		}
	}
}
