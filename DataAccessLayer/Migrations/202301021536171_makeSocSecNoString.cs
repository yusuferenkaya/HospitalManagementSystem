namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeSocSecNoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "socSecNo", c => c.String(maxLength: 11));
            AlterColumn("dbo.Patients", "patientSocSecNo", c => c.String(maxLength: 11));
            AlterColumn("dbo.Staffs", "stafSocSecNo", c => c.String(maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "stafSocSecNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Patients", "patientSocSecNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "socSecNo", c => c.Int(nullable: false));
        }
    }
}
