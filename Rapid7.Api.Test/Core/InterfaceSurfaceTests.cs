using System.Reflection;

namespace Rapid7.Api.Test.Core;

/// <summary>Keeps the endpoint interfaces implementable outside Rapid7.Api (by callers' own classes and by mocking libraries).</summary>
public class InterfaceSurfaceTests
{
	private static readonly List<Type> Interfaces = [.. typeof(Rapid7Client).Assembly.GetExportedTypes()
		.Where(t => t.IsInterface && t.Namespace == "Rapid7.Api.Interfaces")];

	[Fact]
	public void EndpointInterfaces_HaveNoNonPublicMembers()
	{
		var nonPublic = Interfaces
			.SelectMany(t => t.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
				.Select(m => $"{t.Name}.{m.Name}"))
			.ToList();

		Interfaces.Should().NotBeEmpty();
		nonPublic.Should().BeEmpty("a non-public interface member cannot be implemented by an assembly without InternalsVisibleTo, so callers could not implement or mock the interface");
	}

	[Fact]
	public void EndpointInterfaces_AreAllPublic()
		=> typeof(Rapid7Client).Assembly.GetTypes()
			.Where(t => t.IsInterface && t.Namespace == "Rapid7.Api.Interfaces" && !t.IsPublic)
			.Should().BeEmpty("every endpoint interface is exposed by a public property of a Rapid7 client");
}
