using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dedsi.EntityFrameworkCore;

/// <summary>
/// Ulid Ef Core 转换器
/// </summary>
public class UlidConverter() : ValueConverter<Ulid, string>(v => v.ToString(), v => Ulid.Parse(v));