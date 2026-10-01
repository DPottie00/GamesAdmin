using Microsoft.EntityFrameworkCore;

public class GamesAdminContext(DbContextOptions<GamesAdminContext> options) : DbContext(options)
{
    public DbSet<GamesAdmin.Models.Game> Game { get; set; } = default!;
}
