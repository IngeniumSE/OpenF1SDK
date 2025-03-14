// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

namespace OpenF1SDK.Api;

partial interface IOpenF1ApiClient
{
	/// <summary>
	/// Gets the /v1/stints operations.
	/// </summary>
	IStintOperations Stints { get; }
}

partial class OpenF1ApiClient
{
	Lazy<IStintOperations>? _stints;
	public IStintOperations Stints => (_stints ??= Defer<IStintOperations>(
		c => new StintOperations(new("/v1/stints"), c))).Value;
}

/// <summary>
/// Provides operations for operating on the /v1/laps endpoint.
/// </summary>
public partial interface IStintOperations
{
	/// <summary>
	/// Gets all laps for the given driver for the given session.
	/// </summary>
	Task<OpenF1Response<Stint[]>> GetStintsAsync(
		int sessionKey,
		int driverNumber,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the laps for the given driver for the latest/current session.
	/// </summary>
	Task<OpenF1Response<Stint[]>> GetLatestStintsAsync(
		int driverNumber,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the latest/current Stint for the given driver.
	/// </summary>
	Task<OpenF1Response<Stint>> GetLatestStintAsync(
		int driverNumber,
		CancellationToken cancellationToken = default);
}

public partial class StintOperations(PathString path, ApiClient client) : IStintOperations
{
	public async Task<OpenF1Response<Stint[]>> GetStintsAsync(
		int sessionKey,
		int driverNumber,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("session_key", sessionKey)
			.AddParameter("driver_number", driverNumber)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Stint[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<OpenF1Response<Stint[]>> GetLatestStintsAsync(
		int driverNumber,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("session_key", "latest")
			.AddParameter("driver_number", driverNumber)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Stint[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<OpenF1Response<Stint>> GetLatestStintAsync(
		int driverNumber,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("session_key", "latest")
			.AddParameter("driver_number", driverNumber)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		var result = await client.FetchAsync<Stint[]>(request, cancellationToken)
			.ConfigureAwait(false);

		return new OpenF1Response<Stint>(
			request.Method,
			result.RequestUri,
			result.IsSuccess,
			result.StatusCode,
			result.Data.LastOrDefault(),
			result.Meta,
			result.RateLimiting,
			result.Error);
	}
}
