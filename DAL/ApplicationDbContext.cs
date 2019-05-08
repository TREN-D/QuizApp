namespace DAL
{
    using DAL.Entities;
    using DAL.Interfaces;
    using System.Data.Entity;

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<URL> URLs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbContext DbContext => this;
    }
}