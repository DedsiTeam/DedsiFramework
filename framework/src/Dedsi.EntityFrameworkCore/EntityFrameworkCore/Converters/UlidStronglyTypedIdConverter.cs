using Dedsi.Ddd.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dedsi.EntityFrameworkCore;

/// <summary>
/// UlidStronglyTypedId
/// </summary>
public class UlidStronglyTypedIdConverter() : ValueConverter<UlidStronglyTypedId, string>(v => v.Id.ToString(), v => new UlidStronglyTypedId(v));