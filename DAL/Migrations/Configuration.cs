namespace DAL.Migrations
{
    using DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            #region DefaultAdmin
            var admin = new Admin
            {
                Email = "mail.co",
                Password = "1234",
                Phone = "123123"
            };
            var existedAdmin = context.Admins.SingleOrDefault(ea => ea.Email == admin.Email);
            if (existedAdmin == null)
            {
                context.Admins.Add(admin);
                context.SaveChanges();
            }
            #endregion
            #region DefaultClient
            var client = new Client
            {
                Id = "AngularApp",
                Active = true,
                Name = "Angular application",
                RefreshTokenLifeTime = 1440
            };

            var existedClient = context.Clients.Find(client.Id);
            if(existedClient == null)
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
            #endregion

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
