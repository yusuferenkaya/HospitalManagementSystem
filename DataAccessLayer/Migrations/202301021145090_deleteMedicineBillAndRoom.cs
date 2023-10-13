namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteMedicineBillAndRoom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bills", "appointmentID", "dbo.Appointments");
            DropForeignKey("dbo.Medicines", "prescriptionID", "dbo.Prescriptions");
            DropIndex("dbo.Bills", new[] { "appointmentID" });
            DropIndex("dbo.Medicines", new[] { "prescriptionID" });
            DropTable("dbo.Bills");
            DropTable("dbo.Medicines");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        medicineID = c.Int(nullable: false, identity: true),
                        medicineName = c.String(maxLength: 20),
                        prescriptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.medicineID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        billID = c.Int(nullable: false, identity: true),
                        fee = c.Int(nullable: false),
                        appointmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.billID);
            
            CreateIndex("dbo.Medicines", "prescriptionID");
            CreateIndex("dbo.Bills", "appointmentID");
            AddForeignKey("dbo.Medicines", "prescriptionID", "dbo.Prescriptions", "prescriptionID", cascadeDelete: true);
            AddForeignKey("dbo.Bills", "appointmentID", "dbo.Appointments", "appointmentID", cascadeDelete: true);
        }
    }
}
