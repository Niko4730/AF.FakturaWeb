
namespace UdemyApp.Server.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rule>().HasData(
                new Rule
                {
                    Id= 1,
                    Title = "TestRule",
                    Description = "This is a test",
                    Occurrence = "Every damn day",
                    UserId = 1,
                },
                new Rule
                {
                    Id = 2,
                    Title = "TestRule1",
                    Description = "This is a test1",
                    Occurrence = "Every damn day1",
                    UserId = 2,
                }
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rule> Rules { get; set; }
    }
}
