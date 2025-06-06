using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace DedsiIdentity.EntityFrameworkCore;

public static class DedsiIdentityDbContextModelCreatingExtensions
{
    public static void ConfigureProjectName(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}