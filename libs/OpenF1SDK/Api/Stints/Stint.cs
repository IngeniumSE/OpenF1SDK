// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using System.Diagnostics;
using System.Text.Json.Serialization;

namespace OpenF1SDK.Api;

[DebuggerDisplay("{DriverNumber,nq}/{StintNumber,nq} ({LapStart,nq}-{LapEnd,nq}) on {Compound,nq}")]
public partial class Stint : Model<Stint>
{
	/// <summary>
	/// The specific compound of tyre used during the stint (SOFT, MEDIUM, HARD, ...).
	/// </summary>
	[JsonPropertyName("compound")]
	public required string Compound { get; set; }

	/// <summary>
	/// The unique number assigned to an F1 driver (cf. https://en.wikipedia.org/wiki/List_of_Formula_One_driver_numbers#Formula_One_driver_numbers).
	/// </summary>
	[JsonPropertyName("driver_number")]
	public int DriverNumber { get; set; }

	/// <summary>
	/// Number of the last completed lap in this stint.
	/// </summary>
	[JsonPropertyName("lap_end")]
	public int LapEnd { get; set; }

	/// <summary>
	/// 	Number of the initial lap in this stint (starts at 1).
	/// </summary>
	[JsonPropertyName("lap_start")]
	public int LapStart { get; set; }

	/// <summary>
	/// The unique identifier for the meeting.
	/// </summary>
	[JsonPropertyName("meeting_key")]
	public int MeetingKey { get; set; }

	/// <summary>
	/// The unique identifier for the session.
	/// </summary>
	[JsonPropertyName("session_key")]
	public int SessionKey { get; set; }

	/// <summary>
	/// The sequential number of the stint within the session (starts at 1).
	/// </summary>
	[JsonPropertyName("stint_number")]
	public int StintNumber { get; set; }

	/// <summary>
	/// The age of the tyres at the start of the stint, in laps completed.
	/// </summary>
	[JsonPropertyName("tyre_age_at_start")]
	public int TyreAgeAtStart { get; set; }	
}
