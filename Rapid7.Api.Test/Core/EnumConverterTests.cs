using Rapid7.Api.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Test.Core;

/// <summary>Tolerant enum reading and writing, and the wire names shared by JSON and query strings.</summary>
public class EnumConverterTests
{
	public enum Colour
	{
		Unknown = 0,

		[JsonStringEnumMemberName("light-red")]
		Red = 1,

		Green = 2,

#pragma warning disable CA1069 // A deliberate alias: WireNames writes the first member's name and reads both.
		[JsonStringEnumMemberName("verde")]
		Verde = 2
#pragma warning restore CA1069
	}

	public enum Small : byte
	{
		Unknown = 0,
		One = 1
	}

	public enum Big : long
	{
		Unknown = 0,
		Huge = 1L << 40
	}

	private static T ReadEnum<T>(string json) where T : struct, Enum
		=> JsonSerializer.Deserialize<T>(json, Rapid7Json.Options);

	[Theory]
	[InlineData("\"light-red\"", Colour.Red)]
	[InlineData("\"LIGHT-RED\"", Colour.Red)]
	[InlineData("\"Green\"", Colour.Green)]
	[InlineData("\"verde\"", Colour.Green)]
	[InlineData("\"Red\"", Colour.Red)] // the C# name is accepted when no member uses it as a wire name
	[InlineData("\"purple\"", Colour.Unknown)]
	[InlineData("\"\"", Colour.Unknown)]
	[InlineData("1", Colour.Red)]
	[InlineData("7", Colour.Unknown)]
	[InlineData("1.5", Colour.Unknown)]
	[InlineData("null", Colour.Unknown)]
	public void Enum_ReadsWireNamesAndNumbers(string json, Colour expected)
		=> ReadEnum<Colour>(json).Should().Be(expected);

	[Theory]
	[InlineData("1", Small.One)]
	[InlineData("257", Small.Unknown)]
	[InlineData("-1", Small.Unknown)]
	public void Enum_ReadsNumbersForAByteEnum(string json, Small expected)
		=> ReadEnum<Small>(json).Should().Be(expected);

	[Fact]
	public void Enum_ReadsNumbersForALongEnum()
		=> ReadEnum<Big>("1099511627776").Should().Be(Big.Huge);

	[Fact]
	public void Enum_ReadsNullableEnums()
	{
		JsonSerializer.Deserialize<Colour?>("null", Rapid7Json.Options).Should().BeNull();
		JsonSerializer.Deserialize<Colour?>("\"verde\"", Rapid7Json.Options).Should().Be(Colour.Green);
	}

	[Theory]
	[InlineData("true")]
	[InlineData("{}")]
	public void Enum_OtherTokens_Throw(string json)
	{
		var act = () => ReadEnum<Colour>(json);

		act.Should().Throw<JsonException>().WithMessage("*Cannot read Colour from a JSON*");
	}

	[Theory]
	[InlineData(Colour.Red, "\"light-red\"")]
	[InlineData(Colour.Green, "\"Green\"")]
	[InlineData((Colour)9, "\"9\"")]
	public void Enum_WritesTheWireName(Colour value, string json)
		=> JsonSerializer.Serialize(value, Rapid7Json.Options).Should().Be(json);

	[Fact]
	public void WireNames_UndefinedValuesAreNumbers_AndUnknownNamesTheDefault()
	{
		WireNames.Of((Small)200).Should().Be("200");
		WireNames.Of(Colour.Red).Should().Be("light-red");
		WireNames.Parse<Colour>("GREEN").Should().Be(Colour.Green);
		WireNames.Parse<Colour>("nope").Should().Be(Colour.Unknown);
	}

	[Fact]
	public void TheFactory_ConvertsOnlyEnums()
	{
		var factory = new TolerantEnumConverterFactory();

		factory.CanConvert(typeof(Colour)).Should().BeTrue();
		factory.CanConvert(typeof(int)).Should().BeFalse();
		factory.CreateConverter(typeof(Small), Rapid7Json.Options).Should().BeOfType<TolerantEnumConverter<Small>>();
	}
}
