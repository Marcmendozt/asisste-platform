using Microsoft.EntityFrameworkCore;

namespace Asisste.ApiData.Persistence;

public sealed class LegacyAsissteDbContext : DbContext
{
    public LegacyAsissteDbContext(DbContextOptions<LegacyAsissteDbContext> options)
        : base(options)
    {
    }
}