// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

namespace OpenF1SDK.Api;

partial interface IOpenF1ApiClient
{
	/// <summary>
	/// Gets the /v1/meetings operations.
	/// </summary>
	IMeetingOperations Meetings { get; }
}

partial class OpenF1ApiClient
{
	Lazy<IMeetingOperations>? _meetings;
	public IMeetingOperations Meetings => (_meetings ??= Defer<IMeetingOperations>(
		c => new MeetingOperations(new("/v1/meetings"), c))).Value;
}

/// <summary>
/// Provides operations for operating on the /v1/meetings endpoint.
/// </summary>
public partial interface IMeetingOperations
{
	/// <summary>
	/// Gets meetings that match the specified criteria.
	/// </summary>
	Task<OpenF1Response<Meeting[]>> GetMeetingsAsync(
		int? circuitKey = null,
		string? circuitShortName = default,
		string? countryCode = default,
		int? countryKey = null,
		string? countryName = default,
		DateTimeOffset? dateStart = null,
		TimeSpan? gmtOffset = null,
		string? location = null,
		int? meetingKey = null,
		string? meetingName = null,
		string? meetingOfficialName = null,
		int? year = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the latest/current meeting.
	/// </summary>
	Task<OpenF1Response<Meeting>> GetLatestMeetingAsync(CancellationToken cancellationToken = default);
}

public partial class MeetingOperations(PathString path, ApiClient client) : IMeetingOperations
{
	public async Task<OpenF1Response<Meeting[]>> GetMeetingsAsync(
		int? circuitKey = null,
		string? circuitShortName = default,
		string? countryCode = default,
		int? countryKey = null,
		string? countryName = default,
		DateTimeOffset? dateStart = null,
		TimeSpan? gmtOffset = null,
		string? location = null,
		int? meetingKey = null,
		string? meetingName = null,
		string? meetingOfficialName = null,
		int? year = null,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("circuit_key", circuitKey)
			.AddParameter("circuit_short_name", circuitShortName)
			.AddParameter("country_code", countryCode)
			.AddParameter("country_key", countryKey)
			.AddParameter("country_name", countryName)
			.AddParameter("date_start", dateStart)
			.AddParameter("gmt_offset", gmtOffset)
			.AddParameter("location", location)
			.AddParameter("meeting_key", meetingKey)
			.AddParameter("meeting_name", meetingName)
			.AddParameter("meeting_official_name", meetingOfficialName)
			.AddParameter("year", year)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Meeting[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<OpenF1Response<Meeting>> GetLatestMeetingAsync(
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("meeting_key", "latest")
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		var result = await client.FetchAsync<Meeting[]>(request, cancellationToken)
			.ConfigureAwait(false);

		return new OpenF1Response<Meeting>(
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
