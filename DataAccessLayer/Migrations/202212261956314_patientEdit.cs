namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientEdit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "userID", "dbo.Users");
            DropIndex("dbo.Patients", new[] { "userID" });
            DropColumn("dbo.Patients", "patientAge");
            DropColumn("dbo.Patients", "userID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "userID", c => c.Int());
            AddColumn("dbo.Patients", "patientAge", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "userID");
            AddForeignKey("dbo.Patients", "userID", "dbo.Users", "userID");
        }
    }
}
