// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

using FluentValidation;

using Microsoft.Extensions.Options;

namespace OpenF1SDK;

/// <summary>
/// Represents settings for configuring OpenF1.
/// </summary>
public class OpenF1Settings
{
	public const string ConfigurationSection = "OpenF1";

	/// <summary>
	/// Gets or sets the API key.
	/// </summary>
	public string BaseUrl { get; set; } = "https://api.openf1.org/";

	/// <summary>
	/// Gets or sets whether to capture request content.
	/// </summary>
	public bool CaptureRequestContent { get; set; }

	/// <summary>
	/// Gets or sets whether to capture response content.
	/// </summary>
	public bool CaptureResponseContent { get; set; }

	/// <summary>
	/// Returns the settings as a wrapped options instance.
	/// </summary>
	/// <returns>The options instance.</returns>
	public IOptions<OpenF1Settings> AsOptions()
		=> Options.Create(this);

	/// <summary>
	/// Validates the current instance.
	/// </summary>
	public void Validate()
		=> OpenF1SettingsValidator.Instance.Validate(this);
}

/// <summary>
/// Validates instances of <see cref="OpenF1Settings"/>.
/// </summary>
public class OpenF1SettingsValidator : AbstractValidator<OpenF1Settings>
{
	public static readonly OpenF1SettingsValidator Instance = new();

	public OpenF1SettingsValidator()
	{
		bool ValidateUri(string value)
			=> Uri.TryCreate(value, UriKind.Absolute, out var _);

		RuleFor(s => s.BaseUrl)
			.Custom((value, context) =>
			{
				if (!ValidateUri(value))
				{
					context.AddFailure($"'{value}' is not a valid URI.");
				}
			});
	}
}
