using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Dedsi.Core.Extensions;

namespace Dedsi.Framework.Tests.Core;

public class SerializationTests
{
    public class Payload { public string Name { get; set; } = ""; public int Count { get; set; } }
    private const string Xml = "<Payload><Name>中文</Name><Count>3</Count></Payload>";

    [Fact]
    public void Default_json_uses_camel_case_and_preserves_unicode()
    {
        using var doc = JsonDocument.Parse(new Payload { Name = "中文", Count = 3 }.ToJson());
        Assert.Equal("中文", doc.RootElement.GetProperty("name").GetString());
        Assert.Equal(3, doc.RootElement.GetProperty("count").GetInt32());
    }

    [Fact]
    public void Normal_json_roundtrips_public_properties()
    {
        var result = new Payload { Name = "中文", Count = 3 }.ToJsonByNormal().MapToObjectFromJson<Payload>();
        Assert.Equal("中文", result.Name);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Custom_naming_policy_is_used()
    {
        using var doc = JsonDocument.Parse(new { FullName = "Alice" }.ToJson(JsonNamingPolicy.SnakeCaseLower));
        Assert.Equal("Alice", doc.RootElement.GetProperty("full_name").GetString());
    }

    [Fact] public void Invalid_json_is_rejected() => Assert.Throws<JsonException>(() => "{".MapToObjectFromJson<Payload>());
    [Fact] public void Null_json_result_is_rejected() => Assert.Throws<InvalidOperationException>(() => "null".MapToObjectFromJson<Payload>());

    [Fact]
    public void Default_json_should_roundtrip_properties()
    {
        var result = new Payload { Name = "Alice", Count = 3 }.ToJson().MapToObjectFromJson<Payload>();
        Assert.Equal("Alice", result.Name);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Caller_options_should_be_reusable()
    {
        var options = new JsonSerializerOptions();
        var payload = new Payload { Name = "Alice" };
        Assert.Equal(payload.ToJson(options), payload.ToJson(options));
    }

    [Fact]
    public void Caller_encoder_should_be_preserved()
    {
        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Default };
        new Payload { Name = "<script>" }.ToJson(options);
        Assert.Same(JavaScriptEncoder.Default, options.Encoder);
    }

    [Fact]
    public void Read_only_options_and_custom_naming_roundtrip()
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
        options.MakeReadOnly(populateMissingResolver: true);
        var payload = new NestedPayload { FullName = "Alice", Child = new Payload { Name = "中文", Count = 3 } };
        var json = payload.ToJson(options);
        Assert.Contains("full_name", json);
        var result = json.MapToObjectFromJson<NestedPayload>(options);
        Assert.Equal("Alice", result.FullName);
        Assert.Equal("中文", result.Child.Name);
        Assert.Equal(3, result.Child.Count);
        Assert.Equal(json, payload.ToJson(options));
    }

    public class NestedPayload
    {
        public string FullName { get; set; } = "";
        public Payload Child { get; set; } = new();
    }

    [Theory]
    [InlineData("{\"name\":\"Alice\",\"count\":3}")]
    [InlineData("{\"Name\":\"Alice\",\"Count\":3}")]
    [InlineData("{\"NAME\":\"Alice\",\"COUNT\":3}")]
    public void Default_deserialization_accepts_property_casing(string json)
    {
        var result = json.MapToObjectFromJson<Payload>();
        Assert.Equal("Alice", result.Name);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task Shared_options_are_safe_for_concurrent_reuse()
    {
        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Default };
        var payload = new Payload { Name = "<tag>中文", Count = 3 };
        var expected = JsonSerializer.Serialize(payload, options);
        var results = await Task.WhenAll(Enumerable.Range(0, 20).Select(_ => Task.Run(() => payload.ToJson(options))));
        Assert.All(results, json => Assert.Equal(expected, json));
        Assert.Same(JavaScriptEncoder.Default, options.Encoder);
    }

    [Fact]
    public void Custom_options_preserve_strict_deserialization_and_encoding()
    {
        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Default };
        var payload = new Payload { Name = "<script>" };
        Assert.Equal(JsonSerializer.Serialize(payload, options), payload.ToJson(options));
        Assert.Equal("", "{\"name\":\"Alice\"}".MapToObjectFromJson<Payload>(options).Name);
        Assert.Throws<ArgumentNullException>(() => payload.ToJson((JsonSerializerOptions)null!));
        Assert.Throws<ArgumentNullException>(() => "{}".MapToObjectFromJson<Payload>(null!));
    }

    [Fact]
    public void Xml_string_and_stream_deserialize_unicode()
    {
        var fromString = Xml.XmlDeserialize<Payload>(Encoding.UTF8);
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(Xml));
        var fromStream = stream.XmlDeserialize<Payload>(Encoding.UTF8);
        Assert.Equal("中文", fromString.Name);
        Assert.Equal(3, fromString.Count);
        Assert.Equal(fromString.Name, fromStream.Name);
        Assert.Equal(fromString.Count, fromStream.Count);
        Assert.Equal("中文", Xml.DeserializeObject<Payload>().Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Empty_xml_has_explicit_overload_contracts(string? xml)
    {
        Assert.Null(xml!.DeserializeObject<Payload>());
        Assert.Throws<ArgumentNullException>(() => xml!.XmlDeserialize<Payload>(Encoding.UTF8));
    }

    [Fact]
    public void Xml_requires_encoding_and_well_formed_input()
    {
        Assert.Throws<ArgumentNullException>(() => Xml.XmlDeserialize<Payload>(null!));
        using var stream = new MemoryStream();
        Assert.Throws<ArgumentNullException>(() => stream.XmlDeserialize<Payload>(null!));
        Assert.Throws<InvalidOperationException>(() => "<Payload>".DeserializeObject<Payload>());
    }

    [Fact]
    public async Task Xml_serializer_cache_handles_concurrent_calls()
    {
        var results = await Task.WhenAll(Enumerable.Range(0, 20).Select(_ => Task.Run(() => Xml.DeserializeObject<Payload>())));
        Assert.All(results, x => Assert.Equal("中文", x.Name));
    }
}
