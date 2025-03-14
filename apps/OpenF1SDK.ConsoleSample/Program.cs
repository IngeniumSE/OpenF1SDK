// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using System.Net.Http.Headers;

using Microsoft.Extensions.Configuration;

using OpenF1SDK;
using OpenF1SDK.Api;

var settings = GetSettings();
var http = CreateHttpClient();
var api = new OpenF1ApiClient(http, settings);

//var meetings = await api.Meetings.GetMeetingsAsync(year: 2025);
//Console.WriteLine("Found {0} meetings", meetings.Data.Length);

//var meeting = await api.Meetings.GetLatestMeetingAsync();

//var sessions = await api.Sessions.GetLatestSessionsAsync();
//Console.WriteLine("Found {0} sessions", sessions.Data.Length);

//var practice1 = sessions.Data.First();
//var drivers = await api.Drivers.GetDriversAsync(sessionKey: practice1.SessionKey);
//Console.WriteLine("Found {0} drivers", drivers.Data.Length);

//var laps = await api.Laps.GetLatestLapsAsync(44);
//var lap = await api.Laps.GetLatestLapAsync(44);
//Console.WriteLine("Found {0} laps", laps.Data.Length);

var stints = await api.Stints.GetLatestStintsAsync(44);
var stint = await api.Stints.GetLatestStintAsync(44);
Console.WriteLine("Found {0} stints", stints.Data.Length);


OpenF1Settings GetSettings()
{
	var configuration = new ConfigurationBuilder()
		.AddJsonFile("./appsettings.json", optional: false)
		.AddJsonFile("./appsettings.env.json", optional: true)
		.Build();

	OpenF1Settings settings = new();
	configuration.GetSection(OpenF1Settings.ConfigurationSection).Bind(settings);

	settings.Validate();

	return settings;
}

HttpClient CreateHttpClient()
{
	var http = new HttpClient();

	http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

	return http;
}
