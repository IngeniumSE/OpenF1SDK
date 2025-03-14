// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

namespace OpenF1SDK.Api;

partial interface IOpenF1ApiClient
{
	/// <summary>
	/// Gets the /v1/sessions operations.
	/// </summary>
	ISessionOperations Sessions { get; }
}

partial class  OpenF1ApiClient
{
	Lazy<ISessionOperations>? _sessions;
	public ISessionOperations Sessions => (_sessions ??= Defer<ISessionOperations>(
		c => new SessionOperations(new("/v1/sessions"), c))).Value;
}

/// <summary>
/// Provides operations for operating on the /v1/sessions endpoint.
/// </summary>
public partial interface ISessionOperations
{
	/// <summary>
	/// Gets sessions that match the specified criteria.
	/// </summary>
	Task<OpenF1Response<Session[]>> GetSessionsAsync(
		int? circuitKey = null,
		string? circuitShortName = default,
		string? countryCode = default,
		int? countryKey = null,
		string? countryName = default,
		DateTimeOffset? dateEnd = null,
		DateTimeOffset? dateStart = null,
		TimeSpan? gmtOffset = null,
		string? location = null,
		int? meetingKey = null,
		int? sessionKey = null,
		string? sessionName = null,
		string? sessionType = null,
		int? year = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the sessions that match the specified criteria, for the latest/current meeting.
	/// </summary>
	Task<OpenF1Response<Session[]>> GetLatestSessionsAsync(
		int? circuitKey = null,
		string? circuitShortName = default,
		string? countryCode = default,
		int? countryKey = null,
		string? countryName = default,
		DateTimeOffset? dateEnd = null,
		DateTimeOffset? dateStart = null,
		TimeSpan? gmtOffset = null,
		string? location = null,
		int? sessionKey = null,
		string? sessionName = null,
		string? sessionType = null,
		int? year = null,
		CancellationToken cancellationToken = default);
}

public partial class SessionOperations(PathString path, ApiClient client) : ISessionOperations
{
	public async Task<OpenF1Response<Session[]>> GetSessionsAsync(
		int? circuitKey = null,
		string? circuitShortName = default,
		string? countryCode = default,
		int? countryKey = null,
		string? countryName = default,
		DateTimeOffset? dateEnd = null,
		DateTimeOffset? dateStart = null,
		TimeSpan? gmtOffset = null,
		string? location = null,
		int? meetingKey = null,
		int? sessionKey = null,
		string? sessionName = null,
		string? sessionType = null,
		int? year = null,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("circuit_key", circuitKey)
			.AddParameter("circuit_short_name", circuitShortName)
			.AddParameter("country_code", countryCode)
			.AddParameter("country_key", countryKey)
			.AddParameter("country_name", countryName)
			.AddParameter("date_end", dateEnd)
			.AddParameter("date_start", dateStart)
			.AddParameter("gmt_offset", gmtOffset)
			.AddParameter("location", location)
			.AddParameter("meeting_key", meetingKey)
			.AddParameter("session_key", sessionKey)
			.AddParameter("session_name", sessionName)
			.AddParameter("session_type", sessionType)
			.AddParameter("year", year)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Session[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<OpenF1Response<Session[]>> GetLatestSessionsAsync(
		int? circuitKey = null,
		string? circuitShortName = default,
		string? countryCode = default,
		int? countryKey = null,
		string? countryName = default,
		DateTimeOffset? dateEnd = null,
		DateTimeOffset? dateStart = null,
		TimeSpan? gmtOffset = null,
		string? location = null,
		int? sessionKey = null,
		string? sessionName = null,
		string? sessionType = null,
		int? year = null,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("circuit_key", circuitKey)
			.AddParameter("circuit_short_name", circuitShortName)
			.AddParameter("country_code", countryCode)
			.AddParameter("country_key", countryKey)
			.AddParameter("country_name", countryName)
			.AddParameter("date_end", dateEnd)
			.AddParameter("date_start", dateStart)
			.AddParameter("gmt_offset", gmtOffset)
			.AddParameter("location", location)
			.AddParameter("meeting_key", "latest")
			.AddParameter("session_key", sessionKey)
			.AddParameter("session_name", sessionName)
			.AddParameter("session_type", sessionType)
			.AddParameter("year", year)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Session[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}
}
