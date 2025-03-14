// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

namespace OpenF1SDK;

/// <summary>
/// Represents a request to a Trybe API resource.
/// </summary>
/// <param name="method">The HTTP method.</param>
/// <param name="resource">The relative resource.</param>
/// <param name="query">The query string.</param>
public class OpenF1Request(
	HttpMethod method,
	PathString resource,
	QueryString? query = null)
{
	/// <summary>
	/// Gets the HTTP method for the request.
	/// </summary>
	public HttpMethod Method => method;

	/// <summary>
	/// Gets the relative resource for the request.
	/// </summary>
	public PathString Resource => resource;

	/// <summary>
	/// Gets the query string.
	/// </summary>
	public QueryString? Query => query;
}

/// <summary>
/// Represents a request to a Trybe API resource.
/// </summary>
/// <param name="method">The HTTP method.</param>
/// <param name="resource">The relative resource.</param>
/// <param name="data">The data.</param>
/// <typeparam name="TData">The data type.</typeparam>
public class OpenF1Request<TData>(
	HttpMethod method,
	PathString resource,
	TData data,
	QueryString? query = null) : OpenF1Request(method, resource, query)
	where TData : notnull
{
	/// <summary>
	/// Gets the model for the request.
	/// </summary>
	public TData Data => data;
}
