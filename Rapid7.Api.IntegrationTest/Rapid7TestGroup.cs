namespace Rapid7.Api.IntegrationTest;

/// <summary>Shares one <see cref="Rapid7Fixture"/> across every integration test class.</summary>
[CollectionDefinition(Name)]
public sealed class Rapid7TestGroup : ICollectionFixture<Rapid7Fixture>
{
	internal const string Name = "Rapid7";
}
