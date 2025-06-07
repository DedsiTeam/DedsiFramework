using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace DedsiPermission.EntityFrameworkCore;

public static class DedsiPermissionDbContextModelCreatingExtensions
{
    public static void ConfigureProjectName(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}