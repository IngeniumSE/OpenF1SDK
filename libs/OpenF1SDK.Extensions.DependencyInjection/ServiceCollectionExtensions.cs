// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using System.Net.Http.Headers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using OpenF1SDK;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extensions for the <see cref="IServiceCollection"/> type.
/// </summary>
public static class ServiceCollectionExtensions
{
	const string DefaultApiClientName = "OpenF1";

	/// <summary>
	/// Adds Sailthru services to the given services collection.
	/// </summary>
	/// <param name="services">The services collection.</param>
	/// <param name="configure">The configure delegate.</param>
	/// <returns>The services collection.</returns>
	public static IServiceCollection AddOpenF1(
		this IServiceCollection services,
		Action<OpenF1Settings> configure)
	{
		Ensure.IsNotNull(services, nameof(services));
		Ensure.IsNotNull(configure, nameof(configure));

		services.Configure(configure);

		AddCoreServices(services);

		return services;
	}

	/// <summary>
	/// Adds Sailthru services to the given services collection.
	/// </summary>
	/// <param name="services">The services collection.</param>
	/// <param name="settings">The Sailthru settings.</param>
	/// <returns>The services collection.</returns>
	public static IServiceCollection AddOpenF1(
		this IServiceCollection services,
		OpenF1Settings settings)
	{
		Ensure.IsNotNull(services, nameof(services));
		Ensure.IsNotNull(settings, nameof(settings));

		services.AddSingleton(settings.AsOptions());

		AddCoreServices(services);

		return services;
	}

	/// <summary>
	/// Adds Sailthru services to the given services collection.
	/// </summary>
	/// <param name="services">The services collection.</param>
	/// <param name="configuration">The configuration.</param>
	/// <returns>The services collection.</returns>
	public static IServiceCollection AddOpenF1(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		Ensure.IsNotNull(services, nameof(services));
		Ensure.IsNotNull(configuration, nameof(configuration));

		services.Configure<OpenF1Settings>(
			configuration.GetSection(OpenF1Settings.ConfigurationSection));

		AddCoreServices(services);

		return services;
	}

	static void AddCoreServices(IServiceCollection services)
	{
		services.AddSingleton(sp =>
		{
			var settings = sp.GetRequiredService<IOptions<OpenF1Settings>>().Value;

			settings.Validate();

			return settings;
		});

		services.AddScoped<IOpenF1HttpClientFactory, OpenF1HttpClientFactory>();
		services.AddScoped<IOpenF1ApiClientFactory, OpenF1ApiClientFactory>();
		AddApiClient(
			services,
			DefaultApiClientName,
			(cf, settings) => cf.CreateApiClient(settings, DefaultApiClientName));
	}

	static void AddApiClient<TClient>(
		IServiceCollection services,
		string name,
		Func<IOpenF1ApiClientFactory, OpenF1Settings, TClient> factory)
		where TClient : class
	{
		void ConfigureHttpDefaults(HttpClient http)
		{
			http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		services.AddHttpClient(name, ConfigureHttpDefaults);

		services.AddScoped(sp =>
		{
			var settings = sp.GetRequiredService<OpenF1Settings>();
			var clientFactory = sp.GetRequiredService<IOpenF1ApiClientFactory>();

			return factory(clientFactory, settings);
		});
	}
}
