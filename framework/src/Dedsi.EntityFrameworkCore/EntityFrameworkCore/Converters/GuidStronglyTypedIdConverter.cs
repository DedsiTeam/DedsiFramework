using Dedsi.Ddd.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dedsi.EntityFrameworkCore;

/// <summary>
/// GuidStronglyTypedId
/// </summary>
public class GuidStronglyTypedIdConverter() : ValueConverter<GuidStronglyTypedId, Guid>(v => v.Id, v => new GuidStronglyTypedId(v));