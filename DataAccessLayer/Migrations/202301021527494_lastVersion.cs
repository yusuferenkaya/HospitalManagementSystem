namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastVersion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "patientID", "dbo.Patients");
            DropForeignKey("dbo.Staffs", "roomID", "dbo.Rooms");
            DropIndex("dbo.Staffs", new[] { "roomID" });
            DropIndex("dbo.Rooms", new[] { "patientID" });
            DropColumn("dbo.Staffs", "roomID");
            DropTable("dbo.Rooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        roomID = c.Int(nullable: false, identity: true),
                        roomAvailability = c.Boolean(nullable: false),
                        patientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.roomID);
            
            AddColumn("dbo.Staffs", "roomID", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "patientID");
            CreateIndex("dbo.Staffs", "roomID");
            AddForeignKey("dbo.Staffs", "roomID", "dbo.Rooms", "roomID", cascadeDelete: true);
            AddForeignKey("dbo.Rooms", "patientID", "dbo.Patients", "patientID", cascadeDelete: true);
        }
    }
}
