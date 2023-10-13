namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admins", "userID", "dbo.Users");
            DropIndex("dbo.Admins", new[] { "userID" });
            AddColumn("dbo.Admins", "userRole", c => c.String(maxLength: 1));
            DropColumn("dbo.Admins", "userID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "userID", c => c.Int(nullable: false));
            DropColumn("dbo.Admins", "userRole");
            CreateIndex("dbo.Admins", "userID");
            AddForeignKey("dbo.Admins", "userID", "dbo.Users", "userID", cascadeDelete: true);
        }
    }
}
