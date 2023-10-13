namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makePhoneNoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "doctorPhoneNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "doctorPhoneNo", c => c.Int(nullable: false));
        }
    }
}
