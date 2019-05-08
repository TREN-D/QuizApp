namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addconstraintsforentities : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Email", c => c.String(maxLength: 100));
            AlterColumn("dbo.Admins", "Password", c => c.String(maxLength: 100));
            AlterColumn("dbo.Admins", "Phone", c => c.String(maxLength: 100, unicode: false));
            CreateIndex("dbo.Admins", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Admins", new[] { "Email" });
            AlterColumn("dbo.Admins", "Phone", c => c.String());
            AlterColumn("dbo.Admins", "Password", c => c.String());
            AlterColumn("dbo.Admins", "Email", c => c.String());
        }
    }
}
