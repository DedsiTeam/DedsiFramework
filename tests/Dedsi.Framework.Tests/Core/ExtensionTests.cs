using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Dedsi.Core.Extensions;

namespace Dedsi.Framework.Tests.Core;

public class ExtensionTests
{
    [Theory]
    [InlineData("", "d41d8cd98f00b204e9800998ecf8427e")]
    [InlineData("abc", "900150983cd24fb0d6963f7d28e17f72")]
    public void Md5_matches_known_vectors(string input, string expected) => Assert.Equal(expected, input.GetMd5Hash());

    [Theory]
    [InlineData("")]
    [InlineData("中文🙂\nline two")]
    [InlineData("hello")]
    public void Aes_roundtrips_utf8(string input)
    {
        const string key = "0123456789abcdef0123456789abcdef", iv = "abcdef0123456789";
        var encrypted = input.GetAseEncrypt(key, iv);
        Assert.NotEqual(input, encrypted);
        Assert.Equal(input, encrypted.GetAseDecrypt(key, iv));
    }

    [Fact]
    public void Aes_rejects_invalid_key_iv_and_ciphertext()
    {
        Assert.Throws<CryptographicException>(() => "data".GetAseEncrypt("short", "abcdef0123456789"));
        Assert.Throws<CryptographicException>(() => "data".GetAseEncrypt("0123456789abcdef", "short"));
        Assert.Throws<FormatException>(() => "!".GetAseDecrypt("0123456789abcdef", "abcdef0123456789"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("中文🙂")]
    public void Byte_conversion_roundtrips(string text) => Assert.Equal(text, text.ToBytes().ByteToString());

    [Theory]
    [InlineData("FullName", "fullName")]
    [InlineData("URLValue", "urlValue")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void Camel_case_handles_acronyms_and_empty_input(string? input, string? expected) => Assert.Equal(expected, input!.ToCamelCase());

    [Theory]
    [InlineData(null, "")]
    [InlineData("   ", "")]
    [InlineData("<p> Hello   <b>world</b> &amp; 中文 </p>", "Hello world & 中文")]
    [InlineData("<SCRIPT>danger\n()</SCRIPT><style>p{}</style><p>Safe</p>", "Safe")]
    public void Strip_html_removes_markup_script_and_style(string? input, string expected) => Assert.Equal(expected, input!.StripHtml());

    [Fact]
    public void Conditional_predicates_combine_with_and()
    {
        Expression<Func<int, bool>> positive = value => value > 0;
        var combined = positive.WhereIf(true, other => other < 10).Compile();
        Assert.True(combined(5));
        Assert.False(combined(-1));
        Assert.False(combined(10));
        Assert.Same(positive, positive.WhereIf(false, _ => false));
        Assert.True(ExpressionExtensions.GetTrue<int>().Compile()(0));
        Assert.False(ExpressionExtensions.GetFalse<int>().Compile()(0));
    }

    public class Row { public string Name { get; set; } = ""; public int? Quantity { get; set; } }

    [Fact]
    public void Data_table_preserves_schema_values_and_nulls()
    {
        using var table = new List<Row> { new() { Name = "A", Quantity = 2 }, new() { Name = "B" } }.ToDataTable();
        Assert.Equal(typeof(int), table.Columns["Quantity"]!.DataType);
        Assert.Equal("A", table.Rows[0]["Name"]);
        Assert.Equal(2, table.Rows[0]["Quantity"]);
        Assert.Equal(DBNull.Value, table.Rows[1]["Quantity"]);
    }

    [Fact]
    public void Empty_list_still_produces_table_schema()
    {
        using var table = new List<Row>().ToDataTable();
        Assert.Equal(2, table.Columns.Count);
        Assert.Empty(table.Rows.Cast<System.Data.DataRow>());
    }

    public enum Status { [Description("已完成")] Done, Pending }
    [Theory]
    [InlineData(Status.Done, "已完成")]
    [InlineData(Status.Pending, "")]
    public void Enum_description_is_optional(Status status, string expected) => Assert.Equal(expected, status.GetDescriptionValue());

    [Fact]
    public void Undefined_enum_should_not_throw() => Assert.Equal(string.Empty, ((Status)99).GetDescriptionValue());

    [Flags]
    public enum Permissions
    {
        [Description("读取")] Read = 1,
        Write = 2,
        [Description("全部")] All = Read | Write,
        Execute = 4
    }

    [Theory]
    [InlineData(Permissions.Read, "读取")]
    [InlineData(Permissions.Write, "")]
    [InlineData(Permissions.All, "全部")]
    [InlineData(Permissions.Read | Permissions.Execute, "")]
    [InlineData((Permissions)0, "")]
    [InlineData((Permissions)99, "")]
    public void Flags_use_description_only_for_a_named_field(Permissions value, string expected)
        => Assert.Equal(expected, value.GetDescriptionValue());

    [Fact]
    public void Null_enum_is_rejected() => Assert.Throws<ArgumentNullException>(() => ((Enum)null!).GetDescriptionValue());

    [Fact]
    public void Date_text_uses_requested_format()
    {
        var before = DateTime.Now.Date;
        var result = DateTime.ParseExact(DateTimeExtensions.GetNowString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Assert.InRange(result, before, DateTime.Now.Date);
        Assert.True(DateTime.TryParseExact(DateTimeExtensions.GetNowString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
    }
}
