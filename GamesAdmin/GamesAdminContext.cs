using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

public class GamesAdminContext(DbContextOptions<GamesAdminContext> options) : DbContext(options)
{
    public DbSet<GamesAdmin.Models.Game> Game { get; set; } = default!;
    public DbSet<GamesAdmin.Models.Genre> Genre { get; set; } = default!;
    public DbSet<GamesAdmin.Models.AudienceRating> AudienceRating { get; set; } = default!;
}
