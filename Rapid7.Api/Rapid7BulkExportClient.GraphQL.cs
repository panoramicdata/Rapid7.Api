using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7BulkExportClient
{
	/// <summary>The Bulk Export GraphQL operations, one method per mutation or query (<c>export/graphql</c>).</summary>
	public IBulkExport Exports => field ??= For<IBulkExport>();
}
