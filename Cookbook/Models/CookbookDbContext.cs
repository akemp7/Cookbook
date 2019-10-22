using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Models
{
  public class CookbookContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Recipe> Recipes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RecipeTag> RecipeTag { get; set; }

    public CookbookContext(DbContextOptions options) : base(options) { }
  }
}