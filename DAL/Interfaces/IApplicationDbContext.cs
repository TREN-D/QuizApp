using DAL.Entities;
using System;
using System.Data.Entity;

namespace DAL.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Admin> Admins { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<Option> Options { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Test> Tests { get; set; }
        DbSet<TestResult> TestResults { get; set; }
        DbSet<URL> URLs { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }

        DbContext DbContext { get; }
    }
}
