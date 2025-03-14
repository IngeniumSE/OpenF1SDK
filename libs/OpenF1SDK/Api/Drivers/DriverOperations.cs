// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

namespace OpenF1SDK.Api;

partial interface IOpenF1ApiClient
{
	/// <summary>
	/// Gets the /v1/drivers operations.
	/// </summary>
	IDriverOperations Drivers { get; }
}

partial class  OpenF1ApiClient
{
	Lazy<IDriverOperations>? _drivers;
	public IDriverOperations Drivers => (_drivers ??= Defer<IDriverOperations>(
		c => new DriverOperations(new("/v1/drivers"), c))).Value;
}

/// <summary>
/// Provides operations for operating on the /v1/drivers endpoint.
/// </summary>
public partial interface IDriverOperations
{
	/// <summary>
	/// Gets the drivers that match the specified criteria.
	/// </summary>
	/// <returns></returns>
	Task<OpenF1Response<Driver[]>> GetDriversAsync(
		string? broadcastName = null,
		string? countryCode = null,
		int? driverNumber = null,
		string? driverFirstName = null,
		string? driverFullName = null,
		string? driverLastName = null,
		int? meetingKey = null,
		string? nameAcroynom = null,
		int? sessionKey = null,
		string? teamName = null,
		CancellationToken cancellationToken = default);
}

public partial class DriverOperations(PathString path, ApiClient client) : IDriverOperations
{
	public async Task<OpenF1Response<Driver[]>> GetDriversAsync(
		string? broadcastName = null,
		string? countryCode = null,
		int? driverNumber = null,
		string? firstName = null,
		string? fullName = null,
		string? lastName = null,
		int? meetingKey = null,
		string? nameAcroynom = null,
		int? sessionKey = null,
		string? teamName = null,
		CancellationToken cancellationToken = default)
	{
		var query = new QueryStringBuilder()
			.AddParameter("broadcast_name", broadcastName)
			.AddParameter("country_code", countryCode)
			.AddParameter("driver_number", driverNumber)
			.AddParameter("first_name", firstName)
			.AddParameter("full_name", fullName)
			.AddParameter("last_name", lastName)
			.AddParameter("meeting_key", meetingKey)
			.AddParameter("name_acronym", nameAcroynom)
			.AddParameter("session_key", sessionKey)
			.AddParameter("team_name", teamName)
			.Build();

		var request = new OpenF1Request(HttpMethod.Get, path, query);

		return await client.FetchAsync<Driver[]>(request, cancellationToken)
			.ConfigureAwait(false);
	}
}
