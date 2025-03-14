// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using System.Diagnostics;
using System.Text.Json.Serialization;

namespace OpenF1SDK.Api;

[DebuggerDisplay("{DriverNumber,nq} {NameAcronym,nq} ({BroadcaseName,nq})")]
public class Driver : Model<Driver>
{
	/// <summary>
	/// The driver's name, as displayed on TV.
	/// </summary>
	[JsonPropertyName("broadcast_name")]
	public required string BroadcastName { get; set; }

	/// <summary>
	/// A code that uniquely identifies the country.
	/// </summary>
	[JsonPropertyName("country_code")]
	public required string CountryCode { get; set; }

	/// <summary>
	/// The unique number assigned to an F1 driver (cf. https://en.wikipedia.org/wiki/List_of_Formula_One_driver_numbers#Formula_One_driver_numbers).
	/// </summary>
	[JsonPropertyName("driver_number")]
	public int DriverNumber { get; set; }

	/// <summary>
	/// The driver's first name.
	/// </summary>
	[JsonPropertyName("first_name")]
	public required string FirstName { get; set; }

	/// <summary>
	/// The driver's full name.
	/// </summary>
	[JsonPropertyName("full_name")]
	public required string FullName { get; set; }

	/// <summary>
	/// URL of the driver's face photo.
	/// </summary>
	[JsonPropertyName("headshot_url")]
	public string? HeadshotUrl { get; set; }

	/// <summary>
	/// The driver's last name.
	/// </summary>
	[JsonPropertyName("last_name")]
	public required string LastName { get; set; }

	/// <summary>
	/// The unique identifier for the meeting.
	/// </summary>
	[JsonPropertyName("meeting_key")]
	public int MeetingKey { get; set; }

	/// <summary>
	/// Three-letter acronym of the driver's name.
	/// </summary>
	[JsonPropertyName("name_acronym")]
	public required string NameAcronym { get; set; }

	/// <summary>
	/// The unique identifier for the session.
	/// </summary>
	[JsonPropertyName("session_key")]
	public int SessionKey { get; set; }

	/// <summary>
	/// The hexadecimal color value (RRGGBB) of the driver's team.
	/// </summary>
	[JsonPropertyName("team_colour")]
	public string? TeamColour { get; set; }

	/// <summary>
	/// Name of the driver's team.
	/// </summary>
	[JsonPropertyName("team_name")]
	public required string TeamName { get; set; }
}
