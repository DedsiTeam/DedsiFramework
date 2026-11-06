using System.Text.Json;
using Dedsi.Ddd.Application.Contracts.Dtos;

namespace Dedsi.Framework.Tests.Contracts;

public class PagingTests
{
    [Theory]
    [InlineData(1, 10, 0)]
    [InlineData(3, 20, 40)]
    [InlineData(2, 1000, 1000)]
    public void Valid_paging_returns_offset(int page, int size, int expected)
    {
        var request = new DedsiPagedRequestDto { PageIndex = page, PageSize = size };
        Assert.Equal(expected, request.GetSkipCount());
        Assert.Equal(page, request.PageIndex);
        Assert.Equal(size, request.PageSize);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, -5)]
    [InlineData(1, 1001)]
    public void Invalid_paging_is_normalized(int page, int size)
    {
        var request = new DedsiPagedRequestDto { PageIndex = page, PageSize = size };
        Assert.Equal(0, request.GetSkipCount());
        Assert.Equal(1, request.PageIndex);
        Assert.Equal(10, request.PageSize);
    }

    [Fact]
    public void Defaults_and_normalization_are_available_before_offset_calculation()
    {
        var request = new DedsiPagedRequestDto();
        Assert.Equal(1, request.PageIndex);
        Assert.Equal(10, request.PageSize);
        request.PageIndex = int.MinValue;
        request.PageSize = int.MaxValue;
        Assert.Equal(1, request.PageIndex);
        Assert.Equal(10, request.PageSize);
        Assert.Equal(0, request.GetSkipCount());
        Assert.Equal(0, request.GetSkipCount());
        Assert.Equal(1, request.PageIndex);
        Assert.Equal(10, request.PageSize);
    }

    [Fact]
    public void Json_binding_normalizes_invalid_paging_immediately()
    {
        var request = JsonSerializer.Deserialize<DedsiPagedRequestDto>("{\"PageIndex\":-1,\"PageSize\":1001}")!;
        Assert.Equal(1, request.PageIndex);
        Assert.Equal(10, request.PageSize);
    }

    [Theory]
    [InlineData(214748364, 10, 2147483630)]
    [InlineData(214748365, 10, 2147483640)]
    [InlineData(int.MaxValue, 1, int.MaxValue - 1)]
    public void Largest_representable_offsets_are_accepted(int page, int size, int expected)
        => Assert.Equal(expected, new DedsiPagedRequestDto { PageIndex = page, PageSize = size }.GetSkipCount());

    [Fact]
    public void First_overflowing_page_is_rejected_without_changing_request()
    {
        var request = new DedsiPagedRequestDto { PageIndex = 214748366, PageSize = 10 };
        Assert.Throws<ArgumentOutOfRangeException>(() => request.GetSkipCount());
        Assert.Equal(214748366, request.PageIndex);
        Assert.Equal(10, request.PageSize);
    }

    [Fact]
    public void Export_flag_is_excluded_from_json_in_both_directions()
    {
        var request = new DedsiPagedRequestDto();
        request.EnableExport();
        Assert.True(request.IsExport);
        var json = JsonSerializer.Serialize(request);
        Assert.DoesNotContain("IsExport", json);
        Assert.False(JsonSerializer.Deserialize<DedsiPagedRequestDto>("{\"IsExport\":true}")!.IsExport);
    }

    [Fact]
    public void Paged_result_preserves_long_count_and_rows()
    {
        var result = new DedsiPagedResultDto<string>((long)int.MaxValue + 1, ["A", "B"]);
        Assert.Equal((long)int.MaxValue + 1, result.TotalCount);
        Assert.Equal(new[] { "A", "B" }, result.Items);
        Assert.Empty(new DedsiPagedResultDto<string>().Items);
    }

    [Fact]
    public void Overflowing_page_should_be_rejected()
    {
        var request = new DedsiPagedRequestDto { PageIndex = int.MaxValue, PageSize = 10 };
        Assert.Equal("pageIndex", Assert.Throws<ArgumentOutOfRangeException>(() => request.GetSkipCount()).ParamName);
    }
}
