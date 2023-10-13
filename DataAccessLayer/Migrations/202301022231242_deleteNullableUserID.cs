namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteNullableUserID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "userID", "dbo.Users");
            DropIndex("dbo.Patients", new[] { "userID" });
            AlterColumn("dbo.Patients", "userID", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "userID");
            AddForeignKey("dbo.Patients", "userID", "dbo.Users", "userID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "userID", "dbo.Users");
            DropIndex("dbo.Patients", new[] { "userID" });
            AlterColumn("dbo.Patients", "userID", c => c.Int());
            CreateIndex("dbo.Patients", "userID");
            AddForeignKey("dbo.Patients", "userID", "dbo.Users", "userID");
        }
    }
}
