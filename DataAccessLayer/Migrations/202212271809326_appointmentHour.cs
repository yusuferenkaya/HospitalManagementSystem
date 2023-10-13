namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentHour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hour = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppointmentHours");
        }
    }
}
