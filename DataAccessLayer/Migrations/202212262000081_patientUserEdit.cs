namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientUserEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "userID", c => c.Int());
            CreateIndex("dbo.Patients", "userID");
            AddForeignKey("dbo.Patients", "userID", "dbo.Users", "userID");
            DropColumn("dbo.Users", "userType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "userType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Patients", "userID", "dbo.Users");
            DropIndex("dbo.Patients", new[] { "userID" });
            DropColumn("dbo.Patients", "userID");
        }
    }
}
