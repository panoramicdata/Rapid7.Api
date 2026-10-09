using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;
using Rapid7.Api.Models.Tags;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Tags and their search criteria (<c>api/3/tags</c>). Creating, changing and deleting tags needs the Manage Tags
/// privilege. For the assets, asset groups and sites a tag applies to, see <see cref="ITagMembers"/>.
/// </summary>
public interface ITags
{
	/// <summary>Lists, a page at a time, the tags (<c>GET api/3/tags</c>).</summary>
	/// <param name="options">Paging, sorting and filters by name and type, or <see langword="null"/> for the first page sorted by name.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of tags.</returns>
	[Get("api/3/tags")]
	Task<Page<Tag>> ListAsync([Query] TagListOptions? options, CancellationToken cancellationToken);

	/// <summary>Creates a tag (<c>POST api/3/tags</c>).</summary>
	/// <param name="request">The tag's name, type and settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new tag's identifier.</returns>
	[Post("api/3/tags")]
	Task<CreatedReference<int>> CreateAsync([Body] TagRequest request, CancellationToken cancellationToken);

	/// <summary>Gets a tag (<c>GET api/3/tags/{id}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The tag.</returns>
	[Get("api/3/tags/{tagId}")]
	Task<Tag> GetAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Replaces a tag's settings (<c>PUT api/3/tags/{id}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="request">The tag's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the tag.</returns>
	[Put("api/3/tags/{tagId}")]
	Task<LinksResource> UpdateAsync(int tagId, [Body] TagRequest request, CancellationToken cancellationToken);

	/// <summary>Deletes a tag, removing it from every asset (<c>DELETE api/3/tags/{id}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/tags/{tagId}")]
	Task<LinksResource> DeleteAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Gets the search criteria that apply a tag automatically (<c>GET api/3/tags/{id}/search_criteria</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The search criteria.</returns>
	[Get("api/3/tags/{tagId}/search_criteria")]
	Task<SearchCriteria> GetSearchCriteriaAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Replaces the search criteria that apply a tag automatically (<c>PUT api/3/tags/{id}/search_criteria</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="criteria">The new criteria.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the tag's criteria.</returns>
	[Put("api/3/tags/{tagId}/search_criteria")]
	Task<LinksResource> UpdateSearchCriteriaAsync(int tagId, [Body] SearchCriteria criteria, CancellationToken cancellationToken);

	/// <summary>
	/// Removes a tag's search criteria (<c>DELETE api/3/tags/{id}/search_criteria</c>); assets tagged only through them
	/// lose the tag.
	/// </summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/tags/{tagId}/search_criteria")]
	Task<LinksResource> DeleteSearchCriteriaAsync(int tagId, CancellationToken cancellationToken);
}
