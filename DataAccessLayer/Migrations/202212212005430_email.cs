namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "doctorEmail", c => c.String(maxLength: 50));
            AddColumn("dbo.Patients", "patientEmail", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "patientEmail");
            DropColumn("dbo.Doctors", "doctorEmail");
        }
    }
}
