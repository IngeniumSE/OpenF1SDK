// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using System.Diagnostics;
using System.Text.Json.Serialization;

namespace OpenF1SDK.Api;

[DebuggerDisplay("{MeetingKey,nq} - {MeetingName,nq} {Year,nq}")]
public partial class  Meeting : Model<Meeting>
{
	/// <summary>
	/// The unique identifier for the circuit where the event takes place.
	/// </summary>
	[JsonPropertyName("circuit_key")]
	public int CircuitKey { get; set; }

	/// <summary>
	/// The short or common name of the circuit where the event takes place.
	/// </summary>
	[JsonPropertyName("circuit_short_name")]
	public required string CircuitShortName { get; set; }

	/// <summary>
	/// A code that uniquely identifies the country.
	/// </summary>
	[JsonPropertyName("country_code")]
	public required string CountryCode { get; set; }

	/// <summary>
	/// The unique identifier for the country where the event takes place.
	/// </summary>
	[JsonPropertyName("country_key")]
	public int CountryKey { get; set; }

	/// <summary>
	/// The full name of the country where the event takes place.
	/// </summary>
	[JsonPropertyName("country_name")]
	public required string CountryName { get; set; }

	/// <summary>
	/// The UTC starting date and time, in ISO 8601 format.
	/// </summary>
	[JsonPropertyName("date_start")]
	public DateTimeOffset DateStart { get; set; }

	/// <summary>
	/// The difference in hours and minutes between local time at the location of the event and Greenwich Mean Time (GMT).
	/// </summary>
	[JsonPropertyName("gmt_offset")]
	public TimeSpan GmtOffset { get; set; }

	/// <summary>
	/// The city or geographical location where the event takes place.
	/// </summary>
	[JsonPropertyName("location")]
	public required string Location { get; set; }

	/// <summary>
	/// The unique identifier for the meeting.
	/// </summary>
	[JsonPropertyName("meeting_key")]
	public int MeetingKey { get; set; }

	/// <summary>
	///		The name of the meeting. 
	/// </summary>
	[JsonPropertyName("meeting_name")]
	public required string MeetingName { get; set; }

	/// <summary>
	/// The official name of the meeting.
	/// </summary>
	[JsonPropertyName("meeting_official_name")]	
	public string? MeetingOfficialName { get; set; }

	/// <summary>
	/// The year the event takes place.
	/// </summary>
	[JsonPropertyName("year")]
	public int Year { get; set; }
}
