namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicineNameToPrescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "medicineName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescriptions", "medicineName");
        }
    }
}
