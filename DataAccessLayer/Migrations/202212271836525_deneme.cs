namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentHourID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "AppointmentHourID");
            AddForeignKey("dbo.Appointments", "AppointmentHourID", "dbo.AppointmentHours", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "AppointmentHourID", "dbo.AppointmentHours");
            DropIndex("dbo.Appointments", new[] { "AppointmentHourID" });
            DropColumn("dbo.Appointments", "AppointmentHourID");
        }
    }
}
