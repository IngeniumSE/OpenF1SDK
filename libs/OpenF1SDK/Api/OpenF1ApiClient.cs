// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

namespace OpenF1SDK.Api;

public partial interface IOpenF1ApiClient
{

}

public partial class OpenF1ApiClient : ApiClient, IOpenF1ApiClient
{
	public OpenF1ApiClient(HttpClient http, OpenF1Settings settings)
		: base(http, settings) { }
}
