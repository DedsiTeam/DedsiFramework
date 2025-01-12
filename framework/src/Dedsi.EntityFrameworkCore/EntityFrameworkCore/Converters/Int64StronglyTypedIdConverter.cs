using Dedsi.Ddd.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dedsi.EntityFrameworkCore;

/// <summary>
/// Int64StronglyTypedId
/// </summary>
public class Int64StronglyTypedIdConverter() : ValueConverter<Int64StronglyTypedId, Int64>(v => v.Id, v => new Int64StronglyTypedId(v));