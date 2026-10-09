using Rapid7.Api.Serialization;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Test.Core;

/// <summary>Pins how enum wire names are parsed and written.</summary>
public class WireNamesTests
{
	public enum Level
	{
		Unknown = 0,

		[JsonStringEnumMemberName("H")]
		High,

		// Uses another member's C# name as its wire name: the wire name wins.
		[JsonStringEnumMemberName("Medium")]
		Low,

		[JsonStringEnumMemberName("M")]
		Medium,
	}

	[Theory]
	[InlineData("H", Level.High)]
	[InlineData("h", Level.High)]
	[InlineData("HIGH", Level.High)]
	[InlineData("M", Level.Medium)]
	[InlineData("MEDIUM", Level.Low)]
	[InlineData("low", Level.Low)]
	[InlineData("nonsense", Level.Unknown)]
	public void Parse_AcceptsWireNamesThenMemberNames(string wire, Level expected)
		=> WireNames.Parse<Level>(wire).Should().Be(expected);

	[Fact]
	public void Of_WritesTheWireName()
		=> WireNames.Of(Level.High).Should().Be("H");

	[Fact]
	public void Of_WritesAnUndefinedValueAsItsNumber()
		=> WireNames.Of((Level)42).Should().Be("42");
}
