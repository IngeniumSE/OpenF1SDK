# OpenF1 SDK

[![.github/workflows/main.yml](https://github.com/IngeniumSE/OpenF1SDK/actions/workflows/main.yml/badge.svg)](https://github.com/IngeniumSE/OpenF1SDK/actions/workflows/main.yml) [![.github/workflows/release.yml](https://github.com/IngeniumSE/OpenF1SDK/actions/workflows/release.yml/badge.svg)](https://github.com/IngeniumSE/OpenF1SDK/actions/workflows/release.yml)

A .NET SDK built for the Open F1 API.

https://openf1.org/.

The Open F1 API is a free API that provides Formula 1 data in a simple and easy to use format. The API was created an is maintained by [Bruno Godefroy](https://x.com/brgodefroy) (https://x.com/brgodefroy).

> Please note, the OpenF1SDK is unofficial and is not associated in any way with the Formula 1 companies. F1, FORMULA ONE, FORMULA 1, FIA FORMULA ONE WORLD CHAMPIONSHIP, GRAND PRIX and related marks are trade marks of Formula One Licensing B.V.

## Installation

The OpenF1 SDK is available on NuGet. Please install the `OpenF1SDK` package.

### .NET CLI

```
dotnet add package OpenF1SDK
```

### Nuget Package Manager

```
Install-Package OpenF1SDK
```

### Nuget CLI

```
nuget install OpenF1SDK
```

## Getting started

### Integration with Microsoft.Extensions.DependencyInjection

The OpenF1 SDK can be integrated with the `Microsoft.Extensions.DependencyInjection` library. This allows for easy dependency injection of the OpenF1 SDK services.

```csharp
services.AddOpenF1();
```

### Manual setup

If you do not wish to use the `Microsoft.Extensions.DependencyInjection` library, you can manually create an instance of the `OpenF1Client` class.

```csharp
var http = new HttpClient();
var client = new OpenF1Client(http, new OpenF1Settings());
```

Or, alternatively, you can use the API client factory:

```csharp
var httpFactory = new OpenF1HttpClientFactory();
var clientFactory = new OpenF1ApiClientFactory(httpFactory);
var client = clientFactory.CreateClient(new OpenF1Settings());
```

**NOTE** - On .NET Framework, it is recommended to use a single instance of `HttpClient` for the lifetime of your application. This is because the `HttpClient` class is designed to be reused and not disposed of after each request.

A `IOpenF1HttpClientFactory` can be implemented to manage the lifecycle of the `HttpClient` instance.

## Terminology

| Term | Description |
|-|-|
| Meeting | A meeting refers to a Grand Prix or testing weekend and usually includes multiple sessions (practice, qualifying, race, ...). |
| Session | A session refers to a distinct period of track activity during a Grand Prix or testing weekend (practice, qualifying, sprint, race, ...). |
| Stint | A stint refers to a period of continuous driving by a driver during a session. |

Other terms such as Driver, Lap, etc are self-explanatory.

## Supported operations

The SDK supports a subset of the available operations in the Open F1 API. The following operations are supported:

The SDK is designed to be extensible, so any calls not currently supported by the SDK, can be made using the `OpenF1Client` class directly.

| Endpoint | Client Property | Operation | Description |
|-|-|-|
| `/v1/meetings` | `Meetings` | `GetMeetingsAsync` | Gets meetings that match the specified criteria. |
| `/v1/meetings` | `Meetings` | `GetLatestMeetingAsync` | Gets the latest/current meeting. |
| `/v1/sessions` | `Sessions` | `GetSessionsAsync` | Gets sessions that match the specified criteria. |
| `/v1/sessions` | `Sessions` | `GetLatestSessionsAsync` | Gets sessions for the latest/current meeting. |
| `/v1/drivers` | `Drivers` | `GetDriversAsync` | Gets drivers that match the specified criteria. |
| `/v1/laps` | `Laps` | `GetLapsAsync` | Gets all laps for the given driver, for the given session. |
| `/v1/laps` | `Laps` | `GetLatestLapsAsync` | Gets the latest laps for the given driver for the latest/current session. |
| `/v1/laps` | `Laps` | `GetLatestLapAsync` | Gets the latest lap for the given driver for the latest/current session. |
| `/v1/stints` | `Stints` | `GetStintsAsync` | Gets all stints for the given driver, for the given session. |
| `/v1/stints` | `Stints` | `GetLatestStrintsAsync` | Gets the latest stints for the given driver for the latest/current session. |
| `/v1/stints` | `Stints` | `GetLatestStintAsync` | Gets the latest stint for the given driver for the latest/current session. |

## Example

The following example demonstrates how to use the OpenF1 SDK to get the latest meeting and sessions.

```csharp
// Get all laps by Sir Lewis Hamilton for the latest session
var laps = await client.Laps.GetLatestLapsAsync(44);

// Get all stints by Sir Lewis Hamilton for the latest session
var stints = await client.Stints.GetLatestStintsAsync(44);
```

### Debugging

To aid in debugging results from the Open F1 API, you can enable the following settings:

```json
{
  "OpenF1": {
    "CaptureRequestContent": true,
    "CaptureResponseContent": true
  }
}
```

These settings, when enabled will capture the request and response content for each API call, and the content of these will be available to the `OpenF1Response` as `RequestContent` and `ResponseContent` properties. The SDK will automatically map these results, but for unexpected results, it is useful to understand what has been sent/received.

## Open Source

This SDK is open source and is available under the MIT license. Feel free to contribute to the project by submitting pull requests or issues.

| Component | Authors | Website | License |
|-----------|---------|---------|---------|
| .NET Platform | Microsoft and contributors | [GitHub](https://github.com/dotnet) | MIT |
| Ben.Demystifier | Ben Adams | [GitHub](https://github.com/benaadams/Ben.Demystifier) | Apache V2 |
| FluentValidation | Jeremy Skinner and contributors | [GitHub](https://github.com/FluentValidation/FluentValidation) | Apache V2 |
| MinVer | Adam Ralph and contributors | [GitHub](https://github.com/adamralph/minver) | Apache V2 |

By using this SDK, you agree to the terms of the MIT license used by this project, as well as the terms of the licenses of the components used by this SDK.
