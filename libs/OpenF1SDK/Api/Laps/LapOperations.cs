// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

namespace OpenF1SDK.Api;

partial interface IOpenF1ApiClient
{
	/// <summary>
	/// Gets the /v1/laps operations.
	/// </summary>
	ILapOperations Laps { get; }
}

partial class OpenF1ApiClient
{
	Lazy<ILapOperations>? _laps;
	public ILapOperations Laps => (_laps ??= Defer<ILapOperations>(
		c => new LapOperations(new("/v1/laps"), c))).Value;
}

/// <summary>
/// Provides operations for operating on the /v1/laps endpoint.
/// </summary>
public partial interface ILapOperations
{
	/// <summary>
	/// Gets all laps for the given driver for the given session.
	/// </summary>
	Task<OpenF1Response<Lap[]>> GetLapsAsync(
		int sessionKey,
		int driverNumber,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the laps for the given driver for the latest/current session.
	/// </summary>
	Task<OpenF1Response<Lap[]>> GetLatestLapsAsync(
		int driverNumber,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the latest/current Lap for the given driver.
	/// </summary>
	Task<OpenF1Response<Lap>> GetLatestLapAsync(
		int driverNumber,
		CancellationToken cancellationToken = default);
}

public partial class LapOperations(PathString path, ApiClient client) : ILapOperations
{
	public async Task<OpenF1Response<Lap[]>> GetLapsAsync(
		int sessionKey,
		int driverNumber,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("session_key", sessionKey)
			.AddParameter("driver_number", driverNumber)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Lap[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<OpenF1Response<Lap[]>> GetLatestLapsAsync(
		int driverNumber,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("session_key", "latest")
			.AddParameter("driver_number", driverNumber)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Lap[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<OpenF1Response<Lap>> GetLatestLapAsync(
		int driverNumber,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("session_key", "latest")
			.AddParameter("driver_number", driverNumber)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		var result = await client.FetchAsync<Lap[]>(request, cancellationToken)
			.ConfigureAwait(false);

		return new OpenF1Response<Lap>(
			request.Method,
			result.RequestUri,
			result.IsSuccess,
			result.StatusCode,
			result.Data.FirstOrDefault(),
			result.Meta,
			result.RateLimiting,
			result.Error);
	}
}
