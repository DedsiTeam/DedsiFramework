using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dedsi.EntityFrameworkCore;

public class UlidConverter : ValueConverter<Ulid, string>
{
    public UlidConverter() : base(v => v.ToString(), v => Ulid.Parse(v))
    {
    }
}