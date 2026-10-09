using Rapid7.Api.Models;

namespace Rapid7.Api.Test.Support;

/// <summary>Assertions on hypermedia links and create answers, shared by every endpoint group's tests.</summary>
internal static class LinkAssertions
{
	/// <summary>Asserts that the links are exactly one <c>self</c> link.</summary>
	public static void ShouldBeSelfOnly(this IReadOnlyList<Link>? links)
		=> links.Should().ContainSingle().Which.Rel.Should().Be("self");

	/// <summary>Asserts a create answer: the new identifier and a single <c>self</c> link.</summary>
	public static void ShouldBeCreated<TId>(this CreatedReference<TId> created, TId id)
	{
		created.Id.Should().Be(id);
		created.Links.ShouldBeSelfOnly();
	}
}
