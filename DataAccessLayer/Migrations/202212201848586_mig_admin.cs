namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_admin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        adminID = c.Int(nullable: false, identity: true),
                        adminEmail = c.String(maxLength: 50),
                        adminPassword = c.String(maxLength: 50),
                        userID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.adminID)
                .ForeignKey("dbo.Users", t => t.userID, cascadeDelete: true)
                .Index(t => t.userID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "userID", "dbo.Users");
            DropIndex("dbo.Admins", new[] { "userID" });
            DropTable("dbo.Admins");
        }
    }
}
