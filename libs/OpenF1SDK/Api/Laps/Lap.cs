// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using System.Diagnostics;
using System.Text.Json.Serialization;

namespace OpenF1SDK.Api;

[DebuggerDisplay("{DriverNumber,nq}/{LapNumber,nq} - {LapDuration,nq} [{DurationSector1,nq},{DurationSector2,nq},{DurationSector3,nq}]")]
public partial class Lap : Model<Lap>
{
	/// <summary>
	/// The UTC starting date and time.
	/// </summary>
	[JsonPropertyName("date_start")]
	public DateTimeOffset DateStart { get; set; }

	/// <summary>
	/// The unique number assigned to an F1 driver (cf. https://en.wikipedia.org/wiki/List_of_Formula_One_driver_numbers#Formula_One_driver_numbers).
	/// </summary>
	[JsonPropertyName("driver_number")]
	public int DriverNumber { get; set; }

	/// <summary>
	/// The time taken, in seconds, to complete the first sector of the lap.
	/// </summary>
	[JsonPropertyName("duration_sector_1")]
	public decimal? DurationSector1 { get; set; }

	/// <summary>
	/// The time taken, in seconds, to complete the second sector of the lap.
	/// </summary>
	[JsonPropertyName("duration_sector_2")]
	public decimal? DurationSector2 { get; set; }

	/// <summary>
	/// The time taken, in seconds, to complete the third sector of the lap.
	/// </summary>
	[JsonPropertyName("duration_sector_3")]
	public decimal? DurationSector3 { get; set; }

	/// <summary>
	/// The speed of the car, in km/h, at the first intermediate point on the track.
	/// </summary>
	[JsonPropertyName("i1_speed")]
	public int? IntermediateSpeed1 { get; set; }

	/// <summary>
	/// The speed of the car, in km/h, at the second intermediate point on the track.
	/// </summary>
	[JsonPropertyName("i2_speed")]
	public int? IntermediateSpeed2 { get; set; }

	/// <summary>
	/// A boolean value indicating whether the lap is an "out lap" from the pit.
	/// </summary>
	[JsonPropertyName("is_pit_out_lap")]
	public bool IsPitOutLap { get; set; }

	/// <summary>
	/// The total time taken, in seconds, to complete the entire lap.
	/// </summary>
	[JsonPropertyName("lap_duration")]
	public decimal? LapDuration { get; set; }

	/// <summary>
	/// The sequential number of the lap within the session (starts at 1).
	/// </summary>
	[JsonPropertyName("lap_number")]
	public int LapNumber { get; set; }

	/// <summary>
	/// The unique identifier for the meeting.
	/// </summary>
	[JsonPropertyName("meeting_key")]
	public int MeetingKey { get; set; }

	/// <summary>
	/// A list of values representing the "mini-sectors" within the first sector.
	/// </summary>
	[JsonPropertyName("segments_sector_1")]
	public int?[]? SegmentsSector1 { get; set; }

	/// <summary>
	/// A list of values representing the "mini-sectors" within the second sector.
	/// </summary>
	[JsonPropertyName("segments_sector_2")]
	public int?[]? SegmentsSector2 { get; set; }

	/// <summary>
	/// A list of values representing the "mini-sectors" within the third sector.
	/// </summary>
	[JsonPropertyName("segments_sector_3")]
	public int?[]? SegmentsSector3 { get; set; }

	/// <summary>
	/// The unique identifier for the session.
	/// </summary>
	[JsonPropertyName("session_key")]
	public int SessionKey { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[JsonPropertyName("st_speed")]
	public int? SpeedTrapSpeed { get; set; }
}
