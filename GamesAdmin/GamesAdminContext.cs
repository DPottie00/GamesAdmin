using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

public class GamesAdminContext(DbContextOptions<GamesAdminContext> options) : DbContext(options)
{
    public DbSet<GamesAdmin.Models.Game> Game { get; set; } = default!;
    public DbSet<GamesAdmin.Models.Genre> Genre { get; set; } = default!;
    public DbSet<GamesAdmin.Models.AudienceRating> AudienceRating { get; set; } = default!;
    public DbSet<GamesAdmin.Models.CriticReview> CriticReview { get; set; } = default!;
    public DbSet<GamesAdmin.Models.Platform> Platform { get; set; } = default!;
    public DbSet<GamesAdmin.Models.Developer> Developer { get; set; } = default!;
}
