// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using OpenF1SDK.Api;

namespace OpenF1SDK;

public interface IOpenF1ApiClientFactory
{
	IOpenF1ApiClient CreateApiClient(OpenF1Settings settings, string? name = null);
}

/// <summary>
/// Provides factory services for creating Trybe client instances.
/// </summary>
public class OpenF1ApiClientFactory : IOpenF1ApiClientFactory
{
	readonly IOpenF1HttpClientFactory _httpClientFactory;

	public OpenF1ApiClientFactory(IOpenF1HttpClientFactory httpClientFactory)
	{
		_httpClientFactory = Ensure.IsNotNull(httpClientFactory, nameof(httpClientFactory));
	}

	public IOpenF1ApiClient CreateApiClient(OpenF1Settings settings, string? name = null)
		=> new OpenF1ApiClient(_httpClientFactory.CreateHttpClient(name), settings);
}
